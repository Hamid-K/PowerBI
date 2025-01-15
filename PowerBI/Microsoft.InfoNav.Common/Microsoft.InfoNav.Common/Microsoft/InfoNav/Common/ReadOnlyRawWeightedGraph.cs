using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x02000056 RID: 86
	[ImmutableObject(true)]
	public sealed class ReadOnlyRawWeightedGraph : RawWeightedGraph
	{
		// Token: 0x0600037C RID: 892 RVA: 0x00009960 File Offset: 0x00007B60
		private ReadOnlyRawWeightedGraph()
			: base(0, 0)
		{
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000996A File Offset: 0x00007B6A
		public ReadOnlyRawWeightedGraph(IRawWeightedGraph baseGraph)
			: base(baseGraph)
		{
		}

		// Token: 0x040000BC RID: 188
		public static readonly ReadOnlyRawWeightedGraph Empty = new ReadOnlyRawWeightedGraph();
	}
}
