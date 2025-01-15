using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.OAuth2;

namespace Microsoft.Identity.Client.Internal.Requests
{
	// Token: 0x02000241 RID: 577
	internal class ByRefreshTokenRequest : RequestBase
	{
		// Token: 0x06001771 RID: 6001 RVA: 0x0004DA18 File Offset: 0x0004BC18
		public ByRefreshTokenRequest(IServiceBundle serviceBundle, AuthenticationRequestParameters authenticationRequestParameters, AcquireTokenByRefreshTokenParameters refreshTokenParameters)
			: base(serviceBundle, authenticationRequestParameters, refreshTokenParameters)
		{
			this._refreshTokenParameters = refreshTokenParameters;
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x0004DA2C File Offset: 0x0004BC2C
		protected override async Task<AuthenticationResult> ExecuteAsync(CancellationToken cancellationToken)
		{
			base.AuthenticationRequestParameters.RequestContext.Logger.Verbose(() => "Begin acquire token by refresh token...");
			await base.ResolveAuthorityAsync().ConfigureAwait(false);
			MsalTokenResponse msalTokenResponse = await base.SendTokenRequestAsync(ByRefreshTokenRequest.GetBodyParameters(this._refreshTokenParameters.RefreshToken), cancellationToken).ConfigureAwait(false);
			if (msalTokenResponse.RefreshToken == null)
			{
				base.AuthenticationRequestParameters.RequestContext.Logger.Error("Acquire by refresh token request completed, but no refresh token was found. ");
				throw new MsalServiceException(msalTokenResponse.Error, msalTokenResponse.ErrorDescription, null);
			}
			return await base.CacheTokenResponseAndCreateAuthenticationResultAsync(msalTokenResponse).ConfigureAwait(false);
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x0004DA77 File Offset: 0x0004BC77
		private static Dictionary<string, string> GetBodyParameters(string refreshTokenSecret)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["grant_type"] = "refresh_token";
			dictionary["refresh_token"] = refreshTokenSecret;
			dictionary["client_info"] = "1";
			return dictionary;
		}

		// Token: 0x04000A49 RID: 2633
		private readonly AcquireTokenByRefreshTokenParameters _refreshTokenParameters;
	}
}
