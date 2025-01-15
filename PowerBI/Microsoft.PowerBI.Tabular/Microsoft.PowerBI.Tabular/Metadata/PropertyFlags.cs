using System;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x02000202 RID: 514
	[Flags]
	internal enum PropertyFlags
	{
		// Token: 0x040006A9 RID: 1705
		None = 0,
		// Token: 0x040006AA RID: 1706
		Ddl = 1,
		// Token: 0x040006AB RID: 1707
		User = 2,
		// Token: 0x040006AC RID: 1708
		ReadOnly = 4,
		// Token: 0x040006AD RID: 1709
		DdlAndUser = 3,
		// Token: 0x040006AE RID: 1710
		Json = 8,
		// Token: 0x040006AF RID: 1711
		ModelReference = 16
	}
}
