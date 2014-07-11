using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LW.ViewModels
{
    public class VM_MLogin:VM_Base
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public string ValidCode { get; set; }
    }
}
