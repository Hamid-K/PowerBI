using System;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000FA RID: 250
	public class DeserializeOptions
	{
		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x0600103C RID: 4156 RVA: 0x000784D7 File Offset: 0x000766D7
		// (set) Token: 0x0600103D RID: 4157 RVA: 0x000784DF File Offset: 0x000766DF
		public bool PartitionsMergedWithTable { get; set; }

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x0600103E RID: 4158 RVA: 0x000784E8 File Offset: 0x000766E8
		// (set) Token: 0x0600103F RID: 4159 RVA: 0x000784F0 File Offset: 0x000766F0
		internal bool ErrorOnUnresolvedLinks { get; set; }
	}
}
