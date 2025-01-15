using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000691 RID: 1681
	internal sealed class OrderByClause : Node
	{
		// Token: 0x06004F67 RID: 20327 RVA: 0x00120712 File Offset: 0x0011E912
		internal OrderByClause(NodeList<OrderByClauseItem> orderByClauseItem, Node skipExpr, Node limitExpr, uint methodCallCount)
		{
			this._orderByClauseItem = orderByClauseItem;
			this._skipExpr = skipExpr;
			this._limitExpr = limitExpr;
			this._methodCallCount = methodCallCount;
		}

		// Token: 0x17000F6B RID: 3947
		// (get) Token: 0x06004F68 RID: 20328 RVA: 0x00120737 File Offset: 0x0011E937
		internal NodeList<OrderByClauseItem> OrderByClauseItem
		{
			get
			{
				return this._orderByClauseItem;
			}
		}

		// Token: 0x17000F6C RID: 3948
		// (get) Token: 0x06004F69 RID: 20329 RVA: 0x0012073F File Offset: 0x0011E93F
		internal Node SkipSubClause
		{
			get
			{
				return this._skipExpr;
			}
		}

		// Token: 0x17000F6D RID: 3949
		// (get) Token: 0x06004F6A RID: 20330 RVA: 0x00120747 File Offset: 0x0011E947
		internal Node LimitSubClause
		{
			get
			{
				return this._limitExpr;
			}
		}

		// Token: 0x17000F6E RID: 3950
		// (get) Token: 0x06004F6B RID: 20331 RVA: 0x0012074F File Offset: 0x0011E94F
		internal bool HasMethodCall
		{
			get
			{
				return this._methodCallCount > 0U;
			}
		}

		// Token: 0x04001D0D RID: 7437
		private readonly NodeList<OrderByClauseItem> _orderByClauseItem;

		// Token: 0x04001D0E RID: 7438
		private readonly Node _skipExpr;

		// Token: 0x04001D0F RID: 7439
		private readonly Node _limitExpr;

		// Token: 0x04001D10 RID: 7440
		private readonly uint _methodCallCount;
	}
}
