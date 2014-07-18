using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#region 命名空间
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using LW.Utility;
#endregion

namespace LW.ViewModels.WebSite
{
    public class VM_Register : VM_Base
    {
        [Required(ErrorMessage = "请填写您的邮箱！")]
        [RegularExpression(RegularHelper.Email, ErrorMessage = "请填写正确的邮箱！")]
        [Remote("ExistAccount", "Service", ErrorMessage = "该邮箱已存在！")]
        public string UserMail { get; set; }

        [Required(ErrorMessage = "请填写您的密码！")]
        [RegularExpression(RegularHelper.Password, ErrorMessage = "请填写6-16位密码(字母、数字、下划线)！")]
        public string Password { get; set; }

        [Required(ErrorMessage = "请填写验证码！")]
        [RegularExpression(RegularHelper.ValidCode,ErrorMessage="请填写4位验证码(字母、数字)！")]
        public string ValidCode { get; set; }
    }
}
