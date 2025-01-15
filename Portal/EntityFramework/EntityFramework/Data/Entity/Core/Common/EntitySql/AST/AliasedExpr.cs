using System;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000671 RID: 1649
	internal sealed class AliasedExpr : Node
	{
		// Token: 0x06004EEE RID: 20206 RVA: 0x0011F548 File Offset: 0x0011D748
		internal AliasedExpr(Node expr, Identifier alias)
		{
			if (string.IsNullOrEmpty(alias.Name))
			{
				ErrorContext errCtx = alias.ErrCtx;
				string invalidEmptyIdentifier = Strings.InvalidEmptyIdentifier;
				throw EntitySqlException.Create(errCtx, invalidEmptyIdentifier, null);
			}
			this._expr = expr;
			this._alias = alias;
		}

		// Token: 0x06004EEF RID: 20207 RVA: 0x0011F58A File Offset: 0x0011D78A
		internal AliasedExpr(Node expr)
		{
			this._expr = expr;
		}

		// Token: 0x17000F32 RID: 3890
		// (get) Token: 0x06004EF0 RID: 20208 RVA: 0x0011F599 File Offset: 0x0011D799
		internal Node Expr
		{
			get
			{
				return this._expr;
			}
		}

		// Token: 0x17000F33 RID: 3891
		// (get) Token: 0x06004EF1 RID: 20209 RVA: 0x0011F5A1 File Offset: 0x0011D7A1
		internal Identifier Alias
		{
			get
			{
				return this._alias;
			}
		}

		// Token: 0x04001C87 RID: 7303
		private readonly Node _expr;

		// Token: 0x04001C88 RID: 7304
		private readonly Identifier _alias;
	}
}
