using System;
using System.Collections.Generic;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace Microsoft.Mashup.Security.Cryptography.Xml
{
	// Token: 0x02002002 RID: 8194
	public static class TransformFactory
	{
		// Token: 0x0600C7A4 RID: 51108 RVA: 0x0027B8B4 File Offset: 0x00279AB4
		public static XmlDsigXPathTransform CreateXPathTransform(string xpath)
		{
			return TransformFactory.CreateXPathTransform(xpath, null);
		}

		// Token: 0x0600C7A5 RID: 51109 RVA: 0x0027B8C0 File Offset: 0x00279AC0
		public static XmlDsigXPathTransform CreateXPathTransform(string xpath, IDictionary<string, string> namespaces)
		{
			if (xpath == null)
			{
				throw new ArgumentNullException("xpath");
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.XmlResolver = null;
			XmlElement xmlElement = xmlDocument.CreateElement("XPath");
			xmlElement.InnerText = xpath;
			if (namespaces != null)
			{
				foreach (string text in namespaces.Keys)
				{
					XmlAttribute xmlAttribute = xmlDocument.CreateAttribute("xmlns", text, "http://www.w3.org/2000/xmlns/");
					xmlAttribute.Value = namespaces[text];
					xmlElement.Attributes.Append(xmlAttribute);
				}
			}
			XmlDsigXPathTransform xmlDsigXPathTransform = new XmlDsigXPathTransform();
			xmlDsigXPathTransform.LoadInnerXml(xmlElement.SelectNodes("."));
			return xmlDsigXPathTransform;
		}
	}
}
