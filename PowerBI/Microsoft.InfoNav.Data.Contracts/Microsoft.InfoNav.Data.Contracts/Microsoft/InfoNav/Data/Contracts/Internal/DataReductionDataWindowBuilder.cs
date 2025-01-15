using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001B0 RID: 432
	public sealed class DataReductionDataWindowBuilder<TParent> : BaseBindingBuilder<DataReductionDataWindow, TParent>
	{
		// Token: 0x06000B8C RID: 2956 RVA: 0x00016BC0 File Offset: 0x00014DC0
		public DataReductionDataWindowBuilder(TParent parent, int? count)
			: base(parent)
		{
			this._count = count;
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x00016BD0 File Offset: 0x00014DD0
		public DataReductionDataWindowBuilder<TParent> WithCount(int? count)
		{
			this._count = count;
			return this;
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x00016BDA File Offset: 0x00014DDA
		public DataReductionDataWindowBuilder<TParent> WithRestartToken(params string[] token)
		{
			if (this._restartTokens == null)
			{
				this._restartTokens = new List<IList<string>>();
			}
			this._restartTokens.Add(token);
			return this;
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x00016BFC File Offset: 0x00014DFC
		public DataReductionDataWindowBuilder<TParent> WithRestartMatchingBehavior(RestartMatchingBehavior restartMatchingBehavior)
		{
			this._restartMatchingBehavior = new RestartMatchingBehavior?(restartMatchingBehavior);
			return this;
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x00016C0B File Offset: 0x00014E0B
		public override DataReductionDataWindow Build()
		{
			return new DataReductionDataWindow
			{
				Count = this._count,
				RestartTokens = this._restartTokens,
				RestartMatchingBehavior = this._restartMatchingBehavior
			};
		}

		// Token: 0x04000634 RID: 1588
		private int? _count;

		// Token: 0x04000635 RID: 1589
		private List<IList<string>> _restartTokens;

		// Token: 0x04000636 RID: 1590
		private RestartMatchingBehavior? _restartMatchingBehavior;
	}
}
