using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x02000605 RID: 1541
	internal abstract class BoolExpr<T_Identifier> : IEquatable<BoolExpr<T_Identifier>>
	{
		// Token: 0x17000EB0 RID: 3760
		// (get) Token: 0x06004B45 RID: 19269
		internal abstract ExprType ExprType { get; }

		// Token: 0x06004B46 RID: 19270
		internal abstract T_Return Accept<T_Return>(Visitor<T_Identifier, T_Return> visitor);

		// Token: 0x06004B47 RID: 19271 RVA: 0x0010A133 File Offset: 0x00108333
		internal BoolExpr<T_Identifier> Simplify()
		{
			return IdentifierService<T_Identifier>.Instance.LocalSimplify(this);
		}

		// Token: 0x06004B48 RID: 19272 RVA: 0x0010A140 File Offset: 0x00108340
		internal BoolExpr<T_Identifier> ExpensiveSimplify(out Converter<T_Identifier> converter)
		{
			ConversionContext<T_Identifier> conversionContext = IdentifierService<T_Identifier>.Instance.CreateConversionContext();
			converter = new Converter<T_Identifier>(this, conversionContext);
			if (converter.Vertex.IsOne())
			{
				return TrueExpr<T_Identifier>.Value;
			}
			if (converter.Vertex.IsZero())
			{
				return FalseExpr<T_Identifier>.Value;
			}
			return BoolExpr<T_Identifier>.ChooseCandidate(new BoolExpr<T_Identifier>[]
			{
				this,
				converter.Cnf.Expr,
				converter.Dnf.Expr
			});
		}

		// Token: 0x06004B49 RID: 19273 RVA: 0x0010A1B8 File Offset: 0x001083B8
		private static BoolExpr<T_Identifier> ChooseCandidate(params BoolExpr<T_Identifier>[] candidates)
		{
			int num = 0;
			int num2 = 0;
			BoolExpr<T_Identifier> boolExpr = null;
			for (int i = 0; i < candidates.Length; i++)
			{
				BoolExpr<T_Identifier> boolExpr2 = candidates[i].Simplify();
				int num3 = boolExpr2.GetTerms().Distinct<TermExpr<T_Identifier>>().Count<TermExpr<T_Identifier>>();
				int num4 = boolExpr2.CountTerms();
				if (boolExpr == null || num3 < num || (num3 == num && num4 < num2))
				{
					boolExpr = boolExpr2;
					num = num3;
					num2 = num4;
				}
			}
			return boolExpr;
		}

		// Token: 0x06004B4A RID: 19274 RVA: 0x0010A222 File Offset: 0x00108422
		internal List<TermExpr<T_Identifier>> GetTerms()
		{
			return LeafVisitor<T_Identifier>.GetTerms(this);
		}

		// Token: 0x06004B4B RID: 19275 RVA: 0x0010A22A File Offset: 0x0010842A
		internal int CountTerms()
		{
			return TermCounter<T_Identifier>.CountTerms(this);
		}

		// Token: 0x06004B4C RID: 19276 RVA: 0x0010A232 File Offset: 0x00108432
		public static implicit operator BoolExpr<T_Identifier>(T_Identifier value)
		{
			return new TermExpr<T_Identifier>(value);
		}

		// Token: 0x06004B4D RID: 19277 RVA: 0x0010A23A File Offset: 0x0010843A
		internal virtual BoolExpr<T_Identifier> MakeNegated()
		{
			return new NotExpr<T_Identifier>(this);
		}

		// Token: 0x06004B4E RID: 19278 RVA: 0x0010A244 File Offset: 0x00108444
		public override string ToString()
		{
			return this.ExprType.ToString();
		}

		// Token: 0x06004B4F RID: 19279 RVA: 0x0010A265 File Offset: 0x00108465
		public bool Equals(BoolExpr<T_Identifier> other)
		{
			return other != null && this.ExprType == other.ExprType && this.EquivalentTypeEquals(other);
		}

		// Token: 0x06004B50 RID: 19280
		protected abstract bool EquivalentTypeEquals(BoolExpr<T_Identifier> other);
	}
}
