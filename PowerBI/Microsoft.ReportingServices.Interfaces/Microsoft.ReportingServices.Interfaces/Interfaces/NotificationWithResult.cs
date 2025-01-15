using System;

namespace Microsoft.ReportingServices.Interfaces
{
	// Token: 0x02000032 RID: 50
	public abstract class NotificationWithResult : Notification
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600007A RID: 122
		// (set) Token: 0x0600007B RID: 123
		public abstract string SubscriptionResult { get; set; }
	}
}
