using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData
{
	// Token: 0x020001CF RID: 463
	internal sealed class ODataBatchFormat : ODataFormat
	{
		// Token: 0x06000DA2 RID: 3490 RVA: 0x00030754 File Offset: 0x0002E954
		public override string ToString()
		{
			return "Batch";
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x0003075B File Offset: 0x0002E95B
		internal override IEnumerable<ODataPayloadKind> DetectPayloadKind(IODataResponseMessage responseMessage, ODataPayloadKindDetectionInfo detectionInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<IODataResponseMessage>(responseMessage, "responseMessage");
			ExceptionUtils.CheckArgumentNotNull<ODataPayloadKindDetectionInfo>(detectionInfo, "detectionInfo");
			return ODataBatchFormat.DetectPayloadKindImplementation(detectionInfo.ContentType);
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x0003077E File Offset: 0x0002E97E
		internal override IEnumerable<ODataPayloadKind> DetectPayloadKind(IODataRequestMessage requestMessage, ODataPayloadKindDetectionInfo detectionInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<IODataRequestMessage>(requestMessage, "requestMessage");
			ExceptionUtils.CheckArgumentNotNull<ODataPayloadKindDetectionInfo>(detectionInfo, "detectionInfo");
			return ODataBatchFormat.DetectPayloadKindImplementation(detectionInfo.ContentType);
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x000307A4 File Offset: 0x0002E9A4
		internal override ODataInputContext CreateInputContext(ODataPayloadKind readerPayloadKind, ODataMessage message, MediaType contentType, Encoding encoding, ODataMessageReaderSettings messageReaderSettings, ODataVersion version, bool readingResponse, IEdmModel model, IODataUrlResolver urlResolver, object payloadKindDetectionFormatState)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessage>(message, "message");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
			Stream stream = message.GetStream();
			return new ODataRawInputContext(this, stream, encoding, messageReaderSettings, version, readingResponse, true, model, urlResolver, readerPayloadKind);
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x000307E4 File Offset: 0x0002E9E4
		internal override ODataOutputContext CreateOutputContext(ODataMessage message, MediaType mediaType, Encoding encoding, ODataMessageWriterSettings messageWriterSettings, bool writingResponse, IEdmModel model, IODataUrlResolver urlResolver)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessage>(message, "message");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageWriterSettings>(messageWriterSettings, "messageWriterSettings");
			Stream stream = message.GetStream();
			return new ODataRawOutputContext(this, stream, encoding, messageWriterSettings, writingResponse, true, model, urlResolver);
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x00030834 File Offset: 0x0002EA34
		private static IEnumerable<ODataPayloadKind> DetectPayloadKindImplementation(MediaType contentType)
		{
			if (HttpUtils.CompareMediaTypeNames("multipart", contentType.TypeName) && HttpUtils.CompareMediaTypeNames("mixed", contentType.SubTypeName) && contentType.Parameters != null)
			{
				if (Enumerable.Any<KeyValuePair<string, string>>(contentType.Parameters, (KeyValuePair<string, string> kvp) => HttpUtils.CompareMediaTypeParameterNames("boundary", kvp.Key)))
				{
					return new ODataPayloadKind[] { ODataPayloadKind.Batch };
				}
			}
			return Enumerable.Empty<ODataPayloadKind>();
		}
	}
}
