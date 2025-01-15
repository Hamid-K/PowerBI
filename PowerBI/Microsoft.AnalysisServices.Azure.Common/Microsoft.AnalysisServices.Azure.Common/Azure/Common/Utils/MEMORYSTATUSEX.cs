using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.Azure.Common.Utils
{
	// Token: 0x02000143 RID: 323
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	internal class MEMORYSTATUSEX
	{
		// Token: 0x06001160 RID: 4448 RVA: 0x00046B90 File Offset: 0x00044D90
		public MEMORYSTATUSEX()
		{
			this.dwLength = (uint)Marshal.SizeOf(typeof(MEMORYSTATUSEX));
		}

		// Token: 0x040003E2 RID: 994
		public uint dwLength;

		// Token: 0x040003E3 RID: 995
		public uint dwMemoryLoad;

		// Token: 0x040003E4 RID: 996
		public ulong ullTotalPhys;

		// Token: 0x040003E5 RID: 997
		public ulong ullAvailPhys;

		// Token: 0x040003E6 RID: 998
		public ulong ullTotalPageFile;

		// Token: 0x040003E7 RID: 999
		public ulong ullAvailPageFile;

		// Token: 0x040003E8 RID: 1000
		public ulong ullTotalVirtual;

		// Token: 0x040003E9 RID: 1001
		public ulong ullAvailVirtual;

		// Token: 0x040003EA RID: 1002
		public ulong ullAvailExtendedVirtual;
	}
}
