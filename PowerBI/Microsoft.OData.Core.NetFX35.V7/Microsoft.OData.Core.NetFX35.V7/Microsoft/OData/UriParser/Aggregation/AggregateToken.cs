using System;
using System.Collections.Generic;

namespace Microsoft.OData.UriParser.Aggregation
{
	// Token: 0x020001CB RID: 459
	public sealed class AggregateToken : ApplyTransformationToken
	{
		// Token: 0x060011E9 RID: 4585 RVA: 0x0003206E File Offset: 0x0003026E
		public AggregateToken(IEnumerable<AggregateExpressionToken> expressions)
		{
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<AggregateExpressionToken>>(expressions, "expressions");
			this.expressions = expressions;
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x060011EA RID: 4586 RVA: 0x0002ADFF File Offset: 0x00028FFF
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.Aggregate;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x060011EB RID: 4587 RVA: 0x00032089 File Offset: 0x00030289
		public IEnumerable<AggregateExpressionToken> Expressions
		{
			get
			{
				return this.expressions;
			}
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x00032091 File Offset: 0x00030291
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0400091B RID: 2331
		private readonly IEnumerable<AggregateExpressionToken> expressions;
	}
}
