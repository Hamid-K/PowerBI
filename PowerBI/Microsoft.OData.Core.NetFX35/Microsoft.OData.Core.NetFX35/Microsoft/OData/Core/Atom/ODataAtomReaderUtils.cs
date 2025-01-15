using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x0200005B RID: 91
	internal static class ODataAtomReaderUtils
	{
		// Token: 0x060003C5 RID: 965 RVA: 0x0000DB0C File Offset: 0x0000BD0C
		internal static XmlReader CreateXmlReader(Stream stream, Encoding encoding, ODataMessageReaderSettings messageReaderSettings)
		{
			XmlReaderSettings xmlReaderSettings = ODataAtomReaderUtils.CreateXmlReaderSettings(messageReaderSettings);
			if (encoding != null)
			{
				return XmlReader.Create(new StreamReader(stream, encoding), xmlReaderSettings);
			}
			return XmlReader.Create(stream, xmlReaderSettings);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000DB38 File Offset: 0x0000BD38
		internal static bool ReadMetadataNullAttributeValue(string attributeValue)
		{
			return XmlConvert.ToBoolean(attributeValue);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000DB40 File Offset: 0x0000BD40
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
