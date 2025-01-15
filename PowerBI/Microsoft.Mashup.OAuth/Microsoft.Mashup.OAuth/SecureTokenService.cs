using System;

namespace Microsoft.Mashup.OAuth
{
	// Token: 0x02000024 RID: 36
	public sealed class SecureTokenService : ISecureTokenService
	{
		// Token: 0x060000FF RID: 255 RVA: 0x00005804 File Offset: 0x00003A04
		public SecureTokenService(string authorityId)
			: this(authorityId, authorityId + "/{TENANT}/oauth2/authorize", authorityId + "/{TENANT}/oauth2/token", authorityId + "/{TENANT}/oauth2/logout")
		{
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000582E File Offset: 0x00003A2E
		public SecureTokenService(string authorityId, string authorizeUriTemplate, string tokenUriTemplate, string logoutUriTemplate)
			: this(authorityId, authorizeUriTemplate, tokenUriTemplate, logoutUriTemplate, "{TENANT}", "common")
		{
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005848 File Offset: 0x00003A48
		public SecureTokenService(string authorityId, string authorizeUriTemplate, string tokenUriTemplate, string logoutUriTemplate, string tenantPlaceholder, string defaultTenant)
		{
			if (string.IsNullOrEmpty(authorityId))
			{
				throw new ArgumentNullException("authorityId");
			}
			if (string.IsNullOrEmpty(authorizeUriTemplate))
			{
				throw new ArgumentNullException("authorizeUriTemplate");
			}
			if (string.IsNullOrEmpty(tokenUriTemplate))
			{
				throw new ArgumentNullException("tokenUriTemplate");
			}
			if (string.IsNullOrEmpty(logoutUriTemplate))
			{
				throw new ArgumentNullException("logoutUriTemplate");
			}
			if (string.IsNullOrEmpty(tenantPlaceholder))
			{
				throw new ArgumentNullException("tenantPlaceholder");
			}
			if (string.IsNullOrEmpty(defaultTenant))
			{
				throw new ArgumentNullException("defaultTenant");
			}
			this.CheckHttps(authorityId, "authorityId");
			this.CheckHttps(authorizeUriTemplate, "authorizeUriTemplate");
			this.CheckHttps(tokenUriTemplate, "tokenUriTemplate");
			this.CheckHttps(logoutUriTemplate, "logoutUriTemplate");
			this.authorityId = authorityId;
			this.authorizeUriTemplate = authorizeUriTemplate;
			this.tokenUriTemplate = tokenUriTemplate;
			this.logoutUriTemplate = logoutUriTemplate;
			this.tenantPlaceholder = tenantPlaceholder;
			this.defaultTenant = defaultTenant;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000102 RID: 258 RVA: 0x0000592E File Offset: 0x00003B2E
		public string AuthorityId
		{
			get
			{
				return this.authorityId;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00005936 File Offset: 0x00003B36
		internal string AuthorizeUriTemplate
		{
			get
			{
				return this.authorizeUriTemplate;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000104 RID: 260 RVA: 0x0000593E File Offset: 0x00003B3E
		internal string TokenUriTemplate
		{
			get
			{
				return this.tokenUriTemplate;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00005946 File Offset: 0x00003B46
		internal string LogoutUriTemplate
		{
			get
			{
				return this.logoutUriTemplate;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000106 RID: 262 RVA: 0x0000594E File Offset: 0x00003B4E
		internal string TenantPlaceholder
		{
			get
			{
				return this.tenantPlaceholder;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00005956 File Offset: 0x00003B56
		internal string DefaultTenant
		{
			get
			{
				return this.defaultTenant;
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005960 File Offset: 0x00003B60
		public static ISecureTokenService CreateAdfsSecureTokenService(string authenticationEndpoint)
		{
			if (string.IsNullOrEmpty(authenticationEndpoint))
			{
				throw new ArgumentNullException("authenticationEndpoint");
			}
			string authority = Utilities.GetAuthority(authenticationEndpoint);
			UriBuilder uriBuilder = new UriBuilder(authority);
			uriBuilder.Path = "/adfs/oauth2/token";
			string absoluteUri = uriBuilder.Uri.AbsoluteUri;
			uriBuilder.Path = "/adfs/ls/";
			uriBuilder.Query = "wa=wsignout1.0";
			string absoluteUri2 = uriBuilder.Uri.AbsoluteUri;
			return new SecureTokenService(authority, authenticationEndpoint, absoluteUri, absoluteUri2);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000059CC File Offset: 0x00003BCC
		public static bool IsAdfsSts(Uri tokenUrl)
		{
			return string.Equals(tokenUrl.PathAndQuery, "/adfs/oauth2/token");
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000059DE File Offset: 0x00003BDE
		public Uri GetAuthorizeUri(string tenant)
		{
			return this.GetTenantBasedUri(this.authorizeUriTemplate, tenant);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000059ED File Offset: 0x00003BED
		public Uri GetTokenUri(string tenant)
		{
			return this.GetTenantBasedUri(this.tokenUriTemplate, tenant);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000059FC File Offset: 0x00003BFC
		public Uri GetLogoutUri(string tenant)
		{
			return this.GetTenantBasedUri(this.logoutUriTemplate, tenant);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005A0B File Offset: 0x00003C0B
		public static SecureTokenService CreateTenant(ISecureTokenService service, string tenant)
		{
			return new SecureTokenService(service.AuthorityId, service.GetAuthorizeUri(tenant).AbsoluteUri, service.GetTokenUri(tenant).AbsoluteUri, service.GetLogoutUri(tenant).AbsoluteUri);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005A3C File Offset: 0x00003C3C
		public override bool Equals(object obj)
		{
			SecureTokenService secureTokenService = obj as SecureTokenService;
			return secureTokenService != null && (secureTokenService.authorityId == this.authorityId && secureTokenService.authorizeUriTemplate == this.authorizeUriTemplate && secureTokenService.defaultTenant == this.defaultTenant && secureTokenService.logoutUriTemplate == this.logoutUriTemplate && secureTokenService.tenantPlaceholder == this.tenantPlaceholder) && secureTokenService.tokenUriTemplate == this.tokenUriTemplate;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00005AC8 File Offset: 0x00003CC8
		public override int GetHashCode()
		{
			return this.authorityId.GetHashCode() ^ this.authorizeUriTemplate.GetHashCode() ^ this.defaultTenant.GetHashCode() ^ this.logoutUriTemplate.GetHashCode() ^ this.tenantPlaceholder.GetHashCode() ^ this.tokenUriTemplate.GetHashCode();
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00005B1C File Offset: 0x00003D1C
		private Uri GetTenantBasedUri(string template, string tenant)
		{
			if (!string.IsNullOrEmpty(tenant))
			{
				return new Uri(template.Replace(this.tenantPlaceholder, tenant), UriKind.Absolute);
			}
			return new Uri(template.Replace(this.tenantPlaceholder, this.defaultTenant), UriKind.Absolute);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005B52 File Offset: 0x00003D52
		private void CheckHttps(string template, string templateName)
		{
			if (!template.StartsWith(Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase))
			{
				throw new OAuthException(OAuthStrings.HttpsRequired(templateName));
			}
		}

		// Token: 0x040000E3 RID: 227
		private const string DefaultTenantPlaceholder = "{TENANT}";

		// Token: 0x040000E4 RID: 228
		private const string DefaultDefaultTenant = "common";

		// Token: 0x040000E5 RID: 229
		private const string DefaultAuthorizeTemplate = "/{TENANT}/oauth2/authorize";

		// Token: 0x040000E6 RID: 230
		private const string DefaultTokenTemplate = "/{TENANT}/oauth2/token";

		// Token: 0x040000E7 RID: 231
		private const string DefaultLogoutTemplate = "/{TENANT}/oauth2/logout";

		// Token: 0x040000E8 RID: 232
		private readonly string authorityId;

		// Token: 0x040000E9 RID: 233
		private readonly string authorizeUriTemplate;

		// Token: 0x040000EA RID: 234
		private readonly string tokenUriTemplate;

		// Token: 0x040000EB RID: 235
		private readonly string logoutUriTemplate;

		// Token: 0x040000EC RID: 236
		private readonly string tenantPlaceholder;

		// Token: 0x040000ED RID: 237
		private readonly string defaultTenant;
	}
}
