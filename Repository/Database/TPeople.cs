using Repository.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Database
{

    /// <summary>
    /// 人員表
    /// </summary>
    public class TPeople:CD
    {
        public long PeopleId {  get; set; }
        

        /// <summary>
        /// 人員名字
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 信箱
        /// </summary>
        public string Account { get; set; }
    }
}
