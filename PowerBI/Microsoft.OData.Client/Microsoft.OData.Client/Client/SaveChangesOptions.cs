using System;

namespace Microsoft.OData.Client
{
	// Token: 0x020000DC RID: 220
	[Flags]
	public enum SaveChangesOptions
	{
		// Token: 0x0400034E RID: 846
		None = 0,
		// Token: 0x0400034F RID: 847
		BatchWithSingleChangeset = 1,
		// Token: 0x04000350 RID: 848
		ContinueOnError = 2,
		// Token: 0x04000351 RID: 849
		ReplaceOnUpdate = 4,
		// Token: 0x04000352 RID: 850
		BatchWithIndependentOperations = 16,
		// Token: 0x04000353 RID: 851
		PostOnlySetProperties = 8
	}
}
