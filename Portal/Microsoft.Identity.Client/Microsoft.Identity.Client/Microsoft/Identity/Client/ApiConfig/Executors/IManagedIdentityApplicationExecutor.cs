using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.Internal;

namespace Microsoft.Identity.Client.ApiConfig.Executors
{
	// Token: 0x020002E5 RID: 741
	internal interface IManagedIdentityApplicationExecutor
	{
		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06001B5E RID: 7006
		IServiceBundle ServiceBundle { get; }

		// Token: 0x06001B5F RID: 7007
		Task<AuthenticationResult> ExecuteAsync(AcquireTokenCommonParameters commonParameters, AcquireTokenForManagedIdentityParameters managedIdentityParameters, CancellationToken cancellationToken);
	}
}
