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
    
    public partial class shop
    {
        public int id { get; set; }
        public string name { get; set; }
        public short brandid { get; set; }
        public short province_areaid { get; set; }
        public short city_areaid { get; set; }
        public short county_areaid { get; set; }
        public string address { get; set; }
        public double longitude { get; set; }
        public double latitude { get; set; }
        public string linkman { get; set; }
        public string telphone { get; set; }
        public bool status { get; set; }
        public System.DateTime c_datetime { get; set; }
    }
}
