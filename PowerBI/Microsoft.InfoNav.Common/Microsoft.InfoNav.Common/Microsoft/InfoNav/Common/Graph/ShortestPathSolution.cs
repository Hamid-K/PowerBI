using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.InfoNav.Common.Graph
{
	// Token: 0x02000083 RID: 131
	[ImmutableObject(true)]
	public sealed class ShortestPathSolution
	{
		// Token: 0x060004CD RID: 1229 RVA: 0x0000C755 File Offset: 0x0000A955
		public ShortestPathSolution(ISet<int> edges, bool hasDisconnectedIslands)
		{
			this.Edges = edges.AsReadOnlySet(null);
			this.HasDisconnectedIslands = hasDisconnectedIslands;
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x0000C771 File Offset: 0x0000A971
		public ReadOnlySet<int> Edges { get; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x0000C779 File Offset: 0x0000A979
		public bool HasDisconnectedIslands { get; }
	}
}
