using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Xml;
using System.Reflection;
using System.Web;

namespace Utility
{
    public enum ConfigFileType
    {
        WebConfig,
        AppConfig
    }

    /// <summary>
    /// Summary description for ReadWriteConfig.
    /// </summary>
    public class ReadWriteConfig
    {
        public string docName = String.Empty;
        private XmlNode node = null;
        private int _configType;
        public int ConfigType
        {
            get { return _configType; }
            set { _configType = value; }
        }

        #region SetValue
        public bool SetValue(string key, string value)
        {
            XmlDocument cfgDoc = new XmlDocument();
            loadConfigDoc(cfgDoc);
            // retrieve the connectionStrings node 
            node = cfgDoc.SelectSingleNode("//connectionStrings");
            if (node == null)
            {
                throw new InvalidOperationException("connectionStrings section not found");
            }
            try
            {
                // XPath select setting "add" element that contains this key     
                XmlElement addElem = (XmlElement)node.SelectSingleNode("//add[@name='" + key + "']");
                if (addElem != null)
                {
                    addElem.SetAttribute("connectionString", value);
                }
                // not found, so we need to add the element, name and connectionString  connectionString
                else
                {
                    XmlElement entry = cfgDoc.CreateElement("add");
                    entry.SetAttribute("name", key);
                    entry.SetAttribute("connectionString", value);
                    entry.SetAttribute("providerName", "System.Data.SqlClient");                    
                    node.AppendChild(entry);
                }
                //save it 
                saveConfigDoc(cfgDoc, docName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region saveConfigDoc
        private void saveConfigDoc(XmlDocument cfgDoc, string cfgDocPath)
        {
            try
            {
                XmlTextWriter writer = new XmlTextWriter(cfgDocPath, null);
                writer.Formatting = Formatting.Indented;
                cfgDoc.WriteTo(writer);
                writer.Flush();
                writer.Close();
                return;
            }
            catch
            {
                throw;
            }
        }

        public string readConfigDoc(string elementKey)
        {
            try
            {
                XmlDocument cfgDoc = new XmlDocument();
                loadConfigDoc(cfgDoc);
                // retrieve the appSettings node
                node = cfgDoc.SelectSingleNode("//connectionStrings");
                if (node == null)
                {
                    throw new InvalidOperationException("connectionStrings section not found");
                }
                XmlElement addElem = (XmlElement)node.SelectSingleNode("//add[@name='" + elementKey + "']");

                if (addElem != null)
                {
                    return addElem.GetAttribute("connectionString");
                }
                // not found, so we need to add the element, key and value 
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }
        #endregion

        #region removeElement
        public bool removeElement(string elementKey)
        {
            try
            {
                XmlDocument cfgDoc = new XmlDocument();
                loadConfigDoc(cfgDoc);
                // retrieve the appSettings node
                node = cfgDoc.SelectSingleNode("//connectionStrings");
                if (node == null)
                {
                    throw new InvalidOperationException("connectionStrings section not found");
                }
                // XPath select setting "add" element that contains this key to remove    
                node.RemoveChild(node.SelectSingleNode("//add[@name='" + elementKey + "']"));
                saveConfigDoc(cfgDoc, docName);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region modifyElement
        public bool modifyElement(string elementKey)
        {
            try
            {
                XmlDocument cfgDoc = new XmlDocument();
                loadConfigDoc(cfgDoc);
                // retrieve the appSettings node
                node = cfgDoc.SelectSingleNode("//connectionStrings");
                if (node == null)
                {
                    throw new InvalidOperationException("connectionStrings section not found");
                }
                // XPath select setting "add" element that contains this key to remove    
                node.RemoveChild(node.SelectSingleNode("//add[@name='" + elementKey + "']"));
                saveConfigDoc(cfgDoc, docName);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region loadConfigDoc
        private XmlDocument loadConfigDoc(XmlDocument cfgDoc)
        {
            // load the config file 
            if (System.Convert.ToInt32(ConfigType) == System.Convert.ToInt32(ConfigFileType.AppConfig))
            {
                docName = ((Assembly.GetEntryAssembly()).GetName()).Name;
                docName += ".exe.config";
            }
            else
            {
                docName = HttpContext.Current.Server.MapPath("~/Web.config"); //你的配置文件名称
            }
            cfgDoc.Load(docName);
            return cfgDoc;
        }
        #endregion
    }
    public class ConfigHelper
    {
        public static bool SetConfigValue(string Key ,string Value)
        {
            bool flag = false;
            ReadWriteConfig config = new ReadWriteConfig();
            flag = config.SetValue(Key, Value);
            return flag;
        }

    }
}
