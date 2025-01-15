using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableManagers;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x02000182 RID: 386
	internal sealed class ContextTableCollector : DataShapeVisitor
	{
		// Token: 0x06000D8D RID: 3469 RVA: 0x00037920 File Offset: 0x00035B20
		private ContextTableCollector(BatchDataSetPlannerContext plannerContext, PlanDeclarationCollection declarations, DataShapeContext rootDataShapeContext)
		{
			this.m_plannerContext = plannerContext;
			this.m_declarations = declarations;
			this.m_rootDataShapeContext = rootDataShapeContext;
			this.m_contextTableManager = new WritableContextTableManager();
			this.m_parentDataShapes = new List<DataShape>();
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x00037953 File Offset: 0x00035B53
		internal static ContextTableManager Analyze(BatchDataSetPlannerContext plannerContext, PlanDeclarationCollection declarations, DataShapeContext dataShapeContext)
		{
			ContextTableCollector contextTableCollector = new ContextTableCollector(plannerContext, declarations, dataShapeContext);
			contextTableCollector.Visit(dataShapeContext.DataShape);
			return contextTableCollector.m_contextTableManager;
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x00037970 File Offset: 0x00035B70
		protected override void Enter(DataShape dataShape)
		{
			if (this.m_plannerContext.Annotations.HasComplexSlicer(dataShape))
			{
				return;
			}
			this.CreateAndRegisterContextTables(dataShape);
			this.AnalyzeApplyFilters(dataShape);
			if (!dataShape.IsIndependent)
			{
				foreach (DataShape dataShape2 in this.m_parentDataShapes.Where((DataShape ds) => this.m_plannerContext.ScopeTree.IsParentScope(ds, dataShape)))
				{
					IList<PlanOperation> list;
					IList<FilterCondition> list2;
					if (this.m_contextTableManager.TryGetContextTables(dataShape2, out list) && this.m_contextTableManager.TryGetFilterConditions(dataShape2, out list2))
					{
						this.m_contextTableManager.RegisterContextTables(dataShape, list, list2);
					}
				}
			}
			this.m_parentDataShapes.Add(dataShape);
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x00037A60 File Offset: 0x00035C60
		protected override void Exit(DataShape dataShape)
		{
			int num = this.m_parentDataShapes.Count - 1;
			if (num >= 0 && this.m_parentDataShapes[num] == dataShape)
			{
				this.m_parentDataShapes.RemoveAt(this.m_parentDataShapes.Count - 1);
			}
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x00037AA6 File Offset: 0x00035CA6
		protected override void Visit(Filter filter, Identifier dataShapeId)
		{
			FilterDataShapeVisitor.Visit(filter, new VisitDataShapeDelegate(this.VisitFilterConditionDataShape));
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x00037ABA File Offset: 0x00035CBA
		private void VisitFilterConditionDataShape(DataShape dataShape, ObjectType filterConditionType)
		{
			if (filterConditionType == ObjectType.ApplyFilterCondition)
			{
				return;
			}
			this.Visit(dataShape);
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x00037AC8 File Offset: 0x00035CC8
		private void AnalyzeApplyFilters(DataShape dataShape)
		{
			IDataShapeDefaultValueContextManager defaultValueContextManager = this.m_plannerContext.DefaultValueContextManager;
			IEnumerable<ApplyFilterCondition> applyFilterDataShapes = ContextTableCollector.GetApplyFilterDataShapes(this.m_plannerContext.Annotations, dataShape);
			List<PlanOperation> list = null;
			foreach (ApplyFilterCondition applyFilterCondition in applyFilterDataShapes)
			{
				DataShape applyFilterDataShape = applyFilterCondition.GetApplyFilterDataShape(this.m_plannerContext.OutputExpressionTable);
				DataShapeContext nestedDataShapeContext = this.m_rootDataShapeContext.GetNestedDataShapeContext(applyFilterDataShape);
				this.Visit(applyFilterDataShape);
				PlanOperationTreeGenerator planOperationTreeGenerator = new PlanOperationTreeGenerator(this.m_plannerContext, this.m_plannerContext.OutputExpressionTable, this.m_declarations, this.m_contextTableManager);
				PlanOperation planOperation = new SubqueryPlanOperationGenerator(this.m_plannerContext, this.m_declarations, planOperationTreeGenerator).GetOrCreateSubqueryTable(nestedDataShapeContext, true).Table;
				planOperation = planOperation.DeclareIfNotDeclared(PlanNames.ApplyFilter(nestedDataShapeContext.DataShape.Id), this.m_declarations, false, false, null, false);
				Util.AddToLazyList<PlanOperation>(ref list, planOperation);
				this.m_contextTableManager.RegisterContextTable(dataShape, planOperation, applyFilterCondition);
				defaultValueContextManager.AddFilter(dataShape, applyFilterCondition);
			}
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x00037BE4 File Offset: 0x00035DE4
		private static IReadOnlyList<ApplyFilterCondition> GetApplyFilterDataShapes(DataShapeAnnotations annotations, DataShape dataShape)
		{
			return annotations.GetDataShapeAnnotation(dataShape).ApplyFilters;
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x00037BF4 File Offset: 0x00035DF4
		private void CreateAndRegisterContextTables(DataShape dataShape)
		{
			IDataShapeDefaultValueContextManager defaultValueContextManager = this.m_plannerContext.DefaultValueContextManager;
			FilterCondition filterCondition;
			PlanOperation planOperation = SimpleSlicerTableBuilder.BuildSlicerTableDeclaration(dataShape, this.m_plannerContext.Annotations, this.m_declarations, out filterCondition);
			if (planOperation != null)
			{
				defaultValueContextManager.AddFilter(dataShape, filterCondition);
				this.m_contextTableManager.RegisterContextTable(dataShape, planOperation, filterCondition);
			}
			defaultValueContextManager.AddDefaultValueFilters(dataShape);
		}

		// Token: 0x040006A2 RID: 1698
		private readonly BatchDataSetPlannerContext m_plannerContext;

		// Token: 0x040006A3 RID: 1699
		private readonly WritableContextTableManager m_contextTableManager;

		// Token: 0x040006A4 RID: 1700
		private readonly PlanDeclarationCollection m_declarations;

		// Token: 0x040006A5 RID: 1701
		private readonly List<DataShape> m_parentDataShapes;

		// Token: 0x040006A6 RID: 1702
		private readonly DataShapeContext m_rootDataShapeContext;
	}
}
