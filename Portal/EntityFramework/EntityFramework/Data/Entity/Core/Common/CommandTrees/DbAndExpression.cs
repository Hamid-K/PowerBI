using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006A4 RID: 1700
	public sealed class DbAndExpression : DbBinaryExpression
	{
		// Token: 0x06004FDE RID: 20446 RVA: 0x0012141A File Offset: 0x0011F61A
		internal DbAndExpression(TypeUsage booleanResultType, DbExpression left, DbExpression right)
			: base(DbExpressionKind.And, booleanResultType, left, right)
		{
		}

		// Token: 0x06004FDF RID: 20447 RVA: 0x00121426 File Offset: 0x0011F626
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06004FE0 RID: 20448 RVA: 0x0012143B File Offset: 0x0011F63B
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}
	}
}
