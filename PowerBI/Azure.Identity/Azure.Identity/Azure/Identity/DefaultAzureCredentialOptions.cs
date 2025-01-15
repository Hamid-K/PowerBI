using System;
using System.Collections.Generic;
using System.ComponentModel;
using Azure.Core;

namespace Azure.Identity
{
	// Token: 0x02000041 RID: 65
	public class DefaultAzureCredentialOptions : TokenCredentialOptions, ISupportsDisableInstanceDiscovery, ISupportsAdditionallyAllowedTenants
	{
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00007025 File Offset: 0x00005225
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x00007034 File Offset: 0x00005234
		public string TenantId
		{
			get
			{
				return this._tenantId.Value;
			}
			set
			{
				if (this._interactiveBrowserTenantId.Updated && value != this._interactiveBrowserTenantId.Value)
				{
					throw new InvalidOperationException("Applications should not set both TenantId and InteractiveBrowserTenantId. TenantId is preferred, and is functionally equivalent. InteractiveBrowserTenantId exists only to provide backwards compatibility.");
				}
				if (this._sharedTokenCacheTenantId.Updated && value != this._sharedTokenCacheTenantId.Value)
				{
					throw new InvalidOperationException("Applications should not set both TenantId and SharedTokenCacheTenantId. TenantId is preferred, and is functionally equivalent. SharedTokenCacheTenantId exists only to provide backwards compatibility.");
				}
				if (this._visualStudioTenantId.Updated && value != this._visualStudioTenantId.Value)
				{
					throw new InvalidOperationException("Applications should not set both TenantId and VisualStudioTenantId. TenantId is preferred, and is functionally equivalent. VisualStudioTenantId exists only to provide backwards compatibility.");
				}
				if (this._visualStudioCodeTenantId.Updated && value != this._visualStudioCodeTenantId.Value)
				{
					throw new InvalidOperationException("Applications should not set both TenantId and VisualStudioCodeTenantId. TenantId is preferred, and is functionally equivalent. VisualStudioCodeTenantId exists only to provide backwards compatibility.");
				}
				this._tenantId.Value = value;
				this._interactiveBrowserTenantId.Value = value;
				this._sharedTokenCacheTenantId.Value = value;
				this._visualStudioCodeTenantId.Value = value;
				this._visualStudioTenantId.Value = value;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00007129 File Offset: 0x00005329
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x00007136 File Offset: 0x00005336
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string InteractiveBrowserTenantId
		{
			get
			{
				return this._interactiveBrowserTenantId.Value;
			}
			set
			{
				if (this._tenantId.Updated && value != this._tenantId.Value)
				{
					throw new InvalidOperationException("Applications should not set both TenantId and InteractiveBrowserTenantId. TenantId is preferred, and is functionally equivalent. InteractiveBrowserTenantId exists only to provide backwards compatibility.");
				}
				this._interactiveBrowserTenantId.Value = value;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000716F File Offset: 0x0000536F
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x0000717C File Offset: 0x0000537C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string SharedTokenCacheTenantId
		{
			get
			{
				return this._sharedTokenCacheTenantId.Value;
			}
			set
			{
				if (this._tenantId.Updated && value != this._tenantId.Value)
				{
					throw new InvalidOperationException("Applications should not set both TenantId and SharedTokenCacheTenantId. TenantId is preferred, and is functionally equivalent. SharedTokenCacheTenantId exists only to provide backwards compatibility.");
				}
				this._sharedTokenCacheTenantId.Value = value;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x000071B5 File Offset: 0x000053B5
		// (set) Token: 0x060001DA RID: 474 RVA: 0x000071C2 File Offset: 0x000053C2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string VisualStudioTenantId
		{
			get
			{
				return this._visualStudioTenantId.Value;
			}
			set
			{
				if (this._tenantId.Updated && value != this._tenantId.Value)
				{
					throw new InvalidOperationException("Applications should not set both TenantId and VisualStudioTenantId. TenantId is preferred, and is functionally equivalent. VisualStudioTenantId exists only to provide backwards compatibility.");
				}
				this._visualStudioTenantId.Value = value;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001DB RID: 475 RVA: 0x000071FB File Offset: 0x000053FB
		// (set) Token: 0x060001DC RID: 476 RVA: 0x00007208 File Offset: 0x00005408
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string VisualStudioCodeTenantId
		{
			get
			{
				return this._visualStudioCodeTenantId.Value;
			}
			set
			{
				if (this._tenantId.Updated && value != this._tenantId.Value)
				{
					throw new InvalidOperationException("Applications should not set both TenantId and VisualStudioCodeTenantId. TenantId is preferred, and is functionally equivalent. VisualStudioCodeTenantId exists only to provide backwards compatibility.");
				}
				this._visualStudioCodeTenantId.Value = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00007241 File Offset: 0x00005441
		// (set) Token: 0x060001DE RID: 478 RVA: 0x00007249 File Offset: 0x00005449
		public IList<string> AdditionallyAllowedTenants { get; internal set; } = EnvironmentVariables.AdditionallyAllowedTenants;

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00007252 File Offset: 0x00005452
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x0000725A File Offset: 0x0000545A
		public string SharedTokenCacheUsername { get; set; } = EnvironmentVariables.Username;

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00007263 File Offset: 0x00005463
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x0000726B File Offset: 0x0000546B
		public string InteractiveBrowserCredentialClientId { get; set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00007274 File Offset: 0x00005474
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x0000727C File Offset: 0x0000547C
		public string WorkloadIdentityClientId { get; set; } = EnvironmentVariables.ClientId;

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00007285 File Offset: 0x00005485
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x0000728D File Offset: 0x0000548D
		public string ManagedIdentityClientId { get; set; } = EnvironmentVariables.ClientId;

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00007296 File Offset: 0x00005496
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x0000729E File Offset: 0x0000549E
		public ResourceIdentifier ManagedIdentityResourceId { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x000072A7 File Offset: 0x000054A7
		// (set) Token: 0x060001EA RID: 490 RVA: 0x000072AF File Offset: 0x000054AF
		public TimeSpan? CredentialProcessTimeout { get; set; } = new TimeSpan?(TimeSpan.FromSeconds(30.0));

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001EB RID: 491 RVA: 0x000072B8 File Offset: 0x000054B8
		// (set) Token: 0x060001EC RID: 492 RVA: 0x000072C0 File Offset: 0x000054C0
		public bool ExcludeEnvironmentCredential { get; set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001ED RID: 493 RVA: 0x000072C9 File Offset: 0x000054C9
		// (set) Token: 0x060001EE RID: 494 RVA: 0x000072D1 File Offset: 0x000054D1
		public bool ExcludeWorkloadIdentityCredential { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001EF RID: 495 RVA: 0x000072DA File Offset: 0x000054DA
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x000072E2 File Offset: 0x000054E2
		public bool ExcludeManagedIdentityCredential { get; set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x000072EB File Offset: 0x000054EB
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x000072F3 File Offset: 0x000054F3
		public bool ExcludeAzureDeveloperCliCredential { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x000072FC File Offset: 0x000054FC
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x00007304 File Offset: 0x00005504
		public bool ExcludeSharedTokenCacheCredential { get; set; } = true;

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000730D File Offset: 0x0000550D
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x00007315 File Offset: 0x00005515
		public bool ExcludeInteractiveBrowserCredential { get; set; } = true;

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000731E File Offset: 0x0000551E
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x00007326 File Offset: 0x00005526
		public bool ExcludeAzureCliCredential { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000732F File Offset: 0x0000552F
		// (set) Token: 0x060001FA RID: 506 RVA: 0x00007337 File Offset: 0x00005537
		public bool ExcludeVisualStudioCredential { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00007340 File Offset: 0x00005540
		// (set) Token: 0x060001FC RID: 508 RVA: 0x00007348 File Offset: 0x00005548
		public bool ExcludeVisualStudioCodeCredential { get; set; } = true;

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00007351 File Offset: 0x00005551
		// (set) Token: 0x060001FE RID: 510 RVA: 0x00007359 File Offset: 0x00005559
		public bool ExcludeAzurePowerShellCredential { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00007362 File Offset: 0x00005562
		// (set) Token: 0x06000200 RID: 512 RVA: 0x0000736A File Offset: 0x0000556A
		public bool DisableInstanceDiscovery { get; set; }

		// Token: 0x06000201 RID: 513 RVA: 0x00007374 File Offset: 0x00005574
		internal override T Clone<T>()
		{
			T t = base.Clone<T>();
			DefaultAzureCredentialOptions defaultAzureCredentialOptions = t as DefaultAzureCredentialOptions;
			if (defaultAzureCredentialOptions != null)
			{
				defaultAzureCredentialOptions._tenantId = this._tenantId;
				defaultAzureCredentialOptions._interactiveBrowserTenantId = this._interactiveBrowserTenantId;
				defaultAzureCredentialOptions._sharedTokenCacheTenantId = this._sharedTokenCacheTenantId;
				defaultAzureCredentialOptions._visualStudioTenantId = this._visualStudioTenantId;
				defaultAzureCredentialOptions._visualStudioCodeTenantId = this._visualStudioCodeTenantId;
				defaultAzureCredentialOptions.SharedTokenCacheUsername = this.SharedTokenCacheUsername;
				defaultAzureCredentialOptions.InteractiveBrowserCredentialClientId = this.InteractiveBrowserCredentialClientId;
				defaultAzureCredentialOptions.WorkloadIdentityClientId = this.WorkloadIdentityClientId;
				defaultAzureCredentialOptions.ManagedIdentityClientId = this.ManagedIdentityClientId;
				defaultAzureCredentialOptions.ManagedIdentityResourceId = this.ManagedIdentityResourceId;
				defaultAzureCredentialOptions.CredentialProcessTimeout = this.CredentialProcessTimeout;
				defaultAzureCredentialOptions.ExcludeEnvironmentCredential = this.ExcludeEnvironmentCredential;
				defaultAzureCredentialOptions.ExcludeWorkloadIdentityCredential = this.ExcludeWorkloadIdentityCredential;
				defaultAzureCredentialOptions.ExcludeManagedIdentityCredential = this.ExcludeManagedIdentityCredential;
				defaultAzureCredentialOptions.ExcludeAzureDeveloperCliCredential = this.ExcludeAzureDeveloperCliCredential;
				defaultAzureCredentialOptions.ExcludeSharedTokenCacheCredential = this.ExcludeSharedTokenCacheCredential;
				defaultAzureCredentialOptions.ExcludeInteractiveBrowserCredential = this.ExcludeInteractiveBrowserCredential;
				defaultAzureCredentialOptions.ExcludeAzureCliCredential = this.ExcludeAzureCliCredential;
				defaultAzureCredentialOptions.ExcludeVisualStudioCredential = this.ExcludeVisualStudioCredential;
				defaultAzureCredentialOptions.ExcludeVisualStudioCodeCredential = this.ExcludeVisualStudioCodeCredential;
				defaultAzureCredentialOptions.ExcludeAzurePowerShellCredential = this.ExcludeAzurePowerShellCredential;
			}
			return t;
		}

		// Token: 0x04000144 RID: 324
		private DefaultAzureCredentialOptions.UpdateTracker<string> _tenantId = new DefaultAzureCredentialOptions.UpdateTracker<string>(EnvironmentVariables.TenantId);

		// Token: 0x04000145 RID: 325
		private DefaultAzureCredentialOptions.UpdateTracker<string> _interactiveBrowserTenantId = new DefaultAzureCredentialOptions.UpdateTracker<string>(EnvironmentVariables.TenantId);

		// Token: 0x04000146 RID: 326
		private DefaultAzureCredentialOptions.UpdateTracker<string> _sharedTokenCacheTenantId = new DefaultAzureCredentialOptions.UpdateTracker<string>(EnvironmentVariables.TenantId);

		// Token: 0x04000147 RID: 327
		private DefaultAzureCredentialOptions.UpdateTracker<string> _visualStudioTenantId = new DefaultAzureCredentialOptions.UpdateTracker<string>(EnvironmentVariables.TenantId);

		// Token: 0x04000148 RID: 328
		private DefaultAzureCredentialOptions.UpdateTracker<string> _visualStudioCodeTenantId = new DefaultAzureCredentialOptions.UpdateTracker<string>(EnvironmentVariables.TenantId);

		// Token: 0x020000C6 RID: 198
		private struct UpdateTracker<T>
		{
			// Token: 0x0600053C RID: 1340 RVA: 0x00013046 File Offset: 0x00011246
			public UpdateTracker(T initialValue)
			{
				this._value = initialValue;
				this._updated = false;
			}

			// Token: 0x17000152 RID: 338
			// (get) Token: 0x0600053D RID: 1341 RVA: 0x00013056 File Offset: 0x00011256
			// (set) Token: 0x0600053E RID: 1342 RVA: 0x0001305E File Offset: 0x0001125E
			public T Value
			{
				get
				{
					return this._value;
				}
				set
				{
					this._value = value;
					this._updated = true;
				}
			}

			// Token: 0x17000153 RID: 339
			// (get) Token: 0x0600053F RID: 1343 RVA: 0x0001306E File Offset: 0x0001126E
			public bool Updated
			{
				get
				{
					return this._updated;
				}
			}

			// Token: 0x040003C0 RID: 960
			private bool _updated;

			// Token: 0x040003C1 RID: 961
			private T _value;
		}
	}
}
