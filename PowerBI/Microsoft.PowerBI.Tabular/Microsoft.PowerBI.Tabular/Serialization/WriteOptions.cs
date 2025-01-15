using System;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x02000192 RID: 402
	[Flags]
	internal enum WriteOptions
	{
		// Token: 0x040004AD RID: 1197
		Default = 0,
		// Token: 0x040004AE RID: 1198
		WriteObjectPath = 1,
		// Token: 0x040004AF RID: 1199
		WriteOriginalNameInPath = 2
	}
}
