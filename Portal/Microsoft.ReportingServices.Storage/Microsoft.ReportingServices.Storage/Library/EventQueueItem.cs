using System;
using System.Globalization;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200000C RID: 12
	internal sealed class EventQueueItem : QueueItem
	{
		// Token: 0x06000085 RID: 133 RVA: 0x00004820 File Offset: 0x00002A20
		public override string ItemString()
		{
			string text = "EventData";
			if (string.Compare(this.EventType, "SharedSchedule", StringComparison.OrdinalIgnoreCase) == 0)
			{
				text = "ScheduleID";
			}
			else if (string.Compare(this.EventType, "DataDrivenSubscription", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(this.EventType, "TimedSubscription", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(this.EventType, "RefreshCache", StringComparison.OrdinalIgnoreCase) == 0)
			{
				text = "SubscriptionID";
			}
			else if (string.Compare(this.EventType, "ReportHistorySchedule", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(this.EventType, "ReportExecutionUpdateSchedule", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(this.EventType, "SnapshotUpdated", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(this.EventType, "CacheInvalidateSchedule", StringComparison.OrdinalIgnoreCase) == 0)
			{
				text = "ReportID";
			}
			return string.Format(CultureInfo.InvariantCulture, "TimeEntered: {0}, Type: Event, EventType: {1}, {2}: {3}", new object[] { base.TimeEntered, this.EventType, text, this.EventData });
		}

		// Token: 0x0400007D RID: 125
		public string EventType;

		// Token: 0x0400007E RID: 126
		public string EventData;
	}
}
