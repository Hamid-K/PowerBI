using System;

namespace dotless.Core.Parser
{
	// Token: 0x02000027 RID: 39
	public class Location
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600015F RID: 351 RVA: 0x0000832F File Offset: 0x0000652F
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00008337 File Offset: 0x00006537
		public int Index { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00008340 File Offset: 0x00006540
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00008348 File Offset: 0x00006548
		public int CurrentChunk { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00008351 File Offset: 0x00006551
		// (set) Token: 0x06000164 RID: 356 RVA: 0x00008359 File Offset: 0x00006559
		public int CurrentChunkIndex { get; set; }
	}
}
