using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml;

namespace LW.Utility
{
    public class ConfigHelper
    {
        #region 根据键名获取配置文件中的值
        /// <summary>
        /// 根据键名获取配置文件中的值
        /// </summary>
        /// <param name="key">键名</param>
        /// <returns>返回值</returns>
        public static string GetValue(string key)
        {
            string value = string.Empty;

            if (ConfigurationManager.AppSettings[key] != null)
            {
                value = ConfigurationManager.AppSettings[key] as string;
            }

            return value;
        }
        #endregion

        #region 设置配置文件的键值
        /// <summary>
        /// 设置配置文件的键值
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">值</param>
        public static void UpdateConfigValueOfKey(string key, string value)
        {
            XmlDocument doc = new XmlDocument();

            //获取配置文件的全路径
            string strFileName = AppDomain.CurrentDomain.BaseDirectory.ToString() + "Web.config";

            doc.Load(strFileName);

            //找到add的所有节点

            XmlNodeList nodes = doc.GetElementsByTagName("add");
            for (int i = 0; i < nodes.Count; i++)
            {
                //获得将当前元素的key属性


                XmlAttribute attr = nodes[i].Attributes["key"];
                //根据元素的第一个属性来判断当前的元素是不是目标元素
                if (attr != null && attr.Value.ToString().Equals(key))
                {
                    //对目标元素中的第二个属性赋值

                    attr = nodes[i].Attributes["value"];
                    attr.Value = value;
                    break;
                }
            }

            doc.Save(strFileName);
        }
        #endregion
    }
}
