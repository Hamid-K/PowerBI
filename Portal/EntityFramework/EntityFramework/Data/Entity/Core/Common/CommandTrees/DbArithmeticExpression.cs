using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006A6 RID: 1702
	public sealed class DbArithmeticExpression : DbExpression
	{
		// Token: 0x06004FE6 RID: 20454 RVA: 0x001214A4 File Offset: 0x0011F6A4
		internal DbArithmeticExpression(DbExpressionKind kind, TypeUsage numericResultType, DbExpressionList args)
			: base(kind, numericResultType, true)
		{
			this._args = args;
		}

		// Token: 0x17000F8E RID: 3982
		// (get) Token: 0x06004FE7 RID: 20455 RVA: 0x001214B6 File Offset: 0x0011F6B6
		public IList<DbExpression> Arguments
		{
			get
			{
				return this._args;
			}
		}

		// Token: 0x06004FE8 RID: 20456 RVA: 0x001214BE File Offset: 0x0011F6BE
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06004FE9 RID: 20457 RVA: 0x001214D3 File Offset: 0x0011F6D3
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04001D36 RID: 7478
		private readonly DbExpressionList _args;
	}
}
