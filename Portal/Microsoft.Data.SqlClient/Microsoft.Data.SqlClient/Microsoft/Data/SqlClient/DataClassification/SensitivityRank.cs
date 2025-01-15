using System;

namespace Microsoft.Data.SqlClient.DataClassification
{
	// Token: 0x02000156 RID: 342
	public enum SensitivityRank
	{
		// Token: 0x04000A96 RID: 2710
		NOT_DEFINED = -1,
		// Token: 0x04000A97 RID: 2711
		NONE,
		// Token: 0x04000A98 RID: 2712
		LOW = 10,
		// Token: 0x04000A99 RID: 2713
		MEDIUM = 20,
		// Token: 0x04000A9A RID: 2714
		HIGH = 30,
		// Token: 0x04000A9B RID: 2715
		CRITICAL = 40
	}
}
