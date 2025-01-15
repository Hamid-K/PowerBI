using System;
using Azure.Core;

namespace Azure.Identity
{
	// Token: 0x02000070 RID: 112
	internal class ManagedIdentityClientOptions
	{
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x0000B52F File Offset: 0x0000972F
		// (set) Token: 0x060003C9 RID: 969 RVA: 0x0000B537 File Offset: 0x00009737
		public TokenCredentialOptions Options { get; set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060003CA RID: 970 RVA: 0x0000B540 File Offset: 0x00009740
		// (set) Token: 0x060003CB RID: 971 RVA: 0x0000B548 File Offset: 0x00009748
		public string ClientId { get; set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060003CC RID: 972 RVA: 0x0000B551 File Offset: 0x00009751
		// (set) Token: 0x060003CD RID: 973 RVA: 0x0000B559 File Offset: 0x00009759
		public ResourceIdentifier ResourceIdentifier { get; set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060003CE RID: 974 RVA: 0x0000B562 File Offset: 0x00009762
		// (set) Token: 0x060003CF RID: 975 RVA: 0x0000B56A File Offset: 0x0000976A
		public bool PreserveTransport { get; set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x0000B573 File Offset: 0x00009773
		// (set) Token: 0x060003D1 RID: 977 RVA: 0x0000B57B File Offset: 0x0000977B
		public TimeSpan? InitialImdsConnectionTimeout { get; set; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x0000B584 File Offset: 0x00009784
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x0000B58C File Offset: 0x0000978C
		public CredentialPipeline Pipeline { get; set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x0000B595 File Offset: 0x00009795
		// (set) Token: 0x060003D5 RID: 981 RVA: 0x0000B59D File Offset: 0x0000979D
		public bool ExcludeTokenExchangeManagedIdentitySource { get; set; }
	}
}
