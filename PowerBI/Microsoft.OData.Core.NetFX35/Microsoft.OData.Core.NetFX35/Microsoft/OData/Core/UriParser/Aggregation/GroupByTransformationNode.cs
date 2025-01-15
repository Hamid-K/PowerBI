using System;
using System.Collections.Generic;
using Microsoft.OData.Core.UriParser.Semantic;

namespace Microsoft.OData.Core.UriParser.Aggregation
{
	// Token: 0x020002AB RID: 683
	public sealed class GroupByTransformationNode : TransformationNode
	{
		// Token: 0x06001798 RID: 6040 RVA: 0x00050CC2 File Offset: 0x0004EEC2
		public GroupByTransformationNode(IList<GroupByPropertyNode> groupingProperties, TransformationNode childTransformations, CollectionNode source)
		{
			ExceptionUtils.CheckArgumentNotNull<IList<GroupByPropertyNode>>(groupingProperties, "groupingProperties");
			this.groupingProperties = groupingProperties;
			this.childTransformations = childTransformations;
			this.source = source;
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001799 RID: 6041 RVA: 0x00050CEA File Offset: 0x0004EEEA
		public IEnumerable<GroupByPropertyNode> GroupingProperties
		{
			get
			{
				return this.groupingProperties;
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x0600179A RID: 6042 RVA: 0x00050CF2 File Offset: 0x0004EEF2
		public TransformationNode ChildTransformations
		{
			get
			{
				return this.childTransformations;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x0600179B RID: 6043 RVA: 0x00050CFA File Offset: 0x0004EEFA
		public CollectionNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x0600179C RID: 6044 RVA: 0x00050D02 File Offset: 0x0004EF02
		public override TransformationNodeKind Kind
		{
			get
			{
				return TransformationNodeKind.GroupBy;
			}
		}

		// Token: 0x04000A29 RID: 2601
		private readonly CollectionNode source;

		// Token: 0x04000A2A RID: 2602
		private readonly TransformationNode childTransformations;

		// Token: 0x04000A2B RID: 2603
		private readonly IEnumerable<GroupByPropertyNode> groupingProperties;
	}
}
