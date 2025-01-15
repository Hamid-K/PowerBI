using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000154 RID: 340
	public interface IPublicClientApplication : IClientApplicationBase, IApplicationBase
	{
		// Token: 0x17000370 RID: 880
		// (get) Token: 0x060010D4 RID: 4308
		bool IsSystemWebViewAvailable { get; }

		// Token: 0x060010D5 RID: 4309
		AcquireTokenInteractiveParameterBuilder AcquireTokenInteractive(IEnumerable<string> scopes);

		// Token: 0x060010D6 RID: 4310
		AcquireTokenWithDeviceCodeParameterBuilder AcquireTokenWithDeviceCode(IEnumerable<string> scopes, Func<DeviceCodeResult, Task> deviceCodeResultCallback);

		// Token: 0x060010D7 RID: 4311
		AcquireTokenByIntegratedWindowsAuthParameterBuilder AcquireTokenByIntegratedWindowsAuth(IEnumerable<string> scopes);

		// Token: 0x060010D8 RID: 4312
		[Obsolete("Using SecureString is not recommended. Use AcquireTokenByUsernamePassword(IEnumerable<string> scopes, string username, string password) instead.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		AcquireTokenByUsernamePasswordParameterBuilder AcquireTokenByUsernamePassword(IEnumerable<string> scopes, string username, SecureString password);

		// Token: 0x060010D9 RID: 4313
		AcquireTokenByUsernamePasswordParameterBuilder AcquireTokenByUsernamePassword(IEnumerable<string> scopes, string username, string password);

		// Token: 0x060010DA RID: 4314
		[Obsolete("Use AcquireTokenInteractive instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<string> scopes);

		// Token: 0x060010DB RID: 4315
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenInteractive instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<string> scopes, string loginHint);

		// Token: 0x060010DC RID: 4316
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenInteractive instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<string> scopes, IAccount account);

		// Token: 0x060010DD RID: 4317
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenInteractive instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<string> scopes, string loginHint, Prompt prompt, string extraQueryParameters);

		// Token: 0x060010DE RID: 4318
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenInteractive instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<string> scopes, IAccount account, Prompt prompt, string extraQueryParameters);

		// Token: 0x060010DF RID: 4319
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenInteractive instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<string> scopes, string loginHint, Prompt prompt, string extraQueryParameters, IEnumerable<string> extraScopesToConsent, string authority);

		// Token: 0x060010E0 RID: 4320
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenInteractive instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<string> scopes, IAccount account, Prompt prompt, string extraQueryParameters, IEnumerable<string> extraScopesToConsent, string authority);

		// Token: 0x060010E1 RID: 4321
		[Obsolete("Use AcquireTokenInteractive instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<string> scopes, UIParent parent);

		// Token: 0x060010E2 RID: 4322
		[Obsolete("Use AcquireTokenInteractive instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<string> scopes, string loginHint, UIParent parent);

		// Token: 0x060010E3 RID: 4323
		[Obsolete("Use AcquireTokenInteractive instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<string> scopes, IAccount account, UIParent parent);

		// Token: 0x060010E4 RID: 4324
		[Obsolete("Use AcquireTokenInteractive instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<string> scopes, string loginHint, Prompt prompt, string extraQueryParameters, UIParent parent);

		// Token: 0x060010E5 RID: 4325
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenInteractive instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<string> scopes, IAccount account, Prompt prompt, string extraQueryParameters, UIParent parent);

		// Token: 0x060010E6 RID: 4326
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenInteractive instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<string> scopes, string loginHint, Prompt prompt, string extraQueryParameters, IEnumerable<string> extraScopesToConsent, string authority, UIParent parent);

		// Token: 0x060010E7 RID: 4327
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenInteractive instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<string> scopes, IAccount account, Prompt prompt, string extraQueryParameters, IEnumerable<string> extraScopesToConsent, string authority, UIParent parent);

		// Token: 0x060010E8 RID: 4328
		[Obsolete("Use AcquireTokenByUsernamePassword instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		Task<AuthenticationResult> AcquireTokenByUsernamePasswordAsync(IEnumerable<string> scopes, string username, SecureString securePassword);

		// Token: 0x060010E9 RID: 4329
		[Obsolete("Use AcquireTokenWithDeviceCode instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		Task<AuthenticationResult> AcquireTokenWithDeviceCodeAsync(IEnumerable<string> scopes, Func<DeviceCodeResult, Task> deviceCodeResultCallback);

		// Token: 0x060010EA RID: 4330
		[Obsolete("Use AcquireTokenWithDeviceCode instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		Task<AuthenticationResult> AcquireTokenWithDeviceCodeAsync(IEnumerable<string> scopes, string extraQueryParameters, Func<DeviceCodeResult, Task> deviceCodeResultCallback);

		// Token: 0x060010EB RID: 4331
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenWithDeviceCode instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		Task<AuthenticationResult> AcquireTokenWithDeviceCodeAsync(IEnumerable<string> scopes, Func<DeviceCodeResult, Task> deviceCodeResultCallback, CancellationToken cancellationToken);

		// Token: 0x060010EC RID: 4332
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenWithDeviceCode instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		Task<AuthenticationResult> AcquireTokenWithDeviceCodeAsync(IEnumerable<string> scopes, string extraQueryParameters, Func<DeviceCodeResult, Task> deviceCodeResultCallback, CancellationToken cancellationToken);

		// Token: 0x060010ED RID: 4333
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenByIntegratedWindowsAuth instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		Task<AuthenticationResult> AcquireTokenByIntegratedWindowsAuthAsync(IEnumerable<string> scopes);

		// Token: 0x060010EE RID: 4334
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenByIntegratedWindowsAuth instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		Task<AuthenticationResult> AcquireTokenByIntegratedWindowsAuthAsync(IEnumerable<string> scopes, string username);
	}
}
