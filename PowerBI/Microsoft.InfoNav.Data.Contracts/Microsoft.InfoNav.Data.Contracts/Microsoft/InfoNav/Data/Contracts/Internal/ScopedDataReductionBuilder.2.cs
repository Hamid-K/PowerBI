using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001B7 RID: 439
	public class ScopedDataReductionBuilder<TParent> : BaseBindingBuilder<ScopedDataReduction, TParent>
	{
		// Token: 0x06000BA2 RID: 2978 RVA: 0x00016DE4 File Offset: 0x00014FE4
		public ScopedDataReductionBuilder(TParent parent)
			: base(parent)
		{
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x00016DED File Offset: 0x00014FED
		public DataReductionScopeBuilder<ScopedDataReductionBuilder<TParent>> WithDataReductionScope()
		{
			this._scopeBuilder = new DataReductionScopeBuilder<ScopedDataReductionBuilder<TParent>>(this);
			return this._scopeBuilder;
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x00016E01 File Offset: 0x00015001
		public DataReductionAlgorithmBuilder<ScopedDataReductionBuilder<TParent>> WithAlgorithm()
		{
			this._algorithmBuilder = new DataReductionAlgorithmBuilder<ScopedDataReductionBuilder<TParent>>(this);
			return this._algorithmBuilder;
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x00016E15 File Offset: 0x00015015
		public override ScopedDataReduction Build()
		{
			return new ScopedDataReduction
			{
				Scope = this._scopeBuilder.Build(),
				Algorithm = this._algorithmBuilder.Build()
			};
		}

		// Token: 0x04000644 RID: 1604
		private DataReductionScopeBuilder<ScopedDataReductionBuilder<TParent>> _scopeBuilder;

		// Token: 0x04000645 RID: 1605
		private DataReductionAlgorithmBuilder<ScopedDataReductionBuilder<TParent>> _algorithmBuilder;
	}
}
