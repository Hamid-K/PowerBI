using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Microsoft.OData.Metadata
{
	// Token: 0x020001DC RID: 476
	internal static class ODataMetadataWriterUtils
	{
		// Token: 0x060012A6 RID: 4774 RVA: 0x000358D4 File Offset: 0x00033AD4
		internal static XmlWriter CreateXmlWriter(Stream stream, ODataMessageWriterSettings messageWriterSettings, Encoding encoding)
		{
			XmlWriterSettings xmlWriterSettings = ODataMetadataWriterUtils.CreateXmlWriterSettings(messageWriterSettings, encoding);
			return XmlWriter.Create(stream, xmlWriterSettings);
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x000358F2 File Offset: 0x00033AF2
		internal static void WriteError(XmlWriter writer, ODataError error, bool includeDebugInformation, int maxInnerErrorDepth)
		{
			ErrorUtils.WriteXmlError(writer, error, includeDebugInformation, maxInnerErrorDepth);
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x00035900 File Offset: 0x00033B00
		private static XmlWriterSettings CreateXmlWriterSettings(ODataMessageWriterSettings messageWriterSettings, Encoding encoding)
		{
			return new XmlWriterSettings
			{
				CheckCharacters = messageWriterSettings.EnableCharactersCheck,
				ConformanceLevel = 2,
				OmitXmlDeclaration = false,
				Encoding = (encoding ?? MediaTypeUtils.EncodingUtf8NoPreamble),
				NewLineHandling = 1,
				CloseOutput = false
			};
		}
	}
}
