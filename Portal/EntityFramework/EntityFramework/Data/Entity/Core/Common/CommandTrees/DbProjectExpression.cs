using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006D5 RID: 1749
	public sealed class DbProjectExpression : DbExpression
	{
		// Token: 0x06005144 RID: 20804 RVA: 0x0012352A File Offset: 0x0012172A
		internal DbProjectExpression(TypeUsage resultType, DbExpressionBinding input, DbExpression projection)
			: base(DbExpressionKind.Project, resultType, true)
		{
			this._input = input;
			this._projection = projection;
		}

		// Token: 0x17000FD5 RID: 4053
		// (get) Token: 0x06005145 RID: 20805 RVA: 0x00123544 File Offset: 0x00121744
		public DbExpressionBinding Input
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x17000FD6 RID: 4054
		// (get) Token: 0x06005146 RID: 20806 RVA: 0x0012354C File Offset: 0x0012174C
		public DbExpression Projection
		{
			get
			{
				return this._projection;
			}
		}

		// Token: 0x06005147 RID: 20807 RVA: 0x00123554 File Offset: 0x00121754
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06005148 RID: 20808 RVA: 0x00123569 File Offset: 0x00121769
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04001DB5 RID: 7605
		private readonly DbExpressionBinding _input;

		// Token: 0x04001DB6 RID: 7606
		private readonly DbExpression _projection;
	}
}
