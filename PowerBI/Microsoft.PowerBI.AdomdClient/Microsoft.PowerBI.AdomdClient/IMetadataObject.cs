using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000093 RID: 147
	internal interface IMetadataObject
	{
		// Token: 0x1700029B RID: 667
		// (get) Token: 0x060008B0 RID: 2224
		AdomdConnection Connection { get; }

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x060008B1 RID: 2225
		string SessionId { get; }

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x060008B2 RID: 2226
		string Catalog { get; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x060008B3 RID: 2227
		string CubeName { get; }

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x060008B4 RID: 2228
		string UniqueName { get; }

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x060008B5 RID: 2229
		Type Type { get; }
	}
}
