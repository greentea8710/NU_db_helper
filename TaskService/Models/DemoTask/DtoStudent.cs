using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskService.Models.DemoTask
{
    public class DtoStudent
    {

        public string StudentId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }




        public List<ScoreInfo> ScoreInfos { get; set; }

        public class ScoreInfo
        {

            public long Id { get; set; }
            public string SubjectName { get; set; }


            public DateOnly ScoreDate { get; set; }

            public decimal? Score { get; set; }
        }
    }
}
