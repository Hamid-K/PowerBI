using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x0200008C RID: 140
	internal static class LimitPropertyConstants
	{
		// Token: 0x04000177 RID: 375
		internal const string DbCount = "DbCount";

		// Token: 0x04000178 RID: 376
		internal const string IsExceededDbCount = "IsExceededDbCount";

		// Token: 0x04000179 RID: 377
		internal const string DbPrimaryCount = "DbPrimaryCount";

		// Token: 0x0400017A RID: 378
		internal const string DbSecondaryCount = "DbSecondaryCount";

		// Token: 0x0400017B RID: 379
		internal const string XTransformApplied = "XTransformApplied";

		// Token: 0x0400017C RID: 380
		internal const string YTransformApplied = "YTransformApplied";

		// Token: 0x0400017D RID: 381
		internal static readonly StringComparer NameComparer = StringComparer.Ordinal;
	}
}
