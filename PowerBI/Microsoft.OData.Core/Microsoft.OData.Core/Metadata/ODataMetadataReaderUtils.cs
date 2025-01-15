using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Microsoft.OData.Metadata
{
	// Token: 0x020000F6 RID: 246
	internal static class ODataMetadataReaderUtils
	{
		// Token: 0x06000E8F RID: 3727 RVA: 0x00022E08 File Offset: 0x00021008
		internal static XmlReader CreateXmlReader(Stream stream, Encoding encoding, ODataMessageReaderSettings messageReaderSettings)
		{
			XmlReaderSettings xmlReaderSettings = ODataMetadataReaderUtils.CreateXmlReaderSettings(messageReaderSettings);
			if (encoding != null)
			{
				return XmlReader.Create(new StreamReader(stream, encoding), xmlReaderSettings);
			}
			return XmlReader.Create(stream, xmlReaderSettings);
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x00022E34 File Offset: 0x00021034
		private static XmlReaderSettings CreateXmlReaderSettings(ODataMessageReaderSettings messageReaderSettings)
		{
			return new XmlReaderSettings
			{
				CheckCharacters = messageReaderSettings.EnableCharactersCheck,
				ConformanceLevel = ConformanceLevel.Document,
				CloseInput = true,
				DtdProcessing = DtdProcessing.Prohibit
			};
		}
	}
}
