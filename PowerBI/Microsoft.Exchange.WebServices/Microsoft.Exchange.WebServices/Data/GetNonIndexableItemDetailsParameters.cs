using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200027C RID: 636
	public sealed class GetNonIndexableItemDetailsParameters : NonIndexableItemParameters
	{
		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001648 RID: 5704 RVA: 0x0003DC24 File Offset: 0x0003CC24
		// (set) Token: 0x06001649 RID: 5705 RVA: 0x0003DC2C File Offset: 0x0003CC2C
		public int? PageSize { get; set; }

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x0600164A RID: 5706 RVA: 0x0003DC35 File Offset: 0x0003CC35
		// (set) Token: 0x0600164B RID: 5707 RVA: 0x0003DC3D File Offset: 0x0003CC3D
		public string PageItemReference { get; set; }

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x0600164C RID: 5708 RVA: 0x0003DC46 File Offset: 0x0003CC46
		// (set) Token: 0x0600164D RID: 5709 RVA: 0x0003DC4E File Offset: 0x0003CC4E
		public SearchPageDirection? PageDirection { get; set; }
	}
}
