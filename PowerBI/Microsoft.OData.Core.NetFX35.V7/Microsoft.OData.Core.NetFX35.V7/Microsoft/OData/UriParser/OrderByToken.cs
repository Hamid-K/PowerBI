using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000178 RID: 376
	public sealed class OrderByToken : QueryToken
	{
		// Token: 0x06000FB0 RID: 4016 RVA: 0x0002C186 File Offset: 0x0002A386
		public OrderByToken(QueryToken expression, OrderByDirection direction)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(expression, "expression");
			this.expression = expression;
			this.direction = direction;
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x0002B764 File Offset: 0x00029964
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.OrderBy;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x0002C1A8 File Offset: 0x0002A3A8
		public OrderByDirection Direction
		{
			get
			{
				return this.direction;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000FB3 RID: 4019 RVA: 0x0002C1B0 File Offset: 0x0002A3B0
		public QueryToken Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040007DA RID: 2010
		private readonly OrderByDirection direction;

		// Token: 0x040007DB RID: 2011
		private readonly QueryToken expression;
	}
}
