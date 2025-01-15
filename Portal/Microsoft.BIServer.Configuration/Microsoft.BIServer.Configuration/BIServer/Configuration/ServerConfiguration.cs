using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;

namespace Microsoft.BIServer.Configuration
{
	// Token: 0x0200000C RID: 12
	public class ServerConfiguration
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000027D8 File Offset: 0x000009D8
		// (set) Token: 0x06000026 RID: 38 RVA: 0x000027E0 File Offset: 0x000009E0
		public string LoginUrl { get; internal set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000027E9 File Offset: 0x000009E9
		// (set) Token: 0x06000028 RID: 40 RVA: 0x000027F1 File Offset: 0x000009F1
		public string FormsCookieName { get; internal set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000027FA File Offset: 0x000009FA
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002802 File Offset: 0x00000A02
		public string FormsCookiePath { get; internal set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000280B File Offset: 0x00000A0B
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002813 File Offset: 0x00000A13
		public int FormsCookieTimeoutInMinutes { get; internal set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002D RID: 45 RVA: 0x0000281C File Offset: 0x00000A1C
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002824 File Offset: 0x00000A24
		public bool FormsCookieSlidingExpiration { get; internal set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002F RID: 47 RVA: 0x0000282D File Offset: 0x00000A2D
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00002835 File Offset: 0x00000A35
		public AuthenticationType AuthenticationMode { get; internal set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000031 RID: 49 RVA: 0x0000283E File Offset: 0x00000A3E
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002846 File Offset: 0x00000A46
		public AuthenticationTypes AuthenticationTypes { get; internal set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000284F File Offset: 0x00000A4F
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002857 File Offset: 0x00000A57
		public StringCollection PassthroughCookies { get; internal set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002860 File Offset: 0x00000A60
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002868 File Offset: 0x00000A68
		public IReadOnlyCollection<Extension> AuthenticationExtensions { get; internal set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002871 File Offset: 0x00000A71
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00002879 File Offset: 0x00000A79
		public LogonType BasicAuthenticationLogonType { get; internal set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002882 File Offset: 0x00000A82
		// (set) Token: 0x0600003A RID: 58 RVA: 0x0000288A File Offset: 0x00000A8A
		public string BasicAuthenticationDomain { get; internal set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002893 File Offset: 0x00000A93
		// (set) Token: 0x0600003C RID: 60 RVA: 0x0000289B File Offset: 0x00000A9B
		public string BasicAuthenticationRealm { get; internal set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000028A4 File Offset: 0x00000AA4
		// (set) Token: 0x0600003E RID: 62 RVA: 0x000028AC File Offset: 0x00000AAC
		public bool AuthPersistence { get; internal set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000028B5 File Offset: 0x00000AB5
		// (set) Token: 0x06000040 RID: 64 RVA: 0x000028BD File Offset: 0x00000ABD
		public AuthenticationSchemes AuthenticationSchemes { get; internal set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000028C6 File Offset: 0x00000AC6
		// (set) Token: 0x06000042 RID: 66 RVA: 0x000028CE File Offset: 0x00000ACE
		public int MaxActiveReqForOneUser { get; internal set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000028D7 File Offset: 0x00000AD7
		// (set) Token: 0x06000044 RID: 68 RVA: 0x000028DF File Offset: 0x00000ADF
		public ServiceSettings Service { get; internal set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000028E8 File Offset: 0x00000AE8
		// (set) Token: 0x06000046 RID: 70 RVA: 0x000028F0 File Offset: 0x00000AF0
		public string ReportServerVirtualDirectory { get; internal set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000028F9 File Offset: 0x00000AF9
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002901 File Offset: 0x00000B01
		public string ReportServerWebAppVirtualDirectory { get; internal set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000049 RID: 73 RVA: 0x0000290A File Offset: 0x00000B0A
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002912 File Offset: 0x00000B12
		public IEnumerable<string> WebAppUrls { get; internal set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004B RID: 75 RVA: 0x0000291B File Offset: 0x00000B1B
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00002923 File Offset: 0x00000B23
		public IEnumerable<string> ReportServerUrls { get; internal set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004D RID: 77 RVA: 0x0000292C File Offset: 0x00000B2C
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00002934 File Offset: 0x00000B34
		public string ReportServerUrlOverride { get; internal set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004F RID: 79 RVA: 0x0000293D File Offset: 0x00000B3D
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00002945 File Offset: 0x00000B45
		public string PortalUrlOverride { get; internal set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000051 RID: 81 RVA: 0x0000294E File Offset: 0x00000B4E
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002956 File Offset: 0x00000B56
		public MachineKeySettings MachineKey { get; internal set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000053 RID: 83 RVA: 0x0000295F File Offset: 0x00000B5F
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002967 File Offset: 0x00000B67
		public int SecureConnectionLevel { get; internal set; }
	}
}
