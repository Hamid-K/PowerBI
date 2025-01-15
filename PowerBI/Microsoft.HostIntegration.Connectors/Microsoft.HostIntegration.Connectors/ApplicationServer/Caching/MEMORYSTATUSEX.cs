using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000342 RID: 834
	internal struct MEMORYSTATUSEX
	{
		// Token: 0x06001DE2 RID: 7650 RVA: 0x00059DEC File Offset: 0x00057FEC
		public void Init()
		{
			this.dwLength = 64U;
		}

		// Token: 0x0400109B RID: 4251
		private const int SIZEOF_MEMORYSTATUSEX = 64;

		// Token: 0x0400109C RID: 4252
		public uint dwLength;

		// Token: 0x0400109D RID: 4253
		public uint dwMemoryLoad;

		// Token: 0x0400109E RID: 4254
		public ulong ullTotalPhys;

		// Token: 0x0400109F RID: 4255
		public ulong ullAvailPhys;

		// Token: 0x040010A0 RID: 4256
		public ulong ullTotalPageFile;

		// Token: 0x040010A1 RID: 4257
		public ulong ullAvailPageFile;

		// Token: 0x040010A2 RID: 4258
		public ulong ullTotalVirtual;

		// Token: 0x040010A3 RID: 4259
		public ulong ullAvailVirtual;

		// Token: 0x040010A4 RID: 4260
		public ulong ullAvailExtendedVirtual;
	}
}
