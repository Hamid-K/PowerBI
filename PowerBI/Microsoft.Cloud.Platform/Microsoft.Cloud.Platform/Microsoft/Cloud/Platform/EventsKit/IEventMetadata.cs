using System;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Eventing.Base;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x02000347 RID: 839
	public interface IEventMetadata
	{
		// Token: 0x17000357 RID: 855
		// (get) Token: 0x060018E1 RID: 6369
		EventsKitIdentifiers Id { get; }

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x060018E2 RID: 6370
		EventLevel Level { get; }

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x060018E3 RID: 6371
		bool IsPublishedEvent { get; }

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x060018E4 RID: 6372
		bool IsPublishedToEventingServer { get; }

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x060018E5 RID: 6373
		bool IsPublishedToEtw { get; }

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x060018E6 RID: 6374
		bool IsAuditEvent { get; }

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x060018E7 RID: 6375
		MethodInfo EventMethod { get; }

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x060018E8 RID: 6376
		int Order { get; }

		// Token: 0x060018E9 RID: 6377
		string GetFriendlyName();

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x060018EA RID: 6378
		bool IsWindowsEventLog { get; }

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x060018EB RID: 6379
		bool IsFilteredWindowsEventLog { get; }

		// Token: 0x060018EC RID: 6380
		int GetWindowsEventLogId();

		// Token: 0x060018ED RID: 6381
		EventLogEntryType GetWindowsEventLogEntrySeverity();

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x060018EE RID: 6382
		EventPurpose EventTypes { get; }

		// Token: 0x060018EF RID: 6383
		string GetSchemaAsString();

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x060018F0 RID: 6384
		string Format { get; }
	}
}
