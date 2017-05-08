using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace UtilsHelper.xmlHelper
{
    public static class XmlHelper
    {
        public static XmlNode GetXmlNode(string xmlPath, string nodeText)
        {
            var xmldocument = LoadXml(xmlPath);
            XmlNode xmlnode = xmldocument.SelectSingleNode(nodeText);
            return xmlnode;
        }

        public static XmlNodeList GetXmlNodeList(string xmlPath, string nodeText)
        {
            var xmldocument = LoadXml(xmlPath);
            XmlNodeList xmlnodelist = xmldocument.SelectNodes(nodeText);
            return xmlnodelist;
        }

        public static XmlAttribute GetXmlAttribute(string xmlPath, string nodeText, string attributeName)
        {
            XmlAttribute xmlattribute = null;
            var xmldocument = LoadXml(xmlPath);
            XmlNode xmlnode = xmldocument.SelectSingleNode(nodeText);
            if (xmlnode != null)
            {
                if (xmlnode.Attributes != null && xmlnode.Attributes.Count > 0)
                {
                    xmlattribute = xmlnode.Attributes[attributeName];
                }
            }
            return xmlattribute;
        }

        public static XmlAttributeCollection GetNodeAttributes(string xmlPath, string nodeText)
        {
            XmlAttributeCollection xmlattributes = null;
            var xmldocument = LoadXml(xmlPath);
            XmlNode xmlnode = xmldocument.SelectSingleNode(nodeText);
            if (xmlnode != null)
            {
                if (xmlnode.Attributes != null && xmlnode.Attributes.Count > 0)
                {
                    xmlattributes = xmlnode.Attributes;
                }
            }
            return xmlattributes;
        }

        public static bool UpdateAttribute(string xmlPath, string nodeText, string attributeName, string value)
        {
            bool isSuccess = false;
            var xmldocument = LoadXml(xmlPath);
            XmlNode xmlnode = xmldocument.SelectSingleNode(nodeText);
            if (xmlnode == null) return false;
            if (xmlnode.Attributes == null) return false;
            foreach (XmlAttribute attribute in xmlnode.Attributes)
            {
                if (attribute.Name.ToLower() == attributeName.ToLower())
                {
                    isSuccess = true;
                    attribute.Value = value;
                    xmldocument.Save(xmlPath);
                    break;
                }
            }
            return isSuccess;
        }

        public static bool DeleteAttributes(string xmlPath, string nodeText)
        {
            bool isSuccess = false;
            var xmldocument = LoadXml(xmlPath);
            XmlNode xmlnode = xmldocument.SelectSingleNode(nodeText);
            if (xmlnode == null) return false;
            if (xmlnode.Attributes != null && xmlnode.Attributes.Count > 0)
            {
                xmlnode.Attributes.RemoveAll();
                xmldocument.Save(xmlPath);
                isSuccess = true;
            }
            return isSuccess;
        }

        public static bool DeleteOneAttribute(string xmlPath, string nodeText, string attributeName)
        {
            bool isSuccess = false;
            var xmldocument = LoadXml(xmlPath);
            XmlAttribute xmlAttribute = null;
            XmlNode xmlnode = xmldocument.SelectSingleNode(nodeText);
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
                xmldocument.Save(xmlPath);
                isSuccess = true;
            }
            return isSuccess;
        }

        public static bool AddAttribute(string xmlPath, string nodeText, string attributeName, string value)
        {
            var xmldocument = LoadXml(xmlPath);
            XmlNode xmlnode = xmldocument.SelectSingleNode(nodeText);
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
            XmlAttribute xmlAttribute = xmldocument.CreateAttribute(attributeName);
            xmlAttribute.Value = value;
            if (xmlnode.Attributes != null) xmlnode.Attributes.Append(xmlAttribute);
            xmldocument.Save(xmlPath);
            return true;
        }

        /// <summary/>
        /// 删除指定节点名称为nodeName的所有节点，如果该节点有子节点，则不能删除
        public static bool DeleteNode(string xmlPath, string nodeText)
        {
            bool isSuccess = false;
            var xmldocument = LoadXml(xmlPath);
            XmlNode xmlnode = xmldocument.SelectSingleNode(nodeText);
            if (xmlnode == null) return false;
            if (!xmlnode.HasChildNodes)
            {
                if (xmlnode.ParentNode != null) xmlnode.ParentNode.RemoveChild(xmlnode); //删除节点
                isSuccess = true;
                xmldocument.Save(xmlPath);
            }
            return isSuccess;
        }

        public static XmlDocument CreateXmlDocument(string xmlPath, string rootNodeName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDoc.AppendChild(xmlDeclaration);
            XmlNode root = xmlDoc.CreateElement(rootNodeName);
            xmlDoc.AppendChild(root);
            xmlDoc.Save(xmlPath);
            return xmlDoc;
        }

        public static void AppendNode(string xmlPath, string nodeText, Dictionary<string, string> attributeDic)
        {
            var xmlDoc = LoadXml(xmlPath);
            var node = xmlDoc.CreateElement(nodeText);
            foreach (var attri in attributeDic)
            {
                node.SetAttribute(attri.Key, attri.Value);
            }
            if (xmlDoc.DocumentElement != null) xmlDoc.DocumentElement.AppendChild(node);
            xmlDoc.Save(xmlPath);
        }

        private static XmlDocument LoadXml(string xmlPath)
        {
            if (File.Exists(xmlPath))
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);
                return xmlDoc;
            }
            return null;
        }

        public static Dictionary<string, string> ReadXmlNode(string xmlPath, string nodeText)
        {
            var xmlDoc = LoadXml(xmlPath);
            var rootEle = xmlDoc.DocumentElement;
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
