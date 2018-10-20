using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace UtilsHelper.XmlHelper
{
    /// <summary>
    /// xml帮助api
    /// 通过LoadXml接口获取XMLDocument，然后进行操作，所有的操作都没有进行保存的，要保存自己调用保存接口
    /// </summary>
    public static class XmlHelper
    {
        /// <summary>
        /// 获取单个node
        /// </summary>
        /// <param name="xmlDocument"></param>
        /// <param name="nodeText"></param>
        /// <returns></returns>
        public static XmlNode GetXmlNode(this XmlDocument xmlDocument, string nodeText)
        {
            XmlNode xmlnode = xmlDocument.SelectSingleNode(nodeText);
            return xmlnode;
        }

       /// <summary>
       /// 获取多个node
       /// </summary>
       /// <param name="xmlDocument"></param>
       /// <param name="nodeText"></param>
       /// <returns></returns>
        public static XmlNodeList GetXmlNodeList(this XmlDocument xmlDocument, string nodeText)
        {
            XmlNodeList xmlnodelist = xmlDocument.SelectNodes(nodeText);
            return xmlnodelist;
        }

        /// <summary>
        /// 获取node的属性值
        /// </summary>
        /// <param name="xmlDocument"></param>
        /// <param name="nodeText"></param>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public static XmlAttribute GetXmlAttribute(this XmlDocument xmlDocument, string nodeText, string attributeName)
        {
            XmlAttribute xmlattribute = null;
            XmlNode xmlnode = xmlDocument.SelectSingleNode(nodeText);
            if (xmlnode != null)
            {
                if (xmlnode.Attributes != null && xmlnode.Attributes.Count > 0)
                {
                    xmlattribute = xmlnode.Attributes[attributeName];
                }
            }
            return xmlattribute;
        }

        /// <summary>
        /// 获取属性集合
        /// </summary>
        /// <param name="xmlDocument"></param>
        /// <param name="nodeText"></param>
        /// <returns></returns>
        public static XmlAttributeCollection GetNodeAttributes(this XmlDocument xmlDocument, string nodeText)
        {
            XmlAttributeCollection xmlattributes = null;
            XmlNode xmlnode = xmlDocument.SelectSingleNode(nodeText);
            if (xmlnode != null)
            {
                if (xmlnode.Attributes != null && xmlnode.Attributes.Count > 0)
                {
                    xmlattributes = xmlnode.Attributes;
                }
            }
            return xmlattributes;
        }


        /// <summary>
        /// 更新属性值
        /// </summary>
        /// <param name="xmlDocument"></param>
        /// <param name="nodeText"></param>
        /// <param name="attributeName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool UpdateAttribute(this XmlDocument xmlDocument, string nodeText, string attributeName, string value)
        {
            bool isSuccess = false;
            XmlNode xmlnode = xmlDocument.SelectSingleNode(nodeText);
            if (xmlnode == null) return false;
            if (xmlnode.Attributes == null) return false;
            foreach (XmlAttribute attribute in xmlnode.Attributes)
            {
                if (attribute.Name.ToLower() == attributeName.ToLower())
                {
                    isSuccess = true;
                    attribute.Value = value;
                    break;
                }
            }
            return isSuccess;
        }

        /// <summary>
        /// 删除所有的属性集合
        /// </summary>
        /// <param name="xmlDocument"></param>
        /// <param name="nodeText"></param>
        /// <returns></returns>
        public static bool DeleteAttributes(this XmlDocument xmlDocument, string nodeText)
        {
            bool isSuccess = false;
            XmlNode xmlnode = xmlDocument.SelectSingleNode(nodeText);
            if (xmlnode == null) return false;
            if (xmlnode.Attributes != null && xmlnode.Attributes.Count > 0)
            {
                xmlnode.Attributes.RemoveAll();
                isSuccess = true;
            }
            return isSuccess;
        }

        /// <summary>
        /// 删除一个属性
        /// </summary>
        /// <param name="xmlDocument"></param>
        /// <param name="nodeText"></param>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public static bool DeleteOneAttribute(this XmlDocument xmlDocument, string nodeText, string attributeName)
        {
            bool isSuccess = false;
            XmlAttribute xmlAttribute = null;
            XmlNode xmlnode = xmlDocument.SelectSingleNode(nodeText);
            if (xmlnode == null) return false;
            if (xmlnode.Attributes != null && xmlnode.Attributes.Count > 0)
            {
                foreach (XmlAttribute attribute in xmlnode.Attributes)
                {
                    if (attribute.Name.ToLower() == attributeName.ToLower())
                    {
                        xmlAttribute = attribute;
                        break;
                    }
                }
            }
            if (xmlAttribute != null)
            {
                xmlnode.Attributes.Remove(xmlAttribute);
                isSuccess = true;
            }
            return isSuccess;
        }

        /// <summary>
        /// 添加一个属性
        /// </summary>
        /// <param name="xmlDocument"></param>
        /// <param name="nodeText"></param>
        /// <param name="attributeName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool AddAttribute(this XmlDocument xmlDocument, string nodeText, string attributeName, string value)
        {
            XmlNode xmlnode = xmlDocument.SelectSingleNode(nodeText);
            if (xmlnode == null) return false;
            if (xmlnode.Attributes != null && xmlnode.Attributes.Count > 0)//遍历判断有无此属性
            {
                foreach (XmlAttribute attribute in xmlnode.Attributes)
                {
                    if (attribute.Name.ToLower() == attributeName.ToLower())
                    {
                        //有则不改，直接返回true;
                        return true;
                    }
                }
            }
            XmlAttribute xmlAttribute = xmlDocument.CreateAttribute(attributeName);
            xmlAttribute.Value = value;
            if (xmlnode.Attributes != null) xmlnode.Attributes.Append(xmlAttribute);
            return true;
        }

        /// <summary/>
        /// 删除指定节点名称为nodeName的所有节点，如果该节点有子节点，则不能删除
        public static bool DeleteNode(this XmlDocument xmlDocument, string nodeText)
        {
            bool isSuccess = false;
            XmlNode xmlnode = xmlDocument.SelectSingleNode(nodeText);
            if (xmlnode == null) return false;
            if (!xmlnode.HasChildNodes)
            {
                if (xmlnode.ParentNode != null) xmlnode.ParentNode.RemoveChild(xmlnode); //删除节点
                isSuccess = true;
            }
            return isSuccess;
        }

        /// <summary>
        /// 创建xml文档
        ///  XmlDocument xmlDoc = new XmlDocument();
        ///  XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
        ///  xmlDoc.AppendChild(xmlDeclaration);
        ///  XmlNode root = xmlDoc.CreateElement(rootNodeName);
        ///  xmlDoc.AppendChild(root);
        ///  xmlDoc.Save(xmlPath);
        ///  return xmlDoc;
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="rootNodeName"></param>
        /// <returns></returns>
        public static XmlDocument CreateXmlDocument(string xmlPath, string rootNodeName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDoc.AppendChild(xmlDeclaration);
            XmlNode root = xmlDoc.CreateElement(rootNodeName);
            xmlDoc.AppendChild(root);
            return xmlDoc;
        }

        /// <summary>
        /// 添加节点并添加属性
        /// </summary>
        /// <param name="xmlDocument"></param>
        /// <param name="nodeText"></param>
        /// <param name="attributeDic"></param>
        public static void AppendNode(this XmlDocument xmlDocument, string nodeText, Dictionary<string, string> attributeDic)
        {
            var node = xmlDocument.CreateElement(nodeText);
            foreach (var attri in attributeDic)
            {
                node.SetAttribute(attri.Key, attri.Value);
            }
            if (xmlDocument.DocumentElement != null) xmlDocument.DocumentElement.AppendChild(node);
        }

        /// <summary>
        /// 加载xml文档
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <returns></returns>
        public static XmlDocument LoadXml(string xmlPath)
        {
            if (File.Exists(xmlPath))
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);
                return xmlDoc;
            }
            return null;
        }

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="xmlDocument"></param>
        /// <param name="nodeText"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ReadXmlNode(this XmlDocument xmlDocument, string nodeText)
        {
            var rootEle = xmlDocument.DocumentElement;
            if (rootEle == null) return null;
            var curNode = rootEle.SelectSingleNode(nodeText);
            if (curNode == null) return null;
            if (curNode.Attributes == null) return null;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (XmlAttribute attribute in curNode.Attributes)
            {
                var key = attribute.Name;
                var value = attribute.Value;
                dic.Add(key, value);
            }
            return dic;
        }
    }
}
