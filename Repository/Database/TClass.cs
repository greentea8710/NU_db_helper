using Repository.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Database
{
    /// <summary>
    /// 班級表
    /// </summary>
    public class TClass:CD
    {
        /// <summary>
        /// 班級號碼
        /// </summary>
        public int Number { get; set; }


       

        /// <summary>
        /// 年級
        /// </summary>
        public int Grade {  get; set; }

        
        public virtual List<TStudent> TStudent { get; set; }
    }
}
