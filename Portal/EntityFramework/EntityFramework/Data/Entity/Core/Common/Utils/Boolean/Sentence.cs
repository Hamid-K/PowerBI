using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x0200061C RID: 1564
	internal abstract class Sentence<T_Identifier, T_Clause> : NormalFormNode<T_Identifier> where T_Clause : Clause<T_Identifier>, IEquatable<T_Clause>
	{
		// Token: 0x06004BB4 RID: 19380 RVA: 0x0010AEF7 File Offset: 0x001090F7
		protected Sentence(Set<T_Clause> clauses, ExprType treeType)
			: base(Sentence<T_Identifier, T_Clause>.ConvertClausesToExpr(clauses, treeType))
		{
			this._clauses = clauses.AsReadOnly();
		}

		// Token: 0x06004BB5 RID: 19381 RVA: 0x0010AF14 File Offset: 0x00109114
		private static BoolExpr<T_Identifier> ConvertClausesToExpr(Set<T_Clause> clauses, ExprType treeType)
		{
			bool flag = treeType == ExprType.And;
			IEnumerable<BoolExpr<T_Identifier>> enumerable = clauses.Select(new Func<T_Clause, BoolExpr<T_Identifier>>(NormalFormNode<T_Identifier>.ExprSelector<T_Clause>));
			if (flag)
			{
				return new AndExpr<T_Identifier>(enumerable);
			}
			return new OrExpr<T_Identifier>(enumerable);
		}

		// Token: 0x06004BB6 RID: 19382 RVA: 0x0010AF47 File Offset: 0x00109147
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Sentence{");
			stringBuilder.Append(this._clauses);
			return stringBuilder.Append("}").ToString();
		}

		// Token: 0x04001A76 RID: 6774
		private readonly Set<T_Clause> _clauses;
	}
}
