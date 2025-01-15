using System;
using Microsoft.Cloud.Platform.EventsKit;

namespace Microsoft.Cloud.Platform.RequestProtection
{
	// Token: 0x02000469 RID: 1129
	public interface ISPRequestProtectionContextGenerator
	{
		// Token: 0x06002332 RID: 9010
		IRequestProtectionContext GetContext(string siteId, string webId, IEventsKitFactory eventsKitFactory);

		// Token: 0x06002333 RID: 9011
		IRequestProtectionContext GetContext(IEventsKitFactory eventsKitFactory);
	}
}
