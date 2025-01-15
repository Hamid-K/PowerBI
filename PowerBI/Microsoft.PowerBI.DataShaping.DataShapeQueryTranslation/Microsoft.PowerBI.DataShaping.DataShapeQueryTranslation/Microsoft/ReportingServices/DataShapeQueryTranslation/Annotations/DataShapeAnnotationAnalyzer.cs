using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.InternalContracts.Model;
using Microsoft.InfoNav;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations
{
	// Token: 0x02000249 RID: 585
	internal sealed class DataShapeAnnotationAnalyzer : DataShapeVisitor
	{
		// Token: 0x060013EE RID: 5102 RVA: 0x0004D444 File Offset: 0x0004B644
		private DataShapeAnnotationAnalyzer(ExpressionTable expressionTable, IFederatedConceptualSchema schema, ScopeTree scopeTree, TranslationErrorContext errorContext, DataMemberAnnotations dataMemberAnnotations, BatchSubtotalAnnotations subtotalAnnotations)
		{
			this.m_expressionTable = expressionTable;
			this.m_schema = schema;
			this.m_scopeTree = scopeTree;
			this.m_errorContext = errorContext;
			this.m_dataMemberAnnotations = dataMemberAnnotations;
			this.m_parents = new Stack<IIdentifiable>();
			this.m_containingDataShapes = new Dictionary<IContextItem, DataShape>();
			this.m_dataShapesByFilter = new Dictionary<Filter, DataShape>();
			this.m_filtersByTarget = new Dictionary<IIdentifiable, List<Filter>>();
			this.m_dataShapeAnnotations = new Dictionary<DataShape, WriteableDataShapeAnnotation>();
			this.m_calculationAnnotations = new Dictionary<Calculation, CalculationAnnotation>();
			this.m_dataIntersectionAnnotations = new Dictionary<DataIntersection, DataIntersectionAnnotation>();
			this.m_batchSubtotalSortAnnotationAnalyzer = new BatchSubtotalSortAnnotationAnalyzer(scopeTree, dataMemberAnnotations, subtotalAnnotations);
			this.m_currentDataShapeSharedAnnotation = new WriteableDataShapeSharedAnnotation();
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x0004D4F4 File Offset: 0x0004B6F4
		public static DataShapeAnnotations Analyze(DataShape dataShape, IFederatedConceptualSchema schema, DataMemberAnnotations dataMemberAnnotations, ScopeTree scopeTree, ExpressionTable expressionTable, TranslationErrorContext errorContext, BatchSubtotalAnnotations subtotalAnnotations)
		{
			DataShapeAnnotationAnalyzer dataShapeAnnotationAnalyzer = new DataShapeAnnotationAnalyzer(expressionTable, schema, scopeTree, errorContext, dataMemberAnnotations, subtotalAnnotations);
			dataShapeAnnotationAnalyzer.Visit(dataShape);
			Dictionary<WriteableDataShapeSharedAnnotation, DataShapeSharedAnnotation> dictionary = new Dictionary<WriteableDataShapeSharedAnnotation, DataShapeSharedAnnotation>();
			Dictionary<DataShape, DataShapeAnnotation> dictionary2 = new Dictionary<DataShape, DataShapeAnnotation>();
			foreach (KeyValuePair<DataShape, WriteableDataShapeAnnotation> keyValuePair in dataShapeAnnotationAnalyzer.m_dataShapeAnnotations)
			{
				dictionary2.Add(keyValuePair.Key, keyValuePair.Value.ToReadOnly(dictionary));
			}
			return new DataShapeAnnotations(dataShapeAnnotationAnalyzer.m_containingDataShapes, dataShapeAnnotationAnalyzer.m_dataShapesByFilter, dataShapeAnnotationAnalyzer.m_filtersByTarget, dictionary2, dataShapeAnnotationAnalyzer.m_calculationAnnotations, dataMemberAnnotations, dataShapeAnnotationAnalyzer.m_dataIntersectionAnnotations, dataShapeAnnotationAnalyzer.m_batchSubtotalSortAnnotationAnalyzer.SubtotalAnnotations, dataShapeAnnotationAnalyzer.m_batchSubtotalSortAnnotationAnalyzer.SubtotalSortAnnotations, dataShapeAnnotationAnalyzer.m_hasOrContainsTransforms);
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x060013F0 RID: 5104 RVA: 0x0004D5C0 File Offset: 0x0004B7C0
		private DataShape ParentDataShape
		{
			get
			{
				return this.m_parents.OfType<DataShape>().FirstOrDefault<DataShape>();
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x060013F1 RID: 5105 RVA: 0x0004D5D2 File Offset: 0x0004B7D2
		private DataMember ParentDataMember
		{
			get
			{
				return this.m_parents.OfType<DataMember>().FirstOrDefault<DataMember>();
			}
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x0004D5E4 File Offset: 0x0004B7E4
		protected override void Visit(DataShape dataShape)
		{
			WriteableDataShapeSharedAnnotation currentDataShapeSharedAnnotation = this.m_currentDataShapeSharedAnnotation;
			if (dataShape.IsIndependent)
			{
				this.m_currentDataShapeSharedAnnotation = new WriteableDataShapeSharedAnnotation();
			}
			if (!dataShape.Transforms.IsNullOrEmpty<DataTransform>())
			{
				this.m_hasOrContainsTransforms = true;
			}
			int currentRowIndex = this.m_currentRowIndex;
			int currentColumnIndex = this.m_currentColumnIndex;
			this.m_currentRowIndex = -1;
			this.m_currentColumnIndex = -1;
			this.m_dataShapeAnnotations.Add(dataShape, new WriteableDataShapeAnnotation(this.m_currentDataShapeSharedAnnotation));
			base.Visit(dataShape);
			this.m_currentRowIndex = currentRowIndex;
			this.m_currentColumnIndex = currentColumnIndex;
			this.m_currentDataShapeSharedAnnotation = currentDataShapeSharedAnnotation;
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x0004D66E File Offset: 0x0004B86E
		protected override void Enter(DataShape dataShape)
		{
			this.m_parents.Push(dataShape);
			this.AddContainingDataShape(dataShape, dataShape);
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x0004D684 File Offset: 0x0004B884
		protected override void Exit(DataShape dataShape)
		{
			this.m_batchSubtotalSortAnnotationAnalyzer.Analyze(dataShape);
			this.m_parents.Pop();
			this.ComputeFilterTargets(dataShape.Filters, dataShape);
			this.DiscoverAggregates(dataShape);
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x0004D6B4 File Offset: 0x0004B8B4
		private void DiscoverAggregates(DataShape dataShape)
		{
			if (dataShape.ContextOnly.GetValueOrDefault<bool>() && !dataShape.IsIndependent)
			{
				return;
			}
			List<Calculation> list = this.m_scopeTree.GetItems<Calculation>(dataShape.Id).ToList<Calculation>();
			if (list.Count == 0)
			{
				return;
			}
			List<Calculation> list2 = null;
			foreach (Calculation calculation in list)
			{
				CalculationAnnotation calculationAnnotation = this.m_calculationAnnotations[calculation];
				if (!calculationAnnotation.IsSubtotal && !calculationAnnotation.CanBeHandledByProcessing && StructureAggregateAnalyzer.IsAggregate(this.m_expressionTable.GetNode(calculation.Value), dataShape, this.m_scopeTree))
				{
					Microsoft.DataShaping.Util.AddToLazyList<Calculation>(ref list2, calculation);
				}
			}
			DataShape parentDataShape = this.ParentDataShape;
			IScope parentScope = this.m_scopeTree.GetParentScope(dataShape);
			List<Filter> list3;
			DataShape dataShape2;
			if (parentScope != null && parentScope == parentDataShape && !this.m_filtersByTarget.TryGetValue(dataShape, out list3) && !dataShape.IsIndependent)
			{
				dataShape2 = parentDataShape;
			}
			else
			{
				dataShape2 = dataShape;
			}
			this.m_dataShapeAnnotations[dataShape2].AggregatesAndProjections.AddRange(list);
			if (list2 != null)
			{
				this.m_dataShapeAnnotations[dataShape2].AggregatesOverScopes.AddRange(list2);
			}
			if (this.m_calculationAnnotations.Values != null)
			{
				IEnumerable<Calculation> enumerable = (from ca in this.m_calculationAnnotations.Where(delegate(KeyValuePair<Calculation, CalculationAnnotation> ca)
					{
						DataShape dataShape3;
						return ca.Value.IsStructureAggregate && this.m_containingDataShapes.TryGetValue(ca.Key, out dataShape3) && dataShape3 == dataShape;
					})
					select ca.Key).Except(list);
				this.m_dataShapeAnnotations[dataShape].GroupingStructureAggregates.AddRange(enumerable);
			}
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x0004D89C File Offset: 0x0004BA9C
		protected override void Enter(DataMember dataMember)
		{
			this.m_parents.Push(dataMember);
			if (dataMember.ContextOnly)
			{
				this.m_dataShapeAnnotations[this.ParentDataShape].HasContextOnlyDataMembers = true;
			}
			this.AddContainingDataShape(dataMember, this.ParentDataShape);
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x0004D8D6 File Offset: 0x0004BAD6
		protected override void Exit(DataMember dataMember)
		{
			this.m_parents.Pop();
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x0004D8E4 File Offset: 0x0004BAE4
		protected override void Enter(DataRow dataRow)
		{
			this.m_currentRowIndex++;
			this.m_currentColumnIndex = -1;
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x0004D8FC File Offset: 0x0004BAFC
		protected override void Enter(DataIntersection dataIntersection)
		{
			this.m_currentColumnIndex++;
			this.m_parents.Push(dataIntersection);
			this.AddContainingDataShape(dataIntersection, this.ParentDataShape);
			IEnumerable<Limit> limitsForTargetScope = this.ParentDataShape.Limits.GetLimitsForTargetScope(this.m_expressionTable, dataIntersection);
			bool flag = this.AreLeafMembersContentsIncludedInOutput(true, this.m_currentRowIndex) && this.AreLeafMembersContentsIncludedInOutput(false, this.m_currentColumnIndex);
			DataIntersectionAnnotation dataIntersectionAnnotation = new DataIntersectionAnnotation(limitsForTargetScope, flag);
			this.AddAnnotation(dataIntersection, dataIntersectionAnnotation);
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x0004D978 File Offset: 0x0004BB78
		private bool AreLeafMembersContentsIncludedInOutput(bool isPrimary, int currentIndex)
		{
			IScope scope;
			DataMember leafMember = this.ParentDataShape.GetLeafMember(this.m_dataMemberAnnotations, isPrimary, currentIndex, out scope);
			return this.m_dataMemberAnnotations.AreContentsIncludedInOutput(leafMember);
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x0004D9A7 File Offset: 0x0004BBA7
		protected override void Exit(DataIntersection dataIntersection)
		{
			this.m_parents.Pop();
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x0004D9B8 File Offset: 0x0004BBB8
		protected override void Visit(Calculation calculation)
		{
			ExpressionNode node = this.m_expressionTable.GetNode(calculation.Value);
			Calculation calculation2;
			bool flag = SubtotalAnalyzer.IsSubtotal(node, out calculation2);
			SortDirection? sortDirection = null;
			IScope scope = null;
			WriteableDataShapeAnnotation writeableDataShapeAnnotation = this.m_dataShapeAnnotations[this.ParentDataShape];
			if (flag)
			{
				sortDirection = SubtotalSortDirectionAnalyzer.ComputeSubtotalSortDirection(this.m_errorContext, this.m_dataMemberAnnotations, this.ParentDataShape, this.m_parents.ToList<IIdentifiable>(), this.m_currentRowIndex, this.m_currentColumnIndex, out scope);
				writeableDataShapeAnnotation.SubtotalCalculations.Add(calculation);
			}
			bool flag2 = MeasureAnalyzer.IsMeasure(node);
			bool flag3 = ExpressionAnalysisUtils.IsVisualCalculation(node);
			if (flag3)
			{
				writeableDataShapeAnnotation.VisualCalculations.Add(calculation);
			}
			if (calculation.IsContextOnly)
			{
				writeableDataShapeAnnotation.HasContextOnlyCalculations = true;
			}
			if (!string.IsNullOrEmpty(calculation.NativeReferenceName))
			{
				writeableDataShapeAnnotation.CalculationsWithNativeReferenceName.Add(calculation);
			}
			bool flag4 = !calculation.SuppressJoinPredicate.GetValueOrDefault<bool>() && flag2;
			bool flag5 = QueryInclusionAnalyzer.CanBeHandledByProcessing(node, this.m_expressionTable);
			IIdentifiable identifiable = this.m_parents.Peek();
			IReadOnlyList<IScope> readOnlyList = ScopeReferenceExpressionAnalyzer.CollectReferencedScopes(node, this.m_scopeTree);
			bool flag6 = StructureAggregateAnalyzer.IsAggregate(node, this.ParentDataShape, this.m_scopeTree);
			bool flag7 = ExpressionAnalysisUtils.IsSynchronizationIndex(node);
			CalculationAnnotation calculationAnnotation = new CalculationAnnotation(flag, calculation2, sortDirection, scope, flag2, flag4, identifiable, flag5, readOnlyList, flag6, flag7, flag3);
			this.AddAnnotation(calculation, calculationAnnotation);
			this.AddContainingDataShape(calculation, this.ParentDataShape);
			this.AddSubqueryReferenceAnnotation(calculation, this.ParentDataShape);
			if (flag7)
			{
				this.AddSynchronization(calculation, this.ParentDataShape);
			}
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x0004DB30 File Offset: 0x0004BD30
		protected override void Visit(Filter filter, Identifier dataShapeId)
		{
			DataShape parentDataShape = this.ParentDataShape;
			WriteableDataShapeAnnotation writeableDataShapeAnnotation = this.m_dataShapeAnnotations[parentDataShape];
			this.m_dataShapesByFilter.Add(filter, parentDataShape);
			if (filter.UsageKind != FilterUsageKind.Default)
			{
				return;
			}
			FilterAnnotationAnalyzerResult filterAnnotationAnalyzerResult = FilterAnnotationAnalyzer.Analyze(filter, this.m_expressionTable, new VisitDataShapeDelegate(this.VisitFilterConditionDataShape));
			if (filterAnnotationAnalyzerResult.ContextDataShape != null)
			{
				writeableDataShapeAnnotation.FilterContextDataShape = filterAnnotationAnalyzerResult.ContextDataShape;
			}
			if (filterAnnotationAnalyzerResult.IsApplyFilter)
			{
				writeableDataShapeAnnotation.ApplyFilters.Add(filter.Condition as ApplyFilterCondition);
			}
			if (filter.Condition.ObjectType == ObjectType.ExistsFilterCondition)
			{
				writeableDataShapeAnnotation.SharedAnnotation.ExistsFilter = (ExistsFilterCondition)filter.Condition;
			}
			if (filter.Condition.ObjectType == ObjectType.AnyValueFilterCondition)
			{
				AnyValueFilterCondition anyValueFilterCondition = (AnyValueFilterCondition)filter.Condition;
				if (writeableDataShapeAnnotation.AnyValueFilters == null)
				{
					writeableDataShapeAnnotation.AnyValueFilters = new List<AnyValueFilterCondition>(2);
				}
				writeableDataShapeAnnotation.AnyValueFilters.Add(anyValueFilterCondition);
			}
			if (filter.Condition.ObjectType == ObjectType.DefaultValueFilterCondition)
			{
				writeableDataShapeAnnotation.DefaultValueFilter = (DefaultValueFilterCondition)filter.Condition;
			}
			if (filterAnnotationAnalyzerResult.IsScopeFilter)
			{
				if (((ResolvedStructureReferenceExpressionNode)this.m_expressionTable.GetNode(filter.Target)).Target is DataShape)
				{
					writeableDataShapeAnnotation.SharedAnnotation.DataShapeValueFilters.Add(filter);
					return;
				}
				writeableDataShapeAnnotation.SharedAnnotation.ValueFilters.Add(filter);
			}
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x0004DC80 File Offset: 0x0004BE80
		protected override void Visit(DataTransformInput dataTransformInput)
		{
			foreach (DataTransformTableColumn dataTransformTableColumn in dataTransformInput.Table.Columns)
			{
				DataShape dataShape;
				IReadOnlyList<Calculation> readOnlyList;
				if (SubqueryCalculationReferenceAnalyzer.TryGetReferencedSubqueryDataShape(this.m_expressionTable.GetNode(dataTransformTableColumn.Value), this.ParentDataShape, this.m_scopeTree, out dataShape, out readOnlyList))
				{
					this.m_dataShapeAnnotations[this.ParentDataShape].InputSubqueryDataShapes.Add(dataShape);
				}
			}
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x0004DD18 File Offset: 0x0004BF18
		private void VisitFilterConditionDataShape(DataShape dataShape, ObjectType filterConditionType)
		{
			this.Visit(dataShape);
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x0004DD21 File Offset: 0x0004BF21
		private void AddAnnotation(Calculation calculation, CalculationAnnotation annotation)
		{
			this.m_calculationAnnotations.Add(calculation, annotation);
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x0004DD30 File Offset: 0x0004BF30
		private void AddAnnotation(DataIntersection intersection, DataIntersectionAnnotation annotation)
		{
			this.m_dataIntersectionAnnotations.Add(intersection, annotation);
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x0004DD3F File Offset: 0x0004BF3F
		private void AddContainingDataShape(IContextItem contextItem, DataShape dataShape)
		{
			this.m_containingDataShapes.Add(contextItem, dataShape);
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x0004DD50 File Offset: 0x0004BF50
		private void ComputeFilterTargets(List<Filter> filters, DataShape dataShape)
		{
			if (filters == null || filters.Count == 0)
			{
				return;
			}
			WriteableDataShapeAnnotation writeableDataShapeAnnotation = this.m_dataShapeAnnotations[dataShape];
			foreach (Filter filter in filters)
			{
				if (filter.UsageKind == FilterUsageKind.Default && filter.Condition.ObjectType != ObjectType.FilterEmptyGroupsCondition && filter.Condition.ObjectType != ObjectType.ContextFilterCondition && filter.Condition.ObjectType != ObjectType.ApplyFilterCondition && filter.Condition.ObjectType != ObjectType.ExistsFilterCondition && filter.Condition.ObjectType != ObjectType.AnyValueFilterCondition && filter.Condition.ObjectType != ObjectType.DefaultValueFilterCondition && !writeableDataShapeAnnotation.SharedAnnotation.DataShapeValueFilters.Contains(filter))
				{
					ResolvedStructureReferenceExpressionNode resolvedStructureReferenceExpressionNode = (ResolvedStructureReferenceExpressionNode)this.m_expressionTable.GetNode(filter.Target);
					List<Filter> list;
					this.m_filtersByTarget.TryGetValue(resolvedStructureReferenceExpressionNode.Target, out list);
					if (list == null)
					{
						this.m_filtersByTarget.Add(resolvedStructureReferenceExpressionNode.Target, new List<Filter> { filter });
					}
					else
					{
						list.Add(filter);
					}
					DataShape dataShape2 = resolvedStructureReferenceExpressionNode.Target as DataShape;
					bool flag;
					if (dataShape2 != null && FilterComplexityAnalyzer.IsComplexFilter(filter.Condition, this.m_expressionTable, this.m_schema.SupportsHierarchicalFilterDisjunction(), true, out flag, this.m_schema))
					{
						this.m_dataShapeAnnotations[dataShape2].HasComplexSlicers = true;
						this.m_dataShapeAnnotations[dataShape2].ComplexSlicerExceededMaxDepth = flag;
					}
				}
			}
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x0004DF0C File Offset: 0x0004C10C
		private void AddSubqueryReferenceAnnotation(Calculation calculation, DataShape containingDataShape)
		{
			DataShape dataShape;
			IReadOnlyList<Calculation> readOnlyList;
			if (SubqueryCalculationReferenceAnalyzer.TryGetReferencedSubqueryDataShape(this.m_expressionTable.GetNode(calculation.Value), containingDataShape, this.m_scopeTree, out dataShape, out readOnlyList))
			{
				this.m_dataShapeAnnotations[containingDataShape].InputSubqueryDataShapes.Add(dataShape);
			}
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x0004DF54 File Offset: 0x0004C154
		private void AddSynchronization(Calculation calculation, DataShape dataShape)
		{
			WriteableDataShapeAnnotation writeableDataShapeAnnotation = this.m_dataShapeAnnotations[dataShape];
			if (this.m_dataMemberAnnotations.IsPrimaryMember(this.ParentDataMember))
			{
				if (writeableDataShapeAnnotation.PrimaryHierarchySyncCalcs == null)
				{
					writeableDataShapeAnnotation.PrimaryHierarchySyncCalcs = new List<Calculation>();
				}
				writeableDataShapeAnnotation.PrimaryHierarchySyncCalcs.Add(calculation);
				return;
			}
			if (writeableDataShapeAnnotation.SecondaryHierarchySyncCalcs == null)
			{
				writeableDataShapeAnnotation.SecondaryHierarchySyncCalcs = new List<Calculation>();
			}
			writeableDataShapeAnnotation.SecondaryHierarchySyncCalcs.Add(calculation);
		}

		// Token: 0x040008CB RID: 2251
		private readonly ExpressionTable m_expressionTable;

		// Token: 0x040008CC RID: 2252
		private readonly IFederatedConceptualSchema m_schema;

		// Token: 0x040008CD RID: 2253
		private readonly ScopeTree m_scopeTree;

		// Token: 0x040008CE RID: 2254
		private readonly TranslationErrorContext m_errorContext;

		// Token: 0x040008CF RID: 2255
		private readonly Stack<IIdentifiable> m_parents;

		// Token: 0x040008D0 RID: 2256
		private readonly Dictionary<IContextItem, DataShape> m_containingDataShapes;

		// Token: 0x040008D1 RID: 2257
		private readonly Dictionary<Filter, DataShape> m_dataShapesByFilter;

		// Token: 0x040008D2 RID: 2258
		private readonly Dictionary<IIdentifiable, List<Filter>> m_filtersByTarget;

		// Token: 0x040008D3 RID: 2259
		private readonly Dictionary<DataShape, WriteableDataShapeAnnotation> m_dataShapeAnnotations;

		// Token: 0x040008D4 RID: 2260
		private readonly Dictionary<Calculation, CalculationAnnotation> m_calculationAnnotations;

		// Token: 0x040008D5 RID: 2261
		private readonly DataMemberAnnotations m_dataMemberAnnotations;

		// Token: 0x040008D6 RID: 2262
		private readonly Dictionary<DataIntersection, DataIntersectionAnnotation> m_dataIntersectionAnnotations;

		// Token: 0x040008D7 RID: 2263
		private readonly BatchSubtotalSortAnnotationAnalyzer m_batchSubtotalSortAnnotationAnalyzer;

		// Token: 0x040008D8 RID: 2264
		private WriteableDataShapeSharedAnnotation m_currentDataShapeSharedAnnotation;

		// Token: 0x040008D9 RID: 2265
		private int m_currentRowIndex = -1;

		// Token: 0x040008DA RID: 2266
		private int m_currentColumnIndex = -1;

		// Token: 0x040008DB RID: 2267
		private bool m_hasOrContainsTransforms;
	}
}
