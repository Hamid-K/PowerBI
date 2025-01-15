using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006B1 RID: 1713
	public sealed class DbDistinctExpression : DbUnaryExpression
	{
		// Token: 0x0600501F RID: 20511 RVA: 0x001218FF File Offset: 0x0011FAFF
		internal DbDistinctExpression(TypeUsage resultType, DbExpression argument)
			: base(DbExpressionKind.Distinct, resultType, argument)
		{
		}

		// Token: 0x06005020 RID: 20512 RVA: 0x0012190B File Offset: 0x0011FB0B
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06005021 RID: 20513 RVA: 0x00121920 File Offset: 0x0011FB20
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}
	}
}
