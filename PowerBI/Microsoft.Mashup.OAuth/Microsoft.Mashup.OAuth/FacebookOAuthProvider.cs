using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;

namespace Microsoft.Mashup.OAuth
{
	// Token: 0x0200000F RID: 15
	[Obsolete]
	public sealed class FacebookOAuthProvider : IOAuthProvider
	{
		// Token: 0x06000063 RID: 99 RVA: 0x00003A83 File Offset: 0x00001C83
		public FacebookOAuthProvider(OAuthClientApplication clientApplication, string[] scopes)
			: this(OAuthServices.Empty, clientApplication, scopes)
		{
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003A92 File Offset: 0x00001C92
		public FacebookOAuthProvider(IOAuthTracingService tracingService, OAuthClientApplication clientApplication, string[] scopes)
			: this(OAuthServices.From(tracingService), clientApplication, scopes)
		{
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003AA4 File Offset: 0x00001CA4
		public FacebookOAuthProvider(OAuthServices services, OAuthClientApplication clientApplication, string[] scopes)
		{
			if (clientApplication == null)
			{
				throw new ArgumentNullException("clientApplication");
			}
			this.clientApplication = clientApplication;
			this.scope = Utilities.GetScope(scopes);
			this.services = services;
			this.facebookOnStartLogin = new Dictionary<string, string>
			{
				{
					"client_id",
					this.clientApplication.Id
				},
				{
					"redirect_uri",
					this.clientApplication.CallbackUrl
				},
				{ "scope", this.scope },
				{ "response_type", "token" }
			};
			this.facebookOnLogout = new Dictionary<string, string> { 
			{
				"next",
				this.clientApplication.CallbackUrl
			} };
			this.facebookOnRefresh = new NameValueCollection
			{
				{
					"client_id",
					this.clientApplication.Id
				},
				{
					"redirect_uri",
					this.clientApplication.CallbackUrl
				},
				{ "scope", this.scope },
				{ "grant_type", "refresh_token" }
			};
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003BB0 File Offset: 0x00001DB0
		public OAuthBrowserNavigation StartLogin(string state, string display)
		{
			return new OAuthBrowserNavigation(Utilities.GetLoginUri(FacebookOAuthProvider.FacebookAuthorizeUri, this.facebookOnStartLogin, state, display), new Uri(this.clientApplication.CallbackUrl), 445, 580);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003BE4 File Offset: 0x00001DE4
		public TokenCredential FinishLogin(byte[] serializedContext, Uri callbackUri, string state)
		{
			Utilities.ValidateFinishLoginArguments(callbackUri, state);
			NameValueCollection nameValueCollection = Utilities.ValidateResponse(this.services, callbackUri.Fragment, state);
			if (nameValueCollection["access_token"] == null)
			{
				throw new OAuthException(OAuthStrings.InvalidOAuthResponse);
			}
			string text = nameValueCollection["access_token"];
			string text2 = Utilities.GetExpiresAtString(nameValueCollection["expires_in"]);
			if (Utilities.ExpiresInLessThan(nameValueCollection["expires_in"], TimeSpan.FromHours(24.0)))
			{
				OAuthToken oauthToken = this.TryRequestLongLivedAccessToken(text);
				if (oauthToken != null)
				{
					text = oauthToken.AccessToken;
					text2 = oauthToken.Expires;
				}
			}
			return new TokenCredential(text, text2, nameValueCollection["refresh_token"]);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003C8C File Offset: 0x00001E8C
		public TokenCredential Refresh(TokenCredential credential)
		{
			Utilities.ValidateRefreshArguments(credential);
			NameValueCollection nameValueCollection = Utilities.RefreshToken(this.services, credential, FacebookOAuthProvider.FacebookTokenUri, this.facebookOnRefresh);
			return new TokenCredential(nameValueCollection["access_token"], Utilities.GetExpiresAtString(nameValueCollection["expires_in"]), nameValueCollection["refresh_token"]);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003CE2 File Offset: 0x00001EE2
		public Uri Logout(string accessToken)
		{
			if (accessToken != null)
			{
				this.facebookOnLogout["access_token"] = accessToken;
				return Utilities.AddQueryParametersToUri(FacebookOAuthProvider.FacebookLogoutUri, this.facebookOnLogout);
			}
			return null;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003D0C File Offset: 0x00001F0C
		private OAuthToken TryRequestLongLivedAccessToken(string accessToken)
		{
			NameValueCollection nameValueCollection = new NameValueCollection
			{
				{
					"client_id",
					this.clientApplication.Id
				},
				{
					"client_secret",
					this.clientApplication.Secret
				}
			};
			try
			{
				return Utilities.FinishLogin(this.services, FacebookOAuthProvider.FacebookTokenUri, nameValueCollection);
			}
			catch (WebException)
			{
			}
			return null;
		}

		// Token: 0x04000089 RID: 137
		private const int TokenMinimumHours = 24;

		// Token: 0x0400008A RID: 138
		private static readonly Uri FacebookAuthorizeUri = new Uri("https://www.facebook.com/v2.8/dialog/oauth");

		// Token: 0x0400008B RID: 139
		private static readonly Uri FacebookTokenUri = new Uri("https://graph.facebook.com/v2.8/oauth/access_token");

		// Token: 0x0400008C RID: 140
		private static readonly Uri FacebookLogoutUri = new Uri("https://www.facebook.com/logout.php");

		// Token: 0x0400008D RID: 141
		private readonly OAuthClientApplication clientApplication;

		// Token: 0x0400008E RID: 142
		private readonly NameValueCollection facebookOnRefresh;

		// Token: 0x0400008F RID: 143
		private readonly Dictionary<string, string> facebookOnStartLogin;

		// Token: 0x04000090 RID: 144
		private readonly Dictionary<string, string> facebookOnLogout;

		// Token: 0x04000091 RID: 145
		private readonly string scope;

		// Token: 0x04000092 RID: 146
		private readonly OAuthServices services;
	}
}
