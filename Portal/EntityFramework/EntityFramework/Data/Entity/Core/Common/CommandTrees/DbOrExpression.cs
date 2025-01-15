using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006D3 RID: 1747
	public class DbOrExpression : DbBinaryExpression
	{
		// Token: 0x0600513B RID: 20795 RVA: 0x0012349E File Offset: 0x0012169E
		internal DbOrExpression()
		{
		}

		// Token: 0x0600513C RID: 20796 RVA: 0x001234A6 File Offset: 0x001216A6
		internal DbOrExpression(TypeUsage booleanResultType, DbExpression left, DbExpression right)
			: base(DbExpressionKind.Or, booleanResultType, left, right)
		{
		}

		// Token: 0x0600513D RID: 20797 RVA: 0x001234B3 File Offset: 0x001216B3
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x0600513E RID: 20798 RVA: 0x001234C8 File Offset: 0x001216C8
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}
	}
}
