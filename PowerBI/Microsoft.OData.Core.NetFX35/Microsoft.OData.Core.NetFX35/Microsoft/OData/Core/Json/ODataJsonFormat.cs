using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.OData.Core.JsonLight;

namespace Microsoft.OData.Core.Json
{
	// Token: 0x0200010D RID: 269
	internal sealed class ODataJsonFormat : ODataFormat
	{
		// Token: 0x06000A28 RID: 2600 RVA: 0x00025D61 File Offset: 0x00023F61
		public override string ToString()
		{
			return "JsonLight";
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x00025D68 File Offset: 0x00023F68
		public override IEnumerable<ODataPayloadKind> DetectPayloadKind(ODataMessageInfo messageInfo, ODataMessageReaderSettings settings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			return this.DetectPayloadKindImplementation(messageInfo.GetMessageStream.Invoke(), messageInfo.IsResponse, new ODataPayloadKindDetectionInfo(messageInfo.MediaType, messageInfo.Encoding, settings, messageInfo.Model));
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x00025DA4 File Offset: 0x00023FA4
		public override ODataInputContext CreateInputContext(ODataMessageInfo messageInfo, ODataMessageReaderSettings messageReaderSettings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
			return new ODataJsonLightInputContext(this, messageInfo.GetMessageStream.Invoke(), messageInfo.MediaType, messageInfo.Encoding, messageReaderSettings, messageInfo.IsResponse, true, messageInfo.Model, messageInfo.UrlResolver);
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x00025DF8 File Offset: 0x00023FF8
		public override ODataOutputContext CreateOutputContext(ODataMessageInfo messageInfo, ODataMessageWriterSettings messageWriterSettings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageWriterSettings>(messageWriterSettings, "messageWriterSettings");
			return new ODataJsonLightOutputContext(this, messageInfo.GetMessageStream.Invoke(), messageInfo.MediaType, messageInfo.Encoding, messageWriterSettings, messageInfo.IsResponse, true, messageInfo.Model, messageInfo.UrlResolver);
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x00025E4C File Offset: 0x0002404C
		private IEnumerable<ODataPayloadKind> DetectPayloadKindImplementation(Stream messageStream, bool readingResponse, ODataPayloadKindDetectionInfo detectionInfo)
		{
			IEnumerable<ODataPayloadKind> enumerable;
			using (ODataJsonLightInputContext odataJsonLightInputContext = new ODataJsonLightInputContext(this, messageStream, detectionInfo.ContentType, detectionInfo.GetEncoding(), detectionInfo.MessageReaderSettings, readingResponse, true, detectionInfo.Model, null))
			{
				enumerable = odataJsonLightInputContext.DetectPayloadKind(detectionInfo);
			}
			return enumerable;
		}
	}
}
