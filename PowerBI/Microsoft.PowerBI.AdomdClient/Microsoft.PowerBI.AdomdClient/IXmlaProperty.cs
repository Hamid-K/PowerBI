using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000097 RID: 151
	internal interface IXmlaProperty : IXmlaPropertyKey
	{
		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x060008C2 RID: 2242
		// (set) Token: 0x060008C3 RID: 2243
		object Value { get; set; }

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x060008C4 RID: 2244
		// (set) Token: 0x060008C5 RID: 2245
		object Parent { get; set; }
	}
}
