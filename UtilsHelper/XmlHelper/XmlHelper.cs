using System.IO;
using System.Xml;

namespace UtilsHelper.xmlHelper
{
    public class XmlHelper
    {
        private readonly string _filePath = string.Empty;
        private XmlDocument _xml;
        private XmlElement _element;

        public XmlHelper()
        {
            //
        }

        public XmlHelper(string xmlFilePath)
        {
            _filePath = xmlFilePath;
        }

        public static XmlNode GetXmlNode(string xmlFileName, string xPath)
        {
            XmlDocument xmldocument = new XmlDocument();
            //加载xml文档
            xmldocument.Load(xmlFileName);

            XmlNode xmlnode = xmldocument.SelectSingleNode(xPath);
            return xmlnode;
        }

        public static XmlNodeList GetXmlNodeList(string xmlFileName, string xPath)
        {
            XmlDocument xmldocument = new XmlDocument();
            //加载xml文档
            xmldocument.Load(xmlFileName);

            XmlNodeList xmlnodelist = xmldocument.SelectNodes(xPath);
            return xmlnodelist;
        }

        public static XmlAttribute GetXmlAttribute(string xmlFileName, string xPath, string attributeName)
        {
            XmlAttribute xmlattribute = null;
            XmlDocument xmldocument = new XmlDocument();
            xmldocument.Load(xmlFileName);
            XmlNode xmlnode = xmldocument.SelectSingleNode(xPath);
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
        /// 1. 功能：新增节点。
        /// 2. 使用条件：将任意节点插入到当前Xml文件中。
        /// </summary>        
        /// <param name="xmlNode">要插入的Xml节点</param>
        public void AppendNode(XmlNode xmlNode)
        {
            //创建XML的根节点
            CreateXmlElement();

            //导入节点
            XmlNode node = _xml.ImportNode(xmlNode, true);

            //将节点插入到根节点下
            _element.AppendChild(node);
        }

        public static XmlAttributeCollection GetNodeAttributes(string xmlFileName, string xPath)
        {
            XmlAttributeCollection xmlattributes = null;
            XmlDocument xmldocument = new XmlDocument();
            xmldocument.Load(xmlFileName);
            XmlNode xmlnode = xmldocument.SelectSingleNode(xPath);
            if (xmlnode != null)
            {
                if (xmlnode.Attributes != null && xmlnode.Attributes.Count > 0)
                {
                    xmlattributes = xmlnode.Attributes;
                }
            }
            return xmlattributes;
        }

        public static bool UpdateAttribute(string xmlFileName, string xPath, string attributeName, string value)
        {
            bool isSuccess = false;
            XmlDocument xmldocument = new XmlDocument();
            xmldocument.Load(xmlFileName);
            XmlNode xmlnode = xmldocument.SelectSingleNode(xPath);
            if (xmlnode == null) return false;
            if (xmlnode.Attributes == null) return false;
            foreach (XmlAttribute attribute in xmlnode.Attributes)
            {
                if (attribute.Name.ToLower() == attributeName.ToLower())
                {
                    isSuccess = true;
                    attribute.Value = value;
                    xmldocument.Save(xmlFileName);
                    break;
                }
            }
            return isSuccess;
        }

        public static bool DeleteAttributes(string xmlFileName, string xPath)
        {
            bool isSuccess = false;
            XmlDocument xmldocument = new XmlDocument();
            xmldocument.Load(xmlFileName);
            XmlNode xmlnode = xmldocument.SelectSingleNode(xPath);
            if (xmlnode == null) return false;
            if (xmlnode.Attributes != null && xmlnode.Attributes.Count > 0)
            {
                xmlnode.Attributes.RemoveAll();
                xmldocument.Save(xmlFileName);
                isSuccess = true;
            }
            return isSuccess;
        }

        public static bool DeleteOneAttribute(string xmlFileName, string xPath, string attributeName)
        {
            bool isSuccess = false;
            XmlDocument xmldocument = new XmlDocument();
            xmldocument.Load(xmlFileName);
            XmlAttribute xmlAttribute = null;
            XmlNode xmlnode = xmldocument.SelectSingleNode(xPath);
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
                xmldocument.Save(xmlFileName);
                isSuccess = true;
            }
            return isSuccess;
        }

        public static bool AddAttribute(string xmlFileName, string xPath, string attributeName, string value)
        {
            XmlDocument xmldocument = new XmlDocument();
            xmldocument.Load(xmlFileName);
            XmlNode xmlnode = xmldocument.SelectSingleNode(xPath);
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
            xmldocument.Save(xmlFileName);
            return true;
        }

        /// <summary/>
        /// 删除指定节点名称为nodeName的所有节点，如果该节点有子节点，则不能删除
        public static bool DeleteNode(string xmlFileName, string xPath, string nodeName)
        {
            bool isSuccess = false;
            XmlDocument xmldocument = new XmlDocument();
            xmldocument.Load(xmlFileName);
            XmlNode xmlnode = xmldocument.SelectSingleNode(xPath);
            if (xmlnode == null) return false;
            if (!xmlnode.HasChildNodes)
            {
                if (xmlnode.ParentNode != null) xmlnode.ParentNode.RemoveChild(xmlnode); //删除节点
                isSuccess = true;
                xmldocument.Save(xmlFileName);
            }
            return isSuccess;
        }


        /// <summary>
        /// 删除指定XPath表达式的节点
        /// </summary>        
        /// <param name="xPath">XPath表达式,
        /// 范例1: @"Skill/First/SkillItem", 等效于 @"//Skill/First/SkillItem"
        /// 范例2: @"Table[USERNAME='a']" , []表示筛选,USERNAME是Table下的一个子节点.
        /// 范例3: @"ApplyPost/Item[@itemName='岗位编号']",@itemName是Item节点的属性.
        /// </param>
        public void RemoveNode(string xPath)
        {
            //创建XML的根节点
            CreateXmlElement();

            //获取要删除的节点
            XmlNode node = _xml.SelectSingleNode(xPath);

            //删除节点
            if (node != null) _element.RemoveChild(node);
        }

        #region 创建XML的根节点

        /// <summary>
        /// 创建XML的根节点
        /// </summary>
        public void CreateXmlElement()
        {
            //创建一个XML对象
            _xml = new XmlDocument();
            if (File.Exists(_filePath))
            {
                //加载XML文件
                _xml.Load(_filePath);
            }
            //为XML的根节点赋值
            _element = _xml.DocumentElement;
        }
        #endregion

        #region 保存XML文件
        /// <summary>
        /// 保存XML文件
        /// </summary>
        public void Save()
        {
            //创建XML的根节点
            //CreateXMLElement();
            //保存XML文件
            _xml.Save(_filePath);
        }

        #endregion //保存XML文件

        #region XML文档创建和节点或属性的添加、修改

        /// <summary>
        /// 创建一个XML文档
        /// </summary>
        /// <param name="xmlFileName">XML文档完全文件名(包含物理路径)</param>
        /// <param name="rootNodeName">XML文档根节点名称(须指定一个根节点名称)</param>
        /// <param name="version">XML文档版本号(必须为:"1.0")</param>
        /// <param name="encoding">XML文档编码方式</param>
        /// <param name="standalone">该值必须是"yes"或"no",如果为null,Save方法不在XML声明上写出独立属性</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool CreateXmlDocument(string xmlFileName, string rootNodeName, string version, string encoding, string standalone)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration(version, encoding, standalone);
            XmlNode root = xmlDoc.CreateElement(rootNodeName);
            xmlDoc.AppendChild(xmlDeclaration);
            xmlDoc.AppendChild(root);
            xmlDoc.Save(xmlFileName);
            return true;
        }
        #endregion
    }
}
