using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;

namespace Microsoft.Mashup.OAuth
{
	// Token: 0x02000026 RID: 38
	public sealed class SharePointOAuthProvider : IOAuthProvider
	{
		// Token: 0x06000119 RID: 281 RVA: 0x00005C35 File Offset: 0x00003E35
		public SharePointOAuthProvider(string resourceUrl)
			: this(OAuthServices.Empty, resourceUrl)
		{
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005C43 File Offset: 0x00003E43
		public SharePointOAuthProvider(IOAuthTracingService tracingService, string resourceUrl)
			: this(OAuthServices.From(tracingService), resourceUrl)
		{
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005C52 File Offset: 0x00003E52
		public SharePointOAuthProvider(OAuthServices services, string resourceUrl)
		{
			if (string.IsNullOrEmpty(resourceUrl))
			{
				throw new ArgumentNullException("resourceUrl");
			}
			this.resourceUrl = resourceUrl;
			this.services = services;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00005C7B File Offset: 0x00003E7B
		public OAuthBrowserNavigation StartLogin(string state, string display)
		{
			if (this.AuthorizeUri == null)
			{
				throw new OAuthException(OAuthStrings.MS_Online_ID_Not_Supported);
			}
			return new OAuthBrowserNavigation(this.AuthorizeUri, new Uri(this.SharePointRedirectUrl), 730, 1050);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00005CB8 File Offset: 0x00003EB8
		public TokenCredential FinishLogin(byte[] serializedContext, Uri callbackUri, string state)
		{
			CookieCollection cookieCollection;
			if (CookieHelper.TryGetCookies(this.AuthorizeUri, SharePointOAuthProvider.FormsBasedAuthCookieNames, out cookieCollection))
			{
				return new TokenCredential(CookieHelper.SerializeCookies(cookieCollection), Utilities.GetExpiresInString(CookieHelper.GetMinExpiry(cookieCollection)), null, new Dictionary<string, string> { { "ProviderType", "SharePointFBA" } });
			}
			throw new OAuthException(OAuthStrings.FinishLoginMissingFormBasedCookies);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00005D10 File Offset: 0x00003F10
		public TokenCredential Refresh(TokenCredential credential)
		{
			return credential;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00005D13 File Offset: 0x00003F13
		public Uri Logout(string accessToken)
		{
			return this.LogoutUri;
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00005D1B File Offset: 0x00003F1B
		private string SharePointRedirectUrl
		{
			get
			{
				if (this.sharePointRedirectUrl == null)
				{
					this.RequestAuthorizeUrls();
				}
				return this.sharePointRedirectUrl;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00005D31 File Offset: 0x00003F31
		private Uri AuthorizeUri
		{
			get
			{
				if (this.sharePointAuthorizeUri == null)
				{
					this.RequestAuthorizeUrls();
				}
				return this.sharePointAuthorizeUri;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00005D4D File Offset: 0x00003F4D
		private Uri LogoutUri
		{
			get
			{
				if (this.sharePointLogoutUri == null)
				{
					this.RequestAuthorizeUrls();
				}
				return this.sharePointLogoutUri;
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00005D6C File Offset: 0x00003F6C
		private void RequestAuthorizeUrls()
		{
			WebRequest webRequest = this.services.CreateRequest(new Uri(new Uri(this.resourceUrl).GetLeftPart(UriPartial.Authority)));
			webRequest.Method = "OPTIONS";
			try
			{
				using (webRequest.GetResponse())
				{
				}
			}
			catch (WebException ex)
			{
				this.services.Write("OAuth/SharePointOAuthProvider/RequestAuthorizeUrls", ex);
				if (ex.Status == WebExceptionStatus.ProtocolError)
				{
					WebResponse response2 = ex.Response;
					if (response2 != null && this.services.GetResponseStatus(response2) == HttpStatusCode.Forbidden && response2.Headers.AllKeys.Contains("X-Forms_Based_Auth_Required", StringComparer.OrdinalIgnoreCase) && response2.Headers.AllKeys.Contains("X-Forms_Based_Auth_Return_Url", StringComparer.OrdinalIgnoreCase))
					{
						this.sharePointAuthorizeUri = new Uri(response2.Headers["X-Forms_Based_Auth_Required"]);
						this.sharePointRedirectUrl = response2.Headers["X-Forms_Based_Auth_Return_Url"];
						string text = Uri.UnescapeDataString(new UriBuilder(this.sharePointAuthorizeUri)
						{
							Path = "/_layouts/signout.aspx",
							Query = null
						}.Uri.AbsoluteUri);
						if (Uri.IsWellFormedUriString(text, UriKind.Absolute))
						{
							this.sharePointLogoutUri = new Uri(text);
						}
					}
				}
			}
			catch (Exception ex2)
			{
				if (!SafeExceptions.TraceIsSafeException(ex2))
				{
					throw;
				}
				this.services.Write("OAuth/SharePointOAuthProvider/RequestAuthorizeUrls", TraceEventType.Error, new object[] { "Exception", ex2 });
			}
			webRequest.Abort();
		}

		// Token: 0x040000F0 RID: 240
		private static readonly HashSet<string> FormsBasedAuthCookieNames = new HashSet<string> { "FedAuth", "rtFa" };

		// Token: 0x040000F1 RID: 241
		private const string FormsBasedAuthRequiredHeaderName = "X-Forms_Based_Auth_Required";

		// Token: 0x040000F2 RID: 242
		private const string FormsBasedAuthReturnUrlHeaderName = "X-Forms_Based_Auth_Return_Url";

		// Token: 0x040000F3 RID: 243
		private const string SharePointLogoutUrlPath = "/_layouts/signout.aspx";

		// Token: 0x040000F4 RID: 244
		private const string WebRequestMethodOptions = "OPTIONS";

		// Token: 0x040000F5 RID: 245
		private Uri sharePointAuthorizeUri;

		// Token: 0x040000F6 RID: 246
		private Uri sharePointLogoutUri;

		// Token: 0x040000F7 RID: 247
		private string sharePointRedirectUrl;

		// Token: 0x040000F8 RID: 248
		private readonly string resourceUrl;

		// Token: 0x040000F9 RID: 249
		private readonly OAuthServices services;
	}
}
