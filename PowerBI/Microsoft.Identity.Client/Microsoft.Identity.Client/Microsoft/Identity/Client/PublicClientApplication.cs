using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Executors;
using Microsoft.Identity.Client.Instance;
using Microsoft.Identity.Client.Internal.Broker;
using Microsoft.Identity.Client.PlatformsCommon.Factories;
using Microsoft.Identity.Client.WsTrust;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000161 RID: 353
	public sealed class PublicClientApplication : ClientApplicationBase, IPublicClientApplication, IClientApplicationBase, IApplicationBase, IByRefreshToken
	{
		// Token: 0x0600114E RID: 4430 RVA: 0x0003BB36 File Offset: 0x00039D36
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use PublicClientApplicationBuilder instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		public PublicClientApplication(string clientId)
			: this(clientId, "https://login.microsoftonline.com/common/")
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x0003BB49 File Offset: 0x00039D49
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use PublicClientApplicationBuilder instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		public PublicClientApplication(string clientId, string authority)
			: base(PublicClientApplicationBuilder.Create(clientId).WithRedirectUri(PlatformProxyFactory.CreatePlatformProxy(null).GetDefaultRedirectUri(clientId, false)).WithAuthority(new Uri(authority), true)
				.BuildConfiguration())
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x0003BB7F File Offset: 0x00039D7F
		[Obsolete("Use AcquireTokenInteractive instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<string> scopes)
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x0003BB86 File Offset: 0x00039D86
		[Obsolete("Use AcquireTokenInteractive instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<string> scopes, string loginHint)
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x0003BB8D File Offset: 0x00039D8D
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenInteractive instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		public Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<string> scopes, IAccount account)
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x0003BB94 File Offset: 0x00039D94
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenInteractive instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		public Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<string> scopes, string loginHint, Prompt prompt, string extraQueryParameters)
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x0003BB9B File Offset: 0x00039D9B
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenInteractive instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		public Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<string> scopes, IAccount account, Prompt prompt, string extraQueryParameters)
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x0003BBA2 File Offset: 0x00039DA2
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenInteractive instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		public Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<string> scopes, string loginHint, Prompt prompt, string extraQueryParameters, IEnumerable<string> extraScopesToConsent, string authority)
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x0003BBA9 File Offset: 0x00039DA9
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenInteractive instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		public Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<string> scopes, IAccount account, Prompt prompt, string extraQueryParameters, IEnumerable<string> extraScopesToConsent, string authority)
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x0003BBB0 File Offset: 0x00039DB0
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenInteractive instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		public Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<string> scopes, UIParent parent)
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x0003BBB7 File Offset: 0x00039DB7
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenInteractive instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		public Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<string> scopes, string loginHint, UIParent parent)
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x0003BBBE File Offset: 0x00039DBE
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenInteractive instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		public Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<string> scopes, IAccount account, UIParent parent)
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x0003BBC5 File Offset: 0x00039DC5
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenInteractive instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		public Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<string> scopes, string loginHint, Prompt prompt, string extraQueryParameters, UIParent parent)
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x0003BBCC File Offset: 0x00039DCC
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenInteractive instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		public Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<string> scopes, IAccount account, Prompt prompt, string extraQueryParameters, UIParent parent)
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x0003BBD3 File Offset: 0x00039DD3
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenInteractive instead. See https://aka.ms/msal-net-3-breaking-changes. ")]
		public Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<string> scopes, string loginHint, Prompt prompt, string extraQueryParameters, IEnumerable<string> extraScopesToConsent, string authority, UIParent parent)
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x0003BBDA File Offset: 0x00039DDA
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenInteractive instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		public Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<string> scopes, IAccount account, Prompt prompt, string extraQueryParameters, IEnumerable<string> extraScopesToConsent, string authority, UIParent parent)
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x0003BBE1 File Offset: 0x00039DE1
		[Obsolete("Use AcquireTokenByUsernamePassword instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Task<AuthenticationResult> AcquireTokenByUsernamePasswordAsync(IEnumerable<string> scopes, string username, SecureString securePassword)
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x0003BBE8 File Offset: 0x00039DE8
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenWithDeviceCode instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		public Task<AuthenticationResult> AcquireTokenWithDeviceCodeAsync(IEnumerable<string> scopes, Func<DeviceCodeResult, Task> deviceCodeResultCallback)
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x0003BBEF File Offset: 0x00039DEF
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenWithDeviceCode instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		public Task<AuthenticationResult> AcquireTokenWithDeviceCodeAsync(IEnumerable<string> scopes, string extraQueryParameters, Func<DeviceCodeResult, Task> deviceCodeResultCallback)
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x0003BBF6 File Offset: 0x00039DF6
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenWithDeviceCode instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		public Task<AuthenticationResult> AcquireTokenWithDeviceCodeAsync(IEnumerable<string> scopes, Func<DeviceCodeResult, Task> deviceCodeResultCallback, CancellationToken cancellationToken)
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x0003BBFD File Offset: 0x00039DFD
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenWithDeviceCode instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		public Task<AuthenticationResult> AcquireTokenWithDeviceCodeAsync(IEnumerable<string> scopes, string extraQueryParameters, Func<DeviceCodeResult, Task> deviceCodeResultCallback, CancellationToken cancellationToken)
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x0003BC04 File Offset: 0x00039E04
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenByRefreshToken instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		Task<AuthenticationResult> IByRefreshToken.AcquireTokenByRefreshTokenAsync(IEnumerable<string> scopes, string refreshToken)
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x0003BC0B File Offset: 0x00039E0B
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenByIntegratedWindowsAuth instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		public Task<AuthenticationResult> AcquireTokenByIntegratedWindowsAuthAsync(IEnumerable<string> scopes)
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x0003BC12 File Offset: 0x00039E12
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenByIntegratedWindowsAuth instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		public Task<AuthenticationResult> AcquireTokenByIntegratedWindowsAuthAsync(IEnumerable<string> scopes, string username)
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x0003BC19 File Offset: 0x00039E19
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use PublicClientApplicationBuilder instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		public PublicClientApplication(string clientId, string authority, TokenCache userTokenCache)
			: this(PublicClientApplicationBuilder.Create(clientId).WithAuthority(new Uri(authority), true).BuildConfiguration())
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x0003BC3D File Offset: 0x00039E3D
		internal PublicClientApplication(ApplicationConfiguration configuration)
			: base(configuration)
		{
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06001168 RID: 4456 RVA: 0x0003BC46 File Offset: 0x00039E46
		public static IAccount OperatingSystemAccount
		{
			get
			{
				return PublicClientApplication.s_currentOsAccount;
			}
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x0003BC4D File Offset: 0x00039E4D
		internal static bool IsOperatingSystemAccount(IAccount account)
		{
			string text;
			if (account == null)
			{
				text = null;
			}
			else
			{
				AccountId homeAccountId = account.HomeAccountId;
				text = ((homeAccountId != null) ? homeAccountId.Identifier : null);
			}
			return string.Equals(text, "current_os_account", StringComparison.Ordinal);
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x0600116A RID: 4458 RVA: 0x0003BC72 File Offset: 0x00039E72
		public bool IsSystemWebViewAvailable
		{
			get
			{
				return base.ServiceBundle.PlatformProxy.GetWebUiFactory(base.ServiceBundle.Config).IsSystemWebViewAvailable;
			}
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x0003BC94 File Offset: 0x00039E94
		public bool IsEmbeddedWebViewAvailable()
		{
			return base.ServiceBundle.PlatformProxy.GetWebUiFactory(base.ServiceBundle.Config).IsEmbeddedWebViewAvailable;
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x0003BCB6 File Offset: 0x00039EB6
		public bool IsUserInteractive()
		{
			return base.ServiceBundle.PlatformProxy.GetWebUiFactory(base.ServiceBundle.Config).IsUserInteractive;
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x0003BCD8 File Offset: 0x00039ED8
		public bool IsBrokerAvailable()
		{
			IBroker broker = base.ServiceBundle.PlatformProxy.CreateBroker(base.ServiceBundle.Config, null);
			Authority authority = base.ServiceBundle.Config.Authority;
			AuthorityType? authorityType;
			if (authority == null)
			{
				authorityType = null;
			}
			else
			{
				AuthorityInfo authorityInfo = authority.AuthorityInfo;
				authorityType = ((authorityInfo != null) ? new AuthorityType?(authorityInfo.AuthorityType) : null);
			}
			AuthorityType? authorityType2 = authorityType;
			return broker.IsBrokerInstalledAndInvokable(authorityType2.GetValueOrDefault());
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x0003BD4B File Offset: 0x00039F4B
		[CLSCompliant(false)]
		public AcquireTokenInteractiveParameterBuilder AcquireTokenInteractive(IEnumerable<string> scopes)
		{
			return AcquireTokenInteractiveParameterBuilder.Create(ClientExecutorFactory.CreatePublicClientExecutor(this), scopes).WithParentActivityOrWindowFunc(base.ServiceBundle.Config.ParentActivityOrWindowFunc);
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x0003BD6E File Offset: 0x00039F6E
		public AcquireTokenWithDeviceCodeParameterBuilder AcquireTokenWithDeviceCode(IEnumerable<string> scopes, Func<DeviceCodeResult, Task> deviceCodeResultCallback)
		{
			return AcquireTokenWithDeviceCodeParameterBuilder.Create(ClientExecutorFactory.CreatePublicClientExecutor(this), scopes, deviceCodeResultCallback);
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x0003BD7D File Offset: 0x00039F7D
		AcquireTokenByRefreshTokenParameterBuilder IByRefreshToken.AcquireTokenByRefreshToken(IEnumerable<string> scopes, string refreshToken)
		{
			return AcquireTokenByRefreshTokenParameterBuilder.Create(ClientExecutorFactory.CreateClientApplicationBaseExecutor(this), scopes, refreshToken);
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x0003BD8C File Offset: 0x00039F8C
		public AcquireTokenByIntegratedWindowsAuthParameterBuilder AcquireTokenByIntegratedWindowsAuth(IEnumerable<string> scopes)
		{
			return AcquireTokenByIntegratedWindowsAuthParameterBuilder.Create(ClientExecutorFactory.CreatePublicClientExecutor(this), scopes);
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x0003BD9A File Offset: 0x00039F9A
		[Obsolete("Using SecureString is not recommended. Use AcquireTokenByUsernamePassword(IEnumerable<string> scopes, string username, string password) instead.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public AcquireTokenByUsernamePasswordParameterBuilder AcquireTokenByUsernamePassword(IEnumerable<string> scopes, string username, SecureString password)
		{
			return AcquireTokenByUsernamePasswordParameterBuilder.Create(ClientExecutorFactory.CreatePublicClientExecutor(this), scopes, username, new string(password.PasswordToCharArray()));
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x0003BDB4 File Offset: 0x00039FB4
		public AcquireTokenByUsernamePasswordParameterBuilder AcquireTokenByUsernamePassword(IEnumerable<string> scopes, string username, string password)
		{
			return AcquireTokenByUsernamePasswordParameterBuilder.Create(ClientExecutorFactory.CreatePublicClientExecutor(this), scopes, username, password);
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x0003BDC4 File Offset: 0x00039FC4
		public bool IsProofOfPossessionSupportedByClient()
		{
			if (base.ServiceBundle.Config.IsBrokerEnabled)
			{
				IBroker broker = base.ServiceBundle.PlatformProxy.CreateBroker(base.ServiceBundle.Config, null);
				if (broker.IsBrokerInstalledAndInvokable(base.ServiceBundle.Config.Authority.AuthorityInfo.AuthorityType))
				{
					return broker.IsPopSupported;
				}
			}
			return false;
		}

		// Token: 0x04000545 RID: 1349
		private const string CurrentOSAccountDescriptor = "current_os_account";

		// Token: 0x04000546 RID: 1350
		private static readonly IAccount s_currentOsAccount = new Account("current_os_account", null, null, null, null);
	}
}
