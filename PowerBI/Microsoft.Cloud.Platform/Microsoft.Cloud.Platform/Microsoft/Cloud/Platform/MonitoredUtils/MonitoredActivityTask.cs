using System;
using System.Threading.Tasks;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x02000139 RID: 313
	public static class MonitoredActivityTask
	{
		// Token: 0x06000834 RID: 2100 RVA: 0x0001BD7C File Offset: 0x00019F7C
		public static Task<T> Start<T>(this IMonitoredActivityCompletionModelFactory model, object caller, TopLevelHandlerOption topLevelHandlerOption, ActivityType activityType, Func<T> action)
		{
			return ExtendedTask.StartTask<T>(caller, topLevelHandlerOption, delegate
			{
				T result = default(T);
				model.CreateSyncActivityAndInvokeWithNewModel(activityType, delegate
				{
					result = action();
				});
				return result;
			});
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0001BDB8 File Offset: 0x00019FB8
		public static Task Start(this IMonitoredActivityCompletionModelFactory model, object caller, TopLevelHandlerOption topLevelHandlerOption, ActivityType activityType, Action action)
		{
			return ExtendedTask.StartTask(caller, topLevelHandlerOption, delegate
			{
				model.CreateSyncActivityAndInvokeWithNewModel(activityType, action);
			});
		}
	}
}
