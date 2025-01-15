using System;
using System.ServiceModel.Channels;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002AE RID: 686
	internal interface IChannelContainer
	{
		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001926 RID: 6438
		IDuplexSessionChannel Channel { get; }

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001927 RID: 6439
		RemoteAuthorization Authorization { get; }

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001928 RID: 6440
		// (set) Token: 0x06001929 RID: 6441
		ClientVersionInfo RemoteVersionInfo { get; set; }

		// Token: 0x0600192A RID: 6442
		T GetProperty<T>() where T : class;

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x0600192B RID: 6443
		CacheConnectionProperty ConnectionProperty { get; }
	}
}
