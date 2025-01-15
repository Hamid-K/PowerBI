using System;

namespace Microsoft.AnalysisServices.AdomdClient.Hosting
{
	// Token: 0x02000162 RID: 354
	internal interface IConnectivityOwner
	{
		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001121 RID: 4385
		AccessToken AccessToken { get; }

		// Token: 0x06001122 RID: 4386
		void RefreshAccessToken();
	}
}
