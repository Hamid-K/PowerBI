using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001AD RID: 429
	public sealed class DataReductionTopLimitBuilder<TParent> : BaseBindingBuilder<DataReductionTopLimit, TParent>
	{
		// Token: 0x06000B86 RID: 2950 RVA: 0x00016B57 File Offset: 0x00014D57
		public DataReductionTopLimitBuilder(TParent parent, int? count)
			: base(parent)
		{
			this._count = count;
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x00016B67 File Offset: 0x00014D67
		public override DataReductionTopLimit Build()
		{
			return new DataReductionTopLimit
			{
				Count = this._count
			};
		}

		// Token: 0x04000631 RID: 1585
		private int? _count;
	}
}
