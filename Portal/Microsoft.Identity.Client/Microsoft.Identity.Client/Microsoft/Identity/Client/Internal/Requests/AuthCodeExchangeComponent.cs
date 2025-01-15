using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.Internal.Broker;
using Microsoft.Identity.Client.OAuth2;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client.Internal.Requests
{
	// Token: 0x02000246 RID: 582
	internal class AuthCodeExchangeComponent : ITokenRequestComponent
	{
		// Token: 0x0600178C RID: 6028 RVA: 0x0004E014 File Offset: 0x0004C214
		public AuthCodeExchangeComponent(AuthenticationRequestParameters requestParams, AcquireTokenInteractiveParameters interactiveParameters, string authorizationCode, string pkceCodeVerifier, string clientInfo)
		{
			if (requestParams == null)
			{
				throw new ArgumentNullException("requestParams");
			}
			this._requestParams = requestParams;
			if (interactiveParameters == null)
			{
				throw new ArgumentNullException("interactiveParameters");
			}
			this._interactiveParameters = interactiveParameters;
			if (authorizationCode == null)
			{
				throw new ArgumentNullException("authorizationCode");
			}
			this._authorizationCode = authorizationCode;
			if (pkceCodeVerifier == null)
			{
				throw new ArgumentNullException("pkceCodeVerifier");
			}
			this._pkceCodeVerifier = pkceCodeVerifier;
			this._clientInfo = clientInfo;
			this._tokenClient = new TokenClient(requestParams);
			this._interactiveParameters.LogParameters(requestParams.RequestContext.Logger);
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x0004E0AA File Offset: 0x0004C2AA
		public Task<MsalTokenResponse> FetchTokensAsync(CancellationToken cancellationToken)
		{
			this.AddCcsHeadersToTokenClient();
			return this._tokenClient.SendTokenRequestAsync(this.GetBodyParameters(), null, null, cancellationToken);
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x0004E0C8 File Offset: 0x0004C2C8
		private Dictionary<string, string> GetBodyParameters()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["client_info"] = "1";
			dictionary["grant_type"] = "authorization_code";
			dictionary["code"] = this._authorizationCode;
			dictionary["redirect_uri"] = this._requestParams.RedirectUri.OriginalString;
			dictionary["code_verifier"] = this._pkceCodeVerifier;
			return dictionary;
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x0004E138 File Offset: 0x0004C338
		private void AddCcsHeadersToTokenClient()
		{
			if (!string.IsNullOrEmpty(this._clientInfo))
			{
				ClientInfo clientInfo = ClientInfo.CreateFromJson(this._clientInfo);
				this._tokenClient.AddHeaderToClient("x-anchormailbox", CoreHelpers.GetCcsClientInfoHint(clientInfo.UniqueObjectIdentifier, clientInfo.UniqueTenantIdentifier));
				return;
			}
			if (!string.IsNullOrEmpty(this._interactiveParameters.LoginHint))
			{
				this._tokenClient.AddHeaderToClient("x-anchormailbox", CoreHelpers.GetCcsUpnHint(this._interactiveParameters.LoginHint));
			}
		}

		// Token: 0x04000A50 RID: 2640
		private readonly AuthenticationRequestParameters _requestParams;

		// Token: 0x04000A51 RID: 2641
		private readonly AcquireTokenInteractiveParameters _interactiveParameters;

		// Token: 0x04000A52 RID: 2642
		private readonly string _authorizationCode;

		// Token: 0x04000A53 RID: 2643
		private readonly string _pkceCodeVerifier;

		// Token: 0x04000A54 RID: 2644
		private readonly TokenClient _tokenClient;

		// Token: 0x04000A55 RID: 2645
		private readonly string _clientInfo;
	}
}
