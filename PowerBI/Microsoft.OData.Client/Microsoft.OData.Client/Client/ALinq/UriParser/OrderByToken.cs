using System;
using Microsoft.OData.UriParser;

namespace Microsoft.OData.Client.ALinq.UriParser
{
	// Token: 0x0200012E RID: 302
	public sealed class OrderByToken : QueryToken
	{
		// Token: 0x06000C73 RID: 3187 RVA: 0x0002CFFD File Offset: 0x0002B1FD
		public OrderByToken(QueryToken expression, OrderByDirection direction)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(expression, "expression");
			this.expression = expression;
			this.direction = direction;
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x0002D01F File Offset: 0x0002B21F
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.OrderBy;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x0002D022 File Offset: 0x0002B222
		public OrderByDirection Direction
		{
			get
			{
				return this.direction;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x0002D02A File Offset: 0x0002B22A
		public QueryToken Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x00006FEF File Offset: 0x000051EF
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400067C RID: 1660
		private readonly OrderByDirection direction;

		// Token: 0x0400067D RID: 1661
		private readonly QueryToken expression;
	}
}
