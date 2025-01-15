using System;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x0200060C RID: 1548
	internal sealed class DnfSentence<T_Identifier> : Sentence<T_Identifier, DnfClause<T_Identifier>>
	{
		// Token: 0x06004B67 RID: 19303 RVA: 0x0010A5B6 File Offset: 0x001087B6
		internal DnfSentence(Set<DnfClause<T_Identifier>> clauses)
			: base(clauses, ExprType.Or)
		{
		}
	}
}
