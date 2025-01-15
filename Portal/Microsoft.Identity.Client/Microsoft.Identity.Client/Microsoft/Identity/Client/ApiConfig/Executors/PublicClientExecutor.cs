using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.Internal.Requests;

namespace Microsoft.Identity.Client.ApiConfig.Executors
{
	// Token: 0x020002E8 RID: 744
	internal class PublicClientExecutor : AbstractExecutor, IPublicClientApplicationExecutor
	{
		// Token: 0x06001B67 RID: 7015 RVA: 0x000578EB File Offset: 0x00055AEB
		public PublicClientExecutor(IServiceBundle serviceBundle, PublicClientApplication publicClientApplication)
			: base(serviceBundle)
		{
			this._publicClientApplication = publicClientApplication;
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x000578FC File Offset: 0x00055AFC
		public async Task<AuthenticationResult> ExecuteAsync(AcquireTokenCommonParameters commonParameters, AcquireTokenInteractiveParameters interactiveParameters, CancellationToken cancellationToken)
		{
			RequestContext requestContext = base.CreateRequestContextAndLogVersionInfo(commonParameters.CorrelationId, cancellationToken);
			object obj = await this._publicClientApplication.CreateRequestParametersAsync(commonParameters, requestContext, this._publicClientApplication.UserTokenCacheInternal).ConfigureAwait(false);
			obj.LoginHint = interactiveParameters.LoginHint;
			obj.Account = interactiveParameters.Account;
			return await new InteractiveRequest(obj, interactiveParameters, null, null, null).RunAsync(cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x00057958 File Offset: 0x00055B58
		public async Task<AuthenticationResult> ExecuteAsync(AcquireTokenCommonParameters commonParameters, AcquireTokenWithDeviceCodeParameters deviceCodeParameters, CancellationToken cancellationToken)
		{
			RequestContext requestContext = base.CreateRequestContextAndLogVersionInfo(commonParameters.CorrelationId, cancellationToken);
			AuthenticationRequestParameters authenticationRequestParameters = await this._publicClientApplication.CreateRequestParametersAsync(commonParameters, requestContext, this._publicClientApplication.UserTokenCacheInternal).ConfigureAwait(false);
			return await new DeviceCodeRequest(base.ServiceBundle, authenticationRequestParameters, deviceCodeParameters).RunAsync(cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x000579B4 File Offset: 0x00055BB4
		public async Task<AuthenticationResult> ExecuteAsync(AcquireTokenCommonParameters commonParameters, AcquireTokenByIntegratedWindowsAuthParameters integratedWindowsAuthParameters, CancellationToken cancellationToken)
		{
			RequestContext requestContext = base.CreateRequestContextAndLogVersionInfo(commonParameters.CorrelationId, cancellationToken);
			AuthenticationRequestParameters authenticationRequestParameters = await this._publicClientApplication.CreateRequestParametersAsync(commonParameters, requestContext, this._publicClientApplication.UserTokenCacheInternal).ConfigureAwait(false);
			return await new IntegratedWindowsAuthRequest(base.ServiceBundle, authenticationRequestParameters, integratedWindowsAuthParameters).RunAsync(cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06001B6B RID: 7019 RVA: 0x00057A10 File Offset: 0x00055C10
		public async Task<AuthenticationResult> ExecuteAsync(AcquireTokenCommonParameters commonParameters, AcquireTokenByUsernamePasswordParameters usernamePasswordParameters, CancellationToken cancellationToken)
		{
			RequestContext requestContext = base.CreateRequestContextAndLogVersionInfo(commonParameters.CorrelationId, cancellationToken);
			AuthenticationRequestParameters authenticationRequestParameters = await this._publicClientApplication.CreateRequestParametersAsync(commonParameters, requestContext, this._publicClientApplication.UserTokenCacheInternal).ConfigureAwait(false);
			return await new UsernamePasswordRequest(base.ServiceBundle, authenticationRequestParameters, usernamePasswordParameters).RunAsync(cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x04000C76 RID: 3190
		private readonly PublicClientApplication _publicClientApplication;
	}
}
