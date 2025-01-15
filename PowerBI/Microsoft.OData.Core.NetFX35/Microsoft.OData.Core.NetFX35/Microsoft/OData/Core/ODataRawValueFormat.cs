using System;
using System.Collections.Generic;

namespace Microsoft.OData.Core
{
	// Token: 0x02000198 RID: 408
	internal sealed class ODataRawValueFormat : ODataFormat
	{
		// Token: 0x06000F53 RID: 3923 RVA: 0x000356B1 File Offset: 0x000338B1
		public override string ToString()
		{
			return "RawValue";
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x000356B8 File Offset: 0x000338B8
		public override IEnumerable<ODataPayloadKind> DetectPayloadKind(ODataMessageInfo messageInfo, ODataMessageReaderSettings settings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			return ODataRawValueFormat.DetectPayloadKindImplementation(messageInfo.MediaType);
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x000356D0 File Offset: 0x000338D0
		public override ODataInputContext CreateInputContext(ODataMessageInfo messageInfo, ODataMessageReaderSettings messageReaderSettings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
			return new ODataRawInputContext(this, messageInfo.GetMessageStream.Invoke(), messageInfo.Encoding, messageReaderSettings, messageInfo.IsResponse, true, messageInfo.Model, messageInfo.UrlResolver, messageInfo.PayloadKind);
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x00035724 File Offset: 0x00033924
		public override ODataOutputContext CreateOutputContext(ODataMessageInfo messageInfo, ODataMessageWriterSettings messageWriterSettings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageWriterSettings>(messageWriterSettings, "messageWriterSettings");
			return new ODataRawOutputContext(this, messageInfo.GetMessageStream.Invoke(), messageInfo.Encoding, messageWriterSettings, messageInfo.IsResponse, true, messageInfo.Model, messageInfo.UrlResolver);
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x00035774 File Offset: 0x00033974
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
