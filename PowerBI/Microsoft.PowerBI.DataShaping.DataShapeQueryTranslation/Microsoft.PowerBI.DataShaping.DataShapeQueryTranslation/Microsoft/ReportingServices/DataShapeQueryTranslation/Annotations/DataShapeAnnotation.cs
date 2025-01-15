using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations
{
	// Token: 0x0200024D RID: 589
	internal sealed class DataShapeAnnotation
	{
		// Token: 0x0600145D RID: 5213 RVA: 0x0004E628 File Offset: 0x0004C828
		internal DataShapeAnnotation(DataShape filterContextDataShape, IReadOnlyList<ApplyFilterCondition> applyFilters, bool hasComplexSlicers, bool complexSlicerExceededMaxDepth, bool hasContextOnlyCalculations, bool hasContextOnlyDataMemebers, IReadOnlyList<Calculation> visualCalculations, IReadOnlyList<Calculation> calculationsWithNativeReferenceName, IReadOnlyList<Calculation> subtotalCalculations, IReadOnlyList<Calculation> aggregatesAndProjections, IReadOnlyList<Calculation> aggregatesOverScopes, IReadOnlyList<Calculation> groupingStructureAggregates, IReadOnlyList<AnyValueFilterCondition> anyValueFilters, DefaultValueFilterCondition defaultValueFilter, DataShapeSharedAnnotation dataShapeSharedAnnotation, IReadOnlyList<DataShape> inputSubqueryDataShapes, IReadOnlyList<Calculation> primaryHierarchySyncCalcs, IReadOnlyList<Calculation> secondaryHierarchySyncCalcs)
		{
			this.FilterContextDataShape = filterContextDataShape;
			this.ApplyFilters = applyFilters;
			this.HasComplexSlicers = hasComplexSlicers;
			this.ComplexSlicerExceededMaxDepth = complexSlicerExceededMaxDepth;
			this.VisualCalculations = visualCalculations;
			this.CalculationsWithNativeReferenceName = calculationsWithNativeReferenceName;
			this.SubtotalCalculations = subtotalCalculations;
			this.AggregatesAndProjections = aggregatesAndProjections;
			this.AggregatesOverScopes = aggregatesOverScopes;
			this.GroupingStructureAggregates = groupingStructureAggregates;
			this.AnyValueFilters = anyValueFilters;
			this.DefaultValueFilter = defaultValueFilter;
			this.SharedAnnotation = dataShapeSharedAnnotation;
			this.InputSubqueryDataShapes = inputSubqueryDataShapes;
			this.PrimaryHierarchySyncCalcs = primaryHierarchySyncCalcs;
			this.SecondaryHierarchySyncCalcs = secondaryHierarchySyncCalcs;
			this.HasContextOnlyCalculations = hasContextOnlyCalculations;
			this.HasContextOnlyDataMemebers = hasContextOnlyDataMemebers;
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x0600145E RID: 5214 RVA: 0x0004E6C8 File Offset: 0x0004C8C8
		public DataShape FilterContextDataShape { get; }

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x0600145F RID: 5215 RVA: 0x0004E6D0 File Offset: 0x0004C8D0
		public IReadOnlyList<ApplyFilterCondition> ApplyFilters { get; }

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06001460 RID: 5216 RVA: 0x0004E6D8 File Offset: 0x0004C8D8
		public bool HasComplexSlicers { get; }

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06001461 RID: 5217 RVA: 0x0004E6E0 File Offset: 0x0004C8E0
		public bool ComplexSlicerExceededMaxDepth { get; }

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06001462 RID: 5218 RVA: 0x0004E6E8 File Offset: 0x0004C8E8
		public bool HasContextOnlyCalculations { get; }

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06001463 RID: 5219 RVA: 0x0004E6F0 File Offset: 0x0004C8F0
		public bool HasContextOnlyDataMemebers { get; }

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06001464 RID: 5220 RVA: 0x0004E6F8 File Offset: 0x0004C8F8
		public IReadOnlyList<Calculation> VisualCalculations { get; }

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06001465 RID: 5221 RVA: 0x0004E700 File Offset: 0x0004C900
		public IReadOnlyList<Calculation> CalculationsWithNativeReferenceName { get; }

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06001466 RID: 5222 RVA: 0x0004E708 File Offset: 0x0004C908
		public IReadOnlyList<Calculation> SubtotalCalculations { get; }

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06001467 RID: 5223 RVA: 0x0004E710 File Offset: 0x0004C910
		public IReadOnlyList<Calculation> AggregatesAndProjections { get; }

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06001468 RID: 5224 RVA: 0x0004E718 File Offset: 0x0004C918
		public IReadOnlyList<Calculation> AggregatesOverScopes { get; }

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06001469 RID: 5225 RVA: 0x0004E720 File Offset: 0x0004C920
		public IReadOnlyList<Calculation> GroupingStructureAggregates { get; }

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x0600146A RID: 5226 RVA: 0x0004E728 File Offset: 0x0004C928
		public IReadOnlyList<AnyValueFilterCondition> AnyValueFilters { get; }

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x0600146B RID: 5227 RVA: 0x0004E730 File Offset: 0x0004C930
		public DefaultValueFilterCondition DefaultValueFilter { get; }

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x0600146C RID: 5228 RVA: 0x0004E738 File Offset: 0x0004C938
		public DataShapeSharedAnnotation SharedAnnotation { get; }

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x0600146D RID: 5229 RVA: 0x0004E740 File Offset: 0x0004C940
		public IReadOnlyList<DataShape> InputSubqueryDataShapes { get; }

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x0600146E RID: 5230 RVA: 0x0004E748 File Offset: 0x0004C948
		public IReadOnlyList<Calculation> PrimaryHierarchySyncCalcs { get; }

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x0600146F RID: 5231 RVA: 0x0004E750 File Offset: 0x0004C950
		public IReadOnlyList<Calculation> SecondaryHierarchySyncCalcs { get; }

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06001470 RID: 5232 RVA: 0x0004E758 File Offset: 0x0004C958
		public ExistsFilterCondition ExistsFilter
		{
			get
			{
				return this.SharedAnnotation.ExistsFilter;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06001471 RID: 5233 RVA: 0x0004E765 File Offset: 0x0004C965
		public IReadOnlyList<Filter> ScopedValueFilters
		{
			get
			{
				return this.SharedAnnotation.ScopedValueFilters;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06001472 RID: 5234 RVA: 0x0004E772 File Offset: 0x0004C972
		public IReadOnlyList<Filter> DataShapeValueFilters
		{
			get
			{
				return this.SharedAnnotation.DataShapeValueFilters;
			}
		}
	}
}
