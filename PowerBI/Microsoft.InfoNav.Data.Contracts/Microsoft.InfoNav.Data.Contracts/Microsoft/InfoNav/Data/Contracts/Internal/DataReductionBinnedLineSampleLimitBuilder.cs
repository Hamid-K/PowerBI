using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001B1 RID: 433
	public sealed class DataReductionBinnedLineSampleLimitBuilder<TParent> : BaseBindingBuilder<DataReductionBinnedLineSampleLimit, TParent>
	{
		// Token: 0x06000B91 RID: 2961 RVA: 0x00016C36 File Offset: 0x00014E36
		public DataReductionBinnedLineSampleLimitBuilder(TParent parent, int? count, int? minPointsPerSeries, int? maxDynamicSeriesCount, int? primaryScalarKey, int? warningCount)
			: base(parent)
		{
			this._count = count;
			this._minPointsPerSeries = minPointsPerSeries;
			this._maxDynamicSeriesCount = maxDynamicSeriesCount;
			this._primaryScalarKey = primaryScalarKey;
			this._warningCount = warningCount;
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x00016C68 File Offset: 0x00014E68
		public override DataReductionBinnedLineSampleLimit Build()
		{
			return new DataReductionBinnedLineSampleLimit
			{
				Count = this._count,
				MinPointsPerSeries = this._minPointsPerSeries,
				MaxDynamicSeriesCount = this._maxDynamicSeriesCount,
				PrimaryScalarKey = this._primaryScalarKey,
				WarningCount = this._warningCount
			};
		}

		// Token: 0x04000637 RID: 1591
		private int? _count;

		// Token: 0x04000638 RID: 1592
		private int? _minPointsPerSeries;

		// Token: 0x04000639 RID: 1593
		private int? _maxDynamicSeriesCount;

		// Token: 0x0400063A RID: 1594
		private int? _primaryScalarKey;

		// Token: 0x0400063B RID: 1595
		private int? _warningCount;
	}
}
