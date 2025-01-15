using System;

namespace Model
{
	// Token: 0x02000024 RID: 36
	public sealed class CacheOptions
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000026A6 File Offset: 0x000008A6
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x000026AE File Offset: 0x000008AE
		public Guid Id { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x000026B7 File Offset: 0x000008B7
		// (set) Token: 0x060000AA RID: 170 RVA: 0x000026BF File Offset: 0x000008BF
		public ItemExecutionType ExecutionType { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000AB RID: 171 RVA: 0x000026C8 File Offset: 0x000008C8
		// (set) Token: 0x060000AC RID: 172 RVA: 0x000026D0 File Offset: 0x000008D0
		public ExpirationReference Expiration { get; set; }
	}
}
