using System;

namespace Microsoft.OData
{
	// Token: 0x020000D8 RID: 216
	[Flags]
	public enum ValidationKinds
	{
		// Token: 0x04000395 RID: 917
		None = 0,
		// Token: 0x04000396 RID: 918
		ThrowOnDuplicatePropertyNames = 1,
		// Token: 0x04000397 RID: 919
		ThrowOnUndeclaredPropertyForNonOpenType = 2,
		// Token: 0x04000398 RID: 920
		ThrowIfTypeConflictsWithMetadata = 4,
		// Token: 0x04000399 RID: 921
		All = -1
	}
}
