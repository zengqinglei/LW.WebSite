using System;
using System.Collections.Generic;
using System.Text;
#region 命名空间
using System.IO;
#endregion


namespace LW.Utility
{
    /// <summary>
    /// 日志类(常用的都是用log4net，这里简陋地实现一个写入文本日志类)
    /// </summary>
    public class Logger
    {
        private static readonly object LogLock = new object();

        public static void WriteLog(Exception ex)
        {
            // 写入日志
            StringBuilder sbError = new StringBuilder();
            sbError.AppendFormat("异常方法：{0}\r\n", ex.TargetSite);
            sbError.AppendFormat("异常对象：{0}\r\n", ex.Source);
            sbError.AppendFormat("错误信息：{0}\r\n", ex.Message);
            sbError.AppendFormat("错误堆栈：{0}", ex.StackTrace);
            Logger.WriteLog(sbError.ToString());
        }

        /// <summary>
        /// 写入异常日志
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteLog(string errorMsg)
        {
            lock (LogLock)
            {
                string logFile = String.Format("{0}LogFile\\{1}.txt", AppDomain.CurrentDomain.BaseDirectory, DateTime.Now.ToString("yyyy-MM-dd"));
                if (!Directory.Exists(Path.GetDirectoryName(logFile)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(logFile));
                }
                StreamWriter sw = new StreamWriter(logFile, true);
                try
                {
                    sw.WriteLine("-----------------------------------------------------------");
                    sw.WriteLine(string.Format("时间：{0}", DateTime.Now.ToString()));
                    sw.WriteLine(string.Format("{0}\r\n", errorMsg));
                    sw.Flush();
                }
                finally
                {
                    sw.Close();
                }
            }
        }
    }
}
