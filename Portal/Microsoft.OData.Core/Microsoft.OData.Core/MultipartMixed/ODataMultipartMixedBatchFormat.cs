using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.OData.MultipartMixed
{
	// Token: 0x02000202 RID: 514
	internal sealed class ODataMultipartMixedBatchFormat : ODataFormat
	{
		// Token: 0x060016A8 RID: 5800 RVA: 0x0003F4CF File Offset: 0x0003D6CF
		public override string ToString()
		{
			return "Batch";
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x0003F4D6 File Offset: 0x0003D6D6
		public override IEnumerable<ODataPayloadKind> DetectPayloadKind(ODataMessageInfo messageInfo, ODataMessageReaderSettings settings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			return ODataMultipartMixedBatchFormat.DetectPayloadKindImplementation(messageInfo.MediaType);
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x0003F4EF File Offset: 0x0003D6EF
		public override ODataInputContext CreateInputContext(ODataMessageInfo messageInfo, ODataMessageReaderSettings messageReaderSettings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
			return new ODataMultipartMixedBatchInputContext(this, messageInfo, messageReaderSettings);
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x0003F511 File Offset: 0x0003D711
		public override ODataOutputContext CreateOutputContext(ODataMessageInfo messageInfo, ODataMessageWriterSettings messageWriterSettings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageWriterSettings>(messageWriterSettings, "messageWriterSettings");
			return new ODataMultipartMixedBatchOutputContext(this, messageInfo, messageWriterSettings);
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x0003F534 File Offset: 0x0003D734
		public override Task<IEnumerable<ODataPayloadKind>> DetectPayloadKindAsync(ODataMessageInfo messageInfo, ODataMessageReaderSettings settings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			return TaskUtils.GetTaskForSynchronousOperation<IEnumerable<ODataPayloadKind>>(() => ODataMultipartMixedBatchFormat.DetectPayloadKindImplementation(messageInfo.MediaType));
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x0003F570 File Offset: 0x0003D770
		public override Task<ODataInputContext> CreateInputContextAsync(ODataMessageInfo messageInfo, ODataMessageReaderSettings messageReaderSettings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
			return Task.FromResult<ODataInputContext>(new ODataMultipartMixedBatchInputContext(this, messageInfo, messageReaderSettings));
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x0003F597 File Offset: 0x0003D797
		public override Task<ODataOutputContext> CreateOutputContextAsync(ODataMessageInfo messageInfo, ODataMessageWriterSettings messageWriterSettings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageWriterSettings>(messageWriterSettings, "messageWriterSettings");
			return Task.FromResult<ODataOutputContext>(new ODataMultipartMixedBatchOutputContext(this, messageInfo, messageWriterSettings));
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x0003F5C0 File Offset: 0x0003D7C0
		internal override string GetContentType(ODataMediaType mediaType, Encoding encoding, bool writingResponse, out IEnumerable<KeyValuePair<string, string>> mediaTypeParameters)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMediaType>(mediaType, "mediaType");
			IEnumerable<KeyValuePair<string, string>> enumerable2;
			if (mediaType.Parameters == null)
			{
				IEnumerable<KeyValuePair<string, string>> enumerable = new List<KeyValuePair<string, string>>();
				enumerable2 = enumerable;
			}
			else
			{
				enumerable2 = mediaType.Parameters;
			}
			IEnumerable<KeyValuePair<string, string>> enumerable3 = enumerable2;
			IEnumerable<KeyValuePair<string, string>> enumerable4 = enumerable3.Where((KeyValuePair<string, string> p) => string.Compare(p.Key, "boundary", StringComparison.OrdinalIgnoreCase) == 0);
			if (enumerable4.Count<KeyValuePair<string, string>>() > 1)
			{
				throw new ODataContentTypeException(Strings.MediaTypeUtils_NoOrMoreThanOneContentTypeSpecified(mediaType.ToText()));
			}
			string text;
			if (enumerable4.Count<KeyValuePair<string, string>>() == 1)
			{
				text = enumerable4.First<KeyValuePair<string, string>>().Value;
				mediaTypeParameters = mediaType.Parameters;
			}
			else
			{
				text = ODataMultipartMixedBatchWriterUtils.CreateBatchBoundary(writingResponse);
				mediaTypeParameters = new List<KeyValuePair<string, string>>(enumerable3)
				{
					new KeyValuePair<string, string>("boundary", text)
				};
			}
			return ODataMultipartMixedBatchWriterUtils.CreateMultipartMixedContentType(text);
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x0003F684 File Offset: 0x0003D884
		private static IEnumerable<ODataPayloadKind> DetectPayloadKindImplementation(ODataMediaType contentType)
		{
			if (HttpUtils.CompareMediaTypeNames("multipart", contentType.Type) && HttpUtils.CompareMediaTypeNames("mixed", contentType.SubType) && contentType.Parameters != null)
			{
				if (contentType.Parameters.Any((KeyValuePair<string, string> kvp) => HttpUtils.CompareMediaTypeParameterNames("boundary", kvp.Key)))
				{
					return new ODataPayloadKind[] { ODataPayloadKind.Batch };
				}
			}
			return Enumerable.Empty<ODataPayloadKind>();
		}
	}
}
