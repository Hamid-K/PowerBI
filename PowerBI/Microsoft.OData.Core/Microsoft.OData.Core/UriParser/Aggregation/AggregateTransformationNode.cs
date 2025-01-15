using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.UriParser.Aggregation
{
	// Token: 0x020001F4 RID: 500
	public sealed class AggregateTransformationNode : TransformationNode
	{
		// Token: 0x06001668 RID: 5736 RVA: 0x0003EDD8 File Offset: 0x0003CFD8
		public AggregateTransformationNode(IEnumerable<AggregateExpressionBase> expressions)
		{
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<AggregateExpressionBase>>(expressions, "expressions");
			this.expressions = expressions;
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001669 RID: 5737 RVA: 0x0003EDF3 File Offset: 0x0003CFF3
		[Obsolete("Use AggregateExpressions for all aggregation expressions or AggregateExpressions.OfType<AggregateExpressionToken>()  for aggregate(..) expressions only.")]
		public IEnumerable<AggregateExpression> Expressions
		{
			get
			{
				return this.expressions.OfType<AggregateExpression>();
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x0600166A RID: 5738 RVA: 0x0003EE00 File Offset: 0x0003D000
		public IEnumerable<AggregateExpressionBase> AggregateExpressions
		{
			get
			{
				return this.expressions;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x0600166B RID: 5739 RVA: 0x00002390 File Offset: 0x00000590
		public override TransformationNodeKind Kind
		{
			get
			{
				return TransformationNodeKind.Aggregate;
			}
		}

		// Token: 0x04000A18 RID: 2584
		private readonly IEnumerable<AggregateExpressionBase> expressions;
	}
}
