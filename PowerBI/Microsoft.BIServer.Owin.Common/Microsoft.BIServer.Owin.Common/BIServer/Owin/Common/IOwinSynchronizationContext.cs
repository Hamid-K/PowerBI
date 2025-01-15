using System;
using System.Collections.Generic;

namespace Microsoft.BIServer.Owin.Common
{
	// Token: 0x02000006 RID: 6
	public interface IOwinSynchronizationContext
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000F RID: 15
		string AcceptLanguages { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000010 RID: 16
		string AuthenticationHeader { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000011 RID: 17
		KeyValuePair<string, string> OAuthSessionCookie { get; }
	}
}
