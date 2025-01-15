using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.Telemetry;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x02000039 RID: 57
	internal sealed class DataShapingTelemetryService : Microsoft.DataShaping.ServiceContracts.ITelemetryService
	{
		// Token: 0x0600012B RID: 299 RVA: 0x00004681 File Offset: 0x00002881
		private DataShapingTelemetryService(TelemetrySettings telemetrySettings, string requestId)
		{
			this.m_telemetrySettings = telemetrySettings;
			this.m_requestId = requestId;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00004698 File Offset: 0x00002898
		public static DataShapingTelemetryService CreateForRequest(TelemetrySettings telemetrySettings)
		{
			if (telemetrySettings == null)
			{
				return new DataShapingTelemetryService(null, null);
			}
			string text = Guid.NewGuid().ToString();
			TelemetryService.Instance.TraceInfo("Created DataShapingTelemetryService with ClientActivityId:" + telemetrySettings.ClientActivityId + ", RequestId:" + text);
			return new DataShapingTelemetryService(telemetrySettings, text);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000046EC File Offset: 0x000028EC
		public void RunInActivity(ActivityKind activityKind, Action action)
		{
			DataShapingTimedEvent dataShapingTimedEvent = this.StartActivity(activityKind);
			try
			{
				action();
			}
			catch (Exception)
			{
				dataShapingTimedEvent.isError = true;
				throw;
			}
			finally
			{
				this.EndActivity(dataShapingTimedEvent);
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00004738 File Offset: 0x00002938
		public T RunInActivity<T>(ActivityKind activityKind, Func<T> action)
		{
			T result = default(T);
			this.RunInActivity(activityKind, delegate
			{
				result = action();
			});
			return result;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00004778 File Offset: 0x00002978
		public async Task RunInAsyncActivity(ActivityKind activityKind, Func<Task> action)
		{
			DataShapingTimedEvent activity = this.StartActivity(activityKind);
			try
			{
				await action();
			}
			catch (Exception)
			{
				activity.isError = true;
				throw;
			}
			finally
			{
				this.EndActivity(activity);
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000047CC File Offset: 0x000029CC
		public async Task<T> RunInAsyncActivity<T>(ActivityKind activityKind, Func<Task<T>> action)
		{
			DataShapingTelemetryService.<>c__DisplayClass10_0<T> CS$<>8__locals1 = new DataShapingTelemetryService.<>c__DisplayClass10_0<T>();
			CS$<>8__locals1.action = action;
			CS$<>8__locals1.result = default(T);
			await this.RunInAsyncActivity(activityKind, delegate
			{
				DataShapingTelemetryService.<>c__DisplayClass10_0<T>.<<RunInAsyncActivity>b__0>d <<RunInAsyncActivity>b__0>d;
				<<RunInAsyncActivity>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<RunInAsyncActivity>b__0>d.<>4__this = CS$<>8__locals1;
				<<RunInAsyncActivity>b__0>d.<>1__state = -1;
				<<RunInAsyncActivity>b__0>d.<>t__builder.Start<DataShapingTelemetryService.<>c__DisplayClass10_0<T>.<<RunInAsyncActivity>b__0>d>(ref <<RunInAsyncActivity>b__0>d);
				return <<RunInAsyncActivity>b__0>d.<>t__builder.Task;
			});
			return CS$<>8__locals1.result;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00004820 File Offset: 0x00002A20
		public void FireEvent(DataShapingEvents eventKind, params object[] args)
		{
			EventTarget eventTarget = this.DetermineEventTarget(eventKind);
			TelemetryService.Instance.Log(new DataShapingEvent(eventKind.ToString(), this.m_requestId, string.Join(",", args), eventTarget));
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00004864 File Offset: 0x00002A64
		public void FireSanitizedEvent(DataShapingEvents eventKind, params object[] args)
		{
			EventTarget eventTarget = this.DetermineEventTarget(eventKind);
			TelemetryService.Instance.Log(new DataShapingEvent(eventKind.ToString(), this.m_requestId, string.Join(",", args), eventTarget));
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000048A8 File Offset: 0x00002AA8
		public bool TryGetTelemetryIDs(out string clientActivityId, out string currentActivityId, out string rootActivityId)
		{
			if (this.m_telemetrySettings == null || string.IsNullOrEmpty(this.m_telemetrySettings.ClientActivityId) || string.IsNullOrEmpty(this.m_requestId))
			{
				clientActivityId = null;
				currentActivityId = null;
				rootActivityId = null;
				return false;
			}
			clientActivityId = this.m_telemetrySettings.ClientActivityId;
			rootActivityId = this.m_requestId;
			currentActivityId = DataShapingTelemetryService.DefaultActivityId;
			return true;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00004904 File Offset: 0x00002B04
		private DataShapingTimedEvent StartActivity(ActivityKind activityKind)
		{
			DataShapingTimedEvent dataShapingTimedEvent = new DataShapingTimedEvent(activityKind.ToString(), this.m_requestId, false, EventTarget.LogsOnly);
			TelemetryService.Instance.StartTimedEvent(dataShapingTimedEvent);
			return dataShapingTimedEvent;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004938 File Offset: 0x00002B38
		private void EndActivity(DataShapingTimedEvent activity)
		{
			TelemetryService.Instance.EndTimedEvent(activity);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00004945 File Offset: 0x00002B45
		private EventTarget DetermineEventTarget(DataShapingEvents eventKind)
		{
			if (!DataShapingTelemetryService.UnlimitedTelemetryLoggingEventsAllowList.Contains(eventKind))
			{
				return EventTarget.LogsOnly;
			}
			return EventTarget.TelemetryAndLogs;
		}

		// Token: 0x040000EF RID: 239
		public static readonly DataShapingTelemetryService Instance = new DataShapingTelemetryService(null, null);

		// Token: 0x040000F0 RID: 240
		private static readonly string DefaultActivityId = Guid.Empty.ToString();

		// Token: 0x040000F1 RID: 241
		private static readonly HashSet<DataShapingEvents> UnlimitedTelemetryLoggingEventsAllowList = new HashSet<DataShapingEvents> { DataShapingEvents.ExecuteSemanticQueryInfo };

		// Token: 0x040000F2 RID: 242
		private readonly TelemetrySettings m_telemetrySettings;

		// Token: 0x040000F3 RID: 243
		private readonly string m_requestId;
	}
}
