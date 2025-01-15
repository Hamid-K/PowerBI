using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.BIServer.Owin.Common
{
	// Token: 0x02000009 RID: 9
	public static class SynchronizationContextExtensions
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00002324 File Offset: 0x00000524
		public static string GetAcceptLanguage(this SynchronizationContext context)
		{
			IOwinSynchronizationContext owinSynchronizationContext = context as IOwinSynchronizationContext;
			if (owinSynchronizationContext == null)
			{
				return null;
			}
			return owinSynchronizationContext.AcceptLanguages;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002344 File Offset: 0x00000544
		public static string GetAuthenticationHeader(this SynchronizationContext context)
		{
			IOwinSynchronizationContext owinSynchronizationContext = context as IOwinSynchronizationContext;
			if (owinSynchronizationContext == null)
			{
				return null;
			}
			return owinSynchronizationContext.AuthenticationHeader;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002364 File Offset: 0x00000564
		public static KeyValuePair<string, string> GetOAuthSessionCookie(this SynchronizationContext context)
		{
			IOwinSynchronizationContext owinSynchronizationContext = context as IOwinSynchronizationContext;
			if (owinSynchronizationContext == null)
			{
				return new KeyValuePair<string, string>(string.Empty, string.Empty);
			}
			return owinSynchronizationContext.OAuthSessionCookie;
		}
	}
}
