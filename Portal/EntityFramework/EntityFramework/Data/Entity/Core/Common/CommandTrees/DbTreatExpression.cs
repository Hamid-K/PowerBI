using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006E2 RID: 1762
	public sealed class DbTreatExpression : DbUnaryExpression
	{
		// Token: 0x0600518B RID: 20875 RVA: 0x00123BA0 File Offset: 0x00121DA0
		internal DbTreatExpression(TypeUsage asType, DbExpression argument)
			: base(DbExpressionKind.Treat, asType, argument)
		{
		}

		// Token: 0x0600518C RID: 20876 RVA: 0x00123BAC File Offset: 0x00121DAC
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x0600518D RID: 20877 RVA: 0x00123BC1 File Offset: 0x00121DC1
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}
	}
}
