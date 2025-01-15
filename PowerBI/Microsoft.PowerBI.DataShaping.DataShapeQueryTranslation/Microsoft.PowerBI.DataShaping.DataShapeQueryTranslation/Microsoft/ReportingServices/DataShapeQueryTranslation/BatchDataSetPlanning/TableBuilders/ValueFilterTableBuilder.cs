using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Common;
using Microsoft.ReportingServices.DataShapeQueryTranslation.CommonDataSetPlanning;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001E5 RID: 485
	internal sealed class ValueFilterTableBuilder
	{
		// Token: 0x060010BE RID: 4286 RVA: 0x00045DC8 File Offset: 0x00043FC8
		private ValueFilterTableBuilder(IValueFilterPlanningContext context, Filter valueFilter)
		{
			this.m_context = context;
			this.m_valueFilter = valueFilter;
			this.m_valueFilterTargetScope = valueFilter.Target.GetResolvedScope(this.m_context.OutputExpressionTable);
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x00045DFA File Offset: 0x00043FFA
		internal static PlanOperation BuildValueFilterTable(IValueFilterPlanningContext context, Filter valueFilter, IEnumerable<PlanOperation> contextTables, IEnumerable<Calculation> measureCalcs, IEnumerable<PlanDataTransformColumnMeasure> measureColumns, IEnumerable<PlanGroupAndJoinAdditionalColumn> additionalColumns, PlanDeclarationCollection declarations, IScope innermostScope, bool omitAllConstraints)
		{
			return new ValueFilterTableBuilder(context, valueFilter).ToValueFilterTable(contextTables, measureCalcs, measureColumns, additionalColumns, declarations, innermostScope, omitAllConstraints);
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x00045E14 File Offset: 0x00044014
		private PlanOperation ToValueFilterTable(IEnumerable<PlanOperation> contextTables, IEnumerable<Calculation> measureCalcs, IEnumerable<PlanDataTransformColumnMeasure> measureColumns, IEnumerable<PlanGroupAndJoinAdditionalColumn> additionalColumns, PlanDeclarationCollection declarations, IScope innermostScope, bool omitAllConstraints)
		{
			BatchGroupAndJoinBuilder batchGroupAndJoinBuilder;
			if (omitAllConstraints)
			{
				batchGroupAndJoinBuilder = this.BuildValueFilterCore(contextTables, PlanGroupAndJoinPredicateBehavior.SuppressAutoPredicates);
			}
			else
			{
				batchGroupAndJoinBuilder = this.BuildConstrainedValueFilterTable(contextTables, measureCalcs, measureColumns, additionalColumns, declarations, innermostScope);
			}
			BatchGroupAndJoinBuilder batchGroupAndJoinBuilder2 = batchGroupAndJoinBuilder;
			ICommonPlanningContext context = this.m_context;
			NamingContext namingContext = batchGroupAndJoinBuilder.NamingContext;
			Filter valueFilter = this.m_valueFilter;
			Func<string, Expression, ExpressionContext, PlanGroupAndJoinAdditionalColumn> func;
			if ((func = ValueFilterTableBuilder.<>O.<0>__CreateValueFilterGroupAndJoinColumn) == null)
			{
				func = (ValueFilterTableBuilder.<>O.<0>__CreateValueFilterGroupAndJoinColumn = new Func<string, Expression, ExpressionContext, PlanGroupAndJoinAdditionalColumn>(ValueFilterTableBuilder.CreateValueFilterGroupAndJoinColumn));
			}
			batchGroupAndJoinBuilder2.AddAdditionalColumns(ValueFilterTableBuilder.CreateAdditionalColumns<PlanGroupAndJoinAdditionalColumn>(context, namingContext, valueFilter, func));
			return batchGroupAndJoinBuilder.ToPlanOperation(null).FilterBy(this.m_valueFilter.Condition);
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x00045E90 File Offset: 0x00044090
		private BatchGroupAndJoinBuilder BuildConstrainedValueFilterTable(IEnumerable<PlanOperation> contextTables, IEnumerable<Calculation> measureCalcs, IEnumerable<PlanDataTransformColumnMeasure> measureColumns, IEnumerable<PlanGroupAndJoinAdditionalColumn> additionalColumns, PlanDeclarationCollection declarations, IScope innermostScope)
		{
			contextTables = contextTables.Evaluate<PlanOperation>();
			measureCalcs = measureCalcs.Evaluate<Calculation>();
			measureColumns = measureColumns.Evaluate<PlanDataTransformColumnMeasure>();
			additionalColumns = additionalColumns.Evaluate<PlanGroupAndJoinAdditionalColumn>();
			IEnumerable<Expression> enumerable = additionalColumns.Select((PlanGroupAndJoinAdditionalColumn column) => column.Expression);
			bool flag = BatchDataSetPlanningUtils.HasModelMeasureInCalculation(this.m_context.OutputExpressionTable, this.m_context.CalculationMap, measureCalcs) || BatchDataSetPlanningUtils.HasModelMeasure(measureColumns, this.m_context.OutputExpressionTable) || BatchDataSetPlanningUtils.HasModelMeasure(enumerable, this.m_context.OutputExpressionTable);
			BatchGroupAndJoinBuilder batchGroupAndJoinBuilder;
			if (BatchDataSetPlanningUtils.AreEquivalentScopes(innermostScope, this.m_valueFilterTargetScope, this.m_context.ScopeTree) || !flag)
			{
				batchGroupAndJoinBuilder = this.BuildValueFilterCore(contextTables, PlanGroupAndJoinPredicateBehavior.ApplyAutoPredicates);
				batchGroupAndJoinBuilder.AddCalculations(measureCalcs);
				batchGroupAndJoinBuilder.AddMeasureTransformColumns(measureColumns);
				batchGroupAndJoinBuilder.AddAdditionalColumns(additionalColumns);
			}
			else
			{
				batchGroupAndJoinBuilder = this.BuildValueFilterCore(contextTables, PlanGroupAndJoinPredicateBehavior.SuppressAutoPredicates);
				PlanOperation planOperation = this.BuildValueFilterConstraintTable(innermostScope, measureCalcs, measureColumns, contextTables, additionalColumns);
				planOperation = planOperation.DeclareIfNotDeclared(PlanNames.ValueFilterConstraint(this.m_valueFilterTargetScope.Id), declarations, false, false, null, false);
				batchGroupAndJoinBuilder.AddContextTable(planOperation);
			}
			return batchGroupAndJoinBuilder;
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x00045FAC File Offset: 0x000441AC
		private BatchGroupAndJoinBuilder BuildValueFilterCore(IEnumerable<PlanOperation> contextTables, PlanGroupAndJoinPredicateBehavior predicateBehavior)
		{
			BatchGroupAndJoinBuilder batchGroupAndJoinBuilder = new BatchGroupAndJoinBuilder(true, true);
			this.AddGroups(this.m_valueFilterTargetScope, batchGroupAndJoinBuilder);
			if (contextTables != null)
			{
				batchGroupAndJoinBuilder.AddContextTables(contextTables);
			}
			DataShape containingDataShapeOrSelf = this.m_context.ScopeTree.GetContainingDataShapeOrSelf(this.m_valueFilterTargetScope);
			DataShapeAnnotation dataShapeAnnotation = this.m_context.Annotations.GetDataShapeAnnotation(containingDataShapeOrSelf);
			batchGroupAndJoinBuilder.AddExistsFilters(dataShapeAnnotation.ExistsFilter);
			batchGroupAndJoinBuilder.PredicateBehavior = predicateBehavior;
			return batchGroupAndJoinBuilder;
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x00046018 File Offset: 0x00044218
		private PlanOperation BuildValueFilterConstraintTable(IScope innermostScope, IEnumerable<Calculation> measureCalcs, IEnumerable<PlanDataTransformColumnMeasure> measureColumns, IEnumerable<PlanOperation> contextTables, IEnumerable<PlanGroupAndJoinAdditionalColumn> additionalColumns)
		{
			BatchGroupAndJoinBuilder batchGroupAndJoinBuilder = new BatchGroupAndJoinBuilder(true, true);
			this.AddGroups(innermostScope, batchGroupAndJoinBuilder);
			if (contextTables != null)
			{
				batchGroupAndJoinBuilder.AddContextTables(contextTables);
			}
			DataShape containingDataShapeOrSelf = this.m_context.ScopeTree.GetContainingDataShapeOrSelf(this.m_valueFilterTargetScope);
			DataShapeAnnotation dataShapeAnnotation = this.m_context.Annotations.GetDataShapeAnnotation(containingDataShapeOrSelf);
			batchGroupAndJoinBuilder.AddExistsFilters(dataShapeAnnotation.ExistsFilter);
			batchGroupAndJoinBuilder.AddCalculations(measureCalcs);
			batchGroupAndJoinBuilder.AddMeasureTransformColumns(measureColumns);
			batchGroupAndJoinBuilder.AddAdditionalColumns(additionalColumns);
			batchGroupAndJoinBuilder.PredicateBehavior = PlanGroupAndJoinPredicateBehavior.ApplyAutoPredicates;
			return batchGroupAndJoinBuilder.ToPlanOperation(null);
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x0004609C File Offset: 0x0004429C
		private void AddGroups(IScope startScope, BatchGroupAndJoinBuilder groupAndJoinBuilder)
		{
			foreach (IScope scope in this.m_context.ScopeTree.GetAllParentScopes(startScope))
			{
				DataMember dataMember = scope as DataMember;
				if (dataMember != null)
				{
					this.AddGroup(groupAndJoinBuilder, dataMember);
				}
				DataShape dataShape = scope as DataShape;
				if (dataShape != null)
				{
					this.AddTransformGroupingColumns(groupAndJoinBuilder, dataShape);
				}
			}
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x00046110 File Offset: 0x00044310
		private void AddGroup(BatchGroupAndJoinBuilder groupAndJoinBuilder, DataMember dataMember)
		{
			if (this.m_context.TransformReferenceMap.HasDataTransformColumnReference(dataMember))
			{
				return;
			}
			PlanGroupByMember planGroupByMember = dataMember.ToGroupByItem(this.m_context.Annotations, SubtotalUsage.None, false, true, null);
			groupAndJoinBuilder.AddToPrimaryGroupingBucket(planGroupByMember);
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x00046150 File Offset: 0x00044350
		private void AddTransformGroupingColumns(BatchGroupAndJoinBuilder groupAndJoinBuilder, DataShape dataShape)
		{
			if (!this.m_context.ApplyTransformsInQuery)
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
				if (!MeasureAnalyzer.IsMeasure(this.m_context.OutputExpressionTable.GetNode(dataTransformTableColumn.Value)))
				{
					groupAndJoinBuilder.AddGroupingTransformColumn(new PlanGroupByDataTransformColumn(dataTransformTableColumn));
				}
			}
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x000461E0 File Offset: 0x000443E0
		internal static List<T> CreateAdditionalColumns<T>(ICommonPlanningContext context, NamingContext namingContext, Filter valueFilter, Func<string, Expression, ExpressionContext, T> createColumn)
		{
			if (valueFilter == null)
			{
				return null;
			}
			List<FilterExpressionInfo> list = FilterExpressionCollector.CollectMeasureExpressions(valueFilter, context.OutputExpressionTable, context.ErrorContext);
			List<T> list2 = new List<T>(list.Count);
			foreach (FilterExpressionInfo filterExpressionInfo in list)
			{
				string text = namingContext.GenerateUniqueName(filterExpressionInfo.Context.PropertyName);
				list2.Add(createColumn(text, filterExpressionInfo.Expression, filterExpressionInfo.Context));
			}
			return list2;
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x00046278 File Offset: 0x00044478
		internal static PlanGroupAndJoinAdditionalColumn CreateValueFilterGroupAndJoinColumn(string planName, Expression expr, ExpressionContext exprContext)
		{
			return new PlanGroupAndJoinAdditionalColumn(planName, expr, true, exprContext);
		}

		// Token: 0x040007DB RID: 2011
		private readonly IValueFilterPlanningContext m_context;

		// Token: 0x040007DC RID: 2012
		private readonly Filter m_valueFilter;

		// Token: 0x040007DD RID: 2013
		private readonly IScope m_valueFilterTargetScope;

		// Token: 0x0200031A RID: 794
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000B5B RID: 2907
			public static Func<string, Expression, ExpressionContext, PlanGroupAndJoinAdditionalColumn> <0>__CreateValueFilterGroupAndJoinColumn;
		}
	}
}
