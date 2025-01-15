using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x02000621 RID: 1569
	internal sealed class TermExpr<T_Identifier> : BoolExpr<T_Identifier>, IEquatable<TermExpr<T_Identifier>>
	{
		// Token: 0x06004BDB RID: 19419 RVA: 0x0010B6D0 File Offset: 0x001098D0
		internal TermExpr(IEqualityComparer<T_Identifier> comparer, T_Identifier identifier)
		{
			this._identifier = identifier;
			if (comparer == null)
			{
				this._comparer = EqualityComparer<T_Identifier>.Default;
				return;
			}
			this._comparer = comparer;
		}

		// Token: 0x06004BDC RID: 19420 RVA: 0x0010B6F5 File Offset: 0x001098F5
		internal TermExpr(T_Identifier identifier)
			: this(null, identifier)
		{
		}

		// Token: 0x17000EC2 RID: 3778
		// (get) Token: 0x06004BDD RID: 19421 RVA: 0x0010B6FF File Offset: 0x001098FF
		internal T_Identifier Identifier
		{
			get
			{
				return this._identifier;
			}
		}

		// Token: 0x17000EC3 RID: 3779
		// (get) Token: 0x06004BDE RID: 19422 RVA: 0x0010B707 File Offset: 0x00109907
		internal override ExprType ExprType
		{
			get
			{
				return ExprType.Term;
			}
		}

		// Token: 0x06004BDF RID: 19423 RVA: 0x0010B70A File Offset: 0x0010990A
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TermExpr<T_Identifier>);
		}

		// Token: 0x06004BE0 RID: 19424 RVA: 0x0010B718 File Offset: 0x00109918
		public bool Equals(TermExpr<T_Identifier> other)
		{
			return this._comparer.Equals(this._identifier, other._identifier);
		}

		// Token: 0x06004BE1 RID: 19425 RVA: 0x0010B731 File Offset: 0x00109931
		protected override bool EquivalentTypeEquals(BoolExpr<T_Identifier> other)
		{
			return this._comparer.Equals(this._identifier, ((TermExpr<T_Identifier>)other)._identifier);
		}

		// Token: 0x06004BE2 RID: 19426 RVA: 0x0010B74F File Offset: 0x0010994F
		public override int GetHashCode()
		{
			return this._comparer.GetHashCode(this._identifier);
		}

		// Token: 0x06004BE3 RID: 19427 RVA: 0x0010B762 File Offset: 0x00109962
		public override string ToString()
		{
			return StringUtil.FormatInvariant("{0}", new object[] { this._identifier });
		}

		// Token: 0x06004BE4 RID: 19428 RVA: 0x0010B782 File Offset: 0x00109982
		internal override T_Return Accept<T_Return>(Visitor<T_Identifier, T_Return> visitor)
		{
			return visitor.VisitTerm(this);
		}

		// Token: 0x06004BE5 RID: 19429 RVA: 0x0010B78C File Offset: 0x0010998C
		internal override BoolExpr<T_Identifier> MakeNegated()
		{
			Literal<T_Identifier> literal = new Literal<T_Identifier>(this, true).MakeNegated();
			if (literal.IsTermPositive)
			{
				return literal.Term;
			}
			return new NotExpr<T_Identifier>(literal.Term);
		}

		// Token: 0x04001A80 RID: 6784
		private readonly T_Identifier _identifier;

		// Token: 0x04001A81 RID: 6785
		private readonly IEqualityComparer<T_Identifier> _comparer;
	}
}
