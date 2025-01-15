using System;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000B1 RID: 177
	internal interface IQueryGroupValueVisitor<T>
	{
		// Token: 0x06000683 RID: 1667
		T Visit(QueryGroupSingleValue value);

		// Token: 0x06000684 RID: 1668
		T Visit(QueryGroupIntervalValue value);
	}
}
