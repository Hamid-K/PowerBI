using System;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x02000607 RID: 1543
	internal sealed class CnfClause<T_Identifier> : Clause<T_Identifier>, IEquatable<CnfClause<T_Identifier>>
	{
		// Token: 0x06004B59 RID: 19289 RVA: 0x0010A33B File Offset: 0x0010853B
		internal CnfClause(Set<Literal<T_Identifier>> literals)
			: base(literals, ExprType.Or)
		{
		}

		// Token: 0x06004B5A RID: 19290 RVA: 0x0010A345 File Offset: 0x00108545
		public bool Equals(CnfClause<T_Identifier> other)
		{
			return other != null && other.Literals.SetEquals(base.Literals);
		}
	}
}
