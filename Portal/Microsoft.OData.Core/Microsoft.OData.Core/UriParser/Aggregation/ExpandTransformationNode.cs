using System;

namespace Microsoft.OData.UriParser.Aggregation
{
	// Token: 0x020001EB RID: 491
	public sealed class ExpandTransformationNode : TransformationNode
	{
		// Token: 0x0600163F RID: 5695 RVA: 0x0003E351 File Offset: 0x0003C551
		public ExpandTransformationNode(SelectExpandClause expandClause)
		{
			ExceptionUtils.CheckArgumentNotNull<SelectExpandClause>(expandClause, "expandClause");
			this.expandClause = expandClause;
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06001640 RID: 5696 RVA: 0x0003E36C File Offset: 0x0003C56C
		public SelectExpandClause ExpandClause
		{
			get
			{
				return this.expandClause;
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06001641 RID: 5697 RVA: 0x000384BC File Offset: 0x000366BC
		public override TransformationNodeKind Kind
		{
			get
			{
				return TransformationNodeKind.Expand;
			}
		}

		// Token: 0x04000A08 RID: 2568
		private readonly SelectExpandClause expandClause;
	}
}
