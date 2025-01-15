using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Utils.Clustering
{
	// Token: 0x020005FD RID: 1533
	public struct ClusterConstraints
	{
		// Token: 0x0600216A RID: 8554 RVA: 0x0005EF51 File Offset: 0x0005D151
		public ClusterConstraints(IReadOnlyList<IReadOnlyList<object>> inSameCluster, IReadOnlyList<IReadOnlyList<object>> inDifferentCluster)
		{
			this.InSameCluster = inSameCluster;
			this.InDifferentCluster = inDifferentCluster;
		}

		// Token: 0x04000FD7 RID: 4055
		internal IReadOnlyList<IReadOnlyList<object>> InSameCluster;

		// Token: 0x04000FD8 RID: 4056
		internal IReadOnlyList<IReadOnlyList<object>> InDifferentCluster;
	}
}
