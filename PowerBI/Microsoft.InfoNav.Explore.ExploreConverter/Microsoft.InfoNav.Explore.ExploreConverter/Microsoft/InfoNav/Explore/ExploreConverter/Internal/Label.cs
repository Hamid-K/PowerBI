using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200006E RID: 110
	internal sealed class Label
	{
		// Token: 0x06000236 RID: 566 RVA: 0x0000B9DF File Offset: 0x00009BDF
		internal Label(Expression expression)
		{
			this._expression = expression;
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000B9EE File Offset: 0x00009BEE
		internal Expression Expression
		{
			get
			{
				return this._expression;
			}
		}

		// Token: 0x0400017F RID: 383
		private readonly Expression _expression;
	}
}
