using System;
using System.Net;
using Microsoft.ReportingServices.CatalogAccess;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Portal.Interfaces.Configuration;
using Microsoft.ReportingServices.Portal.Interfaces.Enums;

namespace Microsoft.ReportingServices.Portal.Services.Configuration
{
	// Token: 0x0200006E RID: 110
	internal sealed class PortalConfiguration : IPortalConfiguration
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000338 RID: 824 RVA: 0x000152F8 File Offset: 0x000134F8
		// (set) Token: 0x06000339 RID: 825 RVA: 0x00015300 File Offset: 0x00013500
		public bool AppConfigured { get; internal set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600033A RID: 826 RVA: 0x00015309 File Offset: 0x00013509
		// (set) Token: 0x0600033B RID: 827 RVA: 0x00015311 File Offset: 0x00013511
		public string ReportServerWebAppVirtualDirectory { get; internal set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600033C RID: 828 RVA: 0x0001531A File Offset: 0x0001351A
		// (set) Token: 0x0600033D RID: 829 RVA: 0x00015322 File Offset: 0x00013522
		public string[] RegisteredWebAppUrls { get; internal set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600033E RID: 830 RVA: 0x0001532B File Offset: 0x0001352B
		// (set) Token: 0x0600033F RID: 831 RVA: 0x00015333 File Offset: 0x00013533
		public string[] RegisteredReportServerUrls { get; internal set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0001533C File Offset: 0x0001353C
		// (set) Token: 0x06000341 RID: 833 RVA: 0x00015344 File Offset: 0x00013544
		public string ReportServerHostName { get; internal set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0001534D File Offset: 0x0001354D
		// (set) Token: 0x06000343 RID: 835 RVA: 0x00015355 File Offset: 0x00013555
		public string ReportServerUrl { get; internal set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000344 RID: 836 RVA: 0x0001535E File Offset: 0x0001355E
		// (set) Token: 0x06000345 RID: 837 RVA: 0x00015366 File Offset: 0x00013566
		public string PowerBIUrl { get; internal set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000346 RID: 838 RVA: 0x0001536F File Offset: 0x0001356F
		// (set) Token: 0x06000347 RID: 839 RVA: 0x00015377 File Offset: 0x00013577
		public string ReportServerVirtualDirectory { get; internal set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000348 RID: 840 RVA: 0x00015380 File Offset: 0x00013580
		// (set) Token: 0x06000349 RID: 841 RVA: 0x00015388 File Offset: 0x00013588
		public AuthenticationSchemes AuthenticationSchemes { get; internal set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600034A RID: 842 RVA: 0x00015391 File Offset: 0x00013591
		// (set) Token: 0x0600034B RID: 843 RVA: 0x00015399 File Offset: 0x00013599
		public int AuthenticationTypes { get; internal set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600034C RID: 844 RVA: 0x000153A2 File Offset: 0x000135A2
		// (set) Token: 0x0600034D RID: 845 RVA: 0x000153AA File Offset: 0x000135AA
		public bool AuthPersistence { get; internal set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600034E RID: 846 RVA: 0x000153B3 File Offset: 0x000135B3
		// (set) Token: 0x0600034F RID: 847 RVA: 0x000153BB File Offset: 0x000135BB
		public string[] PassthroughCookies { get; internal set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000350 RID: 848 RVA: 0x000153C4 File Offset: 0x000135C4
		// (set) Token: 0x06000351 RID: 849 RVA: 0x000153CC File Offset: 0x000135CC
		public LogonType BasicAuthenticationLogonType { get; internal set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000352 RID: 850 RVA: 0x000153D5 File Offset: 0x000135D5
		// (set) Token: 0x06000353 RID: 851 RVA: 0x000153DD File Offset: 0x000135DD
		public string BasicAuthenticationRealm { get; internal set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000354 RID: 852 RVA: 0x000153E6 File Offset: 0x000135E6
		// (set) Token: 0x06000355 RID: 853 RVA: 0x000153EE File Offset: 0x000135EE
		public string BasicAuthenticationDomain { get; internal set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000356 RID: 854 RVA: 0x000153F7 File Offset: 0x000135F7
		// (set) Token: 0x06000357 RID: 855 RVA: 0x000153FF File Offset: 0x000135FF
		public int MaxActiveReqForOneUser { get; internal set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000358 RID: 856 RVA: 0x00015408 File Offset: 0x00013608
		// (set) Token: 0x06000359 RID: 857 RVA: 0x00015410 File Offset: 0x00013610
		public string InstanceID { get; internal set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600035A RID: 858 RVA: 0x00015419 File Offset: 0x00013619
		// (set) Token: 0x0600035B RID: 859 RVA: 0x00015421 File Offset: 0x00013621
		public Guid InstallationId { get; internal set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0001542A File Offset: 0x0001362A
		// (set) Token: 0x0600035D RID: 861 RVA: 0x00015432 File Offset: 0x00013632
		public IOAuthConfiguration OAuthConfiguration { get; internal set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600035E RID: 862 RVA: 0x0001543B File Offset: 0x0001363B
		// (set) Token: 0x0600035F RID: 863 RVA: 0x00015443 File Offset: 0x00013643
		public ICatalogConfiguration CatalogConfiguration { get; internal set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0001544C File Offset: 0x0001364C
		// (set) Token: 0x06000361 RID: 865 RVA: 0x00015454 File Offset: 0x00013654
		public IFileSizeRestrictions FileSizeRestrictions { get; internal set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0001545D File Offset: 0x0001365D
		// (set) Token: 0x06000363 RID: 867 RVA: 0x00015465 File Offset: 0x00013665
		public bool LogClientIPAddress { get; internal set; }
	}
}
