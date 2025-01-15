using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.UriParser.Aggregation
{
	// Token: 0x020001FC RID: 508
	public sealed class AggregateToken : ApplyTransformationToken
	{
		// Token: 0x06001692 RID: 5778 RVA: 0x0003F336 File Offset: 0x0003D536
		public AggregateToken(IEnumerable<AggregateTokenBase> expressions)
		{
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<AggregateTokenBase>>(expressions, "expressions");
			this.expressions = expressions;
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06001693 RID: 5779 RVA: 0x0003AD47 File Offset: 0x00038F47
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.Aggregate;
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06001694 RID: 5780 RVA: 0x0003F351 File Offset: 0x0003D551
		[Obsolete("Use AggregateExpressions for all aggregation expressions or AggregateExpressions.OfType<AggregateExpressionToken>()  for aggregate(..) expressions only.")]
		public IEnumerable<AggregateExpressionToken> Expressions
		{
			get
			{
				return this.expressions.OfType<AggregateExpressionToken>();
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001695 RID: 5781 RVA: 0x0003F35E File Offset: 0x0003D55E
		public IEnumerable<AggregateTokenBase> AggregateExpressions
		{
			get
			{
				return this.expressions;
			}
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x0003F366 File Offset: 0x0003D566
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000A2F RID: 2607
		private readonly IEnumerable<AggregateTokenBase> expressions;
	}
}
