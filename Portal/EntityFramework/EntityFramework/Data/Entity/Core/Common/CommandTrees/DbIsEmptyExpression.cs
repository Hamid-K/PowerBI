using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006C5 RID: 1733
	public sealed class DbIsEmptyExpression : DbUnaryExpression
	{
		// Token: 0x060050E9 RID: 20713 RVA: 0x001221A0 File Offset: 0x001203A0
		internal DbIsEmptyExpression(TypeUsage booleanResultType, DbExpression argument)
			: base(DbExpressionKind.IsEmpty, booleanResultType, argument)
		{
		}

		// Token: 0x060050EA RID: 20714 RVA: 0x001221AC File Offset: 0x001203AC
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x060050EB RID: 20715 RVA: 0x001221C1 File Offset: 0x001203C1
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}
	}
}
