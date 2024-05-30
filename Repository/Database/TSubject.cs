using NPOI.SS.Formula.Functions;
using Repository.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Database
{
    /// <summary>
    /// 科目表
    /// </summary>
    public class TSubject:CD
    {
        /// <summary>
        /// 科目名稱
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 授課老師
        /// </summary>
        public string Teacher { get; set; }



        public virtual List<TScore> TScore { get; set; }
    }
}
