using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Microsoft.InfoNav.Defaults
{
	// Token: 0x0200003B RID: 59
	[ImmutableObject(true)]
	public sealed class DefaultMonitoredActivityFactory : IMonitoredActivityFactory
	{
		// Token: 0x060002A4 RID: 676 RVA: 0x000082B3 File Offset: 0x000064B3
		private DefaultMonitoredActivityFactory()
		{
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x000082BB File Offset: 0x000064BB
		public void CreateAndInvokeActivitySync(IActivityType activityType, MonitoredActivityAction action)
		{
			action();
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x000082C3 File Offset: 0x000064C3
		public Task CreateAndInvokeActivityAsync(IActivityType activityType, Func<Task> action)
		{
			return action();
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x000082CB File Offset: 0x000064CB
		public Task<T> CreateAndInvokeActivityAsync<T>(IActivityType activityType, Func<Task<T>> action)
		{
			return action();
		}

		// Token: 0x04000097 RID: 151
		public static readonly DefaultMonitoredActivityFactory Instance = new DefaultMonitoredActivityFactory();
	}
}
