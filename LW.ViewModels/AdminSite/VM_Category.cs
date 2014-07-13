using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LW.ViewModels.AdminSite
{
    public class VM_Category
    {
        public int cid { get; set; }
        public string cname { get; set; }
        public int pcid { get; set; }
        public string cpath { get; set; }
        public byte ctype { get; set; }
        public int sortid { get; set; }
        public sbyte cstatu { get; set; }
        public string imgurl { get; set; }
        public string description { get; set; }

        public string state { get; set; }

        public List<VM_Category> children { get; set; }
    }
}
