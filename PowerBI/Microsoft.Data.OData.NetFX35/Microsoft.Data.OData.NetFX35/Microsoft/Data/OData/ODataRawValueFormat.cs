using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData
{
	// Token: 0x020001EB RID: 491
	internal sealed class ODataRawValueFormat : ODataFormat
	{
		// Token: 0x06000E4D RID: 3661 RVA: 0x00033C39 File Offset: 0x00031E39
		public override string ToString()
		{
			return "RawValue";
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x00033C40 File Offset: 0x00031E40
		internal override IEnumerable<ODataPayloadKind> DetectPayloadKind(IODataResponseMessage responseMessage, ODataPayloadKindDetectionInfo detectionInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<IODataResponseMessage>(responseMessage, "responseMessage");
			ExceptionUtils.CheckArgumentNotNull<ODataPayloadKindDetectionInfo>(detectionInfo, "detectionInfo");
			return ODataRawValueFormat.DetectPayloadKindImplementation(detectionInfo.ContentType);
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x00033C63 File Offset: 0x00031E63
		internal override IEnumerable<ODataPayloadKind> DetectPayloadKind(IODataRequestMessage requestMessage, ODataPayloadKindDetectionInfo detectionInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<IODataRequestMessage>(requestMessage, "requestMessage");
			ExceptionUtils.CheckArgumentNotNull<ODataPayloadKindDetectionInfo>(detectionInfo, "detectionInfo");
			return ODataRawValueFormat.DetectPayloadKindImplementation(detectionInfo.ContentType);
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x00033C88 File Offset: 0x00031E88
		internal override ODataInputContext CreateInputContext(ODataPayloadKind readerPayloadKind, ODataMessage message, MediaType contentType, Encoding encoding, ODataMessageReaderSettings messageReaderSettings, ODataVersion version, bool readingResponse, IEdmModel model, IODataUrlResolver urlResolver, object payloadKindDetectionFormatState)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessage>(message, "message");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
			Stream stream = message.GetStream();
			return new ODataRawInputContext(this, stream, encoding, messageReaderSettings, version, readingResponse, true, model, urlResolver, readerPayloadKind);
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x00033CC8 File Offset: 0x00031EC8
		internal override ODataOutputContext CreateOutputContext(ODataMessage message, MediaType mediaType, Encoding encoding, ODataMessageWriterSettings messageWriterSettings, bool writingResponse, IEdmModel model, IODataUrlResolver urlResolver)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessage>(message, "message");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageWriterSettings>(messageWriterSettings, "messageWriterSettings");
			Stream stream = message.GetStream();
			return new ODataRawOutputContext(this, stream, encoding, messageWriterSettings, writingResponse, true, model, urlResolver);
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x00033D04 File Offset: 0x00031F04
		private static IEnumerable<ODataPayloadKind> DetectPayloadKindImplementation(MediaType contentType)
		{
			if (HttpUtils.CompareMediaTypeNames("text", contentType.TypeName) && HttpUtils.CompareMediaTypeNames("text/plain", contentType.SubTypeName))
			{
				return new ODataPayloadKind[] { ODataPayloadKind.Value };
			}
			return new ODataPayloadKind[] { ODataPayloadKind.BinaryValue };
		}
	}
}
