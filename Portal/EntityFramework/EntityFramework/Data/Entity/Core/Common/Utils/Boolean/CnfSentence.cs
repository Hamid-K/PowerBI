using System;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x02000608 RID: 1544
	internal sealed class CnfSentence<T_Identifier> : Sentence<T_Identifier, CnfClause<T_Identifier>>
	{
		// Token: 0x06004B5B RID: 19291 RVA: 0x0010A35D File Offset: 0x0010855D
		internal CnfSentence(Set<CnfClause<T_Identifier>> clauses)
			: base(clauses, ExprType.And)
		{
		}
	}
}
