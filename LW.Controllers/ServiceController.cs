using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#region 命名空间
using System.Web.Mvc;
using LW.Utility;
using LW.Business;
#endregion

namespace LW.Controllers
{
    public class ServiceController : Controller
    {
        #region 公共服务--获取验证码
        [HttpGet]
        public ActionResult GetValidCode(string sessionKey)
        {
            int number;
            char code;
            string StrCode = String.Empty;

            Random random = new Random();
            for (int i = 0; i < 4; i++)
            {
                number = random.Next();

                if (number % 2 == 0)
                {
                    code = (char)('0' + (char)(number % 10));
                }
                else
                {
                    code = (char)('A' + (char)(number % 26));
                }
                StrCode += code.ToString();
            }

            byte[] image = ImageHelper.MakeValidateGraphic(StrCode);

            B_Service.AddValidCode(StrCode);

            return File(image, "image/jpeg");
        }
        #endregion
    }
}
