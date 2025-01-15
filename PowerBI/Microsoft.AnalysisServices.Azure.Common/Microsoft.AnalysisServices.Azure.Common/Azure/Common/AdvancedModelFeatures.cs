using System;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000032 RID: 50
	[Flags]
	public enum AdvancedModelFeatures : uint
	{
		// Token: 0x04000097 RID: 151
		None = 0U,
		// Token: 0x04000098 RID: 152
		IncrementalRefresh = 1U,
		// Token: 0x04000099 RID: 153
		CompositeModel = 16U,
		// Token: 0x0400009A RID: 154
		NativeV3 = 256U,
		// Token: 0x0400009B RID: 155
		AutoConvertedV3 = 4096U
	}
}
