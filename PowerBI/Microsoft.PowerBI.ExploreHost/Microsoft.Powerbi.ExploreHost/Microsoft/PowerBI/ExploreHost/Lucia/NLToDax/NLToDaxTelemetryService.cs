using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.PowerBI.NaturalLanguage.NLToDax;
using Microsoft.PowerBI.Telemetry;

namespace Microsoft.PowerBI.ExploreHost.Lucia.NLToDax
{
	// Token: 0x02000077 RID: 119
	internal class NLToDaxTelemetryService : ITelemetryService
	{
		// Token: 0x06000342 RID: 834 RVA: 0x0000A76B File Offset: 0x0000896B
		private NLToDaxTelemetryService()
		{
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000A774 File Offset: 0x00008974
		public void RunActivity(string activityName, Action action)
		{
			NLToDaxRuntimeTimedEvent nltoDaxRuntimeTimedEvent = NLToDaxTelemetryService.StartActivity(activityName);
			Stopwatch stopwatch = Stopwatch.StartNew();
			try
			{
				action();
			}
			catch (Exception)
			{
				nltoDaxRuntimeTimedEvent.IsError = true;
				throw;
			}
			finally
			{
				stopwatch.Stop();
				nltoDaxRuntimeTimedEvent.Duration = stopwatch.ElapsedMilliseconds;
				NLToDaxTelemetryService.EndActivity(nltoDaxRuntimeTimedEvent);
			}
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000A7D8 File Offset: 0x000089D8
		public T RunActivity<T>(string activityName, Func<T> action)
		{
			T result = default(T);
			this.RunActivity(activityName, delegate
			{
				result = action();
			});
			return result;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000A818 File Offset: 0x00008A18
		public async Task RunActivityAsync(string activityName, Func<Task> action)
		{
			NLToDaxRuntimeTimedEvent activity = NLToDaxTelemetryService.StartActivity(activityName);
			Stopwatch stopwatch = Stopwatch.StartNew();
			try
			{
				await action();
			}
			catch (Exception)
			{
				activity.IsError = true;
				throw;
			}
			finally
			{
				stopwatch.Stop();
				activity.Duration = stopwatch.ElapsedMilliseconds;
				NLToDaxTelemetryService.EndActivity(activity);
			}
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000A864 File Offset: 0x00008A64
		public async Task<T> RunActivityAsync<T>(string activityName, Func<Task<T>> action)
		{
			NLToDaxTelemetryService.<>c__DisplayClass5_0<T> CS$<>8__locals1 = new NLToDaxTelemetryService.<>c__DisplayClass5_0<T>();
			CS$<>8__locals1.action = action;
			CS$<>8__locals1.result = default(T);
			await this.RunActivityAsync(activityName, delegate
			{
				NLToDaxTelemetryService.<>c__DisplayClass5_0<T>.<<RunActivityAsync>b__0>d <<RunActivityAsync>b__0>d;
				<<RunActivityAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<RunActivityAsync>b__0>d.<>4__this = CS$<>8__locals1;
				<<RunActivityAsync>b__0>d.<>1__state = -1;
				<<RunActivityAsync>b__0>d.<>t__builder.Start<NLToDaxTelemetryService.<>c__DisplayClass5_0<T>.<<RunActivityAsync>b__0>d>(ref <<RunActivityAsync>b__0>d);
				return <<RunActivityAsync>b__0>d.<>t__builder.Task;
			});
			return CS$<>8__locals1.result;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000A8B8 File Offset: 0x00008AB8
		public void FireEvent(string eventName, params object[] args)
		{
			string text = string.Join(",", args);
			TelemetryService.Instance.Log(new NLToDaxRuntimeTimedEvent(eventName, text));
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000A8E4 File Offset: 0x00008AE4
		private static NLToDaxRuntimeTimedEvent StartActivity(string activityName)
		{
			NLToDaxRuntimeTimedEvent nltoDaxRuntimeTimedEvent = new NLToDaxRuntimeTimedEvent(activityName, false);
			TelemetryService.Instance.StartTimedEvent(nltoDaxRuntimeTimedEvent);
			return nltoDaxRuntimeTimedEvent;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000A905 File Offset: 0x00008B05
		private static void EndActivity(NLToDaxRuntimeTimedEvent activity)
		{
			TelemetryService.Instance.EndTimedEvent(activity);
		}

		// Token: 0x04000178 RID: 376
		public static readonly NLToDaxTelemetryService Instance = new NLToDaxTelemetryService();
	}
}
