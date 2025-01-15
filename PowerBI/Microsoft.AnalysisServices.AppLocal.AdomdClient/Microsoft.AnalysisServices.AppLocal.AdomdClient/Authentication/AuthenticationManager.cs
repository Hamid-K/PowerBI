using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AnalysisServices.AdomdClient.Redirection;
using Microsoft.AnalysisServices.AdomdClient.Runtime;

namespace Microsoft.AnalysisServices.AdomdClient.Authentication
{
	// Token: 0x020000FF RID: 255
	internal static class AuthenticationManager
	{
		// Token: 0x06000EDE RID: 3806 RVA: 0x00032470 File Offset: 0x00030670
		public static CloudConnectionAuthenticationProperties GetCloudConnectionAuthenticationProperties(string identityProvider, string resource, string tenantId, string userId)
		{
			bool flag;
			string text;
			AuthenticationInformation authenticationInformation = AuthenticationManager.GetAuthenticationInformation(AuthenticationManager.optionsForConnectionProperties, identityProvider, resource, tenantId, userId, out flag, out text);
			CloudConnectionAuthenticationProperties cloudConnectionAuthenticationProperties = new CloudConnectionAuthenticationProperties
			{
				Authority = authenticationInformation.Authority,
				ResourceId = authenticationInformation.ResourceId,
				IsCommonTenant = authenticationInformation.IsCommonTenant
			};
			if (!authenticationInformation.IsCommonTenant)
			{
				cloudConnectionAuthenticationProperties.ManagedTenantNameOrId = tenantId;
			}
			return cloudConnectionAuthenticationProperties;
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x000324CC File Offset: 0x000306CC
		public static AuthenticationHandle Authenticate(AuthenticationOptions options, string identityProvider, string resource, string tenantId, string userId, string password, string workspaceName, string databaseName, bool isForAsAzureRedirection, out AuthenticationInformation authInfo)
		{
			bool flag = AuthenticationInformation.IsDataverseIdentityProvider(identityProvider);
			AuthenticationTracer.TraceInformation("Authentication manager is aquiring a token - options=[Endpoint={0}, UseTokenCache={1}, SsoMode={2}], provider='{3}', resource='{4}', tenantId={5}, userId='{6}', password={7}, isForAsAzureRedirection={8}", new object[]
			{
				options.Endpoint,
				options.UseTokenCache,
				options.SsoMode,
				identityProvider ?? "<null>",
				resource ?? "<null>",
				tenantId ?? string.Empty,
				userId ?? string.Empty,
				string.IsNullOrEmpty(password) ? string.Empty : "********",
				isForAsAzureRedirection
			});
			IAuthenticationService authenticationService = AuthenticationManager.GetAuthenticationService(options);
			bool flag2;
			string text;
			authInfo = AuthenticationManager.GetAuthenticationInformation(options, identityProvider, resource, tenantId, userId, out flag2, out text);
			if (isForAsAzureRedirection)
			{
				RedirectionInformation redirectionInformation;
				if (!RedirectionInformation.TryGetRedirectionInfo(new Uri(resource), out redirectionInformation))
				{
					throw new InvalidOperationException(AuthenticationSR.Exception_RedirectionInfoNotFound);
				}
				authInfo.ResourceId = redirectionInformation.PbiResourceId;
			}
			if (flag2)
			{
				if (password == null)
				{
					throw new ArgumentException(AuthenticationSR.Exception_SpnAuthenticationWithoutPassword, "password");
				}
				if (authInfo.IsCommonTenant)
				{
					throw new ArgumentException(AuthenticationSR.Exception_SpnAuthenticationWithoutTenant, "tenantId\\userId");
				}
				AuthenticationTracer.TraceInformation("Authentication manager is aquiring a service-principal token - authInfo=[endpoint={0}, authority='{1}', applicationId={2}, resourceId={3}], applicationId={4}", new object[] { authInfo.Endpoint, authInfo.Authority, authInfo.ApplicationId, authInfo.ResourceId, text });
				X509Certificate2 x509Certificate;
				if (AuthenticationManager.TryLoadCertificateByThumbprint(password, out x509Certificate))
				{
					return authenticationService.AuthenticateServicePrincipal(options, authInfo, text, x509Certificate);
				}
				return authenticationService.AuthenticateServicePrincipal(options, authInfo, text, password);
			}
			else
			{
				if (options.HasServicePrincipalProfile)
				{
					throw new ArgumentException(AuthenticationSR.Exception_UserAuthenticationWithServicePrincipalProfile);
				}
				AuthenticationTracer.TraceInformation("Authentication manager is acquiring a user token - authInfo=[endpoint={0}, authority='{1}', applicationId={2}, resourceId={3}], userId='{4}', password={5}", new object[]
				{
					authInfo.Endpoint,
					authInfo.Authority,
					authInfo.ApplicationId,
					authInfo.ResourceId,
					userId ?? string.Empty,
					string.IsNullOrEmpty(password) ? string.Empty : "********"
				});
				AuthenticationHandle authenticationHandle;
				if (userId != null && password != null)
				{
					try
					{
						authenticationHandle = authenticationService.AuthenticateUser(options, authInfo, userId, password);
						goto IL_0244;
					}
					catch (MFARequiredException)
					{
						authenticationHandle = authenticationService.AuthenticateUser(options, authInfo, userId);
						goto IL_0244;
					}
				}
				if (userId != null)
				{
					authenticationHandle = authenticationService.AuthenticateUser(options, authInfo, userId);
				}
				else
				{
					authenticationHandle = authenticationService.AuthenticateUser(options, authInfo);
				}
				IL_0244:
				if (flag)
				{
					return new DataverseAuthenticationHandle(authenticationHandle, new Uri(resource).Authority, workspaceName, databaseName);
				}
				return authenticationHandle;
			}
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x0003274C File Offset: 0x0003094C
		public static void EnableStrongSecurityProtocols()
		{
			bool isNetCoreDomain = FrameworkRuntimeHelper.IsNetCoreDomain;
			Version version;
			if (!FrameworkRuntimeHelper.TryGetRuntimeVersion(out version))
			{
				version = new Version(0, 0);
			}
			if (!isNetCoreDomain && version >= AuthenticationManager.systemDefaultNetFxMinRuntime && ServicePointManager.SecurityProtocol == SecurityProtocolType.SystemDefault)
			{
				return;
			}
			if ((ServicePointManager.SecurityProtocol & SecurityProtocolType.Ssl3) == SecurityProtocolType.Ssl3)
			{
				ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Ssl3;
			}
			if ((ServicePointManager.SecurityProtocol & SecurityProtocolType.Tls13) == SecurityProtocolType.Tls13)
			{
				return;
			}
			if ((ServicePointManager.SecurityProtocol & SecurityProtocolType.Tls12) == SecurityProtocolType.Tls12)
			{
				if (!isNetCoreDomain && version >= AuthenticationManager.tls13NetFxMinRuntime)
				{
					ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls13;
				}
				return;
			}
			if ((ServicePointManager.SecurityProtocol & SecurityProtocolType.Tls11) == SecurityProtocolType.Tls11)
			{
				if (!isNetCoreDomain && version >= AuthenticationManager.tls13NetFxMinRuntime)
				{
					ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;
					return;
				}
				ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
				return;
			}
			else if ((ServicePointManager.SecurityProtocol & SecurityProtocolType.Tls) == SecurityProtocolType.Tls)
			{
				if (!isNetCoreDomain && version >= AuthenticationManager.tls13NetFxMinRuntime)
				{
					ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;
					return;
				}
				ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
				return;
			}
			else
			{
				if (!isNetCoreDomain && version >= AuthenticationManager.tls13NetFxMinRuntime)
				{
					ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;
					return;
				}
				ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
				return;
			}
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x000328A0 File Offset: 0x00030AA0
		internal static DateTimeOffset CalculateAccessTokenRefreshBy(DateTimeOffset expiresOn)
		{
			double totalSeconds = expiresOn.Subtract(DateTimeOffset.Now).TotalSeconds;
			if (totalSeconds > 0.0)
			{
				double num = totalSeconds * 0.08;
				return expiresOn.Subtract(TimeSpan.FromSeconds(num));
			}
			return expiresOn;
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x000328EC File Offset: 0x00030AEC
		private static IAuthenticationService GetAuthenticationService(AuthenticationOptions options)
		{
			AuthenticationTracer.TraceInformation("Obtaining the authentication service - endpoint={0}", new object[] { options.Endpoint });
			AuthenticationEndpoint endpoint = options.Endpoint;
			if (endpoint == AuthenticationEndpoint.AadV1)
			{
				throw new InvalidOperationException(AuthenticationSR.Exception_AdalIsNoLongerSupported);
			}
			if (endpoint != AuthenticationEndpoint.AadV2)
			{
				throw new InvalidOperationException(AuthenticationSR.Exception_InvalidEndpointType(options.Endpoint.ToString()));
			}
			return MsalAuthenticationHandle.Service;
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x00032958 File Offset: 0x00030B58
		private static AuthenticationInformation GetAuthenticationInformation(AuthenticationOptions options, string identityProvider, string resource, string tenantId, string userId, out bool isServicePrincipal, out string applicationId)
		{
			AuthenticationTracer.TraceInformation("Authentication manager is obtaining authentication information - endpoint={0}, provider='{1}', resource='{2}', tenantId={3}, userId='{4}'", new object[]
			{
				options.Endpoint,
				identityProvider ?? "<null>",
				resource ?? "<null>",
				tenantId ?? string.Empty,
				userId ?? string.Empty
			});
			Uri uri = new Uri(resource);
			AuthenticationInformation authenticationInformation = AuthenticationInformation.FindMatchingAuthenticationInformation(options.Endpoint, identityProvider, uri, options.BypassAuthInfoValidation);
			if (!string.IsNullOrEmpty(authenticationInformation.ResourceId))
			{
				authenticationInformation.ResourceId = new Uri(authenticationInformation.ResourceId).AbsoluteUri;
			}
			else
			{
				authenticationInformation.ResourceId = string.Format("https://{0}", uri.Host);
			}
			isServicePrincipal = AuthenticationManager.IsServicePrincipalAppID(userId, ref tenantId, out applicationId);
			if (authenticationInformation.IsCommonTenant && !string.IsNullOrEmpty(tenantId))
			{
				authenticationInformation.ReplaceCommonTenant(tenantId);
			}
			return authenticationInformation;
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x00032A34 File Offset: 0x00030C34
		private static bool IsServicePrincipalAppID(string id, ref string tenantId, out string applicationId)
		{
			if (string.IsNullOrEmpty(id) || !id.StartsWith("app:", StringComparison.Ordinal))
			{
				applicationId = string.Empty;
				return false;
			}
			int num = id.IndexOf("@", "app:".Length, StringComparison.Ordinal);
			if (num == -1)
			{
				Guid guid;
				if (!Guid.TryParse(id.Substring("app:".Length), out guid))
				{
					throw new ArgumentException(AuthenticationSR.Exception_InvalidServiceAppId(id), "id");
				}
				applicationId = guid.ToString("D");
			}
			else
			{
				Guid guid2;
				Guid guid3;
				if (!Guid.TryParse(id.Substring("app:".Length, num - "app:".Length), out guid2) || !Guid.TryParse(id.Substring(num + "@".Length), out guid3))
				{
					throw new ArgumentException(AuthenticationSR.Exception_InvalidServiceAppId(id), "id");
				}
				applicationId = guid2.ToString("D");
				tenantId = guid3.ToString("D");
			}
			return true;
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x00032B23 File Offset: 0x00030D23
		private static bool TryLoadCertificateByThumbprint(string thumbprint, out X509Certificate2 certificate)
		{
			if (!thumbprint.StartsWith("cert:", StringComparison.OrdinalIgnoreCase))
			{
				certificate = null;
				return false;
			}
			if (!CertUtils.TryLoadCertificateByThumbprint(thumbprint.Substring("cert:".Length), out certificate))
			{
				throw new ArgumentException(AuthenticationSR.Exception_InvalidCertificateThumbprint(thumbprint), "thumbprint");
			}
			return true;
		}

		// Token: 0x04000881 RID: 2177
		internal const string DefaultAuthenticationRedirectUri = "https://login.microsoftonline.com/common/oauth2/nativeclient";

		// Token: 0x04000882 RID: 2178
		private const string ResourceIdTemplate = "https://{0}";

		// Token: 0x04000883 RID: 2179
		private const string AppIdPrefix = "app:";

		// Token: 0x04000884 RID: 2180
		private const string AppIdTenantDelimeter = "@";

		// Token: 0x04000885 RID: 2181
		private const string CertificateThumbprintPrefix = "cert:";

		// Token: 0x04000886 RID: 2182
		private const string DataverseIdentityProvider = "Dataverse";

		// Token: 0x04000887 RID: 2183
		private const double RefreshTokenInAdvanceTimePercentage = 0.08;

		// Token: 0x04000888 RID: 2184
		private const SecurityProtocolType SecurityProtocolType_SystemDefault = SecurityProtocolType.SystemDefault;

		// Token: 0x04000889 RID: 2185
		private const SecurityProtocolType SecurityProtocolType_Tls11 = SecurityProtocolType.Tls11;

		// Token: 0x0400088A RID: 2186
		private const SecurityProtocolType SecurityProtocolType_Tls12 = SecurityProtocolType.Tls12;

		// Token: 0x0400088B RID: 2187
		private const SecurityProtocolType SecurityProtocolType_Tls13 = SecurityProtocolType.Tls13;

		// Token: 0x0400088C RID: 2188
		private static readonly AuthenticationOptions optionsForConnectionProperties = new AuthenticationOptions(null);

		// Token: 0x0400088D RID: 2189
		private static readonly Version systemDefaultNetFxMinRuntime = new Version(4, 7);

		// Token: 0x0400088E RID: 2190
		private static readonly Version tls13NetFxMinRuntime = new Version(4, 8);
	}
}
