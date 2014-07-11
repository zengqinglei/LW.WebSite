using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LW.ViewModels
{
    public class Result
    {
        public int status { get; set; }
        public object data { get; set; }
        public string msg { get; set; }

        public Result()
        {
        }
        public Result(string error)
        {
            this.msg = error;
        }
    }
}
