using System;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AnalysisServices.AzureClient.Redirection;
using Microsoft.AnalysisServices.AzureClient.Runtime;
using Microsoft.AnalysisServices.AzureClient.Utilities;
using Microsoft.Identity.Client;

namespace Microsoft.AnalysisServices.AzureClient.Authentication
{
	// Token: 0x02000019 RID: 25
	internal static class AuthenticationManager
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x00003480 File Offset: 0x00001680
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

		// Token: 0x060000A4 RID: 164 RVA: 0x00003700 File Offset: 0x00001900
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

		// Token: 0x060000A5 RID: 165 RVA: 0x00003854 File Offset: 0x00001A54
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

		// Token: 0x060000A6 RID: 166 RVA: 0x000038A0 File Offset: 0x00001AA0
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
			AuthenticationManager.EnsureMsalIsAvaliable();
			return MsalAuthenticationHandle.Service;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003910 File Offset: 0x00001B10
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

		// Token: 0x060000A8 RID: 168 RVA: 0x000039EC File Offset: 0x00001BEC
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

		// Token: 0x060000A9 RID: 169 RVA: 0x00003ADB File Offset: 0x00001CDB
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

		// Token: 0x060000AA RID: 170 RVA: 0x00003B1A File Offset: 0x00001D1A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void EnsureMsalIsAvaliable()
		{
			if (AuthenticationManager.msalAppBaseType == null)
			{
				AuthenticationManager.EnsureMsalIsAvaliableImpl();
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003B30 File Offset: 0x00001D30
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void EnsureMsalIsAvaliableImpl()
		{
			using (GlobalContext.CreateGlobalLockScope("MICROSOFT_ANALYSISSERVICES_XMLA_LIBS_MSAL_LOADING_LOCK"))
			{
				if (!(AuthenticationManager.msalAppBaseType != null))
				{
					if (!GlobalContext.TryGetGlobalObject<Type>("MICROSOFT_ANALYSISSERVICES_XMLA_LIBS_MSAL_APP_BASE_TYPE_AZ_CLIENT", out AuthenticationManager.msalAppBaseType))
					{
						string identityModelAbstractionsAssemblyToLoad = null;
						ResolveEventHandler resolveEventHandler = delegate(object _, ResolveEventArgs reArgs)
						{
							string text = reArgs.Name.Trim();
							string text2;
							Version version;
							string text3;
							string text4;
							FrameworkRuntimeHelper.ParseAssemblyName(text, out text2, out version, out text3, out text4);
							if (string.Compare(text2, "Microsoft.Identity.Client", StringComparison.Ordinal) == 0)
							{
								Assembly assembly;
								if (GlobalContext.TryGetGlobalObject<Assembly>("MICROSOFT_ANALYSISSERVICES_XMLA_LIBS_MSAL_RES_BASED_ASSEMBLY", out assembly))
								{
									AuthenticationManager.CheckIfMsalAssemblyRequiresIdentityModelAbstractionsLoad(assembly, out identityModelAbstractionsAssemblyToLoad);
									return assembly;
								}
								if (!FrameworkRuntimeHelper.TryLoadEmbeddedAssembly(Assembly.GetExecutingAssembly(), string.Format("{0}.dll", "Microsoft.Identity.Client"), out assembly))
								{
									return null;
								}
								if (!FrameworkRuntimeHelper.IsReferencedAssembly(assembly.GetName(), text))
								{
									return null;
								}
								AuthenticationManager.CheckIfMsalAssemblyRequiresIdentityModelAbstractionsLoad(assembly, out identityModelAbstractionsAssemblyToLoad);
								GlobalContext.SetGlobalObject("MICROSOFT_ANALYSISSERVICES_XMLA_LIBS_MSAL_RES_BASED_ASSEMBLY", assembly);
								return assembly;
							}
							else
							{
								if (string.Compare(text2, "Microsoft.IdentityModel.Abstractions", StringComparison.Ordinal) != 0)
								{
									return null;
								}
								Assembly assembly2;
								if (GlobalContext.TryGetGlobalObject<Assembly>("MICROSOFT_ANALYSISSERVICES_XMLA_LIBS_ABSTRUCTIONS_RES_BASED_ASSEMBLY", out assembly2))
								{
									return assembly2;
								}
								if (!FrameworkRuntimeHelper.TryLoadEmbeddedAssembly(Assembly.GetExecutingAssembly(), string.Format("{0}.dll", "Microsoft.IdentityModel.Abstractions"), out assembly2))
								{
									return null;
								}
								if (!FrameworkRuntimeHelper.IsReferencedAssembly(assembly2.GetName(), text))
								{
									return null;
								}
								GlobalContext.SetGlobalObject("MICROSOFT_ANALYSISSERVICES_XMLA_LIBS_ABSTRUCTIONS_RES_BASED_ASSEMBLY", assembly2);
								return assembly2;
							}
						};
						AppDomain.CurrentDomain.AssemblyResolve += resolveEventHandler;
						try
						{
							AuthenticationManager.TriggerMsalAssemblyLoad();
							if (!string.IsNullOrEmpty(identityModelAbstractionsAssemblyToLoad))
							{
								Assembly.Load(identityModelAbstractionsAssemblyToLoad);
							}
							GlobalContext.SetGlobalObject("MICROSOFT_ANALYSISSERVICES_XMLA_LIBS_MSAL_APP_BASE_TYPE_AZ_CLIENT", AuthenticationManager.msalAppBaseType);
						}
						catch (Exception ex)
						{
							throw new ASAzureAdalWrapperException(AuthenticationSR.Exception_AssemblyMissingInEmbeddedResources("Microsoft.Identity.Client"), ex);
						}
						finally
						{
							AppDomain.CurrentDomain.AssemblyResolve -= resolveEventHandler;
						}
					}
				}
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003C14 File Offset: 0x00001E14
		private static bool CheckIfMsalAssemblyRequiresIdentityModelAbstractionsLoad(Assembly msalAssembly, out string identityModelAbstractionsAssemblyName)
		{
			foreach (AssemblyName assemblyName in msalAssembly.GetReferencedAssemblies())
			{
				if (string.Compare(assemblyName.Name, "Microsoft.IdentityModel.Abstractions", StringComparison.Ordinal) == 0)
				{
					identityModelAbstractionsAssemblyName = assemblyName.FullName;
					return true;
				}
			}
			identityModelAbstractionsAssemblyName = null;
			return false;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003C5C File Offset: 0x00001E5C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void TriggerMsalAssemblyLoad()
		{
			AuthenticationManager.msalAppBaseType = typeof(IClientApplicationBase);
			AuthenticationTracer.TraceInformation("MSAL assembly is available - Assembly='{0}', Location='{1}'", new object[]
			{
				AuthenticationManager.msalAppBaseType.Assembly.FullName,
				AuthenticationManager.msalAppBaseType.Assembly.Location
			});
		}

		// Token: 0x04000053 RID: 83
		internal const string DefaultAuthenticationRedirectUri = "https://login.microsoftonline.com/common/oauth2/nativeclient";

		// Token: 0x04000054 RID: 84
		private const string ResourceIdTemplate = "https://{0}";

		// Token: 0x04000055 RID: 85
		private const string AppIdPrefix = "app:";

		// Token: 0x04000056 RID: 86
		private const string AppIdTenantDelimeter = "@";

		// Token: 0x04000057 RID: 87
		private const string CertificateThumbprintPrefix = "cert:";

		// Token: 0x04000058 RID: 88
		private const string DataverseIdentityProvider = "Dataverse";

		// Token: 0x04000059 RID: 89
		private const double RefreshTokenInAdvanceTimePercentage = 0.08;

		// Token: 0x0400005A RID: 90
		private const SecurityProtocolType SecurityProtocolType_SystemDefault = SecurityProtocolType.SystemDefault;

		// Token: 0x0400005B RID: 91
		private const SecurityProtocolType SecurityProtocolType_Tls11 = SecurityProtocolType.Tls11;

		// Token: 0x0400005C RID: 92
		private const SecurityProtocolType SecurityProtocolType_Tls12 = SecurityProtocolType.Tls12;

		// Token: 0x0400005D RID: 93
		private const SecurityProtocolType SecurityProtocolType_Tls13 = SecurityProtocolType.Tls13;

		// Token: 0x0400005E RID: 94
		private const string AppDomainKey_MsalLoadingLock = "MICROSOFT_ANALYSISSERVICES_XMLA_LIBS_MSAL_LOADING_LOCK";

		// Token: 0x0400005F RID: 95
		private const string AppDomainKey_MsalAppBaseType = "MICROSOFT_ANALYSISSERVICES_XMLA_LIBS_MSAL_APP_BASE_TYPE_AZ_CLIENT";

		// Token: 0x04000060 RID: 96
		private const string AppDomainKey_MsalResourceBasedAssembly = "MICROSOFT_ANALYSISSERVICES_XMLA_LIBS_MSAL_RES_BASED_ASSEMBLY";

		// Token: 0x04000061 RID: 97
		private const string AppDomainKey_IdentityModelAbstractionsResourceBasedAssembly = "MICROSOFT_ANALYSISSERVICES_XMLA_LIBS_ABSTRUCTIONS_RES_BASED_ASSEMBLY";

		// Token: 0x04000062 RID: 98
		private const string MsalAssemblyName = "Microsoft.Identity.Client";

		// Token: 0x04000063 RID: 99
		private const string IdentityModelAbstractionsAssemblyName = "Microsoft.IdentityModel.Abstractions";

		// Token: 0x04000064 RID: 100
		private static readonly Version systemDefaultNetFxMinRuntime = new Version(4, 7);

		// Token: 0x04000065 RID: 101
		private static readonly Version tls13NetFxMinRuntime = new Version(4, 8);

		// Token: 0x04000066 RID: 102
		private static Type msalAppBaseType;
	}
}
