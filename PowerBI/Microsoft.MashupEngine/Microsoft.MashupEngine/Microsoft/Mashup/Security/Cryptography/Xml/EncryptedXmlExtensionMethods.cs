using System;
using System.IO;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace Microsoft.Mashup.Security.Cryptography.Xml
{
	// Token: 0x02002001 RID: 8193
	public static class EncryptedXmlExtensionMethods
	{
		// Token: 0x0600C7A3 RID: 51107 RVA: 0x0027B7EC File Offset: 0x002799EC
		public static void ReplaceData2(this EncryptedXml encryptedXml, XmlElement inputElement, byte[] decryptedData)
		{
			if (inputElement == null)
			{
				throw new ArgumentNullException("inputElement");
			}
			if (decryptedData == null)
			{
				throw new ArgumentNullException("decryptedData");
			}
			XmlNode parentNode = inputElement.ParentNode;
			if (parentNode.NodeType == XmlNodeType.Document)
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.XmlResolver = null;
				XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
				xmlReaderSettings.ProhibitDtd = true;
				xmlReaderSettings.XmlResolver = null;
				using (XmlReader xmlReader = XmlReader.Create(new StringReader(encryptedXml.Encoding.GetString(decryptedData)), xmlReaderSettings))
				{
					xmlDocument.Load(xmlReader);
				}
				XmlNode xmlNode = inputElement.OwnerDocument.ImportNode(xmlDocument.DocumentElement, true);
				parentNode.RemoveChild(inputElement);
				parentNode.AppendChild(xmlNode);
				return;
			}
			encryptedXml.ReplaceData(inputElement, decryptedData);
		}
	}
}
