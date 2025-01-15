using System;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x02000616 RID: 1558
	internal sealed class Literal<T_Identifier> : NormalFormNode<T_Identifier>, IEquatable<Literal<T_Identifier>>
	{
		// Token: 0x06004B9D RID: 19357 RVA: 0x0010AD88 File Offset: 0x00108F88
		internal Literal(TermExpr<T_Identifier> term, bool isTermPositive)
			: base(isTermPositive ? term : new NotExpr<T_Identifier>(term))
		{
			this._term = term;
			this._isTermPositive = isTermPositive;
		}

		// Token: 0x17000EBC RID: 3772
		// (get) Token: 0x06004B9E RID: 19358 RVA: 0x0010ADAA File Offset: 0x00108FAA
		internal TermExpr<T_Identifier> Term
		{
			get
			{
				return this._term;
			}
		}

		// Token: 0x17000EBD RID: 3773
		// (get) Token: 0x06004B9F RID: 19359 RVA: 0x0010ADB2 File Offset: 0x00108FB2
		internal bool IsTermPositive
		{
			get
			{
				return this._isTermPositive;
			}
		}

		// Token: 0x06004BA0 RID: 19360 RVA: 0x0010ADBA File Offset: 0x00108FBA
		internal Literal<T_Identifier> MakeNegated()
		{
			return IdentifierService<T_Identifier>.Instance.NegateLiteral(this);
		}

		// Token: 0x06004BA1 RID: 19361 RVA: 0x0010ADC7 File Offset: 0x00108FC7
		public override string ToString()
		{
			return StringUtil.FormatInvariant("{0}{1}", new object[]
			{
				this._isTermPositive ? string.Empty : "!",
				this._term
			});
		}

		// Token: 0x06004BA2 RID: 19362 RVA: 0x0010ADF9 File Offset: 0x00108FF9
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Literal<T_Identifier>);
		}

		// Token: 0x06004BA3 RID: 19363 RVA: 0x0010AE07 File Offset: 0x00109007
		public bool Equals(Literal<T_Identifier> other)
		{
			return other != null && other._isTermPositive == this._isTermPositive && other._term.Equals(this._term);
		}

		// Token: 0x06004BA4 RID: 19364 RVA: 0x0010AE2D File Offset: 0x0010902D
		public override int GetHashCode()
		{
			return this._term.GetHashCode();
		}

		// Token: 0x04001A71 RID: 6769
		private readonly TermExpr<T_Identifier> _term;

		// Token: 0x04001A72 RID: 6770
		private readonly bool _isTermPositive;
	}
}
