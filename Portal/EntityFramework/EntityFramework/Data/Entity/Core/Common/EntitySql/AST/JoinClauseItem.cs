using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000688 RID: 1672
	internal sealed class JoinClauseItem : Node
	{
		// Token: 0x06004F32 RID: 20274 RVA: 0x0011FAC0 File Offset: 0x0011DCC0
		internal JoinClauseItem(FromClauseItem joinLeft, FromClauseItem joinRight, JoinKind joinKind)
			: this(joinLeft, joinRight, joinKind, null)
		{
		}

		// Token: 0x06004F33 RID: 20275 RVA: 0x0011FACC File Offset: 0x0011DCCC
		internal JoinClauseItem(FromClauseItem joinLeft, FromClauseItem joinRight, JoinKind joinKind, Node onExpr)
		{
			this._joinLeft = joinLeft;
			this._joinRight = joinRight;
			this.JoinKind = joinKind;
			this._onExpr = onExpr;
		}

		// Token: 0x17000F53 RID: 3923
		// (get) Token: 0x06004F34 RID: 20276 RVA: 0x0011FAF1 File Offset: 0x0011DCF1
		internal FromClauseItem LeftExpr
		{
			get
			{
				return this._joinLeft;
			}
		}

		// Token: 0x17000F54 RID: 3924
		// (get) Token: 0x06004F35 RID: 20277 RVA: 0x0011FAF9 File Offset: 0x0011DCF9
		internal FromClauseItem RightExpr
		{
			get
			{
				return this._joinRight;
			}
		}

		// Token: 0x17000F55 RID: 3925
		// (get) Token: 0x06004F36 RID: 20278 RVA: 0x0011FB01 File Offset: 0x0011DD01
		// (set) Token: 0x06004F37 RID: 20279 RVA: 0x0011FB09 File Offset: 0x0011DD09
		internal JoinKind JoinKind { get; set; }

		// Token: 0x17000F56 RID: 3926
		// (get) Token: 0x06004F38 RID: 20280 RVA: 0x0011FB12 File Offset: 0x0011DD12
		internal Node OnExpr
		{
			get
			{
				return this._onExpr;
			}
		}

		// Token: 0x04001CE3 RID: 7395
		private readonly FromClauseItem _joinLeft;

		// Token: 0x04001CE4 RID: 7396
		private readonly FromClauseItem _joinRight;

		// Token: 0x04001CE5 RID: 7397
		private readonly Node _onExpr;
	}
}
