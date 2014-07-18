using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LW.Utility
{
    public class RegularHelper
    {
        /// <summary>
        /// 常用邮箱验证
        /// </summary>
        public const string Email = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

        /// <summary>
        /// 常用密码验证
        /// </summary>
        public const string Password = @"^[A-Za-z0-9_]{6,16}$";

        /// <summary>
        /// 常用验证码验证
        /// </summary>
        public const string ValidCode = @"^[A-Za-z0-9]{4}$";
    }
}
