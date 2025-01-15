using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Client.ALinq.UriParser
{
	// Token: 0x0200013C RID: 316
	public sealed class EntitySetAggregateToken : AggregateTokenBase
	{
		// Token: 0x06000CC4 RID: 3268 RVA: 0x0002D463 File Offset: 0x0002B663
		public EntitySetAggregateToken(QueryToken entitySet, IEnumerable<AggregateTokenBase> expressions)
		{
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<AggregateTokenBase>>(expressions, "expressions");
			this.expressions = expressions;
			this.entitySet = entitySet;
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x0002D485 File Offset: 0x0002B685
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.EntitySetAggregateExpression;
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x0002D489 File Offset: 0x0002B689
		public IEnumerable<AggregateTokenBase> Expressions
		{
			get
			{
				return this.expressions;
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x0002D491 File Offset: 0x0002B691
		public QueryToken EntitySet
		{
			get
			{
				return this.entitySet;
			}
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x0002D499 File Offset: 0x0002B699
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

		// Token: 0x06000CC9 RID: 3273 RVA: 0x0002D4D4 File Offset: 0x0002B6D4
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

		// Token: 0x06000CCA RID: 3274 RVA: 0x0002D528 File Offset: 0x0002B728
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040006AE RID: 1710
		private readonly QueryToken entitySet;

		// Token: 0x040006AF RID: 1711
		private readonly IEnumerable<AggregateTokenBase> expressions;
	}
}
