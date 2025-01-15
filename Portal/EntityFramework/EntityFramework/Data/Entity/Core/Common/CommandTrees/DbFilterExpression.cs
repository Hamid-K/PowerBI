using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006BB RID: 1723
	public sealed class DbFilterExpression : DbExpression
	{
		// Token: 0x060050B4 RID: 20660 RVA: 0x00121D81 File Offset: 0x0011FF81
		internal DbFilterExpression(TypeUsage resultType, DbExpressionBinding input, DbExpression predicate)
			: base(DbExpressionKind.Filter, resultType, true)
		{
			this._input = input;
			this._predicate = predicate;
		}

		// Token: 0x17000FA6 RID: 4006
		// (get) Token: 0x060050B5 RID: 20661 RVA: 0x00121D9B File Offset: 0x0011FF9B
		public DbExpressionBinding Input
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x17000FA7 RID: 4007
		// (get) Token: 0x060050B6 RID: 20662 RVA: 0x00121DA3 File Offset: 0x0011FFA3
		public DbExpression Predicate
		{
			get
			{
				return this._predicate;
			}
		}

		// Token: 0x060050B7 RID: 20663 RVA: 0x00121DAB File Offset: 0x0011FFAB
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x060050B8 RID: 20664 RVA: 0x00121DC0 File Offset: 0x0011FFC0
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04001D8C RID: 7564
		private readonly DbExpressionBinding _input;

		// Token: 0x04001D8D RID: 7565
		private readonly DbExpression _predicate;
	}
}
