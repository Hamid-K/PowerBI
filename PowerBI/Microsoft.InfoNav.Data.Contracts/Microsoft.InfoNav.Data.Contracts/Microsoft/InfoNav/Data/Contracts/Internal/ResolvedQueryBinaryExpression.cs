using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000214 RID: 532
	[ImmutableObject(true)]
	public abstract class ResolvedQueryBinaryExpression : ResolvedQueryExpression
	{
		// Token: 0x06000F6A RID: 3946 RVA: 0x0001D8EF File Offset: 0x0001BAEF
		protected ResolvedQueryBinaryExpression(ResolvedQueryExpression left, ResolvedQueryExpression right)
		{
			this._left = left;
			this._right = right;
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000F6B RID: 3947 RVA: 0x0001D905 File Offset: 0x0001BB05
		public ResolvedQueryExpression Left
		{
			get
			{
				return this._left;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000F6C RID: 3948 RVA: 0x0001D90D File Offset: 0x0001BB0D
		public ResolvedQueryExpression Right
		{
			get
			{
				return this._right;
			}
		}

		// Token: 0x04000728 RID: 1832
		private readonly ResolvedQueryExpression _left;

		// Token: 0x04000729 RID: 1833
		private readonly ResolvedQueryExpression _right;
	}
}
