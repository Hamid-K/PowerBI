using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x0200004E RID: 78
	[ImmutableObject(false)]
	internal sealed class MutableRawDirectedWeightedGraph : MutableRawWeightedGraph
	{
		// Token: 0x06000351 RID: 849 RVA: 0x0000945D File Offset: 0x0000765D
		internal MutableRawDirectedWeightedGraph(int vertexCapacity, int edgeCapacity)
			: base(vertexCapacity, edgeCapacity)
		{
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00009467 File Offset: 0x00007667
		protected override void AddArcs(int edgeId, int fromVertexId, int toVertexId)
		{
			base.AddArc(edgeId, fromVertexId, toVertexId);
		}
	}
}
