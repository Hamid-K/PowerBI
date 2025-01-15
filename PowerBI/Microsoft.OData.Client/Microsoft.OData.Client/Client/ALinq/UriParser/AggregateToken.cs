using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Client.ALinq.UriParser
{
	// Token: 0x02000114 RID: 276
	public sealed class AggregateToken : ApplyTransformationToken
	{
		// Token: 0x06000BBA RID: 3002 RVA: 0x0002C86C File Offset: 0x0002AA6C
		public AggregateToken(IEnumerable<AggregateTokenBase> expressions)
		{
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<AggregateTokenBase>>(expressions, "expressions");
			this.expressions = expressions;
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x0002C887 File Offset: 0x0002AA87
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.Aggregate;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000BBC RID: 3004 RVA: 0x0002C88B File Offset: 0x0002AA8B
		[Obsolete("Use AggregateExpressions for all aggregation expressions or AggregateExpressions.OfType<AggregateExpressionToken>()  for aggregate(..) expressions only.")]
		public IEnumerable<AggregateExpressionToken> Expressions
		{
			get
			{
				return this.expressions.OfType<AggregateExpressionToken>();
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x0002C898 File Offset: 0x0002AA98
		public IEnumerable<AggregateTokenBase> AggregateExpressions
		{
			get
			{
				return this.expressions;
			}
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0002C8A0 File Offset: 0x0002AAA0
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0400064F RID: 1615
		private readonly IEnumerable<AggregateTokenBase> expressions;
	}
}
