using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000093 RID: 147
	internal interface IMetadataObject
	{
		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x060008BD RID: 2237
		AdomdConnection Connection { get; }

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x060008BE RID: 2238
		string SessionId { get; }

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060008BF RID: 2239
		string Catalog { get; }

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x060008C0 RID: 2240
		string CubeName { get; }

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x060008C1 RID: 2241
		string UniqueName { get; }

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x060008C2 RID: 2242
		Type Type { get; }
	}
}
