using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.InfoNav.Analytics
{
	// Token: 0x02000016 RID: 22
	[ImmutableObject(true)]
	internal sealed class DefaultAnalyticsTelemetryService : ITelemetryService
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00002F7B File Offset: 0x0000117B
		private DefaultAnalyticsTelemetryService()
		{
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002F83 File Offset: 0x00001183
		public T RunInActivity<T>(string activityName, Func<T> action)
		{
			return action();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002F8B File Offset: 0x0000118B
		public void RunInActivity(string activityName, Action action)
		{
			action();
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002F94 File Offset: 0x00001194
		public async Task<T> RunInAsyncActivity<T>(string activityName, Func<Task<T>> action)
		{
			return await action();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002FD8 File Offset: 0x000011D8
		public async Task RunInAsyncActivity(string activityName, Func<Task> action)
		{
			await action();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000301B File Offset: 0x0000121B
		public void FireEvent(string eventName, params object[] args)
		{
		}

		// Token: 0x0400006F RID: 111
		internal static readonly DefaultAnalyticsTelemetryService Instance = new DefaultAnalyticsTelemetryService();
	}
}
