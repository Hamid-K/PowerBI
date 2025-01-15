using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200025D RID: 605
	internal interface INotificationEvent
	{
		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x0600145E RID: 5214
		string CacheName { get; }

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x0600145F RID: 5215
		string RegionName { get; }

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06001460 RID: 5216
		string Key { get; }

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001461 RID: 5217
		ReqType EventType { get; }

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001462 RID: 5218
		object Value { get; }
	}
}
