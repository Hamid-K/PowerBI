using System;
using Microsoft.Cloud.Platform.Common;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x02000113 RID: 275
	[EventsKit(5514626914689010572L, Level = EventLevel.Informational)]
	[PublishedEvent]
	public interface IAuditingEventsKit
	{
		// Token: 0x06000767 RID: 1895
		[Event(4976365537381963869L, 1, Level = EventLevel.Informational)]
		void FireEncryptedAuditableEvent(string OrganizationId, string Id, string CreationTime, string RecordType, string Data);

		// Token: 0x06000768 RID: 1896
		[Event(5320681417374429469L, 2, Level = EventLevel.Informational)]
		void FireEncryptedAuditableEventThruOfficeNrt(string OrganizationId, string Id, string CreationTime, string RecordType, string Data);

		// Token: 0x06000769 RID: 1897
		[Event(4081736626371818645L, 3, Level = EventLevel.Informational)]
		void FireAuditableEventThruOfficeNrt(string OrganizationId, string Id, string CreationTime, string RecordType, string Data);
	}
}
