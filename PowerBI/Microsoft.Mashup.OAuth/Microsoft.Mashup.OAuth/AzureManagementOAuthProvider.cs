using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Microsoft.Mashup.OAuth
{
	// Token: 0x02000009 RID: 9
	[Obsolete]
	public sealed class AzureManagementOAuthProvider : IOAuthProvider
	{
		// Token: 0x06000045 RID: 69 RVA: 0x00003235 File Offset: 0x00001435
		public AzureManagementOAuthProvider(OAuthClientApplication clientApplication)
			: this(OAuthServices.Empty, clientApplication)
		{
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003243 File Offset: 0x00001443
		public AzureManagementOAuthProvider(IOAuthTracingService tracingService, OAuthClientApplication clientApplication)
			: this(OAuthServices.From(tracingService), clientApplication)
		{
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003254 File Offset: 0x00001454
		public AzureManagementOAuthProvider(OAuthServices services, OAuthClientApplication clientApplication)
		{
			if (clientApplication == null)
			{
				throw new ArgumentNullException("clientApplication");
			}
			this.clientApplication = clientApplication;
			this.services = services;
			this.aadOnStartLogin = new Dictionary<string, string>
			{
				{
					"client_id",
					this.clientApplication.Id
				},
				{
					"redirect_uri",
					this.clientApplication.CallbackUrl
				},
				{ "resource", "https://management.core.windows.net/" },
				{ "response_type", "code" },
				{ "prompt", "login" }
			};
			this.aadOnFinishLogin = new NameValueCollection
			{
				{
					"client_id",
					this.clientApplication.Id
				},
				{
					"redirect_uri",
					this.clientApplication.CallbackUrl
				},
				{ "grant_type", "authorization_code" }
			};
			this.aadOnRefresh = new NameValueCollection
			{
				{
					"client_id",
					this.clientApplication.Id
				},
				{
					"redirect_uri",
					this.clientApplication.CallbackUrl
				},
				{ "grant_type", "refresh_token" }
			};
			this.aadOnLogout = new Dictionary<string, string> { 
			{
				"post_logout_redirect_uri",
				this.clientApplication.CallbackUrl
			} };
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003399 File Offset: 0x00001599
		public OAuthBrowserNavigation StartLogin(string state, string display)
		{
			return new OAuthBrowserNavigation(Utilities.GetLoginUri(AzureManagementOAuthProvider.authorizeUrl, this.aadOnStartLogin, state, display), new Uri(this.clientApplication.CallbackUrl), 730, 1050);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000033CC File Offset: 0x000015CC
		public TokenCredential FinishLogin(byte[] serializedContext, Uri callbackUri, string state)
		{
			Utilities.ValidateFinishLoginArguments(callbackUri, state);
			NameValueCollection nameValueCollection = Utilities.ValidateResponse(this.services, callbackUri.Query, state);
			this.aadOnFinishLogin["code"] = nameValueCollection["code"];
			OAuthToken oauthToken = Utilities.FinishLogin(this.services, AzureManagementOAuthProvider.tokenUrl, this.aadOnFinishLogin);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			return new TokenCredential(oauthToken.AccessToken, oauthToken.Expires, oauthToken.RefreshToken, dictionary);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003444 File Offset: 0x00001644
		public TokenCredential Refresh(TokenCredential credential)
		{
			Utilities.ValidateRefreshArguments(credential);
			OAuthToken oauthToken = Utilities.RefreshJsonToken(this.services, credential.RefreshToken, AzureManagementOAuthProvider.tokenUrl, this.aadOnRefresh);
			return new TokenCredential(oauthToken.AccessToken, oauthToken.Expires, oauthToken.RefreshToken);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000348B File Offset: 0x0000168B
		public Uri Logout(string accessToken)
		{
			return Utilities.AddQueryParametersToUri(AzureManagementOAuthProvider.logoutUrl, this.aadOnLogout);
		}

		// Token: 0x04000034 RID: 52
		private const string resourceId = "https://management.core.windows.net/";

		// Token: 0x04000035 RID: 53
		private static readonly Uri authorizeUrl = new Uri("https://login.microsoftonline.com/common/oauth2/authorize");

		// Token: 0x04000036 RID: 54
		private static readonly Uri tokenUrl = new Uri("https://login.microsoftonline.com/common/oauth2/token");

		// Token: 0x04000037 RID: 55
		private static readonly Uri logoutUrl = new Uri("https://login.microsoftonline.com/common/oauth2/logout");

		// Token: 0x04000038 RID: 56
		private readonly OAuthClientApplication clientApplication;

		// Token: 0x04000039 RID: 57
		private readonly Dictionary<string, string> aadOnStartLogin;

		// Token: 0x0400003A RID: 58
		private readonly NameValueCollection aadOnFinishLogin;

		// Token: 0x0400003B RID: 59
		private readonly NameValueCollection aadOnRefresh;

		// Token: 0x0400003C RID: 60
		private readonly Dictionary<string, string> aadOnLogout;

		// Token: 0x0400003D RID: 61
		private readonly OAuthServices services;
	}
}
