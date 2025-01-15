using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.Internal;

namespace Microsoft.Identity.Client.ApiConfig.Executors
{
	// Token: 0x020002E3 RID: 739
	internal interface IClientApplicationBaseExecutor
	{
		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06001B56 RID: 6998
		IServiceBundle ServiceBundle { get; }

		// Token: 0x06001B57 RID: 6999
		Task<AuthenticationResult> ExecuteAsync(AcquireTokenCommonParameters commonParameters, AcquireTokenSilentParameters silentParameters, CancellationToken cancellationToken);

		// Token: 0x06001B58 RID: 7000
		Task<AuthenticationResult> ExecuteAsync(AcquireTokenCommonParameters commonParameters, AcquireTokenByRefreshTokenParameters byRefreshTokenParameters, CancellationToken cancellationToken);
	}
}
