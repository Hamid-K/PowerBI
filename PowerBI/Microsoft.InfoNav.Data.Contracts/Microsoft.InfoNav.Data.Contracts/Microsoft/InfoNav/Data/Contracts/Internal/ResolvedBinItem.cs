using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001E8 RID: 488
	[ImmutableObject(true)]
	internal sealed class ResolvedBinItem
	{
		// Token: 0x06000D5A RID: 3418 RVA: 0x0001A582 File Offset: 0x00018782
		internal ResolvedBinItem(ResolvedQueryExpression expression)
		{
			this._expression = expression;
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000D5B RID: 3419 RVA: 0x0001A591 File Offset: 0x00018791
		internal ResolvedQueryExpression Expression
		{
			get
			{
				return this._expression;
			}
		}

		// Token: 0x040006CB RID: 1739
		private readonly ResolvedQueryExpression _expression;
	}
}
