using System;

namespace Microsoft.ReportingServices.Hybrid.OAuth
{
	// Token: 0x02000006 RID: 6
	internal interface IAadOAuthProvider
	{
		// Token: 0x06000009 RID: 9
		ServiceToken AcquireToken();

		// Token: 0x0600000A RID: 10
		string GetAuthorizationUrl();

		// Token: 0x0600000B RID: 11
		bool UpdateToken(Uri uri);
	}
}
