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
            var validCode = Util.GetRandomAlphanumeric();

            byte[] image = ImageHelper.MakeValidateGraphic(validCode);

            B_Service.AddValidCode(validCode);

            return File(image, "image/jpeg");
        }
        #endregion

        #region 公共服务--if exists nickname,email
        [HttpGet]
        public ActionResult ExistAccount(string nickname = null, string usermail = null)
        {
            var flag = !new B_User().ExistNickname(nickname: nickname, usermail: usermail);

            return Json(flag, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
