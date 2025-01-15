using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x0200061B RID: 1563
	internal class OrExpr<T_Identifier> : TreeExpr<T_Identifier>
	{
		// Token: 0x06004BB0 RID: 19376 RVA: 0x0010AED9 File Offset: 0x001090D9
		internal OrExpr(params BoolExpr<T_Identifier>[] children)
			: this(children)
		{
		}

		// Token: 0x06004BB1 RID: 19377 RVA: 0x0010AEE2 File Offset: 0x001090E2
		internal OrExpr(IEnumerable<BoolExpr<T_Identifier>> children)
			: base(children)
		{
		}

		// Token: 0x17000EC1 RID: 3777
		// (get) Token: 0x06004BB2 RID: 19378 RVA: 0x0010AEEB File Offset: 0x001090EB
		internal override ExprType ExprType
		{
			get
			{
				return ExprType.Or;
			}
		}

		// Token: 0x06004BB3 RID: 19379 RVA: 0x0010AEEE File Offset: 0x001090EE
		internal override T_Return Accept<T_Return>(Visitor<T_Identifier, T_Return> visitor)
		{
			return visitor.VisitOr(this);
		}
	}
}
