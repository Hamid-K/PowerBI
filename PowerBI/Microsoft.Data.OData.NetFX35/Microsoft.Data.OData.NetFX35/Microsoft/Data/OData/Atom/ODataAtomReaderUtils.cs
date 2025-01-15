using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x02000229 RID: 553
	internal static class ODataAtomReaderUtils
	{
		// Token: 0x06001088 RID: 4232 RVA: 0x0003E32C File Offset: 0x0003C52C
		internal static XmlReader CreateXmlReader(Stream stream, Encoding encoding, ODataMessageReaderSettings messageReaderSettings)
		{
			XmlReaderSettings xmlReaderSettings = ODataAtomReaderUtils.CreateXmlReaderSettings(messageReaderSettings);
			if (encoding != null)
			{
				return XmlReader.Create(new StreamReader(stream, encoding), xmlReaderSettings);
			}
			return XmlReader.Create(stream, xmlReaderSettings);
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x0003E358 File Offset: 0x0003C558
		internal static bool ReadMetadataNullAttributeValue(string attributeValue)
		{
			return XmlConvert.ToBoolean(attributeValue);
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x0003E360 File Offset: 0x0003C560
		private static XmlReaderSettings CreateXmlReaderSettings(ODataMessageReaderSettings messageReaderSettings)
		{
			return new XmlReaderSettings
			{
				CheckCharacters = messageReaderSettings.CheckCharacters,
				ConformanceLevel = 2,
				CloseInput = true,
				ProhibitDtd = true
			};
		}
	}
}
