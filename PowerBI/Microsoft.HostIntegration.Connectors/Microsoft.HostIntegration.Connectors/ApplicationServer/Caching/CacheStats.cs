using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200026D RID: 621
	internal class CacheStats
	{
		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x060014CC RID: 5324 RVA: 0x000409E7 File Offset: 0x0003EBE7
		public long TotalMemory
		{
			get
			{
				return this.PrimaryMemory + this.SecondaryMemory + this.IdleMemory;
			}
		}

		// Token: 0x04000C56 RID: 3158
		public long PrimaryMemory;

		// Token: 0x04000C57 RID: 3159
		public long SecondaryMemory;

		// Token: 0x04000C58 RID: 3160
		public long IdleMemory;
	}
}
