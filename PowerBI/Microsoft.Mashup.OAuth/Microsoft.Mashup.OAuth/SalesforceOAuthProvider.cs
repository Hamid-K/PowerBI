using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Microsoft.Mashup.OAuth
{
	// Token: 0x02000023 RID: 35
	public sealed class SalesforceOAuthProvider : IOAuthProvider
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x00005494 File Offset: 0x00003694
		public SalesforceOAuthProvider(OAuthClientApplication clientApplication, string resourceUrl)
			: this(OAuthServices.Empty, clientApplication, resourceUrl)
		{
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000054A3 File Offset: 0x000036A3
		public SalesforceOAuthProvider(IOAuthTracingService tracingService, OAuthClientApplication clientApplication, string resourceUrl)
			: this(OAuthServices.From(tracingService), clientApplication, resourceUrl)
		{
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000054B4 File Offset: 0x000036B4
		public SalesforceOAuthProvider(OAuthServices services, OAuthClientApplication clientApplication, string resourceUrl)
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
			Uri uri = SalesforceOAuthProvider.ValidateSalesforceLoginUrl(resourceUrl);
			this.authorizeUri = new Uri(uri, "services/oauth2/authorize");
			this.tokenUri = new Uri(uri, "services/oauth2/token");
			this.logoutUri = new Uri(uri, "services/oauth2/revoke");
			this.services = services;
			this.salesforceOnStartLogin = new Dictionary<string, string>
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
				{ "prompt", "login" }
			};
			this.salesforceOnFinishLogin = new NameValueCollection
			{
				{
					"client_id",
					this.clientApplication.Id
				},
				{
					"redirect_uri",
					this.clientApplication.CallbackUrl
				},
				{
					"client_secret",
					this.clientApplication.Secret
				},
				{ "grant_type", "authorization_code" }
			};
			this.salesforceOnRefresh = new NameValueCollection
			{
				{
					"client_id",
					this.clientApplication.Id
				},
				{ "grant_type", "refresh_token" },
				{
					"redirect_uri",
					this.clientApplication.CallbackUrl
				}
			};
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000562B File Offset: 0x0000382B
		public OAuthBrowserNavigation StartLogin(string state, string display)
		{
			return new OAuthBrowserNavigation(Utilities.GetLoginUri(this.authorizeUri, this.salesforceOnStartLogin, state, display), new Uri(this.clientApplication.CallbackUrl), 640, 680);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005660 File Offset: 0x00003860
		public TokenCredential FinishLogin(byte[] serializedContext, Uri callbackUri, string state)
		{
			Utilities.ValidateFinishLoginArguments(callbackUri, state);
			NameValueCollection nameValueCollection = Utilities.ValidateResponse(this.services, callbackUri.Query, state);
			this.salesforceOnFinishLogin["code"] = nameValueCollection["code"];
			OAuthToken oauthToken = Utilities.FinishLogin(this.services, this.tokenUri, this.salesforceOnFinishLogin);
			return new TokenCredential(oauthToken.AccessToken, Utilities.GetExpiresAtString("3600"), oauthToken.RefreshToken, new Dictionary<string, string> { { "instance_url", oauthToken.InstanceUrl } });
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000056EC File Offset: 0x000038EC
		public TokenCredential Refresh(TokenCredential credential)
		{
			Utilities.ValidateRefreshArguments(credential);
			OAuthToken oauthToken = Utilities.RefreshJsonToken(this.services, credential.RefreshToken, this.tokenUri, this.salesforceOnRefresh);
			return new TokenCredential(oauthToken.AccessToken, Utilities.GetExpiresAtString("3600"), oauthToken.RefreshToken, new Dictionary<string, string> { { "instance_url", oauthToken.InstanceUrl } });
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000574E File Offset: 0x0000394E
		public Uri Logout(string accessToken)
		{
			if (accessToken != null)
			{
				return Utilities.AddQueryParametersToUri(this.logoutUri, new Dictionary<string, string> { { "token", accessToken } });
			}
			return null;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005774 File Offset: 0x00003974
		public static Uri ValidateSalesforceLoginUrl(string resourceUrl)
		{
			try
			{
				Uri uri = new Uri(resourceUrl);
				if (uri.Host.EndsWith("salesforce.com", StringComparison.Ordinal) || uri.Host.EndsWith("cloudforce.com", StringComparison.Ordinal))
				{
					if (uri.Scheme != Uri.UriSchemeHttps)
					{
						throw new OAuthException(OAuthStrings.HttpsRequired(resourceUrl));
					}
					return uri;
				}
			}
			catch (UriFormatException)
			{
			}
			catch (ArgumentException)
			{
			}
			throw new OAuthException(OAuthStrings.InvalidSalesforceUri(resourceUrl));
		}

		// Token: 0x040000D5 RID: 213
		private const string SalesforceDomain = "salesforce.com";

		// Token: 0x040000D6 RID: 214
		private const string CloudforceDomain = "cloudforce.com";

		// Token: 0x040000D7 RID: 215
		private const string AuthorizeFragment = "services/oauth2/authorize";

		// Token: 0x040000D8 RID: 216
		private const string TokenFragment = "services/oauth2/token";

		// Token: 0x040000D9 RID: 217
		private const string LogoutFragment = "services/oauth2/revoke";

		// Token: 0x040000DA RID: 218
		private const string DefaultExpiresIn = "3600";

		// Token: 0x040000DB RID: 219
		private readonly OAuthClientApplication clientApplication;

		// Token: 0x040000DC RID: 220
		private readonly NameValueCollection salesforceOnRefresh;

		// Token: 0x040000DD RID: 221
		private readonly NameValueCollection salesforceOnFinishLogin;

		// Token: 0x040000DE RID: 222
		private readonly Dictionary<string, string> salesforceOnStartLogin;

		// Token: 0x040000DF RID: 223
		private readonly Uri authorizeUri;

		// Token: 0x040000E0 RID: 224
		private readonly Uri tokenUri;

		// Token: 0x040000E1 RID: 225
		private readonly Uri logoutUri;

		// Token: 0x040000E2 RID: 226
		private readonly OAuthServices services;
	}
}
