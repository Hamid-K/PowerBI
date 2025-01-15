using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.OAuth
{
	// Token: 0x02000010 RID: 16
	public sealed class GoogleOAuthProvider : GoogleOAuthProviderBase
	{
		// Token: 0x0600006C RID: 108 RVA: 0x00003DA7 File Offset: 0x00001FA7
		public GoogleOAuthProvider(OAuthClientApplication clientApplication, string[] scopes)
			: this(OAuthServices.Empty, clientApplication, scopes)
		{
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003DB6 File Offset: 0x00001FB6
		public GoogleOAuthProvider(IOAuthTracingService tracingService, OAuthClientApplication clientApplication, string[] scopes)
			: this(OAuthServices.From(tracingService), clientApplication, scopes)
		{
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003DC8 File Offset: 0x00001FC8
		public GoogleOAuthProvider(OAuthServices services, OAuthClientApplication clientApplication, string[] scopes)
			: base(services, clientApplication, scopes)
		{
			this.googleAnalyticsOnStartLogin.Add("approval_prompt", "force");
			this.googleAnalyticsOnRefresh.Add("client_id", this.clientApplication.Id);
			this.googleAnalyticsOnRefresh.Add("client_secret", this.clientApplication.Secret);
			this.googleAnalyticsOnRefresh.Add("grant_type", "refresh_token");
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00003E3E File Offset: 0x0000203E
		protected override Uri GoogleTokenUri
		{
			get
			{
				return GoogleOAuthProvider.googleTokenUri;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00003E45 File Offset: 0x00002045
		protected override Uri GoogleAuthorizeUri
		{
			get
			{
				return GoogleOAuthProvider.googleAuthorizeUri;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003E4C File Offset: 0x0000204C
		protected override Uri GoogleLogoutUri
		{
			get
			{
				return GoogleOAuthProvider.googleLogoutUri;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003E53 File Offset: 0x00002053
		protected override Uri AccountsListUrl
		{
			get
			{
				return GoogleOAuthProvider.accountsListUrl;
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003E5C File Offset: 0x0000205C
		public override TokenCredential Refresh(TokenCredential credential)
		{
			Utilities.ValidateRefreshArguments(credential);
			OAuthToken oauthToken = Utilities.RefreshJsonToken(this.services, credential.RefreshToken, this.GoogleTokenUri, this.googleAnalyticsOnRefresh);
			if (credential.Properties.ContainsKey("gmail"))
			{
				return new TokenCredential(oauthToken.AccessToken, oauthToken.Expires, oauthToken.RefreshToken, credential.Properties);
			}
			return this.CreateCredential(oauthToken);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003EC4 File Offset: 0x000020C4
		public override Uri Logout(string accessToken)
		{
			if (accessToken != null)
			{
				return Utilities.AddQueryParametersToUri(this.GoogleLogoutUri, new Dictionary<string, string> { { "token", accessToken } });
			}
			return null;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003EE8 File Offset: 0x000020E8
		internal override TokenCredential CreateCredential(OAuthToken token)
		{
			WebRequest webRequest = this.services.CreateRequest(GoogleOAuthProvider.accountsListUrl);
			webRequest.Headers[HttpRequestHeader.Authorization] = string.Format(CultureInfo.InvariantCulture, "{0} {1}", "Bearer", token.AccessToken);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			try
			{
				using (WebResponse response = webRequest.GetResponse())
				{
					using (Stream responseStream = response.GetResponseStream())
					{
						GoogleOAuthProvider.GoogleAccountInfo googleAccountInfo = Utilities.DecodeJsonObject<GoogleOAuthProvider.GoogleAccountInfo>(responseStream);
						dictionary["gmail"] = googleAccountInfo.Username;
					}
				}
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw ex;
				}
			}
			return new TokenCredential(token.AccessToken, token.Expires, token.RefreshToken, dictionary);
		}

		// Token: 0x04000093 RID: 147
		private static readonly Uri googleTokenUri = new Uri("https://accounts.google.com/o/oauth2/token");

		// Token: 0x04000094 RID: 148
		private static readonly Uri googleAuthorizeUri = new Uri("https://accounts.google.com/o/oauth2/auth");

		// Token: 0x04000095 RID: 149
		private static readonly Uri googleLogoutUri = new Uri("https://accounts.google.com/o/oauth2/revoke");

		// Token: 0x04000096 RID: 150
		private static readonly Uri accountsListUrl = new Uri("https://www.googleapis.com/analytics/v3/management/accounts?start-index=1");

		// Token: 0x0200002E RID: 46
		[DataContract]
		private sealed class GoogleAccountInfo
		{
			// Token: 0x17000056 RID: 86
			// (get) Token: 0x0600016A RID: 362 RVA: 0x000071EE File Offset: 0x000053EE
			// (set) Token: 0x0600016B RID: 363 RVA: 0x000071F6 File Offset: 0x000053F6
			[DataMember(Name = "username")]
			public string Username { get; set; }
		}
	}
}
