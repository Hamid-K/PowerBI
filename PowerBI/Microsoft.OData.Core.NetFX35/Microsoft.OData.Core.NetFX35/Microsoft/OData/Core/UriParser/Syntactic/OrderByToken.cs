using System;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;

namespace Microsoft.OData.Core.UriParser.Syntactic
{
	// Token: 0x0200027E RID: 638
	internal sealed class OrderByToken : QueryToken
	{
		// Token: 0x0600162D RID: 5677 RVA: 0x0004C551 File Offset: 0x0004A751
		public OrderByToken(QueryToken expression, OrderByDirection direction)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(expression, "expression");
			this.expression = expression;
			this.direction = direction;
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x0600162E RID: 5678 RVA: 0x0004C572 File Offset: 0x0004A772
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.OrderBy;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x0600162F RID: 5679 RVA: 0x0004C575 File Offset: 0x0004A775
		public OrderByDirection Direction
		{
			get
			{
				return this.direction;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001630 RID: 5680 RVA: 0x0004C57D File Offset: 0x0004A77D
		public QueryToken Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x0004C585 File Offset: 0x0004A785
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000933 RID: 2355
		private readonly OrderByDirection direction;

		// Token: 0x04000934 RID: 2356
		private readonly QueryToken expression;
	}
}
