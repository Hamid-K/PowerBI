using System;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000067 RID: 103
	internal interface IDsqSortKeyVisitor<T, TArg>
	{
		// Token: 0x0600048F RID: 1167
		T Visit(DsqSortKeyExpression sortKey, TArg arg);

		// Token: 0x06000490 RID: 1168
		T Visit(DsqSortKeyProjection sortKey, TArg arg);
	}
}
