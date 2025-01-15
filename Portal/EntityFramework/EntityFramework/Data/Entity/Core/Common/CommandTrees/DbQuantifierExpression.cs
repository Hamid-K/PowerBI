using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006D7 RID: 1751
	public sealed class DbQuantifierExpression : DbExpression
	{
		// Token: 0x06005151 RID: 20817 RVA: 0x00123601 File Offset: 0x00121801
		internal DbQuantifierExpression(DbExpressionKind kind, TypeUsage booleanResultType, DbExpressionBinding input, DbExpression predicate)
			: base(kind, booleanResultType, true)
		{
			this._input = input;
			this._predicate = predicate;
		}

		// Token: 0x17000FD9 RID: 4057
		// (get) Token: 0x06005152 RID: 20818 RVA: 0x0012361B File Offset: 0x0012181B
		public DbExpressionBinding Input
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x17000FDA RID: 4058
		// (get) Token: 0x06005153 RID: 20819 RVA: 0x00123623 File Offset: 0x00121823
		public DbExpression Predicate
		{
			get
			{
				return this._predicate;
			}
		}

		// Token: 0x06005154 RID: 20820 RVA: 0x0012362B File Offset: 0x0012182B
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06005155 RID: 20821 RVA: 0x00123640 File Offset: 0x00121840
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04001DB9 RID: 7609
		private readonly DbExpressionBinding _input;

		// Token: 0x04001DBA RID: 7610
		private readonly DbExpression _predicate;
	}
}
