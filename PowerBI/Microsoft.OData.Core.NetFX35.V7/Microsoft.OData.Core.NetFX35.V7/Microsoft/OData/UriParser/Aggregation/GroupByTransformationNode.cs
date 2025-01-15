using System;
using System.Collections.Generic;

namespace Microsoft.OData.UriParser.Aggregation
{
	// Token: 0x020001C7 RID: 455
	public sealed class GroupByTransformationNode : TransformationNode
	{
		// Token: 0x060011D7 RID: 4567 RVA: 0x00031F95 File Offset: 0x00030195
		public GroupByTransformationNode(IList<GroupByPropertyNode> groupingProperties, TransformationNode childTransformations, CollectionNode source)
		{
			ExceptionUtils.CheckArgumentNotNull<IList<GroupByPropertyNode>>(groupingProperties, "groupingProperties");
			this.groupingProperties = groupingProperties;
			this.childTransformations = childTransformations;
			this.source = source;
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x060011D8 RID: 4568 RVA: 0x00031FBE File Offset: 0x000301BE
		public IEnumerable<GroupByPropertyNode> GroupingProperties
		{
			get
			{
				return this.groupingProperties;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x060011D9 RID: 4569 RVA: 0x00031FC6 File Offset: 0x000301C6
		public TransformationNode ChildTransformations
		{
			get
			{
				return this.childTransformations;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x060011DA RID: 4570 RVA: 0x00031FCE File Offset: 0x000301CE
		public CollectionNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x060011DB RID: 4571 RVA: 0x00002503 File Offset: 0x00000703
		public override TransformationNodeKind Kind
		{
			get
			{
				return TransformationNodeKind.GroupBy;
			}
		}

		// Token: 0x04000913 RID: 2323
		private readonly CollectionNode source;

		// Token: 0x04000914 RID: 2324
		private readonly TransformationNode childTransformations;

		// Token: 0x04000915 RID: 2325
		private readonly IEnumerable<GroupByPropertyNode> groupingProperties;
	}
}
