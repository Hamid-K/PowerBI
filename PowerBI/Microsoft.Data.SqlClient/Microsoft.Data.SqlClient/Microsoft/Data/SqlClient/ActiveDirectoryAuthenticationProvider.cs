using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Azure.Core;
using Azure.Identity;
using Microsoft.Data.Common;
using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Extensibility;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000021 RID: 33
	public sealed class ActiveDirectoryAuthenticationProvider : SqlAuthenticationProvider
	{
		// Token: 0x0600067F RID: 1663 RVA: 0x0000C874 File Offset: 0x0000AA74
		public ActiveDirectoryAuthenticationProvider()
			: this(new Func<DeviceCodeResult, Task>(ActiveDirectoryAuthenticationProvider.DefaultDeviceFlowCallback), null)
		{
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0000C889 File Offset: 0x0000AA89
		public ActiveDirectoryAuthenticationProvider(string applicationClientId)
			: this(new Func<DeviceCodeResult, Task>(ActiveDirectoryAuthenticationProvider.DefaultDeviceFlowCallback), applicationClientId)
		{
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0000C8A0 File Offset: 0x0000AAA0
		public ActiveDirectoryAuthenticationProvider(Func<DeviceCodeResult, Task> deviceCodeFlowCallbackMethod, string applicationClientId = null)
		{
			if (applicationClientId != null)
			{
				this._applicationClientId = applicationClientId;
			}
			this.SetDeviceCodeFlowCallback(deviceCodeFlowCallbackMethod);
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0000C8EF File Offset: 0x0000AAEF
		public static void ClearUserTokenCache()
		{
			if (!ActiveDirectoryAuthenticationProvider.s_pcaMap.IsEmpty)
			{
				ActiveDirectoryAuthenticationProvider.s_pcaMap.Clear();
			}
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0000C907 File Offset: 0x0000AB07
		public void SetDeviceCodeFlowCallback(Func<DeviceCodeResult, Task> deviceCodeFlowCallbackMethod)
		{
			this._deviceCodeFlowCallback = deviceCodeFlowCallbackMethod;
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0000C910 File Offset: 0x0000AB10
		public void SetAcquireAuthorizationCodeAsyncCallback(Func<Uri, Uri, CancellationToken, Task<Uri>> acquireAuthorizationCodeAsyncCallback)
		{
			this._customWebUI = new ActiveDirectoryAuthenticationProvider.CustomWebUi(acquireAuthorizationCodeAsyncCallback);
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0000C91E File Offset: 0x0000AB1E
		public override bool IsSupported(SqlAuthenticationMethod authentication)
		{
			return authentication == SqlAuthenticationMethod.ActiveDirectoryIntegrated || authentication == SqlAuthenticationMethod.ActiveDirectoryPassword || authentication == SqlAuthenticationMethod.ActiveDirectoryInteractive || authentication == SqlAuthenticationMethod.ActiveDirectoryServicePrincipal || authentication == SqlAuthenticationMethod.ActiveDirectoryDeviceCodeFlow || authentication == SqlAuthenticationMethod.ActiveDirectoryManagedIdentity || authentication == SqlAuthenticationMethod.ActiveDirectoryMSI || authentication == SqlAuthenticationMethod.ActiveDirectoryDefault;
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0000C943 File Offset: 0x0000AB43
		public override void BeforeLoad(SqlAuthenticationMethod authentication)
		{
			this._logger.LogInfo(this._type, "BeforeLoad", string.Format("being loaded into SqlAuthProviders for {0}.", authentication));
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0000C96B File Offset: 0x0000AB6B
		public override void BeforeUnload(SqlAuthenticationMethod authentication)
		{
			this._logger.LogInfo(this._type, "BeforeUnload", string.Format("being unloaded from SqlAuthProviders for {0}.", authentication));
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0000C993 File Offset: 0x0000AB93
		public void SetIWin32WindowFunc(Func<IWin32Window> iWin32WindowFunc)
		{
			this._iWin32WindowFunc = iWin32WindowFunc;
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0000C99C File Offset: 0x0000AB9C
		public override async Task<SqlAuthenticationToken> AcquireTokenAsync(SqlAuthenticationParameters parameters)
		{
			CancellationTokenSource cts = new CancellationTokenSource();
			cts.CancelAfter(parameters.ConnectionTimeout * 1000);
			string text = (parameters.Resource.EndsWith(ActiveDirectoryAuthenticationProvider.s_defaultScopeSuffix) ? parameters.Resource : (parameters.Resource + ActiveDirectoryAuthenticationProvider.s_defaultScopeSuffix));
			string[] scopes = new string[] { text };
			TokenRequestContext tokenRequestContext;
			tokenRequestContext..ctor(scopes, null, null, null, false);
			int num = parameters.Authority.LastIndexOf('/');
			string text2 = parameters.Authority.Remove(num + 1);
			string text3 = parameters.Authority.Substring(num + 1);
			string text4 = (string.IsNullOrWhiteSpace(parameters.UserId) ? null : parameters.UserId);
			SqlAuthenticationToken sqlAuthenticationToken;
			if (parameters.AuthenticationMethod == SqlAuthenticationMethod.ActiveDirectoryDefault)
			{
				DefaultAzureCredentialOptions defaultAzureCredentialOptions = new DefaultAzureCredentialOptions
				{
					AuthorityHost = new Uri(text2),
					SharedTokenCacheTenantId = text3,
					VisualStudioCodeTenantId = text3,
					VisualStudioTenantId = text3,
					ExcludeInteractiveBrowserCredential = true
				};
				if (text4 != null)
				{
					defaultAzureCredentialOptions.ManagedIdentityClientId = text4;
					defaultAzureCredentialOptions.SharedTokenCacheUsername = text4;
				}
				AccessToken accessToken = await new DefaultAzureCredential(defaultAzureCredentialOptions).GetTokenAsync(tokenRequestContext, cts.Token).ConfigureAwait(false);
				AccessToken accessToken2 = accessToken;
				SqlClientEventSource.Log.TryTraceEvent<DateTimeOffset>("AcquireTokenAsync | Acquired access token for Default auth mode. Expiry Time: {0}", accessToken2.ExpiresOn);
				sqlAuthenticationToken = new SqlAuthenticationToken(accessToken2.Token, accessToken2.ExpiresOn);
			}
			else
			{
				TokenCredentialOptions tokenCredentialOptions = new TokenCredentialOptions
				{
					AuthorityHost = new Uri(text2)
				};
				if (parameters.AuthenticationMethod == SqlAuthenticationMethod.ActiveDirectoryManagedIdentity || parameters.AuthenticationMethod == SqlAuthenticationMethod.ActiveDirectoryMSI)
				{
					AccessToken accessToken3 = await new ManagedIdentityCredential(text4, tokenCredentialOptions).GetTokenAsync(tokenRequestContext, cts.Token).ConfigureAwait(false);
					SqlClientEventSource.Log.TryTraceEvent<DateTimeOffset>("AcquireTokenAsync | Acquired access token for Managed Identity auth mode. Expiry Time: {0}", accessToken3.ExpiresOn);
					sqlAuthenticationToken = new SqlAuthenticationToken(accessToken3.Token, accessToken3.ExpiresOn);
				}
				else
				{
					AuthenticationResult result = null;
					if (parameters.AuthenticationMethod == SqlAuthenticationMethod.ActiveDirectoryServicePrincipal)
					{
						AccessToken accessToken4 = await new ClientSecretCredential(text3, parameters.UserId, parameters.Password, tokenCredentialOptions).GetTokenAsync(tokenRequestContext, cts.Token).ConfigureAwait(false);
						SqlClientEventSource.Log.TryTraceEvent<DateTimeOffset>("AcquireTokenAsync | Acquired access token for Active Directory Service Principal auth mode. Expiry Time: {0}", accessToken4.ExpiresOn);
						sqlAuthenticationToken = new SqlAuthenticationToken(accessToken4.Token, accessToken4.ExpiresOn);
					}
					else
					{
						string text5 = ActiveDirectoryAuthenticationProvider.s_nativeClientRedirectUri;
						ActiveDirectoryAuthenticationProvider.PublicClientAppKey publicClientAppKey = new ActiveDirectoryAuthenticationProvider.PublicClientAppKey(parameters.Authority, text5, this._applicationClientId, this._iWin32WindowFunc);
						IPublicClientApplication app = this.GetPublicClientAppInstance(publicClientAppKey);
						if (parameters.AuthenticationMethod == SqlAuthenticationMethod.ActiveDirectoryIntegrated)
						{
							result = await ActiveDirectoryAuthenticationProvider.TryAcquireTokenSilent(app, parameters, scopes, cts).ConfigureAwait(false);
							if (result == null)
							{
								if (!string.IsNullOrEmpty(parameters.UserId))
								{
									result = await app.AcquireTokenByIntegratedWindowsAuth(scopes).WithCorrelationId(parameters.ConnectionId).WithUsername(parameters.UserId)
										.ExecuteAsync(cts.Token)
										.ConfigureAwait(false);
								}
								else
								{
									result = await app.AcquireTokenByIntegratedWindowsAuth(scopes).WithCorrelationId(parameters.ConnectionId).ExecuteAsync(cts.Token)
										.ConfigureAwait(false);
								}
								SqlClientEventSource log = SqlClientEventSource.Log;
								string text6 = "AcquireTokenAsync | Acquired access token for Active Directory Integrated auth mode. Expiry Time: {0}";
								AuthenticationResult authenticationResult = result;
								log.TryTraceEvent<DateTimeOffset?>(text6, (authenticationResult != null) ? new DateTimeOffset?(authenticationResult.ExpiresOn) : null);
							}
						}
						else if (parameters.AuthenticationMethod == SqlAuthenticationMethod.ActiveDirectoryPassword)
						{
							string pwCacheKey = ActiveDirectoryAuthenticationProvider.GetAccountPwCacheKey(parameters);
							object obj = ActiveDirectoryAuthenticationProvider.s_accountPwCache.Get(pwCacheKey, null);
							byte[] hash = ActiveDirectoryAuthenticationProvider.GetHash(parameters.Password);
							if (obj != null)
							{
								byte[] array = obj as byte[];
								if (array != null && hash.SequenceEqual(array))
								{
									result = await ActiveDirectoryAuthenticationProvider.TryAcquireTokenSilent(app, parameters, scopes, cts).ConfigureAwait(false);
								}
							}
							if (result == null)
							{
								result = await app.AcquireTokenByUsernamePassword(scopes, parameters.UserId, parameters.Password).WithCorrelationId(parameters.ConnectionId).ExecuteAsync(cts.Token)
									.ConfigureAwait(false);
								if (!ActiveDirectoryAuthenticationProvider.s_accountPwCache.Add(pwCacheKey, ActiveDirectoryAuthenticationProvider.GetHash(parameters.Password), DateTime.UtcNow.AddHours((double)ActiveDirectoryAuthenticationProvider.s_accountPwCacheTtlInHours), null))
								{
									ActiveDirectoryAuthenticationProvider.s_accountPwCache.Remove(pwCacheKey, null);
									ActiveDirectoryAuthenticationProvider.s_accountPwCache.Add(pwCacheKey, ActiveDirectoryAuthenticationProvider.GetHash(parameters.Password), DateTime.UtcNow.AddHours((double)ActiveDirectoryAuthenticationProvider.s_accountPwCacheTtlInHours), null);
								}
								SqlClientEventSource log2 = SqlClientEventSource.Log;
								string text7 = "AcquireTokenAsync | Acquired access token for Active Directory Password auth mode. Expiry Time: {0}";
								AuthenticationResult authenticationResult2 = result;
								log2.TryTraceEvent<DateTimeOffset?>(text7, (authenticationResult2 != null) ? new DateTimeOffset?(authenticationResult2.ExpiresOn) : null);
							}
							pwCacheKey = null;
						}
						else
						{
							if (parameters.AuthenticationMethod != SqlAuthenticationMethod.ActiveDirectoryInteractive && parameters.AuthenticationMethod != SqlAuthenticationMethod.ActiveDirectoryDeviceCodeFlow)
							{
								SqlClientEventSource.Log.TryTraceEvent<SqlAuthenticationMethod>("AcquireTokenAsync | {0} authentication mode not supported by ActiveDirectoryAuthenticationProvider class.", parameters.AuthenticationMethod);
								throw SQL.UnsupportedAuthenticationSpecified(parameters.AuthenticationMethod);
							}
							int num2 = 0;
							try
							{
								result = await ActiveDirectoryAuthenticationProvider.TryAcquireTokenSilent(app, parameters, scopes, cts).ConfigureAwait(false);
								SqlClientEventSource log3 = SqlClientEventSource.Log;
								string text8 = "AcquireTokenAsync | Acquired access token (silent) for {0} auth mode. Expiry Time: {1}";
								SqlAuthenticationMethod authenticationMethod = parameters.AuthenticationMethod;
								AuthenticationResult authenticationResult3 = result;
								log3.TryTraceEvent<SqlAuthenticationMethod, DateTimeOffset?>(text8, authenticationMethod, (authenticationResult3 != null) ? new DateTimeOffset?(authenticationResult3.ExpiresOn) : null);
							}
							catch (MsalUiRequiredException ex)
							{
								num2 = 1;
							}
							if (num2 == 1)
							{
								result = await ActiveDirectoryAuthenticationProvider.AcquireTokenInteractiveDeviceFlowAsync(app, scopes, parameters.ConnectionId, parameters.UserId, parameters.AuthenticationMethod, cts, this._customWebUI, this._deviceCodeFlowCallback).ConfigureAwait(false);
								SqlClientEventSource log4 = SqlClientEventSource.Log;
								string text9 = "AcquireTokenAsync | Acquired access token (interactive) for {0} auth mode. Expiry Time: {1}";
								SqlAuthenticationMethod authenticationMethod2 = parameters.AuthenticationMethod;
								AuthenticationResult authenticationResult4 = result;
								log4.TryTraceEvent<SqlAuthenticationMethod, DateTimeOffset?>(text9, authenticationMethod2, (authenticationResult4 != null) ? new DateTimeOffset?(authenticationResult4.ExpiresOn) : null);
							}
							if (result == null)
							{
								result = await ActiveDirectoryAuthenticationProvider.AcquireTokenInteractiveDeviceFlowAsync(app, scopes, parameters.ConnectionId, parameters.UserId, parameters.AuthenticationMethod, cts, this._customWebUI, this._deviceCodeFlowCallback).ConfigureAwait(false);
								SqlClientEventSource log5 = SqlClientEventSource.Log;
								string text10 = "AcquireTokenAsync | Acquired access token (interactive) for {0} auth mode. Expiry Time: {1}";
								SqlAuthenticationMethod authenticationMethod3 = parameters.AuthenticationMethod;
								AuthenticationResult authenticationResult5 = result;
								log5.TryTraceEvent<SqlAuthenticationMethod, DateTimeOffset?>(text10, authenticationMethod3, (authenticationResult5 != null) ? new DateTimeOffset?(authenticationResult5.ExpiresOn) : null);
							}
						}
						sqlAuthenticationToken = new SqlAuthenticationToken(result.AccessToken, result.ExpiresOn);
					}
				}
			}
			return sqlAuthenticationToken;
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0000C9E8 File Offset: 0x0000ABE8
		private static async Task<AuthenticationResult> TryAcquireTokenSilent(IPublicClientApplication app, SqlAuthenticationParameters parameters, string[] scopes, CancellationTokenSource cts)
		{
			AuthenticationResult result = null;
			IEnumerable<IAccount> enumerable = await app.GetAccountsAsync().ConfigureAwait(false);
			IEnumerator<IAccount> enumerator = enumerable.GetEnumerator();
			IAccount account = null;
			if (enumerator.MoveNext())
			{
				if (!string.IsNullOrEmpty(parameters.UserId))
				{
					IAccount account2;
					for (;;)
					{
						account2 = enumerator.Current;
						if (string.Compare(parameters.UserId, account2.Username, StringComparison.InvariantCultureIgnoreCase) == 0)
						{
							break;
						}
						if (!enumerator.MoveNext())
						{
							goto Block_3;
						}
					}
					account = account2;
					Block_3:;
				}
				else
				{
					account = enumerator.Current;
				}
			}
			if (account != null)
			{
				result = await app.AcquireTokenSilent(scopes, account).ExecuteAsync(cts.Token).ConfigureAwait(false);
				SqlClientEventSource log = SqlClientEventSource.Log;
				string text = "AcquireTokenAsync | Acquired access token (silent) for {0} auth mode. Expiry Time: {1}";
				SqlAuthenticationMethod authenticationMethod = parameters.AuthenticationMethod;
				AuthenticationResult authenticationResult = result;
				log.TryTraceEvent<SqlAuthenticationMethod, DateTimeOffset?>(text, authenticationMethod, (authenticationResult != null) ? new DateTimeOffset?(authenticationResult.ExpiresOn) : null);
			}
			return result;
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0000CA44 File Offset: 0x0000AC44
		private static async Task<AuthenticationResult> AcquireTokenInteractiveDeviceFlowAsync(IPublicClientApplication app, string[] scopes, Guid connectionId, string userId, SqlAuthenticationMethod authenticationMethod, CancellationTokenSource cts, ICustomWebUi customWebUI, Func<DeviceCodeResult, Task> deviceCodeFlowCallback)
		{
			AuthenticationResult authenticationResult2;
			try
			{
				if (authenticationMethod == SqlAuthenticationMethod.ActiveDirectoryInteractive)
				{
					CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
					if (customWebUI != null)
					{
						AuthenticationResult authenticationResult = await AcquireTokenInteractiveParameterBuilderExtensions.WithCustomWebUi(app.AcquireTokenInteractive(scopes).WithCorrelationId(connectionId), customWebUI).WithLoginHint(userId).ExecuteAsync(cancellationTokenSource.Token)
							.ConfigureAwait(false);
						authenticationResult2 = authenticationResult;
					}
					else
					{
						authenticationResult2 = await app.AcquireTokenInteractive(scopes).WithCorrelationId(connectionId).WithLoginHint(userId)
							.ExecuteAsync(cancellationTokenSource.Token)
							.ConfigureAwait(false);
					}
				}
				else
				{
					authenticationResult2 = await app.AcquireTokenWithDeviceCode(scopes, (DeviceCodeResult deviceCodeResult) => deviceCodeFlowCallback(deviceCodeResult)).WithCorrelationId(connectionId).ExecuteAsync(cts.Token)
						.ConfigureAwait(false);
				}
			}
			catch (OperationCanceledException)
			{
				SqlClientEventSource.Log.TryTraceEvent("AcquireTokenInteractiveDeviceFlowAsync | Operation timed out while acquiring access token.");
				throw (authenticationMethod == SqlAuthenticationMethod.ActiveDirectoryInteractive) ? SQL.ActiveDirectoryInteractiveTimeout() : SQL.ActiveDirectoryDeviceFlowTimeout();
			}
			return authenticationResult2;
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0000CAC3 File Offset: 0x0000ACC3
		private static Task DefaultDeviceFlowCallback(DeviceCodeResult result)
		{
			SqlClientEventSource.Log.TryTraceEvent<string>("AcquireTokenInteractiveDeviceFlowAsync | Callback triggered with Device Code Result: {0}", result.Message);
			Console.WriteLine(result.Message);
			return Task.FromResult<int>(0);
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0000CAEC File Offset: 0x0000ACEC
		private IPublicClientApplication GetPublicClientAppInstance(ActiveDirectoryAuthenticationProvider.PublicClientAppKey publicClientAppKey)
		{
			IPublicClientApplication publicClientApplication;
			if (!ActiveDirectoryAuthenticationProvider.s_pcaMap.TryGetValue(publicClientAppKey, out publicClientApplication))
			{
				publicClientApplication = this.CreateClientAppInstance(publicClientAppKey);
				ActiveDirectoryAuthenticationProvider.s_pcaMap.TryAdd(publicClientAppKey, publicClientApplication);
			}
			return publicClientApplication;
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0000CB1E File Offset: 0x0000AD1E
		private static string GetAccountPwCacheKey(SqlAuthenticationParameters parameters)
		{
			return parameters.Authority + "+" + parameters.UserId;
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0000CB38 File Offset: 0x0000AD38
		private static byte[] GetHash(string input)
		{
			byte[] bytes = Encoding.Unicode.GetBytes(input);
			SHA256 sha = SHA256.Create();
			return sha.ComputeHash(bytes);
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0000CB60 File Offset: 0x0000AD60
		private IPublicClientApplication CreateClientAppInstance(ActiveDirectoryAuthenticationProvider.PublicClientAppKey publicClientAppKey)
		{
			IPublicClientApplication publicClientApplication;
			if (this._iWin32WindowFunc != null)
			{
				publicClientApplication = PublicClientApplicationBuilder.Create(publicClientAppKey._applicationClientId).WithAuthority(publicClientAppKey._authority, true).WithClientName("Framework Microsoft SqlClient Data Provider")
					.WithClientVersion(ADP.GetAssemblyVersion().ToString())
					.WithRedirectUri(publicClientAppKey._redirectUri)
					.WithParentActivityOrWindow(this._iWin32WindowFunc)
					.Build();
			}
			else
			{
				publicClientApplication = PublicClientApplicationBuilder.Create(publicClientAppKey._applicationClientId).WithAuthority(publicClientAppKey._authority, true).WithClientName("Framework Microsoft SqlClient Data Provider")
					.WithClientVersion(ADP.GetAssemblyVersion().ToString())
					.WithRedirectUri(publicClientAppKey._redirectUri)
					.Build();
			}
			return publicClientApplication;
		}

		// Token: 0x04000063 RID: 99
		private static ConcurrentDictionary<ActiveDirectoryAuthenticationProvider.PublicClientAppKey, IPublicClientApplication> s_pcaMap = new ConcurrentDictionary<ActiveDirectoryAuthenticationProvider.PublicClientAppKey, IPublicClientApplication>();

		// Token: 0x04000064 RID: 100
		private static readonly MemoryCache s_accountPwCache = new MemoryCache("ActiveDirectoryAuthenticationProvider", null);

		// Token: 0x04000065 RID: 101
		private static readonly int s_accountPwCacheTtlInHours = 2;

		// Token: 0x04000066 RID: 102
		private static readonly string s_nativeClientRedirectUri = "https://login.microsoftonline.com/common/oauth2/nativeclient";

		// Token: 0x04000067 RID: 103
		private static readonly string s_defaultScopeSuffix = "/.default";

		// Token: 0x04000068 RID: 104
		private readonly string _type = typeof(ActiveDirectoryAuthenticationProvider).Name;

		// Token: 0x04000069 RID: 105
		private readonly SqlClientLogger _logger = new SqlClientLogger();

		// Token: 0x0400006A RID: 106
		private Func<DeviceCodeResult, Task> _deviceCodeFlowCallback;

		// Token: 0x0400006B RID: 107
		private ICustomWebUi _customWebUI;

		// Token: 0x0400006C RID: 108
		private readonly string _applicationClientId = "2fd908ad-0664-4344-b9be-cd3e8b574c38";

		// Token: 0x0400006D RID: 109
		private Func<IWin32Window> _iWin32WindowFunc;

		// Token: 0x020001A0 RID: 416
		private class CustomWebUi : ICustomWebUi
		{
			// Token: 0x06001D5B RID: 7515 RVA: 0x0007856F File Offset: 0x0007676F
			internal CustomWebUi(Func<Uri, Uri, CancellationToken, Task<Uri>> acquireAuthorizationCodeAsyncCallback)
			{
				this._acquireAuthorizationCodeAsyncCallback = acquireAuthorizationCodeAsyncCallback;
			}

			// Token: 0x06001D5C RID: 7516 RVA: 0x0007857E File Offset: 0x0007677E
			public Task<Uri> AcquireAuthorizationCodeAsync(Uri authorizationUri, Uri redirectUri, CancellationToken cancellationToken)
			{
				return this._acquireAuthorizationCodeAsyncCallback(authorizationUri, redirectUri, cancellationToken);
			}

			// Token: 0x04001287 RID: 4743
			private readonly Func<Uri, Uri, CancellationToken, Task<Uri>> _acquireAuthorizationCodeAsyncCallback;
		}

		// Token: 0x020001A1 RID: 417
		internal class PublicClientAppKey
		{
			// Token: 0x06001D5D RID: 7517 RVA: 0x0007858E File Offset: 0x0007678E
			public PublicClientAppKey(string authority, string redirectUri, string applicationClientId, Func<IWin32Window> iWin32WindowFunc)
			{
				this._authority = authority;
				this._redirectUri = redirectUri;
				this._applicationClientId = applicationClientId;
				this._iWin32WindowFunc = iWin32WindowFunc;
			}

			// Token: 0x06001D5E RID: 7518 RVA: 0x000785B4 File Offset: 0x000767B4
			public override bool Equals(object obj)
			{
				if (obj != null)
				{
					ActiveDirectoryAuthenticationProvider.PublicClientAppKey publicClientAppKey = obj as ActiveDirectoryAuthenticationProvider.PublicClientAppKey;
					if (publicClientAppKey != null)
					{
						return string.CompareOrdinal(this._authority, publicClientAppKey._authority) == 0 && string.CompareOrdinal(this._redirectUri, publicClientAppKey._redirectUri) == 0 && string.CompareOrdinal(this._applicationClientId, publicClientAppKey._applicationClientId) == 0 && publicClientAppKey._iWin32WindowFunc == this._iWin32WindowFunc;
					}
				}
				return false;
			}

			// Token: 0x06001D5F RID: 7519 RVA: 0x0007861C File Offset: 0x0007681C
			public override int GetHashCode()
			{
				return Tuple.Create<string, string, string, Func<IWin32Window>>(this._authority, this._redirectUri, this._applicationClientId, this._iWin32WindowFunc).GetHashCode();
			}

			// Token: 0x04001288 RID: 4744
			public readonly string _authority;

			// Token: 0x04001289 RID: 4745
			public readonly string _redirectUri;

			// Token: 0x0400128A RID: 4746
			public readonly string _applicationClientId;

			// Token: 0x0400128B RID: 4747
			public readonly Func<IWin32Window> _iWin32WindowFunc;
		}
	}
}
