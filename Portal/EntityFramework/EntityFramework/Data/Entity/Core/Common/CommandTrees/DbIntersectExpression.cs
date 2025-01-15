using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006C4 RID: 1732
	public sealed class DbIntersectExpression : DbBinaryExpression
	{
		// Token: 0x060050E6 RID: 20710 RVA: 0x00122169 File Offset: 0x00120369
		internal DbIntersectExpression(TypeUsage resultType, DbExpression left, DbExpression right)
			: base(DbExpressionKind.Intersect, resultType, left, right)
		{
		}

		// Token: 0x060050E7 RID: 20711 RVA: 0x00122176 File Offset: 0x00120376
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x060050E8 RID: 20712 RVA: 0x0012218B File Offset: 0x0012038B
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}
	}
}
