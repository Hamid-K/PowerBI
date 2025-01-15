using System;
using System.Globalization;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000072 RID: 114
	internal sealed class NotificationQueueItem : QueueItem
	{
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x00013784 File Offset: 0x00011984
		// (set) Token: 0x0600047C RID: 1148 RVA: 0x00013791 File Offset: 0x00011991
		public override Guid ID
		{
			get
			{
				return this.Notification.m_NotificationID;
			}
			set
			{
				this.Notification.m_NotificationID = value;
			}
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x000137A0 File Offset: 0x000119A0
		public override string ItemString()
		{
			return string.Format(CultureInfo.InvariantCulture, "TimeEntered: {0}, Type: Notification, SubscriptionID: {1}, IsDataDriven: {2}, Report: {3}", new object[]
			{
				base.TimeEntered,
				this.Notification.m_subscriptionID,
				this.Notification.IsDataDriven,
				this.Notification.m_path
			});
		}

		// Token: 0x04000233 RID: 563
		public NotificationImpl Notification;

		// Token: 0x04000234 RID: 564
		public bool DeleteItem = true;

		// Token: 0x04000235 RID: 565
		public int SecondsBeforeRetry = 300;

		// Token: 0x04000236 RID: 566
		public bool DeleteAsErrorForDataDrivenNotification;
	}
}
