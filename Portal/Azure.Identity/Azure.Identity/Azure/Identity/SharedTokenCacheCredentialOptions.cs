using System;
using Azure.Core;

namespace Azure.Identity
{
	// Token: 0x0200004F RID: 79
	public class SharedTokenCacheCredentialOptions : TokenCredentialOptions, ISupportsTokenCachePersistenceOptions, ISupportsDisableInstanceDiscovery
	{
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x00008C21 File Offset: 0x00006E21
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x00008C29 File Offset: 0x00006E29
		public string ClientId { get; set; } = "04b07795-8ddb-461a-bbee-02f9e1bf7b46";

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x00008C32 File Offset: 0x00006E32
		// (set) Token: 0x060002BA RID: 698 RVA: 0x00008C3A File Offset: 0x00006E3A
		public string Username { get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002BB RID: 699 RVA: 0x00008C43 File Offset: 0x00006E43
		// (set) Token: 0x060002BC RID: 700 RVA: 0x00008C4B File Offset: 0x00006E4B
		public string TenantId
		{
			get
			{
				return this._tenantId;
			}
			set
			{
				this._tenantId = Validations.ValidateTenantId(value, null, true);
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002BD RID: 701 RVA: 0x00008C5B File Offset: 0x00006E5B
		// (set) Token: 0x060002BE RID: 702 RVA: 0x00008C63 File Offset: 0x00006E63
		public bool EnableGuestTenantAuthentication { get; set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002BF RID: 703 RVA: 0x00008C6C File Offset: 0x00006E6C
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x00008C74 File Offset: 0x00006E74
		public AuthenticationRecord AuthenticationRecord { get; set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x00008C7D File Offset: 0x00006E7D
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x00008C85 File Offset: 0x00006E85
		public TokenCachePersistenceOptions TokenCachePersistenceOptions
		{
			get
			{
				return this._tokenCachePersistenceOptions;
			}
			set
			{
				Argument.AssertNotNull<TokenCachePersistenceOptions>(value, "value");
				this._tokenCachePersistenceOptions = value;
			}
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00008C99 File Offset: 0x00006E99
		public SharedTokenCacheCredentialOptions()
			: this(null)
		{
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00008CA2 File Offset: 0x00006EA2
		public SharedTokenCacheCredentialOptions(TokenCachePersistenceOptions tokenCacheOptions)
		{
			this.TokenCachePersistenceOptions = tokenCacheOptions ?? SharedTokenCacheCredentialOptions.s_defaulTokenCachetPersistenceOptions;
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x00008CC5 File Offset: 0x00006EC5
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x00008CCD File Offset: 0x00006ECD
		public bool DisableInstanceDiscovery { get; set; }

		// Token: 0x040001B6 RID: 438
		private string _tenantId;

		// Token: 0x040001B7 RID: 439
		private TokenCachePersistenceOptions _tokenCachePersistenceOptions;

		// Token: 0x040001B8 RID: 440
		internal static readonly TokenCachePersistenceOptions s_defaulTokenCachetPersistenceOptions = new TokenCachePersistenceOptions();
	}
}
