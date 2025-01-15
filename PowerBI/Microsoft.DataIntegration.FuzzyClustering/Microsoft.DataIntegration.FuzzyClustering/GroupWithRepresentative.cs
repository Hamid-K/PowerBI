using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyClustering
{
	// Token: 0x02000007 RID: 7
	internal class GroupWithRepresentative
	{
		// Token: 0x0600001B RID: 27 RVA: 0x0000237A File Offset: 0x0000057A
		public GroupWithRepresentative(int repreId, int repreCount, List<int> rowIds)
		{
			this.RepreRowId = repreId;
			this.RepreCount = repreCount;
			this.RowIds = rowIds;
		}

		// Token: 0x04000009 RID: 9
		internal int RepreRowId;

		// Token: 0x0400000A RID: 10
		internal int RepreCount;

		// Token: 0x0400000B RID: 11
		internal List<int> RowIds;
	}
}
