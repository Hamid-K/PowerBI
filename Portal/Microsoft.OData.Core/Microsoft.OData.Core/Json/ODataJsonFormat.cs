using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.OData.JsonLight;

namespace Microsoft.OData.Json
{
	// Token: 0x02000211 RID: 529
	internal sealed class ODataJsonFormat : ODataFormat
	{
		// Token: 0x06001728 RID: 5928 RVA: 0x000419E3 File Offset: 0x0003FBE3
		public override string ToString()
		{
			return "JsonLight";
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x000419EA File Offset: 0x0003FBEA
		public override IEnumerable<ODataPayloadKind> DetectPayloadKind(ODataMessageInfo messageInfo, ODataMessageReaderSettings settings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			return ODataJsonFormat.DetectPayloadKindImplementation(messageInfo, settings);
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x000419FF File Offset: 0x0003FBFF
		public override ODataInputContext CreateInputContext(ODataMessageInfo messageInfo, ODataMessageReaderSettings messageReaderSettings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
			return new ODataJsonLightInputContext(messageInfo, messageReaderSettings);
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x00041A20 File Offset: 0x0003FC20
		public override ODataOutputContext CreateOutputContext(ODataMessageInfo messageInfo, ODataMessageWriterSettings messageWriterSettings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageWriterSettings>(messageWriterSettings, "messageWriterSettings");
			return new ODataJsonLightOutputContext(messageInfo, messageWriterSettings);
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x00041A41 File Offset: 0x0003FC41
		public override Task<IEnumerable<ODataPayloadKind>> DetectPayloadKindAsync(ODataMessageInfo messageInfo, ODataMessageReaderSettings settings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			return ODataJsonFormat.DetectPayloadKindImplementationAsync(messageInfo, settings);
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x00041A56 File Offset: 0x0003FC56
		public override Task<ODataInputContext> CreateInputContextAsync(ODataMessageInfo messageInfo, ODataMessageReaderSettings messageReaderSettings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
			return Task.FromResult<ODataInputContext>(new ODataJsonLightInputContext(messageInfo, messageReaderSettings));
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x00041A7C File Offset: 0x0003FC7C
		public override Task<ODataOutputContext> CreateOutputContextAsync(ODataMessageInfo messageInfo, ODataMessageWriterSettings messageWriterSettings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageWriterSettings>(messageWriterSettings, "messageWriterSettings");
			return Task.FromResult<ODataOutputContext>(new ODataJsonLightOutputContext(messageInfo, messageWriterSettings));
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x00041AA4 File Offset: 0x0003FCA4
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

		// Token: 0x06001730 RID: 5936 RVA: 0x00041AF4 File Offset: 0x0003FCF4
		private static Task<IEnumerable<ODataPayloadKind>> DetectPayloadKindImplementationAsync(ODataMessageInfo messageInfo, ODataMessageReaderSettings settings)
		{
			ODataPayloadKindDetectionInfo odataPayloadKindDetectionInfo = new ODataPayloadKindDetectionInfo(messageInfo, settings);
			messageInfo.Encoding = odataPayloadKindDetectionInfo.GetEncoding();
			ODataJsonLightInputContext jsonLightInputContext = new ODataJsonLightInputContext(messageInfo, settings);
			return jsonLightInputContext.DetectPayloadKindAsync(odataPayloadKindDetectionInfo).FollowAlwaysWith(delegate(Task<IEnumerable<ODataPayloadKind>> t)
			{
				jsonLightInputContext.Dispose();
			});
		}
	}
}
