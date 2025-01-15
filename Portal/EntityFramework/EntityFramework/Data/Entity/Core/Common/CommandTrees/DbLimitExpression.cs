using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006CC RID: 1740
	public sealed class DbLimitExpression : DbExpression
	{
		// Token: 0x0600511B RID: 20763 RVA: 0x0012328E File Offset: 0x0012148E
		internal DbLimitExpression(TypeUsage resultType, DbExpression argument, DbExpression limit, bool withTies)
			: base(DbExpressionKind.Limit, resultType, true)
		{
			this._argument = argument;
			this._limit = limit;
			this._withTies = withTies;
		}

		// Token: 0x17000FCB RID: 4043
		// (get) Token: 0x0600511C RID: 20764 RVA: 0x001232B0 File Offset: 0x001214B0
		public DbExpression Argument
		{
			get
			{
				return this._argument;
			}
		}

		// Token: 0x17000FCC RID: 4044
		// (get) Token: 0x0600511D RID: 20765 RVA: 0x001232B8 File Offset: 0x001214B8
		public DbExpression Limit
		{
			get
			{
				return this._limit;
			}
		}

		// Token: 0x17000FCD RID: 4045
		// (get) Token: 0x0600511E RID: 20766 RVA: 0x001232C0 File Offset: 0x001214C0
		public bool WithTies
		{
			get
			{
				return this._withTies;
			}
		}

		// Token: 0x0600511F RID: 20767 RVA: 0x001232C8 File Offset: 0x001214C8
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06005120 RID: 20768 RVA: 0x001232DD File Offset: 0x001214DD
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04001DAC RID: 7596
		private readonly DbExpression _argument;

		// Token: 0x04001DAD RID: 7597
		private readonly DbExpression _limit;

		// Token: 0x04001DAE RID: 7598
		private readonly bool _withTies;
	}
}
