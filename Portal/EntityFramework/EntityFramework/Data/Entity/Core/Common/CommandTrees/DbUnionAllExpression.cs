using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006E4 RID: 1764
	public sealed class DbUnionAllExpression : DbBinaryExpression
	{
		// Token: 0x06005191 RID: 20881 RVA: 0x00123BF8 File Offset: 0x00121DF8
		internal DbUnionAllExpression(TypeUsage resultType, DbExpression left, DbExpression right)
			: base(DbExpressionKind.UnionAll, resultType, left, right)
		{
		}

		// Token: 0x06005192 RID: 20882 RVA: 0x00123C05 File Offset: 0x00121E05
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06005193 RID: 20883 RVA: 0x00123C1A File Offset: 0x00121E1A
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}
	}
}
