using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Eventing.Base;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Eventing
{
	// Token: 0x02000398 RID: 920
	public class EventsQueryFilter
	{
		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001C56 RID: 7254 RVA: 0x0006C06D File Offset: 0x0006A26D
		// (set) Token: 0x06001C57 RID: 7255 RVA: 0x0006C075 File Offset: 0x0006A275
		public long? EventId { get; set; }

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001C58 RID: 7256 RVA: 0x0006C07E File Offset: 0x0006A27E
		// (set) Token: 0x06001C59 RID: 7257 RVA: 0x0006C086 File Offset: 0x0006A286
		public HashSet<long> EventIdsSet { get; set; }

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06001C5A RID: 7258 RVA: 0x0006C08F File Offset: 0x0006A28F
		// (set) Token: 0x06001C5B RID: 7259 RVA: 0x0006C097 File Offset: 0x0006A297
		public Guid? ActivityId { get; set; }

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06001C5C RID: 7260 RVA: 0x0006C0A0 File Offset: 0x0006A2A0
		// (set) Token: 0x06001C5D RID: 7261 RVA: 0x0006C0A8 File Offset: 0x0006A2A8
		public Guid? RootActivityId { get; set; }

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06001C5E RID: 7262 RVA: 0x0006C0B1 File Offset: 0x0006A2B1
		// (set) Token: 0x06001C5F RID: 7263 RVA: 0x0006C0B9 File Offset: 0x0006A2B9
		public string ClientActivityId { get; set; }

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06001C60 RID: 7264 RVA: 0x0006C0C2 File Offset: 0x0006A2C2
		// (set) Token: 0x06001C61 RID: 7265 RVA: 0x0006C0CA File Offset: 0x0006A2CA
		public EventLevel? Level { get; set; }

		// Token: 0x06001C62 RID: 7266 RVA: 0x0006C0D4 File Offset: 0x0006A2D4
		public bool IsMatch([NotNull] EtwEvent etwEvent)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<EtwEvent>(etwEvent, "etwEvent");
			return (this.EventId == null || this.EventId.Value == etwEvent.EventId) && (this.EventIdsSet == null || this.EventIdsSet.Contains(etwEvent.EventId)) && (this.ActivityId == null || !(this.ActivityId.Value != etwEvent.Activity.ActivityId)) && (this.RootActivityId == null || !(this.RootActivityId.Value != etwEvent.Activity.RootActivityId)) && (string.IsNullOrEmpty(this.ClientActivityId) || string.Equals(etwEvent.Activity.ClientActivityId, this.ClientActivityId, StringComparison.OrdinalIgnoreCase)) && (this.Level == null || this.Level.Value == etwEvent.Level);
		}

		// Token: 0x0400098D RID: 2445
		public static EventsQueryFilter Empty = new EventsQueryFilter();
	}
}
