using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x0200069C RID: 1692
	internal sealed class SelectClause : Node
	{
		// Token: 0x06004F88 RID: 20360 RVA: 0x0012093B File Offset: 0x0011EB3B
		internal SelectClause(NodeList<AliasedExpr> items, SelectKind selectKind, DistinctKind distinctKind, Node topExpr, uint methodCallCount)
		{
			this._selectKind = selectKind;
			this._selectClauseItems = items;
			this._distinctKind = distinctKind;
			this._topExpr = topExpr;
			this._methodCallCount = methodCallCount;
		}

		// Token: 0x17000F81 RID: 3969
		// (get) Token: 0x06004F89 RID: 20361 RVA: 0x00120968 File Offset: 0x0011EB68
		internal NodeList<AliasedExpr> Items
		{
			get
			{
				return this._selectClauseItems;
			}
		}

		// Token: 0x17000F82 RID: 3970
		// (get) Token: 0x06004F8A RID: 20362 RVA: 0x00120970 File Offset: 0x0011EB70
		internal SelectKind SelectKind
		{
			get
			{
				return this._selectKind;
			}
		}

		// Token: 0x17000F83 RID: 3971
		// (get) Token: 0x06004F8B RID: 20363 RVA: 0x00120978 File Offset: 0x0011EB78
		internal DistinctKind DistinctKind
		{
			get
			{
				return this._distinctKind;
			}
		}

		// Token: 0x17000F84 RID: 3972
		// (get) Token: 0x06004F8C RID: 20364 RVA: 0x00120980 File Offset: 0x0011EB80
		internal Node TopExpr
		{
			get
			{
				return this._topExpr;
			}
		}

		// Token: 0x17000F85 RID: 3973
		// (get) Token: 0x06004F8D RID: 20365 RVA: 0x00120988 File Offset: 0x0011EB88
		internal bool HasMethodCall
		{
			get
			{
				return this._methodCallCount > 0U;
			}
		}

		// Token: 0x04001D26 RID: 7462
		private readonly NodeList<AliasedExpr> _selectClauseItems;

		// Token: 0x04001D27 RID: 7463
		private readonly SelectKind _selectKind;

		// Token: 0x04001D28 RID: 7464
		private readonly DistinctKind _distinctKind;

		// Token: 0x04001D29 RID: 7465
		private readonly Node _topExpr;

		// Token: 0x04001D2A RID: 7466
		private readonly uint _methodCallCount;
	}
}
