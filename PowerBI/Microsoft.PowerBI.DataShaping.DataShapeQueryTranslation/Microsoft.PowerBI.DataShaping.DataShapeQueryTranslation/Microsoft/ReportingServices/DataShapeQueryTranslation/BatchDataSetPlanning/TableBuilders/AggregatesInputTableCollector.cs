using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001B5 RID: 437
	internal sealed class AggregatesInputTableCollector
	{
		// Token: 0x06000F61 RID: 3937 RVA: 0x0003E510 File Offset: 0x0003C710
		internal AggregatesInputTableCollector(ScopeTree scopeTree, DataShapeAnnotations annotations, BatchSortByMeasureExpressionMappings sortByMeasureExpressionMappings, PlanDeclarationCollection declarations, WritableExpressionTable outputExpressionTable)
		{
			this.m_scopeTree = scopeTree;
			this.m_annotations = annotations;
			this.m_sortByMeasureExpressionMappings = sortByMeasureExpressionMappings;
			this.m_declarations = declarations;
			this.m_outputExpressionTable = outputExpressionTable;
			this.m_inputTablesForAggregates = new List<TableReference>();
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000F62 RID: 3938 RVA: 0x0003E548 File Offset: 0x0003C748
		internal IReadOnlyList<TableReference> References
		{
			get
			{
				return this.m_inputTablesForAggregates;
			}
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x0003E550 File Offset: 0x0003C750
		internal void RegisterHierarchyShowAllNoInstanceFiltersTable(DataShapeContext dsContext, PlanOperationContext showAllTable, bool isPrimary)
		{
			if (!dsContext.HasDataShapeAggregatesAndProjections)
			{
				return;
			}
			this.ValidateAggregatesDoNotRespectInstanceFilters(dsContext);
			string text = (isPrimary ? PlanNames.PrimaryShowAllNoTotals(dsContext.Id) : PlanNames.SecondaryShowAllNoTotals(dsContext.Id));
			this.CreateAggregatesInputTableFromTable(showAllTable, text);
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x0003E594 File Offset: 0x0003C794
		internal void RegisterSubqueryReferenceTable(PlanOperationContext subqueryTableContext, string subqueryDsId, string referringDsId)
		{
			TableReference tableReference = new TableReference(subqueryTableContext, PlanNames.SubqueryTableIn(subqueryDsId, referringDsId), this.m_declarations, RowResultSetType.Unrestricted);
			this.m_inputTablesForAggregates.Add(tableReference);
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x0003E5C4 File Offset: 0x0003C7C4
		internal void RegisterCoreWithInstanceFiltersTable(DataShapeContext dsContext, PlanOperationContext coreWithInstanceFiltersTable)
		{
			bool flag;
			if (!dsContext.HasGroupingStructureAggregates)
			{
				flag = dsContext.DataShapeAggregatesAndProjections.Any((Calculation agg) => agg.RespectInstanceFilters.GetValueOrDefault());
			}
			else
			{
				flag = true;
			}
			bool flag2 = flag;
			if (!dsContext.HasAnyPrimaryDynamic || !flag2)
			{
				return;
			}
			IReadOnlyList<TableReference> readOnlyList = ScopedAggregatesInputTableBuilder.PrepareScopedAggregateInputTables(dsContext, this.m_outputExpressionTable, this.m_declarations, coreWithInstanceFiltersTable, dsContext.PrimaryDynamicsExcludingContextOnly, true);
			this.m_inputTablesForAggregates.AddRange(readOnlyList);
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x0003E63C File Offset: 0x0003C83C
		internal void RegisterCoreNoInstanceFiltersTable(DataShapeContext dsContext, PlanOperationContext coreNoInstanceFiltersTable)
		{
			if (!dsContext.DataShapeAggregatesAndProjections.Any((Calculation agg) => !agg.RespectInstanceFilters.GetValueOrDefault()))
			{
				return;
			}
			this.CreateAggregatesInputTableFromTable(coreNoInstanceFiltersTable, PlanNames.CoreNoInstanceFiltersNoTotals(dsContext.DataShape.Id));
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x0003E690 File Offset: 0x0003C890
		internal void RegisterTransformTable(PlanOperationContext transformTable, string declarationName)
		{
			TableReference tableReference = new TableReference(transformTable, declarationName, this.m_declarations, RowResultSetType.Unrestricted);
			this.m_inputTablesForAggregates.Add(tableReference);
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x0003E6B8 File Offset: 0x0003C8B8
		private void ValidateAggregatesDoNotRespectInstanceFilters(DataShapeContext dsContext)
		{
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x0003E6BC File Offset: 0x0003C8BC
		private void CreateAggregatesInputTableFromTable(PlanOperationContext table, string tableReferenceDeclarationName)
		{
			TableReference tableReference = new TableReference(CoreTableTotalsTransforms.RemoveAllTotalRows(table, BatchDataSetPlanningUtils.CreateCoreTableTransformContext(this.m_scopeTree, this.m_annotations, this.m_sortByMeasureExpressionMappings, this.m_outputExpressionTable)), tableReferenceDeclarationName, this.m_declarations, RowResultSetType.Unrestricted);
			this.m_inputTablesForAggregates.Insert(0, tableReference);
		}

		// Token: 0x04000738 RID: 1848
		private readonly ScopeTree m_scopeTree;

		// Token: 0x04000739 RID: 1849
		private readonly DataShapeAnnotations m_annotations;

		// Token: 0x0400073A RID: 1850
		private readonly BatchSortByMeasureExpressionMappings m_sortByMeasureExpressionMappings;

		// Token: 0x0400073B RID: 1851
		private readonly PlanDeclarationCollection m_declarations;

		// Token: 0x0400073C RID: 1852
		private readonly WritableExpressionTable m_outputExpressionTable;

		// Token: 0x0400073D RID: 1853
		private List<TableReference> m_inputTablesForAggregates;
	}
}
