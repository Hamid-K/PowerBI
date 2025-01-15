using System;
using Microsoft.Data.OData;

namespace Microsoft.Mashup.Engine1.Library.OData.V3
{
	// Token: 0x020008CF RID: 2255
	internal class ODataStreamReferenceValueWrapper : IODataStreamReferenceValueWrapper
	{
		// Token: 0x06004072 RID: 16498 RVA: 0x000D702D File Offset: 0x000D522D
		public ODataStreamReferenceValueWrapper(ODataStreamReferenceValue streamReferenceValue)
		{
			this.streamReferenceValue = streamReferenceValue;
		}

		// Token: 0x170014C5 RID: 5317
		// (get) Token: 0x06004073 RID: 16499 RVA: 0x000D703C File Offset: 0x000D523C
		public string ContentType
		{
			get
			{
				return this.streamReferenceValue.ContentType;
			}
		}

		// Token: 0x170014C6 RID: 5318
		// (get) Token: 0x06004074 RID: 16500 RVA: 0x000D7049 File Offset: 0x000D5249
		public Uri EditLink
		{
			get
			{
				return this.streamReferenceValue.EditLink;
			}
		}

		// Token: 0x170014C7 RID: 5319
		// (get) Token: 0x06004075 RID: 16501 RVA: 0x000D7056 File Offset: 0x000D5256
		public string ETag
		{
			get
			{
				return this.streamReferenceValue.ETag;
			}
		}

		// Token: 0x170014C8 RID: 5320
		// (get) Token: 0x06004076 RID: 16502 RVA: 0x000D7063 File Offset: 0x000D5263
		public Uri ReadLink
		{
			get
			{
				return this.streamReferenceValue.ReadLink;
			}
		}

		// Token: 0x040021D7 RID: 8663
		private readonly ODataStreamReferenceValue streamReferenceValue;
	}
}
