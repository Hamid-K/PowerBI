using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006B4 RID: 1716
	public sealed class DbExceptExpression : DbBinaryExpression
	{
		// Token: 0x0600502A RID: 20522 RVA: 0x001219C3 File Offset: 0x0011FBC3
		internal DbExceptExpression(TypeUsage resultType, DbExpression left, DbExpression right)
			: base(DbExpressionKind.Except, resultType, left, right)
		{
		}

		// Token: 0x0600502B RID: 20523 RVA: 0x001219D0 File Offset: 0x0011FBD0
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x0600502C RID: 20524 RVA: 0x001219E5 File Offset: 0x0011FBE5
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}
	}
}
