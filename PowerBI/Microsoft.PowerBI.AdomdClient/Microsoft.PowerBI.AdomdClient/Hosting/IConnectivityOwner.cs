using System;

namespace Microsoft.AnalysisServices.AdomdClient.Hosting
{
	// Token: 0x02000162 RID: 354
	internal interface IConnectivityOwner
	{
		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001114 RID: 4372
		AccessToken AccessToken { get; }

		// Token: 0x06001115 RID: 4373
		void RefreshAccessToken();
	}
}
