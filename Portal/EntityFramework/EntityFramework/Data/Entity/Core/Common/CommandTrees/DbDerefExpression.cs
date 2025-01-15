using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006B0 RID: 1712
	public sealed class DbDerefExpression : DbUnaryExpression
	{
		// Token: 0x0600501C RID: 20508 RVA: 0x001218CA File Offset: 0x0011FACA
		internal DbDerefExpression(TypeUsage entityResultType, DbExpression refExpr)
			: base(DbExpressionKind.Deref, entityResultType, refExpr)
		{
		}

		// Token: 0x0600501D RID: 20509 RVA: 0x001218D5 File Offset: 0x0011FAD5
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x0600501E RID: 20510 RVA: 0x001218EA File Offset: 0x0011FAEA
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}
	}
}
