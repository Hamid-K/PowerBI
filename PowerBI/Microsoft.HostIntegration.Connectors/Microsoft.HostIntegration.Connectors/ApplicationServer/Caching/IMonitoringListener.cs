using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000AB RID: 171
	internal interface IMonitoringListener
	{
		// Token: 0x06000409 RID: 1033
		void Listen(Action innerDelegate);

		// Token: 0x0600040A RID: 1034
		TResult Listen<TResult>(Func<TResult> innerDelegate);

		// Token: 0x0600040B RID: 1035
		void AddTrackerInfo(IRequestTracker tracker);

		// Token: 0x0600040C RID: 1036
		bool IsRequestTrackingSupported();
	}
}
