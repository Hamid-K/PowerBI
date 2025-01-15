using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006D1 RID: 1745
	public sealed class DbNullExpression : DbExpression
	{
		// Token: 0x06005134 RID: 20788 RVA: 0x00123423 File Offset: 0x00121623
		internal DbNullExpression(TypeUsage type)
			: base(DbExpressionKind.Null, type, true)
		{
		}

		// Token: 0x06005135 RID: 20789 RVA: 0x0012342F File Offset: 0x0012162F
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06005136 RID: 20790 RVA: 0x00123444 File Offset: 0x00121644
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}
	}
}
