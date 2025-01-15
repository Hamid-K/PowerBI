using System;
using Microsoft.MachineLearning.CommandLine;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x020004D5 RID: 1237
	public enum Command
	{
		// Token: 0x04000F3A RID: 3898
		TrainTest,
		// Token: 0x04000F3B RID: 3899
		Train,
		// Token: 0x04000F3C RID: 3900
		Test,
		// Token: 0x04000F3D RID: 3901
		CrossValidation,
		// Token: 0x04000F3E RID: 3902
		FeatureSelection,
		// Token: 0x04000F3F RID: 3903
		CreateInstances,
		// Token: 0x04000F40 RID: 3904
		Sweep,
		// Token: 0x04000F41 RID: 3905
		[HideEnumValue]
		_Lim,
		// Token: 0x04000F42 RID: 3906
		[HideEnumValue]
		CV = 3
	}
}
