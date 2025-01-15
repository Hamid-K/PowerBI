using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000096 RID: 150
	internal interface ISubordinateObject
	{
		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060008BF RID: 2239
		object Parent { get; }

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x060008C0 RID: 2240
		int Ordinal { get; }

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x060008C1 RID: 2241
		Type Type { get; }
	}
}
