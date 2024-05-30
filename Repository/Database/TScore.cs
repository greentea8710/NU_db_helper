using Microsoft.EntityFrameworkCore;
using Repository.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Database
{
    /// <summary>
    /// 成績表
    /// </summary>
    public class TScore:CD
    {
        public long StudentId { get; set; }
        public virtual TStudent Student { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public DateOnly ScoreDate { get; set; }


       public long SubjectId {  get; set; }
       public virtual TSubject Subject { get; set; }


        /// <summary>
        /// 成績
        /// </summary>
        [Precision(5,2)]
        public decimal? Score {get; set; }
    }
}
