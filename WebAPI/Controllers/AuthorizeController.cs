﻿using Common;
using DistributedLock;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Repository.Database;
using SMS;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Xml;
using WebAPI.Filters;
using WebAPI.Libraries;
using WebAPI.Models.AppSetting;
using WebAPI.Models.Authorize;
using WebAPI.Models.Shared;
using WebAPI.Services;

namespace WebAPI.Controllers
{


    /// <summary>
    /// 系统访问授权模块
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {


        private readonly DatabaseContext db;
        private readonly IDistributedLock distLock;
        private readonly IDHelper idHelper;

        private readonly IDistributedCache distributedCache;

        private readonly AuthorizeService authorizeService;

        private readonly long userId;




        public AuthorizeController(DatabaseContext db, IDistributedLock distLock, IDHelper idHelper, IDistributedCache distributedCache, AuthorizeService authorizeService, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this.distLock = distLock;
            this.idHelper = idHelper;
            this.distributedCache = distributedCache;

            this.authorizeService = authorizeService;

            var userIdStr = httpContextAccessor.HttpContext?.GetClaimByUser("userId");

            if (userIdStr != null)
            {
                userId = long.Parse(userIdStr);
            }
        }



        [HttpPost]
        public bool SendMail(DtoSendEmail sendEmail)
        {
            try
            {

                string Account = "greentea8710@gmail.com";
                string Password = "vwfw zawo cjhb ommo";
                SmtpClient client = new()
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(Account, Password),
                    EnableSsl = true
                };

                TMail mail = new()
                {
                    Id = idHelper.GetId(),
                    MailId = idHelper.GetId(),
                    Content = sendEmail.Body,
                    SenderEmail = "greentea8710@gmail.com",
                    ReceiverEmail = sendEmail.ToAddress,
                    Subject = sendEmail.Title,
                    Time = DateTimeOffset.UtcNow,
                    Success = false
                };
                db.TMail.Add(mail);
                db.SaveChanges();

                MailMessage message = new(mail.SenderEmail, mail.ReceiverEmail, mail.Subject, mail.Content);
                client.Send(message);

                return true;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// 获取公钥
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public string GetPublicKey(IConfiguration configuration)
        {
            var rsaSetting = configuration.GetRequiredSection("RSA").Get<RSASetting>();

            if (rsaSetting != null)
            {
                return rsaSetting.PublicKey;
            }
            else
            {
                throw new Exception("RSA公钥加载异常");
            }
        }



        /// <summary>
        /// 获取Token认证信息
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        public string? GetToken(DtoGetToken login)
        {
            var userList = db.TUser.Where(t => t.UserName == login.UserName).Select(t => new { t.Id, t.PassWord }).ToList();

            var user = userList.Where(t => t.PassWord == Convert.ToBase64String(KeyDerivation.Pbkdf2(login.PassWord, Encoding.UTF8.GetBytes(t.Id.ToString()), KeyDerivationPrf.HMACSHA256, 1000, 32))).FirstOrDefault();

            if (user != null)
            {
                return authorizeService.GetTokenByUserId(user.Id);
            }
            else
            {
                HttpContext.SetErrMsg("用户名或密码错误");
                return default;
            }
        }



        /// <summary>
        /// 通过微信小程序Code获取Token认证信息
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        public string? GetTokenByWeiXinMiniApp([FromBody] DtoGetTokenByWeiXinApp login)
        {
            var (openId, sessionKey) = authorizeService.GetWeiXinMiniAppOpenIdAndSessionKey(login.AppId, login.Code);

            var userIdQuery = db.TUserBindExternal.Where(t => t.AppName == "WeiXinMiniApp" && t.AppId == login.AppId && t.OpenId == login.AppId).Select(t => t.User.Id);

            var userId = userIdQuery.FirstOrDefault();

            if (userId == default)
            {

                using (distLock.Lock("GetTokenByWeiXinMiniAppCode" + openId))
                {
                    userId = userIdQuery.FirstOrDefault();

                    if (userId == default)
                    {
                        string userName = DateTime.UtcNow.ToString() + "微信小程序新用户";

                        //注册一个只有基本信息的账户出来
                        TUser user = new()
                        {
                            Id = idHelper.GetId(),
                            Name = userName,
                            UserName = Guid.NewGuid().ToString(),
                            Phone = ""
                        };
                        user.PassWord = Convert.ToBase64String(KeyDerivation.Pbkdf2(Guid.NewGuid().ToString(), Encoding.UTF8.GetBytes(user.Id.ToString()), KeyDerivationPrf.HMACSHA256, 1000, 32));

                        db.TUser.Add(user);

                        db.SaveChanges();

                        TUserBindExternal userBind = new()
                        {
                            Id = idHelper.GetId(),
                            UserId = user.Id,
                            AppName = "WeiXinMiniApp",
                            AppId = login.AppId,
                            OpenId = openId
                        };

                        db.TUserBindExternal.Add(userBind);

                        db.SaveChanges();

                        userId = user.Id;
                    }

                }

            }

            if (userId != default)
            {
                return authorizeService.GetTokenByUserId(userId);

            }
            else
            {
                HttpContext.SetErrMsg("获取Token失败");

                return null;
            }
        }




        /// <summary>
        /// 利用手机号和短信验证码获取Token认证信息
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        public string? GetTokenBySMS(DtoGetTokenBySMS login)
        {
            string key = "VerifyPhone_" + login.Phone;

            var code = distributedCache.GetString(key);

            if (string.IsNullOrEmpty(code) == false && code == login.VerifyCode)
            {
                var userId = db.TUser.Where(t => t.Phone == login.Phone).Select(t => t.Id).FirstOrDefault();

                if (userId == default)
                {
                    //注册一个只有基本信息的账户出来
                    string userName = DateTime.UtcNow.ToString() + "手机短信新用户";

                    TUser user = new()
                    {
                        Id = idHelper.GetId(),
                        Name = userName,
                        UserName = Guid.NewGuid().ToString(),
                        Phone = login.Phone
                    };
                    user.PassWord = Convert.ToBase64String(KeyDerivation.Pbkdf2(Guid.NewGuid().ToString(), Encoding.UTF8.GetBytes(user.Id.ToString()), KeyDerivationPrf.HMACSHA256, 1000, 32));

                    db.TUser.Add(user);

                    db.SaveChanges();

                    userId = user.Id;
                }

                if (userId != default)
                {
                    return authorizeService.GetTokenByUserId(userId);
                }
                else
                {
                    HttpContext.SetErrMsg("系统异常暂时无法登录");

                    return default;
                }
            }
            else
            {
                HttpContext.SetErrMsg("短信验证码错误");

                return default;
            }
        }




        /// <summary>
        /// 获取授权功能列表
        /// </summary>
        /// <param name="sign">模块标记</param>
        /// <returns></returns>
        [Authorize]
        [CacheDataFilter(TTL = 60, IsUseToken = true)]
        [HttpGet]
        public List<DtoKeyValue> GetFunctionList(string sign)
        {

            var roleIds = db.TUserRole.AsNoTracking().Where(t => t.UserId == userId).Select(t => t.RoleId).ToList();

            var kvList = db.TFunctionAuthorize.Where(t => (roleIds.Contains(t.RoleId!.Value) || t.UserId == userId) && t.Function.Parent!.Sign == sign).Select(t => new DtoKeyValue
            {
                Key = t.Function.Sign,
                Value = t.Function.Name
            }).ToList();

            return kvList;
        }




        /// <summary>
        /// 发送短信验证手机号码所有权
        /// </summary>
        /// <param name="sms"></param>
        /// <param name="sendVerifyCode"></param>
        /// <returns></returns>
        [HttpPost]
        public bool SendSMSVerifyCode([FromServices] ISMS sms, DtoSendSMSVerifyCode sendVerifyCode)
        {
            string key = "VerifyPhone_" + sendVerifyCode.Phone;

            if (distributedCache.IsContainKey(key) == false)
            {
                Random ran = new();
                string code = ran.Next(100000, 999999).ToString();

                Dictionary<string, string> templateParams = new()
                {
                    { "code", code }
                };

                sms.SendSMS("短信签名", sendVerifyCode.Phone, "短信模板编号", templateParams);

                distributedCache.Set(key, code, new TimeSpan(0, 0, 5, 0));

                return true;
            }
            else
            {
                HttpContext.SetErrMsg("请勿频繁获取验证码！");

                return false;
            }

        }



        /// <summary>
        /// 通过微信App Code获取Token认证信息
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        public string? GetTokenByWeiXinApp(DtoGetTokenByWeiXinApp login)
        {
            var (accessToken, openId) = authorizeService.GetWeiXinAppAccessTokenAndOpenId(login.AppId, login.Code);

            var userInfo = authorizeService.GetWeiXinAppUserInfo(accessToken, openId);

            if (userInfo.NickName != null)
            {
                var user = db.TUserBindExternal.AsNoTracking().Where(t => t.AppName == "WeiXinApp" && t.AppId == login.AppId && t.OpenId == userInfo.OpenId).Select(t => t.User).FirstOrDefault();

                if (user == null)
                {

                    user = new()
                    {
                        Id = idHelper.GetId(),
                        IsDelete = false,
                        Name = userInfo.NickName,
                        UserName = Guid.NewGuid().ToString(),
                        Phone = ""
                    };
                    user.PassWord = Convert.ToBase64String(KeyDerivation.Pbkdf2(Guid.NewGuid().ToString(), Encoding.UTF8.GetBytes(user.Id.ToString()), KeyDerivationPrf.HMACSHA256, 1000, 32));

                    db.TUser.Add(user);
                    db.SaveChanges();

                    TUserBindExternal bind = new()
                    {
                        Id = idHelper.GetId(),
                        AppName = "WeiXinApp",
                        AppId = login.AppId,
                        OpenId = openId,

                        UserId = user.Id
                    };

                    db.TUserBindExternal.Add(bind);

                    db.SaveChanges();
                }

                return authorizeService.GetTokenByUserId(user.Id);
            }

            HttpContext.SetErrMsg("微信授权失败");

            return default;

        }




        /// <summary>
        /// 通过老密码修改密码
        /// </summary>
        /// <param name="updatePassWord"></param>
        /// <returns></returns>
        [Authorize]
        [QueueLimitFilter(IsBlock = true, IsUseParameter = false, IsUseToken = true)]
        [HttpPost]
        public bool UpdatePassWordByOldPassWord(DtoUpdatePassWordByOldPassWord updatePassWord)
        {

            var user = db.TUser.Where(t => t.Id == userId).FirstOrDefault();

            if (user != null)
            {
                if (user.PassWord == Convert.ToBase64String(KeyDerivation.Pbkdf2(updatePassWord.OldPassWord, Encoding.UTF8.GetBytes(user.Id.ToString()), KeyDerivationPrf.HMACSHA256, 1000, 32)))
                {
                    user.PassWord = Convert.ToBase64String(KeyDerivation.Pbkdf2(updatePassWord.NewPassWord, Encoding.UTF8.GetBytes(user.Id.ToString()), KeyDerivationPrf.HMACSHA256, 1000, 32));
                    user.UpdateUserId = user.Id;
                    db.SaveChanges();

                    return true;
                }
                else
                {
                    HttpContext.SetErrMsg("原始密码验证失败");

                    return false;
                }
            }
            else
            {
                HttpContext.SetErrMsg("账户异常，请联系后台工作人员");

                return false;
            }

        }



        /// <summary>
        /// 通过短信验证码修改账户密码</summary>
        /// <param name="updatePassWord"></param>
        /// <returns></returns>
        [Authorize]
        [QueueLimitFilter(IsBlock = true, IsUseParameter = false, IsUseToken = true)]
        [HttpPost]
        public bool UpdatePassWordBySMS(DtoUpdatePassWordBySMS updatePassWord)
        {

            string phone = db.TUser.Where(t => t.Id == userId).Select(t => t.Phone).FirstOrDefault()!;

            string key = "VerifyPhone_" + phone;

            var code = distributedCache.GetString(key);


            if (string.IsNullOrEmpty(code) == false && code == updatePassWord.SmsCode)
            {
                var user = db.TUser.Where(t => t.Id == userId).FirstOrDefault();

                if (user != null)
                {
                    user.PassWord = Convert.ToBase64String(KeyDerivation.Pbkdf2(updatePassWord.NewPassWord, Encoding.UTF8.GetBytes(user.Id.ToString()), KeyDerivationPrf.HMACSHA256, 1000, 32));
                    user.UpdateUserId = userId;

                    var tokenList = db.TUserToken.Where(t => t.UserId == userId).ToList();

                    db.TUserToken.RemoveRange(tokenList);

                    db.SaveChanges();

                    return true;
                }
                else
                {
                    HttpContext.SetErrMsg("账户不存在");

                    return false;
                }
            }
            else
            {
                HttpContext.SetErrMsg("短信验证码错误");

                return false;
            }

        }




        /// <summary>
        /// 生成密码
        /// </summary>
        /// <param name="passWord"></param>
        /// <returns></returns>
        [HttpGet]
        public DtoKeyValue GeneratePassWord(string passWord)
        {
            DtoKeyValue keyValue = new()
            {
                Key = idHelper.GetId()
            };

            keyValue.Value = Convert.ToBase64String(KeyDerivation.Pbkdf2(passWord, Encoding.UTF8.GetBytes(keyValue.Key.ToString()!), KeyDerivationPrf.HMACSHA256, 1000, 32));

            return keyValue;
        }




        /// <summary>
        /// 更新路由信息表
        /// </summary>
        /// <param name="actionDescriptorCollectionProvider"></param>
        [HttpGet]
        public void UpdateRoute(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            var actionList = actionDescriptorCollectionProvider.ActionDescriptors.Items.Cast<ControllerActionDescriptor>().Select(x => new
            {
                Name = x.DisplayName![..(x.DisplayName!.IndexOf('(') - 1)],
                Route = x.AttributeRouteInfo!.Template,
                IsAuthorize = (x.EndpointMetadata.Where(t => t.GetType().FullName == "Microsoft.AspNetCore.Authorization.AuthorizeAttribute").Any() == true && x.EndpointMetadata.Where(t => t.GetType().FullName == "Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute").Any() == false),
            }).ToList();

            string projectName = typeof(Program).Assembly.GetName().Name!;

            XmlDocument xml = new();
            xml.Load(AppContext.BaseDirectory + projectName + ".xml");
            XmlNodeList memebers = xml.SelectNodes("/doc/members/member")!;

            Dictionary<string, string> remarksDict = [];


            for (int c = 0; c < memebers.Count; c++)
            {
                var xmlNode = memebers[c];

                if (xmlNode != null)
                {
                    if (xmlNode.Attributes!["name"]!.Value.StartsWith("M:" + projectName + ".Controllers."))
                    {
                        for (int s = 0; s < xmlNode.ChildNodes.Count; s++)
                        {
                            var childNode = xmlNode.ChildNodes[s];

                            if (childNode != null && childNode.Name == "summary")
                            {
                                string name = xmlNode.Attributes!["name"]!.Value;

                                string summary = childNode.InnerText;

                                name = name![2..];

                                if (name.Contains('(', StringComparison.CurrentCulture))
                                {
                                    name = name[..name.IndexOf('(')];
                                }

                                summary = summary.Replace("\n", "").Trim();

                                remarksDict.Add(name, summary);
                            }
                        }
                    }
                }
            }


            actionList = actionList.Where(t => t.IsAuthorize == true).Distinct().ToList();


            var functionRoutes = db.TFunctionRoute.Where(t => t.Module == projectName).ToList();

            var delList = functionRoutes.Where(t => actionList.Select(t => t.Route).ToList().Contains(t.Route) == false).ToList();

            foreach (var item in delList)
            {
                item.IsDelete = true;
            }

            foreach (var item in actionList)
            {
                var info = functionRoutes.Where(t => t.Route == item.Route).FirstOrDefault();

                string? remarks = remarksDict.Where(a => a.Key == item.Name).Select(a => a.Value).FirstOrDefault();

                if (info != null)
                {
                    info.Remarks = remarks;
                }
                else
                {
                    TFunctionRoute functionRoute = new()
                    {
                        Id = idHelper.GetId(),
                        Module = projectName,
                        Route = item.Route!,
                        Remarks = remarks
                    };

                    db.TFunctionRoute.Add(functionRoute);
                }
            }

            db.SaveChanges();

        }

    }
}
