using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LW.ViewModels.AdminSite
{
    public class VM_Box
    {
        public int? boxid { get; set; }
        public string name { get; set; }
        public string name_modifier { get; set; }
        public byte category { get; set; }
        public string pic { get; set; }
        public string pic_big { get; set; }
        public Nullable<int> quantity { get; set; }
        public System.DateTime? starttime { get; set; }
        public System.DateTime? endtime { get; set; }
        public System.DateTime? addtime { get; set; }
        public short box_price { get; set; }
        public int member_price { get; set; }
        public string box_intro { get; set; }
        public string box_remark { get; set; }
        public string box_senddate { get; set; }
        public Nullable<bool> state { get; set; }
        public bool only_newuser { get; set; }
        public bool only_member { get; set; }
        public bool if_repeat { get; set; }
        public bool if_use_coupon { get; set; }
        public sbyte if_give_coupon { get; set; }
        public sbyte if_give_member { get; set; }
        public System.DateTime? coupon_valid_date { get; set; }
        public string boxcost { get; set; }
        public bool icontype { get; set; }
        public bool ifshowtime { get; set; }
        public int toptime { get; set; }
        public string special_url { get; set; }
        public int if_hidden { get; set; }
    }
}
