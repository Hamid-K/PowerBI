using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001AA RID: 426
	public class DataReductionBuilder<TParent> : BaseBindingBuilder<DataReduction, TParent>
	{
		// Token: 0x06000B74 RID: 2932 RVA: 0x00016882 File Offset: 0x00014A82
		public DataReductionBuilder(TParent parent, int? dataVolume)
			: base(parent)
		{
			this._dataVolume = dataVolume;
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x00016892 File Offset: 0x00014A92
		public DataReductionAlgorithmBuilder<DataReductionBuilder<TParent>> WithPrimaryGroupAlgorithm()
		{
			this._primaryGroupAlgorithm = new DataReductionAlgorithmBuilder<DataReductionBuilder<TParent>>(this);
			return this._primaryGroupAlgorithm;
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x000168A6 File Offset: 0x00014AA6
		public DataReductionAlgorithmBuilder<DataReductionBuilder<TParent>> WithSecondaryGroupAlgorithm()
		{
			this._secondaryGroupAlgorithm = new DataReductionAlgorithmBuilder<DataReductionBuilder<TParent>>(this);
			return this._secondaryGroupAlgorithm;
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x000168BA File Offset: 0x00014ABA
		public DataReductionAlgorithmBuilder<DataReductionBuilder<TParent>> WithIntersectionAlgorithm()
		{
			this._intersectionAlgorithm = new DataReductionAlgorithmBuilder<DataReductionBuilder<TParent>>(this);
			return this._intersectionAlgorithm;
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x000168D0 File Offset: 0x00014AD0
		public ScopedDataReductionBuilder<DataReductionBuilder<TParent>> WithScopedDataReduction()
		{
			ScopedDataReductionBuilder<DataReductionBuilder<TParent>> scopedDataReductionBuilder = new ScopedDataReductionBuilder<DataReductionBuilder<TParent>>(this);
			BaseBindingBuilder<DataReduction, TParent>.AddToLazyList<ScopedDataReductionBuilder<DataReductionBuilder<TParent>>>(ref this._scoped, scopedDataReductionBuilder);
			return scopedDataReductionBuilder;
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x000168F4 File Offset: 0x00014AF4
		public override DataReduction Build()
		{
			List<ScopedDataReduction> list = null;
			if (!this._scoped.IsNullOrEmptyCollection<ScopedDataReductionBuilder<DataReductionBuilder<TParent>>>())
			{
				list = new List<ScopedDataReduction>();
				foreach (ScopedDataReductionBuilder<DataReductionBuilder<TParent>> scopedDataReductionBuilder in this._scoped)
				{
					list.Add(scopedDataReductionBuilder.Build());
				}
			}
			return new DataReduction
			{
				DataVolume = this._dataVolume,
				Primary = BaseBindingBuilder<DataReduction, TParent>.SafeBuild<DataReductionAlgorithm, DataReductionBuilder<TParent>>(this._primaryGroupAlgorithm),
				Secondary = BaseBindingBuilder<DataReduction, TParent>.SafeBuild<DataReductionAlgorithm, DataReductionBuilder<TParent>>(this._secondaryGroupAlgorithm),
				Intersection = BaseBindingBuilder<DataReduction, TParent>.SafeBuild<DataReductionAlgorithm, DataReductionBuilder<TParent>>(this._intersectionAlgorithm),
				Scoped = list
			};
		}

		// Token: 0x04000624 RID: 1572
		public const int DefaultDataVolume = 3;

		// Token: 0x04000625 RID: 1573
		private int? _dataVolume;

		// Token: 0x04000626 RID: 1574
		private DataReductionAlgorithmBuilder<DataReductionBuilder<TParent>> _primaryGroupAlgorithm;

		// Token: 0x04000627 RID: 1575
		private DataReductionAlgorithmBuilder<DataReductionBuilder<TParent>> _secondaryGroupAlgorithm;

		// Token: 0x04000628 RID: 1576
		private DataReductionAlgorithmBuilder<DataReductionBuilder<TParent>> _intersectionAlgorithm;

		// Token: 0x04000629 RID: 1577
		private IList<ScopedDataReductionBuilder<DataReductionBuilder<TParent>>> _scoped;
	}
}
