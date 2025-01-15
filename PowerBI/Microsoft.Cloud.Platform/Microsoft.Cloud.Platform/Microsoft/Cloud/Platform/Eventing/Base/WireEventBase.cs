using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Eventing.Base
{
	// Token: 0x020003BA RID: 954
	public abstract class WireEventBase
	{
		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06001D7E RID: 7550 RVA: 0x00070389 File Offset: 0x0006E589
		// (set) Token: 0x06001D7F RID: 7551 RVA: 0x00070391 File Offset: 0x0006E591
		public EventIdentifier Id { get; private set; }

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06001D80 RID: 7552 RVA: 0x0007039A File Offset: 0x0006E59A
		// (set) Token: 0x06001D81 RID: 7553 RVA: 0x000703A2 File Offset: 0x0006E5A2
		public Activity Activity { get; private set; }

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001D82 RID: 7554 RVA: 0x000703AB File Offset: 0x0006E5AB
		// (set) Token: 0x06001D83 RID: 7555 RVA: 0x000703B3 File Offset: 0x0006E5B3
		public Guid? ParentActivityId { get; private set; }

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001D84 RID: 7556 RVA: 0x000703BC File Offset: 0x0006E5BC
		// (set) Token: 0x06001D85 RID: 7557 RVA: 0x000703C4 File Offset: 0x0006E5C4
		public DateTime UtcTimestamp { get; private set; }

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001D86 RID: 7558 RVA: 0x000703CD File Offset: 0x0006E5CD
		// (set) Token: 0x06001D87 RID: 7559 RVA: 0x000703D5 File Offset: 0x0006E5D5
		internal WireEventBase Next { get; set; }

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001D88 RID: 7560 RVA: 0x000703DE File Offset: 0x0006E5DE
		// (set) Token: 0x06001D89 RID: 7561 RVA: 0x000703E6 File Offset: 0x0006E5E6
		internal WireEventBase Prev { get; set; }

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001D8A RID: 7562
		public abstract IEnumerable<EventParameter> EventParameters { get; }

		// Token: 0x06001D8B RID: 7563 RVA: 0x000703EF File Offset: 0x0006E5EF
		internal WireEventBase()
		{
			this.UtcTimestamp = ExtendedDateTime.UtcNow;
		}

		// Token: 0x06001D8C RID: 7564 RVA: 0x00070402 File Offset: 0x0006E602
		protected WireEventBase(Guid eventId, ElementId elementId, Activity activity, Guid? parentActivityId)
			: this(eventId, elementId, activity, parentActivityId, ExtendedDateTime.UtcNow)
		{
		}

		// Token: 0x06001D8D RID: 7565 RVA: 0x00070414 File Offset: 0x0006E614
		protected WireEventBase(Guid eventId, [NotNull] ElementId elementId, [NotNull] Activity activity, Guid? parentActivityId, DateTime timestamp)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<ElementId>(elementId, "elementId");
			ExtendedDiagnostics.EnsureArgumentNotNull<Activity>(activity, "activity");
			this.Id = new EventIdentifier(eventId, elementId);
			this.Activity = activity;
			this.ParentActivityId = parentActivityId;
			this.UtcTimestamp = timestamp.ToUniversalTime();
		}

		// Token: 0x06001D8E RID: 7566 RVA: 0x00070466 File Offset: 0x0006E666
		public long FullIdToEventId()
		{
			return new EventsKitIdentifiers(this.Id.EventId).EventId;
		}

		// Token: 0x06001D8F RID: 7567
		public abstract string ToInvariantCultureString();

		// Token: 0x06001D90 RID: 7568
		public abstract string ToMonitoringString();
	}
}
