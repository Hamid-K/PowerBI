using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.InfoNav.Common;

namespace Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations
{
	// Token: 0x02000138 RID: 312
	public sealed class NavigationPropertyGraphAnnotation
	{
		// Token: 0x0600080D RID: 2061 RVA: 0x00010B3C File Offset: 0x0000ED3C
		public NavigationPropertyGraphAnnotation(IDirectedGraph<IConceptualEntity> assocsFromOneGraph, IDirectedGraph<IConceptualEntity> strongAssocsFromOneGraph, IDirectedGraph<IConceptualEntity> assocsFromOneAndDirectedOneHopManyToManyGraph, IDirectedGraph<IConceptualEntity> assocsFromManyGraph, IDirectedGraph<IConceptualEntity> assocsFromOneWithBidirCrossFilteringGraph, IDirectedGraph<IConceptualEntity> assocsFromOneWithBidirCrossFilteringAndDirectedOneHopManyToManyGraph)
		{
			this.AssociationsFromOneGraph = assocsFromOneGraph;
			this.StrongAssociationsFromOneGraph = assocsFromOneGraph;
			this.AssociationsFromOneAndDirectedOneHopManyToManyGraph = assocsFromOneAndDirectedOneHopManyToManyGraph;
			this.AssociationsFromManyGraph = assocsFromManyGraph;
			this.AssociationsFromOneWithBidirCrossFilteringGraph = assocsFromOneWithBidirCrossFilteringGraph;
			this.AssociationsFromOneWithBidirCrossFilteringAndDirectedOneHopManyToManyGraph = assocsFromOneWithBidirCrossFilteringAndDirectedOneHopManyToManyGraph;
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x00010B71 File Offset: 0x0000ED71
		public IDirectedGraph<IConceptualEntity> AssociationsFromOneGraph { get; }

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x00010B79 File Offset: 0x0000ED79
		public IDirectedGraph<IConceptualEntity> StrongAssociationsFromOneGraph { get; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x00010B81 File Offset: 0x0000ED81
		public IDirectedGraph<IConceptualEntity> AssociationsFromOneAndDirectedOneHopManyToManyGraph { get; }

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x00010B89 File Offset: 0x0000ED89
		public IDirectedGraph<IConceptualEntity> AssociationsFromManyGraph { get; }

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x00010B91 File Offset: 0x0000ED91
		public IDirectedGraph<IConceptualEntity> AssociationsFromOneWithBidirCrossFilteringGraph { get; }

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x00010B99 File Offset: 0x0000ED99
		public IDirectedGraph<IConceptualEntity> AssociationsFromOneWithBidirCrossFilteringAndDirectedOneHopManyToManyGraph { get; }

		// Token: 0x06000814 RID: 2068 RVA: 0x00010BA1 File Offset: 0x0000EDA1
		public override string ToString()
		{
			return this.ToString(ConceptualEntityStringifier.Instance, null);
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00010BB0 File Offset: 0x0000EDB0
		public string ToString(IVertexStringifier<IConceptualEntity> vertexStringifier, IComparer<IConceptualEntity> comparer = null)
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.AppendGraph(stringBuilder, this.AssociationsFromOneGraph, "AssociationsFromOneGraph", vertexStringifier, comparer);
			this.AppendGraph(stringBuilder, this.StrongAssociationsFromOneGraph, "StrongAssociationsFromOneGraph", vertexStringifier, comparer);
			this.AppendGraph(stringBuilder, this.AssociationsFromOneAndDirectedOneHopManyToManyGraph, "AssociationsFromOneAndDirectedOneHopManyToManyGraph", vertexStringifier, comparer);
			this.AppendGraph(stringBuilder, this.AssociationsFromManyGraph, "AssociationsFromManyGraph", vertexStringifier, comparer);
			this.AppendGraph(stringBuilder, this.AssociationsFromOneWithBidirCrossFilteringGraph, "AssociationsFromOneWithBidirCrossFilteringGraph", vertexStringifier, comparer);
			this.AppendGraph(stringBuilder, this.AssociationsFromOneWithBidirCrossFilteringAndDirectedOneHopManyToManyGraph, "AssociationsFromOneWithBidirCrossFilteringAndDirectedOneHopManyToManyGraph", vertexStringifier, comparer);
			return stringBuilder.ToString();
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00010C41 File Offset: 0x0000EE41
		private void AppendGraph(StringBuilder stringRep, IDirectedGraph<IConceptualEntity> graph, string name, IVertexStringifier<IConceptualEntity> vertexStringifier, IComparer<IConceptualEntity> comparer)
		{
			if (graph.VertexCount == 0)
			{
				return;
			}
			stringRep.Append(name);
			stringRep.Append(graph.ToString(vertexStringifier, comparer));
			stringRep.AppendLine();
		}
	}
}
