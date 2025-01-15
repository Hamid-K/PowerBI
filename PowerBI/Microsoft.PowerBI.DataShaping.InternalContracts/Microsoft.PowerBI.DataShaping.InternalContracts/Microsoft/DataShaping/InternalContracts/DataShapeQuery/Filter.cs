using System;
using System.Diagnostics;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x02000092 RID: 146
	[DebuggerDisplay("[Filter] Target={Target}")]
	internal sealed class Filter
	{
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600037C RID: 892 RVA: 0x00006FE2 File Offset: 0x000051E2
		// (set) Token: 0x0600037D RID: 893 RVA: 0x00006FEA File Offset: 0x000051EA
		public Expression Target { get; set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600037E RID: 894 RVA: 0x00006FF3 File Offset: 0x000051F3
		// (set) Token: 0x0600037F RID: 895 RVA: 0x00006FFB File Offset: 0x000051FB
		public FilterCondition Condition { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000380 RID: 896 RVA: 0x00007004 File Offset: 0x00005204
		// (set) Token: 0x06000381 RID: 897 RVA: 0x0000700C File Offset: 0x0000520C
		public FilterUsageKind UsageKind { get; set; }
	}
}
