using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001BA RID: 442
	internal sealed class AggregatesTableJoiner
	{
		// Token: 0x06000F7E RID: 3966 RVA: 0x0003EF08 File Offset: 0x0003D108
		internal AggregatesTableJoiner(DataShapeContext dsContext, PlanDeclarationCollection declarations, RowScopesMetadata joinPredicatesRowScopes, IReadOnlyList<Calculation> aggregateCalculations, IScope containingScope)
		{
			this.m_declarations = declarations;
			this.m_joinPredicatesRowScopes = joinPredicatesRowScopes;
			this.m_aggregateCalculations = aggregateCalculations;
			this.m_containingScope = containingScope;
			this.m_aggregateScopeIsDataShape = this.m_containingScope == dsContext.DataShape;
			this.dataShapeIdentifier = dsContext.DataShape.Id;
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x0003EF60 File Offset: 0x0003D160
		internal PlanOperationContext JoinAggregateTables(IReadOnlyList<PlanOperation> planOps, IReadOnlyList<Calculation> calculationsToProject)
		{
			PlanOperation orCreateTopLevelTable = this.GetOrCreateTopLevelTable(planOps);
			return new PlanOperationContext(this.ProjectCalculations(orCreateTopLevelTable, calculationsToProject), this.m_joinPredicatesRowScopes, this.m_aggregateCalculations);
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x0003EF90 File Offset: 0x0003D190
		private PlanOperation GetOrCreateTopLevelTable(IReadOnlyList<PlanOperation> tables)
		{
			if (tables.Count == 1)
			{
				return tables[0];
			}
			if (!this.m_aggregateScopeIsDataShape)
			{
				PlanOperation planOperation = new PlanOperationLeftOuterJoin(tables[0], tables[1]);
				for (int i = 2; i < tables.Count; i++)
				{
					if (tables[i] != null)
					{
						planOperation = new PlanOperationLeftOuterJoin(planOperation, tables[i]);
					}
				}
				return planOperation;
			}
			List<PlanOperation> list = new List<PlanOperation>(tables.Count);
			for (int j = 0; j < tables.Count; j++)
			{
				if (tables[j] != null)
				{
					PlanOperationDeclarationReference planOperationDeclarationReference = tables[j].DeclareIfNotDeclared(PlanNames.FullOuterCrossJoinTable(this.dataShapeIdentifier, j), this.m_declarations, false, false, "AggregatesCrossJoinTables", false);
					list.Add(planOperationDeclarationReference);
				}
			}
			return new PlanOperationFullOuterCrossJoin(list);
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x0003F050 File Offset: 0x0003D250
		private PlanOperation ProjectCalculations(PlanOperation topLevelTable, IReadOnlyList<Calculation> aggregateCalcs)
		{
			List<PlanProjectItem> list = new List<PlanProjectItem>(aggregateCalcs.Count);
			foreach (Calculation calculation in aggregateCalcs)
			{
				list.Add(calculation.ToNewColumnProjectItem());
			}
			list.Add(PlanPreserveAllColumnsProjectItem.Instance);
			return topLevelTable.Project(list, false);
		}

		// Token: 0x0400074B RID: 1867
		private readonly PlanDeclarationCollection m_declarations;

		// Token: 0x0400074C RID: 1868
		private readonly RowScopesMetadata m_joinPredicatesRowScopes;

		// Token: 0x0400074D RID: 1869
		private readonly IScope m_containingScope;

		// Token: 0x0400074E RID: 1870
		private readonly IReadOnlyList<Calculation> m_aggregateCalculations;

		// Token: 0x0400074F RID: 1871
		private readonly bool m_aggregateScopeIsDataShape;

		// Token: 0x04000750 RID: 1872
		private Identifier dataShapeIdentifier;
	}
}
