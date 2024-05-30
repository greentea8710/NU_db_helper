using Repository.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Database
{

    /// <summary>
    /// 學生表
    /// </summary>
    public class TStudent:CD
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 座號
        /// </summary>
        public int Number { get; set; }


        /// <summary>
        /// 性別
        /// </summary>
        public bool Gender {  get; set; }
        


        /// <summary>
        /// 手機號碼
        /// </summary>
        public string? Phone { get; set; }



        public long ClassId { get; set; }
        public virtual TClass Class { get; set; }


       public virtual List<TScore> TScore { get; set; }
    }
}
