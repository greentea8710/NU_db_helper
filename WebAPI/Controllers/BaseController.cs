using Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Repository.Database;
using WebAPI.Models.Shared;

namespace WebAPI.Controllers
{
    /// <summary>
    /// 系统基础方法控制器
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class BaseController(DatabaseContext db, IDHelper idHelper, IDistributedCache distributedCache) : ControllerBase
    {



        /// <summary>
        /// 二维码生成
        /// </summary>
        /// <param name="text">数据内容</param>
        /// <returns></returns>
        [HttpGet]
        public FileResult GetQrCode(string text)
        {
            var image = ImgHelper.GetQrCode(text);
            return File(image, "image/png");
        }




        /// <summary>
        /// 图像验证码生成
        /// </summary>
        /// <param name="sign">标记</param>
        /// <returns></returns>
        [HttpGet]
        public FileResult GetVerifyCode(Guid sign)
        {
            var cacheKey = "VerifyCode" + sign.ToString();
            Random random = new();
            string text = random.Next(1000, 9999).ToString();

            var image = ImgHelper.GetVerifyCode(text);

            distributedCache.Set(cacheKey, text, TimeSpan.FromMinutes(5));

            return File(image, "image/png");
        }



        /// <summary>
        /// 获取指定组ID的KV键值对
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        [HttpGet]
        public List<DtoKeyValue> GetValueList(long groupId)
        {

            var list = db.TAppSetting.Where(t => t.Module == "Dictionary" && t.GroupId == groupId).Select(t => new DtoKeyValue
            {
                Key = t.Key,
                Value = t.Value
            }).ToList();

            return list;
        }



        /// <summary>
        /// 获取一个雪花ID
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public long GetSnowflakeId()
        {
            return idHelper.GetId();
        }


    }
}