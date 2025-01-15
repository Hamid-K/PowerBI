using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.Internal;

namespace Microsoft.Identity.Client.ApiConfig.Executors
{
	// Token: 0x020002E4 RID: 740
	internal interface IConfidentialClientApplicationExecutor
	{
		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06001B59 RID: 7001
		IServiceBundle ServiceBundle { get; }

		// Token: 0x06001B5A RID: 7002
		Task<AuthenticationResult> ExecuteAsync(AcquireTokenCommonParameters commonParameters, AcquireTokenByAuthorizationCodeParameters authorizationCodeParameters, CancellationToken cancellationToken);

		// Token: 0x06001B5B RID: 7003
		Task<AuthenticationResult> ExecuteAsync(AcquireTokenCommonParameters commonParameters, AcquireTokenForClientParameters clientParameters, CancellationToken cancellationToken);

		// Token: 0x06001B5C RID: 7004
		Task<AuthenticationResult> ExecuteAsync(AcquireTokenCommonParameters commonParameters, AcquireTokenOnBehalfOfParameters onBehalfOfParameters, CancellationToken cancellationToken);

		// Token: 0x06001B5D RID: 7005
		Task<Uri> ExecuteAsync(AcquireTokenCommonParameters commonParameters, GetAuthorizationRequestUrlParameters authorizationRequestUrlParameters, CancellationToken cancellationToken);
	}
}
