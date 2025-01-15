using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001C4 RID: 452
	public sealed class OrderByToken : QueryToken
	{
		// Token: 0x060014DD RID: 5341 RVA: 0x0003C156 File Offset: 0x0003A356
		public OrderByToken(QueryToken expression, OrderByDirection direction)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(expression, "expression");
			this.expression = expression;
			this.direction = direction;
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x060014DE RID: 5342 RVA: 0x0003B810 File Offset: 0x00039A10
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.OrderBy;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x060014DF RID: 5343 RVA: 0x0003C178 File Offset: 0x0003A378
		public OrderByDirection Direction
		{
			get
			{
				return this.direction;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x060014E0 RID: 5344 RVA: 0x0003C180 File Offset: 0x0003A380
		public QueryToken Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x000032BD File Offset: 0x000014BD
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400091D RID: 2333
		private readonly OrderByDirection direction;

		// Token: 0x0400091E RID: 2334
		private readonly QueryToken expression;
	}
}
