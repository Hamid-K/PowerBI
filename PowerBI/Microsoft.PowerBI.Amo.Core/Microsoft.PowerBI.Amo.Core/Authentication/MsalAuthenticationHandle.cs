using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AnalysisServices.Hosting;
using Microsoft.AnalysisServices.Utilities;
using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Broker;

namespace Microsoft.AnalysisServices.Authentication
{
	// Token: 0x020000FB RID: 251
	internal abstract class MsalAuthenticationHandle : AuthenticationHandle
	{
		// Token: 0x06000FA8 RID: 4008 RVA: 0x00035C88 File Offset: 0x00033E88
		protected MsalAuthenticationHandle(IClientApplicationBase app, IEnumerable<string> scopes, AuthenticationResult result)
			: base(AuthenticationEndpoint.AadV2, (result.Account != null) ? result.Account.Environment : null, (result.Account != null && result.Account.HomeAccountId != null) ? result.Account.HomeAccountId.TenantId : result.TenantId)
		{
			this.app = app;
			this.scopes = scopes;
			this.accessToken = result.AccessToken;
			this.refreshBy = AuthenticationManager.CalculateAccessTokenRefreshBy(result.ExpiresOn);
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06000FA9 RID: 4009 RVA: 0x00035D0A File Offset: 0x00033F0A
		public override string AuthenticationScheme
		{
			get
			{
				return "Bearer";
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06000FAA RID: 4010 RVA: 0x00035D11 File Offset: 0x00033F11
		internal static IAuthenticationService Service
		{
			get
			{
				return MsalAuthenticationHandle.service;
			}
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x00035D18 File Offset: 0x00033F18
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

		// Token: 0x06000FAC RID: 4012 RVA: 0x00035DBC File Offset: 0x00033FBC
		public sealed override long GetRefreshByTimeAsFileTime()
		{
			return this.refreshBy.ToFileTime();
		}

		// Token: 0x06000FAD RID: 4013
		protected abstract AuthenticationResult RefreshAccessToken(IClientApplicationBase app, IEnumerable<string> scopes);

		// Token: 0x06000FAE RID: 4014 RVA: 0x00035DCC File Offset: 0x00033FCC
		private static bool CheckIfWamBasedAuthenticationSupported()
		{
			Version version;
			bool flag;
			WindowsRuntimeHelper.GetOperatingSystemVersion(out version, out flag);
			return version.Major >= 10 && (version.Minor != 10 || version.Minor != 0 || version.Build >= 17763 || flag);
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x00035E13 File Offset: 0x00034013
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static string GetApplicationDisplayInfo(IClientApplicationBase app)
		{
			return string.Format("[Authority='{0}', ClientId={1}, HashCode={2}]", app.Authority, app.AppConfig.ClientId, app.GetHashCode());
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x00035E3C File Offset: 0x0003403C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static string GetAccountDisplayInfo(IAccount account)
		{
			if (account == null)
			{
				return string.Empty;
			}
			return string.Format("[Username='{0}', Environment='{1}', HomeAccountId={2}]", account.Username, account.Environment, (account.HomeAccountId != null) ? string.Format("[Identifier={0}, ObjectId={1}, TenantId={2}]", account.HomeAccountId.Identifier, account.HomeAccountId.ObjectId, account.HomeAccountId.TenantId) : string.Empty);
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x00035EA4 File Offset: 0x000340A4
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

		// Token: 0x06000FB2 RID: 4018 RVA: 0x00035F24 File Offset: 0x00034124
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

		// Token: 0x0400086E RID: 2158
		private static readonly bool isWamBasedAuthenticationSupported = MsalAuthenticationHandle.CheckIfWamBasedAuthenticationSupported();

		// Token: 0x0400086F RID: 2159
		private static readonly MsalAuthenticationHandle.MsalAuthenticationService service = new MsalAuthenticationHandle.MsalAuthenticationService();

		// Token: 0x04000870 RID: 2160
		private IClientApplicationBase app;

		// Token: 0x04000871 RID: 2161
		private IEnumerable<string> scopes;

		// Token: 0x04000872 RID: 2162
		private string accessToken;

		// Token: 0x04000873 RID: 2163
		private DateTimeOffset refreshBy;

		// Token: 0x020001B5 RID: 437
		private sealed class MsalAuthenticationService : IAuthenticationService
		{
			// Token: 0x06001369 RID: 4969 RVA: 0x00043968 File Offset: 0x00041B68
			public AuthenticationHandle AuthenticateUser(AuthenticationOptions options, AuthenticationInformation authInfo, string userId, string password)
			{
				bool flag;
				IPublicClientApplication publicClientApplication;
				IEnumerable<string> enumerable;
				MsalAuthenticationHandle.MsalAuthenticationService.GetPublicClientApplicationAndScopes(options, authInfo, out flag, out publicClientApplication, out enumerable);
				return new MsalAuthenticationHandle.UsernamePasswordAuthenticationHandle(publicClientApplication, enumerable, MsalAuthenticationHandle.UsernamePasswordAuthenticationHandle.AcquireToken(publicClientApplication, enumerable, userId, password), password);
			}

			// Token: 0x0600136A RID: 4970 RVA: 0x00043998 File Offset: 0x00041B98
			public AuthenticationHandle AuthenticateUser(AuthenticationOptions options, AuthenticationInformation authInfo, string userId)
			{
				bool flag;
				IPublicClientApplication publicClientApplication;
				IEnumerable<string> enumerable;
				MsalAuthenticationHandle.MsalAuthenticationService.GetPublicClientApplicationAndScopes(options, authInfo, out flag, out publicClientApplication, out enumerable);
				return new MsalAuthenticationHandle.InteractiveAuthenticationHandle(publicClientApplication, enumerable, MsalAuthenticationHandle.InteractiveAuthenticationHandle.AcquireToken(flag, publicClientApplication, enumerable, options.SsoMode, userId));
			}

			// Token: 0x0600136B RID: 4971 RVA: 0x000439C8 File Offset: 0x00041BC8
			public AuthenticationHandle AuthenticateUser(AuthenticationOptions options, AuthenticationInformation authInfo)
			{
				bool flag;
				IPublicClientApplication publicClientApplication;
				IEnumerable<string> enumerable;
				MsalAuthenticationHandle.MsalAuthenticationService.GetPublicClientApplicationAndScopes(options, authInfo, out flag, out publicClientApplication, out enumerable);
				return new MsalAuthenticationHandle.InteractiveAuthenticationHandle(publicClientApplication, enumerable, MsalAuthenticationHandle.InteractiveAuthenticationHandle.AcquireToken(flag, publicClientApplication, enumerable, options.SsoMode, null));
			}

			// Token: 0x0600136C RID: 4972 RVA: 0x000439F8 File Offset: 0x00041BF8
			public AuthenticationHandle AuthenticateServicePrincipal(AuthenticationOptions options, AuthenticationInformation authInfo, string applicationId, X509Certificate2 certificate)
			{
				IConfidentialClientApplication confidentialClientApplication;
				IEnumerable<string> enumerable;
				MsalAuthenticationHandle.MsalAuthenticationService.GetConfidentialClientApplicationAndScopes(options, authInfo, applicationId, certificate, null, out confidentialClientApplication, out enumerable);
				return MsalAuthenticationHandle.ServicePrincipalAuthenticationHandle.Create(confidentialClientApplication, enumerable);
			}

			// Token: 0x0600136D RID: 4973 RVA: 0x00043A1C File Offset: 0x00041C1C
			public AuthenticationHandle AuthenticateServicePrincipal(AuthenticationOptions options, AuthenticationInformation authInfo, string applicationId, string secret)
			{
				IConfidentialClientApplication confidentialClientApplication;
				IEnumerable<string> enumerable;
				MsalAuthenticationHandle.MsalAuthenticationService.GetConfidentialClientApplicationAndScopes(options, authInfo, applicationId, null, secret, out confidentialClientApplication, out enumerable);
				return MsalAuthenticationHandle.ServicePrincipalAuthenticationHandle.Create(confidentialClientApplication, enumerable);
			}

			// Token: 0x0600136E RID: 4974 RVA: 0x00043A40 File Offset: 0x00041C40
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

			// Token: 0x0600136F RID: 4975 RVA: 0x00043AA8 File Offset: 0x00041CA8
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

			// Token: 0x06001370 RID: 4976 RVA: 0x00043B08 File Offset: 0x00041D08
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

			// Token: 0x06001371 RID: 4977 RVA: 0x00043B64 File Offset: 0x00041D64
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

			// Token: 0x06001372 RID: 4978 RVA: 0x00043BE3 File Offset: 0x00041DE3
			private static IEnumerable<string> GetDefaultScopes(string resourceId)
			{
				return new string[] { string.Format("{0}/.default", resourceId) };
			}

			// Token: 0x04001105 RID: 4357
			private const string MsalTraceMessageFormat = "MSAL: {0}";

			// Token: 0x04001106 RID: 4358
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

		// Token: 0x020001B6 RID: 438
		private abstract class UserTokenAuthenticationHandle : MsalAuthenticationHandle
		{
			// Token: 0x06001374 RID: 4980 RVA: 0x00043C10 File Offset: 0x00041E10
			protected UserTokenAuthenticationHandle(IPublicClientApplication app, IEnumerable<string> scopes, AuthenticationResult result)
				: base(app, scopes, result)
			{
				this.username = result.Account.Username ?? string.Empty;
				this.provider = result.Account.Environment ?? string.Empty;
				this.accountId = ((result.Account.HomeAccountId != null) ? (result.Account.HomeAccountId.Identifier ?? string.Empty) : string.Empty);
			}

			// Token: 0x1700063F RID: 1599
			// (get) Token: 0x06001375 RID: 4981 RVA: 0x00043C8D File Offset: 0x00041E8D
			public override string Principal
			{
				get
				{
					return this.username;
				}
			}

			// Token: 0x06001376 RID: 4982 RVA: 0x00043C98 File Offset: 0x00041E98
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

			// Token: 0x06001377 RID: 4983
			protected abstract AuthenticationResult RefreshAccessTokenImpl(IPublicClientApplication app, IEnumerable<string> scopes, IAccount account);

			// Token: 0x06001378 RID: 4984 RVA: 0x00043E04 File Offset: 0x00042004
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

			// Token: 0x04001107 RID: 4359
			private readonly string username;

			// Token: 0x04001108 RID: 4360
			private readonly string provider;

			// Token: 0x04001109 RID: 4361
			private readonly string accountId;
		}

		// Token: 0x020001B7 RID: 439
		private sealed class UsernamePasswordAuthenticationHandle : MsalAuthenticationHandle.UserTokenAuthenticationHandle
		{
			// Token: 0x06001379 RID: 4985 RVA: 0x00043E56 File Offset: 0x00042056
			internal UsernamePasswordAuthenticationHandle(IPublicClientApplication app, IEnumerable<string> scopes, AuthenticationResult result, string password)
				: base(app, scopes, result)
			{
				this.password = password;
			}

			// Token: 0x0600137A RID: 4986 RVA: 0x00043E69 File Offset: 0x00042069
			protected override AuthenticationResult RefreshAccessTokenImpl(IPublicClientApplication app, IEnumerable<string> scopes, IAccount account)
			{
				return MsalAuthenticationHandle.UsernamePasswordAuthenticationHandle.AcquireTokenImpl(app, scopes, account.Username, this.password);
			}

			// Token: 0x0600137B RID: 4987 RVA: 0x00043E80 File Offset: 0x00042080
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

			// Token: 0x0600137C RID: 4988 RVA: 0x00043EB0 File Offset: 0x000420B0
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

			// Token: 0x0400110A RID: 4362
			private string password;
		}

		// Token: 0x020001B8 RID: 440
		private sealed class InteractiveAuthenticationHandle : MsalAuthenticationHandle.UserTokenAuthenticationHandle
		{
			// Token: 0x0600137D RID: 4989 RVA: 0x00043F20 File Offset: 0x00042120
			internal InteractiveAuthenticationHandle(IPublicClientApplication app, IEnumerable<string> scopes, AuthenticationResult result)
				: base(app, scopes, result)
			{
			}

			// Token: 0x0600137E RID: 4990 RVA: 0x00043F2B File Offset: 0x0004212B
			protected override AuthenticationResult RefreshAccessTokenImpl(IPublicClientApplication app, IEnumerable<string> scopes, IAccount account)
			{
				return MsalAuthenticationHandle.InteractiveAuthenticationHandle.AcquireTokenImpl(app, scopes, null, account);
			}

			// Token: 0x0600137F RID: 4991 RVA: 0x00043F38 File Offset: 0x00042138
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

			// Token: 0x06001380 RID: 4992 RVA: 0x00043FE4 File Offset: 0x000421E4
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

			// Token: 0x06001381 RID: 4993 RVA: 0x00044048 File Offset: 0x00042248
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

		// Token: 0x020001B9 RID: 441
		private sealed class ServicePrincipalAuthenticationHandle : MsalAuthenticationHandle
		{
			// Token: 0x06001382 RID: 4994 RVA: 0x00044100 File Offset: 0x00042300
			private ServicePrincipalAuthenticationHandle(IConfidentialClientApplication app, IEnumerable<string> scopes, AuthenticationResult result)
				: base(app, scopes, result)
			{
				this.principal = ((result.Account != null) ? (result.Account.Username ?? string.Empty) : string.Empty);
			}

			// Token: 0x06001383 RID: 4995 RVA: 0x00044134 File Offset: 0x00042334
			public static AuthenticationHandle Create(IConfidentialClientApplication app, IEnumerable<string> scopes)
			{
				return new MsalAuthenticationHandle.ServicePrincipalAuthenticationHandle(app, scopes, MsalAuthenticationHandle.ServicePrincipalAuthenticationHandle.AcquireTokenImpl(app, scopes));
			}

			// Token: 0x17000640 RID: 1600
			// (get) Token: 0x06001384 RID: 4996 RVA: 0x00044144 File Offset: 0x00042344
			public override string Principal
			{
				get
				{
					return this.principal;
				}
			}

			// Token: 0x06001385 RID: 4997 RVA: 0x0004414C File Offset: 0x0004234C
			protected override AuthenticationResult RefreshAccessToken(IClientApplicationBase app, IEnumerable<string> scopes)
			{
				return MsalAuthenticationHandle.ServicePrincipalAuthenticationHandle.AcquireTokenImpl((IConfidentialClientApplication)app, scopes);
			}

			// Token: 0x06001386 RID: 4998 RVA: 0x0004415C File Offset: 0x0004235C
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

			// Token: 0x0400110B RID: 4363
			private readonly string principal;
		}

		// Token: 0x020001BA RID: 442
		private sealed class TokenCacheManager
		{
			// Token: 0x06001387 RID: 4999 RVA: 0x00044194 File Offset: 0x00042394
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

			// Token: 0x06001388 RID: 5000 RVA: 0x000441F4 File Offset: 0x000423F4
			private TokenCacheManager(IClientApplicationBase app, string cacheKey)
			{
				this.app = app;
				this.cacheKey = cacheKey;
				this.localVersion = -1L;
				this.app.UserTokenCache.SetBeforeAccess(new TokenCacheCallback(this.BeforeCacheAccess));
				this.app.UserTokenCache.SetBeforeWrite(new TokenCacheCallback(this.BeforeCacheWrite));
				this.app.UserTokenCache.SetAfterAccess(new TokenCacheCallback(this.AfterCacheAccess));
			}

			// Token: 0x06001389 RID: 5001 RVA: 0x00044271 File Offset: 0x00042471
			public static MsalAuthenticationHandle.TokenCacheManager Create(IClientApplicationBase app, string cacheKey)
			{
				return new MsalAuthenticationHandle.TokenCacheManager(app, cacheKey);
			}

			// Token: 0x0600138A RID: 5002 RVA: 0x0004427A File Offset: 0x0004247A
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private static void TraceCacheEvent(IClientApplicationBase app, string @event, TokenCacheNotificationArgs tcn)
			{
			}

			// Token: 0x0600138B RID: 5003 RVA: 0x0004427C File Offset: 0x0004247C
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

			// Token: 0x0600138C RID: 5004 RVA: 0x000442D0 File Offset: 0x000424D0
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

			// Token: 0x0600138D RID: 5005 RVA: 0x0004436C File Offset: 0x0004256C
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

			// Token: 0x0600138E RID: 5006 RVA: 0x00044408 File Offset: 0x00042608
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

			// Token: 0x0400110C RID: 4364
			private const string AppDomainKey_MsalCacheLock = "MICROSOFT_ANALYSISSERVICES_XMLA_LIBS_MSAL_CACHE_LOCK";

			// Token: 0x0400110D RID: 4365
			private const string AppDomainKey_MsalCache = "MICROSOFT_ANALYSISSERVICES_XMLA_LIBS_MSAL_CACHE";

			// Token: 0x0400110E RID: 4366
			private static readonly IDictionary<string, KeyValuePair<long, byte[]>> globalCache;

			// Token: 0x0400110F RID: 4367
			private readonly IClientApplicationBase app;

			// Token: 0x04001110 RID: 4368
			private readonly string cacheKey;

			// Token: 0x04001111 RID: 4369
			private long localVersion;
		}
	}
}
