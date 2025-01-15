using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Core
{
	// Token: 0x0200013B RID: 315
	internal sealed class ODataBatchFormat : ODataFormat
	{
		// Token: 0x06000BF8 RID: 3064 RVA: 0x0002D254 File Offset: 0x0002B454
		public override string ToString()
		{
			return "Batch";
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x0002D25B File Offset: 0x0002B45B
		public override IEnumerable<ODataPayloadKind> DetectPayloadKind(ODataMessageInfo messageInfo, ODataMessageReaderSettings settings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			return ODataBatchFormat.DetectPayloadKindImplementation(messageInfo.MediaType);
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x0002D274 File Offset: 0x0002B474
		public override ODataInputContext CreateInputContext(ODataMessageInfo messageInfo, ODataMessageReaderSettings messageReaderSettings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
			return new ODataRawInputContext(this, messageInfo.GetMessageStream.Invoke(), messageInfo.Encoding, messageReaderSettings, messageInfo.IsResponse, true, messageInfo.Model, messageInfo.UrlResolver, messageInfo.PayloadKind);
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x0002D2C8 File Offset: 0x0002B4C8
		public override ODataOutputContext CreateOutputContext(ODataMessageInfo messageInfo, ODataMessageWriterSettings messageWriterSettings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageWriterSettings>(messageWriterSettings, "messageWriterSettings");
			return new ODataRawOutputContext(this, messageInfo.GetMessageStream.Invoke(), messageInfo.Encoding, messageWriterSettings, messageInfo.IsResponse, true, messageInfo.Model, messageInfo.UrlResolver);
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x0002D32C File Offset: 0x0002B52C
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
