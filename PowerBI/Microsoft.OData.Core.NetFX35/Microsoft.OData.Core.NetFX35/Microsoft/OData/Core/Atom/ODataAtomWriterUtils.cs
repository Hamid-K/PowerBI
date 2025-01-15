using System;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x0200006D RID: 109
	internal static class ODataAtomWriterUtils
	{
		// Token: 0x0600046E RID: 1134 RVA: 0x00010CAC File Offset: 0x0000EEAC
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

		// Token: 0x0600046F RID: 1135 RVA: 0x00010CD9 File Offset: 0x0000EED9
		internal static void WriteError(XmlWriter writer, ODataError error, bool includeDebugInformation, int maxInnerErrorDepth)
		{
			ErrorUtils.WriteXmlError(writer, error, includeDebugInformation, maxInnerErrorDepth);
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00010CE4 File Offset: 0x0000EEE4
		internal static void WriteETag(XmlWriter writer, string etag)
		{
			writer.WriteAttributeString("m", "etag", "http://docs.oasis-open.org/odata/ns/metadata", etag);
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00010CFC File Offset: 0x0000EEFC
		internal static void WriteNullAttribute(XmlWriter writer)
		{
			writer.WriteAttributeString("m", "null", "http://docs.oasis-open.org/odata/ns/metadata", "true");
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00010D18 File Offset: 0x0000EF18
		internal static void WriteRaw(XmlWriter writer, string value)
		{
			ODataAtomWriterUtils.WritePreserveSpaceAttributeIfNeeded(writer, value);
			writer.WriteRaw(value);
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00010D28 File Offset: 0x0000EF28
		internal static void WriteString(XmlWriter writer, string value)
		{
			ODataAtomWriterUtils.WritePreserveSpaceAttributeIfNeeded(writer, value);
			writer.WriteString(value);
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00010D38 File Offset: 0x0000EF38
		internal static string PrefixTypeName(string typeName)
		{
			if (string.IsNullOrEmpty(typeName) || ODataAtomWriterUtils.IsPrimitiveType(typeName))
			{
				return typeName;
			}
			return "#" + typeName;
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00010D58 File Offset: 0x0000EF58
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

		// Token: 0x06000476 RID: 1142 RVA: 0x00010DB0 File Offset: 0x0000EFB0
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

		// Token: 0x06000477 RID: 1143 RVA: 0x00010E04 File Offset: 0x0000F004
		private static bool IsPrimitiveType(string typeName)
		{
			return EdmCoreModel.Instance.GetPrimitiveTypeKind(typeName) != EdmPrimitiveTypeKind.None;
		}
	}
}
