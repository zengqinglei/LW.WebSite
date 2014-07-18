using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Diagnostics;
using LW.Utility;

namespace LW.Controllers.WebSite
{
    public class TestController : ApiController
    {
        #region 测试get
        public string GetAll()
        {
            return "获取所有值成功！";
        }
        public string GetOne(int id)
        {
            return string.Format("获取值:{0}", id);
        }
        public string PostAdd(int id)
        {
            return string.Format("添加{0}成功！", id);
        }
        #endregion

        #region 测试文件上传
        public async Task<HttpResponseMessage> PostUpload()
        {
            Logger.WriteLog("1");
            // Check if the request contains multipart/form-data.
            // 检查该请求是否含有multipart/form-data
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            Logger.WriteLog("2");
            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            Logger.WriteLog("3");
            try
            {
                // Read the form data.
                // 读取表单数据
                await Request.Content.ReadAsMultipartAsync(provider);

                Logger.WriteLog("4");
                // This illustrates how to get the file names.
                // 以下描述如何获取文件名
                foreach (MultipartFileData file in provider.FileData)
                {
                    Logger.WriteLog("5");
                    Trace.WriteLine(file.Headers.ContentDisposition.FileName);
                    Trace.WriteLine("Server file path: " + file.LocalFileName);
                }
                Logger.WriteLog("6");
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                Logger.WriteLog(e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
        #endregion
    }
}
