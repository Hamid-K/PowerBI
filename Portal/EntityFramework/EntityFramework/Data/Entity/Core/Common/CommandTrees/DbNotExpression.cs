using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006D0 RID: 1744
	public sealed class DbNotExpression : DbUnaryExpression
	{
		// Token: 0x06005131 RID: 20785 RVA: 0x001233ED File Offset: 0x001215ED
		internal DbNotExpression(TypeUsage booleanResultType, DbExpression argument)
			: base(DbExpressionKind.Not, booleanResultType, argument)
		{
		}

		// Token: 0x06005132 RID: 20786 RVA: 0x001233F9 File Offset: 0x001215F9
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06005133 RID: 20787 RVA: 0x0012340E File Offset: 0x0012160E
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}
	}
}
