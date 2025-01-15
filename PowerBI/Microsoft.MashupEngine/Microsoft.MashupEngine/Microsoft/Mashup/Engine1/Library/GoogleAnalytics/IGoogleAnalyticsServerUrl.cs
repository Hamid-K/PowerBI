using System;

namespace Microsoft.Mashup.Engine1.Library.GoogleAnalytics
{
	// Token: 0x02000B33 RID: 2867
	internal interface IGoogleAnalyticsServerUrl
	{
		// Token: 0x170018C7 RID: 6343
		// (get) Token: 0x06004F80 RID: 20352
		Uri V1ServerBaseUrl { get; }

		// Token: 0x170018C8 RID: 6344
		// (get) Token: 0x06004F81 RID: 20353
		Uri V2AdminServerBaseUrl { get; }

		// Token: 0x170018C9 RID: 6345
		// (get) Token: 0x06004F82 RID: 20354
		Uri V2DataServerBaseUrl { get; }
	}
}
