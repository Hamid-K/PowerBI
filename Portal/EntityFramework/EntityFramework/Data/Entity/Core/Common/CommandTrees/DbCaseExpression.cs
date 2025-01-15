using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006A8 RID: 1704
	public sealed class DbCaseExpression : DbExpression
	{
		// Token: 0x06004FEE RID: 20462 RVA: 0x0012151A File Offset: 0x0011F71A
		internal DbCaseExpression(TypeUsage commonResultType, DbExpressionList whens, DbExpressionList thens, DbExpression elseExpr)
			: base(DbExpressionKind.Case, commonResultType, true)
		{
			this._when = whens;
			this._then = thens;
			this._else = elseExpr;
		}

		// Token: 0x17000F91 RID: 3985
		// (get) Token: 0x06004FEF RID: 20463 RVA: 0x0012153B File Offset: 0x0011F73B
		public IList<DbExpression> When
		{
			get
			{
				return this._when;
			}
		}

		// Token: 0x17000F92 RID: 3986
		// (get) Token: 0x06004FF0 RID: 20464 RVA: 0x00121543 File Offset: 0x0011F743
		public IList<DbExpression> Then
		{
			get
			{
				return this._then;
			}
		}

		// Token: 0x17000F93 RID: 3987
		// (get) Token: 0x06004FF1 RID: 20465 RVA: 0x0012154B File Offset: 0x0011F74B
		public DbExpression Else
		{
			get
			{
				return this._else;
			}
		}

		// Token: 0x06004FF2 RID: 20466 RVA: 0x00121553 File Offset: 0x0011F753
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06004FF3 RID: 20467 RVA: 0x00121568 File Offset: 0x0011F768
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04001D39 RID: 7481
		private readonly DbExpressionList _when;

		// Token: 0x04001D3A RID: 7482
		private readonly DbExpressionList _then;

		// Token: 0x04001D3B RID: 7483
		private readonly DbExpression _else;
	}
}
