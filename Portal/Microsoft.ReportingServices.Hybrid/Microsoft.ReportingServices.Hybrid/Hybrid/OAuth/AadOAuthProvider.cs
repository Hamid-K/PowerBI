using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Hybrid.OAuth
{
	// Token: 0x02000007 RID: 7
	internal sealed class AadOAuthProvider : IAadOAuthProvider
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600000C RID: 12 RVA: 0x0000234C File Offset: 0x0000054C
		// (remove) Token: 0x0600000D RID: 13 RVA: 0x00002384 File Offset: 0x00000584
		public event AadOAuthProvider.UserLoginHandler LoginSuccessful;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600000E RID: 14 RVA: 0x000023BC File Offset: 0x000005BC
		// (remove) Token: 0x0600000F RID: 15 RVA: 0x000023F4 File Offset: 0x000005F4
		public event AadOAuthProvider.UserLoginHandler LoginFailed;

		// Token: 0x06000010 RID: 16 RVA: 0x00002429 File Offset: 0x00000629
		public AadOAuthProvider(IAadCache cache, IPowerBIOAuthConfiguration serviceConfiguration)
			: this(cache, serviceConfiguration, new ServiceTokenStore())
		{
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002438 File Offset: 0x00000638
		public AadOAuthProvider(IAadCache cache, IPowerBIOAuthConfiguration serviceConfiguration, IServiceTokenStore serviceTokenStore)
		{
			this.m_cache = cache;
			this.m_serviceConfig = serviceConfiguration;
			if (string.IsNullOrEmpty(this.m_cache.GetSessionState()))
			{
				this.m_cache.SetSessionState(Guid.NewGuid().ToString());
			}
			RSTrace.CatalogTrace.Assert(this.m_serviceConfig != null, "PowerBI Connection configuration is either invalid or missing.");
			UriBuilder urlBuilder = AadOAuthHelper.GetUrlBuilder(this.m_serviceConfig.AuthorizationUrl);
			this.m_authResponse = HttpUtility.ParseQueryString(urlBuilder.Query);
			this.m_authResponse["client_id"] = this.m_serviceConfig.ClientId;
			this.m_authResponse["response_type"] = "code";
			Uri requestRedirectUrl = AadOAuthHelper.GetReportManagerAADUrl();
			string text = null;
			if (requestRedirectUrl != null && this.m_serviceConfig.RedirectUrls != null)
			{
				text = this.m_serviceConfig.RedirectUrls.Find((string u) => requestRedirectUrl.Equals(new Uri(u.ToLowerInvariant())));
			}
			this.m_authResponse["redirect_uri"] = text ?? this.m_serviceConfig.RedirectUrls[0];
			this.m_authResponse["resource"] = this.m_serviceConfig.ResourceUrl;
			this.m_authResponse["nux"] = "1";
			this.m_authResponse["mkt"] = CultureInfo.CurrentUICulture.Name;
			if (string.IsNullOrEmpty(this.m_serviceConfig.ClientSecret))
			{
				this.m_tokenResponse = new NameValueCollection(4);
				this.m_refreshResponse = new NameValueCollection(3);
			}
			else
			{
				this.m_tokenResponse = new NameValueCollection(5);
				this.m_tokenResponse["client_secret"] = this.m_serviceConfig.ClientSecret;
				this.m_refreshResponse = new NameValueCollection(4);
				this.m_refreshResponse["client_secret"] = this.m_serviceConfig.ClientSecret;
			}
			this.m_tokenResponse["client_id"] = this.m_serviceConfig.ClientId;
			this.m_tokenResponse["grant_type"] = "authorization_code";
			this.m_tokenResponse["redirect_uri"] = text ?? this.m_serviceConfig.RedirectUrls[0];
			this.m_refreshResponse["client_id"] = this.m_serviceConfig.ClientId;
			this.m_refreshResponse["grant_type"] = "refresh_token";
			this.m_serviceTokenStore = serviceTokenStore;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000026B5 File Offset: 0x000008B5
		// (set) Token: 0x06000013 RID: 19 RVA: 0x000026C2 File Offset: 0x000008C2
		public string AuthorizationCode
		{
			get
			{
				return this.m_cache.GetAuthorizationCodeFromCache();
			}
			set
			{
				this.m_cache.SaveAuthorizationCodeInCache(value);
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000026D0 File Offset: 0x000008D0
		public ServiceToken AcquireToken()
		{
			ServiceToken serviceTokenFromCache = this.m_cache.GetServiceTokenFromCache();
			if (serviceTokenFromCache != null && (AadOAuthProvider.ConvertFromTimeT(serviceTokenFromCache.ExpiresOn) - DateTime.UtcNow).TotalSeconds > 30.0)
			{
				return serviceTokenFromCache;
			}
			return this.AcquireTokenUsingRefreshToken();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000271C File Offset: 0x0000091C
		private ServiceToken AcquireTokenUsingRefreshToken()
		{
			ServiceToken serviceToken = this.m_cache.GetServiceTokenFromCache();
			if (serviceToken == null)
			{
				return null;
			}
			this.m_refreshResponse["refresh_token"] = serviceToken.RefreshToken;
			string idToken = serviceToken.IdToken;
			serviceToken = AadOAuthHelper.GetTokenFromRequestValues(AadOAuthHelper.GetUrlBuilder(this.m_serviceConfig.TokenUrl).Uri, this.m_refreshResponse, this.m_serviceTokenStore);
			if (!string.IsNullOrEmpty(idToken))
			{
				serviceToken.IdToken = idToken;
			}
			if (serviceToken.Error == null)
			{
				this.m_cache.SaveServiceTokenInCache(serviceToken);
				return serviceToken;
			}
			string text = serviceToken.Error + ": " + serviceToken.ErrorDescription;
			if (serviceToken.Error.Equals("interaction_required", StringComparison.OrdinalIgnoreCase) || serviceToken.Error.Equals("temporarily_unavailable", StringComparison.OrdinalIgnoreCase))
			{
				throw new AadCommunicationException(text, null);
			}
			this.m_cache.RemoveServiceTokenFromCache();
			throw new UnauthorizedAccessException(text);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000027FC File Offset: 0x000009FC
		private ServiceToken AcquireNewToken()
		{
			LogInEventArgs logInEventArgs;
			if (this.AuthorizationCode == null)
			{
				logInEventArgs = new LogInEventArgs(null, "Failed to get Authorization Token");
				this.OnLoginFailed(logInEventArgs);
				return null;
			}
			this.m_tokenResponse["code"] = this.AuthorizationCode;
			ServiceToken tokenFromRequestValues = AadOAuthHelper.GetTokenFromRequestValues(AadOAuthHelper.GetUrlBuilder(this.m_serviceConfig.TokenUrl).Uri, this.m_tokenResponse, this.m_serviceTokenStore);
			if (tokenFromRequestValues.Error == null)
			{
				ServiceIdToken idTokenFromResponseString = AadOAuthHelper.GetIdTokenFromResponseString(tokenFromRequestValues.IdToken);
				this.m_cache.SaveServiceTokenInCache(tokenFromRequestValues);
				logInEventArgs = new LogInEventArgs(idTokenFromResponseString, null);
				this.OnLoginSuccessful(logInEventArgs);
				return tokenFromRequestValues;
			}
			string text = tokenFromRequestValues.Error + ": " + tokenFromRequestValues.ErrorDescription;
			logInEventArgs = new LogInEventArgs(null, text);
			this.OnLoginFailed(logInEventArgs);
			if (tokenFromRequestValues.Error.Equals("interaction_required", StringComparison.OrdinalIgnoreCase) || tokenFromRequestValues.Error.Equals("temporarily_unavailable", StringComparison.OrdinalIgnoreCase))
			{
				throw new AadCommunicationException(text, null);
			}
			throw new UnauthorizedAccessException(text);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000028EF File Offset: 0x00000AEF
		private void OnLoginSuccessful(LogInEventArgs args)
		{
			if (this.LoginSuccessful != null)
			{
				this.LoginSuccessful(this, args);
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002906 File Offset: 0x00000B06
		private void OnLoginFailed(LogInEventArgs args)
		{
			if (this.LoginFailed != null)
			{
				this.LoginFailed(this, args);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002920 File Offset: 0x00000B20
		public string GetAuthorizationUrl()
		{
			UriBuilder urlBuilder = AadOAuthHelper.GetUrlBuilder(this.m_serviceConfig.AuthorizationUrl);
			this.m_authResponse["state"] = this.GetStateFromSession();
			this.m_authResponse["login"] = "prompt";
			urlBuilder.Query = this.m_authResponse.ToString();
			return urlBuilder.Uri.ToString();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002984 File Offset: 0x00000B84
		public bool UpdateToken(Uri uri)
		{
			NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(uri.Query);
			if (!nameValueCollection.AllKeys.Contains("code") || !nameValueCollection.AllKeys.Contains("state"))
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Error, string.Format("AAD login failed. Returned Url from AAD: {0}", uri));
				return false;
			}
			if (string.Compare(nameValueCollection["state"], this.GetStateFromSession(), StringComparison.OrdinalIgnoreCase) != 0)
			{
				return false;
			}
			this.AuthorizationCode = nameValueCollection["code"];
			this.AcquireNewToken();
			RSTrace.CatalogTrace.Trace(TraceLevel.Verbose, string.Format("AAD login succeeded. Returned Url from AAD: {0}", uri));
			return true;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002A24 File Offset: 0x00000C24
		private string GetStateFromSession()
		{
			string sessionState = this.m_cache.GetSessionState();
			RSTrace.CatalogTrace.Assert(!string.IsNullOrEmpty(sessionState), "State value not found in session cache. Will not be able to verify authenticity of authorization codes.");
			return sessionState;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002A56 File Offset: 0x00000C56
		private static string GetScript()
		{
			return new StreamReader(Assembly.GetAssembly(typeof(AadOAuthProvider)).GetManifestResourceStream("Microsoft.ReportingServices.Hybrid.OAuth.Redirect_Util.js")).ReadToEnd();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002A7C File Offset: 0x00000C7C
		private static DateTime ConvertFromTimeT(long seconds)
		{
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
			return DateTime.SpecifyKind(dateTime.AddSeconds((double)seconds), DateTimeKind.Utc);
		}

		// Token: 0x04000036 RID: 54
		private readonly IPowerBIOAuthConfiguration m_serviceConfig;

		// Token: 0x04000037 RID: 55
		private readonly NameValueCollection m_authResponse;

		// Token: 0x04000038 RID: 56
		private readonly NameValueCollection m_tokenResponse;

		// Token: 0x04000039 RID: 57
		private readonly NameValueCollection m_refreshResponse;

		// Token: 0x0400003A RID: 58
		private readonly IAadCache m_cache;

		// Token: 0x0400003B RID: 59
		private readonly IServiceTokenStore m_serviceTokenStore;

		// Token: 0x0400003C RID: 60
		private const string c_interactionRequired = "interaction_required";

		// Token: 0x0400003D RID: 61
		private const string c_temporarilyUnavailable = "temporarily_unavailable";

		// Token: 0x0400003E RID: 62
		private const string c_urlParamCode = "code";

		// Token: 0x0400003F RID: 63
		private const string c_urlParamState = "state";

		// Token: 0x02000013 RID: 19
		// (Invoke) Token: 0x06000076 RID: 118
		public delegate void UserLoginHandler(object sender, LogInEventArgs a);
	}
}
