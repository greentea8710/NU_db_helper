using Repository.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Database
{
    /// <summary>
    /// 信件資訊
    /// </summary>
    public class TMail:CD
    {

        public long MailId { get; set; }

        /// <summary>
        /// 信件內文
        /// </summary>
        public string Content { get; set; }


        /// <summary>
        /// 寄件者
        /// </summary>
        public string SenderEmail { get; set; }


        /// <summary>
        /// 收件者
        /// </summary>
        public string ReceiverEmail { get; set; }



        /// <summary>
        /// 標題
        /// </summary>
        public string Subject { get; set; }



        /// <summary>
        /// 成功寄送時間
        /// </summary>
        public DateTimeOffset Time {  get; set; } 


        /// <summary>
        /// 是否寄送成功
        /// </summary>
        public bool Success { get; set; }
    }
}
