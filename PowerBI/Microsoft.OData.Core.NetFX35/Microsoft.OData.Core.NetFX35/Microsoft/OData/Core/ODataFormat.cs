using System;
using System.Collections.Generic;
using Microsoft.OData.Core.Atom;
using Microsoft.OData.Core.Json;

namespace Microsoft.OData.Core
{
	// Token: 0x0200004D RID: 77
	public abstract class ODataFormat
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000BCA8 File Offset: 0x00009EA8
		[Obsolete("ATOM support is obsolete.")]
		public static ODataFormat Atom
		{
			get
			{
				return ODataFormat.atomFormat;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000BCAF File Offset: 0x00009EAF
		public static ODataFormat Json
		{
			get
			{
				return ODataFormat.JsonFormat;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0000BCB6 File Offset: 0x00009EB6
		public static ODataFormat RawValue
		{
			get
			{
				return ODataFormat.rawValueFormat;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000BCBD File Offset: 0x00009EBD
		public static ODataFormat Batch
		{
			get
			{
				return ODataFormat.batchFormat;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000BCC4 File Offset: 0x00009EC4
		public static ODataFormat Metadata
		{
			get
			{
				return ODataFormat.metadataFormat;
			}
		}

		// Token: 0x060002CE RID: 718
		public abstract IEnumerable<ODataPayloadKind> DetectPayloadKind(ODataMessageInfo messageInfo, ODataMessageReaderSettings settings);

		// Token: 0x060002CF RID: 719
		public abstract ODataInputContext CreateInputContext(ODataMessageInfo messageInfo, ODataMessageReaderSettings messageReaderSettings);

		// Token: 0x060002D0 RID: 720
		public abstract ODataOutputContext CreateOutputContext(ODataMessageInfo messageInfo, ODataMessageWriterSettings messageWriterSettings);

		// Token: 0x0400017E RID: 382
		private static ODataAtomFormat atomFormat = new ODataAtomFormat();

		// Token: 0x0400017F RID: 383
		private static ODataJsonFormat JsonFormat = new ODataJsonFormat();

		// Token: 0x04000180 RID: 384
		private static ODataRawValueFormat rawValueFormat = new ODataRawValueFormat();

		// Token: 0x04000181 RID: 385
		private static ODataBatchFormat batchFormat = new ODataBatchFormat();

		// Token: 0x04000182 RID: 386
		private static ODataMetadataFormat metadataFormat = new ODataMetadataFormat();
	}
}
