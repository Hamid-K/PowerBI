using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001AF RID: 431
	public sealed class DataReductionBottomLimitBuilder<TParent> : BaseBindingBuilder<DataReductionBottomLimit, TParent>
	{
		// Token: 0x06000B8A RID: 2954 RVA: 0x00016B9D File Offset: 0x00014D9D
		public DataReductionBottomLimitBuilder(TParent parent, int? count)
			: base(parent)
		{
			this._count = count;
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x00016BAD File Offset: 0x00014DAD
		public override DataReductionBottomLimit Build()
		{
			return new DataReductionBottomLimit
			{
				Count = this._count
			};
		}

		// Token: 0x04000633 RID: 1587
		private int? _count;
	}
}
