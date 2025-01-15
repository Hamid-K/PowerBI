using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.OData
{
	// Token: 0x020000B2 RID: 178
	internal sealed class ODataRawValueFormat : ODataFormat
	{
		// Token: 0x060007BB RID: 1979 RVA: 0x00012AC6 File Offset: 0x00010CC6
		public override string ToString()
		{
			return "RawValue";
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00012ACD File Offset: 0x00010CCD
		public override IEnumerable<ODataPayloadKind> DetectPayloadKind(ODataMessageInfo messageInfo, ODataMessageReaderSettings settings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			return ODataRawValueFormat.DetectPayloadKindImplementation(messageInfo.MediaType);
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x00012AE6 File Offset: 0x00010CE6
		public override ODataInputContext CreateInputContext(ODataMessageInfo messageInfo, ODataMessageReaderSettings messageReaderSettings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
			return new ODataRawInputContext(this, messageInfo, messageReaderSettings);
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x00012B08 File Offset: 0x00010D08
		public override ODataOutputContext CreateOutputContext(ODataMessageInfo messageInfo, ODataMessageWriterSettings messageWriterSettings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageWriterSettings>(messageWriterSettings, "messageWriterSettings");
			return new ODataRawOutputContext(this, messageInfo, messageWriterSettings);
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00012B2C File Offset: 0x00010D2C
		public override Task<IEnumerable<ODataPayloadKind>> DetectPayloadKindAsync(ODataMessageInfo messageInfo, ODataMessageReaderSettings settings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			return TaskUtils.GetTaskForSynchronousOperation<IEnumerable<ODataPayloadKind>>(() => ODataRawValueFormat.DetectPayloadKindImplementation(messageInfo.MediaType));
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00012B68 File Offset: 0x00010D68
		public override Task<ODataInputContext> CreateInputContextAsync(ODataMessageInfo messageInfo, ODataMessageReaderSettings messageReaderSettings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
			return Task.FromResult<ODataInputContext>(new ODataRawInputContext(this, messageInfo, messageReaderSettings));
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00012B8F File Offset: 0x00010D8F
		public override Task<ODataOutputContext> CreateOutputContextAsync(ODataMessageInfo messageInfo, ODataMessageWriterSettings messageWriterSettings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "message");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageWriterSettings>(messageWriterSettings, "messageWriterSettings");
			return Task.FromResult<ODataOutputContext>(new ODataRawOutputContext(this, messageInfo, messageWriterSettings));
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00012BB6 File Offset: 0x00010DB6
		private static IEnumerable<ODataPayloadKind> DetectPayloadKindImplementation(ODataMediaType contentType)
		{
			if (HttpUtils.CompareMediaTypeNames("text", contentType.Type) && HttpUtils.CompareMediaTypeNames("text/plain", contentType.SubType))
			{
				return new ODataPayloadKind[] { ODataPayloadKind.Value };
			}
			return new ODataPayloadKind[] { ODataPayloadKind.BinaryValue };
		}
	}
}
