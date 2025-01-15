using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001F9 RID: 505
	public sealed class MemoryStatus
	{
		// Token: 0x06000D6A RID: 3434 RVA: 0x0002EF02 File Offset: 0x0002D102
		public MemoryStatus(long totalPhysicalMemory, long availablePhysicalMemory, long totalVirtualMemory, long availableVirtualMemory)
		{
			this.TotalPhysicalMemory = totalPhysicalMemory;
			this.AvailablelPhysicalMemory = availablePhysicalMemory;
			this.TotalVirtualMemory = totalVirtualMemory;
			this.AvailableVirtualMemory = availableVirtualMemory;
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000D6B RID: 3435 RVA: 0x0002EF27 File Offset: 0x0002D127
		// (set) Token: 0x06000D6C RID: 3436 RVA: 0x0002EF2F File Offset: 0x0002D12F
		public long TotalPhysicalMemory { get; private set; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000D6D RID: 3437 RVA: 0x0002EF38 File Offset: 0x0002D138
		// (set) Token: 0x06000D6E RID: 3438 RVA: 0x0002EF40 File Offset: 0x0002D140
		public long AvailablelPhysicalMemory { get; private set; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000D6F RID: 3439 RVA: 0x0002EF49 File Offset: 0x0002D149
		// (set) Token: 0x06000D70 RID: 3440 RVA: 0x0002EF51 File Offset: 0x0002D151
		public long TotalVirtualMemory { get; private set; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000D71 RID: 3441 RVA: 0x0002EF5A File Offset: 0x0002D15A
		// (set) Token: 0x06000D72 RID: 3442 RVA: 0x0002EF62 File Offset: 0x0002D162
		public long AvailableVirtualMemory { get; private set; }
	}
}
