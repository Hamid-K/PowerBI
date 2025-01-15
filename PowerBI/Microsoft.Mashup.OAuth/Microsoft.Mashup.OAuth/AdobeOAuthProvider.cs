using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Microsoft.Mashup.OAuth
{
	// Token: 0x02000008 RID: 8
	public sealed class AdobeOAuthProvider : IOAuthProvider
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00002F77 File Offset: 0x00001177
		public AdobeOAuthProvider(IOAuthTracingService tracingService, OAuthClientApplication clientApplication)
			: this(OAuthServices.From(tracingService), clientApplication)
		{
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002F88 File Offset: 0x00001188
		public AdobeOAuthProvider(OAuthServices services, OAuthClientApplication clientApplication)
		{
			if (clientApplication == null)
			{
				throw new ArgumentNullException("clientApplication");
			}
			this.clientApplication = clientApplication;
			this.services = services;
			this.adobeOnStartLogin = new Dictionary<string, string>
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
				{ "scope", "openid,AdobeID,read_organizations,additional_info.job_function,additional_info.projectedProductContext" }
			};
			this.adobeOnFinishLogin = new NameValueCollection
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
			this.adobeOnRefresh = new NameValueCollection
			{
				{
					"client_id",
					this.clientApplication.Id
				},
				{
					"client_secret",
					this.clientApplication.Secret
				},
				{ "grant_type", "refresh_token" },
				{
					"redirect_uri",
					this.clientApplication.CallbackUrl
				}
			};
			this.properties = new Dictionary<string, string> { 
			{
				"ClientId",
				this.clientApplication.Id
			} };
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000030E9 File Offset: 0x000012E9
		public OAuthBrowserNavigation StartLogin(string state, string display)
		{
			return new OAuthBrowserNavigation(Utilities.GetLoginUri(AdobeOAuthProvider.authorizeUri, this.adobeOnStartLogin, state, display), new Uri(this.clientApplication.CallbackUrl), 640, 680);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000311C File Offset: 0x0000131C
		public TokenCredential FinishLogin(byte[] serializedContext, Uri callbackUri, string state)
		{
			Utilities.ValidateFinishLoginArguments(callbackUri, state);
			string text = Utilities.ValidateResponse(this.services, callbackUri.Query, state)["code"];
			this.adobeOnFinishLogin["code"] = text;
			OAuthToken oauthToken = Utilities.FinishLogin(this.services, AdobeOAuthProvider.tokenUri, this.adobeOnFinishLogin);
			return new TokenCredential(oauthToken.AccessToken, Utilities.GetExpiresAtString(string.IsNullOrEmpty(oauthToken.ExpiresIn) ? "2592000" : oauthToken.ExpiresIn), oauthToken.RefreshToken, this.properties);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000031AC File Offset: 0x000013AC
		public TokenCredential Refresh(TokenCredential credential)
		{
			Utilities.ValidateRefreshArguments(credential);
			OAuthToken oauthToken = Utilities.RefreshJsonToken(this.services, credential.RefreshToken, AdobeOAuthProvider.tokenUri, this.adobeOnRefresh);
			return new TokenCredential(oauthToken.AccessToken, Utilities.GetExpiresAtString(string.IsNullOrEmpty(oauthToken.ExpiresIn) ? "2592000" : oauthToken.ExpiresIn), oauthToken.RefreshToken, this.properties);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003212 File Offset: 0x00001412
		public Uri Logout(string accessToken)
		{
			return null;
		}

		// Token: 0x0400002B RID: 43
		private const string DefaultExpiresIn = "2592000";

		// Token: 0x0400002C RID: 44
		private static readonly Uri authorizeUri = new Uri("https://ims-na1.adobelogin.com/ims/authorize/v2");

		// Token: 0x0400002D RID: 45
		private static readonly Uri tokenUri = new Uri("https://ims-na1.adobelogin.com/ims/token/v3");

		// Token: 0x0400002E RID: 46
		private readonly OAuthClientApplication clientApplication;

		// Token: 0x0400002F RID: 47
		private readonly NameValueCollection adobeOnRefresh;

		// Token: 0x04000030 RID: 48
		private readonly NameValueCollection adobeOnFinishLogin;

		// Token: 0x04000031 RID: 49
		private readonly Dictionary<string, string> adobeOnStartLogin;

		// Token: 0x04000032 RID: 50
		private readonly OAuthServices services;

		// Token: 0x04000033 RID: 51
		private readonly Dictionary<string, string> properties;
	}
}
