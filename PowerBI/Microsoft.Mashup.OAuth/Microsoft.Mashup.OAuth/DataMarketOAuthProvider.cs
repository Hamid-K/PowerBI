using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Microsoft.Mashup.OAuth
{
	// Token: 0x0200000E RID: 14
	[Obsolete]
	public sealed class DataMarketOAuthProvider : IOAuthProvider
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00003792 File Offset: 0x00001992
		public DataMarketOAuthProvider(OAuthClientApplication clientApplication, string resourceUrl)
			: this(OAuthServices.Empty, clientApplication, resourceUrl)
		{
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000037A1 File Offset: 0x000019A1
		public DataMarketOAuthProvider(IOAuthTracingService tracingService, OAuthClientApplication clientApplication, string resourceUrl)
			: this(OAuthServices.From(tracingService), clientApplication, resourceUrl)
		{
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000037B4 File Offset: 0x000019B4
		public DataMarketOAuthProvider(OAuthServices services, OAuthClientApplication clientApplication, string resourceUrl)
		{
			if (clientApplication == null)
			{
				throw new ArgumentNullException("clientApplication");
			}
			if (string.IsNullOrEmpty(resourceUrl))
			{
				throw new ArgumentNullException("resourceUrl");
			}
			this.clientApplication = clientApplication;
			this.scope = Utilities.GetAuthority(resourceUrl);
			this.services = services;
			this.dataMarketOnStartLogin = new Dictionary<string, string>
			{
				{
					"client_id",
					this.clientApplication.Id
				},
				{
					"redirect_uri",
					this.clientApplication.CallbackUrl
				},
				{ "response_type", "code" },
				{ "x_permissions", "account" }
			};
			this.dataMarketOnFinishLogin = new NameValueCollection
			{
				{
					"client_id",
					this.clientApplication.Id
				},
				{
					"client_secret",
					this.clientApplication.Secret
				},
				{
					"redirect_uri",
					this.clientApplication.CallbackUrl
				},
				{ "grant_type", "authorization_code" }
			};
			this.dataMarketOnRefresh = new NameValueCollection
			{
				{
					"client_id",
					this.clientApplication.Id
				},
				{
					"client_secret",
					this.clientApplication.Secret
				},
				{
					"redirect_uri",
					this.clientApplication.CallbackUrl
				},
				{ "grant_type", "refresh_token" }
			};
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003914 File Offset: 0x00001B14
		public OAuthBrowserNavigation StartLogin(string state, string display)
		{
			this.dataMarketOnStartLogin["x_scope"] = this.scope;
			return new OAuthBrowserNavigation(Utilities.GetLoginUri(DataMarketOAuthProvider.DataMarketAuthorizeUri, this.dataMarketOnStartLogin, state, display), new Uri(this.clientApplication.CallbackUrl), 730, 1050);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003968 File Offset: 0x00001B68
		public TokenCredential FinishLogin(byte[] serializedContext, Uri callbackUri, string state)
		{
			Utilities.ValidateFinishLoginArguments(callbackUri, state);
			NameValueCollection nameValueCollection = Utilities.ValidateResponse(this.services, callbackUri.Query, state);
			this.dataMarketOnFinishLogin["code"] = nameValueCollection["code"];
			this.dataMarketOnFinishLogin["scope"] = this.scope;
			OAuthToken oauthToken = Utilities.FinishLogin(this.services, DataMarketOAuthProvider.DataMarketTokenUri, this.dataMarketOnFinishLogin);
			return new TokenCredential(oauthToken.AccessToken, oauthToken.Expires, oauthToken.RefreshToken);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000039F0 File Offset: 0x00001BF0
		public TokenCredential Refresh(TokenCredential credential)
		{
			Utilities.ValidateRefreshArguments(credential);
			this.dataMarketOnRefresh["scope"] = this.scope;
			OAuthToken oauthToken = Utilities.RefreshJsonToken(this.services, credential.RefreshToken, DataMarketOAuthProvider.DataMarketTokenUri, this.dataMarketOnRefresh);
			return new TokenCredential(oauthToken.AccessToken, oauthToken.Expires, oauthToken.RefreshToken);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003A4D File Offset: 0x00001C4D
		public Uri Logout(string accessToken)
		{
			return DataMarketOAuthProvider.DataMarketLogoutUri;
		}

		// Token: 0x04000080 RID: 128
		private static readonly Uri DataMarketLogoutUri = new Uri("https://datamarket.azure.com/signout");

		// Token: 0x04000081 RID: 129
		private static readonly Uri DataMarketAuthorizeUri = new Uri("https://datamarket.azure.com/embedded/consent");

		// Token: 0x04000082 RID: 130
		private static readonly Uri DataMarketTokenUri = new Uri("https://datamarket.accesscontrol.windows.net/v2/OAuth2-13");

		// Token: 0x04000083 RID: 131
		private readonly OAuthClientApplication clientApplication;

		// Token: 0x04000084 RID: 132
		private readonly Dictionary<string, string> dataMarketOnStartLogin;

		// Token: 0x04000085 RID: 133
		private readonly NameValueCollection dataMarketOnFinishLogin;

		// Token: 0x04000086 RID: 134
		private readonly NameValueCollection dataMarketOnRefresh;

		// Token: 0x04000087 RID: 135
		private readonly string scope;

		// Token: 0x04000088 RID: 136
		private readonly OAuthServices services;
	}
}
