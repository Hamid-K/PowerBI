using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.Internal.Requests;
using Microsoft.Identity.Client.Internal.Requests.Silent;

namespace Microsoft.Identity.Client.ApiConfig.Executors
{
	// Token: 0x020002E0 RID: 736
	internal class ClientApplicationBaseExecutor : AbstractExecutor, IClientApplicationBaseExecutor
	{
		// Token: 0x06001B4A RID: 6986 RVA: 0x000575EB File Offset: 0x000557EB
		public ClientApplicationBaseExecutor(IServiceBundle serviceBundle, ClientApplicationBase clientApplicationBase)
			: base(serviceBundle)
		{
			this._clientApplicationBase = clientApplicationBase;
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x000575FC File Offset: 0x000557FC
		public async Task<AuthenticationResult> ExecuteAsync(AcquireTokenCommonParameters commonParameters, AcquireTokenSilentParameters silentParameters, CancellationToken cancellationToken)
		{
			RequestContext requestContext = base.CreateRequestContextAndLogVersionInfo(commonParameters.CorrelationId, cancellationToken);
			AuthenticationRequestParameters authenticationRequestParameters = await this._clientApplicationBase.CreateRequestParametersAsync(commonParameters, requestContext, this._clientApplicationBase.UserTokenCacheInternal).ConfigureAwait(false);
			authenticationRequestParameters.SendX5C = silentParameters.SendX5C.GetValueOrDefault();
			return await new SilentRequest(base.ServiceBundle, authenticationRequestParameters, silentParameters, null, null).RunAsync(cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x00057658 File Offset: 0x00055858
		public async Task<AuthenticationResult> ExecuteAsync(AcquireTokenCommonParameters commonParameters, AcquireTokenByRefreshTokenParameters refreshTokenParameters, CancellationToken cancellationToken)
		{
			RequestContext requestContext = base.CreateRequestContextAndLogVersionInfo(commonParameters.CorrelationId, cancellationToken);
			if (commonParameters.Scopes == null || !commonParameters.Scopes.Any<string>())
			{
				commonParameters.Scopes = new SortedSet<string> { this._clientApplicationBase.AppConfig.ClientId + "/.default" };
				requestContext.Logger.Info("No scopes provided for acquire token by refresh token request. Using default scope instead.");
			}
			AuthenticationRequestParameters authenticationRequestParameters = await this._clientApplicationBase.CreateRequestParametersAsync(commonParameters, requestContext, this._clientApplicationBase.UserTokenCacheInternal).ConfigureAwait(false);
			requestContext.Logger.Info(() => LogMessages.UsingXScopesForRefreshTokenRequest(commonParameters.Scopes.Count<string>()));
			authenticationRequestParameters.SendX5C = refreshTokenParameters.SendX5C.GetValueOrDefault();
			return await new ByRefreshTokenRequest(base.ServiceBundle, authenticationRequestParameters, refreshTokenParameters).RunAsync(CancellationToken.None).ConfigureAwait(false);
		}

		// Token: 0x04000C73 RID: 3187
		private readonly ClientApplicationBase _clientApplicationBase;
	}
}
