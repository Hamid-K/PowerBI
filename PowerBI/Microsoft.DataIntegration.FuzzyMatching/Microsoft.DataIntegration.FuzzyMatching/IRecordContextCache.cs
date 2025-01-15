using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200008B RID: 139
	public interface IRecordContextCache
	{
		// Token: 0x060005B3 RID: 1459
		bool TryGetRecordContext(ISession session, int rid, int lookupId, out RecordContext recordContext);

		// Token: 0x060005B4 RID: 1460
		void CacheRecordContext(int rid, int lookupId, RecordContext recordContext);

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060005B5 RID: 1461
		// (set) Token: 0x060005B6 RID: 1462
		bool EnableReferenceContextCaching { get; set; }
	}
}
