using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000218 RID: 536
	internal class DMIndexSchema : IIndexSchema
	{
		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x060011CD RID: 4557 RVA: 0x00038F57 File Offset: 0x00037157
		// (set) Token: 0x060011CE RID: 4558 RVA: 0x00038F5F File Offset: 0x0003715F
		public GetKeyFromCacheItemDelegate TagExtractorDelegate
		{
			get
			{
				return this._tagExtractorDelegate;
			}
			set
			{
				this._tagExtractorDelegate = value;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x060011CF RID: 4559 RVA: 0x00038F68 File Offset: 0x00037168
		// (set) Token: 0x060011D0 RID: 4560 RVA: 0x00038F70 File Offset: 0x00037170
		public IIndexStoreSchema IndexStoreSchema
		{
			get
			{
				return this._indexStoreSchema;
			}
			set
			{
				this._indexStoreSchema = value;
			}
		}

		// Token: 0x04000B04 RID: 2820
		private GetKeyFromCacheItemDelegate _tagExtractorDelegate;

		// Token: 0x04000B05 RID: 2821
		private IIndexStoreSchema _indexStoreSchema;
	}
}
