using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Extensibility;

namespace Azure.Identity
{
	// Token: 0x0200006F RID: 111
	internal class ManagedIdentityClient
	{
		// Token: 0x060003BD RID: 957 RVA: 0x0000B283 File Offset: 0x00009483
		protected ManagedIdentityClient()
		{
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000B28B File Offset: 0x0000948B
		public ManagedIdentityClient(CredentialPipeline pipeline, string clientId = null)
			: this(new ManagedIdentityClientOptions
			{
				Pipeline = pipeline,
				ClientId = clientId
			})
		{
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000B2A6 File Offset: 0x000094A6
		public ManagedIdentityClient(CredentialPipeline pipeline, ResourceIdentifier resourceId)
			: this(new ManagedIdentityClientOptions
			{
				Pipeline = pipeline,
				ResourceIdentifier = resourceId
			})
		{
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000B2C4 File Offset: 0x000094C4
		public ManagedIdentityClient(ManagedIdentityClientOptions options)
		{
			if (options.ClientId != null && options.ResourceIdentifier != null)
			{
				throw new ArgumentException("ManagedIdentityClientOptions cannot specify both ResourceIdentifier and ClientId.");
			}
			this.ClientId = (string.IsNullOrEmpty(options.ClientId) ? null : options.ClientId);
			this.ResourceIdentifier = (string.IsNullOrEmpty(options.ResourceIdentifier) ? null : options.ResourceIdentifier);
			this.Pipeline = options.Pipeline;
			this._identitySource = new Lazy<ManagedIdentitySource>(() => ManagedIdentityClient.SelectManagedIdentitySource(options));
			this._msal = new MsalConfidentialClient(this.Pipeline, "MANAGED-IDENTITY-RESOURCE-TENENT", this.ClientId ?? "SYSTEM-ASSIGNED-MANAGED-IDENTITY", new Func<AppTokenProviderParameters, Task<AppTokenProviderResult>>(this.AppTokenProviderImpl), options.Options);
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000B3C4 File Offset: 0x000095C4
		internal CredentialPipeline Pipeline { get; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0000B3CC File Offset: 0x000095CC
		protected internal string ClientId { get; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000B3D4 File Offset: 0x000095D4
		internal ResourceIdentifier ResourceIdentifier { get; }

		// Token: 0x060003C4 RID: 964 RVA: 0x0000B3DC File Offset: 0x000095DC
		public async ValueTask<AccessToken> AuthenticateAsync(bool async, TokenRequestContext context, CancellationToken cancellationToken)
		{
			AuthenticationResult authenticationResult = await this._msal.AcquireTokenForClientAsync(context.Scopes, context.TenantId, context.Claims, context.IsCaeEnabled, async, cancellationToken).ConfigureAwait(false);
			return new AccessToken(authenticationResult.AccessToken, authenticationResult.ExpiresOn);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000B438 File Offset: 0x00009638
		public virtual async ValueTask<AccessToken> AuthenticateCoreAsync(bool async, TokenRequestContext context, CancellationToken cancellationToken)
		{
			return await this._identitySource.Value.AuthenticateAsync(async, context, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000B494 File Offset: 0x00009694
		private async Task<AppTokenProviderResult> AppTokenProviderImpl(AppTokenProviderParameters parameters)
		{
			TokenRequestContext tokenRequestContext = new TokenRequestContext(parameters.Scopes.ToArray<string>(), null, parameters.Claims, null, false);
			AccessToken accessToken = await this.AuthenticateCoreAsync(true, tokenRequestContext, parameters.CancellationToken).ConfigureAwait(false);
			return new AppTokenProviderResult
			{
				AccessToken = accessToken.Token,
				ExpiresInSeconds = Math.Max(Convert.ToInt64((accessToken.ExpiresOn - DateTimeOffset.UtcNow).TotalSeconds), 1L)
			};
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000B4E0 File Offset: 0x000096E0
		private static ManagedIdentitySource SelectManagedIdentitySource(ManagedIdentityClientOptions options)
		{
			ManagedIdentitySource managedIdentitySource;
			if ((managedIdentitySource = ServiceFabricManagedIdentitySource.TryCreate(options)) == null && (managedIdentitySource = AppServiceV2019ManagedIdentitySource.TryCreate(options)) == null && (managedIdentitySource = AppServiceV2017ManagedIdentitySource.TryCreate(options)) == null && (managedIdentitySource = CloudShellManagedIdentitySource.TryCreate(options)) == null && (managedIdentitySource = AzureArcManagedIdentitySource.TryCreate(options)) == null)
			{
				managedIdentitySource = TokenExchangeManagedIdentitySource.TryCreate(options) ?? new ImdsManagedIdentitySource(options);
			}
			return managedIdentitySource;
		}

		// Token: 0x0400022C RID: 556
		internal const string MsiUnavailableError = "ManagedIdentityCredential authentication unavailable. No Managed Identity endpoint found.";

		// Token: 0x0400022D RID: 557
		internal Lazy<ManagedIdentitySource> _identitySource;

		// Token: 0x0400022E RID: 558
		private MsalConfidentialClient _msal;
	}
}
