using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200010C RID: 268
	public enum TransformationType
	{
		// Token: 0x04000443 RID: 1091
		Null,
		// Token: 0x04000444 RID: 1092
		Undefined,
		// Token: 0x04000445 RID: 1093
		StaticTransformation,
		// Token: 0x04000446 RID: 1094
		EditTransformation,
		// Token: 0x04000447 RID: 1095
		PrefixTransformation,
		// Token: 0x04000448 RID: 1096
		TokenMerge,
		// Token: 0x04000449 RID: 1097
		TokenSplit,
		// Token: 0x0400044A RID: 1098
		BingSynonym,
		// Token: 0x0400044B RID: 1099
		BridgeTable,
		// Token: 0x0400044C RID: 1100
		FuzzyTransformation
	}
}
