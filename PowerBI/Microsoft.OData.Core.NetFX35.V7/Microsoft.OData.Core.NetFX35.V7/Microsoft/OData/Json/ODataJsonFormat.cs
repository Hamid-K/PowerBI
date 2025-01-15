using System;
using System.Collections.Generic;
using Microsoft.OData.JsonLight;

namespace Microsoft.OData.Json
{
	// Token: 0x020001DF RID: 479
	internal sealed class ODataJsonFormat : ODataFormat
	{
		// Token: 0x060012C5 RID: 4805 RVA: 0x0003633B File Offset: 0x0003453B
		public override string ToString()
		{
			return "JsonLight";
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x00036342 File Offset: 0x00034542
		public override IEnumerable<ODataPayloadKind> DetectPayloadKind(ODataMessageInfo messageInfo, ODataMessageReaderSettings settings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			return ODataJsonFormat.DetectPayloadKindImplementation(messageInfo, settings);
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x00036357 File Offset: 0x00034557
		public override ODataInputContext CreateInputContext(ODataMessageInfo messageInfo, ODataMessageReaderSettings messageReaderSettings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
			return new ODataJsonLightInputContext(messageInfo, messageReaderSettings);
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x00036378 File Offset: 0x00034578
		public override ODataOutputContext CreateOutputContext(ODataMessageInfo messageInfo, ODataMessageWriterSettings messageWriterSettings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageWriterSettings>(messageWriterSettings, "messageWriterSettings");
			return new ODataJsonLightOutputContext(messageInfo, messageWriterSettings);
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x0003639C File Offset: 0x0003459C
		private static IEnumerable<ODataPayloadKind> DetectPayloadKindImplementation(ODataMessageInfo messageInfo, ODataMessageReaderSettings settings)
		{
			ODataPayloadKindDetectionInfo odataPayloadKindDetectionInfo = new ODataPayloadKindDetectionInfo(messageInfo, settings);
			messageInfo.Encoding = odataPayloadKindDetectionInfo.GetEncoding();
			IEnumerable<ODataPayloadKind> enumerable;
			using (ODataJsonLightInputContext odataJsonLightInputContext = new ODataJsonLightInputContext(messageInfo, settings))
			{
				enumerable = odataJsonLightInputContext.DetectPayloadKind(odataPayloadKindDetectionInfo);
			}
			return enumerable;
		}
	}
}
