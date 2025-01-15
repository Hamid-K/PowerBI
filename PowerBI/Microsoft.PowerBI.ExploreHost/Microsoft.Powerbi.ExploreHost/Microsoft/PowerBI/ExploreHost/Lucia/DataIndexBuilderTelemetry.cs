using System;
using Microsoft.Lucia.Core;

namespace Microsoft.PowerBI.ExploreHost.Lucia
{
	// Token: 0x02000053 RID: 83
	internal sealed class DataIndexBuilderTelemetry : DataIndexTelemetry
	{
		// Token: 0x0600027B RID: 635 RVA: 0x00008546 File Offset: 0x00006746
		public DataIndexBuilderTelemetry(LuciaSessionOptions options)
		{
			base.Options = options;
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600027C RID: 636 RVA: 0x00008555 File Offset: 0x00006755
		// (set) Token: 0x0600027D RID: 637 RVA: 0x0000855D File Offset: 0x0000675D
		public BuildDataIndexWarnings Warnings { get; set; }
	}
}
