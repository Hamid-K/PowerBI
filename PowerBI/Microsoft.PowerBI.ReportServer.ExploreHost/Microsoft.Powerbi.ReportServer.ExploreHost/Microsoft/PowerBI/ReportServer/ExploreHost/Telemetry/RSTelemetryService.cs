using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.ReportingServicesHost;
using Microsoft.PowerBI.Telemetry;

namespace Microsoft.PowerBI.ReportServer.ExploreHost.Telemetry
{
	// Token: 0x0200001C RID: 28
	internal sealed class RSTelemetryService : Microsoft.DataShaping.ServiceContracts.ITelemetryService
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x00003A31 File Offset: 0x00001C31
		public RSTelemetryService(RSTelemetryConfiguration telemetryConfiguration)
		{
			this._requestId = telemetryConfiguration.RequestId;
			this._activityId = telemetryConfiguration.ClientSessionId;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003A54 File Offset: 0x00001C54
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

		// Token: 0x060000C6 RID: 198 RVA: 0x00003AA0 File Offset: 0x00001CA0
		public T RunInActivity<T>(ActivityKind activityKind, Func<T> action)
		{
			T result = default(T);
			this.RunInActivity(activityKind, delegate
			{
				result = action();
			});
			return result;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003AE0 File Offset: 0x00001CE0
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

		// Token: 0x060000C8 RID: 200 RVA: 0x00003B34 File Offset: 0x00001D34
		public async Task<T> RunInAsyncActivity<T>(ActivityKind activityKind, Func<Task<T>> action)
		{
			RSTelemetryService.<>c__DisplayClass8_0<T> CS$<>8__locals1 = new RSTelemetryService.<>c__DisplayClass8_0<T>();
			CS$<>8__locals1.action = action;
			CS$<>8__locals1.result = default(T);
			await this.RunInAsyncActivity(activityKind, delegate
			{
				RSTelemetryService.<>c__DisplayClass8_0<T>.<<RunInAsyncActivity>b__0>d <<RunInAsyncActivity>b__0>d;
				<<RunInAsyncActivity>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<RunInAsyncActivity>b__0>d.<>4__this = CS$<>8__locals1;
				<<RunInAsyncActivity>b__0>d.<>1__state = -1;
				<<RunInAsyncActivity>b__0>d.<>t__builder.Start<RSTelemetryService.<>c__DisplayClass8_0<T>.<<RunInAsyncActivity>b__0>d>(ref <<RunInAsyncActivity>b__0>d);
				return <<RunInAsyncActivity>b__0>d.<>t__builder.Task;
			});
			return CS$<>8__locals1.result;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003B87 File Offset: 0x00001D87
		public bool TryGetTelemetryIDs(out string clientActivityId, out string currentActivityId, out string rootActivityId)
		{
			if (string.IsNullOrEmpty(this._activityId) || string.IsNullOrEmpty(this._requestId))
			{
				clientActivityId = null;
				currentActivityId = null;
				rootActivityId = null;
				return false;
			}
			clientActivityId = this._activityId;
			rootActivityId = this._requestId;
			currentActivityId = RSTelemetryService.DefaultActivityId;
			return true;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003BC8 File Offset: 0x00001DC8
		public void FireEvent(DataShapingEvents eventKind, params object[] args)
		{
			EventTarget eventTarget = this.DetermineEventTarget(eventKind);
			TelemetryService.Instance.Log(new DataShapingEvent(eventKind.ToString(), this._requestId, string.Join(",", args), eventTarget));
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003C0C File Offset: 0x00001E0C
		public void FireSanitizedEvent(DataShapingEvents eventKind, params object[] args)
		{
			EventTarget eventTarget = this.DetermineEventTarget(eventKind);
			TelemetryService.Instance.Log(new DataShapingEvent(eventKind.ToString(), this._requestId, string.Join(",", args), eventTarget));
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003C50 File Offset: 0x00001E50
		private DataShapingTimedEvent StartActivity(ActivityKind activityKind)
		{
			DataShapingTimedEvent dataShapingTimedEvent = new DataShapingTimedEvent(activityKind.ToString(), this._requestId, false, EventTarget.LogsOnly);
			TelemetryService.Instance.StartTimedEvent(dataShapingTimedEvent);
			return dataShapingTimedEvent;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003C84 File Offset: 0x00001E84
		private void EndActivity(DataShapingTimedEvent activity)
		{
			TelemetryService.Instance.EndTimedEvent(activity);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003C91 File Offset: 0x00001E91
		private EventTarget DetermineEventTarget(DataShapingEvents eventKind)
		{
			if (!RSTelemetryService.UnlimitedTelemetryLoggingEventsAllowList.Contains(eventKind))
			{
				return EventTarget.LogsOnly;
			}
			return EventTarget.TelemetryAndLogs;
		}

		// Token: 0x04000067 RID: 103
		private readonly string _activityId;

		// Token: 0x04000068 RID: 104
		private readonly string _requestId;

		// Token: 0x04000069 RID: 105
		private static readonly string DefaultActivityId = Guid.Empty.ToString();

		// Token: 0x0400006A RID: 106
		private static readonly HashSet<DataShapingEvents> UnlimitedTelemetryLoggingEventsAllowList = new HashSet<DataShapingEvents> { DataShapingEvents.ExecuteSemanticQueryInfo };
	}
}
