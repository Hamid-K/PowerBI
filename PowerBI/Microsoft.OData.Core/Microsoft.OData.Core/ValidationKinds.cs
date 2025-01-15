using System;

namespace Microsoft.OData
{
	// Token: 0x02000024 RID: 36
	[Flags]
	public enum ValidationKinds
	{
		// Token: 0x0400006A RID: 106
		None = 0,
		// Token: 0x0400006B RID: 107
		ThrowOnDuplicatePropertyNames = 1,
		// Token: 0x0400006C RID: 108
		ThrowOnUndeclaredPropertyForNonOpenType = 2,
		// Token: 0x0400006D RID: 109
		ThrowIfTypeConflictsWithMetadata = 4,
		// Token: 0x0400006E RID: 110
		All = -1
	}
}
