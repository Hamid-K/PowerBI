using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006A9 RID: 1705
	public class DbCastExpression : DbUnaryExpression
	{
		// Token: 0x06004FF4 RID: 20468 RVA: 0x0012157D File Offset: 0x0011F77D
		internal DbCastExpression()
		{
		}

		// Token: 0x06004FF5 RID: 20469 RVA: 0x00121585 File Offset: 0x0011F785
		internal DbCastExpression(TypeUsage type, DbExpression argument)
			: base(DbExpressionKind.Cast, type, argument)
		{
		}

		// Token: 0x06004FF6 RID: 20470 RVA: 0x00121590 File Offset: 0x0011F790
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06004FF7 RID: 20471 RVA: 0x001215A5 File Offset: 0x0011F7A5
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}
	}
}
