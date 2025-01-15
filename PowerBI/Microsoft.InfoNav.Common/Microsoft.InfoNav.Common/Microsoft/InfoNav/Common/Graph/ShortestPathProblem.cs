using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Microsoft.InfoNav.Common.Graph
{
	// Token: 0x02000082 RID: 130
	[ImmutableObject(true)]
	public sealed class ShortestPathProblem
	{
		// Token: 0x060004C9 RID: 1225 RVA: 0x0000C715 File Offset: 0x0000A915
		public ShortestPathProblem(IRawWeightedGraph graph, ISet<int> terminals, IEnumerable<int> searchRoots)
		{
			this.Graph = graph;
			this.SearchTerminals = terminals.AsReadOnlySet(null);
			this.SearchRoots = searchRoots.AsReadOnlyCollection<int>();
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x0000C73D File Offset: 0x0000A93D
		public IRawWeightedGraph Graph { get; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x0000C745 File Offset: 0x0000A945
		public ReadOnlySet<int> SearchTerminals { get; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x0000C74D File Offset: 0x0000A94D
		public ReadOnlyCollection<int> SearchRoots { get; }
	}
}
