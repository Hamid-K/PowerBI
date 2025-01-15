using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000069 RID: 105
	internal sealed class GroupExpression
	{
		// Token: 0x0600021F RID: 543 RVA: 0x0000B97C File Offset: 0x00009B7C
		internal GroupExpression(Expression expression)
		{
			this._expression = expression;
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000220 RID: 544 RVA: 0x0000B98B File Offset: 0x00009B8B
		internal Expression Expression
		{
			get
			{
				return this._expression;
			}
		}

		// Token: 0x0400017B RID: 379
		private readonly Expression _expression;
	}
}
