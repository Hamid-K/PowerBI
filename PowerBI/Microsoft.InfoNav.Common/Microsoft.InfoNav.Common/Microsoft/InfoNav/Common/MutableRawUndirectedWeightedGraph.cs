using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x0200004F RID: 79
	[ImmutableObject(false)]
	internal sealed class MutableRawUndirectedWeightedGraph : MutableRawWeightedGraph
	{
		// Token: 0x06000353 RID: 851 RVA: 0x00009472 File Offset: 0x00007672
		internal MutableRawUndirectedWeightedGraph(int vertexCapacity, int edgeCapacity)
			: base(vertexCapacity, edgeCapacity)
		{
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000947C File Offset: 0x0000767C
		internal MutableRawUndirectedWeightedGraph(IRawWeightedGraph baseGraph)
			: base(baseGraph)
		{
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00009485 File Offset: 0x00007685
		protected override void AddArcs(int edgeId, int fromVertexId, int toVertexId)
		{
			base.AddArc(edgeId, fromVertexId, toVertexId);
			base.AddArc(edgeId, toVertexId, fromVertexId);
		}
	}
}
