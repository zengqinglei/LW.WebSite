//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace LW.EFEntity
{
    using System;
    using System.Collections.Generic;
    
    public partial class stat_user_order
    {
        public System.DateTime statdate { get; set; }
        public int ordernum { get; set; }
        public short ordernum_exchange { get; set; }
        public short ordernum_postage { get; set; }
        public int registernum { get; set; }
        public int sales { get; set; }
        public decimal sales_exchange { get; set; }
        public decimal sales_postage { get; set; }
        public int discount { get; set; }
        public decimal totalvalue { get; set; }
        public decimal totalvalue_all { get; set; }
        public int totalprice { get; set; }
        public int addtime { get; set; }
    }
}
