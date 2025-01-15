using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableManagers;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Common;
using Microsoft.ReportingServices.DataShapeQueryTranslation.CommonDataSetPlanning;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001DD RID: 477
	internal sealed class ScopedTablesCollector : DataShapeVisitor
	{
		// Token: 0x0600108A RID: 4234 RVA: 0x000447EC File Offset: 0x000429EC
		private ScopedTablesCollector(BatchDataSetPlannerContext plannerContext, DataShapeContext dsContext, ScopedTablesBuilder scopedTablesBuilder, ContextTableManager attributeFilterContextTableManager, IFilterDeclarationCollection instanceFilterDeclarations, bool shouldAddInstanceFilters)
		{
			this.m_plannerContext = plannerContext;
			this.m_dsContext = dsContext;
			this.m_scopedTablesBuilder = scopedTablesBuilder;
			this.m_parentDataShapes = new Stack<Identifier>();
			this.m_attributeFilterContextTableManager = attributeFilterContextTableManager;
			this.m_instanceFilterDeclarations = instanceFilterDeclarations;
			this.m_shouldAddInstanceFilters = shouldAddInstanceFilters;
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x0004482C File Offset: 0x00042A2C
		internal static void Collect(BatchDataSetPlannerContext plannerContext, DataShapeContext dsContext, ScopedTablesBuilder scopedTablesBuilder, ContextTableManager attributeFilterContextTableManager, IFilterDeclarationCollection instanceFilterDeclarations, bool shouldAddInstanceFilters, out IScope innermostScope)
		{
			ScopedTablesCollector scopedTablesCollector = new ScopedTablesCollector(plannerContext, dsContext, scopedTablesBuilder, attributeFilterContextTableManager, instanceFilterDeclarations, shouldAddInstanceFilters);
			scopedTablesCollector.m_innermostScope = dsContext.InnermostScope;
			scopedTablesCollector.Visit(dsContext.DataShape);
			innermostScope = scopedTablesCollector.m_innermostScope;
			BatchGroupAndJoinBuilder innermostScopedTable = scopedTablesBuilder.InnermostScopedTable;
			if (innermostScopedTable.InnermostScope == null)
			{
				innermostScopedTable.InnermostScope = scopedTablesCollector.m_innermostScope;
			}
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x00044884 File Offset: 0x00042A84
		protected override void Visit(DataShape dataShape)
		{
			IScope parentScope = this.m_plannerContext.ScopeTree.GetParentScope(dataShape);
			if (parentScope != null && ((this.m_parentDataShapes.Count > 0 && parentScope.ObjectType == ObjectType.DataShape) || this.m_parentDataShapes.Count > 1))
			{
				return;
			}
			this.AddCalculations(dataShape, this.m_plannerContext.ScopeTree.GetItems<Calculation>(dataShape.Id));
			this.AddContextTables(dataShape);
			this.m_parentDataShapes.Push(dataShape.Id);
			this.AddTransformContent(dataShape);
			IScope innermostScopeInDataShape = this.m_plannerContext.ScopeTree.GetInnermostScopeInDataShape(dataShape);
			if (this.m_plannerContext.ScopeTree.IsParentScope(this.m_innermostScope, innermostScopeInDataShape))
			{
				this.m_innermostScope = innermostScopeInDataShape;
			}
			base.Visit(dataShape);
			this.m_parentDataShapes.Pop();
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x00044958 File Offset: 0x00042B58
		private void AddTransformContent(DataShape dataShape)
		{
			if (!this.m_plannerContext.ApplyTransformsInQuery)
			{
				return;
			}
			DataTransformTable dataTransformInputTable = CommonDataSetPlanningUtils.GetDataTransformInputTable(dataShape);
			if (dataTransformInputTable == null)
			{
				return;
			}
			foreach (DataTransformTableColumn dataTransformTableColumn in dataTransformInputTable.Columns)
			{
				if (MeasureAnalyzer.IsMeasure(this.m_plannerContext.OutputExpressionTable.GetNode(dataTransformTableColumn.Value)))
				{
					PlanDataTransformColumnMeasure planDataTransformColumnMeasure = new PlanDataTransformColumnMeasure(dataTransformInputTable, dataTransformTableColumn);
					this.m_scopedTablesBuilder.AddMeasureTransformColumn(planDataTransformColumnMeasure);
				}
				else
				{
					this.m_scopedTablesBuilder.AddGroupingTransformColumn(new PlanGroupByDataTransformColumn(dataTransformTableColumn));
				}
			}
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x00044A04 File Offset: 0x00042C04
		protected override void Enter(DataMember dataMember)
		{
			if (dataMember.IsDynamic)
			{
				if (this.m_plannerContext.TransformReferenceMap.HasDataTransformColumnReference(dataMember))
				{
					return;
				}
				IReadOnlyList<PlanOperation> instanceFilterTables = this.GetInstanceFilterTables(dataMember);
				PlanGroupByMember planGroupByMember = dataMember.ToGroupByItem(this.m_plannerContext.Annotations, SubtotalUsage.VisualCalculations, true, false, instanceFilterTables);
				bool flag = this.m_plannerContext.Annotations.IsPrimaryMember(dataMember);
				if (!this.m_dsContext.HasTotals || flag)
				{
					this.m_scopedTablesBuilder.AddToPrimaryGroupingBucket(planGroupByMember);
				}
				else
				{
					this.m_scopedTablesBuilder.AddToSecondaryGroupingBucket(planGroupByMember);
				}
				ReadOnlyCollection<Calculation> items = this.m_plannerContext.ScopeTree.GetItems<Calculation>(dataMember.Id);
				this.AddCalculations(dataMember, items);
				this.m_plannerContext.DefaultValueContextManager.AddGrouping(dataMember);
			}
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x00044ABC File Offset: 0x00042CBC
		protected override void Enter(DataIntersection dataIntersection)
		{
			if (this.m_plannerContext.ScopeTree.HasScope(dataIntersection.Id))
			{
				this.AddCalculations(dataIntersection, dataIntersection.Calculations);
			}
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x00044AE4 File Offset: 0x00042CE4
		private void AddCalculations(IScope currentInnermostScope, IList<Calculation> calculations)
		{
			if (calculations == null || calculations.Count == 0)
			{
				return;
			}
			foreach (Calculation calculation in calculations)
			{
				if (calculation.ShouldIncludeCalculationInBaseQuery(this.m_dsContext, this.m_plannerContext.Annotations, this.m_plannerContext.TransformReferenceMap) && !this.m_dsContext.Annotations.IsAggregate(calculation))
				{
					this.m_scopedTablesBuilder.AddCalculation(currentInnermostScope, calculation, this.m_dsContext, this.m_plannerContext, this.IsJoinPredicate(calculation, this.m_plannerContext.Annotations));
				}
			}
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x00044B94 File Offset: 0x00042D94
		private void AddContextTables(DataShape dataShape)
		{
			ReadOnlyCollection<PlanOperation> contextTables = this.m_attributeFilterContextTableManager.GetContextTables(dataShape);
			this.m_scopedTablesBuilder.AddContextTables(contextTables);
			IList<FilterCondition> filterConditions = this.m_attributeFilterContextTableManager.GetFilterConditions(dataShape);
			this.m_scopedTablesBuilder.AddAttributeFilters(filterConditions);
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x00044BD4 File Offset: 0x00042DD4
		private IReadOnlyList<PlanOperation> GetInstanceFilterTables(DataMember dataMember)
		{
			if (!this.m_shouldAddInstanceFilters)
			{
				return null;
			}
			IReadOnlyList<PlanOperation> instanceFilterTables = BatchDataSetPlanningFilterUtils.GetInstanceFilterTables(dataMember, this.m_instanceFilterDeclarations);
			if (instanceFilterTables.IsNullOrEmpty<PlanOperation>() || dataMember.HasOutputTotal(this.m_plannerContext.Annotations))
			{
				return instanceFilterTables;
			}
			Contract.RetailFail("NonVisual context tables on groups without totals are not supported.");
			return null;
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x00044C20 File Offset: 0x00042E20
		private bool IsJoinPredicate(Calculation calc, DataShapeAnnotations annotations)
		{
			return annotations.NeededForQueryCalculationContext(calc);
		}

		// Token: 0x040007C0 RID: 1984
		private readonly BatchDataSetPlannerContext m_plannerContext;

		// Token: 0x040007C1 RID: 1985
		private readonly DataShapeContext m_dsContext;

		// Token: 0x040007C2 RID: 1986
		private readonly ScopedTablesBuilder m_scopedTablesBuilder;

		// Token: 0x040007C3 RID: 1987
		private readonly Stack<Identifier> m_parentDataShapes;

		// Token: 0x040007C4 RID: 1988
		private readonly ContextTableManager m_attributeFilterContextTableManager;

		// Token: 0x040007C5 RID: 1989
		private readonly IFilterDeclarationCollection m_instanceFilterDeclarations;

		// Token: 0x040007C6 RID: 1990
		private readonly bool m_shouldAddInstanceFilters;

		// Token: 0x040007C7 RID: 1991
		private IScope m_innermostScope;
	}
}
