using System;
using System.Collections.Generic;

namespace Microsoft.OData.UriParser.Aggregation
{
	// Token: 0x020001C4 RID: 452
	public sealed class AggregateTransformationNode : TransformationNode
	{
		// Token: 0x060011C6 RID: 4550 RVA: 0x00031D06 File Offset: 0x0002FF06
		public AggregateTransformationNode(IEnumerable<AggregateExpression> expressions)
		{
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<AggregateExpression>>(expressions, "expressions");
			this.expressions = expressions;
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x060011C7 RID: 4551 RVA: 0x00031D21 File Offset: 0x0002FF21
		public IEnumerable<AggregateExpression> Expressions
		{
			get
			{
				return this.expressions;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x060011C8 RID: 4552 RVA: 0x00002500 File Offset: 0x00000700
		public override TransformationNodeKind Kind
		{
			get
			{
				return TransformationNodeKind.Aggregate;
			}
		}

		// Token: 0x0400090A RID: 2314
		private readonly IEnumerable<AggregateExpression> expressions;
	}
}
