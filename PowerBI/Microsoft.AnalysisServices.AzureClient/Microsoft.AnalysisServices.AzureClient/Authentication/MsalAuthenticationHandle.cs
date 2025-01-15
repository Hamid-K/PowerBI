using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AnalysisServices.AzureClient.Hosting;
using Microsoft.AnalysisServices.AzureClient.Utilities;
using Microsoft.Identity.Client;

namespace Microsoft.AnalysisServices.AzureClient.Authentication
{
	// Token: 0x0200001F RID: 31
	internal abstract class MsalAuthenticationHandle : AuthenticationHandle
	{
		// Token: 0x060000DC RID: 220 RVA: 0x00004454 File Offset: 0x00002654
		protected MsalAuthenticationHandle(IClientApplicationBase app, IEnumerable<string> scopes, AuthenticationResult result)
			: base(AuthenticationEndpoint.AadV2, (result.Account != null) ? result.Account.Environment : null, (result.Account != null && result.Account.HomeAccountId != null) ? result.Account.HomeAccountId.TenantId : result.TenantId)
		{
			this.app = app;
			this.scopes = scopes;
			this.accessToken = result.AccessToken;
			this.refreshBy = AuthenticationManager.CalculateAccessTokenRefreshBy(result.ExpiresOn);
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000DD RID: 221 RVA: 0x000044D6 File Offset: 0x000026D6
		public override string AuthenticationScheme
		{
			get
			{
				return "Bearer";
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000DE RID: 222 RVA: 0x000044DD File Offset: 0x000026DD
		internal static IAuthenticationService Service
		{
			get
			{
				return MsalAuthenticationHandle.service;
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000044E4 File Offset: 0x000026E4
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

		// Token: 0x060000E0 RID: 224 RVA: 0x00004588 File Offset: 0x00002788
		public sealed override long GetRefreshByTimeAsFileTime()
		{
			return this.refreshBy.ToFileTime();
		}

		// Token: 0x060000E1 RID: 225
		protected abstract AuthenticationResult RefreshAccessToken(IClientApplicationBase app, IEnumerable<string> scopes);

		// Token: 0x060000E2 RID: 226 RVA: 0x00004595 File Offset: 0x00002795
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static string GetApplicationDisplayInfo(IClientApplicationBase app)
		{
			return string.Format("[Authority='{0}', ClientId={1}, HashCode={2}]", app.Authority, app.AppConfig.ClientId, app.GetHashCode());
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000045C0 File Offset: 0x000027C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static string GetAccountDisplayInfo(IAccount account)
		{
			if (account == null)
			{
				return string.Empty;
			}
			return string.Format("[Username='{0}', Environment='{1}', HomeAccountId={2}]", account.Username, account.Environment, (account.HomeAccountId != null) ? string.Format("[Identifier={0}, ObjectId={1}, TenantId={2}]", account.HomeAccountId.Identifier, account.HomeAccountId.ObjectId, account.HomeAccountId.TenantId) : string.Empty);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004628 File Offset: 0x00002828
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

		// Token: 0x060000E5 RID: 229 RVA: 0x000046A8 File Offset: 0x000028A8
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

		// Token: 0x04000085 RID: 133
		private static readonly MsalAuthenticationHandle.MsalAuthenticationService service = new MsalAuthenticationHandle.MsalAuthenticationService();

		// Token: 0x04000086 RID: 134
		private IClientApplicationBase app;

		// Token: 0x04000087 RID: 135
		private IEnumerable<string> scopes;

		// Token: 0x04000088 RID: 136
		private string accessToken;

		// Token: 0x04000089 RID: 137
		private DateTimeOffset refreshBy;

		// Token: 0x02000052 RID: 82
		private sealed class MsalAuthenticationService : IAuthenticationService
		{
			// Token: 0x0600024D RID: 589 RVA: 0x0000B05C File Offset: 0x0000925C
			public AuthenticationHandle AuthenticateUser(AuthenticationOptions options, AuthenticationInformation authInfo, string userId, string password)
			{
				bool flag;
				IPublicClientApplication publicClientApplication;
				IEnumerable<string> enumerable;
				MsalAuthenticationHandle.MsalAuthenticationService.GetPublicClientApplicationAndScopes(options, authInfo, out flag, out publicClientApplication, out enumerable);
				return new MsalAuthenticationHandle.UsernamePasswordAuthenticationHandle(publicClientApplication, enumerable, MsalAuthenticationHandle.UsernamePasswordAuthenticationHandle.AcquireToken(publicClientApplication, enumerable, userId, password), password);
			}

			// Token: 0x0600024E RID: 590 RVA: 0x0000B08C File Offset: 0x0000928C
			public AuthenticationHandle AuthenticateUser(AuthenticationOptions options, AuthenticationInformation authInfo, string userId)
			{
				bool flag;
				IPublicClientApplication publicClientApplication;
				IEnumerable<string> enumerable;
				MsalAuthenticationHandle.MsalAuthenticationService.GetPublicClientApplicationAndScopes(options, authInfo, out flag, out publicClientApplication, out enumerable);
				return new MsalAuthenticationHandle.InteractiveAuthenticationHandle(publicClientApplication, enumerable, MsalAuthenticationHandle.InteractiveAuthenticationHandle.AcquireToken(flag, publicClientApplication, enumerable, options.SsoMode, userId));
			}

			// Token: 0x0600024F RID: 591 RVA: 0x0000B0BC File Offset: 0x000092BC
			public AuthenticationHandle AuthenticateUser(AuthenticationOptions options, AuthenticationInformation authInfo)
			{
				bool flag;
				IPublicClientApplication publicClientApplication;
				IEnumerable<string> enumerable;
				MsalAuthenticationHandle.MsalAuthenticationService.GetPublicClientApplicationAndScopes(options, authInfo, out flag, out publicClientApplication, out enumerable);
				return new MsalAuthenticationHandle.InteractiveAuthenticationHandle(publicClientApplication, enumerable, MsalAuthenticationHandle.InteractiveAuthenticationHandle.AcquireToken(flag, publicClientApplication, enumerable, options.SsoMode, null));
			}

			// Token: 0x06000250 RID: 592 RVA: 0x0000B0EC File Offset: 0x000092EC
			public AuthenticationHandle AuthenticateServicePrincipal(AuthenticationOptions options, AuthenticationInformation authInfo, string applicationId, X509Certificate2 certificate)
			{
				IConfidentialClientApplication confidentialClientApplication;
				IEnumerable<string> enumerable;
				MsalAuthenticationHandle.MsalAuthenticationService.GetConfidentialClientApplicationAndScopes(options, authInfo, applicationId, certificate, null, out confidentialClientApplication, out enumerable);
				return MsalAuthenticationHandle.ServicePrincipalAuthenticationHandle.Create(confidentialClientApplication, enumerable);
			}

			// Token: 0x06000251 RID: 593 RVA: 0x0000B110 File Offset: 0x00009310
			public AuthenticationHandle AuthenticateServicePrincipal(AuthenticationOptions options, AuthenticationInformation authInfo, string applicationId, string secret)
			{
				IConfidentialClientApplication confidentialClientApplication;
				IEnumerable<string> enumerable;
				MsalAuthenticationHandle.MsalAuthenticationService.GetConfidentialClientApplicationAndScopes(options, authInfo, applicationId, null, secret, out confidentialClientApplication, out enumerable);
				return MsalAuthenticationHandle.ServicePrincipalAuthenticationHandle.Create(confidentialClientApplication, enumerable);
			}

			// Token: 0x06000252 RID: 594 RVA: 0x0000B134 File Offset: 0x00009334
			private static void GetPublicClientApplicationAndScopes(AuthenticationOptions options, AuthenticationInformation authInfo, out bool isWamBasedAuthenticationEnabled, out IPublicClientApplication app, out IEnumerable<string> scopes)
			{
				isWamBasedAuthenticationEnabled = false;
				app = MsalAuthenticationHandle.MsalAuthenticationService.CreatePublicClientApplication(authInfo.ApplicationId, authInfo.Authority, isWamBasedAuthenticationEnabled && options.SsoMode > SingleSignOnMode.Disabled);
				if (options.UseTokenCache)
				{
					string tokenCacheKey = authInfo.GetTokenCacheKey();
					MsalAuthenticationHandle.TokenCacheManager.Create(app, tokenCacheKey);
				}
				scopes = MsalAuthenticationHandle.MsalAuthenticationService.GetDefaultScopes(authInfo.ResourceId);
			}

			// Token: 0x06000253 RID: 595 RVA: 0x0000B190 File Offset: 0x00009390
			private static IPublicClientApplication CreatePublicClientApplication(string clientId, string authority, bool enableWamBroker)
			{
				PublicClientApplicationBuilder publicClientApplicationBuilder = PublicClientApplicationBuilder.Create(clientId).WithAuthority(authority, true).WithRedirectUri("https://login.microsoftonline.com/common/oauth2/nativeclient");
				if (AuthenticationTracer.IsTracingEnabled)
				{
					publicClientApplicationBuilder = publicClientApplicationBuilder.WithLogging(MsalAuthenticationHandle.MsalAuthenticationService.onMsalTraceEvent, new LogLevel?(2), new bool?(false), null);
				}
				return publicClientApplicationBuilder.Build();
			}

			// Token: 0x06000254 RID: 596 RVA: 0x0000B1E4 File Offset: 0x000093E4
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

			// Token: 0x06000255 RID: 597 RVA: 0x0000B263 File Offset: 0x00009463
			private static IEnumerable<string> GetDefaultScopes(string resourceId)
			{
				return new string[] { string.Format("{0}/.default", resourceId) };
			}

			// Token: 0x040001A5 RID: 421
			private const string MsalTraceMessageFormat = "MSAL: {0}";

			// Token: 0x040001A6 RID: 422
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

		// Token: 0x02000053 RID: 83
		private abstract class UserTokenAuthenticationHandle : MsalAuthenticationHandle
		{
			// Token: 0x06000257 RID: 599 RVA: 0x0000B290 File Offset: 0x00009490
			protected UserTokenAuthenticationHandle(IPublicClientApplication app, IEnumerable<string> scopes, AuthenticationResult result)
				: base(app, scopes, result)
			{
				this.username = result.Account.Username ?? string.Empty;
				this.provider = result.Account.Environment ?? string.Empty;
				this.accountId = ((result.Account.HomeAccountId != null) ? (result.Account.HomeAccountId.Identifier ?? string.Empty) : string.Empty);
			}

			// Token: 0x17000064 RID: 100
			// (get) Token: 0x06000258 RID: 600 RVA: 0x0000B30D File Offset: 0x0000950D
			public override string Principal
			{
				get
				{
					return this.username;
				}
			}

			// Token: 0x06000259 RID: 601 RVA: 0x0000B318 File Offset: 0x00009518
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
				return flag;
			}

			// Token: 0x0600025A RID: 602
			protected abstract AuthenticationResult RefreshAccessTokenImpl(IPublicClientApplication app, IEnumerable<string> scopes, IAccount account);

			// Token: 0x0600025B RID: 603 RVA: 0x0000B438 File Offset: 0x00009638
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

			// Token: 0x040001A7 RID: 423
			private readonly string username;

			// Token: 0x040001A8 RID: 424
			private readonly string provider;

			// Token: 0x040001A9 RID: 425
			private readonly string accountId;
		}

		// Token: 0x02000054 RID: 84
		private sealed class UsernamePasswordAuthenticationHandle : MsalAuthenticationHandle.UserTokenAuthenticationHandle
		{
			// Token: 0x0600025C RID: 604 RVA: 0x0000B48A File Offset: 0x0000968A
			internal UsernamePasswordAuthenticationHandle(IPublicClientApplication app, IEnumerable<string> scopes, AuthenticationResult result, string password)
				: base(app, scopes, result)
			{
				this.password = password;
			}

			// Token: 0x0600025D RID: 605 RVA: 0x0000B49D File Offset: 0x0000969D
			protected override AuthenticationResult RefreshAccessTokenImpl(IPublicClientApplication app, IEnumerable<string> scopes, IAccount account)
			{
				return MsalAuthenticationHandle.UsernamePasswordAuthenticationHandle.AcquireTokenImpl(app, scopes, account.Username, this.password);
			}

			// Token: 0x0600025E RID: 606 RVA: 0x0000B4B4 File Offset: 0x000096B4
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

			// Token: 0x0600025F RID: 607 RVA: 0x0000B4E4 File Offset: 0x000096E4
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

			// Token: 0x040001AA RID: 426
			private string password;
		}

		// Token: 0x02000055 RID: 85
		private sealed class InteractiveAuthenticationHandle : MsalAuthenticationHandle.UserTokenAuthenticationHandle
		{
			// Token: 0x06000260 RID: 608 RVA: 0x0000B554 File Offset: 0x00009754
			internal InteractiveAuthenticationHandle(IPublicClientApplication app, IEnumerable<string> scopes, AuthenticationResult result)
				: base(app, scopes, result)
			{
			}

			// Token: 0x06000261 RID: 609 RVA: 0x0000B55F File Offset: 0x0000975F
			protected override AuthenticationResult RefreshAccessTokenImpl(IPublicClientApplication app, IEnumerable<string> scopes, IAccount account)
			{
				return MsalAuthenticationHandle.InteractiveAuthenticationHandle.AcquireTokenImpl(app, scopes, null, account);
			}

			// Token: 0x06000262 RID: 610 RVA: 0x0000B56C File Offset: 0x0000976C
			internal static AuthenticationResult AcquireToken(bool isWamBasedAuthenticationEnabled, IPublicClientApplication app, IEnumerable<string> scopes, SingleSignOnMode ssoMode, string userId)
			{
				IAccount account = null;
				AuthenticationResult authenticationResult;
				Exception ex;
				if ((!string.IsNullOrEmpty(userId) || ssoMode != SingleSignOnMode.Disabled) && MsalAuthenticationHandle.UserTokenAuthenticationHandle.TryAcquireTokenSilent(app, scopes, false, isWamBasedAuthenticationEnabled, userId, null, ref account, out authenticationResult, out ex))
				{
					return authenticationResult;
				}
				if (ssoMode != SingleSignOnMode.Disabled)
				{
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

			// Token: 0x06000263 RID: 611 RVA: 0x0000B5EC File Offset: 0x000097EC
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

			// Token: 0x06000264 RID: 612 RVA: 0x0000B650 File Offset: 0x00009850
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

		// Token: 0x02000056 RID: 86
		private sealed class ServicePrincipalAuthenticationHandle : MsalAuthenticationHandle
		{
			// Token: 0x06000265 RID: 613 RVA: 0x0000B708 File Offset: 0x00009908
			private ServicePrincipalAuthenticationHandle(IConfidentialClientApplication app, IEnumerable<string> scopes, AuthenticationResult result)
				: base(app, scopes, result)
			{
				this.principal = ((result.Account != null) ? (result.Account.Username ?? string.Empty) : string.Empty);
			}

			// Token: 0x06000266 RID: 614 RVA: 0x0000B73C File Offset: 0x0000993C
			public static AuthenticationHandle Create(IConfidentialClientApplication app, IEnumerable<string> scopes)
			{
				return new MsalAuthenticationHandle.ServicePrincipalAuthenticationHandle(app, scopes, MsalAuthenticationHandle.ServicePrincipalAuthenticationHandle.AcquireTokenImpl(app, scopes));
			}

			// Token: 0x17000065 RID: 101
			// (get) Token: 0x06000267 RID: 615 RVA: 0x0000B74C File Offset: 0x0000994C
			public override string Principal
			{
				get
				{
					return this.principal;
				}
			}

			// Token: 0x06000268 RID: 616 RVA: 0x0000B754 File Offset: 0x00009954
			protected override AuthenticationResult RefreshAccessToken(IClientApplicationBase app, IEnumerable<string> scopes)
			{
				return MsalAuthenticationHandle.ServicePrincipalAuthenticationHandle.AcquireTokenImpl((IConfidentialClientApplication)app, scopes);
			}

			// Token: 0x06000269 RID: 617 RVA: 0x0000B764 File Offset: 0x00009964
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

			// Token: 0x040001AB RID: 427
			private readonly string principal;
		}

		// Token: 0x02000057 RID: 87
		private sealed class TokenCacheManager
		{
			// Token: 0x0600026A RID: 618 RVA: 0x0000B79C File Offset: 0x0000999C
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

			// Token: 0x0600026B RID: 619 RVA: 0x0000B7FC File Offset: 0x000099FC
			private TokenCacheManager(IClientApplicationBase app, string cacheKey)
			{
				this.app = app;
				this.cacheKey = cacheKey;
				this.localVersion = -1L;
				this.app.UserTokenCache.SetBeforeAccess(new TokenCacheCallback(this.BeforeCacheAccess));
				this.app.UserTokenCache.SetBeforeWrite(new TokenCacheCallback(this.BeforeCacheWrite));
				this.app.UserTokenCache.SetAfterAccess(new TokenCacheCallback(this.AfterCacheAccess));
			}

			// Token: 0x0600026C RID: 620 RVA: 0x0000B879 File Offset: 0x00009A79
			public static MsalAuthenticationHandle.TokenCacheManager Create(IClientApplicationBase app, string cacheKey)
			{
				return new MsalAuthenticationHandle.TokenCacheManager(app, cacheKey);
			}

			// Token: 0x0600026D RID: 621 RVA: 0x0000B882 File Offset: 0x00009A82
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private static void TraceCacheEvent(IClientApplicationBase app, string @event, TokenCacheNotificationArgs tcn)
			{
			}

			// Token: 0x0600026E RID: 622 RVA: 0x0000B884 File Offset: 0x00009A84
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

			// Token: 0x0600026F RID: 623 RVA: 0x0000B8D8 File Offset: 0x00009AD8
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

			// Token: 0x06000270 RID: 624 RVA: 0x0000B974 File Offset: 0x00009B74
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

			// Token: 0x06000271 RID: 625 RVA: 0x0000BA10 File Offset: 0x00009C10
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

			// Token: 0x040001AC RID: 428
			private const string AppDomainKey_MsalCacheLock = "MICROSOFT_ANALYSISSERVICES_XMLA_LIBS_MSAL_CACHE_LOCK";

			// Token: 0x040001AD RID: 429
			private const string AppDomainKey_MsalCache = "MICROSOFT_ANALYSISSERVICES_XMLA_LIBS_MSAL_CACHE";

			// Token: 0x040001AE RID: 430
			private static readonly IDictionary<string, KeyValuePair<long, byte[]>> globalCache;

			// Token: 0x040001AF RID: 431
			private readonly IClientApplicationBase app;

			// Token: 0x040001B0 RID: 432
			private readonly string cacheKey;

			// Token: 0x040001B1 RID: 433
			private long localVersion;
		}
	}
}
