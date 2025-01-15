using System;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration
{
	// Token: 0x0200007B RID: 123
	[Flags]
	internal enum UnconstrainedQueryReasons
	{
		// Token: 0x040002F9 RID: 761
		None = 0,
		// Token: 0x040002FA RID: 762
		StartAtOnVariant = 1,
		// Token: 0x040002FB RID: 763
		UnconstrainedGroup = 2
	}
}
