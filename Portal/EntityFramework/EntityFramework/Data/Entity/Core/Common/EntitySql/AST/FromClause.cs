using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x0200067F RID: 1663
	internal sealed class FromClause : Node
	{
		// Token: 0x06004F1A RID: 20250 RVA: 0x0011F924 File Offset: 0x0011DB24
		internal FromClause(NodeList<FromClauseItem> fromClauseItems)
		{
			this._fromClauseItems = fromClauseItems;
		}

		// Token: 0x17000F45 RID: 3909
		// (get) Token: 0x06004F1B RID: 20251 RVA: 0x0011F933 File Offset: 0x0011DB33
		internal NodeList<FromClauseItem> FromClauseItems
		{
			get
			{
				return this._fromClauseItems;
			}
		}

		// Token: 0x04001CCF RID: 7375
		private readonly NodeList<FromClauseItem> _fromClauseItems;
	}
}
