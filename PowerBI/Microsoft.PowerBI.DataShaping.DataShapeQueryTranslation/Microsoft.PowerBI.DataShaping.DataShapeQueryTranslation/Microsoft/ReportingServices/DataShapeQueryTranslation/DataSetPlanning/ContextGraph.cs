using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000D8 RID: 216
	internal sealed class ContextGraph
	{
		// Token: 0x060008E8 RID: 2280 RVA: 0x00022892 File Offset: 0x00020A92
		internal ContextGraph()
		{
			this.m_nodeMap = new Dictionary<IContextItem, ContextGraph.Node>();
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x000228A8 File Offset: 0x00020AA8
		public ContextGraph.Node GetNode(IContextItem item)
		{
			ContextGraph.Node node;
			this.TryGetNode(item, out node);
			return node;
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x000228C0 File Offset: 0x00020AC0
		public bool TryGetNode(IContextItem item, out ContextGraph.Node node)
		{
			return this.m_nodeMap.TryGetValue(item, out node);
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x000228D0 File Offset: 0x00020AD0
		public ContextGraph.Node GetOrCreateNode(IContextItem item)
		{
			ContextGraph.Node node;
			if (this.TryGetNode(item, out node))
			{
				return node;
			}
			node = new ContextGraph.Node(item);
			this.m_nodeMap.Add(item, node);
			return node;
		}

		// Token: 0x0400043F RID: 1087
		private readonly Dictionary<IContextItem, ContextGraph.Node> m_nodeMap;

		// Token: 0x020002AF RID: 687
		[DebuggerDisplay("[Item] Id={Item.Id} [EdgeCount={Edges.Count}]")]
		internal sealed class Node
		{
			// Token: 0x060015EC RID: 5612 RVA: 0x00050B28 File Offset: 0x0004ED28
			internal Node(IContextItem item)
			{
				this.m_item = item;
				this.m_edges = new List<ContextGraph.Edge>(2);
			}

			// Token: 0x170003E6 RID: 998
			// (get) Token: 0x060015ED RID: 5613 RVA: 0x00050B43 File Offset: 0x0004ED43
			public IContextItem Item
			{
				get
				{
					return this.m_item;
				}
			}

			// Token: 0x170003E7 RID: 999
			// (get) Token: 0x060015EE RID: 5614 RVA: 0x00050B4B File Offset: 0x0004ED4B
			public List<ContextGraph.Edge> Edges
			{
				get
				{
					return this.m_edges;
				}
			}

			// Token: 0x04000A48 RID: 2632
			private readonly IContextItem m_item;

			// Token: 0x04000A49 RID: 2633
			private readonly List<ContextGraph.Edge> m_edges;
		}

		// Token: 0x020002B0 RID: 688
		[DebuggerDisplay("[End] Id={End.Item.Id} [State={State}]")]
		internal sealed class Edge
		{
			// Token: 0x060015EF RID: 5615 RVA: 0x00050B53 File Offset: 0x0004ED53
			internal Edge(ContextGraph.Node end, ContextState state, ContextState? requiredEntryState)
			{
				this.m_end = end;
				this.m_state = state;
				this.m_requiredEntryState = requiredEntryState;
			}

			// Token: 0x170003E8 RID: 1000
			// (get) Token: 0x060015F0 RID: 5616 RVA: 0x00050B70 File Offset: 0x0004ED70
			public ContextGraph.Node End
			{
				get
				{
					return this.m_end;
				}
			}

			// Token: 0x170003E9 RID: 1001
			// (get) Token: 0x060015F1 RID: 5617 RVA: 0x00050B78 File Offset: 0x0004ED78
			public ContextState State
			{
				get
				{
					return this.m_state;
				}
			}

			// Token: 0x170003EA RID: 1002
			// (get) Token: 0x060015F2 RID: 5618 RVA: 0x00050B80 File Offset: 0x0004ED80
			public ContextState? RequiredEntryState
			{
				get
				{
					return this.m_requiredEntryState;
				}
			}

			// Token: 0x060015F3 RID: 5619 RVA: 0x00050B88 File Offset: 0x0004ED88
			public bool ShouldTraverse(ContextState entryState)
			{
				if (this.m_requiredEntryState != null)
				{
					ContextState? requiredEntryState = this.m_requiredEntryState;
					return (requiredEntryState.GetValueOrDefault() == entryState) & (requiredEntryState != null);
				}
				return true;
			}

			// Token: 0x04000A4A RID: 2634
			private readonly ContextGraph.Node m_end;

			// Token: 0x04000A4B RID: 2635
			private readonly ContextState m_state;

			// Token: 0x04000A4C RID: 2636
			private readonly ContextState? m_requiredEntryState;
		}
	}
}
