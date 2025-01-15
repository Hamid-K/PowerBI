using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000192 RID: 402
	internal sealed class ODataJsonLightFormat : ODataFormat
	{
		// Token: 0x06000B37 RID: 2871 RVA: 0x00027FCB File Offset: 0x000261CB
		public override string ToString()
		{
			return "JsonLight";
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x00027FD4 File Offset: 0x000261D4
		internal override IEnumerable<ODataPayloadKind> DetectPayloadKind(IODataResponseMessage responseMessage, ODataPayloadKindDetectionInfo detectionInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<IODataResponseMessage>(responseMessage, "responseMessage");
			ExceptionUtils.CheckArgumentNotNull<ODataPayloadKindDetectionInfo>(detectionInfo, "detectionInfo");
			ODataMessage odataMessage = (ODataMessage)responseMessage;
			Stream stream = odataMessage.GetStream();
			return this.DetectPayloadKindImplementation(stream, odataMessage, true, detectionInfo);
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x00028010 File Offset: 0x00026210
		internal override IEnumerable<ODataPayloadKind> DetectPayloadKind(IODataRequestMessage requestMessage, ODataPayloadKindDetectionInfo detectionInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<IODataRequestMessage>(requestMessage, "requestMessage");
			ExceptionUtils.CheckArgumentNotNull<ODataPayloadKindDetectionInfo>(detectionInfo, "detectionInfo");
			ODataMessage odataMessage = (ODataMessage)requestMessage;
			Stream stream = odataMessage.GetStream();
			return this.DetectPayloadKindImplementation(stream, odataMessage, false, detectionInfo);
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x0002804C File Offset: 0x0002624C
		internal override ODataInputContext CreateInputContext(ODataPayloadKind readerPayloadKind, ODataMessage message, MediaType contentType, Encoding encoding, ODataMessageReaderSettings messageReaderSettings, ODataVersion version, bool readingResponse, IEdmModel model, IODataUrlResolver urlResolver, object payloadKindDetectionFormatState)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessage>(message, "message");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
			Stream stream = message.GetStream();
			return new ODataJsonLightInputContext(this, stream, contentType, encoding, messageReaderSettings, version, readingResponse, true, model, urlResolver, (ODataJsonLightPayloadKindDetectionState)payloadKindDetectionFormatState);
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00028094 File Offset: 0x00026294
		internal override ODataOutputContext CreateOutputContext(ODataMessage message, MediaType mediaType, Encoding encoding, ODataMessageWriterSettings messageWriterSettings, bool writingResponse, IEdmModel model, IODataUrlResolver urlResolver)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessage>(message, "message");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageWriterSettings>(messageWriterSettings, "messageWriterSettings");
			Stream stream = message.GetStream();
			return new ODataJsonLightOutputContext(this, stream, mediaType, encoding, messageWriterSettings, writingResponse, true, model, urlResolver);
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x000280D4 File Offset: 0x000262D4
		private IEnumerable<ODataPayloadKind> DetectPayloadKindImplementation(Stream messageStream, ODataMessage message, bool readingResponse, ODataPayloadKindDetectionInfo detectionInfo)
		{
			IEnumerable<ODataPayloadKind> enumerable;
			using (ODataJsonLightInputContext odataJsonLightInputContext = new ODataJsonLightInputContext(this, messageStream, detectionInfo.ContentType, detectionInfo.GetEncoding(), detectionInfo.MessageReaderSettings, ODataVersion.V3, readingResponse, true, detectionInfo.Model, null, null))
			{
				enumerable = odataJsonLightInputContext.DetectPayloadKind(detectionInfo);
			}
			return enumerable;
		}
	}
}
