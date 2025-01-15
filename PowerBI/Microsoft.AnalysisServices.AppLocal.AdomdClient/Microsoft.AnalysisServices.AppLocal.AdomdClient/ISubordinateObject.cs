using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000096 RID: 150
	internal interface ISubordinateObject
	{
		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x060008CC RID: 2252
		object Parent { get; }

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x060008CD RID: 2253
		int Ordinal { get; }

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x060008CE RID: 2254
		Type Type { get; }
	}
}
