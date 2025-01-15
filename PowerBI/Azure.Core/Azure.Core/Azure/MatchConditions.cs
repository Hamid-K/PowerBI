using System;

namespace Azure
{
	// Token: 0x02000024 RID: 36
	public class MatchConditions
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00002CCC File Offset: 0x00000ECC
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public ETag? IfMatch { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00002CDD File Offset: 0x00000EDD
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00002CE5 File Offset: 0x00000EE5
		public ETag? IfNoneMatch { get; set; }
	}
}
