using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200009A RID: 154
	internal abstract class QueryColumnAdapter<TColumn>
	{
		// Token: 0x060005C3 RID: 1475
		internal abstract ExpressionNode ToDsqExpression(TColumn column);

		// Token: 0x060005C4 RID: 1476
		internal abstract IConceptualColumn GetConceptualColumn(TColumn column);

		// Token: 0x060005C5 RID: 1477
		internal abstract string GetFormatString(TColumn column);

		// Token: 0x060005C6 RID: 1478
		internal abstract TColumn GetOrCreateColumn(IConceptualColumn newColumn, TColumn existingColumn, DsqExpressionGenerator expressionGenerator, bool propagateRoleAndOmitFromOutput);
	}
}
