using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Microsoft.Mashup.OAuth
{
	// Token: 0x02000011 RID: 17
	public abstract class GoogleOAuthProviderBase : IOAuthProvider
	{
		// Token: 0x06000077 RID: 119 RVA: 0x0000400A File Offset: 0x0000220A
		public GoogleOAuthProviderBase(IOAuthTracingService tracingService, OAuthClientApplication clientApplication, string[] scopes)
			: this(OAuthServices.From(tracingService), clientApplication, scopes)
		{
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000401C File Offset: 0x0000221C
		public GoogleOAuthProviderBase(OAuthServices services, OAuthClientApplication clientApplication, string[] scopes)
		{
			if (clientApplication == null)
			{
				throw new ArgumentNullException("clientApplication");
			}
			this.services = services;
			this.clientApplication = clientApplication;
			this.scope = Utilities.GetScope(scopes);
			this.googleAnalyticsOnStartLogin = new Dictionary<string, string>
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
				{ "response_type", "code" },
				{ "access_type", "offline" }
			};
			this.googleAnalyticsOnFinishLogin = new NameValueCollection
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
			this.googleAnalyticsOnRefresh = new NameValueCollection();
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000079 RID: 121
		protected abstract Uri GoogleTokenUri { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600007A RID: 122
		protected abstract Uri GoogleAuthorizeUri { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600007B RID: 123
		protected abstract Uri GoogleLogoutUri { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600007C RID: 124
		protected abstract Uri AccountsListUrl { get; }

		// Token: 0x0600007D RID: 125
		public abstract TokenCredential Refresh(TokenCredential credential);

		// Token: 0x0600007E RID: 126
		public abstract Uri Logout(string accessToken);

		// Token: 0x0600007F RID: 127
		internal abstract TokenCredential CreateCredential(OAuthToken token);

		// Token: 0x06000080 RID: 128 RVA: 0x00004127 File Offset: 0x00002327
		public OAuthBrowserNavigation StartLogin(string state, string display)
		{
			return new OAuthBrowserNavigation(Utilities.GetLoginUri(this.GoogleAuthorizeUri, this.googleAnalyticsOnStartLogin, state, display), new Uri(this.clientApplication.CallbackUrl), 780, 480);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000415C File Offset: 0x0000235C
		public TokenCredential FinishLogin(byte[] serializedContext, Uri callbackUri, string state)
		{
			Utilities.ValidateFinishLoginArguments(callbackUri, state);
			NameValueCollection nameValueCollection = Utilities.ValidateResponse(this.services, callbackUri.Query, state);
			this.googleAnalyticsOnFinishLogin["code"] = nameValueCollection["code"];
			OAuthToken oauthToken = Utilities.FinishLogin(this.services, this.GoogleTokenUri, this.googleAnalyticsOnFinishLogin);
			return this.CreateCredential(oauthToken);
		}

		// Token: 0x04000097 RID: 151
		protected readonly OAuthClientApplication clientApplication;

		// Token: 0x04000098 RID: 152
		protected readonly Dictionary<string, string> googleAnalyticsOnStartLogin;

		// Token: 0x04000099 RID: 153
		protected readonly NameValueCollection googleAnalyticsOnFinishLogin;

		// Token: 0x0400009A RID: 154
		protected readonly NameValueCollection googleAnalyticsOnRefresh;

		// Token: 0x0400009B RID: 155
		protected readonly string scope;

		// Token: 0x0400009C RID: 156
		protected readonly OAuthServices services;
	}
}
