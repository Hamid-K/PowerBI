using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Microsoft.OData.Metadata
{
	// Token: 0x020000F7 RID: 247
	internal static class ODataMetadataWriterUtils
	{
		// Token: 0x06000E91 RID: 3729 RVA: 0x00022E6C File Offset: 0x0002106C
		internal static XmlWriter CreateXmlWriter(Stream stream, ODataMessageWriterSettings messageWriterSettings, Encoding encoding)
		{
			XmlWriterSettings xmlWriterSettings = ODataMetadataWriterUtils.CreateXmlWriterSettings(messageWriterSettings, encoding);
			return XmlWriter.Create(stream, xmlWriterSettings);
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x00022E8A File Offset: 0x0002108A
		internal static void WriteError(XmlWriter writer, ODataError error, bool includeDebugInformation, int maxInnerErrorDepth)
		{
			ErrorUtils.WriteXmlError(writer, error, includeDebugInformation, maxInnerErrorDepth);
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x00022E98 File Offset: 0x00021098
		private static XmlWriterSettings CreateXmlWriterSettings(ODataMessageWriterSettings messageWriterSettings, Encoding encoding)
		{
			return new XmlWriterSettings
			{
				CheckCharacters = messageWriterSettings.EnableCharactersCheck,
				ConformanceLevel = ConformanceLevel.Document,
				OmitXmlDeclaration = false,
				Encoding = (encoding ?? MediaTypeUtils.EncodingUtf8NoPreamble),
				NewLineHandling = NewLineHandling.Entitize,
				CloseOutput = false
			};
		}
	}
}
