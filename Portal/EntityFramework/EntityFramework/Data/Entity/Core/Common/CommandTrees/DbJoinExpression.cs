using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006C8 RID: 1736
	public sealed class DbJoinExpression : DbExpression
	{
		// Token: 0x060050F4 RID: 20724 RVA: 0x00122259 File Offset: 0x00120459
		internal DbJoinExpression(DbExpressionKind joinKind, TypeUsage collectionOfRowResultType, DbExpressionBinding left, DbExpressionBinding right, DbExpression condition)
			: base(joinKind, collectionOfRowResultType, true)
		{
			this._left = left;
			this._right = right;
			this._condition = condition;
		}

		// Token: 0x17000FC1 RID: 4033
		// (get) Token: 0x060050F5 RID: 20725 RVA: 0x0012227B File Offset: 0x0012047B
		public DbExpressionBinding Left
		{
			get
			{
				return this._left;
			}
		}

		// Token: 0x17000FC2 RID: 4034
		// (get) Token: 0x060050F6 RID: 20726 RVA: 0x00122283 File Offset: 0x00120483
		public DbExpressionBinding Right
		{
			get
			{
				return this._right;
			}
		}

		// Token: 0x17000FC3 RID: 4035
		// (get) Token: 0x060050F7 RID: 20727 RVA: 0x0012228B File Offset: 0x0012048B
		public DbExpression JoinCondition
		{
			get
			{
				return this._condition;
			}
		}

		// Token: 0x060050F8 RID: 20728 RVA: 0x00122293 File Offset: 0x00120493
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x060050F9 RID: 20729 RVA: 0x001222A8 File Offset: 0x001204A8
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04001DA2 RID: 7586
		private readonly DbExpressionBinding _left;

		// Token: 0x04001DA3 RID: 7587
		private readonly DbExpressionBinding _right;

		// Token: 0x04001DA4 RID: 7588
		private readonly DbExpression _condition;
	}
}
