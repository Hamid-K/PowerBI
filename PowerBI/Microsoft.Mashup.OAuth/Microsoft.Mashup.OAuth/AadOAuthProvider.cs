using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace Microsoft.Mashup.OAuth
{
	// Token: 0x02000006 RID: 6
	public sealed class AadOAuthProvider : IOAuthProvider
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002176 File Offset: 0x00000376
		public AadOAuthProvider(OAuthClientApplication clientApplication, string resourceUrl)
			: this(clientApplication, resourceUrl, null)
		{
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002181 File Offset: 0x00000381
		public AadOAuthProvider(OAuthClientApplication clientApplication, string resourceUrl, string locale)
			: this(clientApplication, resourceUrl, locale, null)
		{
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000218D File Offset: 0x0000038D
		public AadOAuthProvider(OAuthClientApplication clientApplication, string resourceUrl, string locale, string scope)
			: this(OAuthServices.Empty, clientApplication, AadOAuthProvider.DefaultSettings, resourceUrl, locale, scope)
		{
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000021A4 File Offset: 0x000003A4
		public AadOAuthProvider(IOAuthTracingService tracingService, OAuthClientApplication clientApplication, string resourceUrl, string locale)
			: this(OAuthServices.From(tracingService), clientApplication, AadOAuthProvider.DefaultSettings, resourceUrl, locale)
		{
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000021BB File Offset: 0x000003BB
		public AadOAuthProvider(OAuthServices services, OAuthClientApplication clientApplication, string resourceUrl, string locale)
			: this(services, clientApplication, AadOAuthProvider.DefaultSettings, resourceUrl, locale)
		{
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000021CD File Offset: 0x000003CD
		public AadOAuthProvider(OAuthServices services, OAuthClientApplication clientApplication, string resourceUrl, string locale, string scope)
			: this(services, clientApplication, AadOAuthProvider.DefaultSettings, resourceUrl, locale, null, scope)
		{
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000021E2 File Offset: 0x000003E2
		public AadOAuthProvider(OAuthClientApplication clientApplication, OAuthSettings settings, string resourceUrl, string locale)
			: this(clientApplication, settings, resourceUrl, locale, null)
		{
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000021F0 File Offset: 0x000003F0
		public AadOAuthProvider(OAuthClientApplication clientApplication, OAuthSettings settings, string resourceUrl, string locale, string scope)
			: this(OAuthServices.Empty, clientApplication, settings, resourceUrl, locale, scope)
		{
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002204 File Offset: 0x00000404
		public AadOAuthProvider(IOAuthTracingService tracingService, OAuthClientApplication clientApplication, OAuthSettings settings, string resourceUrl, string locale)
			: this(OAuthServices.From(tracingService), clientApplication, settings, resourceUrl, locale, null)
		{
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002219 File Offset: 0x00000419
		public AadOAuthProvider(OAuthServices services, OAuthClientApplication clientApplication, OAuthSettings settings, string resourceUrl, string locale)
			: this(services, clientApplication, settings, resourceUrl, locale, null)
		{
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002229 File Offset: 0x00000429
		public AadOAuthProvider(OAuthServices services, OAuthClientApplication clientApplication, OAuthSettings settings, string resourceUrl, string locale, string scope)
			: this(services, clientApplication, settings, resourceUrl, locale, null, scope)
		{
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000223B File Offset: 0x0000043B
		public AadOAuthProvider(IOAuthTracingService tracingService, OAuthClientApplication clientApplication, OAuthResource resource)
			: this(OAuthServices.From(tracingService), clientApplication, resource, null)
		{
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000224C File Offset: 0x0000044C
		public AadOAuthProvider(OAuthServices services, OAuthClientApplication clientApplication, OAuthResource resource)
			: this(services, clientApplication, resource, null)
		{
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002258 File Offset: 0x00000458
		private AadOAuthProvider(OAuthServices services, OAuthClientApplication clientApplication, OAuthSettings settings, string resourceUrl, string locale, string resourceId, string scope)
			: this(services, clientApplication, AadOAuthProvider.GetResource(services, settings, resourceUrl, resourceId, scope), locale)
		{
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002274 File Offset: 0x00000474
		private AadOAuthProvider(OAuthServices services, OAuthClientApplication clientApplication, OAuthResource resource, string locale)
		{
			if (clientApplication == null)
			{
				throw new ArgumentNullException("clientApplication");
			}
			if (resource == null)
			{
				throw new ArgumentNullException("resource");
			}
			this.services = services.AddInterestingHeader("x-ms-request-id");
			this.clientApplication = clientApplication;
			this.resource = resource;
			if (!OAuthResource.TryExtractResourceFromScopes(this.resource.Scope, out this.resources))
			{
				this.resources = new KeyValuePair<string, string>[]
				{
					new KeyValuePair<string, string>(this.resource.Resource, this.resource.Scope)
				};
			}
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
				{
					"resource",
					this.resources[0].Key
				},
				{ "response_type", "code" },
				{ "prompt", "select_account" }
			};
			string value = this.resources[0].Value;
			if (!string.IsNullOrEmpty(value))
			{
				this.aadOnStartLogin.Add("scope", value);
			}
			if (!string.IsNullOrEmpty(locale))
			{
				this.aadOnStartLogin["mkt"] = locale;
			}
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
				{ "grant_type", "refresh_token" }
			};
			this.aadOnLogout = new Dictionary<string, string> { 
			{
				"post_logout_redirect_uri",
				this.clientApplication.CallbackUrl
			} };
			if (!string.IsNullOrEmpty(this.clientApplication.Secret))
			{
				switch (this.clientApplication.SecretType)
				{
				case ClientApplicationSecretType.Default:
				case ClientApplicationSecretType.Thumbprint:
					services.WritePiiSafe("OAuth/AadOAuthProvider/GetSigningCertificate/ByThumbprint", TraceEventType.Verbose, Array.Empty<object>());
					this.signingCertificate = JwtHelpers.CertificateFromThumbprint(this.clientApplication.Secret);
					goto IL_0293;
				case ClientApplicationSecretType.SubjectNameIssuer:
				{
					services.WritePiiSafe("OAuth/AadOAuthProvider/GetSigningCertificate/BySubjectNameIssuer", TraceEventType.Verbose, Array.Empty<object>());
					SubjectNameIssuer subjectNameIssuer = this.clientApplication.SubjectNameIssuer;
					this.signingCertificate = JwtHelpers.CertificateFromSubjectNameIssuer(subjectNameIssuer.SubjectName, subjectNameIssuer.Issuer);
					goto IL_0293;
				}
				}
				throw new OAuthException(OAuthStrings.UnsupportedSecretType(this.clientApplication.SecretType));
				IL_0293:
				if (this.signingCertificate == null)
				{
					throw new OAuthException(OAuthStrings.CouldNotLoadCertificate(AadOAuthProvider.TruncateSecret(this.clientApplication.Secret)));
				}
				if (!this.signingCertificate.HasPrivateKey)
				{
					throw new OAuthException(OAuthStrings.NoPrivateKey(this.clientApplication.Secret));
				}
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000255A File Offset: 0x0000075A
		private static OAuthResource GetResource(OAuthServices services, OAuthSettings settings, string resourceUrl, string resourceId, string scope)
		{
			if (string.IsNullOrEmpty(resourceUrl) && string.IsNullOrEmpty(resourceId))
			{
				throw new ArgumentNullException("resourceUrl");
			}
			if (resourceId != null)
			{
				return AadOAuthProvider.CreateResourceForId(services, resourceId, scope, settings);
			}
			return AadOAuthProvider.CreateResourceForUrl(services, resourceUrl, scope, settings);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000258F File Offset: 0x0000078F
		public static AadOAuthProvider NewCommon(IOAuthTracingService tracingService, OAuthClientApplication clientApplication, string resourceId, string locale)
		{
			return new AadOAuthProvider(OAuthServices.From(tracingService), clientApplication, AadSettings.CommonSettings, null, locale, resourceId);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000025A5 File Offset: 0x000007A5
		public static AadOAuthProvider NewCommonPPE(IOAuthTracingService tracingService, OAuthClientApplication clientApplication, string resourceId, string locale)
		{
			return new AadOAuthProvider(OAuthServices.From(tracingService), clientApplication, AadSettings.CommonSettingsPPE, null, locale, resourceId);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000025BB File Offset: 0x000007BB
		public static OAuthResource CreateResourceForId(IOAuthTracingService tracingService, string resourceId, OAuthSettings settings = null)
		{
			return AadOAuthProvider.CreateResourceForId(OAuthServices.From(tracingService), resourceId, settings);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000025CA File Offset: 0x000007CA
		public static OAuthResource CreateResourceForUrl(IOAuthTracingService tracingService, string resourceUrl, OAuthSettings settings = null)
		{
			return AadOAuthProvider.CreateResourceForUrl(OAuthServices.From(tracingService), resourceUrl, settings);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000025D9 File Offset: 0x000007D9
		public static OAuthResource CreateResourceForAuthorizationUrl(IOAuthTracingService tracingService, string authorizationUrl, string resource, OAuthSettings settings = null)
		{
			return AadOAuthProvider.CreateResourceForAuthorizationUrl(OAuthServices.From(tracingService), authorizationUrl, resource, settings);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000025EC File Offset: 0x000007EC
		public static OAuthResource CreateResourceForAuthorizationUrl(OAuthServices services, string authorizationUrl, string resource, OAuthSettings settings = null)
		{
			settings = settings ?? AadOAuthProvider.DefaultSettings;
			ISecureTokenService serviceForBearerUri = new AadOAuthProvider.AadDiscovery(services).GetServiceForBearerUri(authorizationUrl, settings.AllowedSecureTokenServices);
			if (serviceForBearerUri != null)
			{
				return new OAuthResource(resource, ".default", serviceForBearerUri, null);
			}
			throw new OAuthException(OAuthStrings.NotTrustedSts, authorizationUrl);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002634 File Offset: 0x00000834
		public static bool GetAuthenticationUrl(IOAuthTracingService iTracingService, string url, out ISecureTokenService tokenService)
		{
			return AadOAuthProvider.GetAuthenticationUrl(OAuthServices.From(iTracingService), url, out tokenService);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002643 File Offset: 0x00000843
		public static bool GetAuthenticationUrl(OAuthServices services, string url, out ISecureTokenService tokenService)
		{
			return AadOAuthProvider.GetAuthenticationUrl(services, url, null, out tokenService);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002650 File Offset: 0x00000850
		public static bool GetAuthenticationUrl(OAuthServices services, string url, IEnumerable<KeyValuePair<string, string>> headers, out ISecureTokenService tokenService)
		{
			string text = null;
			string text2 = null;
			tokenService = null;
			services = services.AddInterestingHeader("x-ms-request-id");
			AadOAuthProvider.AadDiscovery aadDiscovery = new AadOAuthProvider.AadDiscovery(services);
			if (aadDiscovery.TryGetAuthenticationEndpoint(url, headers, out text, out text2))
			{
				tokenService = aadDiscovery.GetServiceForBearerUri(text, AadOAuthProvider.DefaultSettings.AllowedSecureTokenServices);
				return tokenService != null;
			}
			services.Write("OAuth/AadOAuthProvider/GetAuthenticationUrl", TraceEventType.Warning, new object[] { text2 });
			return false;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000026B8 File Offset: 0x000008B8
		public static OAuthResource CreateResourceForId(OAuthServices services, string resourceId, OAuthSettings settings = null)
		{
			settings = settings ?? AadSettings.CommonSettings;
			return new OAuthResource(resourceId, ".default", settings.AllowedSecureTokenServices[0], new Dictionary<string, string>());
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000026DE File Offset: 0x000008DE
		public static OAuthResource CreateResourceForId(OAuthServices services, string resourceId, string scope, OAuthSettings settings = null)
		{
			scope = scope ?? ".default";
			settings = settings ?? AadSettings.CommonSettings;
			return new OAuthResource(resourceId, scope, settings.AllowedSecureTokenServices[0], new Dictionary<string, string>());
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000270C File Offset: 0x0000090C
		public static OAuthResource CreateResourceForUrl(OAuthServices services, string resourceUrl, OAuthSettings settings = null)
		{
			services = services.AddInterestingHeader("x-ms-request-id");
			return AadOAuthProvider.CreateResourceForUrl(new AadOAuthProvider.AadDiscovery(services), resourceUrl, null, settings);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000272A File Offset: 0x0000092A
		public static OAuthResource CreateResourceForUrl(OAuthServices services, string resourceUrl, string scope, OAuthSettings settings = null)
		{
			services = services.AddInterestingHeader("x-ms-request-id");
			return AadOAuthProvider.CreateResourceForUrl(new AadOAuthProvider.AadDiscovery(services), resourceUrl, scope, settings);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002748 File Offset: 0x00000948
		private static OAuthResource CreateResourceForUrl(AadOAuthProvider.AadDiscovery discovery, string resourceUrl, string scope, OAuthSettings settings = null)
		{
			settings = settings ?? AadOAuthProvider.DefaultSettings;
			string text = discovery.EnsureAuthentication(resourceUrl, settings);
			if (scope == null)
			{
				scope = (SecureTokenService.IsAdfsSts(discovery.TokenService.GetTokenUri(null)) ? string.Empty : ".default");
			}
			return new OAuthResource(text, scope, discovery.TokenService, discovery.Properties);
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000279F File Offset: 0x0000099F
		public string AuthorityId
		{
			get
			{
				return this.resource.TokenService.AuthorityId;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000027B1 File Offset: 0x000009B1
		internal Uri AuthorizeUrl
		{
			get
			{
				return this.resource.TokenService.GetAuthorizeUri(null);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000027C4 File Offset: 0x000009C4
		internal Uri TokenUrl
		{
			get
			{
				return this.resource.TokenService.GetTokenUri(null);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000027D7 File Offset: 0x000009D7
		internal Uri LogoutUrl
		{
			get
			{
				return this.resource.TokenService.GetLogoutUri(null);
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000027EA File Offset: 0x000009EA
		public OAuthBrowserNavigation StartLogin(string state, string display)
		{
			return new OAuthBrowserNavigation(Utilities.GetLoginUri(this.AuthorizeUrl, this.aadOnStartLogin, state, display), new Uri(this.clientApplication.CallbackUrl), 628, 580);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002820 File Offset: 0x00000A20
		public TokenCredential FinishLogin(byte[] serializedContext, Uri callbackUri, string state)
		{
			Utilities.ValidateFinishLoginArguments(callbackUri, state);
			NameValueCollection nameValueCollection = Utilities.ValidateResponse(this.services, callbackUri.Query, state);
			this.aadOnFinishLogin["code"] = nameValueCollection["code"];
			if (this.signingCertificate != null)
			{
				this.SignRequest(this.aadOnFinishLogin, this.resources[0].Key);
			}
			OAuthToken oauthToken = Utilities.FinishLogin(this.services, this.TokenUrl, this.aadOnFinishLogin);
			return this.RefreshAll(oauthToken, this.resource.Properties);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000028B4 File Offset: 0x00000AB4
		public TokenCredential Refresh(TokenCredential credential)
		{
			Utilities.ValidateRefreshArguments(credential);
			OAuthToken oauthToken = this.Refresh(this.aadOnRefresh, credential.RefreshToken, this.resources[0].Key);
			return this.RefreshAll(oauthToken, credential.Properties);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000028F8 File Offset: 0x00000AF8
		public TokenCredential ExchangeToken(TokenCredential fromToken)
		{
			Utilities.ValidateRefreshArguments(fromToken);
			OAuthToken oauthToken = this.OnBehalfOf(fromToken.RefreshToken, this.resources[0].Key, this.resources[0].Value);
			return new TokenCredential(oauthToken.AccessToken, oauthToken.Expires, oauthToken.RefreshToken);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002951 File Offset: 0x00000B51
		public Uri Logout(string accessToken)
		{
			return Utilities.AddQueryParametersToUri(this.LogoutUrl, this.aadOnLogout);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002964 File Offset: 0x00000B64
		private OAuthToken Refresh(NameValueCollection attributes, string refreshToken, string resource)
		{
			if (this.signingCertificate != null)
			{
				this.SignRequest(attributes, resource);
			}
			OAuthToken oauthToken;
			try
			{
				oauthToken = Utilities.RefreshJsonToken(this.services, refreshToken, this.TokenUrl, attributes);
			}
			catch (OAuthWebException ex)
			{
				throw new AadOAuthException(Utilities.GetOAuthErrorFromResponseBytes<AadOAuthError>(ex.ResponseBody), ex);
			}
			return oauthToken;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000029BC File Offset: 0x00000BBC
		private TokenCredential RefreshAll(OAuthToken firstToken, Dictionary<string, string> startingProperties)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>(startingProperties);
			OAuthToken oauthToken = firstToken;
			if (this.resources.Length > 1)
			{
				dictionary[TokenCredential.EncodeAccessTokenKey(this.resources[0].Key)] = oauthToken.AccessToken;
			}
			for (int i = 1; i < this.resources.Length; i++)
			{
				oauthToken = this.OnBehalfOf(oauthToken.RefreshToken, this.resources[i].Key, this.resources[i].Value);
				dictionary[TokenCredential.EncodeAccessTokenKey(this.resources[i].Key)] = oauthToken.AccessToken;
			}
			return new TokenCredential(firstToken.AccessToken, firstToken.Expires, firstToken.RefreshToken, dictionary);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002A7C File Offset: 0x00000C7C
		private OAuthToken OnBehalfOf(string refreshToken, string resource, string scope)
		{
			if (!string.IsNullOrEmpty(scope) && SecureTokenService.IsAdfsSts(this.TokenUrl))
			{
				scope = null;
			}
			NameValueCollection nameValueCollection = new NameValueCollection(this.aadOnRefresh);
			nameValueCollection["resource"] = resource;
			if (!string.IsNullOrEmpty(scope))
			{
				nameValueCollection["scope"] = scope;
			}
			return this.Refresh(nameValueCollection, refreshToken, resource);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002AD8 File Offset: 0x00000CD8
		private void SignRequest(NameValueCollection properties, string resource)
		{
			properties["resource"] = resource;
			properties["client_assertion_type"] = "urn:ietf:params:oauth:client-assertion-type:jwt-bearer";
			properties["client_assertion"] = JwtHelpers.MakeSignature(this.clientApplication.Id, this.signingCertificate, this.TokenUrl.AbsoluteUri, this.clientApplication.SecretType == ClientApplicationSecretType.SubjectNameIssuer);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002B3B File Offset: 0x00000D3B
		private static string TruncateSecret(string secret)
		{
			if (secret.Length < 10)
			{
				return "...";
			}
			return secret.Substring(0, 2) + "..." + secret.Substring(secret.Length - 2);
		}

		// Token: 0x0400000A RID: 10
		private static readonly Dictionary<string, string> CrmGetOAuthUriQuery = new Dictionary<string, string> { { "SDKClientVersion", "7.0.0.2067" } };

		// Token: 0x0400000B RID: 11
		private static readonly Regex authorizationUriPattern = new Regex(".*authorization_uri\\s*=\\s*(?<authUrl>[^\\s,]*)?", RegexOptions.IgnoreCase);

		// Token: 0x0400000C RID: 12
		private static readonly Regex realmPattern = new Regex("realm=\\\"(?<realm>[^\"]+)\\\"", RegexOptions.IgnoreCase);

		// Token: 0x0400000D RID: 13
		private const string wwwAuthNResponseHeader = "WWW-Authenticate";

		// Token: 0x0400000E RID: 14
		private const string sharePointOnlineHeader = "MicrosoftSharePointTeamServices";

		// Token: 0x0400000F RID: 15
		private const string commonSubstring = "/common/";

		// Token: 0x04000010 RID: 16
		private static readonly List<string> wildCardEnabledAppIds = new List<string> { "Microsoft.CRM", "0000001b-0000-0000-c000-000000000000" };

		// Token: 0x04000011 RID: 17
		public static OAuthSettings DefaultSettings = AadSettings.Production;

		// Token: 0x04000012 RID: 18
		private readonly OAuthClientApplication clientApplication;

		// Token: 0x04000013 RID: 19
		private readonly NameValueCollection aadOnFinishLogin;

		// Token: 0x04000014 RID: 20
		private readonly NameValueCollection aadOnRefresh;

		// Token: 0x04000015 RID: 21
		private readonly Dictionary<string, string> aadOnStartLogin;

		// Token: 0x04000016 RID: 22
		private readonly Dictionary<string, string> aadOnLogout;

		// Token: 0x04000017 RID: 23
		private readonly OAuthServices services;

		// Token: 0x04000018 RID: 24
		private readonly OAuthResource resource;

		// Token: 0x04000019 RID: 25
		private readonly X509Certificate2 signingCertificate;

		// Token: 0x0400001A RID: 26
		private readonly KeyValuePair<string, string>[] resources;

		// Token: 0x0200002C RID: 44
		private class AadDiscovery
		{
			// Token: 0x0600015E RID: 350 RVA: 0x00006C1F File Offset: 0x00004E1F
			public AadDiscovery(OAuthServices services)
			{
				this.services = services;
			}

			// Token: 0x17000054 RID: 84
			// (get) Token: 0x0600015F RID: 351 RVA: 0x00006C2E File Offset: 0x00004E2E
			public ISecureTokenService TokenService
			{
				get
				{
					return this.tokenService;
				}
			}

			// Token: 0x17000055 RID: 85
			// (get) Token: 0x06000160 RID: 352 RVA: 0x00006C38 File Offset: 0x00004E38
			public Dictionary<string, string> Properties
			{
				get
				{
					Dictionary<string, string> dictionary = new Dictionary<string, string>(1);
					if (this.isSharePoint)
					{
						dictionary["ProviderType"] = "SharePointAAD";
					}
					return dictionary;
				}
			}

			// Token: 0x06000161 RID: 353 RVA: 0x00006C68 File Offset: 0x00004E68
			public string EnsureAuthentication(string resourceUrl, OAuthSettings settings)
			{
				string text;
				string text2;
				if (!this.TryGetAuthenticationEndpoint(resourceUrl, null, out text, out text2))
				{
					throw new OAuthException(text2);
				}
				string resourceValue = this.GetResourceValue(resourceUrl, settings.ResourceList);
				ISecureTokenService serviceForBearerUri = this.GetServiceForBearerUri(text, settings.AllowedSecureTokenServices);
				if (serviceForBearerUri != null)
				{
					this.tokenService = serviceForBearerUri;
					return resourceValue;
				}
				this.services.Write("OAuth/AadOAuthProvider/EnsureAuthentication", TraceEventType.Error, new object[] { "ResourceUrl", resourceUrl, "AuthorizationUrl", text });
				throw new OAuthException(OAuthStrings.NotTrustedSts, text);
			}

			// Token: 0x06000162 RID: 354 RVA: 0x00006CF0 File Offset: 0x00004EF0
			public ISecureTokenService GetServiceForBearerUri(string authorizationUri, IEnumerable<ISecureTokenService> allowedList)
			{
				if (!Uri.IsWellFormedUriString(authorizationUri, UriKind.Absolute))
				{
					OAuthServices oauthServices = this.services;
					string text = "OAuth/AadOAuthProvider/GetServiceForBearerUri/NotWellFormed";
					TraceEventType traceEventType = TraceEventType.Error;
					object[] array = new string[] { "authorizationUri", authorizationUri };
					oauthServices.Write(text, traceEventType, array);
					return null;
				}
				Uri uri = new Uri(authorizationUri);
				string authority = uri.GetLeftPart(UriPartial.Authority);
				ISecureTokenService secureTokenService = allowedList.FirstOrDefault((ISecureTokenService sts) => authority.Equals(sts.AuthorityId, StringComparison.OrdinalIgnoreCase));
				if (secureTokenService == null)
				{
					List<string> list = new List<string>();
					list.Add("authorizationUri");
					list.Add(authorizationUri);
					int num = 0;
					foreach (ISecureTokenService secureTokenService2 in allowedList)
					{
						list.Add("allowedList[" + num.ToString(CultureInfo.InvariantCulture) + "]");
						list.Add(secureTokenService2.AuthorityId);
						num++;
					}
					OAuthServices oauthServices = this.services;
					string text2 = "OAuth/AadOAuthProvider/GetServiceForBearerUri/NoMatchingStsFound";
					TraceEventType traceEventType2 = TraceEventType.Error;
					object[] array = list.ToArray();
					oauthServices.Write(text2, traceEventType2, array);
					return null;
				}
				return SecureTokenService.CreateTenant(secureTokenService, this.GetTenantFromBearer(authorizationUri));
			}

			// Token: 0x06000163 RID: 355 RVA: 0x00006E24 File Offset: 0x00005024
			public bool TryGetAuthenticationEndpoint(string resourceUrl, IEnumerable<KeyValuePair<string, string>> headers, out string authUri, out string error)
			{
				authUri = null;
				string text;
				if (this.TryGetAuthenticationHeader(resourceUrl, headers, out text, out error, true) && !this.TryGetAuthorizationUrl(text, out authUri, out error))
				{
					Uri uri;
					if (this.isSharePoint)
					{
						uri = new UriBuilder(resourceUrl)
						{
							Path = "_api/$metadata"
						}.Uri;
					}
					else
					{
						uri = new Uri(new Uri(resourceUrl.EndsWith("/", StringComparison.OrdinalIgnoreCase) ? resourceUrl : (resourceUrl + "/")), "web");
						uri = Utilities.AddQueryParametersToUri(uri, AadOAuthProvider.CrmGetOAuthUriQuery);
					}
					return this.TryGetAuthenticationHeader(uri.AbsoluteUri, headers, out text, out error, true) && this.TryGetAuthorizationUrl(text, out authUri, out error);
				}
				return error == null;
			}

			// Token: 0x06000164 RID: 356 RVA: 0x00006ED4 File Offset: 0x000050D4
			private string GetTenantFromBearer(string bearerAuthorizationUri)
			{
				if (!Uri.IsWellFormedUriString(bearerAuthorizationUri, UriKind.Absolute))
				{
					return null;
				}
				string[] array = new Uri(bearerAuthorizationUri).PathAndQuery.Split(new char[] { '/' });
				if (array.Length > 1 && !string.IsNullOrEmpty(array[1]))
				{
					return array[1];
				}
				return null;
			}

			// Token: 0x06000165 RID: 357 RVA: 0x00006F20 File Offset: 0x00005120
			private bool TryGetAuthenticationHeader(string resourceUrl, IEnumerable<KeyValuePair<string, string>> headers, out string authHeader, out string error, bool setAuthHeader = true)
			{
				authHeader = null;
				error = null;
				Uri uri = new Uri(resourceUrl);
				WebRequest webRequest = this.services.CreateRequest(uri);
				if (setAuthHeader)
				{
					webRequest.Headers[HttpRequestHeader.Authorization] = "Bearer";
				}
				if (headers != null)
				{
					foreach (KeyValuePair<string, string> keyValuePair in headers)
					{
						if (keyValuePair.Value == null)
						{
							webRequest.Headers.Remove(keyValuePair.Key);
						}
						else
						{
							webRequest.Headers[keyValuePair.Key] = keyValuePair.Value;
						}
					}
				}
				WebResponse webResponse = null;
				try
				{
					webResponse = webRequest.GetResponse();
				}
				catch (WebException ex)
				{
					this.services.Write("OAuth/AadOAuthProvider/TryGetAuthenticationHeader", ex);
					if (ex.Status != WebExceptionStatus.ProtocolError || ex.Response == null)
					{
						error = ex.Message;
						return false;
					}
					webResponse = ex.Response;
				}
				using (webResponse)
				{
					if (webResponse.Headers.AllKeys.Contains("WWW-Authenticate", StringComparer.OrdinalIgnoreCase))
					{
						authHeader = webResponse.Headers["WWW-Authenticate"];
						this.isSharePoint = this.IsSharePoint(webResponse);
					}
					else
					{
						if (setAuthHeader)
						{
							return this.TryGetAuthenticationHeader(resourceUrl, headers, out authHeader, out error, false);
						}
						error = OAuthStrings.MS_Online_ID_Not_Supported;
						return false;
					}
				}
				webRequest.Abort();
				return true;
			}

			// Token: 0x06000166 RID: 358 RVA: 0x000070B4 File Offset: 0x000052B4
			private bool IsSharePoint(WebResponse response)
			{
				return response.Headers["MicrosoftSharePointTeamServices"] != null;
			}

			// Token: 0x06000167 RID: 359 RVA: 0x000070CC File Offset: 0x000052CC
			private bool TryGetAuthorizationUrl(string authHeader, out string authUri, out string error)
			{
				authUri = null;
				error = null;
				Match match = AadOAuthProvider.authorizationUriPattern.Match(authHeader);
				if (match.Success)
				{
					authUri = match.Result("${authUrl}");
					authUri = authUri.Trim(new char[] { '"' });
					if (this.isSharePoint && authUri.Contains("/common/"))
					{
						Match match2 = AadOAuthProvider.realmPattern.Match(authHeader);
						if (match2.Success)
						{
							authUri = authUri.Replace("/common/", match2.Result("/${realm}/"));
						}
					}
					return true;
				}
				this.services.Write("OAuth/AadOAuthProvider/TryGetAuthorizationUrl", TraceEventType.Error, new object[] { "Header", authHeader });
				error = OAuthStrings.OAuthHeader_NoAuthorizationUri(authHeader);
				return false;
			}

			// Token: 0x06000168 RID: 360 RVA: 0x00007188 File Offset: 0x00005388
			private string GetResourceValue(string resourceUrl, IEnumerable<TrustedResource> resourceList)
			{
				Uri uri = new Uri(resourceUrl);
				string uriAuthority = uri.GetLeftPart(UriPartial.Authority);
				if (resourceList == null)
				{
					return uriAuthority;
				}
				TrustedResource trustedResource = resourceList.FirstOrDefault(delegate(TrustedResource resource)
				{
					foreach (string text in resource.Urls)
					{
						string[] array = text.Split(new char[] { '*' });
						if (array.Length == 2 && uriAuthority.StartsWith(array[0], StringComparison.OrdinalIgnoreCase) && uriAuthority.EndsWith(array[1], StringComparison.OrdinalIgnoreCase))
						{
							return true;
						}
						if (uriAuthority.Equals(text, StringComparison.OrdinalIgnoreCase))
						{
							return true;
						}
					}
					return false;
				});
				if (trustedResource == null || AadOAuthProvider.wildCardEnabledAppIds.Contains(trustedResource.ResourceValue))
				{
					return uriAuthority;
				}
				return trustedResource.ResourceValue;
			}

			// Token: 0x04000102 RID: 258
			private readonly OAuthServices services;

			// Token: 0x04000103 RID: 259
			private ISecureTokenService tokenService;

			// Token: 0x04000104 RID: 260
			private bool isSharePoint;
		}
	}
}
