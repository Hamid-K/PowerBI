using System;

namespace Microsoft.OData.UriParser.Aggregation
{
	// Token: 0x020001C8 RID: 456
	public sealed class FilterTransformationNode : TransformationNode
	{
		// Token: 0x060011DC RID: 4572 RVA: 0x00031FD6 File Offset: 0x000301D6
		public FilterTransformationNode(FilterClause filterClause)
		{
			ExceptionUtils.CheckArgumentNotNull<FilterClause>(filterClause, "filterClause");
			this.filterClause = filterClause;
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x060011DD RID: 4573 RVA: 0x00031FF1 File Offset: 0x000301F1
		public FilterClause FilterClause
		{
			get
			{
				return this.filterClause;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x060011DE RID: 4574 RVA: 0x0002900C File Offset: 0x0002720C
		public override TransformationNodeKind Kind
		{
			get
			{
				return TransformationNodeKind.Filter;
			}
		}

		// Token: 0x04000916 RID: 2326
		private readonly FilterClause filterClause;
	}
}
