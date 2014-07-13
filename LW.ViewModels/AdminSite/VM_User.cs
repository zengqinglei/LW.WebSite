using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LW.ViewModels.AdminSite
{
    public class VM_User:VM_Base
    {
        public string addtime { get; set; }
        public short blog_num { get; set; }
        public short collect_num { get; set; }
        public short coupon_num { get; set; }
        public short evaluate_num { get; set; }
        public int experience { get; set; }
        public short fans_num { get; set; }
        public short follow_num { get; set; }
        public bool if_super { get; set; }
        public int invite_uid { get; set; }
        public string invitecode { get; set; }
        public bool is_mobile { get; set; }
        public bool is_solution { get; set; }
        public bool is_spreader { get; set; }
        public string know_way { get; set; }
        public string nickname { get; set; }
        public short order_num { get; set; }
        public int score { get; set; }
        public bool state { get; set; }
        public bool tel_status { get; set; }
        public long? userid { get; set; }
        public string password { get; set; }
        public string usermail { get; set; }
    }
}
