using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000198 RID: 408
	public sealed class DataShapeBindingAggregateContainerBuilder<TParent> : BaseBindingBuilder<DataShapeBindingAggregateContainer, TParent>
	{
		// Token: 0x06000B03 RID: 2819 RVA: 0x00015BFC File Offset: 0x00013DFC
		public DataShapeBindingAggregateContainerBuilder(TParent parent, int? scopePrimaryDepth, int? scopeSecondaryDepth, bool respectInstanceFilters)
			: base(parent)
		{
			if (scopePrimaryDepth != null)
			{
				this._scope = new AggregateScope
				{
					PrimaryDepth = scopePrimaryDepth.Value,
					SecondaryDepth = scopeSecondaryDepth
				};
				this._respectInstanceFilters = respectInstanceFilters;
			}
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x00015C35 File Offset: 0x00013E35
		public DataShapeBindingAggregateContainerBuilder<TParent> WithMin(IncludeAllTypes includeAllTypes = IncludeAllTypes.Default)
		{
			this._min = new DataShapeBindingMinAggregate(includeAllTypes);
			return this;
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x00015C44 File Offset: 0x00013E44
		public DataShapeBindingAggregateContainerBuilder<TParent> WithMax(IncludeAllTypes includeAllTypes = IncludeAllTypes.Default)
		{
			this._max = new DataShapeBindingMaxAggregate(includeAllTypes);
			return this;
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00015C53 File Offset: 0x00013E53
		public DataShapeBindingAggregateContainerBuilder<TParent> WithMedian()
		{
			this._median = DataShapeBindingMedianAggregate.ContainerInstance.Median;
			return this;
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x00015C66 File Offset: 0x00013E66
		public DataShapeBindingAggregateContainerBuilder<TParent> WithAverage()
		{
			this._average = DataShapeBindingAverageAggregate.ContainerInstance.Average;
			return this;
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x00015C79 File Offset: 0x00013E79
		public DataShapeBindingAggregateContainerBuilder<TParent> WithPercentile(double k, bool exclusive = false)
		{
			this._percentile = new DataShapeBindingPercentileAggregate(k, exclusive);
			return this;
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x00015C8C File Offset: 0x00013E8C
		public override DataShapeBindingAggregateContainer Build()
		{
			return new DataShapeBindingAggregateContainer
			{
				Min = this._min,
				Max = this._max,
				Median = this._median,
				Average = this._average,
				Percentile = this._percentile,
				Scope = this._scope,
				RespectInstanceFilters = this._respectInstanceFilters
			};
		}

		// Token: 0x040005EE RID: 1518
		private DataShapeBindingPercentileAggregate _percentile;

		// Token: 0x040005EF RID: 1519
		private DataShapeBindingMedianAggregate _median;

		// Token: 0x040005F0 RID: 1520
		private DataShapeBindingMinAggregate _min;

		// Token: 0x040005F1 RID: 1521
		private DataShapeBindingMaxAggregate _max;

		// Token: 0x040005F2 RID: 1522
		private DataShapeBindingAverageAggregate _average;

		// Token: 0x040005F3 RID: 1523
		private AggregateScope _scope;

		// Token: 0x040005F4 RID: 1524
		private bool _respectInstanceFilters;
	}
}
