using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Monitoring
{
	// Token: 0x0200009C RID: 156
	public interface IFailureAnalyzer
	{
		// Token: 0x0600045B RID: 1115
		void ReportSuccess(DateTime reportTime, string[] differentiators);

		// Token: 0x0600045C RID: 1116
		void ReportFailure(DateTime reportTime, string[] differentiators, object context);

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600045D RID: 1117
		string StreamId { get; }

		// Token: 0x0600045E RID: 1118
		void SubscribeForStateCalculatedNotifications(IIdentifiable subscriber, WorkTicket subscriberTicket, EventHandler<StateCalculatedEventArgs> callback);

		// Token: 0x0600045F RID: 1119
		void UnsubscribeFromStateCalculatedNotifications(IIdentifiable subscriber);
	}
}
