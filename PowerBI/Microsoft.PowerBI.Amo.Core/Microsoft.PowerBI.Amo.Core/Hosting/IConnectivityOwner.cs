using System;

namespace Microsoft.AnalysisServices.Hosting
{
	// Token: 0x02000158 RID: 344
	internal interface IConnectivityOwner
	{
		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x060011B0 RID: 4528
		AccessToken AccessToken { get; }

		// Token: 0x060011B1 RID: 4529
		void RefreshAccessToken();
	}
}
