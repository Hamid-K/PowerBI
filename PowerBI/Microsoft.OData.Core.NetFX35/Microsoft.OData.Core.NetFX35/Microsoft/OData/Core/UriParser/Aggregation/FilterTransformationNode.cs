using System;
using Microsoft.OData.Core.UriParser.Semantic;

namespace Microsoft.OData.Core.UriParser.Aggregation
{
	// Token: 0x020002AC RID: 684
	public sealed class FilterTransformationNode : TransformationNode
	{
		// Token: 0x0600179D RID: 6045 RVA: 0x00050D05 File Offset: 0x0004EF05
		public FilterTransformationNode(FilterClause filterClause)
		{
			ExceptionUtils.CheckArgumentNotNull<FilterClause>(filterClause, "filterClause");
			this.filterClause = filterClause;
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x0600179E RID: 6046 RVA: 0x00050D1F File Offset: 0x0004EF1F
		public FilterClause FilterClause
		{
			get
			{
				return this.filterClause;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x0600179F RID: 6047 RVA: 0x00050D27 File Offset: 0x0004EF27
		public override TransformationNodeKind Kind
		{
			get
			{
				return TransformationNodeKind.Filter;
			}
		}

		// Token: 0x04000A2C RID: 2604
		private readonly FilterClause filterClause;
	}
}
