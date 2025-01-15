using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x02000097 RID: 151
	internal sealed class FilterConditionComparer : ExpressionEquatableComparer<FilterCondition>
	{
		// Token: 0x06000391 RID: 913 RVA: 0x000070C8 File Offset: 0x000052C8
		internal FilterConditionComparer(IEqualityComparer<Expression> exprComparer)
			: base(exprComparer)
		{
		}

		// Token: 0x04000197 RID: 407
		internal static readonly ExpressionComparerById DefaultExpressionComparer = ExpressionComparerById.Instance;
	}
}
