using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001AC RID: 428
	public class DataReductionAlgorithmBuilder<TParent> : BaseBindingBuilder<DataReductionAlgorithm, TParent>
	{
		// Token: 0x06000B7B RID: 2939 RVA: 0x000169B1 File Offset: 0x00014BB1
		public DataReductionAlgorithmBuilder(TParent parent)
			: base(parent)
		{
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x000169BA File Offset: 0x00014BBA
		public DataReductionAlgorithmBuilder<TParent> WithTopLimit(int? count)
		{
			this._topLimitBuilder = new DataReductionTopLimitBuilder<DataReductionAlgorithmBuilder<TParent>>(this, count);
			return this;
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x000169CA File Offset: 0x00014BCA
		public DataReductionAlgorithmBuilder<TParent> WithSampleLimit(int? count)
		{
			this._sampleLimitBuilder = new DataReductionSampleLimitBuilder<DataReductionAlgorithmBuilder<TParent>>(this, count);
			return this;
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x000169DA File Offset: 0x00014BDA
		public DataReductionAlgorithmBuilder<TParent> WithBottomLimit(int? count)
		{
			this._bottomLimitBuilder = new DataReductionBottomLimitBuilder<DataReductionAlgorithmBuilder<TParent>>(this, count);
			return this;
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x000169EA File Offset: 0x00014BEA
		public DataReductionAlgorithmBuilder<TParent> WithDataWindow(int? count)
		{
			this._dataWindowBuilder = new DataReductionDataWindowBuilder<DataReductionAlgorithmBuilder<TParent>>(this, count);
			return this;
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x000169FC File Offset: 0x00014BFC
		public DataReductionDataWindowBuilder<DataReductionAlgorithmBuilder<TParent>> WithDataWindow()
		{
			this.WithDataWindow(null);
			return this._dataWindowBuilder;
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x00016A1F File Offset: 0x00014C1F
		public DataReductionAlgorithmBuilder<TParent> WithBinnedLineSampleLimit(int? count, int? minPointsPerSeries, int? maxDynamicSeriesCount, int? primaryScalarKey, int? warningCount)
		{
			this._binnedLineSampleLimitBuilder = new DataReductionBinnedLineSampleLimitBuilder<DataReductionAlgorithmBuilder<TParent>>(this, count, minPointsPerSeries, maxDynamicSeriesCount, primaryScalarKey, warningCount);
			return this;
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x00016A35 File Offset: 0x00014C35
		public DataReductionOverlappingPointSampleLimitBuilder<DataReductionAlgorithmBuilder<TParent>> WithOverlapingPointSampleLimit()
		{
			this._overlappingPointsSampleLimitBuilder = new DataReductionOverlappingPointSampleLimitBuilder<DataReductionAlgorithmBuilder<TParent>>(this);
			return this._overlappingPointsSampleLimitBuilder;
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x00016A49 File Offset: 0x00014C49
		public DataReductionTopNPerLevelSampleLimitBuilder<DataReductionAlgorithmBuilder<TParent>> WithTopNPerLevelSampleLimit(int? count = null, DataReductionWindowExpansionState windowExpansionState = null)
		{
			this._topNPerLevelSampleLimitBuilder = new DataReductionTopNPerLevelSampleLimitBuilder<DataReductionAlgorithmBuilder<TParent>>(this, count, windowExpansionState);
			return this._topNPerLevelSampleLimitBuilder;
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x00016A60 File Offset: 0x00014C60
		public override DataReductionAlgorithm Build()
		{
			DataReductionAlgorithm dataReductionAlgorithm = new DataReductionAlgorithm();
			DataReductionTopLimitBuilder<DataReductionAlgorithmBuilder<TParent>> topLimitBuilder = this._topLimitBuilder;
			dataReductionAlgorithm.Top = ((topLimitBuilder != null) ? topLimitBuilder.Build() : null);
			DataReductionBottomLimitBuilder<DataReductionAlgorithmBuilder<TParent>> bottomLimitBuilder = this._bottomLimitBuilder;
			dataReductionAlgorithm.Bottom = ((bottomLimitBuilder != null) ? bottomLimitBuilder.Build() : null);
			DataReductionSampleLimitBuilder<DataReductionAlgorithmBuilder<TParent>> sampleLimitBuilder = this._sampleLimitBuilder;
			dataReductionAlgorithm.Sample = ((sampleLimitBuilder != null) ? sampleLimitBuilder.Build() : null);
			DataReductionDataWindowBuilder<DataReductionAlgorithmBuilder<TParent>> dataWindowBuilder = this._dataWindowBuilder;
			dataReductionAlgorithm.Window = ((dataWindowBuilder != null) ? dataWindowBuilder.Build() : null);
			DataReductionBinnedLineSampleLimitBuilder<DataReductionAlgorithmBuilder<TParent>> binnedLineSampleLimitBuilder = this._binnedLineSampleLimitBuilder;
			dataReductionAlgorithm.BinnedLineSample = ((binnedLineSampleLimitBuilder != null) ? binnedLineSampleLimitBuilder.Build() : null);
			DataReductionOverlappingPointSampleLimitBuilder<DataReductionAlgorithmBuilder<TParent>> overlappingPointsSampleLimitBuilder = this._overlappingPointsSampleLimitBuilder;
			dataReductionAlgorithm.OverlappingPointsSample = ((overlappingPointsSampleLimitBuilder != null) ? overlappingPointsSampleLimitBuilder.Build() : null);
			DataReductionTopNPerLevelSampleLimitBuilder<DataReductionAlgorithmBuilder<TParent>> topNPerLevelSampleLimitBuilder = this._topNPerLevelSampleLimitBuilder;
			dataReductionAlgorithm.TopNPerLevel = ((topNPerLevelSampleLimitBuilder != null) ? topNPerLevelSampleLimitBuilder.Build() : null);
			return dataReductionAlgorithm;
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x00016B1A File Offset: 0x00014D1A
		private bool IsDataReductionAlgorithmDefined()
		{
			return this._topLimitBuilder != null || this._bottomLimitBuilder != null || this._sampleLimitBuilder != null || this._dataWindowBuilder != null || this._binnedLineSampleLimitBuilder != null || this._overlappingPointsSampleLimitBuilder != null || this._topNPerLevelSampleLimitBuilder != null;
		}

		// Token: 0x0400062A RID: 1578
		private DataReductionTopLimitBuilder<DataReductionAlgorithmBuilder<TParent>> _topLimitBuilder;

		// Token: 0x0400062B RID: 1579
		private DataReductionSampleLimitBuilder<DataReductionAlgorithmBuilder<TParent>> _sampleLimitBuilder;

		// Token: 0x0400062C RID: 1580
		private DataReductionBottomLimitBuilder<DataReductionAlgorithmBuilder<TParent>> _bottomLimitBuilder;

		// Token: 0x0400062D RID: 1581
		private DataReductionDataWindowBuilder<DataReductionAlgorithmBuilder<TParent>> _dataWindowBuilder;

		// Token: 0x0400062E RID: 1582
		private DataReductionBinnedLineSampleLimitBuilder<DataReductionAlgorithmBuilder<TParent>> _binnedLineSampleLimitBuilder;

		// Token: 0x0400062F RID: 1583
		private DataReductionOverlappingPointSampleLimitBuilder<DataReductionAlgorithmBuilder<TParent>> _overlappingPointsSampleLimitBuilder;

		// Token: 0x04000630 RID: 1584
		private DataReductionTopNPerLevelSampleLimitBuilder<DataReductionAlgorithmBuilder<TParent>> _topNPerLevelSampleLimitBuilder;
	}
}
