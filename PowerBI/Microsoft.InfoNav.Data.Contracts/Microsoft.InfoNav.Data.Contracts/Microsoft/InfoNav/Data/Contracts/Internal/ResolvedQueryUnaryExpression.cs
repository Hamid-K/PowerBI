using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000263 RID: 611
	[ImmutableObject(true)]
	public abstract class ResolvedQueryUnaryExpression : ResolvedQueryExpression
	{
		// Token: 0x06001255 RID: 4693 RVA: 0x000202CA File Offset: 0x0001E4CA
		protected ResolvedQueryUnaryExpression(ResolvedQueryExpression expression)
		{
			this._expr = expression;
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06001256 RID: 4694 RVA: 0x000202D9 File Offset: 0x0001E4D9
		public ResolvedQueryExpression Expression
		{
			get
			{
				return this._expr;
			}
		}

		// Token: 0x040007C1 RID: 1985
		private readonly ResolvedQueryExpression _expr;
	}
}
