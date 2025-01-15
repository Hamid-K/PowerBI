using System;
using System.Net;
using Microsoft.Cloud.Platform.MonitoredUtils;

namespace Microsoft.Cloud.Platform.RequestProtection
{
	// Token: 0x02000467 RID: 1127
	public interface IRequestProtectionContext : IHttpRequestContext, IHttpRequestMessage, IHttpResponseMessage
	{
		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06002323 RID: 8995
		// (set) Token: 0x06002324 RID: 8996
		string EndpointIdentifier { get; set; }

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06002325 RID: 8997
		// (set) Token: 0x06002326 RID: 8998
		AuthenticationResult AuthenticationResult { get; set; }

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06002327 RID: 8999
		// (set) Token: 0x06002328 RID: 9000
		AuthorizationResult AuthorizationResult { get; set; }

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06002329 RID: 9001
		// (set) Token: 0x0600232A RID: 9002
		RequestProtectionOptions Options { get; set; }

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x0600232B RID: 9003
		// (set) Token: 0x0600232C RID: 9004
		IDenialOfServiceProtection<IPAddress> DenialOfServiceProtectionHandle { get; set; }

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x0600232D RID: 9005
		// (set) Token: 0x0600232E RID: 9006
		IRequestBlocker<string> KeyBasedProtectionHandle { get; set; }

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x0600232F RID: 9007
		// (set) Token: 0x06002330 RID: 9008
		string RequestBlockerKey { get; set; }
	}
}
