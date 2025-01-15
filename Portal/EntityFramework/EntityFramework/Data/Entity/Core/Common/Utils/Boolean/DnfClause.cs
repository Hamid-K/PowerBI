using System;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x0200060B RID: 1547
	internal sealed class DnfClause<T_Identifier> : Clause<T_Identifier>, IEquatable<DnfClause<T_Identifier>>
	{
		// Token: 0x06004B65 RID: 19301 RVA: 0x0010A594 File Offset: 0x00108794
		internal DnfClause(Set<Literal<T_Identifier>> literals)
			: base(literals, ExprType.And)
		{
		}

		// Token: 0x06004B66 RID: 19302 RVA: 0x0010A59E File Offset: 0x0010879E
		public bool Equals(DnfClause<T_Identifier> other)
		{
			return other != null && other.Literals.SetEquals(base.Literals);
		}
	}
}
