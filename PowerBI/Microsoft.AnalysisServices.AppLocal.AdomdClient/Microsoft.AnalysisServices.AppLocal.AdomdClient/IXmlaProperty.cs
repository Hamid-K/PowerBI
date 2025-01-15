using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000097 RID: 151
	internal interface IXmlaProperty : IXmlaPropertyKey
	{
		// Token: 0x170002AC RID: 684
		// (get) Token: 0x060008CF RID: 2255
		// (set) Token: 0x060008D0 RID: 2256
		object Value { get; set; }

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060008D1 RID: 2257
		// (set) Token: 0x060008D2 RID: 2258
		object Parent { get; set; }
	}
}
