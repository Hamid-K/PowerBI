using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006DA RID: 1754
	public sealed class DbRefKeyExpression : DbUnaryExpression
	{
		// Token: 0x06005164 RID: 20836 RVA: 0x001237C5 File Offset: 0x001219C5
		internal DbRefKeyExpression(TypeUsage rowResultType, DbExpression reference)
			: base(DbExpressionKind.RefKey, rowResultType, reference)
		{
		}

		// Token: 0x06005165 RID: 20837 RVA: 0x001237D1 File Offset: 0x001219D1
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06005166 RID: 20838 RVA: 0x001237E6 File Offset: 0x001219E6
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}
	}
}
