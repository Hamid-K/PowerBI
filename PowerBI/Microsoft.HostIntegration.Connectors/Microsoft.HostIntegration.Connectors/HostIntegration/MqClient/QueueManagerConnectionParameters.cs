using System;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000BCA RID: 3018
	public class QueueManagerConnectionParameters
	{
		// Token: 0x170016D2 RID: 5842
		// (get) Token: 0x06005D9A RID: 23962 RVA: 0x0017F1F6 File Offset: 0x0017D3F6
		// (set) Token: 0x06005D9B RID: 23963 RVA: 0x0017F1FE File Offset: 0x0017D3FE
		public string Name { get; set; }

		// Token: 0x170016D3 RID: 5843
		// (get) Token: 0x06005D9C RID: 23964 RVA: 0x0017F207 File Offset: 0x0017D407
		// (set) Token: 0x06005D9D RID: 23965 RVA: 0x0017F20F File Offset: 0x0017D40F
		public string Channel { get; set; }

		// Token: 0x170016D4 RID: 5844
		// (get) Token: 0x06005D9E RID: 23966 RVA: 0x0017F218 File Offset: 0x0017D418
		// (set) Token: 0x06005D9F RID: 23967 RVA: 0x0017F220 File Offset: 0x0017D420
		public string ConnectAs { get; set; }

		// Token: 0x170016D5 RID: 5845
		// (get) Token: 0x06005DA0 RID: 23968 RVA: 0x0017F229 File Offset: 0x0017D429
		// (set) Token: 0x06005DA1 RID: 23969 RVA: 0x0017F231 File Offset: 0x0017D431
		public string AuthorizationUser { get; set; }

		// Token: 0x170016D6 RID: 5846
		// (get) Token: 0x06005DA2 RID: 23970 RVA: 0x0017F23A File Offset: 0x0017D43A
		// (set) Token: 0x06005DA3 RID: 23971 RVA: 0x0017F242 File Offset: 0x0017D442
		public string AuthorizationPassword { get; set; }

		// Token: 0x170016D7 RID: 5847
		// (get) Token: 0x06005DA4 RID: 23972 RVA: 0x0017F24B File Offset: 0x0017D44B
		// (set) Token: 0x06005DA5 RID: 23973 RVA: 0x0017F253 File Offset: 0x0017D453
		public bool IsTransactional { get; set; }

		// Token: 0x170016D8 RID: 5848
		// (get) Token: 0x06005DA6 RID: 23974 RVA: 0x0017F25C File Offset: 0x0017D45C
		// (set) Token: 0x06005DA7 RID: 23975 RVA: 0x0017F264 File Offset: 0x0017D464
		public int ResourceManagerId { get; set; }

		// Token: 0x170016D9 RID: 5849
		// (get) Token: 0x06005DA8 RID: 23976 RVA: 0x0017F26D File Offset: 0x0017D46D
		// (set) Token: 0x06005DA9 RID: 23977 RVA: 0x0017F275 File Offset: 0x0017D475
		public bool InMsdtc { get; set; }
	}
}
