using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.Internal.Requests;

namespace Microsoft.Identity.Client.ApiConfig.Executors
{
	// Token: 0x020002E2 RID: 738
	internal class ConfidentialClientExecutor : AbstractExecutor, IConfidentialClientApplicationExecutor
	{
		// Token: 0x06001B51 RID: 6993 RVA: 0x000576F5 File Offset: 0x000558F5
		public ConfidentialClientExecutor(IServiceBundle serviceBundle, ConfidentialClientApplication confidentialClientApplication)
			: base(serviceBundle)
		{
			ApplicationBase.GuardMobileFrameworks();
			this._confidentialClientApplication = confidentialClientApplication;
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x0005770C File Offset: 0x0005590C
		public async Task<AuthenticationResult> ExecuteAsync(AcquireTokenCommonParameters commonParameters, AcquireTokenByAuthorizationCodeParameters authorizationCodeParameters, CancellationToken cancellationToken)
		{
			RequestContext requestContext = base.CreateRequestContextAndLogVersionInfo(commonParameters.CorrelationId, cancellationToken);
			AuthenticationRequestParameters authenticationRequestParameters = await this._confidentialClientApplication.CreateRequestParametersAsync(commonParameters, requestContext, this._confidentialClientApplication.UserTokenCacheInternal).ConfigureAwait(false);
			authenticationRequestParameters.SendX5C = authorizationCodeParameters.SendX5C.GetValueOrDefault();
			return await new ConfidentialAuthCodeRequest(base.ServiceBundle, authenticationRequestParameters, authorizationCodeParameters).RunAsync(cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x00057768 File Offset: 0x00055968
		public async Task<AuthenticationResult> ExecuteAsync(AcquireTokenCommonParameters commonParameters, AcquireTokenForClientParameters clientParameters, CancellationToken cancellationToken)
		{
			RequestContext requestContext = base.CreateRequestContextAndLogVersionInfo(commonParameters.CorrelationId, cancellationToken);
			AuthenticationRequestParameters authenticationRequestParameters = await this._confidentialClientApplication.CreateRequestParametersAsync(commonParameters, requestContext, this._confidentialClientApplication.AppTokenCacheInternal).ConfigureAwait(false);
			authenticationRequestParameters.SendX5C = clientParameters.SendX5C.GetValueOrDefault();
			return await new ClientCredentialRequest(base.ServiceBundle, authenticationRequestParameters, clientParameters).RunAsync(cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x000577C4 File Offset: 0x000559C4
		public async Task<AuthenticationResult> ExecuteAsync(AcquireTokenCommonParameters commonParameters, AcquireTokenOnBehalfOfParameters onBehalfOfParameters, CancellationToken cancellationToken)
		{
			RequestContext requestContext = base.CreateRequestContextAndLogVersionInfo(commonParameters.CorrelationId, cancellationToken);
			AuthenticationRequestParameters authenticationRequestParameters = await this._confidentialClientApplication.CreateRequestParametersAsync(commonParameters, requestContext, this._confidentialClientApplication.UserTokenCacheInternal).ConfigureAwait(false);
			authenticationRequestParameters.SendX5C = onBehalfOfParameters.SendX5C.GetValueOrDefault();
			authenticationRequestParameters.UserAssertion = onBehalfOfParameters.UserAssertion;
			authenticationRequestParameters.LongRunningOboCacheKey = onBehalfOfParameters.LongRunningOboCacheKey;
			return await new OnBehalfOfRequest(base.ServiceBundle, authenticationRequestParameters, onBehalfOfParameters).RunAsync(cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x00057820 File Offset: 0x00055A20
		public async Task<Uri> ExecuteAsync(AcquireTokenCommonParameters commonParameters, GetAuthorizationRequestUrlParameters authorizationRequestUrlParameters, CancellationToken cancellationToken)
		{
			RequestContext requestContext = base.CreateRequestContextAndLogVersionInfo(commonParameters.CorrelationId, cancellationToken);
			AuthenticationRequestParameters authenticationRequestParameters = await this._confidentialClientApplication.CreateRequestParametersAsync(commonParameters, requestContext, this._confidentialClientApplication.UserTokenCacheInternal).ConfigureAwait(false);
			AuthenticationRequestParameters requestParameters = authenticationRequestParameters;
			requestParameters.Account = authorizationRequestUrlParameters.Account;
			requestParameters.LoginHint = authorizationRequestUrlParameters.LoginHint;
			requestParameters.CcsRoutingHint = authorizationRequestUrlParameters.CcsRoutingHint;
			if (!string.IsNullOrWhiteSpace(authorizationRequestUrlParameters.RedirectUri))
			{
				requestParameters.RedirectUri = new Uri(authorizationRequestUrlParameters.RedirectUri);
			}
			await requestParameters.AuthorityManager.RunInstanceDiscoveryAndValidationAsync().ConfigureAwait(false);
			AuthCodeRequestComponent authCodeRequestComponent = new AuthCodeRequestComponent(requestParameters, authorizationRequestUrlParameters.ToInteractiveParameters());
			Uri uri;
			if (authorizationRequestUrlParameters.CodeVerifier != null)
			{
				uri = await authCodeRequestComponent.GetAuthorizationUriWithPkceAsync(authorizationRequestUrlParameters.CodeVerifier, cancellationToken).ConfigureAwait(false);
			}
			else
			{
				uri = await authCodeRequestComponent.GetAuthorizationUriWithoutPkceAsync(cancellationToken).ConfigureAwait(false);
			}
			return uri;
		}

		// Token: 0x04000C74 RID: 3188
		private readonly ConfidentialClientApplication _confidentialClientApplication;
	}
}
