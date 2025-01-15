using System;

namespace Microsoft.AnalysisServices.Tabular.Tmdl
{
	// Token: 0x0200013D RID: 317
	internal enum TmdlLineType
	{
		// Token: 0x0400037B RID: 891
		Unknown,
		// Token: 0x0400037C RID: 892
		Empty,
		// Token: 0x0400037D RID: 893
		Description,
		// Token: 0x0400037E RID: 894
		ReferenceObject,
		// Token: 0x0400037F RID: 895
		NamedObject,
		// Token: 0x04000380 RID: 896
		NamedObjectWithDefaultProperty,
		// Token: 0x04000381 RID: 897
		Property,
		// Token: 0x04000382 RID: 898
		OldSyntaxExpressionProperty,
		// Token: 0x04000383 RID: 899
		ElementWithDefaultPropertyOrExpression,
		// Token: 0x04000384 RID: 900
		UnnamedObjectOrSimplifiedProperty,
		// Token: 0x04000385 RID: 901
		Other
	}
}
