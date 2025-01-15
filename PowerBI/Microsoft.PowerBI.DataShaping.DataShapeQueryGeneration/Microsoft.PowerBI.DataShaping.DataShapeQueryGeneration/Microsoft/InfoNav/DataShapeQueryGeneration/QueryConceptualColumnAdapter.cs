using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200009B RID: 155
	internal sealed class QueryConceptualColumnAdapter : QueryColumnAdapter<IConceptualColumn>
	{
		// Token: 0x060005C8 RID: 1480 RVA: 0x00016744 File Offset: 0x00014944
		private QueryConceptualColumnAdapter()
		{
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0001674C File Offset: 0x0001494C
		internal override ExpressionNode ToDsqExpression(IConceptualColumn column)
		{
			return column.DsqExpression();
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00016754 File Offset: 0x00014954
		internal override IConceptualColumn GetConceptualColumn(IConceptualColumn column)
		{
			return column;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00016757 File Offset: 0x00014957
		internal override string GetFormatString(IConceptualColumn column)
		{
			return column.FormatString;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0001675F File Offset: 0x0001495F
		internal override IConceptualColumn GetOrCreateColumn(IConceptualColumn newColumn, IConceptualColumn existingColumn, DsqExpressionGenerator expressionGenerator, bool propagateRoleAndOmitFromOutput)
		{
			return newColumn;
		}

		// Token: 0x0400032F RID: 815
		internal static readonly QueryConceptualColumnAdapter Instance = new QueryConceptualColumnAdapter();
	}
}
