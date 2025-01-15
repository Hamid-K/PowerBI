using System;
using System.Threading;

namespace Microsoft.Identity.Client.UI
{
	// Token: 0x020001D9 RID: 473
	internal class CoreUIParent
	{
		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x0600148D RID: 5261 RVA: 0x00045AEA File Offset: 0x00043CEA
		// (set) Token: 0x0600148E RID: 5262 RVA: 0x00045AF2 File Offset: 0x00043CF2
		internal SynchronizationContext SynchronizationContext { get; set; }

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x0600148F RID: 5263 RVA: 0x00045AFB File Offset: 0x00043CFB
		// (set) Token: 0x06001490 RID: 5264 RVA: 0x00045B03 File Offset: 0x00043D03
		internal SystemWebViewOptions SystemWebViewOptions { get; set; }

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06001491 RID: 5265 RVA: 0x00045B0C File Offset: 0x00043D0C
		// (set) Token: 0x06001492 RID: 5266 RVA: 0x00045B14 File Offset: 0x00043D14
		internal EmbeddedWebViewOptions EmbeddedWebviewOptions { get; set; }

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06001493 RID: 5267 RVA: 0x00045B1D File Offset: 0x00043D1D
		// (set) Token: 0x06001494 RID: 5268 RVA: 0x00045B25 File Offset: 0x00043D25
		internal bool UseHiddenBrowser { get; set; }

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06001495 RID: 5269 RVA: 0x00045B2E File Offset: 0x00043D2E
		// (set) Token: 0x06001496 RID: 5270 RVA: 0x00045B36 File Offset: 0x00043D36
		internal object OwnerWindow { get; set; }
	}
}
