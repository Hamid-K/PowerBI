using System;

namespace System.Data.Entity.Core.Mapping.ViewGeneration
{
	// Token: 0x0200056A RID: 1386
	internal enum PerfType
	{
		// Token: 0x04001828 RID: 6184
		InitialSetup,
		// Token: 0x04001829 RID: 6185
		CellCreation,
		// Token: 0x0400182A RID: 6186
		KeyConstraint,
		// Token: 0x0400182B RID: 6187
		ViewgenContext,
		// Token: 0x0400182C RID: 6188
		UpdateViews,
		// Token: 0x0400182D RID: 6189
		DisjointConstraint,
		// Token: 0x0400182E RID: 6190
		PartitionConstraint,
		// Token: 0x0400182F RID: 6191
		DomainConstraint,
		// Token: 0x04001830 RID: 6192
		ForeignConstraint,
		// Token: 0x04001831 RID: 6193
		QueryViews,
		// Token: 0x04001832 RID: 6194
		BoolResolution,
		// Token: 0x04001833 RID: 6195
		Unsatisfiability,
		// Token: 0x04001834 RID: 6196
		ViewParsing
	}
}
