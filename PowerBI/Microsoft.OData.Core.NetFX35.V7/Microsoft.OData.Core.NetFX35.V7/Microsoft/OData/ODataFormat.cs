using System;
using System.Collections.Generic;
using Microsoft.OData.Json;

namespace Microsoft.OData
{
	// Token: 0x02000065 RID: 101
	public abstract class ODataFormat
	{
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600033E RID: 830 RVA: 0x0000A367 File Offset: 0x00008567
		public static ODataFormat Json
		{
			get
			{
				return ODataFormat.JsonFormat;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000A36E File Offset: 0x0000856E
		public static ODataFormat RawValue
		{
			get
			{
				return ODataFormat.rawValueFormat;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000A375 File Offset: 0x00008575
		public static ODataFormat Batch
		{
			get
			{
				return ODataFormat.batchFormat;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000A37C File Offset: 0x0000857C
		public static ODataFormat Metadata
		{
			get
			{
				return ODataFormat.metadataFormat;
			}
		}

		// Token: 0x06000342 RID: 834
		public abstract IEnumerable<ODataPayloadKind> DetectPayloadKind(ODataMessageInfo messageInfo, ODataMessageReaderSettings settings);

		// Token: 0x06000343 RID: 835
		public abstract ODataInputContext CreateInputContext(ODataMessageInfo messageInfo, ODataMessageReaderSettings messageReaderSettings);

		// Token: 0x06000344 RID: 836
		public abstract ODataOutputContext CreateOutputContext(ODataMessageInfo messageInfo, ODataMessageWriterSettings messageWriterSettings);

		// Token: 0x040001BF RID: 447
		private static ODataJsonFormat JsonFormat = new ODataJsonFormat();

		// Token: 0x040001C0 RID: 448
		private static ODataRawValueFormat rawValueFormat = new ODataRawValueFormat();

		// Token: 0x040001C1 RID: 449
		private static ODataBatchFormat batchFormat = new ODataBatchFormat();

		// Token: 0x040001C2 RID: 450
		private static ODataMetadataFormat metadataFormat = new ODataMetadataFormat();
	}
}
