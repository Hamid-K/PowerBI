using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.Internal.Requests;

namespace Microsoft.Identity.Client.ApiConfig.Executors
{
	// Token: 0x020002E7 RID: 743
	internal class ManagedIdentityExecutor : AbstractExecutor, IManagedIdentityApplicationExecutor
	{
		// Token: 0x06001B65 RID: 7013 RVA: 0x0005787B File Offset: 0x00055A7B
		public ManagedIdentityExecutor(IServiceBundle serviceBundle, ManagedIdentityApplication managedIdentityApplication)
			: base(serviceBundle)
		{
			ApplicationBase.GuardMobileFrameworks();
			this._managedIdentityApplication = managedIdentityApplication;
		}

		// Token: 0x06001B66 RID: 7014 RVA: 0x00057890 File Offset: 0x00055A90
		public async Task<AuthenticationResult> ExecuteAsync(AcquireTokenCommonParameters commonParameters, AcquireTokenForManagedIdentityParameters managedIdentityParameters, CancellationToken cancellationToken)
		{
			RequestContext requestContext = base.CreateRequestContextAndLogVersionInfo(commonParameters.CorrelationId, cancellationToken);
			AuthenticationRequestParameters authenticationRequestParameters = await this._managedIdentityApplication.CreateRequestParametersAsync(commonParameters, requestContext, this._managedIdentityApplication.AppTokenCacheInternal).ConfigureAwait(false);
			return await new ManagedIdentityAuthRequest(base.ServiceBundle, authenticationRequestParameters, managedIdentityParameters).RunAsync(cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x04000C75 RID: 3189
		private readonly ManagedIdentityApplication _managedIdentityApplication;
	}
}
