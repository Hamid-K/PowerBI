using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200028E RID: 654
	internal struct JOBOBJECT_EXTENDED_LIMIT_INFORMATION
	{
		// Token: 0x0400067E RID: 1662
		public JOBOBJECT_BASIC_LIMIT_INFORMATION BasicLimitInformation;

		// Token: 0x0400067F RID: 1663
		public IO_COUNTERS IoInfo;

		// Token: 0x04000680 RID: 1664
		public UIntPtr ProcessMemoryLimit;

		// Token: 0x04000681 RID: 1665
		public UIntPtr JobMemoryLimit;

		// Token: 0x04000682 RID: 1666
		public UIntPtr PeakProcessMemoryUsed;

		// Token: 0x04000683 RID: 1667
		public UIntPtr PeakJobMemoryUsed;
	}
}
