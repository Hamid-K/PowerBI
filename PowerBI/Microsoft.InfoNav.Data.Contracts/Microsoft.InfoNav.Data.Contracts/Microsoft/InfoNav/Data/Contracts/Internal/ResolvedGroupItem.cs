using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001EB RID: 491
	[ImmutableObject(true)]
	internal sealed class ResolvedGroupItem
	{
		// Token: 0x06000D63 RID: 3427 RVA: 0x0001A605 File Offset: 0x00018805
		internal ResolvedGroupItem(string displayName, ResolvedQueryExpression expression, bool blankDefaultPlaceholder)
		{
			this._displayName = displayName;
			this._expression = expression;
			this._blankDefaultPlaceholder = blankDefaultPlaceholder;
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000D64 RID: 3428 RVA: 0x0001A622 File Offset: 0x00018822
		internal string DisplayName
		{
			get
			{
				return this._displayName;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000D65 RID: 3429 RVA: 0x0001A62A File Offset: 0x0001882A
		internal ResolvedQueryExpression Expression
		{
			get
			{
				return this._expression;
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000D66 RID: 3430 RVA: 0x0001A632 File Offset: 0x00018832
		internal bool BlankDefaultPlaceholder
		{
			get
			{
				return this._blankDefaultPlaceholder;
			}
		}

		// Token: 0x040006D5 RID: 1749
		private readonly string _displayName;

		// Token: 0x040006D6 RID: 1750
		private readonly ResolvedQueryExpression _expression;

		// Token: 0x040006D7 RID: 1751
		private readonly bool _blankDefaultPlaceholder;
	}
}
