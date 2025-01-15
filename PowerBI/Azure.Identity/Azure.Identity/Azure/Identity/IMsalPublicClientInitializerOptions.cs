using System;
using Microsoft.Identity.Client;

namespace Azure.Identity
{
	// Token: 0x02000064 RID: 100
	[Friend("Azure.Identity.Broker")]
	internal interface IMsalPublicClientInitializerOptions
	{
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000389 RID: 905
		Action<PublicClientApplicationBuilder> BeforeBuildClient { get; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600038A RID: 906
		// (set) Token: 0x0600038B RID: 907
		bool UseDefaultBrokerAccount { get; set; }
	}
}
