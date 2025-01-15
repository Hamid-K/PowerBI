using System;
using System.Net;
using Microsoft.ReportingServices.CatalogAccess;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Portal.Interfaces.Enums;

namespace Microsoft.ReportingServices.Portal.Interfaces.Configuration
{
	// Token: 0x020000C6 RID: 198
	public interface IPortalConfiguration
	{
		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000594 RID: 1428
		bool AppConfigured { get; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000595 RID: 1429
		string ReportServerWebAppVirtualDirectory { get; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000596 RID: 1430
		string[] RegisteredWebAppUrls { get; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000597 RID: 1431
		string[] RegisteredReportServerUrls { get; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000598 RID: 1432
		string ReportServerHostName { get; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000599 RID: 1433
		string ReportServerUrl { get; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x0600059A RID: 1434
		string PowerBIUrl { get; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600059B RID: 1435
		string ReportServerVirtualDirectory { get; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600059C RID: 1436
		AuthenticationSchemes AuthenticationSchemes { get; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600059D RID: 1437
		int AuthenticationTypes { get; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600059E RID: 1438
		bool AuthPersistence { get; }

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x0600059F RID: 1439
		string[] PassthroughCookies { get; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060005A0 RID: 1440
		LogonType BasicAuthenticationLogonType { get; }

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060005A1 RID: 1441
		string BasicAuthenticationRealm { get; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060005A2 RID: 1442
		string BasicAuthenticationDomain { get; }

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060005A3 RID: 1443
		int MaxActiveReqForOneUser { get; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060005A4 RID: 1444
		string InstanceID { get; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060005A5 RID: 1445
		Guid InstallationId { get; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060005A6 RID: 1446
		IOAuthConfiguration OAuthConfiguration { get; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060005A7 RID: 1447
		ICatalogConfiguration CatalogConfiguration { get; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060005A8 RID: 1448
		IFileSizeRestrictions FileSizeRestrictions { get; }

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060005A9 RID: 1449
		bool LogClientIPAddress { get; }
	}
}
