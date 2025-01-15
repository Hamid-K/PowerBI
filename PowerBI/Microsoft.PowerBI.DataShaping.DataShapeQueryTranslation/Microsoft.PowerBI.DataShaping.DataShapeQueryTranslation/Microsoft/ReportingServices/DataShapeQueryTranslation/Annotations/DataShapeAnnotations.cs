using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations
{
	// Token: 0x0200024C RID: 588
	internal sealed class DataShapeAnnotations
	{
		// Token: 0x0600142B RID: 5163 RVA: 0x0004E25C File Offset: 0x0004C45C
		internal DataShapeAnnotations(Dictionary<IContextItem, DataShape> containingDataShapes, Dictionary<Filter, DataShape> dataShapesByFilter, Dictionary<IIdentifiable, List<Filter>> filtersByTarget, Dictionary<DataShape, DataShapeAnnotation> dataShapeAnnotations, Dictionary<Calculation, CalculationAnnotation> calculationAnnotations, DataMemberAnnotations dataMemberAnnotations, Dictionary<DataIntersection, DataIntersectionAnnotation> dataIntersectionAnnotations, BatchSubtotalAnnotations batchSubtotalAnnotations, BatchSubtotalSortAnnotations batchSubtotalSortAnnotations, bool hasOrContainsTransforms)
		{
			this.m_containingDataShapes = containingDataShapes;
			this.m_dataShapesByFilter = dataShapesByFilter;
			this.m_filtersByTarget = filtersByTarget;
			this.m_dataShapeAnnotations = dataShapeAnnotations;
			this.m_calculationAnnotations = calculationAnnotations;
			this.DataMemberAnnotations = dataMemberAnnotations;
			this.m_dataIntersectionAnnotations = dataIntersectionAnnotations;
			this.SubtotalAnnotations = batchSubtotalAnnotations;
			this.m_batchSubtotalSortAnnotations = batchSubtotalSortAnnotations;
			this.m_hasOrContainsTransforms = hasOrContainsTransforms;
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x0600142C RID: 5164 RVA: 0x0004E2BC File Offset: 0x0004C4BC
		internal DataMemberAnnotations DataMemberAnnotations { get; }

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x0600142D RID: 5165 RVA: 0x0004E2C4 File Offset: 0x0004C4C4
		internal BatchSubtotalAnnotations SubtotalAnnotations { get; }

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x0600142E RID: 5166 RVA: 0x0004E2CC File Offset: 0x0004C4CC
		internal IReadOnlyDictionary<IIdentifiable, List<Filter>> FiltersByTarget
		{
			get
			{
				return this.m_filtersByTarget;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x0600142F RID: 5167 RVA: 0x0004E2D4 File Offset: 0x0004C4D4
		internal bool HasOrContainsTransforms
		{
			get
			{
				return this.m_hasOrContainsTransforms;
			}
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x0004E2DC File Offset: 0x0004C4DC
		internal DataShapeAnnotation GetDataShapeAnnotation(DataShape dataShape)
		{
			return this.m_dataShapeAnnotations[dataShape];
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x0004E2EA File Offset: 0x0004C4EA
		public bool IsValueFilter(Filter filter)
		{
			return this.GetDataShapeAnnotation(this.m_dataShapesByFilter[filter]).ScopedValueFilters.Contains(filter);
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x0004E309 File Offset: 0x0004C509
		public bool IsDataShapeValueFilter(Filter filter)
		{
			return this.GetDataShapeAnnotation(this.m_dataShapesByFilter[filter]).DataShapeValueFilters.Contains(filter);
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x0004E328 File Offset: 0x0004C528
		public DataShape GetContainingDataShape(IContextItem contextItem)
		{
			return this.m_containingDataShapes[contextItem];
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x0004E336 File Offset: 0x0004C536
		public DataShape GetContainingDataShape(Filter filter)
		{
			return this.m_dataShapesByFilter[filter];
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x0004E344 File Offset: 0x0004C544
		public bool TryGetFilterContextDataShape(DataShape dataShape, out DataShape filterContextDataShape)
		{
			filterContextDataShape = this.GetDataShapeAnnotation(dataShape).FilterContextDataShape;
			return filterContextDataShape != null;
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x0004E359 File Offset: 0x0004C559
		public bool HasFilterContextDataShape(DataShape dataShape)
		{
			return this.GetDataShapeAnnotation(dataShape).FilterContextDataShape != null;
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x0004E36C File Offset: 0x0004C56C
		public Filter GetFilter(IIdentifiable item, IReadOnlyDictionary<IIdentifiable, List<Filter>> overrideFilterTable = null)
		{
			IReadOnlyDictionary<IIdentifiable, List<Filter>> readOnlyDictionary;
			if (overrideFilterTable == null)
			{
				readOnlyDictionary = this.m_filtersByTarget;
			}
			else
			{
				readOnlyDictionary = overrideFilterTable;
			}
			List<Filter> list = null;
			readOnlyDictionary.TryGetValue(item, out list);
			if (list == null || list.Count == 0)
			{
				return null;
			}
			return list.Single<Filter>();
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x0004E3A6 File Offset: 0x0004C5A6
		public Limit GetLimitWithInnermostTarget(DataMember member)
		{
			return this.DataMemberAnnotations.GetLimitWithInnermostTarget(member);
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x0004E3B4 File Offset: 0x0004C5B4
		public bool IsVisualCalculation(Calculation calculation)
		{
			return this.GetAnnotation(calculation).IsVisualCalculation;
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x0004E3C2 File Offset: 0x0004C5C2
		public bool IsMeasure(Calculation calculation)
		{
			return this.GetAnnotation(calculation).IsMeasure;
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x0004E3D0 File Offset: 0x0004C5D0
		public bool CanBeHandledByProcessing(Calculation calculation)
		{
			return this.GetAnnotation(calculation).CanBeHandledByProcessing;
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x0004E3DE File Offset: 0x0004C5DE
		public bool IsSubtotal(Calculation calculation)
		{
			return this.GetAnnotation(calculation).IsSubtotal;
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x0004E3EC File Offset: 0x0004C5EC
		public bool IsSubtotal(Calculation calculation, out Calculation targetCalc)
		{
			CalculationAnnotation annotation = this.GetAnnotation(calculation);
			targetCalc = annotation.SubtotalTargetCalculation;
			return annotation.IsSubtotal;
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x0004E40F File Offset: 0x0004C60F
		internal bool IsSynchronizationIndex(Calculation calculation)
		{
			return this.GetAnnotation(calculation).IsSynchronizationIndex;
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x0004E41D File Offset: 0x0004C61D
		public bool IsAggregate(Calculation calculation)
		{
			return this.GetAnnotation(calculation).IsStructureAggregate;
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x0004E42C File Offset: 0x0004C62C
		public SortDirection GetSubtotalSortDirection(Calculation calculation)
		{
			CalculationAnnotation annotation = this.GetAnnotation(calculation);
			Contract.RetailAssert(annotation.SubtotalSortDirection != null, "Expected a sort direction to be computed for a rollup calculation.");
			return annotation.SubtotalSortDirection.Value;
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x0004E465 File Offset: 0x0004C665
		public IScope GetRollupParent(Calculation calculation)
		{
			return this.GetAnnotation(calculation).RollupParent;
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x0004E473 File Offset: 0x0004C673
		public bool NeededForQueryCalculationContext(Calculation calculation)
		{
			return this.GetAnnotation(calculation).IsNeededForQueryCalculationContext;
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x0004E481 File Offset: 0x0004C681
		public IReadOnlyList<IScope> GetReferencedScopes(Calculation calculation)
		{
			return this.GetAnnotation(calculation).ReferencedScopes;
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x0004E490 File Offset: 0x0004C690
		private CalculationAnnotation GetAnnotation(Calculation calculation)
		{
			CalculationAnnotation calculationAnnotation;
			this.m_calculationAnnotations.TryGetValue(calculation, out calculationAnnotation);
			return calculationAnnotation;
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x0004E4AD File Offset: 0x0004C6AD
		public IReadOnlyList<Calculation> GetVisualCalculations(DataShape dataShape)
		{
			return this.GetDataShapeAnnotation(dataShape).VisualCalculations;
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x0004E4BB File Offset: 0x0004C6BB
		public IReadOnlyList<Calculation> GetCalculationsWithNativeReferenceName(DataShape dataShape)
		{
			return this.GetDataShapeAnnotation(dataShape).CalculationsWithNativeReferenceName;
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x0004E4C9 File Offset: 0x0004C6C9
		public IReadOnlyList<Calculation> GetSubtotalCalculations(DataShape dataShape)
		{
			return this.GetDataShapeAnnotation(dataShape).SubtotalCalculations;
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x0004E4D7 File Offset: 0x0004C6D7
		public bool HasGroupingStructureAggregates(DataShape dataShape)
		{
			return this.GetDataShapeAnnotation(dataShape).GroupingStructureAggregates.Any<Calculation>();
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x0004E4EA File Offset: 0x0004C6EA
		public bool IsPrimaryMember(DataMember member)
		{
			return this.DataMemberAnnotations.IsPrimaryMember(member);
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x0004E4F8 File Offset: 0x0004C6F8
		public bool IsLeaf(DataMember member)
		{
			return this.DataMemberAnnotations.IsLeaf(member);
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x0004E506 File Offset: 0x0004C706
		public int GetLeafIndex(DataMember member)
		{
			return this.DataMemberAnnotations.GetLeafIndex(member);
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x0004E514 File Offset: 0x0004C714
		public IIdentifiable GetParent(Calculation calculation)
		{
			return this.GetAnnotation(calculation).Parent;
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x0004E522 File Offset: 0x0004C722
		public bool ValidateBatchSubtotalAnnotations(TranslationErrorContext errorContext)
		{
			return this.SubtotalAnnotations.Validate(errorContext);
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x0600144E RID: 5198 RVA: 0x0004E530 File Offset: 0x0004C730
		public int BatchSubtotalAnnotationCount
		{
			get
			{
				return this.SubtotalAnnotations.SubtotalAnnotationCount;
			}
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x0004E53D File Offset: 0x0004C73D
		public bool TryGetBatchSubtotalAnnotation(IScope rollupStopScope, out BatchSubtotalAnnotation batchSubtotalAnnotation)
		{
			return this.SubtotalAnnotations.TryGetSubtotalAnnotation(rollupStopScope, out batchSubtotalAnnotation);
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x0004E54C File Offset: 0x0004C74C
		public bool TryGetBatchSubtotalSourceAnnotation(DataMember member, out BatchSubtotalAnnotation batchSubtotalAnnotation)
		{
			return this.SubtotalAnnotations.TryGetSubtotalSourceAnnotation(member, out batchSubtotalAnnotation);
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x0004E55B File Offset: 0x0004C75B
		public bool TryGetBatchSortByMeasureSourceAnnotation(DataMember member, out BatchSortByMeasureSourceAnnotation batchSortByMeasureAnnotation)
		{
			return this.m_batchSubtotalSortAnnotations.TryGetSortyByMeasureSourceAnnotation(member, out batchSortByMeasureAnnotation);
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x0004E56A File Offset: 0x0004C76A
		private DataIntersectionAnnotation GetAnnotation(DataIntersection intersection)
		{
			return this.m_dataIntersectionAnnotations[intersection];
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x0004E578 File Offset: 0x0004C778
		public Limit GetLimit(DataIntersection intersection)
		{
			return this.GetAnnotation(intersection).Limit;
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x0004E586 File Offset: 0x0004C786
		public bool AreContentsIncludedInOutput(DataIntersection intersection)
		{
			return this.GetAnnotation(intersection).AreContentsIncludedInOutput;
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x0004E594 File Offset: 0x0004C794
		public bool HasComplexSlicer(DataShape dataShape)
		{
			return dataShape != null && this.GetDataShapeAnnotation(dataShape).HasComplexSlicers;
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x0004E5A7 File Offset: 0x0004C7A7
		public IReadOnlyList<DataShape> InputSubqueryDataShapes(DataShape dataShape)
		{
			return this.GetDataShapeAnnotation(dataShape).InputSubqueryDataShapes;
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x0004E5B5 File Offset: 0x0004C7B5
		public bool ComplexSlicerExceededMaxDepth(DataShape dataShape)
		{
			return dataShape != null && this.GetDataShapeAnnotation(dataShape).ComplexSlicerExceededMaxDepth;
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x0004E5C8 File Offset: 0x0004C7C8
		public IReadOnlyList<Calculation> GetDataShapeAggregatesAndProjections(DataShape dataShape)
		{
			return this.GetDataShapeAnnotation(dataShape).AggregatesAndProjections;
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x0004E5D6 File Offset: 0x0004C7D6
		public IReadOnlyList<Calculation> GetDataShapeAggregatesOverScopes(DataShape dataShape)
		{
			return this.GetDataShapeAnnotation(dataShape).AggregatesOverScopes;
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x0004E5E4 File Offset: 0x0004C7E4
		public IReadOnlyList<AnyValueFilterCondition> GetAnyValueFilters(DataShape dataShape)
		{
			return this.GetDataShapeAnnotation(dataShape).AnyValueFilters;
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x0004E5F2 File Offset: 0x0004C7F2
		public DefaultValueFilterCondition GetDefaultValueFilter(DataShape dataShape)
		{
			return this.GetDataShapeAnnotation(dataShape).DefaultValueFilter;
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x0004E600 File Offset: 0x0004C800
		internal IReadOnlyList<Calculation> GetHierarchySyncCalcs(DataShape dataShape, bool forPrimary = true)
		{
			DataShapeAnnotation dataShapeAnnotation = this.GetDataShapeAnnotation(dataShape);
			if (forPrimary)
			{
				return dataShapeAnnotation.PrimaryHierarchySyncCalcs;
			}
			return dataShapeAnnotation.SecondaryHierarchySyncCalcs;
		}

		// Token: 0x040008F1 RID: 2289
		private readonly Dictionary<IContextItem, DataShape> m_containingDataShapes;

		// Token: 0x040008F2 RID: 2290
		private readonly Dictionary<Filter, DataShape> m_dataShapesByFilter;

		// Token: 0x040008F3 RID: 2291
		private readonly Dictionary<IIdentifiable, List<Filter>> m_filtersByTarget;

		// Token: 0x040008F4 RID: 2292
		private readonly Dictionary<DataShape, DataShapeAnnotation> m_dataShapeAnnotations;

		// Token: 0x040008F5 RID: 2293
		private readonly Dictionary<Calculation, CalculationAnnotation> m_calculationAnnotations;

		// Token: 0x040008F6 RID: 2294
		private readonly Dictionary<DataIntersection, DataIntersectionAnnotation> m_dataIntersectionAnnotations;

		// Token: 0x040008F7 RID: 2295
		private readonly BatchSubtotalSortAnnotations m_batchSubtotalSortAnnotations;

		// Token: 0x040008F8 RID: 2296
		private readonly bool m_hasOrContainsTransforms;
	}
}
