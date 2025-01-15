using System;

namespace Microsoft.MachineLearning.Training
{
	// Token: 0x02000488 RID: 1160
	[Flags]
	public enum CursOpt : uint
	{
		// Token: 0x04000E8C RID: 3724
		Weight = 1U,
		// Token: 0x04000E8D RID: 3725
		Group = 2U,
		// Token: 0x04000E8E RID: 3726
		Id = 4U,
		// Token: 0x04000E8F RID: 3727
		Label = 8U,
		// Token: 0x04000E90 RID: 3728
		Features = 16U,
		// Token: 0x04000E91 RID: 3729
		AllowBadWeights = 256U,
		// Token: 0x04000E92 RID: 3730
		AllowBadGroups = 512U,
		// Token: 0x04000E93 RID: 3731
		AllowBadLabels = 2048U,
		// Token: 0x04000E94 RID: 3732
		AllowBadFeatures = 4096U,
		// Token: 0x04000E95 RID: 3733
		AllowBadEverything = 6912U,
		// Token: 0x04000E96 RID: 3734
		AllWeights = 257U,
		// Token: 0x04000E97 RID: 3735
		AllGroups = 514U,
		// Token: 0x04000E98 RID: 3736
		AllLabels = 2056U,
		// Token: 0x04000E99 RID: 3737
		AllFeatures = 4112U
	}
}
