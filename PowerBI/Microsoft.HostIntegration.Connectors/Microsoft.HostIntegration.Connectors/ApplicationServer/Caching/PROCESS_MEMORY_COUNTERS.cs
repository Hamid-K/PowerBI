using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000343 RID: 835
	internal struct PROCESS_MEMORY_COUNTERS
	{
		// Token: 0x040010A5 RID: 4261
		public uint cb;

		// Token: 0x040010A6 RID: 4262
		public uint PageFaultCount;

		// Token: 0x040010A7 RID: 4263
		public UIntPtr PeakWorkingSetSize;

		// Token: 0x040010A8 RID: 4264
		public UIntPtr WorkingSetSize;

		// Token: 0x040010A9 RID: 4265
		public UIntPtr QuotaPeakPagedPoolUsage;

		// Token: 0x040010AA RID: 4266
		public UIntPtr QuotaPagedPoolUsage;

		// Token: 0x040010AB RID: 4267
		public UIntPtr QuotaPeakNonPagedPoolUsage;

		// Token: 0x040010AC RID: 4268
		public UIntPtr QuotaNonPagedPoolUsage;

		// Token: 0x040010AD RID: 4269
		public UIntPtr PagefileUsage;

		// Token: 0x040010AE RID: 4270
		public UIntPtr PeakPagefileUsage;
	}
}
