using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.OData.Json;
using Microsoft.OData.MultipartMixed;

namespace Microsoft.OData
{
	// Token: 0x0200008B RID: 139
	public abstract class ODataFormat
	{
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x0000C40A File Offset: 0x0000A60A
		public static ODataFormat Json
		{
			get
			{
				return ODataFormat.JsonFormat;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x0000C411 File Offset: 0x0000A611
		public static ODataFormat RawValue
		{
			get
			{
				return ODataFormat.rawValueFormat;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x0000C418 File Offset: 0x0000A618
		public static ODataFormat Batch
		{
			get
			{
				return ODataFormat.batchFormat;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x0000C41F File Offset: 0x0000A61F
		public static ODataFormat Metadata
		{
			get
			{
				return ODataFormat.metadataFormat;
			}
		}

		// Token: 0x060004E3 RID: 1251
		public abstract IEnumerable<ODataPayloadKind> DetectPayloadKind(ODataMessageInfo messageInfo, ODataMessageReaderSettings settings);

		// Token: 0x060004E4 RID: 1252
		public abstract ODataInputContext CreateInputContext(ODataMessageInfo messageInfo, ODataMessageReaderSettings messageReaderSettings);

		// Token: 0x060004E5 RID: 1253
		public abstract ODataOutputContext CreateOutputContext(ODataMessageInfo messageInfo, ODataMessageWriterSettings messageWriterSettings);

		// Token: 0x060004E6 RID: 1254
		public abstract Task<IEnumerable<ODataPayloadKind>> DetectPayloadKindAsync(ODataMessageInfo messageInfo, ODataMessageReaderSettings settings);

		// Token: 0x060004E7 RID: 1255
		public abstract Task<ODataInputContext> CreateInputContextAsync(ODataMessageInfo messageInfo, ODataMessageReaderSettings messageReaderSettings);

		// Token: 0x060004E8 RID: 1256
		public abstract Task<ODataOutputContext> CreateOutputContextAsync(ODataMessageInfo messageInfo, ODataMessageWriterSettings messageWriterSettings);

		// Token: 0x060004E9 RID: 1257 RVA: 0x0000C426 File Offset: 0x0000A626
		internal virtual string GetContentType(ODataMediaType mediaType, Encoding encoding, bool writingResponse, out IEnumerable<KeyValuePair<string, string>> mediaTypeParameters)
		{
			mediaTypeParameters = mediaType.Parameters;
			return HttpUtils.BuildContentType(mediaType, encoding);
		}

		// Token: 0x04000222 RID: 546
		private static ODataJsonFormat JsonFormat = new ODataJsonFormat();

		// Token: 0x04000223 RID: 547
		private static ODataRawValueFormat rawValueFormat = new ODataRawValueFormat();

		// Token: 0x04000224 RID: 548
		private static ODataMultipartMixedBatchFormat batchFormat = new ODataMultipartMixedBatchFormat();

		// Token: 0x04000225 RID: 549
		private static ODataMetadataFormat metadataFormat = new ODataMetadataFormat();
	}
}
