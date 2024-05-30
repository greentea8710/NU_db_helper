using AdminAPI.Filters;
using AdminShared.Models;
using Common;
using Microsoft.AspNetCore.Mvc;
using Repository.Database;

namespace AdminAPI.Controllers
{
    /// <summary>
    /// 系统基础方法控制器
    /// </summary>
    [SignVerifyFilter]
    [Route("[controller]/[action]")]
    [ApiController]
    public class BaseController(DatabaseContext db, IDHelper idHelper) : ControllerBase
    {



 



        /// <summary>
        /// 自定义二维码生成方法
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