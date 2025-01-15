using System;
using System.Collections.Generic;

namespace Azure.Identity
{
	// Token: 0x02000047 RID: 71
	public class InteractiveBrowserCredentialOptions : TokenCredentialOptions, ISupportsTokenCachePersistenceOptions, ISupportsDisableInstanceDiscovery, ISupportsAdditionallyAllowedTenants
	{
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600026D RID: 621 RVA: 0x000082DF File Offset: 0x000064DF
		// (set) Token: 0x0600026E RID: 622 RVA: 0x000082E7 File Offset: 0x000064E7
		public bool DisableAutomaticAuthentication { get; set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600026F RID: 623 RVA: 0x000082F0 File Offset: 0x000064F0
		// (set) Token: 0x06000270 RID: 624 RVA: 0x000082F8 File Offset: 0x000064F8
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

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000271 RID: 625 RVA: 0x00008308 File Offset: 0x00006508
		// (set) Token: 0x06000272 RID: 626 RVA: 0x00008310 File Offset: 0x00006510
		public IList<string> AdditionallyAllowedTenants { get; internal set; } = new List<string>();

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000273 RID: 627 RVA: 0x00008319 File Offset: 0x00006519
		// (set) Token: 0x06000274 RID: 628 RVA: 0x00008321 File Offset: 0x00006521
		public string ClientId { get; set; } = "04b07795-8ddb-461a-bbee-02f9e1bf7b46";

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000832A File Offset: 0x0000652A
		// (set) Token: 0x06000276 RID: 630 RVA: 0x00008332 File Offset: 0x00006532
		public TokenCachePersistenceOptions TokenCachePersistenceOptions { get; set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000833B File Offset: 0x0000653B
		// (set) Token: 0x06000278 RID: 632 RVA: 0x00008343 File Offset: 0x00006543
		public Uri RedirectUri { get; set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000834C File Offset: 0x0000654C
		// (set) Token: 0x0600027A RID: 634 RVA: 0x00008354 File Offset: 0x00006554
		public AuthenticationRecord AuthenticationRecord { get; set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000835D File Offset: 0x0000655D
		// (set) Token: 0x0600027C RID: 636 RVA: 0x00008365 File Offset: 0x00006565
		public string LoginHint { get; set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000836E File Offset: 0x0000656E
		// (set) Token: 0x0600027E RID: 638 RVA: 0x00008376 File Offset: 0x00006576
		public bool DisableInstanceDiscovery { get; set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000837F File Offset: 0x0000657F
		// (set) Token: 0x06000280 RID: 640 RVA: 0x00008387 File Offset: 0x00006587
		public BrowserCustomizationOptions BrowserCustomization { get; set; }

		// Token: 0x0400018C RID: 396
		private string _tenantId;
	}
}
