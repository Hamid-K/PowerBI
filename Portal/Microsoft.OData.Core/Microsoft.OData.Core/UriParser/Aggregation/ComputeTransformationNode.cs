using System;
using System.Collections.Generic;

namespace Microsoft.OData.UriParser.Aggregation
{
	// Token: 0x020001F5 RID: 501
	public sealed class ComputeTransformationNode : TransformationNode
	{
		// Token: 0x0600166C RID: 5740 RVA: 0x0003EE08 File Offset: 0x0003D008
		public ComputeTransformationNode(IEnumerable<ComputeExpression> expressions)
		{
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<ComputeExpression>>(expressions, "expressions");
			this.expressions = expressions;
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x0600166D RID: 5741 RVA: 0x0003EE23 File Offset: 0x0003D023
		public IEnumerable<ComputeExpression> Expressions
		{
			get
			{
				return this.expressions;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x0600166E RID: 5742 RVA: 0x000397C8 File Offset: 0x000379C8
		public override TransformationNodeKind Kind
		{
			get
			{
				return TransformationNodeKind.Compute;
			}
		}

		// Token: 0x04000A19 RID: 2585
		private readonly IEnumerable<ComputeExpression> expressions;
	}
}
