using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000692 RID: 1682
	internal sealed class OrderByClauseItem : Node
	{
		// Token: 0x06004F6C RID: 20332 RVA: 0x0012075A File Offset: 0x0011E95A
		internal OrderByClauseItem(Node orderExpr, OrderKind orderKind)
			: this(orderExpr, orderKind, null)
		{
		}

		// Token: 0x06004F6D RID: 20333 RVA: 0x00120765 File Offset: 0x0011E965
		internal OrderByClauseItem(Node orderExpr, OrderKind orderKind, Identifier optCollationIdentifier)
		{
			this._orderExpr = orderExpr;
			this._orderKind = orderKind;
			this._optCollationIdentifier = optCollationIdentifier;
		}

		// Token: 0x17000F6F RID: 3951
		// (get) Token: 0x06004F6E RID: 20334 RVA: 0x00120782 File Offset: 0x0011E982
		internal Node OrderExpr
		{
			get
			{
				return this._orderExpr;
			}
		}

		// Token: 0x17000F70 RID: 3952
		// (get) Token: 0x06004F6F RID: 20335 RVA: 0x0012078A File Offset: 0x0011E98A
		internal OrderKind OrderKind
		{
			get
			{
				return this._orderKind;
			}
		}

		// Token: 0x17000F71 RID: 3953
		// (get) Token: 0x06004F70 RID: 20336 RVA: 0x00120792 File Offset: 0x0011E992
		internal Identifier Collation
		{
			get
			{
				return this._optCollationIdentifier;
			}
		}

		// Token: 0x04001D11 RID: 7441
		private readonly Node _orderExpr;

		// Token: 0x04001D12 RID: 7442
		private readonly OrderKind _orderKind;

		// Token: 0x04001D13 RID: 7443
		private readonly Identifier _optCollationIdentifier;
	}
}
