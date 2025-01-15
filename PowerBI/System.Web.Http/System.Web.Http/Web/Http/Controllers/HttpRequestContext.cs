using System;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Web.Http.Routing;

namespace System.Web.Http.Controllers
{
	// Token: 0x020000F5 RID: 245
	public class HttpRequestContext
	{
		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x00010214 File Offset: 0x0000E414
		// (set) Token: 0x06000658 RID: 1624 RVA: 0x0001021C File Offset: 0x0000E41C
		public virtual X509Certificate2 ClientCertificate { get; set; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x00010225 File Offset: 0x0000E425
		// (set) Token: 0x0600065A RID: 1626 RVA: 0x0001022D File Offset: 0x0000E42D
		public virtual HttpConfiguration Configuration { get; set; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x00010236 File Offset: 0x0000E436
		// (set) Token: 0x0600065C RID: 1628 RVA: 0x0001023E File Offset: 0x0000E43E
		public virtual bool IncludeErrorDetail { get; set; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x00010247 File Offset: 0x0000E447
		// (set) Token: 0x0600065E RID: 1630 RVA: 0x0001024F File Offset: 0x0000E44F
		public virtual bool IsLocal { get; set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x00010258 File Offset: 0x0000E458
		// (set) Token: 0x06000660 RID: 1632 RVA: 0x00010260 File Offset: 0x0000E460
		public virtual IPrincipal Principal { get; set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x00010269 File Offset: 0x0000E469
		// (set) Token: 0x06000662 RID: 1634 RVA: 0x00010271 File Offset: 0x0000E471
		public virtual IHttpRouteData RouteData { get; set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x0001027A File Offset: 0x0000E47A
		// (set) Token: 0x06000664 RID: 1636 RVA: 0x00010282 File Offset: 0x0000E482
		public virtual UrlHelper Url { get; set; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x0001028B File Offset: 0x0000E48B
		// (set) Token: 0x06000666 RID: 1638 RVA: 0x00010293 File Offset: 0x0000E493
		public virtual string VirtualPathRoot { get; set; }
	}
}
