using System;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x020004D6 RID: 1238
	[Flags]
	public enum FilterSet
	{
		// Token: 0x04000F44 RID: 3908
		TrainTest = 1,
		// Token: 0x04000F45 RID: 3909
		Train = 2,
		// Token: 0x04000F46 RID: 3910
		Test = 4,
		// Token: 0x04000F47 RID: 3911
		CV = 8,
		// Token: 0x04000F48 RID: 3912
		Sweep = 64,
		// Token: 0x04000F49 RID: 3913
		Output = 128,
		// Token: 0x04000F4A RID: 3914
		Run = 256,
		// Token: 0x04000F4B RID: 3915
		AnyTrain = 11,
		// Token: 0x04000F4C RID: 3916
		AnyTest = 13,
		// Token: 0x04000F4D RID: 3917
		AnyCreateModel = 11,
		// Token: 0x04000F4E RID: 3918
		Any = 15
	}
}
