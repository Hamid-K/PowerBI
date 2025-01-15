using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001B5 RID: 437
	public class DataReductionScopeBuilder<TParent> : BaseBindingBuilder<DataReductionScope, TParent>
	{
		// Token: 0x06000B9D RID: 2973 RVA: 0x00016D9F File Offset: 0x00014F9F
		public DataReductionScopeBuilder(TParent parent)
			: base(parent)
		{
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x00016DA8 File Offset: 0x00014FA8
		public DataReductionScopeBuilder<TParent> WithPrimary(params int[] primary)
		{
			this._primary = primary;
			return this;
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x00016DB2 File Offset: 0x00014FB2
		public DataReductionScopeBuilder<TParent> WithSecondary(params int[] secondary)
		{
			this._secondary = secondary;
			return this;
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x00016DBC File Offset: 0x00014FBC
		public override DataReductionScope Build()
		{
			return new DataReductionScope
			{
				Primary = this._primary,
				Secondary = this._secondary
			};
		}

		// Token: 0x04000642 RID: 1602
		private IList<int> _primary;

		// Token: 0x04000643 RID: 1603
		private IList<int> _secondary;
	}
}
