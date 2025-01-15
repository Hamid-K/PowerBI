using System;

namespace Microsoft.AspNet.OData.Formatter.Serialization
{
	// Token: 0x020001A3 RID: 419
	public class EntitySelfLinks
	{
		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000DE5 RID: 3557 RVA: 0x00037CD8 File Offset: 0x00035ED8
		// (set) Token: 0x06000DE6 RID: 3558 RVA: 0x00037CE0 File Offset: 0x00035EE0
		public Uri IdLink { get; set; }

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000DE7 RID: 3559 RVA: 0x00037CE9 File Offset: 0x00035EE9
		// (set) Token: 0x06000DE8 RID: 3560 RVA: 0x00037CF1 File Offset: 0x00035EF1
		public Uri EditLink { get; set; }

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x00037CFA File Offset: 0x00035EFA
		// (set) Token: 0x06000DEA RID: 3562 RVA: 0x00037D02 File Offset: 0x00035F02
		public Uri ReadLink { get; set; }
	}
}
