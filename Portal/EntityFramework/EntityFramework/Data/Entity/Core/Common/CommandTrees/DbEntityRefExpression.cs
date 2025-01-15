using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006B3 RID: 1715
	public sealed class DbEntityRefExpression : DbUnaryExpression
	{
		// Token: 0x06005027 RID: 20519 RVA: 0x0012198D File Offset: 0x0011FB8D
		internal DbEntityRefExpression(TypeUsage refResultType, DbExpression entity)
			: base(DbExpressionKind.EntityRef, refResultType, entity)
		{
		}

		// Token: 0x06005028 RID: 20520 RVA: 0x00121999 File Offset: 0x0011FB99
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06005029 RID: 20521 RVA: 0x001219AE File Offset: 0x0011FBAE
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}
	}
}
