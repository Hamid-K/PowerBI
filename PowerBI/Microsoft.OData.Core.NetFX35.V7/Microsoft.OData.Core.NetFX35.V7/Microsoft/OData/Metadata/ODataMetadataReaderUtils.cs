using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Microsoft.OData.Metadata
{
	// Token: 0x020001DB RID: 475
	internal static class ODataMetadataReaderUtils
	{
		// Token: 0x060012A4 RID: 4772 RVA: 0x00035870 File Offset: 0x00033A70
		internal static XmlReader CreateXmlReader(Stream stream, Encoding encoding, ODataMessageReaderSettings messageReaderSettings)
		{
			XmlReaderSettings xmlReaderSettings = ODataMetadataReaderUtils.CreateXmlReaderSettings(messageReaderSettings);
			if (encoding != null)
			{
				return XmlReader.Create(new StreamReader(stream, encoding), xmlReaderSettings);
			}
			return XmlReader.Create(stream, xmlReaderSettings);
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x0003589C File Offset: 0x00033A9C
		private static XmlReaderSettings CreateXmlReaderSettings(ODataMessageReaderSettings messageReaderSettings)
		{
			return new XmlReaderSettings
			{
				CheckCharacters = messageReaderSettings.EnableCharactersCheck,
				ConformanceLevel = 2,
				CloseInput = true,
				ProhibitDtd = true
			};
		}
	}
}
