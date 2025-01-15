using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Atom;

namespace Microsoft.Data.OData
{
	// Token: 0x020001D0 RID: 464
	internal sealed class ODataMetadataFormat : ODataFormat
	{
		// Token: 0x06000DAA RID: 3498 RVA: 0x000308B2 File Offset: 0x0002EAB2
		public override string ToString()
		{
			return "Metadata";
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x000308BC File Offset: 0x0002EABC
		internal override IEnumerable<ODataPayloadKind> DetectPayloadKind(IODataResponseMessage responseMessage, ODataPayloadKindDetectionInfo detectionInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<IODataResponseMessage>(responseMessage, "responseMessage");
			ExceptionUtils.CheckArgumentNotNull<ODataPayloadKindDetectionInfo>(detectionInfo, "detectionInfo");
			Stream stream = ((ODataMessage)responseMessage).GetStream();
			return ODataMetadataFormat.DetectPayloadKindImplementation(stream, detectionInfo);
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x000308F2 File Offset: 0x0002EAF2
		internal override IEnumerable<ODataPayloadKind> DetectPayloadKind(IODataRequestMessage requestMessage, ODataPayloadKindDetectionInfo detectionInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<IODataRequestMessage>(requestMessage, "requestMessage");
			ExceptionUtils.CheckArgumentNotNull<ODataPayloadKindDetectionInfo>(detectionInfo, "detectionInfo");
			return Enumerable.Empty<ODataPayloadKind>();
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x00030910 File Offset: 0x0002EB10
		internal override ODataInputContext CreateInputContext(ODataPayloadKind readerPayloadKind, ODataMessage message, MediaType contentType, Encoding encoding, ODataMessageReaderSettings messageReaderSettings, ODataVersion version, bool readingResponse, IEdmModel model, IODataUrlResolver urlResolver, object payloadKindDetectionFormatState)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessage>(message, "message");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
			Stream stream = message.GetStream();
			return new ODataMetadataInputContext(this, stream, encoding, messageReaderSettings, version, readingResponse, true, model, urlResolver);
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x00030950 File Offset: 0x0002EB50
		internal override ODataOutputContext CreateOutputContext(ODataMessage message, MediaType mediaType, Encoding encoding, ODataMessageWriterSettings messageWriterSettings, bool writingResponse, IEdmModel model, IODataUrlResolver urlResolver)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessage>(message, "message");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageWriterSettings>(messageWriterSettings, "messageWriterSettings");
			Stream stream = message.GetStream();
			return new ODataMetadataOutputContext(this, stream, encoding, messageWriterSettings, writingResponse, true, model, urlResolver);
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x0003098C File Offset: 0x0002EB8C
		private static IEnumerable<ODataPayloadKind> DetectPayloadKindImplementation(Stream messageStream, ODataPayloadKindDetectionInfo detectionInfo)
		{
			try
			{
				using (XmlReader xmlReader = ODataAtomReaderUtils.CreateXmlReader(messageStream, detectionInfo.GetEncoding(), detectionInfo.MessageReaderSettings))
				{
					string namespaceURI;
					if (xmlReader.TryReadToNextElement() && string.CompareOrdinal("Edmx", xmlReader.LocalName) == 0 && (namespaceURI = xmlReader.NamespaceURI) != null && (namespaceURI == "http://schemas.microsoft.com/ado/2007/06/edmx" || namespaceURI == "http://schemas.microsoft.com/ado/2008/10/edmx" || namespaceURI == "http://schemas.microsoft.com/ado/2009/11/edmx"))
					{
						return new ODataPayloadKind[] { ODataPayloadKind.MetadataDocument };
					}
				}
			}
			catch (XmlException)
			{
			}
			return Enumerable.Empty<ODataPayloadKind>();
		}
	}
}
