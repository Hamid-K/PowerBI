using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000AC RID: 172
	internal abstract class QueryGroupValue
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600065E RID: 1630
		internal abstract bool? IsScalar { get; }

		// Token: 0x0600065F RID: 1631
		internal abstract T Accept<T>(IQueryGroupValueVisitor<T> visitor);

		// Token: 0x06000660 RID: 1632
		internal abstract bool MatchesExpression(ExpressionNode expression);
	}
}
