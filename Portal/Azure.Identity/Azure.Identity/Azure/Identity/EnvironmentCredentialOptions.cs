using System;
using System.Collections.Generic;

namespace Azure.Identity
{
	// Token: 0x02000045 RID: 69
	public class EnvironmentCredentialOptions : TokenCredentialOptions, ISupportsDisableInstanceDiscovery, ISupportsAdditionallyAllowedTenants
	{
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00007CEB File Offset: 0x00005EEB
		// (set) Token: 0x06000239 RID: 569 RVA: 0x00007CF3 File Offset: 0x00005EF3
		internal string TenantId { get; set; } = EnvironmentVariables.TenantId;

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00007CFC File Offset: 0x00005EFC
		// (set) Token: 0x0600023B RID: 571 RVA: 0x00007D04 File Offset: 0x00005F04
		internal string ClientId { get; set; } = EnvironmentVariables.ClientId;

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00007D0D File Offset: 0x00005F0D
		// (set) Token: 0x0600023D RID: 573 RVA: 0x00007D15 File Offset: 0x00005F15
		internal string ClientSecret { get; set; } = EnvironmentVariables.ClientSecret;

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00007D1E File Offset: 0x00005F1E
		// (set) Token: 0x0600023F RID: 575 RVA: 0x00007D26 File Offset: 0x00005F26
		internal string ClientCertificatePath { get; set; } = EnvironmentVariables.ClientCertificatePath;

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00007D2F File Offset: 0x00005F2F
		// (set) Token: 0x06000241 RID: 577 RVA: 0x00007D37 File Offset: 0x00005F37
		internal string ClientCertificatePassword { get; set; } = EnvironmentVariables.ClientCertificatePassword;

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00007D40 File Offset: 0x00005F40
		// (set) Token: 0x06000243 RID: 579 RVA: 0x00007D48 File Offset: 0x00005F48
		internal bool SendCertificateChain { get; set; } = EnvironmentVariables.ClientSendCertificateChain;

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000244 RID: 580 RVA: 0x00007D51 File Offset: 0x00005F51
		// (set) Token: 0x06000245 RID: 581 RVA: 0x00007D59 File Offset: 0x00005F59
		internal string Username { get; set; } = EnvironmentVariables.Username;

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000246 RID: 582 RVA: 0x00007D62 File Offset: 0x00005F62
		// (set) Token: 0x06000247 RID: 583 RVA: 0x00007D6A File Offset: 0x00005F6A
		internal string Password { get; set; } = EnvironmentVariables.Password;

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00007D73 File Offset: 0x00005F73
		// (set) Token: 0x06000249 RID: 585 RVA: 0x00007D7B File Offset: 0x00005F7B
		internal MsalConfidentialClient MsalConfidentialClient { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00007D84 File Offset: 0x00005F84
		// (set) Token: 0x0600024B RID: 587 RVA: 0x00007D8C File Offset: 0x00005F8C
		internal MsalPublicClient MsalPublicClient { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600024C RID: 588 RVA: 0x00007D95 File Offset: 0x00005F95
		// (set) Token: 0x0600024D RID: 589 RVA: 0x00007D9D File Offset: 0x00005F9D
		public bool DisableInstanceDiscovery { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00007DA6 File Offset: 0x00005FA6
		// (set) Token: 0x0600024F RID: 591 RVA: 0x00007DAE File Offset: 0x00005FAE
		public IList<string> AdditionallyAllowedTenants { get; internal set; } = EnvironmentVariables.AdditionallyAllowedTenants;
	}
}
