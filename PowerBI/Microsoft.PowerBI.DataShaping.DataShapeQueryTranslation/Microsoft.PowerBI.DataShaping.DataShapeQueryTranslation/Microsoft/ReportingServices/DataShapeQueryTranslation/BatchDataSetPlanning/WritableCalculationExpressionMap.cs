using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x02000180 RID: 384
	internal sealed class WritableCalculationExpressionMap : CalculationExpressionMap
	{
		// Token: 0x06000D88 RID: 3464 RVA: 0x000378B1 File Offset: 0x00035AB1
		internal WritableCalculationExpressionMap()
		{
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x000378BC File Offset: 0x00035ABC
		public void AddExpression(Calculation calc, ExpressionId expression)
		{
			List<ExpressionId> list;
			if (!this.m_idToExpressions.TryGetValue(calc.Id, out list))
			{
				list = new List<ExpressionId>();
				this.m_idToExpressions.Add(calc.Id, list);
			}
			list.Add(expression);
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x000378FD File Offset: 0x00035AFD
		public ReadOnlyCalculationExpressionMap AsReadOnly()
		{
			return new ReadOnlyCalculationExpressionMap(this);
		}
	}
}
