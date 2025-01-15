using System;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x02000049 RID: 73
	public sealed class OrderByQueryToken : QueryToken
	{
		// Token: 0x060001CC RID: 460 RVA: 0x0000A840 File Offset: 0x00008A40
		public OrderByQueryToken(QueryToken expression, OrderByDirection direction)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(expression, "expression");
			this.expression = expression;
			this.direction = direction;
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001CD RID: 461 RVA: 0x0000A861 File Offset: 0x00008A61
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.OrderBy;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001CE RID: 462 RVA: 0x0000A864 File Offset: 0x00008A64
		public OrderByDirection Direction
		{
			get
			{
				return this.direction;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0000A86C File Offset: 0x00008A6C
		public QueryToken Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x040001C0 RID: 448
		private readonly OrderByDirection direction;

		// Token: 0x040001C1 RID: 449
		private readonly QueryToken expression;
	}
}
