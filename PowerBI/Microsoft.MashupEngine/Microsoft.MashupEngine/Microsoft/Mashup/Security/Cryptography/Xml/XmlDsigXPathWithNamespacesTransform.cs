using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography.Xml;
using System.Xml;
using System.Xml.XPath;

namespace Microsoft.Mashup.Security.Cryptography.Xml
{
	// Token: 0x02002004 RID: 8196
	public sealed class XmlDsigXPathWithNamespacesTransform : XmlDsigXPathTransform
	{
		// Token: 0x0600C7AB RID: 51115 RVA: 0x0027B9CA File Offset: 0x00279BCA
		public XmlDsigXPathWithNamespacesTransform()
		{
		}

		// Token: 0x0600C7AC RID: 51116 RVA: 0x0027B9D2 File Offset: 0x00279BD2
		public XmlDsigXPathWithNamespacesTransform(string xpath)
			: this(xpath, null)
		{
		}

		// Token: 0x0600C7AD RID: 51117 RVA: 0x0027B9DC File Offset: 0x00279BDC
		public XmlDsigXPathWithNamespacesTransform(string xpath, IDictionary<string, string> explicitNamespaces)
			: this(xpath, explicitNamespaces, null)
		{
		}

		// Token: 0x0600C7AE RID: 51118 RVA: 0x0027B9E8 File Offset: 0x00279BE8
		public XmlDsigXPathWithNamespacesTransform(string xpath, IDictionary<string, string> explicitNamespaces, IDictionary<string, string> additionalNamespaces)
		{
			if (xpath == null)
			{
				throw new ArgumentNullException("xpath");
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.XmlResolver = null;
			XmlElement xmlElement = xmlDocument.CreateElement("XPathRoot");
			if (additionalNamespaces != null)
			{
				foreach (string text in additionalNamespaces.Keys)
				{
					XmlAttribute xmlAttribute = xmlDocument.CreateAttribute("xmlns", text, "http://www.w3.org/2000/xmlns/");
					xmlAttribute.Value = additionalNamespaces[text];
					xmlElement.Attributes.Append(xmlAttribute);
				}
			}
			XmlElement xmlElement2 = xmlDocument.CreateElement("XPath");
			xmlElement2.InnerText = xpath;
			if (explicitNamespaces != null)
			{
				foreach (string text2 in explicitNamespaces.Keys)
				{
					XmlAttribute xmlAttribute2 = xmlDocument.CreateAttribute("xmlns", text2, "http://www.w3.org/2000/xmlns/");
					xmlAttribute2.Value = explicitNamespaces[text2];
					xmlElement2.Attributes.Append(xmlAttribute2);
				}
			}
			xmlElement.AppendChild(xmlElement2);
			this.LoadInnerXml(xmlElement2.SelectNodes("."));
		}

		// Token: 0x0600C7AF RID: 51119 RVA: 0x0027BB28 File Offset: 0x00279D28
		public override void LoadInnerXml(XmlNodeList nodeList)
		{
			base.LoadInnerXml(nodeList);
			int num = 0;
			while (num < nodeList.Count && this.m_xpathExpression == null)
			{
				XmlElement xmlElement = nodeList[num] as XmlElement;
				if (xmlElement != null && string.Equals(xmlElement.LocalName, "XPath", StringComparison.Ordinal))
				{
					this.m_xpathExpression = xmlElement.InnerXml.Trim();
					this.m_namespaces = xmlElement.CreateNavigator().GetNamespacesInScope(XmlNamespaceScope.All);
				}
				num++;
			}
		}

		// Token: 0x0600C7B0 RID: 51120 RVA: 0x0027BB9C File Offset: 0x00279D9C
		public override void LoadInput(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			XmlDsigC14NTransform xmlDsigC14NTransform = new XmlDsigC14NTransform(true);
			xmlDsigC14NTransform.LoadInput(obj);
			Stream stream = xmlDsigC14NTransform.GetOutput(typeof(Stream)) as Stream;
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.XmlResolver = null;
			using (XmlReader xmlReader = XmlReader.Create(stream, new XmlReaderSettings
			{
				ProhibitDtd = true,
				XmlResolver = null
			}))
			{
				xmlDocument.Load(xmlReader);
			}
			this.m_inputNodes = xmlDocument;
		}

		// Token: 0x0600C7B1 RID: 51121 RVA: 0x0027BC2C File Offset: 0x00279E2C
		public override object GetOutput()
		{
			XmlDSigNodeList xmlDSigNodeList = new XmlDSigNodeList();
			if (this.m_xpathExpression != null && this.m_inputNodes != null)
			{
				XPathNavigator xpathNavigator = this.m_inputNodes.CreateNavigator();
				XPathExpression xpathExpression = xpathNavigator.Compile(string.Format(CultureInfo.InvariantCulture, "boolean({0})", new object[] { this.m_xpathExpression }));
				XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(this.m_inputNodes.NameTable);
				foreach (KeyValuePair<string, string> keyValuePair in this.m_namespaces)
				{
					xmlNamespaceManager.AddNamespace(keyValuePair.Key, keyValuePair.Value);
				}
				xpathExpression.SetContext(xmlNamespaceManager);
				XPathNodeIterator xpathNodeIterator = xpathNavigator.Select("//. | //@*");
				while (xpathNodeIterator.MoveNext())
				{
					XPathNavigator xpathNavigator2 = xpathNodeIterator.Current;
					if ((bool)xpathNavigator2.Evaluate(xpathExpression))
					{
						xmlDSigNodeList.Add((xpathNavigator2 as IHasXmlNode).GetNode());
					}
				}
			}
			return xmlDSigNodeList;
		}

		// Token: 0x040065F5 RID: 26101
		private XmlDocument m_inputNodes;

		// Token: 0x040065F6 RID: 26102
		private IDictionary<string, string> m_namespaces;

		// Token: 0x040065F7 RID: 26103
		private string m_xpathExpression;
	}
}
