using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.Identity
{
	// Token: 0x02000043 RID: 67
	public class DeviceCodeCredentialOptions : TokenCredentialOptions, ISupportsTokenCachePersistenceOptions, ISupportsDisableInstanceDiscovery, ISupportsAdditionallyAllowedTenants
	{
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00007A17 File Offset: 0x00005C17
		// (set) Token: 0x0600021F RID: 543 RVA: 0x00007A1F File Offset: 0x00005C1F
		public bool DisableAutomaticAuthentication { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00007A28 File Offset: 0x00005C28
		// (set) Token: 0x06000221 RID: 545 RVA: 0x00007A30 File Offset: 0x00005C30
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

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00007A40 File Offset: 0x00005C40
		// (set) Token: 0x06000223 RID: 547 RVA: 0x00007A48 File Offset: 0x00005C48
		public IList<string> AdditionallyAllowedTenants { get; internal set; } = new List<string>();

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00007A51 File Offset: 0x00005C51
		// (set) Token: 0x06000225 RID: 549 RVA: 0x00007A59 File Offset: 0x00005C59
		public string ClientId { get; set; } = "04b07795-8ddb-461a-bbee-02f9e1bf7b46";

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00007A62 File Offset: 0x00005C62
		// (set) Token: 0x06000227 RID: 551 RVA: 0x00007A6A File Offset: 0x00005C6A
		public TokenCachePersistenceOptions TokenCachePersistenceOptions { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00007A73 File Offset: 0x00005C73
		// (set) Token: 0x06000229 RID: 553 RVA: 0x00007A7B File Offset: 0x00005C7B
		public AuthenticationRecord AuthenticationRecord { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00007A84 File Offset: 0x00005C84
		// (set) Token: 0x0600022B RID: 555 RVA: 0x00007A8C File Offset: 0x00005C8C
		public Func<DeviceCodeInfo, CancellationToken, Task> DeviceCodeCallback { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00007A95 File Offset: 0x00005C95
		// (set) Token: 0x0600022D RID: 557 RVA: 0x00007A9D File Offset: 0x00005C9D
		public bool DisableInstanceDiscovery { get; set; }

		// Token: 0x04000167 RID: 359
		private string _tenantId;
	}
}
