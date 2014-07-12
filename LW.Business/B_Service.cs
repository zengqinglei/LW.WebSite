using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
#region 命名空间
using LW.EFEntity;
#endregion

namespace LW.Business
{
    public class B_Service
    {
        #region 静态变量
        public static string SessionKey_ValidCode = "ValidCode";
        #endregion        

        #region 验证码管理--将验证码添加到session中
        public static void AddValidCode(string code)
        {
            HttpContext.Current.Session[SessionKey_ValidCode] = code;
        }
        #endregion

        #region 验证码管理--从session获取验证码
        public static string GetValidCode()
        {
            object code = HttpContext.Current.Session[SessionKey_ValidCode];
            if (code == null)
            {
                return null;
            }
            return code.ToString();
        }
        #endregion
    }
}
