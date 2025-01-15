using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AnalysisServices.AdomdClient.Hosting;
using Microsoft.AnalysisServices.AdomdClient.Utilities;
using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Broker;

namespace Microsoft.AnalysisServices.AdomdClient.Authentication
{
	// Token: 0x02000106 RID: 262
	internal abstract class MsalAuthenticationHandle : AuthenticationHandle
	{
		// Token: 0x06000F0C RID: 3852 RVA: 0x00033044 File Offset: 0x00031244
		protected MsalAuthenticationHandle(IClientApplicationBase app, IEnumerable<string> scopes, AuthenticationResult result)
			: base(AuthenticationEndpoint.AadV2, (result.Account != null) ? result.Account.Environment : null, (result.Account != null && result.Account.HomeAccountId != null) ? result.Account.HomeAccountId.TenantId : result.TenantId)
		{
			this.app = app;
			this.scopes = scopes;
			this.accessToken = result.AccessToken;
			this.refreshBy = AuthenticationManager.CalculateAccessTokenRefreshBy(result.ExpiresOn);
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06000F0D RID: 3853 RVA: 0x000330C6 File Offset: 0x000312C6
		public override string AuthenticationScheme
		{
			get
			{
				return "Bearer";
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06000F0E RID: 3854 RVA: 0x000330CD File Offset: 0x000312CD
		internal static IAuthenticationService Service
		{
			get
			{
				return MsalAuthenticationHandle.service;
			}
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x000330D4 File Offset: 0x000312D4
		public sealed override string GetAccessToken()
		{
			if (this.refreshBy < DateTimeOffset.Now)
			{
				int num = AuthenticationTracer.StartScopeImpl("MsalAuthenticationHandle.GetAccessToken");
				try
				{
					AuthenticationResult authenticationResult = this.RefreshAccessToken(this.app, this.scopes);
					this.accessToken = authenticationResult.AccessToken;
					this.refreshBy = AuthenticationManager.CalculateAccessTokenRefreshBy(authenticationResult.ExpiresOn);
				}
				catch (Exception ex)
				{
					AuthenticationTracer.TraceError("MsalAuthenticationHandle.GetAccessToken failed; Exception: {0}", new object[] { ex });
					throw;
				}
				finally
				{
					AuthenticationTracer.CompleteScopeImpl("MsalAuthenticationHandle.GetAccessToken", num);
				}
			}
			return this.accessToken;
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x00033178 File Offset: 0x00031378
		public sealed override long GetRefreshByTimeAsFileTime()
		{
			return this.refreshBy.ToFileTime();
		}

		// Token: 0x06000F11 RID: 3857
		protected abstract AuthenticationResult RefreshAccessToken(IClientApplicationBase app, IEnumerable<string> scopes);

		// Token: 0x06000F12 RID: 3858 RVA: 0x00033188 File Offset: 0x00031388
		private static bool CheckIfWamBasedAuthenticationSupported()
		{
			Version version;
			bool flag;
			WindowsRuntimeHelper.GetOperatingSystemVersion(out version, out flag);
			return version.Major >= 10 && (version.Minor != 10 || version.Minor != 0 || version.Build >= 17763 || flag);
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x000331CF File Offset: 0x000313CF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static string GetApplicationDisplayInfo(IClientApplicationBase app)
		{
			return string.Format("[Authority='{0}', ClientId={1}, HashCode={2}]", app.Authority, app.AppConfig.ClientId, app.GetHashCode());
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x000331F8 File Offset: 0x000313F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static string GetAccountDisplayInfo(IAccount account)
		{
			if (account == null)
			{
				return string.Empty;
			}
			return string.Format("[Username='{0}', Environment='{1}', HomeAccountId={2}]", account.Username, account.Environment, (account.HomeAccountId != null) ? string.Format("[Identifier={0}, ObjectId={1}, TenantId={2}]", account.HomeAccountId.Identifier, account.HomeAccountId.ObjectId, account.HomeAccountId.TenantId) : string.Empty);
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x00033260 File Offset: 0x00031460
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static string GetScopesDisplayInfo(IEnumerable<string> scopes)
		{
			StringBuilder stringBuilder = new StringBuilder("[");
			foreach (string text in scopes)
			{
				stringBuilder.AppendFormat("'{0}', ", text);
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Remove(stringBuilder.Length - 2, 2).Append(']');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x000332E0 File Offset: 0x000314E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsClientExceptionIndicateInteractiveLoginIsNeeded(MsalClientException e, bool isAcquireTokenSilent)
		{
			if (ClientFeaturesManager.CheckIfUnrestrictedFallbackToInteractiveFlowIsEnabled())
			{
				return true;
			}
			if (isAcquireTokenSilent)
			{
				return e.ErrorCode == "failed_to_refresh_token";
			}
			string errorCode = e.ErrorCode;
			if (errorCode != null)
			{
				int length = errorCode.Length;
				if (length <= 30)
				{
					if (length <= 20)
					{
						if (length != 19)
						{
							if (length != 20)
							{
								return false;
							}
							if (!(errorCode == "interaction_required"))
							{
								return false;
							}
						}
						else if (!(errorCode == "uap_cannot_find_upn"))
						{
							return false;
						}
					}
					else if (length != 26)
					{
						if (length != 30)
						{
							return false;
						}
						if (!(errorCode == "user_information_access_failed"))
						{
							return false;
						}
					}
					else
					{
						char c = errorCode[0];
						if (c != 'r')
						{
							if (c != 'w')
							{
								return false;
							}
							if (!(errorCode == "wstrust_endpoint_not_found"))
							{
								return false;
							}
						}
						else if (!(errorCode == "ropc_not_supported_for_msa"))
						{
							return false;
						}
					}
				}
				else if (length <= 35)
				{
					if (length != 31)
					{
						if (length != 35)
						{
							return false;
						}
						if (!(errorCode == "parsing_ws_metadata_exchange_failed"))
						{
							return false;
						}
					}
					else if (!(errorCode == "parsing_wstrust_response_failed"))
					{
						return false;
					}
				}
				else if (length != 40)
				{
					if (length != 50)
					{
						return false;
					}
					if (!(errorCode == "integrated_windows_auth_not_supported_managed_user"))
					{
						return false;
					}
				}
				else if (!(errorCode == "integrated_windows_authentication_failed"))
				{
					return false;
				}
				return true;
			}
			return false;
		}

		// Token: 0x040008A8 RID: 2216
		private static readonly bool isWamBasedAuthenticationSupported = MsalAuthenticationHandle.CheckIfWamBasedAuthenticationSupported();

		// Token: 0x040008A9 RID: 2217
		private static readonly MsalAuthenticationHandle.MsalAuthenticationService service = new MsalAuthenticationHandle.MsalAuthenticationService();

		// Token: 0x040008AA RID: 2218
		private IClientApplicationBase app;

		// Token: 0x040008AB RID: 2219
		private IEnumerable<string> scopes;

		// Token: 0x040008AC RID: 2220
		private string accessToken;

		// Token: 0x040008AD RID: 2221
		private DateTimeOffset refreshBy;

		// Token: 0x020001D8 RID: 472
		private sealed class MsalAuthenticationService : IAuthenticationService
		{
			// Token: 0x06001400 RID: 5120 RVA: 0x000451E0 File Offset: 0x000433E0
			public AuthenticationHandle AuthenticateUser(AuthenticationOptions options, AuthenticationInformation authInfo, string userId, string password)
			{
				bool flag;
				IPublicClientApplication publicClientApplication;
				IEnumerable<string> enumerable;
				MsalAuthenticationHandle.MsalAuthenticationService.GetPublicClientApplicationAndScopes(options, authInfo, out flag, out publicClientApplication, out enumerable);
				return new MsalAuthenticationHandle.UsernamePasswordAuthenticationHandle(publicClientApplication, enumerable, MsalAuthenticationHandle.UsernamePasswordAuthenticationHandle.AcquireToken(publicClientApplication, enumerable, userId, password), password);
			}

			// Token: 0x06001401 RID: 5121 RVA: 0x00045210 File Offset: 0x00043410
			public AuthenticationHandle AuthenticateUser(AuthenticationOptions options, AuthenticationInformation authInfo, string userId)
			{
				bool flag;
				IPublicClientApplication publicClientApplication;
				IEnumerable<string> enumerable;
				MsalAuthenticationHandle.MsalAuthenticationService.GetPublicClientApplicationAndScopes(options, authInfo, out flag, out publicClientApplication, out enumerable);
				return new MsalAuthenticationHandle.InteractiveAuthenticationHandle(publicClientApplication, enumerable, MsalAuthenticationHandle.InteractiveAuthenticationHandle.AcquireToken(flag, publicClientApplication, enumerable, options.SsoMode, userId));
			}

			// Token: 0x06001402 RID: 5122 RVA: 0x00045240 File Offset: 0x00043440
			public AuthenticationHandle AuthenticateUser(AuthenticationOptions options, AuthenticationInformation authInfo)
			{
				bool flag;
				IPublicClientApplication publicClientApplication;
				IEnumerable<string> enumerable;
				MsalAuthenticationHandle.MsalAuthenticationService.GetPublicClientApplicationAndScopes(options, authInfo, out flag, out publicClientApplication, out enumerable);
				return new MsalAuthenticationHandle.InteractiveAuthenticationHandle(publicClientApplication, enumerable, MsalAuthenticationHandle.InteractiveAuthenticationHandle.AcquireToken(flag, publicClientApplication, enumerable, options.SsoMode, null));
			}

			// Token: 0x06001403 RID: 5123 RVA: 0x00045270 File Offset: 0x00043470
			public AuthenticationHandle AuthenticateServicePrincipal(AuthenticationOptions options, AuthenticationInformation authInfo, string applicationId, X509Certificate2 certificate)
			{
				IConfidentialClientApplication confidentialClientApplication;
				IEnumerable<string> enumerable;
				MsalAuthenticationHandle.MsalAuthenticationService.GetConfidentialClientApplicationAndScopes(options, authInfo, applicationId, certificate, null, out confidentialClientApplication, out enumerable);
				return MsalAuthenticationHandle.ServicePrincipalAuthenticationHandle.Create(confidentialClientApplication, enumerable);
			}

			// Token: 0x06001404 RID: 5124 RVA: 0x00045294 File Offset: 0x00043494
			public AuthenticationHandle AuthenticateServicePrincipal(AuthenticationOptions options, AuthenticationInformation authInfo, string applicationId, string secret)
			{
				IConfidentialClientApplication confidentialClientApplication;
				IEnumerable<string> enumerable;
				MsalAuthenticationHandle.MsalAuthenticationService.GetConfidentialClientApplicationAndScopes(options, authInfo, applicationId, null, secret, out confidentialClientApplication, out enumerable);
				return MsalAuthenticationHandle.ServicePrincipalAuthenticationHandle.Create(confidentialClientApplication, enumerable);
			}

			// Token: 0x06001405 RID: 5125 RVA: 0x000452B8 File Offset: 0x000434B8
			private static void GetPublicClientApplicationAndScopes(AuthenticationOptions options, AuthenticationInformation authInfo, out bool isWamBasedAuthenticationEnabled, out IPublicClientApplication app, out IEnumerable<string> scopes)
			{
				isWamBasedAuthenticationEnabled = MsalAuthenticationHandle.isWamBasedAuthenticationSupported && ClientFeaturesManager.CheckIfWamBasedSsoIsEnabled();
				app = MsalAuthenticationHandle.MsalAuthenticationService.CreatePublicClientApplication(authInfo.ApplicationId, authInfo.Authority, isWamBasedAuthenticationEnabled && options.SsoMode > SingleSignOnMode.Disabled);
				if (options.UseTokenCache)
				{
					string tokenCacheKey = authInfo.GetTokenCacheKey();
					MsalAuthenticationHandle.TokenCacheManager.Create(app, tokenCacheKey);
				}
				scopes = MsalAuthenticationHandle.MsalAuthenticationService.GetDefaultScopes(authInfo.ResourceId);
			}

			// Token: 0x06001406 RID: 5126 RVA: 0x00045320 File Offset: 0x00043520
			private static IPublicClientApplication CreatePublicClientApplication(string clientId, string authority, bool enableWamBroker)
			{
				PublicClientApplicationBuilder publicClientApplicationBuilder = PublicClientApplicationBuilder.Create(clientId).WithAuthority(authority, true).WithRedirectUri("https://login.microsoftonline.com/common/oauth2/nativeclient");
				if (AuthenticationTracer.IsTracingEnabled)
				{
					publicClientApplicationBuilder = publicClientApplicationBuilder.WithLogging(MsalAuthenticationHandle.MsalAuthenticationService.onMsalTraceEvent, new LogLevel?(2), new bool?(false), null);
				}
				if (enableWamBroker)
				{
					publicClientApplicationBuilder = MsalAuthenticationHandle.MsalAuthenticationService.EnableWamBroker(publicClientApplicationBuilder);
				}
				return publicClientApplicationBuilder.Build();
			}

			// Token: 0x06001407 RID: 5127 RVA: 0x00045380 File Offset: 0x00043580
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private static PublicClientApplicationBuilder EnableWamBroker(PublicClientApplicationBuilder builder)
			{
				IntPtr mainWindow;
				if (WindowsRuntimeHelper.IsProcessWithUserInterface(out mainWindow) && mainWindow != IntPtr.Zero)
				{
					return BrokerExtension.WithBroker(builder.WithParentActivityOrWindow(() => mainWindow), new BrokerOptions(1));
				}
				return BrokerExtension.WithBroker(builder, new BrokerOptions(1));
			}

			// Token: 0x06001408 RID: 5128 RVA: 0x000453DC File Offset: 0x000435DC
			private static void GetConfidentialClientApplicationAndScopes(AuthenticationOptions options, AuthenticationInformation authInfo, string applicationId, X509Certificate2 certificate, string secret, out IConfidentialClientApplication app, out IEnumerable<string> scopes)
			{
				ConfidentialClientApplicationBuilder confidentialClientApplicationBuilder = ConfidentialClientApplicationBuilder.Create(applicationId).WithAuthority(authInfo.Authority, true).WithRedirectUri("https://login.microsoftonline.com/common/oauth2/nativeclient");
				if (certificate != null)
				{
					confidentialClientApplicationBuilder = confidentialClientApplicationBuilder.WithCertificate(certificate);
				}
				else
				{
					confidentialClientApplicationBuilder = confidentialClientApplicationBuilder.WithClientSecret(secret);
				}
				if (AuthenticationTracer.IsTracingEnabled)
				{
					confidentialClientApplicationBuilder = confidentialClientApplicationBuilder.WithLogging(MsalAuthenticationHandle.MsalAuthenticationService.onMsalTraceEvent, new LogLevel?(2), new bool?(false), null);
				}
				app = confidentialClientApplicationBuilder.Build();
				scopes = MsalAuthenticationHandle.MsalAuthenticationService.GetDefaultScopes(authInfo.ResourceId);
			}

			// Token: 0x06001409 RID: 5129 RVA: 0x0004545B File Offset: 0x0004365B
			private static IEnumerable<string> GetDefaultScopes(string resourceId)
			{
				return new string[] { string.Format("{0}/.default", resourceId) };
			}

			// Token: 0x04000E37 RID: 3639
			private const string MsalTraceMessageFormat = "MSAL: {0}";

			// Token: 0x04000E38 RID: 3640
			private static LogCallback onMsalTraceEvent = delegate(LogLevel level, string message, bool containsPii)
			{
				if (string.IsNullOrEmpty(message))
				{
					return;
				}
				switch (level)
				{
				case 0:
					AuthenticationTracer.TraceError("MSAL: {0}", new object[] { message });
					return;
				case 1:
					AuthenticationTracer.TraceWarning("MSAL: {0}", new object[] { message });
					return;
				case 2:
					AuthenticationTracer.TraceInformation("MSAL: {0}", new object[] { message });
					return;
				default:
					return;
				}
			};
		}

		// Token: 0x020001D9 RID: 473
		private abstract class UserTokenAuthenticationHandle : MsalAuthenticationHandle
		{
			// Token: 0x0600140B RID: 5131 RVA: 0x00045488 File Offset: 0x00043688
			protected UserTokenAuthenticationHandle(IPublicClientApplication app, IEnumerable<string> scopes, AuthenticationResult result)
				: base(app, scopes, result)
			{
				this.username = result.Account.Username ?? string.Empty;
				this.provider = result.Account.Environment ?? string.Empty;
				this.accountId = ((result.Account.HomeAccountId != null) ? (result.Account.HomeAccountId.Identifier ?? string.Empty) : string.Empty);
			}

			// Token: 0x170006F4 RID: 1780
			// (get) Token: 0x0600140C RID: 5132 RVA: 0x00045505 File Offset: 0x00043705
			public override string Principal
			{
				get
				{
					return this.username;
				}
			}

			// Token: 0x0600140D RID: 5133 RVA: 0x00045510 File Offset: 0x00043710
			protected static bool TryAcquireTokenSilent(IPublicClientApplication app, IEnumerable<string> scopes, bool isForRefresh, bool isWamBasedAuthenticationEnabled, string username, string provider, ref IAccount account, out AuthenticationResult result, out Exception error)
			{
				if (account == null)
				{
					List<IAccount> list = new List<IAccount>(app.GetAccountsAsync().WaitForTaskCompletionAndGetResult<IEnumerable<IAccount>>());
					if (!string.IsNullOrEmpty(username))
					{
						account = list.FirstOrDefault((IAccount acct) => string.Compare(acct.Username, username, StringComparison.InvariantCultureIgnoreCase) == 0 && (string.IsNullOrEmpty(provider) || string.Compare(acct.Environment, provider, StringComparison.InvariantCultureIgnoreCase) == 0));
					}
					else if (list.Count == 1)
					{
						account = list[0];
					}
					if ((isForRefresh || string.IsNullOrEmpty(username)) && account == null)
					{
						result = null;
						error = null;
						return false;
					}
				}
				bool flag;
				try
				{
					Task<AuthenticationResult> task;
					if (account != null)
					{
						task = app.AcquireTokenSilent(scopes, account).ExecuteAsync();
					}
					else
					{
						task = app.AcquireTokenSilent(scopes, username).ExecuteAsync();
					}
					result = task.WaitForTaskCompletionAndGetResult<AuthenticationResult>();
					error = null;
					flag = true;
				}
				catch (MsalUiRequiredException ex)
				{
					result = null;
					error = ex;
					flag = false;
				}
				catch (MsalClientException ex2) when (MsalAuthenticationHandle.IsClientExceptionIndicateInteractiveLoginIsNeeded(ex2, true))
				{
					result = null;
					error = ex2;
					flag = false;
				}
				catch (MsalException ex3) when (isWamBasedAuthenticationEnabled && string.IsNullOrEmpty(username) && account == PublicClientApplication.OperatingSystemAccount)
				{
					result = null;
					error = ex3;
					flag = false;
				}
				return flag;
			}

			// Token: 0x0600140E RID: 5134
			protected abstract AuthenticationResult RefreshAccessTokenImpl(IPublicClientApplication app, IEnumerable<string> scopes, IAccount account);

			// Token: 0x0600140F RID: 5135 RVA: 0x0004567C File Offset: 0x0004387C
			protected sealed override AuthenticationResult RefreshAccessToken(IClientApplicationBase app, IEnumerable<string> scopes)
			{
				IPublicClientApplication publicClientApplication = (IPublicClientApplication)app;
				IAccount account = null;
				AuthenticationResult authenticationResult;
				Exception ex;
				if (MsalAuthenticationHandle.UserTokenAuthenticationHandle.TryAcquireTokenSilent(publicClientApplication, scopes, true, false, this.username, this.provider, ref account, out authenticationResult, out ex))
				{
					return authenticationResult;
				}
				if (account == null)
				{
					throw new InvalidOperationException(AuthenticationSR.Exception_CantObtainUserInfoOnTokenRefresh(this.username));
				}
				return this.RefreshAccessTokenImpl(publicClientApplication, scopes, account);
			}

			// Token: 0x06001410 RID: 5136 RVA: 0x000456CE File Offset: 0x000438CE
			internal sealed override void AddUserRelatedProperties(AdomdPropertyCollection properties)
			{
				base.AddUserRelatedProperties(properties);
				properties.Add("AadUsername", this.username);
				properties.Add("AadUserAccountId", this.accountId);
			}

			// Token: 0x04000E39 RID: 3641
			private const string UserProperty_Username = "AadUsername";

			// Token: 0x04000E3A RID: 3642
			private const string UserProperty_AccountId = "AadUserAccountId";

			// Token: 0x04000E3B RID: 3643
			private readonly string username;

			// Token: 0x04000E3C RID: 3644
			private readonly string provider;

			// Token: 0x04000E3D RID: 3645
			private readonly string accountId;
		}

		// Token: 0x020001DA RID: 474
		private sealed class UsernamePasswordAuthenticationHandle : MsalAuthenticationHandle.UserTokenAuthenticationHandle
		{
			// Token: 0x06001411 RID: 5137 RVA: 0x000456FB File Offset: 0x000438FB
			internal UsernamePasswordAuthenticationHandle(IPublicClientApplication app, IEnumerable<string> scopes, AuthenticationResult result, string password)
				: base(app, scopes, result)
			{
				this.password = password;
			}

			// Token: 0x06001412 RID: 5138 RVA: 0x0004570E File Offset: 0x0004390E
			protected override AuthenticationResult RefreshAccessTokenImpl(IPublicClientApplication app, IEnumerable<string> scopes, IAccount account)
			{
				return MsalAuthenticationHandle.UsernamePasswordAuthenticationHandle.AcquireTokenImpl(app, scopes, account.Username, this.password);
			}

			// Token: 0x06001413 RID: 5139 RVA: 0x00045724 File Offset: 0x00043924
			internal static AuthenticationResult AcquireToken(IPublicClientApplication app, IEnumerable<string> scopes, string userId, string password)
			{
				IAccount account = null;
				AuthenticationResult authenticationResult;
				Exception ex;
				if (MsalAuthenticationHandle.UserTokenAuthenticationHandle.TryAcquireTokenSilent(app, scopes, false, false, userId, null, ref account, out authenticationResult, out ex))
				{
					return authenticationResult;
				}
				return MsalAuthenticationHandle.UsernamePasswordAuthenticationHandle.AcquireTokenImpl(app, scopes, userId, password);
			}

			// Token: 0x06001414 RID: 5140 RVA: 0x00045754 File Offset: 0x00043954
			private static AuthenticationResult AcquireTokenImpl(IPublicClientApplication app, IEnumerable<string> scopes, string userId, string password)
			{
				AuthenticationResult authenticationResult;
				try
				{
					authenticationResult = app.AcquireTokenByUsernamePassword(scopes, userId, password).ExecuteAsync().WaitForTaskCompletionAndGetResult<AuthenticationResult>();
				}
				catch (MsalUiRequiredException ex)
				{
					if (ex.Classification == 2 && ex.ErrorCode == "invalid_grant")
					{
						throw new MFARequiredException(ex);
					}
					throw new NonInteractiveLoginException(ex);
				}
				catch (MsalException ex2)
				{
					throw new AuthenticationException(ex2);
				}
				return authenticationResult;
			}

			// Token: 0x04000E3E RID: 3646
			private string password;
		}

		// Token: 0x020001DB RID: 475
		private sealed class InteractiveAuthenticationHandle : MsalAuthenticationHandle.UserTokenAuthenticationHandle
		{
			// Token: 0x06001415 RID: 5141 RVA: 0x000457C4 File Offset: 0x000439C4
			internal InteractiveAuthenticationHandle(IPublicClientApplication app, IEnumerable<string> scopes, AuthenticationResult result)
				: base(app, scopes, result)
			{
			}

			// Token: 0x06001416 RID: 5142 RVA: 0x000457CF File Offset: 0x000439CF
			protected override AuthenticationResult RefreshAccessTokenImpl(IPublicClientApplication app, IEnumerable<string> scopes, IAccount account)
			{
				return MsalAuthenticationHandle.InteractiveAuthenticationHandle.AcquireTokenImpl(app, scopes, null, account);
			}

			// Token: 0x06001417 RID: 5143 RVA: 0x000457DC File Offset: 0x000439DC
			internal static AuthenticationResult AcquireToken(bool isWamBasedAuthenticationEnabled, IPublicClientApplication app, IEnumerable<string> scopes, SingleSignOnMode ssoMode, string userId)
			{
				IAccount account = null;
				if (!string.IsNullOrEmpty(userId) || ssoMode != SingleSignOnMode.Disabled)
				{
					if (isWamBasedAuthenticationEnabled && string.IsNullOrEmpty(userId))
					{
						account = PublicClientApplication.OperatingSystemAccount;
					}
					AuthenticationResult authenticationResult;
					Exception ex;
					if (MsalAuthenticationHandle.UserTokenAuthenticationHandle.TryAcquireTokenSilent(app, scopes, false, isWamBasedAuthenticationEnabled, userId, null, ref account, out authenticationResult, out ex))
					{
						return authenticationResult;
					}
					if (isWamBasedAuthenticationEnabled)
					{
						if (ssoMode == SingleSignOnMode.Mandatory)
						{
							throw new NonInteractiveLoginException(ex);
						}
						if (account == PublicClientApplication.OperatingSystemAccount)
						{
							account = null;
						}
					}
				}
				if (!isWamBasedAuthenticationEnabled && ssoMode != SingleSignOnMode.Disabled)
				{
					AuthenticationResult authenticationResult;
					Exception ex2;
					if (MsalAuthenticationHandle.InteractiveAuthenticationHandle.TryAcquireTokenUsingIntegratedWindowsFlow(app, scopes, userId, out authenticationResult, out ex2))
					{
						return authenticationResult;
					}
					if (ssoMode == SingleSignOnMode.Mandatory)
					{
						if (!(ex2 is MsalUiRequiredException))
						{
							MsalClientException ex3 = ex2 as MsalClientException;
							if (ex3 == null || !MsalAuthenticationHandle.IsClientExceptionIndicateInteractiveLoginIsNeeded(ex3, false))
							{
								throw new AuthenticationException(ex2);
							}
						}
						throw new NonInteractiveLoginException(ex2);
					}
				}
				return MsalAuthenticationHandle.InteractiveAuthenticationHandle.AcquireTokenImpl(app, scopes, userId, account);
			}

			// Token: 0x06001418 RID: 5144 RVA: 0x00045888 File Offset: 0x00043A88
			private static bool TryAcquireTokenUsingIntegratedWindowsFlow(IPublicClientApplication app, IEnumerable<string> scopes, string userId, out AuthenticationResult result, out Exception error)
			{
				bool flag;
				try
				{
					Task<AuthenticationResult> task;
					if (!string.IsNullOrEmpty(userId))
					{
						task = app.AcquireTokenByIntegratedWindowsAuth(scopes).WithUsername(userId).ExecuteAsync();
					}
					else
					{
						task = app.AcquireTokenByIntegratedWindowsAuth(scopes).ExecuteAsync();
					}
					result = task.WaitForTaskCompletionAndGetResult<AuthenticationResult>();
					error = null;
					flag = true;
				}
				catch (Exception ex)
				{
					result = null;
					error = ex;
					flag = false;
				}
				return flag;
			}

			// Token: 0x06001419 RID: 5145 RVA: 0x000458EC File Offset: 0x00043AEC
			private static AuthenticationResult AcquireTokenImpl(IPublicClientApplication app, IEnumerable<string> scopes, string userId, IAccount account)
			{
				AuthenticationResult authenticationResult;
				using (MsalAuthenticationHelper.CreateMsalInteractiveAuthenticationScope())
				{
					try
					{
						Task<AuthenticationResult> task;
						if (account != null)
						{
							task = app.AcquireTokenInteractive(scopes).WithAccount(account).WithCustomPlatformUi()
								.WithPrompt(Prompt.SelectAccount)
								.ExecuteAsync();
						}
						else if (!string.IsNullOrEmpty(userId))
						{
							task = app.AcquireTokenInteractive(scopes).WithLoginHint(userId).WithCustomPlatformUi()
								.WithPrompt(Prompt.SelectAccount)
								.ExecuteAsync();
						}
						else
						{
							task = app.AcquireTokenInteractive(scopes).WithCustomPlatformUi().WithPrompt(Prompt.SelectAccount)
								.ExecuteAsync();
						}
						authenticationResult = task.WaitForTaskCompletionAndGetResult<AuthenticationResult>();
					}
					catch (MsalException ex)
					{
						throw new AuthenticationException(ex);
					}
				}
				return authenticationResult;
			}
		}

		// Token: 0x020001DC RID: 476
		private sealed class ServicePrincipalAuthenticationHandle : MsalAuthenticationHandle
		{
			// Token: 0x0600141A RID: 5146 RVA: 0x000459A4 File Offset: 0x00043BA4
			private ServicePrincipalAuthenticationHandle(IConfidentialClientApplication app, IEnumerable<string> scopes, AuthenticationResult result)
				: base(app, scopes, result)
			{
				this.principal = ((result.Account != null) ? (result.Account.Username ?? string.Empty) : string.Empty);
			}

			// Token: 0x0600141B RID: 5147 RVA: 0x000459D8 File Offset: 0x00043BD8
			public static AuthenticationHandle Create(IConfidentialClientApplication app, IEnumerable<string> scopes)
			{
				return new MsalAuthenticationHandle.ServicePrincipalAuthenticationHandle(app, scopes, MsalAuthenticationHandle.ServicePrincipalAuthenticationHandle.AcquireTokenImpl(app, scopes));
			}

			// Token: 0x170006F5 RID: 1781
			// (get) Token: 0x0600141C RID: 5148 RVA: 0x000459E8 File Offset: 0x00043BE8
			public override string Principal
			{
				get
				{
					return this.principal;
				}
			}

			// Token: 0x0600141D RID: 5149 RVA: 0x000459F0 File Offset: 0x00043BF0
			protected override AuthenticationResult RefreshAccessToken(IClientApplicationBase app, IEnumerable<string> scopes)
			{
				return MsalAuthenticationHandle.ServicePrincipalAuthenticationHandle.AcquireTokenImpl((IConfidentialClientApplication)app, scopes);
			}

			// Token: 0x0600141E RID: 5150 RVA: 0x00045A00 File Offset: 0x00043C00
			private static AuthenticationResult AcquireTokenImpl(IConfidentialClientApplication app, IEnumerable<string> scopes)
			{
				AuthenticationResult authenticationResult;
				try
				{
					authenticationResult = app.AcquireTokenForClient(scopes).ExecuteAsync().WaitForTaskCompletionAndGetResult<AuthenticationResult>();
				}
				catch (MsalException ex)
				{
					throw new AuthenticationException(ex);
				}
				return authenticationResult;
			}

			// Token: 0x04000E3F RID: 3647
			private readonly string principal;
		}

		// Token: 0x020001DD RID: 477
		private sealed class TokenCacheManager
		{
			// Token: 0x0600141F RID: 5151 RVA: 0x00045A38 File Offset: 0x00043C38
			static TokenCacheManager()
			{
				using (GlobalContext.CreateGlobalLockScope("MICROSOFT_ANALYSISSERVICES_XMLA_LIBS_MSAL_CACHE_LOCK"))
				{
					if (!GlobalContext.TryGetGlobalObject<IDictionary<string, KeyValuePair<long, byte[]>>>("MICROSOFT_ANALYSISSERVICES_XMLA_LIBS_MSAL_CACHE", out MsalAuthenticationHandle.TokenCacheManager.globalCache))
					{
						MsalAuthenticationHandle.TokenCacheManager.globalCache = new Dictionary<string, KeyValuePair<long, byte[]>>();
						GlobalContext.SetGlobalObject("MICROSOFT_ANALYSISSERVICES_XMLA_LIBS_MSAL_CACHE", MsalAuthenticationHandle.TokenCacheManager.globalCache);
					}
				}
			}

			// Token: 0x06001420 RID: 5152 RVA: 0x00045A98 File Offset: 0x00043C98
			private TokenCacheManager(IClientApplicationBase app, string cacheKey)
			{
				this.app = app;
				this.cacheKey = cacheKey;
				this.localVersion = -1L;
				this.app.UserTokenCache.SetBeforeAccess(new TokenCacheCallback(this.BeforeCacheAccess));
				this.app.UserTokenCache.SetBeforeWrite(new TokenCacheCallback(this.BeforeCacheWrite));
				this.app.UserTokenCache.SetAfterAccess(new TokenCacheCallback(this.AfterCacheAccess));
			}

			// Token: 0x06001421 RID: 5153 RVA: 0x00045B15 File Offset: 0x00043D15
			public static MsalAuthenticationHandle.TokenCacheManager Create(IClientApplicationBase app, string cacheKey)
			{
				return new MsalAuthenticationHandle.TokenCacheManager(app, cacheKey);
			}

			// Token: 0x06001422 RID: 5154 RVA: 0x00045B1E File Offset: 0x00043D1E
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private static void TraceCacheEvent(IClientApplicationBase app, string @event, TokenCacheNotificationArgs tcn)
			{
			}

			// Token: 0x06001423 RID: 5155 RVA: 0x00045B20 File Offset: 0x00043D20
			private static void UpdateLocalCache(IClientApplicationBase app, string @event, ITokenCacheSerializer serializer, bool clearLocalCache, long globalVersion, byte[] payload, ref long localVersion)
			{
				try
				{
					serializer.DeserializeMsalV3(payload, clearLocalCache);
					localVersion = globalVersion;
				}
				catch (Exception ex)
				{
					AuthenticationTracer.TraceError("Token-cache update failed - app={0}, event={1}, exception={2}", new object[]
					{
						MsalAuthenticationHandle.GetApplicationDisplayInfo(app),
						@event,
						ex
					});
					throw;
				}
			}

			// Token: 0x06001424 RID: 5156 RVA: 0x00045B74 File Offset: 0x00043D74
			private void BeforeCacheAccess(TokenCacheNotificationArgs tcn)
			{
				MsalAuthenticationHandle.TokenCacheManager.TraceCacheEvent(this.app, "BeforeAccess", tcn);
				KeyValuePair<long, byte[]> keyValuePair;
				using (GlobalContext.CreateGlobalLockScope("MICROSOFT_ANALYSISSERVICES_XMLA_LIBS_MSAL_CACHE_LOCK"))
				{
					if (!MsalAuthenticationHandle.TokenCacheManager.globalCache.TryGetValue(this.cacheKey, out keyValuePair))
					{
						keyValuePair = new KeyValuePair<long, byte[]>(-1L, null);
					}
				}
				if (this.localVersion < keyValuePair.Key)
				{
					MsalAuthenticationHandle.TokenCacheManager.UpdateLocalCache(this.app, "BeforeAccess", tcn.TokenCache, true, keyValuePair.Key, keyValuePair.Value, ref this.localVersion);
				}
			}

			// Token: 0x06001425 RID: 5157 RVA: 0x00045C10 File Offset: 0x00043E10
			private void BeforeCacheWrite(TokenCacheNotificationArgs tcn)
			{
				MsalAuthenticationHandle.TokenCacheManager.TraceCacheEvent(this.app, "BeforeWrite", tcn);
				KeyValuePair<long, byte[]> keyValuePair;
				using (GlobalContext.CreateGlobalLockScope("MICROSOFT_ANALYSISSERVICES_XMLA_LIBS_MSAL_CACHE_LOCK"))
				{
					if (!MsalAuthenticationHandle.TokenCacheManager.globalCache.TryGetValue(this.cacheKey, out keyValuePair))
					{
						keyValuePair = new KeyValuePair<long, byte[]>(-1L, null);
					}
				}
				if (this.localVersion < keyValuePair.Key)
				{
					MsalAuthenticationHandle.TokenCacheManager.UpdateLocalCache(this.app, "BeforeWrite", tcn.TokenCache, false, keyValuePair.Key, keyValuePair.Value, ref this.localVersion);
				}
			}

			// Token: 0x06001426 RID: 5158 RVA: 0x00045CAC File Offset: 0x00043EAC
			private void AfterCacheAccess(TokenCacheNotificationArgs tcn)
			{
				MsalAuthenticationHandle.TokenCacheManager.TraceCacheEvent(this.app, "AfterAccess", tcn);
				if (tcn.HasStateChanged)
				{
					using (GlobalContext.CreateGlobalLockScope("MICROSOFT_ANALYSISSERVICES_XMLA_LIBS_MSAL_CACHE_LOCK"))
					{
						KeyValuePair<long, byte[]> keyValuePair;
						if (!MsalAuthenticationHandle.TokenCacheManager.globalCache.TryGetValue(this.cacheKey, out keyValuePair))
						{
							keyValuePair = new KeyValuePair<long, byte[]>(-1L, null);
						}
						if (this.localVersion < keyValuePair.Key)
						{
							MsalAuthenticationHandle.TokenCacheManager.UpdateLocalCache(this.app, "AfterAccess", tcn.TokenCache, false, keyValuePair.Key, keyValuePair.Value, ref this.localVersion);
						}
						try
						{
							this.localVersion += 1L;
							MsalAuthenticationHandle.TokenCacheManager.globalCache[this.cacheKey] = new KeyValuePair<long, byte[]>(this.localVersion, tcn.TokenCache.SerializeMsalV3());
						}
						catch (Exception ex)
						{
							AuthenticationTracer.TraceError("Token-cache serialization failed - app={0}, exception={1}", new object[]
							{
								MsalAuthenticationHandle.GetApplicationDisplayInfo(this.app),
								ex
							});
							this.localVersion = keyValuePair.Key;
							throw;
						}
					}
				}
			}

			// Token: 0x04000E40 RID: 3648
			private const string AppDomainKey_MsalCacheLock = "MICROSOFT_ANALYSISSERVICES_XMLA_LIBS_MSAL_CACHE_LOCK";

			// Token: 0x04000E41 RID: 3649
			private const string AppDomainKey_MsalCache = "MICROSOFT_ANALYSISSERVICES_XMLA_LIBS_MSAL_CACHE";

			// Token: 0x04000E42 RID: 3650
			private static readonly IDictionary<string, KeyValuePair<long, byte[]>> globalCache;

			// Token: 0x04000E43 RID: 3651
			private readonly IClientApplicationBase app;

			// Token: 0x04000E44 RID: 3652
			private readonly string cacheKey;

			// Token: 0x04000E45 RID: 3653
			private long localVersion;
		}
	}
}
