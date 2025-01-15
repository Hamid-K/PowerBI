using System;
using System.Collections.Generic;
using Azure.Core;

namespace Azure.Identity
{
	// Token: 0x0200005A RID: 90
	internal class DefaultAzureCredentialFactory
	{
		// Token: 0x06000334 RID: 820 RVA: 0x0000A16A File Offset: 0x0000836A
		public DefaultAzureCredentialFactory(DefaultAzureCredentialOptions options)
			: this(options, CredentialPipeline.GetInstance(options, false))
		{
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000A17A File Offset: 0x0000837A
		protected DefaultAzureCredentialFactory(DefaultAzureCredentialOptions options, CredentialPipeline pipeline)
		{
			this.Pipeline = pipeline;
			this._useDefaultCredentialChain = options == null;
			this.Options = ((options != null) ? options.Clone<DefaultAzureCredentialOptions>() : null) ?? new DefaultAzureCredentialOptions();
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0000A1AE File Offset: 0x000083AE
		public DefaultAzureCredentialOptions Options { get; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0000A1B6 File Offset: 0x000083B6
		public CredentialPipeline Pipeline { get; }

		// Token: 0x06000338 RID: 824 RVA: 0x0000A1C0 File Offset: 0x000083C0
		public TokenCredential[] CreateCredentialChain()
		{
			if (this._useDefaultCredentialChain)
			{
				return DefaultAzureCredentialFactory.s_defaultCredentialChain;
			}
			List<TokenCredential> list = new List<TokenCredential>(10);
			if (!this.Options.ExcludeEnvironmentCredential)
			{
				list.Add(this.CreateEnvironmentCredential());
			}
			if (!this.Options.ExcludeWorkloadIdentityCredential)
			{
				list.Add(this.CreateWorkloadIdentityCredential());
			}
			if (!this.Options.ExcludeManagedIdentityCredential)
			{
				list.Add(this.CreateManagedIdentityCredential());
			}
			if (!this.Options.ExcludeSharedTokenCacheCredential)
			{
				list.Add(this.CreateSharedTokenCacheCredential());
			}
			if (!this.Options.ExcludeVisualStudioCredential)
			{
				list.Add(this.CreateVisualStudioCredential());
			}
			if (!this.Options.ExcludeVisualStudioCodeCredential)
			{
				list.Add(this.CreateVisualStudioCodeCredential());
			}
			if (!this.Options.ExcludeAzureCliCredential)
			{
				list.Add(this.CreateAzureCliCredential());
			}
			if (!this.Options.ExcludeAzurePowerShellCredential)
			{
				list.Add(this.CreateAzurePowerShellCredential());
			}
			if (!this.Options.ExcludeAzureDeveloperCliCredential)
			{
				list.Add(this.CreateAzureDeveloperCliCredential());
			}
			if (!this.Options.ExcludeInteractiveBrowserCredential)
			{
				list.Add(this.CreateInteractiveBrowserCredential());
			}
			if (list.Count == 0)
			{
				throw new ArgumentException("At least one credential type must be included in the authentication flow.", "options");
			}
			return list.ToArray();
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000A2FC File Offset: 0x000084FC
		public virtual TokenCredential CreateEnvironmentCredential()
		{
			EnvironmentCredentialOptions environmentCredentialOptions = this.Options.Clone<EnvironmentCredentialOptions>();
			if (!string.IsNullOrEmpty(environmentCredentialOptions.TenantId))
			{
				environmentCredentialOptions.TenantId = this.Options.TenantId;
			}
			return new EnvironmentCredential(this.Pipeline, environmentCredentialOptions);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000A33F File Offset: 0x0000853F
		public virtual TokenCredential CreateWorkloadIdentityCredential()
		{
			WorkloadIdentityCredentialOptions workloadIdentityCredentialOptions = this.Options.Clone<WorkloadIdentityCredentialOptions>();
			workloadIdentityCredentialOptions.ClientId = this.Options.WorkloadIdentityClientId;
			workloadIdentityCredentialOptions.TenantId = this.Options.TenantId;
			workloadIdentityCredentialOptions.Pipeline = this.Pipeline;
			return new WorkloadIdentityCredential(workloadIdentityCredentialOptions);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000A380 File Offset: 0x00008580
		public virtual TokenCredential CreateManagedIdentityCredential()
		{
			DefaultAzureCredentialOptions defaultAzureCredentialOptions = this.Options.Clone<DefaultAzureCredentialOptions>();
			defaultAzureCredentialOptions.IsChainedCredential = true;
			return new ManagedIdentityCredential(new ManagedIdentityClient(new ManagedIdentityClientOptions
			{
				ResourceIdentifier = defaultAzureCredentialOptions.ManagedIdentityResourceId,
				ClientId = defaultAzureCredentialOptions.ManagedIdentityClientId,
				Pipeline = CredentialPipeline.GetInstance(defaultAzureCredentialOptions, true),
				Options = defaultAzureCredentialOptions,
				InitialImdsConnectionTimeout = new TimeSpan?(TimeSpan.FromSeconds(1.0)),
				ExcludeTokenExchangeManagedIdentitySource = defaultAzureCredentialOptions.ExcludeWorkloadIdentityCredential
			}));
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000A400 File Offset: 0x00008600
		public virtual TokenCredential CreateSharedTokenCacheCredential()
		{
			SharedTokenCacheCredentialOptions sharedTokenCacheCredentialOptions = this.Options.Clone<SharedTokenCacheCredentialOptions>();
			sharedTokenCacheCredentialOptions.TenantId = this.Options.SharedTokenCacheTenantId;
			sharedTokenCacheCredentialOptions.Username = this.Options.SharedTokenCacheUsername;
			return new SharedTokenCacheCredential(this.Options.SharedTokenCacheTenantId, this.Options.SharedTokenCacheUsername, sharedTokenCacheCredentialOptions, this.Pipeline);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000A460 File Offset: 0x00008660
		public virtual TokenCredential CreateInteractiveBrowserCredential()
		{
			InteractiveBrowserCredentialOptions interactiveBrowserCredentialOptions = this.Options.Clone<InteractiveBrowserCredentialOptions>();
			interactiveBrowserCredentialOptions.TokenCachePersistenceOptions = new TokenCachePersistenceOptions();
			interactiveBrowserCredentialOptions.TenantId = this.Options.InteractiveBrowserTenantId;
			return new InteractiveBrowserCredential(this.Options.InteractiveBrowserTenantId, this.Options.InteractiveBrowserCredentialClientId ?? "04b07795-8ddb-461a-bbee-02f9e1bf7b46", interactiveBrowserCredentialOptions, this.Pipeline);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000A4C0 File Offset: 0x000086C0
		public virtual TokenCredential CreateAzureDeveloperCliCredential()
		{
			AzureDeveloperCliCredentialOptions azureDeveloperCliCredentialOptions = this.Options.Clone<AzureDeveloperCliCredentialOptions>();
			azureDeveloperCliCredentialOptions.TenantId = this.Options.TenantId;
			azureDeveloperCliCredentialOptions.ProcessTimeout = this.Options.CredentialProcessTimeout;
			azureDeveloperCliCredentialOptions.IsChainedCredential = true;
			return new AzureDeveloperCliCredential(this.Pipeline, null, azureDeveloperCliCredentialOptions);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000A510 File Offset: 0x00008710
		public virtual TokenCredential CreateAzureCliCredential()
		{
			AzureCliCredentialOptions azureCliCredentialOptions = this.Options.Clone<AzureCliCredentialOptions>();
			azureCliCredentialOptions.TenantId = this.Options.TenantId;
			azureCliCredentialOptions.ProcessTimeout = this.Options.CredentialProcessTimeout;
			azureCliCredentialOptions.IsChainedCredential = true;
			return new AzureCliCredential(this.Pipeline, null, azureCliCredentialOptions);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000A560 File Offset: 0x00008760
		public virtual TokenCredential CreateVisualStudioCredential()
		{
			VisualStudioCredentialOptions visualStudioCredentialOptions = this.Options.Clone<VisualStudioCredentialOptions>();
			visualStudioCredentialOptions.TenantId = this.Options.VisualStudioTenantId;
			visualStudioCredentialOptions.ProcessTimeout = this.Options.CredentialProcessTimeout;
			visualStudioCredentialOptions.IsChainedCredential = true;
			return new VisualStudioCredential(this.Options.VisualStudioTenantId, this.Pipeline, null, null, visualStudioCredentialOptions);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000A5BB File Offset: 0x000087BB
		public virtual TokenCredential CreateVisualStudioCodeCredential()
		{
			VisualStudioCodeCredentialOptions visualStudioCodeCredentialOptions = this.Options.Clone<VisualStudioCodeCredentialOptions>();
			visualStudioCodeCredentialOptions.TenantId = this.Options.VisualStudioCodeTenantId;
			visualStudioCodeCredentialOptions.IsChainedCredential = true;
			return new VisualStudioCodeCredential(visualStudioCodeCredentialOptions, this.Pipeline, null, null, null);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000A5F0 File Offset: 0x000087F0
		public virtual TokenCredential CreateAzurePowerShellCredential()
		{
			AzurePowerShellCredentialOptions azurePowerShellCredentialOptions = this.Options.Clone<AzurePowerShellCredentialOptions>();
			azurePowerShellCredentialOptions.TenantId = this.Options.TenantId;
			azurePowerShellCredentialOptions.ProcessTimeout = this.Options.CredentialProcessTimeout;
			azurePowerShellCredentialOptions.IsChainedCredential = true;
			return new AzurePowerShellCredential(azurePowerShellCredentialOptions, this.Pipeline, null);
		}

		// Token: 0x040001F9 RID: 505
		private static readonly TokenCredential[] s_defaultCredentialChain = new DefaultAzureCredentialFactory(new DefaultAzureCredentialOptions()).CreateCredentialChain();

		// Token: 0x040001FA RID: 506
		private bool _useDefaultCredentialChain;
	}
}
