using System;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001DF RID: 479
	internal class SingleRowPlaceholderTableBuilder
	{
		// Token: 0x0600109D RID: 4253 RVA: 0x00045468 File Offset: 0x00043668
		public SingleRowPlaceholderTableBuilder(IAggregatesPlanningContext plannerContext, IScope containingScope, string columnName = "Placeholder")
		{
			this._plannerContext = plannerContext;
			this._containingScope = containingScope;
			this._columnName = columnName;
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x00045485 File Offset: 0x00043685
		internal PlanOperationContext ToTableContext()
		{
			return new PlanOperationContext(SingleRowPlaceholderTableBuilder.CreateVisualCalcsInputPlaceholderTable(this._plannerContext, this._columnName), this._containingScope.AsList<IScope>(), Array.Empty<Calculation>());
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x000454B0 File Offset: 0x000436B0
		private static PlanOperation CreateVisualCalcsInputPlaceholderTable(IAggregatesPlanningContext plannerContext, string columnName)
		{
			ExpressionId expressionId = plannerContext.OutputExpressionTable.Add(LiteralExpressionNode.NullInt64);
			ExpressionContext expressionContext = new ExpressionContext(plannerContext.ErrorContext, ObjectType.DataShape, new Identifier("VisualCalcPlaceholder"), "VisualCalcPlaceholder");
			return PlanOperationBuilder.SingleRow(null, null, null, new SingleRowAdditionalColumn(columnName, expressionId, expressionContext).AsEnumerable<SingleRowAdditionalColumn>());
		}

		// Token: 0x040007C8 RID: 1992
		private readonly IAggregatesPlanningContext _plannerContext;

		// Token: 0x040007C9 RID: 1993
		private readonly IScope _containingScope;

		// Token: 0x040007CA RID: 1994
		private readonly string _columnName;
	}
}
