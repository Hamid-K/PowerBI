using System;

namespace Microsoft.Data.Mashup.ProviderCommon
{
	// Token: 0x02000012 RID: 18
	internal sealed class MashupCacheSettings2
	{
		// Token: 0x06000053 RID: 83 RVA: 0x0000327E File Offset: 0x0000147E
		public MashupCacheSettings2()
		{
			this.TimeToLive = TimeSpan.Zero;
			this.MaxSize = -1L;
			this.InMemory = false;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000032A0 File Offset: 0x000014A0
		// (set) Token: 0x06000055 RID: 85 RVA: 0x000032A8 File Offset: 0x000014A8
		public TimeSpan TimeToLive { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000056 RID: 86 RVA: 0x000032B1 File Offset: 0x000014B1
		// (set) Token: 0x06000057 RID: 87 RVA: 0x000032B9 File Offset: 0x000014B9
		public long MaxSize { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000032C2 File Offset: 0x000014C2
		// (set) Token: 0x06000059 RID: 89 RVA: 0x000032CA File Offset: 0x000014CA
		public bool InMemory { get; set; }
	}
}
