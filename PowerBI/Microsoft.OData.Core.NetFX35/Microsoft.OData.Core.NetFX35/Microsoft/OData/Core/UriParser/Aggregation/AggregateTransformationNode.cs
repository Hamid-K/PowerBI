using System;
using System.Collections.Generic;

namespace Microsoft.OData.Core.UriParser.Aggregation
{
	// Token: 0x020002A8 RID: 680
	public sealed class AggregateTransformationNode : TransformationNode
	{
		// Token: 0x06001787 RID: 6023 RVA: 0x00050A20 File Offset: 0x0004EC20
		public AggregateTransformationNode(IEnumerable<AggregateExpression> expressions)
		{
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<AggregateExpression>>(expressions, "expressions");
			this.expressions = expressions;
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06001788 RID: 6024 RVA: 0x00050A3A File Offset: 0x0004EC3A
		public IEnumerable<AggregateExpression> Expressions
		{
			get
			{
				return this.expressions;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001789 RID: 6025 RVA: 0x00050A42 File Offset: 0x0004EC42
		public override TransformationNodeKind Kind
		{
			get
			{
				return TransformationNodeKind.Aggregate;
			}
		}

		// Token: 0x04000A1F RID: 2591
		private readonly IEnumerable<AggregateExpression> expressions;
	}
}
