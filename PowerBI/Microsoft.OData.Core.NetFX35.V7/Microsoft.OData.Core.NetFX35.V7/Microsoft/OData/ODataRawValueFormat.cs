using System;
using System.Collections.Generic;

namespace Microsoft.OData
{
	// Token: 0x0200008F RID: 143
	internal sealed class ODataRawValueFormat : ODataFormat
	{
		// Token: 0x0600058B RID: 1419 RVA: 0x0000F3EB File Offset: 0x0000D5EB
		public override string ToString()
		{
			return "RawValue";
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0000F3F2 File Offset: 0x0000D5F2
		public override IEnumerable<ODataPayloadKind> DetectPayloadKind(ODataMessageInfo messageInfo, ODataMessageReaderSettings settings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			return ODataRawValueFormat.DetectPayloadKindImplementation(messageInfo.MediaType);
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0000584C File Offset: 0x00003A4C
		public override ODataInputContext CreateInputContext(ODataMessageInfo messageInfo, ODataMessageReaderSettings messageReaderSettings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
			return new ODataRawInputContext(this, messageInfo, messageReaderSettings);
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0000586E File Offset: 0x00003A6E
		public override ODataOutputContext CreateOutputContext(ODataMessageInfo messageInfo, ODataMessageWriterSettings messageWriterSettings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageWriterSettings>(messageWriterSettings, "messageWriterSettings");
			return new ODataRawOutputContext(this, messageInfo, messageWriterSettings);
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0000F40B File Offset: 0x0000D60B
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
