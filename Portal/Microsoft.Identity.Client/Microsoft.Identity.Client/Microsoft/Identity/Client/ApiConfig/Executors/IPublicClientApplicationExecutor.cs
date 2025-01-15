using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.Internal;

namespace Microsoft.Identity.Client.ApiConfig.Executors
{
	// Token: 0x020002E6 RID: 742
	internal interface IPublicClientApplicationExecutor
	{
		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06001B60 RID: 7008
		IServiceBundle ServiceBundle { get; }

		// Token: 0x06001B61 RID: 7009
		Task<AuthenticationResult> ExecuteAsync(AcquireTokenCommonParameters commonParameters, AcquireTokenInteractiveParameters interactiveParameters, CancellationToken cancellationToken);

		// Token: 0x06001B62 RID: 7010
		Task<AuthenticationResult> ExecuteAsync(AcquireTokenCommonParameters commonParameters, AcquireTokenWithDeviceCodeParameters withDeviceCodeParameters, CancellationToken cancellationToken);

		// Token: 0x06001B63 RID: 7011
		Task<AuthenticationResult> ExecuteAsync(AcquireTokenCommonParameters commonParameters, AcquireTokenByIntegratedWindowsAuthParameters integratedWindowsAuthParameters, CancellationToken cancellationToken);

		// Token: 0x06001B64 RID: 7012
		Task<AuthenticationResult> ExecuteAsync(AcquireTokenCommonParameters commonParameters, AcquireTokenByUsernamePasswordParameters usernamePasswordParameters, CancellationToken cancellationToken);
	}
}
