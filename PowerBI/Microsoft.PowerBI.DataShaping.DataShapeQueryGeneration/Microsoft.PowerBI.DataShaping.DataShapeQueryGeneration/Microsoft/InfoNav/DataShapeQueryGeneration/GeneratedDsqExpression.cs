using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000084 RID: 132
	internal struct GeneratedDsqExpression
	{
		// Token: 0x06000543 RID: 1347 RVA: 0x00013490 File Offset: 0x00011690
		internal GeneratedDsqExpression(ExpressionNode expr, bool hasAggregate, IReadOnlyList<ResolvedQueryFilter> filters, bool? isScalar, ExpressionContent expressionContent)
		{
			this.Expression = expr;
			this.HasAggregate = hasAggregate;
			this.Filters = filters;
			this.IsScalar = isScalar;
			this.ExpressionContent = expressionContent;
		}

		// Token: 0x040002DD RID: 733
		internal readonly ExpressionNode Expression;

		// Token: 0x040002DE RID: 734
		internal readonly bool HasAggregate;

		// Token: 0x040002DF RID: 735
		internal readonly IReadOnlyList<ResolvedQueryFilter> Filters;

		// Token: 0x040002E0 RID: 736
		internal readonly bool? IsScalar;

		// Token: 0x040002E1 RID: 737
		internal readonly ExpressionContent ExpressionContent;
	}
}
