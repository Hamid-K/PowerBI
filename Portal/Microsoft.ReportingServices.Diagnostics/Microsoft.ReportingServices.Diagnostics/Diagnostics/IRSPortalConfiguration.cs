using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200002A RID: 42
	internal interface IRSPortalConfiguration
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600009B RID: 155
		AuthenticationTypes AuthenticationTypes { get; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600009C RID: 156
		LogonMethod LogonMethod { get; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600009D RID: 157
		string AuthRealm { get; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600009E RID: 158
		string AuthDomain { get; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600009F RID: 159
		bool AuthPersistence { get; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000A0 RID: 160
		int MaxActiveReqForOneUser { get; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000A1 RID: 161
		string ReportServerUrlCalculated { get; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000A2 RID: 162
		string ReportServerVirtualDirectory { get; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000A3 RID: 163
		string ReportServerUrlHostnameCalculated { get; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000A4 RID: 164
		StringCollection PassthroughCookies { get; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000A5 RID: 165
		Dictionary<RunningApplication, UrlConfiguration> UrlConfiguration { get; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000A6 RID: 166
		IOAuthConfiguration OAuthConfiguration { get; }
	}
}
