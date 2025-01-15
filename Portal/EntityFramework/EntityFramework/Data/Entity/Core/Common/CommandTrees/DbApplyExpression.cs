using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006A5 RID: 1701
	public sealed class DbApplyExpression : DbExpression
	{
		// Token: 0x06004FE1 RID: 20449 RVA: 0x00121450 File Offset: 0x0011F650
		internal DbApplyExpression(DbExpressionKind applyKind, TypeUsage resultRowCollectionTypeUsage, DbExpressionBinding input, DbExpressionBinding apply)
			: base(applyKind, resultRowCollectionTypeUsage, true)
		{
			this._input = input;
			this._apply = apply;
		}

		// Token: 0x17000F8C RID: 3980
		// (get) Token: 0x06004FE2 RID: 20450 RVA: 0x0012146A File Offset: 0x0011F66A
		public DbExpressionBinding Apply
		{
			get
			{
				return this._apply;
			}
		}

		// Token: 0x17000F8D RID: 3981
		// (get) Token: 0x06004FE3 RID: 20451 RVA: 0x00121472 File Offset: 0x0011F672
		public DbExpressionBinding Input
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x06004FE4 RID: 20452 RVA: 0x0012147A File Offset: 0x0011F67A
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06004FE5 RID: 20453 RVA: 0x0012148F File Offset: 0x0011F68F
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04001D34 RID: 7476
		private readonly DbExpressionBinding _input;

		// Token: 0x04001D35 RID: 7477
		private readonly DbExpressionBinding _apply;
	}
}
