using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001A1 RID: 417
	public class DataShapeBindingAxisGroupingBuilder<TParent> : BaseBindingBuilder<DataShapeBindingAxisGrouping, TParent>
	{
		// Token: 0x06000B53 RID: 2899 RVA: 0x000164D0 File Offset: 0x000146D0
		public DataShapeBindingAxisGroupingBuilder(TParent parent)
			: base(parent)
		{
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x000164D9 File Offset: 0x000146D9
		public DataShapeBindingAxisGroupingBuilder<TParent> WithProjections(params int[] selectIndices)
		{
			this._projections = selectIndices;
			return this;
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x000164E3 File Offset: 0x000146E3
		public DataShapeBindingAxisGroupingBuilder<TParent> WithSuppressedProjections(params int[] selectIndices)
		{
			this._suppressedProjections = new List<int>(selectIndices);
			return this;
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x000164F2 File Offset: 0x000146F2
		public DataShapeBindingAxisGroupingBuilder<TParent> AddProjection(int index)
		{
			if (this._projections == null || !this._projections.Contains(index))
			{
				BaseBindingBuilder<DataShapeBindingAxisGrouping, TParent>.AddToLazyList<int>(ref this._projections, index);
			}
			return this;
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x00016517 File Offset: 0x00014717
		public DataShapeBindingAxisGroupingBuilder<TParent> WithGroupBy(params int[] groupByIndices)
		{
			this._groupBy = groupByIndices;
			return this;
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x00016521 File Offset: 0x00014721
		public DataShapeBindingAxisGroupingBuilder<TParent> WithShowAll(params int[] selectIndices)
		{
			this._showItemsWithNoData = selectIndices;
			return this;
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x0001652B File Offset: 0x0001472B
		public DataShapeBindingAxisGroupingBuilder<TParent> WithSubtotal(SubtotalType subtotal)
		{
			this._subtotal = subtotal;
			return this;
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x00016538 File Offset: 0x00014738
		public FilterDefinitionBuilder<DataShapeBindingAxisGroupingBuilder<TParent>> WithInstanceFilter()
		{
			FilterDefinitionBuilder<DataShapeBindingAxisGroupingBuilder<TParent>> filterDefinitionBuilder = new FilterDefinitionBuilder<DataShapeBindingAxisGroupingBuilder<TParent>>(this);
			BaseBindingBuilder<DataShapeBindingAxisGrouping, TParent>.AddToLazyList<FilterDefinitionBuilder<DataShapeBindingAxisGroupingBuilder<TParent>>>(ref this._instanceFilters, filterDefinitionBuilder);
			return filterDefinitionBuilder;
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0001655C File Offset: 0x0001475C
		public DataShapeBindingAggregateBuilder<DataShapeBindingAxisGroupingBuilder<TParent>> WithAggregate(int selectIndex)
		{
			DataShapeBindingAggregateBuilder<DataShapeBindingAxisGroupingBuilder<TParent>> dataShapeBindingAggregateBuilder = new DataShapeBindingAggregateBuilder<DataShapeBindingAxisGroupingBuilder<TParent>>(this, selectIndex);
			BaseBindingBuilder<DataShapeBindingAxisGrouping, TParent>.AddToLazyList<DataShapeBindingAggregateBuilder<DataShapeBindingAxisGroupingBuilder<TParent>>>(ref this._aggregateBuilders, dataShapeBindingAggregateBuilder);
			return dataShapeBindingAggregateBuilder;
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x00016580 File Offset: 0x00014780
		public override DataShapeBindingAxisGrouping Build()
		{
			return new DataShapeBindingAxisGrouping
			{
				Projections = this._projections,
				SuppressedProjections = this._suppressedProjections,
				GroupBy = this._groupBy,
				Subtotal = new SubtotalType?(this._subtotal),
				ShowItemsWithNoData = this._showItemsWithNoData,
				InstanceFilters = BaseBindingBuilder<DataShapeBindingAxisGrouping, TParent>.SafeBuild<FilterDefinition, FilterDefinitionBuilder<DataShapeBindingAxisGroupingBuilder<TParent>>, DataShapeBindingAxisGroupingBuilder<TParent>>(this._instanceFilters),
				Aggregates = BaseBindingBuilder<DataShapeBindingAxisGrouping, TParent>.SafeBuild<DataShapeBindingAggregate, DataShapeBindingAggregateBuilder<DataShapeBindingAxisGroupingBuilder<TParent>>, DataShapeBindingAxisGroupingBuilder<TParent>>(this._aggregateBuilders)
			};
		}

		// Token: 0x04000614 RID: 1556
		private IList<int> _projections;

		// Token: 0x04000615 RID: 1557
		private IList<int> _groupBy;

		// Token: 0x04000616 RID: 1558
		private List<int> _suppressedProjections;

		// Token: 0x04000617 RID: 1559
		private IList<int> _showItemsWithNoData;

		// Token: 0x04000618 RID: 1560
		private SubtotalType _subtotal;

		// Token: 0x04000619 RID: 1561
		private IList<FilterDefinitionBuilder<DataShapeBindingAxisGroupingBuilder<TParent>>> _instanceFilters;

		// Token: 0x0400061A RID: 1562
		private List<DataShapeBindingAggregateBuilder<DataShapeBindingAxisGroupingBuilder<TParent>>> _aggregateBuilders;
	}
}
