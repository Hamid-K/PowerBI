using System;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200001B RID: 27
	[CompatibilityRequirement("1450")]
	public enum PowerBIDataSourceVersion
	{
		// Token: 0x0400007D RID: 125
		PowerBI_V1,
		// Token: 0x0400007E RID: 126
		PowerBI_V2,
		// Token: 0x0400007F RID: 127
		[CompatibilityRequirement("1465")]
		PowerBI_V3
	}
}
