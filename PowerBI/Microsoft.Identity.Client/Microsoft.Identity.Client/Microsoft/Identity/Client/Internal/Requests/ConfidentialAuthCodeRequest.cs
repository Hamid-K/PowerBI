using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.Http;
using Microsoft.Identity.Client.OAuth2;

namespace Microsoft.Identity.Client.Internal.Requests
{
	// Token: 0x02000243 RID: 579
	internal class ConfidentialAuthCodeRequest : RequestBase
	{
		// Token: 0x0600177E RID: 6014 RVA: 0x0004DC98 File Offset: 0x0004BE98
		public ConfidentialAuthCodeRequest(IServiceBundle serviceBundle, AuthenticationRequestParameters authenticationRequestParameters, AcquireTokenByAuthorizationCodeParameters authorizationCodeParameters)
			: base(serviceBundle, authenticationRequestParameters, authorizationCodeParameters)
		{
			this._authorizationCodeParameters = authorizationCodeParameters;
			RedirectUriHelper.Validate(authenticationRequestParameters.RedirectUri, false);
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x0004DCB8 File Offset: 0x0004BEB8
		protected override async Task<AuthenticationResult> ExecuteAsync(CancellationToken cancellationToken)
		{
			await base.ResolveAuthorityAsync().ConfigureAwait(false);
			MsalTokenResponse msalTokenResponse = await base.SendTokenRequestAsync(this.GetBodyParameters(), cancellationToken).ConfigureAwait(false);
			return await base.CacheTokenResponseAndCreateAuthenticationResultAsync(msalTokenResponse).ConfigureAwait(false);
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x0004DD04 File Offset: 0x0004BF04
		private Dictionary<string, string> GetBodyParameters()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["client_info"] = "1";
			dictionary["grant_type"] = "authorization_code";
			dictionary["code"] = this._authorizationCodeParameters.AuthorizationCode;
			dictionary["redirect_uri"] = base.AuthenticationRequestParameters.RedirectUri.OriginalString;
			Dictionary<string, string> dictionary2 = dictionary;
			if (!string.IsNullOrEmpty(this._authorizationCodeParameters.PkceCodeVerifier))
			{
				dictionary2["code_verifier"] = this._authorizationCodeParameters.PkceCodeVerifier;
			}
			if (this._authorizationCodeParameters.SpaCode)
			{
				dictionary2["return_spa_code"] = "1";
			}
			return dictionary2;
		}

		// Token: 0x04000A4C RID: 2636
		private readonly AcquireTokenByAuthorizationCodeParameters _authorizationCodeParameters;
	}
}
