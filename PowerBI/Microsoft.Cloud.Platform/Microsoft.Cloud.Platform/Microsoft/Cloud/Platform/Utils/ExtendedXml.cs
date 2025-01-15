using System;
using System.Xml;
using System.Xml.XPath;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002F8 RID: 760
	public static class ExtendedXml
	{
		// Token: 0x06001425 RID: 5157 RVA: 0x000462EC File Offset: 0x000444EC
		public static XmlNode SelectMandatorySingleNode(this XmlDocument document, [NotNull] string xpath)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(xpath, "xpath");
			XmlNode xmlNode = document.SelectSingleNode(xpath);
			if (xmlNode == null)
			{
				throw new XPathException("Expected node '{0}' is missing. Check if xml document is valid.".FormatWithInvariantCulture(new object[] { xpath }));
			}
			return xmlNode;
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x0004632C File Offset: 0x0004452C
		public static XmlElement CreateChildElement(this XmlDocument document, string parentElementPath, string childElementName)
		{
			XmlNode xmlNode = document.SelectMandatorySingleNode(parentElementPath);
			return document.CreateChildElement(xmlNode, childElementName);
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x0004634C File Offset: 0x0004454C
		public static XmlElement CreateChildElement(this XmlDocument document, XmlNode parentElement, [NotNull] string childElementName)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(childElementName, "childElementName");
			XmlElement xmlElement = document.CreateElement(string.Empty, childElementName, string.Empty);
			parentElement.AppendChild(xmlElement);
			return xmlElement;
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x00046380 File Offset: 0x00044580
		public static XmlElement CreateChildElement<T>(this XmlDocument document, string parentElementPath, string childElementName, T childElementInnerData)
		{
			XmlNode xmlNode = document.SelectMandatorySingleNode(parentElementPath);
			return document.CreateChildElement(xmlNode, childElementName, childElementInnerData);
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x000463A0 File Offset: 0x000445A0
		public static XmlElement CreateChildElement<T>(this XmlDocument document, XmlNode parentElement, [NotNull] string childElementName, T childElementInnerData)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(childElementName, "childElementName");
			XmlElement xmlElement = document.CreateElement(string.Empty, childElementName, string.Empty);
			xmlElement.InnerText = "{0}".FormatWithInvariantCulture(new object[] { childElementInnerData });
			parentElement.AppendChild(xmlElement);
			return xmlElement;
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x000463F4 File Offset: 0x000445F4
		public static void RemoveElementByName(this XmlDocument document, [NotNull] string elementName)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(elementName, "elementName");
			XmlNode xmlNode = document.SelectSingleNode(elementName);
			if (xmlNode != null)
			{
				xmlNode.ParentNode.RemoveChild(xmlNode);
			}
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x00046424 File Offset: 0x00044624
		public static void AddAttributeToElement(this XmlDocument document, XmlElement element, [NotNull] string attributeName, string attributeValue)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(attributeName, "attributeName");
			XmlAttribute xmlAttribute = document.CreateAttribute(attributeName);
			xmlAttribute.Value = attributeValue;
			element.Attributes.Append(xmlAttribute);
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x00046458 File Offset: 0x00044658
		public static void SecureLoadDocument(this XmlDocument document, string fileName)
		{
			using (XmlReader xmlReader = XmlReader.Create(fileName, ExtendedXml.s_secureXmlReaderSettings))
			{
				document.Load(xmlReader);
			}
		}

		// Token: 0x040007B1 RID: 1969
		private static XmlReaderSettings s_secureXmlReaderSettings = new XmlReaderSettings
		{
			XmlResolver = null,
			DtdProcessing = DtdProcessing.Prohibit
		};
	}
}
