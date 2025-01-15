using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x02000196 RID: 406
	internal static class PlanExpressionBuilder
	{
		// Token: 0x06000DDE RID: 3550 RVA: 0x000389BA File Offset: 0x00036BBA
		internal static ExpressionNode CountRows(this PlanOperationContext input)
		{
			return input.Table.CountRows();
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x000389C7 File Offset: 0x00036BC7
		internal static ExpressionNode CountRows(this PlanOperation input)
		{
			return ExprNodes.CountRows(new ExpressionNode[] { ExprNodes.BatchSubQuery(input) });
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x000389DD File Offset: 0x00036BDD
		internal static ExpressionNode IsEmptyTable(this PlanOperationContext input)
		{
			return ExprNodes.IsEmptyTable(ExprNodes.BatchSubQuery(input.Table));
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x000389EF File Offset: 0x00036BEF
		internal static ExpressionNode SingleValue(this PlanOperation input)
		{
			return ExprNodes.BatchSubQuery(input).SingleValue();
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x000389FC File Offset: 0x00036BFC
		internal static ExpressionNode ExtractScalarFromSingleRowTable(this PlanOperation table, string columnPlanName, string declarationName, PlanDeclarationCollection declarations)
		{
			return table.Project(new PlanProjectItem[]
			{
				new PlanNamedColumnProjectItem(columnPlanName)
			}, false).DeclareIfNotDeclared(declarationName, declarations, false, false, null, false).SingleValue();
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x00038A24 File Offset: 0x00036C24
		internal static ExpressionNode DeclareIfNotDeclared(this ExpressionNode input, string name, PlanDeclarationCollection declarations, TranslationErrorContext errorContext, ObjectType objectType, Identifier objectId)
		{
			if (input is BatchScalarDeclarationReferenceExpressionNode)
			{
				return input;
			}
			ExpressionContext expressionContext = new ExpressionContext(errorContext, objectType, objectId, name);
			return declarations.DeclareScalar(name, new PlanExpression(input, expressionContext));
		}
	}
}
