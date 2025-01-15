using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x020006A0 RID: 1696
	internal class WhenThenExpr : Node
	{
		// Token: 0x06004F92 RID: 20370 RVA: 0x001209C1 File Offset: 0x0011EBC1
		internal WhenThenExpr(Node whenExpr, Node thenExpr)
		{
			this._whenExpr = whenExpr;
			this._thenExpr = thenExpr;
		}

		// Token: 0x17000F88 RID: 3976
		// (get) Token: 0x06004F93 RID: 20371 RVA: 0x001209D7 File Offset: 0x0011EBD7
		internal Node WhenExpr
		{
			get
			{
				return this._whenExpr;
			}
		}

		// Token: 0x17000F89 RID: 3977
		// (get) Token: 0x06004F94 RID: 20372 RVA: 0x001209DF File Offset: 0x0011EBDF
		internal Node ThenExpr
		{
			get
			{
				return this._thenExpr;
			}
		}

		// Token: 0x04001D30 RID: 7472
		private readonly Node _whenExpr;

		// Token: 0x04001D31 RID: 7473
		private readonly Node _thenExpr;
	}
}
