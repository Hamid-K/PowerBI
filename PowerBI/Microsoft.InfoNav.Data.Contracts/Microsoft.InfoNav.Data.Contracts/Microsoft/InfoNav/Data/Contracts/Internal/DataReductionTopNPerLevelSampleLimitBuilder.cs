using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001B3 RID: 435
	public sealed class DataReductionTopNPerLevelSampleLimitBuilder<TParent> : BaseBindingBuilder<DataReductionTopNPerLevelSampleLimit, TParent>
	{
		// Token: 0x06000B98 RID: 2968 RVA: 0x00016D2C File Offset: 0x00014F2C
		public DataReductionTopNPerLevelSampleLimitBuilder(TParent parent, int? count, DataReductionWindowExpansionState windowExpansionState = null)
			: base(parent)
		{
			this._count = count;
			this._windowExpansionState = windowExpansionState;
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x00016D43 File Offset: 0x00014F43
		public DataReductionTopNPerLevelSampleLimitBuilder<TParent> WithCount(int? count)
		{
			this._count = count;
			return this;
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x00016D4D File Offset: 0x00014F4D
		public DataReductionWindowExpansionStateBuilder<DataReductionTopNPerLevelSampleLimitBuilder<TParent>> WithWindowExpansionState()
		{
			this._windowExpansionStateBuilder = new DataReductionWindowExpansionStateBuilder<DataReductionTopNPerLevelSampleLimitBuilder<TParent>>(this);
			return this._windowExpansionStateBuilder;
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x00016D61 File Offset: 0x00014F61
		public override DataReductionTopNPerLevelSampleLimit Build()
		{
			DataReductionTopNPerLevelSampleLimit dataReductionTopNPerLevelSampleLimit = new DataReductionTopNPerLevelSampleLimit();
			dataReductionTopNPerLevelSampleLimit.Count = this._count;
			DataReductionWindowExpansionState dataReductionWindowExpansionState;
			if ((dataReductionWindowExpansionState = this._windowExpansionState) == null)
			{
				DataReductionWindowExpansionStateBuilder<DataReductionTopNPerLevelSampleLimitBuilder<TParent>> windowExpansionStateBuilder = this._windowExpansionStateBuilder;
				dataReductionWindowExpansionState = ((windowExpansionStateBuilder != null) ? windowExpansionStateBuilder.Build() : null);
			}
			dataReductionTopNPerLevelSampleLimit.WindowExpansion = dataReductionWindowExpansionState;
			return dataReductionTopNPerLevelSampleLimit;
		}

		// Token: 0x0400063F RID: 1599
		private int? _count;

		// Token: 0x04000640 RID: 1600
		private DataReductionWindowExpansionState _windowExpansionState;

		// Token: 0x04000641 RID: 1601
		private DataReductionWindowExpansionStateBuilder<DataReductionTopNPerLevelSampleLimitBuilder<TParent>> _windowExpansionStateBuilder;
	}
}
