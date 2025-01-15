using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Microsoft.InfoNav
{
	// Token: 0x02000009 RID: 9
	internal static class ActivityUtils
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000020FD File Offset: 0x000002FD
		internal static Task<ActivityResult<T>> MonitorAndHandleExceptions<T>(ITracer tracer, string taskName, Func<Task<T>> task, bool useSanitizedTracing = false)
		{
			return ActivityUtils.MonitorCore<T>(tracer, taskName, task, true, useSanitizedTracing);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000210C File Offset: 0x0000030C
		internal static ActivityResult<T> MonitorAndHandleExceptions<T>(ITracer tracer, string taskName, Func<T> task, bool useSanitizedTracing = false)
		{
			return ActivityUtils.MonitorCore<T>(tracer, taskName, () => Task.FromResult<T>(task()), true, useSanitizedTracing).Result;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002140 File Offset: 0x00000340
		internal static async Task<T> Monitor<T>(ITracer tracer, string taskName, Func<Task<T>> task, bool useSanitizedTracing = false)
		{
			return (await ActivityUtils.MonitorCore<T>(tracer, taskName, task, false, useSanitizedTracing)).Value;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000219C File Offset: 0x0000039C
		internal static Task<ActivityResult<bool>> MonitorAndHandleExceptions(ITracer tracer, string taskName, Func<Task> task, bool useSanitizedTracing = false)
		{
			ActivityUtils.<>c__DisplayClass3_0 CS$<>8__locals1 = new ActivityUtils.<>c__DisplayClass3_0();
			CS$<>8__locals1.task = task;
			return ActivityUtils.MonitorAndHandleExceptions<bool>(tracer, taskName, delegate
			{
				ActivityUtils.<>c__DisplayClass3_0.<<MonitorAndHandleExceptions>b__0>d <<MonitorAndHandleExceptions>b__0>d;
				<<MonitorAndHandleExceptions>b__0>d.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
				<<MonitorAndHandleExceptions>b__0>d.<>4__this = CS$<>8__locals1;
				<<MonitorAndHandleExceptions>b__0>d.<>1__state = -1;
				<<MonitorAndHandleExceptions>b__0>d.<>t__builder.Start<ActivityUtils.<>c__DisplayClass3_0.<<MonitorAndHandleExceptions>b__0>d>(ref <<MonitorAndHandleExceptions>b__0>d);
				return <<MonitorAndHandleExceptions>b__0>d.<>t__builder.Task;
			}, useSanitizedTracing);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021CC File Offset: 0x000003CC
		private static async Task<ActivityResult<T>> MonitorCore<T>(ITracer tracer, string taskName, Func<Task<T>> task, bool handleExceptions, bool useSanitizedTracing)
		{
			Stopwatch stopWatch = Stopwatch.StartNew();
			ActivityResult<T> activityResult;
			try
			{
				ActivityUtils.Trace(tracer, TraceLevel.Info, "Starting activity `" + taskName + "`", useSanitizedTracing);
				T t = await task();
				stopWatch.Stop();
				ActivityUtils.Trace(tracer, TraceLevel.Info, string.Format("Successfully completed activity `{0}` (duration: {1})", taskName, stopWatch.Elapsed), useSanitizedTracing);
				activityResult = ActivityResult<T>.FromValue(t);
			}
			catch (Exception ex) when (!ex.IsStoppingException())
			{
				stopWatch.Stop();
				ActivityUtils.Trace(tracer, TraceLevel.Error, string.Format("Failed to complete activity `{0}` (duration: {1}): ExceptionType: {2}-StackTrace: {3}", new object[]
				{
					taskName,
					stopWatch.Elapsed,
					ex.GetType(),
					ex.StackTrace
				}), useSanitizedTracing);
				if (!handleExceptions)
				{
					throw;
				}
				activityResult = ActivityResult<T>.FromException(ex);
			}
			return activityResult;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002230 File Offset: 0x00000430
		private static void Trace(ITracer tracer, TraceLevel level, string message, bool useSanitizedTracing)
		{
			switch (level)
			{
			case TraceLevel.Error:
				if (useSanitizedTracing)
				{
					tracer.SanitizedTraceError(message, new string[0]);
					return;
				}
				tracer.TraceError(message);
				return;
			case TraceLevel.Warning:
				if (useSanitizedTracing)
				{
					tracer.SanitizedTraceWarning(message, new string[0]);
					return;
				}
				tracer.TraceWarning(message);
				return;
			case TraceLevel.Info:
				if (useSanitizedTracing)
				{
					tracer.SanitizedTraceInformation(message, new string[0]);
					return;
				}
				tracer.TraceInformation(message);
				return;
			default:
				throw new ArgumentException(string.Format("Unsupported trace level {0}", level));
			}
		}
	}
}
