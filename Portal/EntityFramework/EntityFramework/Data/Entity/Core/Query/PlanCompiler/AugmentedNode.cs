using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000331 RID: 817
	internal class AugmentedNode
	{
		// Token: 0x060026FA RID: 9978 RVA: 0x00071177 File Offset: 0x0006F377
		internal AugmentedNode(int id, Node node)
			: this(id, node, new List<AugmentedNode>())
		{
		}

		// Token: 0x060026FB RID: 9979 RVA: 0x00071188 File Offset: 0x0006F388
		internal AugmentedNode(int id, Node node, List<AugmentedNode> children)
		{
			this.m_id = id;
			this.m_node = node;
			this.m_children = children;
			PlanCompiler.Assert(children != null, "null children (gasp!)");
			foreach (AugmentedNode augmentedNode in this.m_children)
			{
				augmentedNode.m_parent = this;
			}
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x060026FC RID: 9980 RVA: 0x00071210 File Offset: 0x0006F410
		internal int Id
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x060026FD RID: 9981 RVA: 0x00071218 File Offset: 0x0006F418
		internal Node Node
		{
			get
			{
				return this.m_node;
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x060026FE RID: 9982 RVA: 0x00071220 File Offset: 0x0006F420
		internal AugmentedNode Parent
		{
			get
			{
				return this.m_parent;
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x060026FF RID: 9983 RVA: 0x00071228 File Offset: 0x0006F428
		internal List<AugmentedNode> Children
		{
			get
			{
				return this.m_children;
			}
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06002700 RID: 9984 RVA: 0x00071230 File Offset: 0x0006F430
		internal List<JoinEdge> JoinEdges
		{
			get
			{
				return this.m_joinEdges;
			}
		}

		// Token: 0x04000D97 RID: 3479
		private readonly int m_id;

		// Token: 0x04000D98 RID: 3480
		private readonly Node m_node;

		// Token: 0x04000D99 RID: 3481
		protected AugmentedNode m_parent;

		// Token: 0x04000D9A RID: 3482
		private readonly List<AugmentedNode> m_children;

		// Token: 0x04000D9B RID: 3483
		private readonly List<JoinEdge> m_joinEdges = new List<JoinEdge>();
	}
}
