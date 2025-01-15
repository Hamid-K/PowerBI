using System;
using System.Collections.Generic;
using System.Data;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000AF RID: 175
	public interface IFuzzyComparer
	{
		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060006C9 RID: 1737
		bool IsSymmetric { get; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060006CA RID: 1738
		// (set) Token: 0x060006CB RID: 1739
		double Threshold { get; set; }

		// Token: 0x060006CC RID: 1740
		void Initialize(RecordBinding leftRecordBinding, RecordBinding rightRecordBinding, IEnumerable<string> domains, IEnumerable<string> exactMatchDomains);

		// Token: 0x060006CD RID: 1741
		ISession CreateSession(IDomainManager domainManager, ITokenIdProvider tokenIdProvider);

		// Token: 0x060006CE RID: 1742
		void ResetLeftRecord(ISession session, RecordContext leftRecordContext);

		// Token: 0x060006CF RID: 1743
		void ResetRightRecord(ISession session, RecordContext rightRecordContext);

		// Token: 0x060006D0 RID: 1744
		bool Compare(ISession session, out ComparisonResult result);

		// Token: 0x060006D1 RID: 1745
		bool Compare(ISession session, IDataRecord leftRecord, IDataRecord rightRecord, out ComparisonResult result);
	}
}
