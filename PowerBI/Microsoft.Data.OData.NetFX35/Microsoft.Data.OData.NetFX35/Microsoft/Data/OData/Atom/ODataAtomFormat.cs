using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x020000F9 RID: 249
	internal sealed class ODataAtomFormat : ODataFormat
	{
		// Token: 0x0600065A RID: 1626 RVA: 0x0001715C File Offset: 0x0001535C
		public override string ToString()
		{
			return "Atom";
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00017164 File Offset: 0x00015364
		internal override IEnumerable<ODataPayloadKind> DetectPayloadKind(IODataResponseMessage responseMessage, ODataPayloadKindDetectionInfo detectionInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<IODataResponseMessage>(responseMessage, "responseMessage");
			ExceptionUtils.CheckArgumentNotNull<ODataPayloadKindDetectionInfo>(detectionInfo, "detectionInfo");
			Stream stream = ((ODataMessage)responseMessage).GetStream();
			return this.DetectPayloadKindImplementation(stream, true, true, detectionInfo);
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x000171A0 File Offset: 0x000153A0
		internal override IEnumerable<ODataPayloadKind> DetectPayloadKind(IODataRequestMessage requestMessage, ODataPayloadKindDetectionInfo detectionInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<IODataRequestMessage>(requestMessage, "requestMessage");
			ExceptionUtils.CheckArgumentNotNull<ODataPayloadKindDetectionInfo>(detectionInfo, "detectionInfo");
			Stream stream = ((ODataMessage)requestMessage).GetStream();
			return this.DetectPayloadKindImplementation(stream, false, true, detectionInfo);
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x000171DC File Offset: 0x000153DC
		internal override ODataInputContext CreateInputContext(ODataPayloadKind readerPayloadKind, ODataMessage message, MediaType contentType, Encoding encoding, ODataMessageReaderSettings messageReaderSettings, ODataVersion version, bool readingResponse, IEdmModel model, IODataUrlResolver urlResolver, object payloadKindDetectionFormatState)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessage>(message, "message");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
			Stream stream = message.GetStream();
			return new ODataAtomInputContext(this, stream, encoding, messageReaderSettings, version, readingResponse, true, model, urlResolver);
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0001721C File Offset: 0x0001541C
		internal override ODataOutputContext CreateOutputContext(ODataMessage message, MediaType mediaType, Encoding encoding, ODataMessageWriterSettings messageWriterSettings, bool writingResponse, IEdmModel model, IODataUrlResolver urlResolver)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessage>(message, "message");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageWriterSettings>(messageWriterSettings, "messageWriterSettings");
			Stream stream = message.GetStream();
			return new ODataAtomOutputContext(this, stream, encoding, messageWriterSettings, writingResponse, true, model, urlResolver);
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00017258 File Offset: 0x00015458
		private IEnumerable<ODataPayloadKind> DetectPayloadKindImplementation(Stream messageStream, bool readingResponse, bool synchronous, ODataPayloadKindDetectionInfo detectionInfo)
		{
			IEnumerable<ODataPayloadKind> enumerable;
			using (ODataAtomInputContext odataAtomInputContext = new ODataAtomInputContext(this, messageStream, detectionInfo.GetEncoding(), detectionInfo.MessageReaderSettings, ODataVersion.V3, readingResponse, synchronous, detectionInfo.Model, null))
			{
				enumerable = odataAtomInputContext.DetectPayloadKind(detectionInfo);
			}
			return enumerable;
		}
	}
}
