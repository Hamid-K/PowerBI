using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.PowerBI.Analytics.Contracts;
using Microsoft.PowerBI.Telemetry;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x0200002B RID: 43
	internal sealed class DataTransformTelemetryService : Microsoft.PowerBI.Analytics.Contracts.ITelemetryService
	{
		// Token: 0x060000CD RID: 205 RVA: 0x00003B14 File Offset: 0x00001D14
		private DataTransformTelemetryService()
		{
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003B1C File Offset: 0x00001D1C
		public void RunInActivity(string activityName, Action action)
		{
			DataTransformTimedEvent dataTransformTimedEvent = this.StartActivity(activityName);
			try
			{
				action();
			}
			catch (Exception)
			{
				dataTransformTimedEvent.IsError = true;
				throw;
			}
			finally
			{
				this.EndActivity(dataTransformTimedEvent);
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003B68 File Offset: 0x00001D68
		public T RunInActivity<T>(string activityName, Func<T> action)
		{
			T result = default(T);
			this.RunInActivity(activityName, delegate
			{
				result = action();
			});
			return result;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003BA8 File Offset: 0x00001DA8
		public async Task RunInAsyncActivity(string activityName, Func<Task> action)
		{
			DataTransformTimedEvent activity = this.StartActivity(activityName);
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
				this.EndActivity(activity);
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003BFC File Offset: 0x00001DFC
		public async Task<T> RunInAsyncActivity<T>(string activityName, Func<Task<T>> action)
		{
			DataTransformTelemetryService.<>c__DisplayClass5_0<T> CS$<>8__locals1 = new DataTransformTelemetryService.<>c__DisplayClass5_0<T>();
			CS$<>8__locals1.action = action;
			CS$<>8__locals1.result = default(T);
			await this.RunInAsyncActivity(activityName, delegate
			{
				DataTransformTelemetryService.<>c__DisplayClass5_0<T>.<<RunInAsyncActivity>b__0>d <<RunInAsyncActivity>b__0>d;
				<<RunInAsyncActivity>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<RunInAsyncActivity>b__0>d.<>4__this = CS$<>8__locals1;
				<<RunInAsyncActivity>b__0>d.<>1__state = -1;
				<<RunInAsyncActivity>b__0>d.<>t__builder.Start<DataTransformTelemetryService.<>c__DisplayClass5_0<T>.<<RunInAsyncActivity>b__0>d>(ref <<RunInAsyncActivity>b__0>d);
				return <<RunInAsyncActivity>b__0>d.<>t__builder.Task;
			});
			return CS$<>8__locals1.result;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003C50 File Offset: 0x00001E50
		public void FireEvent(string eventName, params object[] args)
		{
			string text = string.Join(",", args);
			TelemetryService.Instance.Log(new DataTransformEvent(eventName, text));
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003C7C File Offset: 0x00001E7C
		private DataTransformTimedEvent StartActivity(string activityName)
		{
			DataTransformTimedEvent dataTransformTimedEvent = new DataTransformTimedEvent(activityName, false);
			TelemetryService.Instance.StartTimedEvent(dataTransformTimedEvent);
			return dataTransformTimedEvent;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003C9D File Offset: 0x00001E9D
		private void EndActivity(DataTransformTimedEvent activity)
		{
			TelemetryService.Instance.EndTimedEvent(activity);
		}

		// Token: 0x040000C8 RID: 200
		public static readonly DataTransformTelemetryService Instance = new DataTransformTelemetryService();
	}
}
