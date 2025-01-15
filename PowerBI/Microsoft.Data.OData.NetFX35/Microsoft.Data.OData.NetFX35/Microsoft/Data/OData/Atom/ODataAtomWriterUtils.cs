using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x02000296 RID: 662
	internal static class ODataAtomWriterUtils
	{
		// Token: 0x06001522 RID: 5410 RVA: 0x0004CFAC File Offset: 0x0004B1AC
		internal static XmlWriter CreateXmlWriter(Stream stream, ODataMessageWriterSettings messageWriterSettings, Encoding encoding)
		{
			XmlWriterSettings xmlWriterSettings = ODataAtomWriterUtils.CreateXmlWriterSettings(messageWriterSettings, encoding);
			XmlWriter xmlWriter = XmlWriter.Create(stream, xmlWriterSettings);
			if (messageWriterSettings.AlwaysUseDefaultXmlNamespaceForRootElement)
			{
				xmlWriter = new DefaultNamespaceCompensatingXmlWriter(xmlWriter);
			}
			return xmlWriter;
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x0004CFD9 File Offset: 0x0004B1D9
		internal static void WriteError(XmlWriter writer, ODataError error, bool includeDebugInformation, int maxInnerErrorDepth)
		{
			ErrorUtils.WriteXmlError(writer, error, includeDebugInformation, maxInnerErrorDepth);
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x0004CFE4 File Offset: 0x0004B1E4
		internal static void WriteETag(XmlWriter writer, string etag)
		{
			writer.WriteAttributeString("m", "etag", "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", etag);
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x0004CFFC File Offset: 0x0004B1FC
		internal static void WriteNullAttribute(XmlWriter writer)
		{
			writer.WriteAttributeString("m", "null", "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", "true");
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x0004D018 File Offset: 0x0004B218
		internal static void WriteRaw(XmlWriter writer, string value)
		{
			ODataAtomWriterUtils.WritePreserveSpaceAttributeIfNeeded(writer, value);
			writer.WriteRaw(value);
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x0004D028 File Offset: 0x0004B228
		internal static void WriteString(XmlWriter writer, string value)
		{
			ODataAtomWriterUtils.WritePreserveSpaceAttributeIfNeeded(writer, value);
			writer.WriteString(value);
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x0004D038 File Offset: 0x0004B238
		private static XmlWriterSettings CreateXmlWriterSettings(ODataMessageWriterSettings messageWriterSettings, Encoding encoding)
		{
			return new XmlWriterSettings
			{
				CheckCharacters = messageWriterSettings.CheckCharacters,
				ConformanceLevel = 2,
				OmitXmlDeclaration = false,
				Encoding = (encoding ?? MediaTypeUtils.EncodingUtf8NoPreamble),
				NewLineHandling = 1,
				Indent = messageWriterSettings.Indent,
				CloseOutput = false
			};
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x0004D090 File Offset: 0x0004B290
		private static void WritePreserveSpaceAttributeIfNeeded(XmlWriter writer, string value)
		{
			if (value == null)
			{
				return;
			}
			int length = value.Length;
			if (length > 0 && (char.IsWhiteSpace(value.get_Chars(0)) || char.IsWhiteSpace(value.get_Chars(length - 1))))
			{
				writer.WriteAttributeString("xml", "space", "http://www.w3.org/XML/1998/namespace", "preserve");
			}
		}
	}
}
