using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations
{
	// Token: 0x0200024A RID: 586
	internal sealed class WriteableDataShapeAnnotation
	{
		// Token: 0x06001406 RID: 5126 RVA: 0x0004DFC0 File Offset: 0x0004C1C0
		internal WriteableDataShapeAnnotation(WriteableDataShapeSharedAnnotation sharedAnnotation)
		{
			this.ApplyFilters = new List<ApplyFilterCondition>();
			this.VisualCalculations = new List<Calculation>();
			this.CalculationsWithNativeReferenceName = new List<Calculation>();
			this.SubtotalCalculations = new List<Calculation>();
			this.AggregatesAndProjections = new List<Calculation>();
			this.AggregatesOverScopes = new List<Calculation>();
			this.GroupingStructureAggregates = new List<Calculation>();
			this.FilterContextDataShape = null;
			this.HasComplexSlicers = false;
			this.ComplexSlicerExceededMaxDepth = false;
			this.AnyValueFilters = null;
			this.DefaultValueFilter = null;
			this.SharedAnnotation = sharedAnnotation ?? new WriteableDataShapeSharedAnnotation();
			this.InputSubqueryDataShapes = new HashSet<DataShape>();
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06001407 RID: 5127 RVA: 0x0004E05E File Offset: 0x0004C25E
		// (set) Token: 0x06001408 RID: 5128 RVA: 0x0004E066 File Offset: 0x0004C266
		public DataShape FilterContextDataShape { get; set; }

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06001409 RID: 5129 RVA: 0x0004E06F File Offset: 0x0004C26F
		// (set) Token: 0x0600140A RID: 5130 RVA: 0x0004E077 File Offset: 0x0004C277
		public List<ApplyFilterCondition> ApplyFilters { get; set; }

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x0600140B RID: 5131 RVA: 0x0004E080 File Offset: 0x0004C280
		// (set) Token: 0x0600140C RID: 5132 RVA: 0x0004E088 File Offset: 0x0004C288
		public bool HasComplexSlicers { get; set; }

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x0600140D RID: 5133 RVA: 0x0004E091 File Offset: 0x0004C291
		// (set) Token: 0x0600140E RID: 5134 RVA: 0x0004E099 File Offset: 0x0004C299
		public bool ComplexSlicerExceededMaxDepth { get; set; }

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x0600140F RID: 5135 RVA: 0x0004E0A2 File Offset: 0x0004C2A2
		// (set) Token: 0x06001410 RID: 5136 RVA: 0x0004E0AA File Offset: 0x0004C2AA
		public bool HasContextOnlyCalculations { get; set; }

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06001411 RID: 5137 RVA: 0x0004E0B3 File Offset: 0x0004C2B3
		// (set) Token: 0x06001412 RID: 5138 RVA: 0x0004E0BB File Offset: 0x0004C2BB
		public bool HasContextOnlyDataMembers { get; set; }

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06001413 RID: 5139 RVA: 0x0004E0C4 File Offset: 0x0004C2C4
		public List<Calculation> VisualCalculations { get; }

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06001414 RID: 5140 RVA: 0x0004E0CC File Offset: 0x0004C2CC
		public List<Calculation> CalculationsWithNativeReferenceName { get; }

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06001415 RID: 5141 RVA: 0x0004E0D4 File Offset: 0x0004C2D4
		public List<Calculation> SubtotalCalculations { get; }

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06001416 RID: 5142 RVA: 0x0004E0DC File Offset: 0x0004C2DC
		public List<Calculation> AggregatesAndProjections { get; }

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06001417 RID: 5143 RVA: 0x0004E0E4 File Offset: 0x0004C2E4
		public List<Calculation> AggregatesOverScopes { get; }

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06001418 RID: 5144 RVA: 0x0004E0EC File Offset: 0x0004C2EC
		public List<Calculation> GroupingStructureAggregates { get; }

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06001419 RID: 5145 RVA: 0x0004E0F4 File Offset: 0x0004C2F4
		// (set) Token: 0x0600141A RID: 5146 RVA: 0x0004E0FC File Offset: 0x0004C2FC
		public List<Calculation> PrimaryHierarchySyncCalcs { get; set; }

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x0600141B RID: 5147 RVA: 0x0004E105 File Offset: 0x0004C305
		// (set) Token: 0x0600141C RID: 5148 RVA: 0x0004E10D File Offset: 0x0004C30D
		public List<Calculation> SecondaryHierarchySyncCalcs { get; set; }

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x0600141D RID: 5149 RVA: 0x0004E116 File Offset: 0x0004C316
		// (set) Token: 0x0600141E RID: 5150 RVA: 0x0004E11E File Offset: 0x0004C31E
		public List<AnyValueFilterCondition> AnyValueFilters { get; set; }

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x0600141F RID: 5151 RVA: 0x0004E127 File Offset: 0x0004C327
		// (set) Token: 0x06001420 RID: 5152 RVA: 0x0004E12F File Offset: 0x0004C32F
		public DefaultValueFilterCondition DefaultValueFilter { get; set; }

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06001421 RID: 5153 RVA: 0x0004E138 File Offset: 0x0004C338
		public WriteableDataShapeSharedAnnotation SharedAnnotation { get; }

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06001422 RID: 5154 RVA: 0x0004E140 File Offset: 0x0004C340
		// (set) Token: 0x06001423 RID: 5155 RVA: 0x0004E148 File Offset: 0x0004C348
		public HashSet<DataShape> InputSubqueryDataShapes { get; set; }

		// Token: 0x06001424 RID: 5156 RVA: 0x0004E154 File Offset: 0x0004C354
		public DataShapeAnnotation ToReadOnly(Dictionary<WriteableDataShapeSharedAnnotation, DataShapeSharedAnnotation> sharedAnnotationsMap)
		{
			DataShapeSharedAnnotation dataShapeSharedAnnotation;
			if (!sharedAnnotationsMap.TryGetValue(this.SharedAnnotation, out dataShapeSharedAnnotation))
			{
				dataShapeSharedAnnotation = this.SharedAnnotation.ToReadOnly();
				sharedAnnotationsMap.Add(this.SharedAnnotation, dataShapeSharedAnnotation);
			}
			return new DataShapeAnnotation(this.FilterContextDataShape, this.ApplyFilters, this.HasComplexSlicers, this.ComplexSlicerExceededMaxDepth, this.HasContextOnlyCalculations, this.HasContextOnlyDataMembers, this.VisualCalculations, this.CalculationsWithNativeReferenceName, this.SubtotalCalculations, this.AggregatesAndProjections, this.AggregatesOverScopes, this.GroupingStructureAggregates, this.AnyValueFilters, this.DefaultValueFilter, dataShapeSharedAnnotation, this.InputSubqueryDataShapes.ToReadOnlyList<DataShape>(), this.PrimaryHierarchySyncCalcs, this.SecondaryHierarchySyncCalcs);
		}
	}
}
