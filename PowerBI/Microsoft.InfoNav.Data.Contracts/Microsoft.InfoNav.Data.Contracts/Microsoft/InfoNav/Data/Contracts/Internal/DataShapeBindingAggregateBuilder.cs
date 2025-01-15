using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000197 RID: 407
	public sealed class DataShapeBindingAggregateBuilder<TParent> : BaseBindingBuilder<DataShapeBindingAggregate, TParent>
	{
		// Token: 0x06000B00 RID: 2816 RVA: 0x00015BA4 File Offset: 0x00013DA4
		public DataShapeBindingAggregateBuilder(TParent parent, int selectIndex)
			: base(parent)
		{
			this._selectIndex = selectIndex;
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x00015BB4 File Offset: 0x00013DB4
		public DataShapeBindingAggregateContainerBuilder<DataShapeBindingAggregateBuilder<TParent>> WithAggregation(int? scopePrimaryDepth = null, int? scopeSecondaryDepth = null, bool respectInstanceFilters = true)
		{
			DataShapeBindingAggregateContainerBuilder<DataShapeBindingAggregateBuilder<TParent>> dataShapeBindingAggregateContainerBuilder = new DataShapeBindingAggregateContainerBuilder<DataShapeBindingAggregateBuilder<TParent>>(this, scopePrimaryDepth, scopeSecondaryDepth, respectInstanceFilters);
			BaseBindingBuilder<DataShapeBindingAggregate, TParent>.AddToLazyList<DataShapeBindingAggregateContainerBuilder<DataShapeBindingAggregateBuilder<TParent>>>(ref this._aggregationBuilders, dataShapeBindingAggregateContainerBuilder);
			return dataShapeBindingAggregateContainerBuilder;
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x00015BD8 File Offset: 0x00013DD8
		public override DataShapeBindingAggregate Build()
		{
			return new DataShapeBindingAggregate
			{
				Select = this._selectIndex,
				Aggregations = BaseBindingBuilder<DataShapeBindingAggregate, TParent>.SafeBuild<DataShapeBindingAggregateContainer, DataShapeBindingAggregateContainerBuilder<DataShapeBindingAggregateBuilder<TParent>>, DataShapeBindingAggregateBuilder<TParent>>(this._aggregationBuilders)
			};
		}

		// Token: 0x040005EC RID: 1516
		private IList<DataShapeBindingAggregateContainerBuilder<DataShapeBindingAggregateBuilder<TParent>>> _aggregationBuilders;

		// Token: 0x040005ED RID: 1517
		private int _selectIndex;
	}
}
