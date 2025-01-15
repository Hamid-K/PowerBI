using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x0200004E RID: 78
	internal sealed class ODataAtomFormat : ODataFormat
	{
		// Token: 0x060002D3 RID: 723 RVA: 0x0000BD07 File Offset: 0x00009F07
		public override string ToString()
		{
			return "Atom";
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000BD0E File Offset: 0x00009F0E
		public override IEnumerable<ODataPayloadKind> DetectPayloadKind(ODataMessageInfo messageInfo, ODataMessageReaderSettings settings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			return this.DetectPayloadKindImplementation(messageInfo.GetMessageStream.Invoke(), messageInfo.IsResponse, true, new ODataPayloadKindDetectionInfo(messageInfo.MediaType, messageInfo.Encoding, settings, messageInfo.Model));
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000BD4C File Offset: 0x00009F4C
		public override ODataInputContext CreateInputContext(ODataMessageInfo messageInfo, ODataMessageReaderSettings messageReaderSettings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
			return new ODataAtomInputContext(this, messageInfo.GetMessageStream.Invoke(), messageInfo.Encoding, messageReaderSettings, messageInfo.IsResponse, true, messageInfo.Model, messageInfo.UrlResolver);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000BD9C File Offset: 0x00009F9C
		public override ODataOutputContext CreateOutputContext(ODataMessageInfo messageInfo, ODataMessageWriterSettings messageWriterSettings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageWriterSettings>(messageWriterSettings, "messageWriterSettings");
			return new ODataAtomOutputContext(this, messageInfo.GetMessageStream.Invoke(), messageInfo.Encoding, messageWriterSettings, messageInfo.IsResponse, true, messageInfo.Model, messageInfo.UrlResolver);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000BDEC File Offset: 0x00009FEC
		private IEnumerable<ODataPayloadKind> DetectPayloadKindImplementation(Stream messageStream, bool readingResponse, bool synchronous, ODataPayloadKindDetectionInfo detectionInfo)
		{
			IEnumerable<ODataPayloadKind> enumerable;
			using (ODataAtomInputContext odataAtomInputContext = new ODataAtomInputContext(this, messageStream, detectionInfo.GetEncoding(), detectionInfo.MessageReaderSettings, readingResponse, synchronous, detectionInfo.Model, null))
			{
				enumerable = odataAtomInputContext.DetectPayloadKind(detectionInfo);
			}
			return enumerable;
		}
	}
}
