using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x02000606 RID: 1542
	internal abstract class Clause<T_Identifier> : NormalFormNode<T_Identifier>
	{
		// Token: 0x06004B52 RID: 19282 RVA: 0x0010A289 File Offset: 0x00108489
		protected Clause(Set<Literal<T_Identifier>> literals, ExprType treeType)
			: base(Clause<T_Identifier>.ConvertLiteralsToExpr(literals, treeType))
		{
			this._literals = literals.AsReadOnly();
			this._hashCode = this._literals.GetElementsHashCode();
		}

		// Token: 0x17000EB1 RID: 3761
		// (get) Token: 0x06004B53 RID: 19283 RVA: 0x0010A2B5 File Offset: 0x001084B5
		internal Set<Literal<T_Identifier>> Literals
		{
			get
			{
				return this._literals;
			}
		}

		// Token: 0x06004B54 RID: 19284 RVA: 0x0010A2C0 File Offset: 0x001084C0
		private static BoolExpr<T_Identifier> ConvertLiteralsToExpr(Set<Literal<T_Identifier>> literals, ExprType treeType)
		{
			bool flag = treeType == ExprType.And;
			IEnumerable<BoolExpr<T_Identifier>> enumerable = literals.Select(new Func<Literal<T_Identifier>, BoolExpr<T_Identifier>>(Clause<T_Identifier>.ConvertLiteralToExpression));
			if (flag)
			{
				return new AndExpr<T_Identifier>(enumerable);
			}
			return new OrExpr<T_Identifier>(enumerable);
		}

		// Token: 0x06004B55 RID: 19285 RVA: 0x0010A2F3 File Offset: 0x001084F3
		private static BoolExpr<T_Identifier> ConvertLiteralToExpression(Literal<T_Identifier> literal)
		{
			return literal.Expr;
		}

		// Token: 0x06004B56 RID: 19286 RVA: 0x0010A2FB File Offset: 0x001084FB
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Clause{");
			stringBuilder.Append(this._literals);
			return stringBuilder.Append("}").ToString();
		}

		// Token: 0x06004B57 RID: 19287 RVA: 0x0010A32A File Offset: 0x0010852A
		public override int GetHashCode()
		{
			return this._hashCode;
		}

		// Token: 0x06004B58 RID: 19288 RVA: 0x0010A332 File Offset: 0x00108532
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x04001A52 RID: 6738
		private readonly Set<Literal<T_Identifier>> _literals;

		// Token: 0x04001A53 RID: 6739
		private readonly int _hashCode;
	}
}
