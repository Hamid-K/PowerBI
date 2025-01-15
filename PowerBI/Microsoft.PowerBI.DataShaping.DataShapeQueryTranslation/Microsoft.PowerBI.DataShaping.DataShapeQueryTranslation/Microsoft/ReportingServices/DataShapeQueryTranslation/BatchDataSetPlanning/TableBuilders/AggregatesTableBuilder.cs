using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001B6 RID: 438
	internal sealed class AggregatesTableBuilder
	{
		// Token: 0x06000F6A RID: 3946 RVA: 0x0003E708 File Offset: 0x0003C908
		internal static PlanOperationContext CreateAggregatesTable(IAggregatesPlanningContext plannerContext, DataShapeContext dsContext, PlanDeclarationCollection declarations, IReadOnlyList<TableReference> referenceTables, IReadOnlyList<PlanOperation> contextTables, WritableExpressionTable outputExpressionTable, RowScopesMetadata joinPredicatesRowScopes, IReadOnlyList<Calculation> aggregateCalculations, IScope containingScope, DataShapeQueryTranslationTelemetry telemetry)
		{
			AggregatesTableJoiner aggregatesTableJoiner = new AggregatesTableJoiner(dsContext, declarations, joinPredicatesRowScopes, aggregateCalculations, containingScope);
			return new AggregatesTableGroupByStrategy(plannerContext, dsContext, declarations, referenceTables, contextTables, outputExpressionTable, joinPredicatesRowScopes, aggregateCalculations, containingScope, aggregatesTableJoiner).ToTableContext();
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x0003E73C File Offset: 0x0003C93C
		internal static PlanOperationContext JoinAggregatesTable(DataShapeContext dsContext, PlanDeclarationCollection declarations, RowScopesMetadata joinPredicatesRowScopes, IReadOnlyList<Calculation> aggregateCalculations, IScope containingScope, PlanOperationContext firstPlanOperationContext, PlanOperationContext secondPlanOperationContext, IReadOnlyList<Calculation> calculationsToProject)
		{
			if (firstPlanOperationContext == null)
			{
				return secondPlanOperationContext;
			}
			if (secondPlanOperationContext == null)
			{
				return firstPlanOperationContext;
			}
			List<PlanOperation> list = new List<PlanOperation> { firstPlanOperationContext.Table, secondPlanOperationContext.Table };
			return new AggregatesTableJoiner(dsContext, declarations, joinPredicatesRowScopes, aggregateCalculations, containingScope).JoinAggregateTables(list, calculationsToProject);
		}
	}
}
