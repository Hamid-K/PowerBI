using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001B2 RID: 434
	public sealed class DataReductionOverlappingPointSampleLimitBuilder<TParent> : BaseBindingBuilder<DataReductionOverlappingPointsSampleLimit, TParent>
	{
		// Token: 0x06000B93 RID: 2963 RVA: 0x00016CB6 File Offset: 0x00014EB6
		public DataReductionOverlappingPointSampleLimitBuilder(TParent parent)
			: base(parent)
		{
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x00016CBF File Offset: 0x00014EBF
		public DataReductionOverlappingPointSampleLimitBuilder<TParent> WithCount(int? count)
		{
			this._count = count;
			return this;
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x00016CC9 File Offset: 0x00014EC9
		public DataReductionOverlappingPointSampleLimitBuilder<TParent> WithX(int index, DataReductionPlotAxisTransform transform)
		{
			this._x = new DataReductionPlotAxisBinding
			{
				Index = index,
				Transform = transform
			};
			return this;
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x00016CE5 File Offset: 0x00014EE5
		public DataReductionOverlappingPointSampleLimitBuilder<TParent> WithY(int index, DataReductionPlotAxisTransform transform)
		{
			this._y = new DataReductionPlotAxisBinding
			{
				Index = index,
				Transform = transform
			};
			return this;
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x00016D01 File Offset: 0x00014F01
		public override DataReductionOverlappingPointsSampleLimit Build()
		{
			return new DataReductionOverlappingPointsSampleLimit
			{
				Count = this._count,
				X = this._x,
				Y = this._y
			};
		}

		// Token: 0x0400063C RID: 1596
		private int? _count;

		// Token: 0x0400063D RID: 1597
		private DataReductionPlotAxisBinding _x;

		// Token: 0x0400063E RID: 1598
		private DataReductionPlotAxisBinding _y;
	}
}
