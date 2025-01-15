using System;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x02000022 RID: 34
	public sealed class ReplicatedDatabaseClient<TInterface> where TInterface : IDatabaseClient
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x00004300 File Offset: 0x00002500
		public ReplicatedDatabaseClient(TInterface primary, TInterface secondary, TInterface active)
		{
			this.Primary = primary;
			this.Secondary = secondary;
			this.Active = active;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x0000431D File Offset: 0x0000251D
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00004325 File Offset: 0x00002525
		public TInterface Primary { get; private set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x0000432E File Offset: 0x0000252E
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00004336 File Offset: 0x00002536
		public TInterface Secondary { get; private set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x0000433F File Offset: 0x0000253F
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00004347 File Offset: 0x00002547
		public TInterface Active { get; private set; }
	}
}
