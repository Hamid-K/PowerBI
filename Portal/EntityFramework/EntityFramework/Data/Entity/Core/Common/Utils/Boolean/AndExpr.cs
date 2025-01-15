using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x02000602 RID: 1538
	internal class AndExpr<T_Identifier> : TreeExpr<T_Identifier>
	{
		// Token: 0x06004B31 RID: 19249 RVA: 0x0010A043 File Offset: 0x00108243
		internal AndExpr(params BoolExpr<T_Identifier>[] children)
			: this(children)
		{
		}

		// Token: 0x06004B32 RID: 19250 RVA: 0x0010A04C File Offset: 0x0010824C
		internal AndExpr(IEnumerable<BoolExpr<T_Identifier>> children)
			: base(children)
		{
		}

		// Token: 0x17000EAF RID: 3759
		// (get) Token: 0x06004B33 RID: 19251 RVA: 0x0010A055 File Offset: 0x00108255
		internal override ExprType ExprType
		{
			get
			{
				return ExprType.And;
			}
		}

		// Token: 0x06004B34 RID: 19252 RVA: 0x0010A058 File Offset: 0x00108258
		internal override T_Return Accept<T_Return>(Visitor<T_Identifier, T_Return> visitor)
		{
			return visitor.VisitAnd(this);
		}
	}
}
