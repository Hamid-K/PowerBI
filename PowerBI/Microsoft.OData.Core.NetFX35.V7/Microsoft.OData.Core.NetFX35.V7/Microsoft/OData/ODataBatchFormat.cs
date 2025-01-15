using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData
{
	// Token: 0x0200002D RID: 45
	internal sealed class ODataBatchFormat : ODataFormat
	{
		// Token: 0x0600012E RID: 302 RVA: 0x0000582C File Offset: 0x00003A2C
		public override string ToString()
		{
			return "Batch";
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00005833 File Offset: 0x00003A33
		public override IEnumerable<ODataPayloadKind> DetectPayloadKind(ODataMessageInfo messageInfo, ODataMessageReaderSettings settings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			return ODataBatchFormat.DetectPayloadKindImplementation(messageInfo.MediaType);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000584C File Offset: 0x00003A4C
		public override ODataInputContext CreateInputContext(ODataMessageInfo messageInfo, ODataMessageReaderSettings messageReaderSettings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
			return new ODataRawInputContext(this, messageInfo, messageReaderSettings);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000586E File Offset: 0x00003A6E
		public override ODataOutputContext CreateOutputContext(ODataMessageInfo messageInfo, ODataMessageWriterSettings messageWriterSettings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageWriterSettings>(messageWriterSettings, "messageWriterSettings");
			return new ODataRawOutputContext(this, messageInfo, messageWriterSettings);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00005890 File Offset: 0x00003A90
		private static IEnumerable<ODataPayloadKind> DetectPayloadKindImplementation(ODataMediaType contentType)
		{
			if (HttpUtils.CompareMediaTypeNames("multipart", contentType.Type) && HttpUtils.CompareMediaTypeNames("mixed", contentType.SubType) && contentType.Parameters != null)
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
