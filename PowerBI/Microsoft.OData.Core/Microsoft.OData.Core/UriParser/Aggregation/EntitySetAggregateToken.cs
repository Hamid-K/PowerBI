using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.UriParser.Aggregation
{
	// Token: 0x020001EF RID: 495
	public sealed class EntitySetAggregateToken : AggregateTokenBase
	{
		// Token: 0x06001647 RID: 5703 RVA: 0x0003E3AE File Offset: 0x0003C5AE
		public EntitySetAggregateToken(QueryToken entitySet, IEnumerable<AggregateTokenBase> expressions)
		{
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<AggregateTokenBase>>(expressions, "expressions");
			this.expressions = expressions;
			this.entitySet = entitySet;
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001648 RID: 5704 RVA: 0x00026501 File Offset: 0x00024701
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.EntitySetAggregateExpression;
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001649 RID: 5705 RVA: 0x0003E3D0 File Offset: 0x0003C5D0
		public IEnumerable<AggregateTokenBase> Expressions
		{
			get
			{
				return this.expressions;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x0600164A RID: 5706 RVA: 0x0003E3D8 File Offset: 0x0003C5D8
		public QueryToken EntitySet
		{
			get
			{
				return this.entitySet;
			}
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x0003E3E0 File Offset: 0x0003C5E0
		public static EntitySetAggregateToken Merge(EntitySetAggregateToken token1, EntitySetAggregateToken token2)
		{
			if (token1 == null)
			{
				return token2;
			}
			if (token2 == null)
			{
				return token1;
			}
			object.Equals(token1.entitySet, token2.entitySet);
			return new EntitySetAggregateToken(token1.entitySet, token1.expressions.Concat(token2.expressions));
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x0003E41C File Offset: 0x0003C61C
		public string Path()
		{
			List<string> list = new List<string>();
			QueryToken queryToken = this.entitySet;
			for (PathToken pathToken = queryToken as PathToken; pathToken != null; pathToken = pathToken.NextToken as PathToken)
			{
				list.Add(pathToken.Identifier);
			}
			list.Reverse();
			return string.Join("/", list.ToArray());
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x0003E470 File Offset: 0x0003C670
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000A0B RID: 2571
		private readonly QueryToken entitySet;

		// Token: 0x04000A0C RID: 2572
		private readonly IEnumerable<AggregateTokenBase> expressions;
	}
}
