using System;

namespace Microsoft.OData.Client
{
	// Token: 0x020000DB RID: 219
	[Flags]
	public enum EntityStates
	{
		// Token: 0x04000348 RID: 840
		Detached = 1,
		// Token: 0x04000349 RID: 841
		Unchanged = 2,
		// Token: 0x0400034A RID: 842
		Added = 4,
		// Token: 0x0400034B RID: 843
		Deleted = 8,
		// Token: 0x0400034C RID: 844
		Modified = 16
	}
}
