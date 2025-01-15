using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006AC RID: 1708
	public sealed class DbComparisonExpression : DbBinaryExpression
	{
		// Token: 0x06005008 RID: 20488 RVA: 0x0012174A File Offset: 0x0011F94A
		internal DbComparisonExpression(DbExpressionKind kind, TypeUsage booleanResultType, DbExpression left, DbExpression right)
			: base(kind, booleanResultType, left, right)
		{
		}

		// Token: 0x06005009 RID: 20489 RVA: 0x00121757 File Offset: 0x0011F957
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x0600500A RID: 20490 RVA: 0x0012176C File Offset: 0x0011F96C
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}
	}
}
