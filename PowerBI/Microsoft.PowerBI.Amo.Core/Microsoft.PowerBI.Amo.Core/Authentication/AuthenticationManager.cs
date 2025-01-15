using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AnalysisServices.Redirection;
using Microsoft.AnalysisServices.Runtime;

namespace Microsoft.AnalysisServices.Authentication
{
	// Token: 0x020000F4 RID: 244
	internal static class AuthenticationManager
	{
		// Token: 0x06000F6E RID: 3950 RVA: 0x00034DF4 File Offset: 0x00032FF4
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

		// Token: 0x06000F6F RID: 3951 RVA: 0x00035074 File Offset: 0x00033274
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

		// Token: 0x06000F70 RID: 3952 RVA: 0x000351C8 File Offset: 0x000333C8
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

		// Token: 0x06000F71 RID: 3953 RVA: 0x00035214 File Offset: 0x00033414
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

		// Token: 0x06000F72 RID: 3954 RVA: 0x00035280 File Offset: 0x00033480
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

		// Token: 0x06000F73 RID: 3955 RVA: 0x0003535C File Offset: 0x0003355C
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

		// Token: 0x06000F74 RID: 3956 RVA: 0x0003544B File Offset: 0x0003364B
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

		// Token: 0x0400083B RID: 2107
		internal const string DefaultAuthenticationRedirectUri = "https://login.microsoftonline.com/common/oauth2/nativeclient";

		// Token: 0x0400083C RID: 2108
		private const string ResourceIdTemplate = "https://{0}";

		// Token: 0x0400083D RID: 2109
		private const string AppIdPrefix = "app:";

		// Token: 0x0400083E RID: 2110
		private const string AppIdTenantDelimeter = "@";

		// Token: 0x0400083F RID: 2111
		private const string CertificateThumbprintPrefix = "cert:";

		// Token: 0x04000840 RID: 2112
		private const string DataverseIdentityProvider = "Dataverse";

		// Token: 0x04000841 RID: 2113
		private const double RefreshTokenInAdvanceTimePercentage = 0.08;

		// Token: 0x04000842 RID: 2114
		private const SecurityProtocolType SecurityProtocolType_SystemDefault = SecurityProtocolType.SystemDefault;

		// Token: 0x04000843 RID: 2115
		private const SecurityProtocolType SecurityProtocolType_Tls11 = SecurityProtocolType.Tls11;

		// Token: 0x04000844 RID: 2116
		private const SecurityProtocolType SecurityProtocolType_Tls12 = SecurityProtocolType.Tls12;

		// Token: 0x04000845 RID: 2117
		private const SecurityProtocolType SecurityProtocolType_Tls13 = SecurityProtocolType.Tls13;

		// Token: 0x04000846 RID: 2118
		private static readonly Version systemDefaultNetFxMinRuntime = new Version(4, 7);

		// Token: 0x04000847 RID: 2119
		private static readonly Version tls13NetFxMinRuntime = new Version(4, 8);
	}
}
