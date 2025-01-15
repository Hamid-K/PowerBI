using System;
using System.Collections.Generic;
using Azure.Core;

namespace Azure.Identity
{
	// Token: 0x02000050 RID: 80
	public class TokenCredentialOptions : ClientOptions
	{
		// Token: 0x060002C8 RID: 712 RVA: 0x00008CE2 File Offset: 0x00006EE2
		public TokenCredentialOptions()
			: base(new TokenCredentialDiagnosticsOptions())
		{
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x00008CFA File Offset: 0x00006EFA
		// (set) Token: 0x060002CA RID: 714 RVA: 0x00008D0B File Offset: 0x00006F0B
		public Uri AuthorityHost
		{
			get
			{
				return this._authorityHost ?? AzureAuthorityHosts.GetDefault();
			}
			set
			{
				this._authorityHost = Validations.ValidateAuthorityHost(value);
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002CB RID: 715 RVA: 0x00008D19 File Offset: 0x00006F19
		// (set) Token: 0x060002CC RID: 716 RVA: 0x00008D21 File Offset: 0x00006F21
		public bool IsUnsafeSupportLoggingEnabled { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002CD RID: 717 RVA: 0x00008D2A File Offset: 0x00006F2A
		// (set) Token: 0x060002CE RID: 718 RVA: 0x00008D32 File Offset: 0x00006F32
		internal bool IsChainedCredential { get; set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060002CF RID: 719 RVA: 0x00008D3B File Offset: 0x00006F3B
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x00008D43 File Offset: 0x00006F43
		internal TenantIdResolverBase TenantIdResolver { get; set; } = TenantIdResolverBase.Default;

		// Token: 0x060002D1 RID: 721 RVA: 0x00008D4C File Offset: 0x00006F4C
		internal virtual T Clone<T>() where T : TokenCredentialOptions, new()
		{
			T t = new T();
			t.AuthorityHost = this.AuthorityHost;
			t.IsUnsafeSupportLoggingEnabled = this.IsUnsafeSupportLoggingEnabled;
			t.Diagnostics.IsAccountIdentifierLoggingEnabled = this.Diagnostics.IsAccountIdentifierLoggingEnabled;
			TokenCredentialOptions.CloneIfImplemented<ISupportsDisableInstanceDiscovery>(this, t, delegate(ISupportsDisableInstanceDiscovery o, ISupportsDisableInstanceDiscovery c)
			{
				c.DisableInstanceDiscovery = o.DisableInstanceDiscovery;
			});
			TokenCredentialOptions.CloneIfImplemented<ISupportsTokenCachePersistenceOptions>(this, t, delegate(ISupportsTokenCachePersistenceOptions o, ISupportsTokenCachePersistenceOptions c)
			{
				c.TokenCachePersistenceOptions = o.TokenCachePersistenceOptions;
			});
			TokenCredentialOptions.CloneIfImplemented<ISupportsAdditionallyAllowedTenants>(this, t, delegate(ISupportsAdditionallyAllowedTenants o, ISupportsAdditionallyAllowedTenants c)
			{
				TokenCredentialOptions.CloneListItems<string>(o.AdditionallyAllowedTenants, c.AdditionallyAllowedTenants);
			});
			if (base.Transport != ClientOptions.Default.Transport)
			{
				t.Transport = base.Transport;
			}
			t.Diagnostics.ApplicationId = this.Diagnostics.ApplicationId;
			t.Diagnostics.IsLoggingEnabled = this.Diagnostics.IsLoggingEnabled;
			t.Diagnostics.IsTelemetryEnabled = this.Diagnostics.IsTelemetryEnabled;
			t.Diagnostics.LoggedContentSizeLimit = this.Diagnostics.LoggedContentSizeLimit;
			t.Diagnostics.IsDistributedTracingEnabled = this.Diagnostics.IsDistributedTracingEnabled;
			t.Diagnostics.IsLoggingContentEnabled = this.Diagnostics.IsLoggingContentEnabled;
			TokenCredentialOptions.CloneListItems<string>(this.Diagnostics.LoggedHeaderNames, t.Diagnostics.LoggedHeaderNames);
			TokenCredentialOptions.CloneListItems<string>(this.Diagnostics.LoggedQueryParameters, t.Diagnostics.LoggedQueryParameters);
			t.RetryPolicy = base.RetryPolicy;
			t.Retry.MaxRetries = base.Retry.MaxRetries;
			t.Retry.Delay = base.Retry.Delay;
			t.Retry.MaxDelay = base.Retry.MaxDelay;
			t.Retry.Mode = base.Retry.Mode;
			t.Retry.NetworkTimeout = base.Retry.NetworkTimeout;
			return t;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00008FBC File Offset: 0x000071BC
		private static void CloneListItems<T>(IList<T> original, IList<T> clone)
		{
			clone.Clear();
			foreach (T t in original)
			{
				clone.Add(t);
			}
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000900C File Offset: 0x0000720C
		private static void CloneIfImplemented<T>(TokenCredentialOptions original, TokenCredentialOptions clone, Action<T, T> cloneOperation) where T : class
		{
			T t = original as T;
			if (t != null)
			{
				T t2 = clone as T;
				if (t2 != null)
				{
					cloneOperation(t, t2);
				}
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x00009049 File Offset: 0x00007249
		public new TokenCredentialDiagnosticsOptions Diagnostics
		{
			get
			{
				return base.Diagnostics as TokenCredentialDiagnosticsOptions;
			}
		}

		// Token: 0x040001BE RID: 446
		private Uri _authorityHost;
	}
}
