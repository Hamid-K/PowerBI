using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000680 RID: 1664
	internal sealed class FromClauseItem : Node
	{
		// Token: 0x06004F1C RID: 20252 RVA: 0x0011F93B File Offset: 0x0011DB3B
		internal FromClauseItem(AliasedExpr aliasExpr)
		{
			this._fromClauseItemExpr = aliasExpr;
			this._fromClauseItemKind = FromClauseItemKind.AliasedFromClause;
		}

		// Token: 0x06004F1D RID: 20253 RVA: 0x0011F951 File Offset: 0x0011DB51
		internal FromClauseItem(JoinClauseItem joinClauseItem)
		{
			this._fromClauseItemExpr = joinClauseItem;
			this._fromClauseItemKind = FromClauseItemKind.JoinFromClause;
		}

		// Token: 0x06004F1E RID: 20254 RVA: 0x0011F967 File Offset: 0x0011DB67
		internal FromClauseItem(ApplyClauseItem applyClauseItem)
		{
			this._fromClauseItemExpr = applyClauseItem;
			this._fromClauseItemKind = FromClauseItemKind.ApplyFromClause;
		}

		// Token: 0x17000F46 RID: 3910
		// (get) Token: 0x06004F1F RID: 20255 RVA: 0x0011F97D File Offset: 0x0011DB7D
		internal Node FromExpr
		{
			get
			{
				return this._fromClauseItemExpr;
			}
		}

		// Token: 0x17000F47 RID: 3911
		// (get) Token: 0x06004F20 RID: 20256 RVA: 0x0011F985 File Offset: 0x0011DB85
		internal FromClauseItemKind FromClauseItemKind
		{
			get
			{
				return this._fromClauseItemKind;
			}
		}

		// Token: 0x04001CD0 RID: 7376
		private readonly Node _fromClauseItemExpr;

		// Token: 0x04001CD1 RID: 7377
		private readonly FromClauseItemKind _fromClauseItemKind;
	}
}
