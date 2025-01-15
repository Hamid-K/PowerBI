using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace Azure.Identity
{
	// Token: 0x02000077 RID: 119
	internal class MsalPublicClient : MsalClientBase<IPublicClientApplication>
	{
		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x0000C26B File Offset: 0x0000A46B
		internal string RedirectUrl { get; }

		// Token: 0x0600040E RID: 1038 RVA: 0x0000C273 File Offset: 0x0000A473
		protected MsalPublicClient()
		{
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000C27C File Offset: 0x0000A47C
		public MsalPublicClient(CredentialPipeline pipeline, string tenantId, string clientId, string redirectUrl, TokenCredentialOptions options)
			: base(pipeline, tenantId, clientId, options)
		{
			this.RedirectUrl = redirectUrl;
			IMsalPublicClientInitializerOptions msalPublicClientInitializerOptions = options as IMsalPublicClientInitializerOptions;
			if (msalPublicClientInitializerOptions != null)
			{
				this._beforeBuildClient = msalPublicClientInitializerOptions.BeforeBuildClient;
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000C2B3 File Offset: 0x0000A4B3
		protected override ValueTask<IPublicClientApplication> CreateClientAsync(bool enableCae, bool async, CancellationToken cancellationToken)
		{
			return this.CreateClientCoreAsync(enableCae, async, cancellationToken);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000C2C0 File Offset: 0x0000A4C0
		protected virtual ValueTask<IPublicClientApplication> CreateClientCoreAsync(bool enableCae, bool async, CancellationToken cancellationToken)
		{
			string[] array = (enableCae ? this.cp1Capabilities : Array.Empty<string>());
			Uri uri = new UriBuilder(base.AuthorityHost.Scheme, base.AuthorityHost.Host, base.AuthorityHost.Port, base.TenantId ?? "organizations").Uri;
			BaseAbstractApplicationBuilder<PublicClientApplicationBuilder> baseAbstractApplicationBuilder = PublicClientApplicationBuilder.Create(base.ClientId).WithAuthority(uri, true).WithHttpClientFactory(new HttpPipelineClientFactory(base.Pipeline.HttpPipeline));
			LogCallback logCallback = new LogCallback(base.LogMsal);
			bool? flag = new bool?(base.IsSupportLoggingEnabled);
			PublicClientApplicationBuilder publicClientApplicationBuilder = baseAbstractApplicationBuilder.WithLogging(logCallback, null, flag, null);
			if (!string.IsNullOrEmpty(this.RedirectUrl))
			{
				publicClientApplicationBuilder = publicClientApplicationBuilder.WithRedirectUri(this.RedirectUrl);
			}
			if (array.Length != 0)
			{
				publicClientApplicationBuilder.WithClientCapabilities(array);
			}
			if (this._beforeBuildClient != null)
			{
				this._beforeBuildClient(publicClientApplicationBuilder);
			}
			if (base.DisableInstanceDiscovery)
			{
				publicClientApplicationBuilder.WithInstanceDiscovery(false);
			}
			return new ValueTask<IPublicClientApplication>(publicClientApplicationBuilder.Build());
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000C3CC File Offset: 0x0000A5CC
		public async ValueTask<List<IAccount>> GetAccountsAsync(bool async, bool enableCae, CancellationToken cancellationToken)
		{
			return await this.GetAccountsCoreAsync(async, enableCae, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000C428 File Offset: 0x0000A628
		protected virtual async ValueTask<List<IAccount>> GetAccountsCoreAsync(bool async, bool enableCae, CancellationToken cancellationToken)
		{
			return await MsalPublicClient.GetAccountsAsync(await base.GetClientAsync(enableCae, async, cancellationToken).ConfigureAwait(false), async).ConfigureAwait(false);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000C484 File Offset: 0x0000A684
		public async ValueTask<AuthenticationResult> AcquireTokenSilentAsync(string[] scopes, string claims, IAccount account, string tenantId, bool enableCae, bool async, CancellationToken cancellationToken)
		{
			AuthenticationResult authenticationResult = await this.AcquireTokenSilentCoreAsync(scopes, claims, account, tenantId, enableCae, async, cancellationToken).ConfigureAwait(false);
			base.LogAccountDetails(authenticationResult);
			return authenticationResult;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000C504 File Offset: 0x0000A704
		protected virtual async ValueTask<AuthenticationResult> AcquireTokenSilentCoreAsync(string[] scopes, string claims, IAccount account, string tenantId, bool enableCae, bool async, CancellationToken cancellationToken)
		{
			AcquireTokenSilentParameterBuilder acquireTokenSilentParameterBuilder = (await base.GetClientAsync(enableCae, async, cancellationToken).ConfigureAwait(false)).AcquireTokenSilent(scopes, account);
			if (!string.IsNullOrEmpty(claims))
			{
				acquireTokenSilentParameterBuilder.WithClaims(claims);
			}
			if (tenantId != null)
			{
				acquireTokenSilentParameterBuilder.WithTenantIdFromAuthority(new UriBuilder(base.AuthorityHost)
				{
					Path = (base.TenantId ?? tenantId)
				}.Uri);
			}
			return await acquireTokenSilentParameterBuilder.ExecuteAsync(async, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000C584 File Offset: 0x0000A784
		public async ValueTask<AuthenticationResult> AcquireTokenSilentAsync(string[] scopes, string claims, AuthenticationRecord record, string tenantId, bool enableCae, bool async, CancellationToken cancellationToken)
		{
			AuthenticationResult authenticationResult = await this.AcquireTokenSilentCoreAsync(scopes, claims, record, tenantId, enableCae, async, cancellationToken).ConfigureAwait(false);
			base.LogAccountDetails(authenticationResult);
			return authenticationResult;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000C604 File Offset: 0x0000A804
		protected virtual async ValueTask<AuthenticationResult> AcquireTokenSilentCoreAsync(string[] scopes, string claims, AuthenticationRecord record, string tenantId, bool enableCae, bool async, CancellationToken cancellationToken)
		{
			AcquireTokenSilentParameterBuilder acquireTokenSilentParameterBuilder = (await base.GetClientAsync(enableCae, async, cancellationToken).ConfigureAwait(false)).AcquireTokenSilent(scopes, (AuthenticationAccount)record);
			if (tenantId != null || record.TenantId != null)
			{
				acquireTokenSilentParameterBuilder.WithTenantIdFromAuthority(new UriBuilder(base.AuthorityHost)
				{
					Path = (tenantId ?? record.TenantId)
				}.Uri);
			}
			if (!string.IsNullOrEmpty(claims))
			{
				acquireTokenSilentParameterBuilder.WithClaims(claims);
			}
			return await acquireTokenSilentParameterBuilder.ExecuteAsync(async, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000C684 File Offset: 0x0000A884
		public async ValueTask<AuthenticationResult> AcquireTokenInteractiveAsync(string[] scopes, string claims, Prompt prompt, string loginHint, string tenantId, bool enableCae, BrowserCustomizationOptions browserOptions, bool async, CancellationToken cancellationToken)
		{
			MsalPublicClient.<>c__DisplayClass14_0 CS$<>8__locals1 = new MsalPublicClient.<>c__DisplayClass14_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.scopes = scopes;
			CS$<>8__locals1.claims = claims;
			CS$<>8__locals1.prompt = prompt;
			CS$<>8__locals1.loginHint = loginHint;
			CS$<>8__locals1.tenantId = tenantId;
			CS$<>8__locals1.enableCae = enableCae;
			CS$<>8__locals1.browserOptions = browserOptions;
			CS$<>8__locals1.cancellationToken = cancellationToken;
			AuthenticationResult authenticationResult;
			if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA && !IdentityCompatSwitches.DisableInteractiveBrowserThreadpoolExecution)
			{
				AzureIdentityEventSource.Singleton.InteractiveAuthenticationExecutingOnThreadPool();
				authenticationResult = Task.Run<AuthenticationResult>(delegate
				{
					MsalPublicClient.<>c__DisplayClass14_0.<<AcquireTokenInteractiveAsync>b__0>d <<AcquireTokenInteractiveAsync>b__0>d;
					<<AcquireTokenInteractiveAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder<AuthenticationResult>.Create();
					<<AcquireTokenInteractiveAsync>b__0>d.<>4__this = CS$<>8__locals1;
					<<AcquireTokenInteractiveAsync>b__0>d.<>1__state = -1;
					<<AcquireTokenInteractiveAsync>b__0>d.<>t__builder.Start<MsalPublicClient.<>c__DisplayClass14_0.<<AcquireTokenInteractiveAsync>b__0>d>(ref <<AcquireTokenInteractiveAsync>b__0>d);
					return <<AcquireTokenInteractiveAsync>b__0>d.<>t__builder.Task;
				}).GetAwaiter().GetResult();
			}
			else
			{
				AzureIdentityEventSource.Singleton.InteractiveAuthenticationExecutingInline();
				AuthenticationResult authenticationResult2 = await this.AcquireTokenInteractiveCoreAsync(CS$<>8__locals1.scopes, CS$<>8__locals1.claims, CS$<>8__locals1.prompt, CS$<>8__locals1.loginHint, CS$<>8__locals1.tenantId, CS$<>8__locals1.enableCae, CS$<>8__locals1.browserOptions, async, CS$<>8__locals1.cancellationToken).ConfigureAwait(false);
				base.LogAccountDetails(authenticationResult2);
				authenticationResult = authenticationResult2;
			}
			return authenticationResult;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000C718 File Offset: 0x0000A918
		protected virtual async ValueTask<AuthenticationResult> AcquireTokenInteractiveCoreAsync(string[] scopes, string claims, Prompt prompt, string loginHint, string tenantId, bool enableCae, BrowserCustomizationOptions browserOptions, bool async, CancellationToken cancellationToken)
		{
			AcquireTokenInteractiveParameterBuilder acquireTokenInteractiveParameterBuilder = (await base.GetClientAsync(enableCae, async, cancellationToken).ConfigureAwait(false)).AcquireTokenInteractive(scopes).WithPrompt(prompt);
			if (!string.IsNullOrEmpty(claims))
			{
				acquireTokenInteractiveParameterBuilder.WithClaims(claims);
			}
			if (loginHint != null)
			{
				acquireTokenInteractiveParameterBuilder.WithLoginHint(loginHint);
			}
			if (tenantId != null)
			{
				acquireTokenInteractiveParameterBuilder.WithTenantIdFromAuthority(new UriBuilder(base.AuthorityHost)
				{
					Path = tenantId
				}.Uri);
			}
			if (browserOptions != null)
			{
				if (browserOptions.UseEmbeddedWebView != null)
				{
					acquireTokenInteractiveParameterBuilder.WithUseEmbeddedWebView(browserOptions.UseEmbeddedWebView.Value);
				}
				if (browserOptions.SystemBrowserOptions != null)
				{
					acquireTokenInteractiveParameterBuilder.WithSystemWebViewOptions(browserOptions.SystemBrowserOptions);
				}
			}
			return await acquireTokenInteractiveParameterBuilder.ExecuteAsync(async, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000C7AC File Offset: 0x0000A9AC
		public async ValueTask<AuthenticationResult> AcquireTokenByUsernamePasswordAsync(string[] scopes, string claims, string username, string password, string tenantId, bool enableCae, bool async, CancellationToken cancellationToken)
		{
			AuthenticationResult authenticationResult = await this.AcquireTokenByUsernamePasswordCoreAsync(scopes, claims, username, password, tenantId, enableCae, async, cancellationToken).ConfigureAwait(false);
			base.LogAccountDetails(authenticationResult);
			return authenticationResult;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000C834 File Offset: 0x0000AA34
		protected virtual async ValueTask<AuthenticationResult> AcquireTokenByUsernamePasswordCoreAsync(string[] scopes, string claims, string username, string password, string tenantId, bool enableCae, bool async, CancellationToken cancellationToken)
		{
			AcquireTokenByUsernamePasswordParameterBuilder acquireTokenByUsernamePasswordParameterBuilder = (await base.GetClientAsync(enableCae, async, cancellationToken).ConfigureAwait(false)).AcquireTokenByUsernamePassword(scopes, username, password);
			if (!string.IsNullOrEmpty(claims))
			{
				acquireTokenByUsernamePasswordParameterBuilder.WithClaims(claims);
			}
			if (!string.IsNullOrEmpty(tenantId))
			{
				acquireTokenByUsernamePasswordParameterBuilder.WithTenantIdFromAuthority(new UriBuilder(base.AuthorityHost)
				{
					Path = tenantId
				}.Uri);
			}
			return await acquireTokenByUsernamePasswordParameterBuilder.ExecuteAsync(async, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000C8BC File Offset: 0x0000AABC
		public async ValueTask<AuthenticationResult> AcquireTokenWithDeviceCodeAsync(string[] scopes, string claims, Func<DeviceCodeResult, Task> deviceCodeCallback, bool enableCae, bool async, CancellationToken cancellationToken)
		{
			AuthenticationResult authenticationResult = await this.AcquireTokenWithDeviceCodeCoreAsync(scopes, claims, deviceCodeCallback, enableCae, async, cancellationToken).ConfigureAwait(false);
			base.LogAccountDetails(authenticationResult);
			return authenticationResult;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000C934 File Offset: 0x0000AB34
		protected virtual async ValueTask<AuthenticationResult> AcquireTokenWithDeviceCodeCoreAsync(string[] scopes, string claims, Func<DeviceCodeResult, Task> deviceCodeCallback, bool enableCae, bool async, CancellationToken cancellationToken)
		{
			AcquireTokenWithDeviceCodeParameterBuilder acquireTokenWithDeviceCodeParameterBuilder = (await base.GetClientAsync(enableCae, async, cancellationToken).ConfigureAwait(false)).AcquireTokenWithDeviceCode(scopes, deviceCodeCallback);
			if (!string.IsNullOrEmpty(claims))
			{
				acquireTokenWithDeviceCodeParameterBuilder.WithClaims(claims);
			}
			return await acquireTokenWithDeviceCodeParameterBuilder.ExecuteAsync(async, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000C9AC File Offset: 0x0000ABAC
		public async ValueTask<AuthenticationResult> AcquireTokenByRefreshTokenAsync(string[] scopes, string claims, string refreshToken, AzureCloudInstance azureCloudInstance, string tenant, bool enableCae, bool async, CancellationToken cancellationToken)
		{
			AuthenticationResult authenticationResult = await this.AcquireTokenByRefreshTokenCoreAsync(scopes, claims, refreshToken, azureCloudInstance, tenant, enableCae, async, cancellationToken).ConfigureAwait(false);
			base.LogAccountDetails(authenticationResult);
			return authenticationResult;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000CA34 File Offset: 0x0000AC34
		protected virtual async ValueTask<AuthenticationResult> AcquireTokenByRefreshTokenCoreAsync(string[] scopes, string claims, string refreshToken, AzureCloudInstance azureCloudInstance, string tenant, bool enableCae, bool async, CancellationToken cancellationToken)
		{
			ConfiguredValueTaskAwaitable<IPublicClientApplication>.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter = base.GetClientAsync(enableCae, async, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredValueTaskAwaiter.IsCompleted)
			{
				await configuredValueTaskAwaiter;
				ConfiguredValueTaskAwaitable<IPublicClientApplication>.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2;
				configuredValueTaskAwaiter = configuredValueTaskAwaiter2;
				configuredValueTaskAwaiter2 = default(ConfiguredValueTaskAwaitable<IPublicClientApplication>.ConfiguredValueTaskAwaiter);
			}
			AcquireTokenByRefreshTokenParameterBuilder acquireTokenByRefreshTokenParameterBuilder = ((IByRefreshToken)configuredValueTaskAwaiter.GetResult()).AcquireTokenByRefreshToken(scopes, refreshToken);
			if (!string.IsNullOrEmpty(claims))
			{
				acquireTokenByRefreshTokenParameterBuilder.WithClaims(claims);
			}
			if (!string.IsNullOrEmpty(base.TenantId))
			{
				acquireTokenByRefreshTokenParameterBuilder.WithTenantIdFromAuthority(new UriBuilder(base.AuthorityHost)
				{
					Path = tenant
				}.Uri);
			}
			return await acquireTokenByRefreshTokenParameterBuilder.ExecuteAsync(async, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000CAB4 File Offset: 0x0000ACB4
		private static async ValueTask<List<IAccount>> GetAccountsAsync(IPublicClientApplication client, bool async)
		{
			IEnumerable<IAccount> enumerable;
			if (async)
			{
				enumerable = await client.GetAccountsAsync().ConfigureAwait(false);
			}
			else
			{
				enumerable = client.GetAccountsAsync().GetAwaiter().GetResult();
			}
			return enumerable.ToList<IAccount>();
		}

		// Token: 0x04000257 RID: 599
		private Action<PublicClientApplicationBuilder> _beforeBuildClient;
	}
}
