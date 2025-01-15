using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace Microsoft.BIServer.Owin.Common.Services
{
	// Token: 0x02000015 RID: 21
	public sealed class OwinSynchronizationContext : SynchronizationContext, IOwinSynchronizationContext
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00002CCB File Offset: 0x00000ECB
		private OwinSynchronizationContext()
		{
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002CD3 File Offset: 0x00000ED3
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002CDB File Offset: 0x00000EDB
		public string AcceptLanguages { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002CE4 File Offset: 0x00000EE4
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00002CEC File Offset: 0x00000EEC
		public CultureInfo ThreadCultureInfo { get; private set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002CF5 File Offset: 0x00000EF5
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002CFD File Offset: 0x00000EFD
		public string AuthenticationHeader { get; private set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002D06 File Offset: 0x00000F06
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00002D0E File Offset: 0x00000F0E
		public KeyValuePair<string, string> OAuthSessionCookie { get; private set; }

		// Token: 0x0600005B RID: 91 RVA: 0x00002D17 File Offset: 0x00000F17
		public static void SetLanguageInfo(CultureInfo threadCultureInfo, string acceptLanguages)
		{
			OwinSynchronizationContext owinSynchronizationContext = (SynchronizationContext.Current as OwinSynchronizationContext) ?? new OwinSynchronizationContext();
			owinSynchronizationContext.ThreadCultureInfo = threadCultureInfo;
			owinSynchronizationContext.AcceptLanguages = acceptLanguages;
			owinSynchronizationContext.Apply();
			SynchronizationContext.SetSynchronizationContext(owinSynchronizationContext);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002D45 File Offset: 0x00000F45
		public static void SetAuthenticationHeader(string authenticationHeader)
		{
			OwinSynchronizationContext owinSynchronizationContext = (SynchronizationContext.Current as OwinSynchronizationContext) ?? new OwinSynchronizationContext();
			owinSynchronizationContext.AuthenticationHeader = authenticationHeader;
			owinSynchronizationContext.Apply();
			SynchronizationContext.SetSynchronizationContext(owinSynchronizationContext);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002D6C File Offset: 0x00000F6C
		public static void SetOAuthSessionCookie(string name, string value)
		{
			OwinSynchronizationContext owinSynchronizationContext = (SynchronizationContext.Current as OwinSynchronizationContext) ?? new OwinSynchronizationContext();
			owinSynchronizationContext.OAuthSessionCookie = new KeyValuePair<string, string>(name, value);
			owinSynchronizationContext.Apply();
			SynchronizationContext.SetSynchronizationContext(owinSynchronizationContext);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002D9C File Offset: 0x00000F9C
		public override void Post(SendOrPostCallback callback, object state)
		{
			base.Post(delegate(object s)
			{
				this.Apply();
				SynchronizationContext.SetSynchronizationContext(this.CreateCopy());
				callback(s);
			}, state);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002DD0 File Offset: 0x00000FD0
		private void Apply()
		{
			if (this.ThreadCultureInfo != null)
			{
				Thread.CurrentThread.CurrentCulture = this.ThreadCultureInfo;
				Thread.CurrentThread.CurrentUICulture = this.ThreadCultureInfo;
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002DFA File Offset: 0x00000FFA
		public override SynchronizationContext CreateCopy()
		{
			return new OwinSynchronizationContext
			{
				ThreadCultureInfo = this.ThreadCultureInfo,
				AcceptLanguages = this.AcceptLanguages,
				AuthenticationHeader = this.AuthenticationHeader,
				OAuthSessionCookie = this.OAuthSessionCookie
			};
		}
	}
}
