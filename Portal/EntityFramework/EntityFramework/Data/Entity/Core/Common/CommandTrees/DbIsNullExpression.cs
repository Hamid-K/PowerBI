using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006C6 RID: 1734
	public class DbIsNullExpression : DbUnaryExpression
	{
		// Token: 0x060050EC RID: 20716 RVA: 0x001221D6 File Offset: 0x001203D6
		internal DbIsNullExpression()
		{
		}

		// Token: 0x060050ED RID: 20717 RVA: 0x001221DE File Offset: 0x001203DE
		internal DbIsNullExpression(TypeUsage booleanResultType, DbExpression arg)
			: base(DbExpressionKind.IsNull, booleanResultType, arg)
		{
		}

		// Token: 0x060050EE RID: 20718 RVA: 0x001221EA File Offset: 0x001203EA
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x060050EF RID: 20719 RVA: 0x001221FF File Offset: 0x001203FF
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}
	}
}
