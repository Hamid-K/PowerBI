using System;
using System.Collections.Generic;

namespace Microsoft.OData.UriParser.Aggregation
{
	// Token: 0x020001F9 RID: 505
	public sealed class GroupByTransformationNode : TransformationNode
	{
		// Token: 0x0600167C RID: 5756 RVA: 0x0003F1FD File Offset: 0x0003D3FD
		public GroupByTransformationNode(IList<GroupByPropertyNode> groupingProperties, TransformationNode childTransformations, CollectionNode source)
		{
			ExceptionUtils.CheckArgumentNotNull<IList<GroupByPropertyNode>>(groupingProperties, "groupingProperties");
			this.groupingProperties = groupingProperties;
			this.childTransformations = childTransformations;
			this.source = source;
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x0600167D RID: 5757 RVA: 0x0003F226 File Offset: 0x0003D426
		public IEnumerable<GroupByPropertyNode> GroupingProperties
		{
			get
			{
				return this.groupingProperties;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x0600167E RID: 5758 RVA: 0x0003F22E File Offset: 0x0003D42E
		public TransformationNode ChildTransformations
		{
			get
			{
				return this.childTransformations;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x0600167F RID: 5759 RVA: 0x0003F236 File Offset: 0x0003D436
		public CollectionNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001680 RID: 5760 RVA: 0x00002393 File Offset: 0x00000593
		public override TransformationNodeKind Kind
		{
			get
			{
				return TransformationNodeKind.GroupBy;
			}
		}

		// Token: 0x04000A24 RID: 2596
		private readonly CollectionNode source;

		// Token: 0x04000A25 RID: 2597
		private readonly TransformationNode childTransformations;

		// Token: 0x04000A26 RID: 2598
		private readonly IEnumerable<GroupByPropertyNode> groupingProperties;
	}
}
