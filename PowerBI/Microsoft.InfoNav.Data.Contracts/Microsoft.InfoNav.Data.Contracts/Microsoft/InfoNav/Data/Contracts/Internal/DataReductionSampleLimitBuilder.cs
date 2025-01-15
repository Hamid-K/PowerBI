using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001AE RID: 430
	public sealed class DataReductionSampleLimitBuilder<TParent> : BaseBindingBuilder<DataReductionSampleLimit, TParent>
	{
		// Token: 0x06000B88 RID: 2952 RVA: 0x00016B7A File Offset: 0x00014D7A
		public DataReductionSampleLimitBuilder(TParent parent, int? count)
			: base(parent)
		{
			this._count = count;
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00016B8A File Offset: 0x00014D8A
		public override DataReductionSampleLimit Build()
		{
			return new DataReductionSampleLimit
			{
				Count = this._count
			};
		}

		// Token: 0x04000632 RID: 1586
		private int? _count;
	}
}
