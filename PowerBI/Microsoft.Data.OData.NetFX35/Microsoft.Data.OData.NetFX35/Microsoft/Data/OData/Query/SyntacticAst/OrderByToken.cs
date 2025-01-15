using System;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x020000D1 RID: 209
	internal sealed class OrderByToken : QueryToken
	{
		// Token: 0x0600051A RID: 1306 RVA: 0x000119FC File Offset: 0x0000FBFC
		public OrderByToken(QueryToken expression, OrderByDirection direction)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(expression, "expression");
			this.expression = expression;
			this.direction = direction;
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x00011A1D File Offset: 0x0000FC1D
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.OrderBy;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x00011A20 File Offset: 0x0000FC20
		public OrderByDirection Direction
		{
			get
			{
				return this.direction;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x00011A28 File Offset: 0x0000FC28
		public QueryToken Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00011A30 File Offset: 0x0000FC30
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040001E0 RID: 480
		private readonly OrderByDirection direction;

		// Token: 0x040001E1 RID: 481
		private readonly QueryToken expression;
	}
}
