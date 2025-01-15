using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200028D RID: 653
	internal struct JOBOBJECT_BASIC_LIMIT_INFORMATION
	{
		// Token: 0x04000675 RID: 1653
		public long PerProcessUserTimeLimit;

		// Token: 0x04000676 RID: 1654
		public long PerJobUserTimeLimit;

		// Token: 0x04000677 RID: 1655
		public uint LimitFlags;

		// Token: 0x04000678 RID: 1656
		public UIntPtr MinimumWorkingSetSize;

		// Token: 0x04000679 RID: 1657
		public UIntPtr MaximumWorkingSetSize;

		// Token: 0x0400067A RID: 1658
		public uint ActiveProcessLimit;

		// Token: 0x0400067B RID: 1659
		public UIntPtr Affinity;

		// Token: 0x0400067C RID: 1660
		public uint PriorityClass;

		// Token: 0x0400067D RID: 1661
		public uint SchedulingClass;
	}
}
