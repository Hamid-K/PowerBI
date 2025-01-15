using System;
using System.Data;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000053 RID: 83
	public interface IMatchResult
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000320 RID: 800
		IDataRecord InputRecord { get; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000321 RID: 801
		IDataRecord RightRecord { get; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000322 RID: 802
		int RightRecordId { get; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000323 RID: 803
		ComparisonResult ComparisonResult { get; }
	}
}
