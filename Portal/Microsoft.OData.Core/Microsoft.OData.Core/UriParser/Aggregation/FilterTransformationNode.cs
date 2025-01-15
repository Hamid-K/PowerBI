using System;

namespace Microsoft.OData.UriParser.Aggregation
{
	// Token: 0x020001F2 RID: 498
	public sealed class FilterTransformationNode : TransformationNode
	{
		// Token: 0x06001663 RID: 5731 RVA: 0x0003EDB5 File Offset: 0x0003CFB5
		public FilterTransformationNode(FilterClause filterClause)
		{
			ExceptionUtils.CheckArgumentNotNull<FilterClause>(filterClause, "filterClause");
			this.filterClause = filterClause;
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001664 RID: 5732 RVA: 0x0003EDD0 File Offset: 0x0003CFD0
		public FilterClause FilterClause
		{
			get
			{
				return this.filterClause;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001665 RID: 5733 RVA: 0x00038940 File Offset: 0x00036B40
		public override TransformationNodeKind Kind
		{
			get
			{
				return TransformationNodeKind.Filter;
			}
		}

		// Token: 0x04000A17 RID: 2583
		private readonly FilterClause filterClause;
	}
}
