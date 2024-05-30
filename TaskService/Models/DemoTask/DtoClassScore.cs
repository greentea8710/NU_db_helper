using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskService.Models.DemoTask
{
    public class DtoClassScore
    {

        public string ClassName {  get; set; }
        public long ClassId { get; set; }


        public List<DtoStudent> Students { get; set; }

    }


}
