using System;
using System.Collections.Generic;
using Microsoft.PowerBI.Telemetry;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x02000063 RID: 99
	internal sealed class ExploreTelemetryService : IExploreTelemetryService
	{
		// Token: 0x06000235 RID: 565 RVA: 0x00006374 File Offset: 0x00004574
		public ExploreTelemetryService(ITelemetryService telemetryService)
		{
			this._telemetryService = telemetryService;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00006384 File Offset: 0x00004584
		public void RunInActivity(string activityName, Action<ExploreBaseEvent> action)
		{
			ExploreTimedEvent exploreTimedEvent = this.StartActivity(activityName);
			try
			{
				action(exploreTimedEvent);
			}
			catch (Exception)
			{
				exploreTimedEvent.isError = true;
				throw;
			}
			finally
			{
				this.EndActivity(exploreTimedEvent);
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x000063D0 File Offset: 0x000045D0
		public T RunInActivity<T>(string activityName, Func<ExploreBaseEvent, T> action)
		{
			T result = default(T);
			this.RunInActivity(activityName, delegate(ExploreBaseEvent activity)
			{
				result = action(activity);
			});
			return result;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00006410 File Offset: 0x00004610
		private ExploreTimedEvent StartActivity(string activityName)
		{
			EventTarget eventTarget = this.DetermineEventTarget(activityName);
			ExploreTimedEvent exploreTimedEvent = new ExploreTimedEvent(activityName.ToString(), false, eventTarget);
			this._telemetryService.StartTimedEvent(exploreTimedEvent);
			return exploreTimedEvent;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00006440 File Offset: 0x00004640
		private void EndActivity(ExploreTimedEvent activity)
		{
			this._telemetryService.EndTimedEvent(activity);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000644E File Offset: 0x0000464E
		private EventTarget DetermineEventTarget(string activityKind)
		{
			if (!ExploreTelemetryService.UnlimitedTelemetryLoggingEventsAllowList.Contains(activityKind))
			{
				return EventTarget.LogsOnly;
			}
			return EventTarget.TelemetryAndLogs;
		}

		// Token: 0x04000157 RID: 343
		private static readonly HashSet<string> UnlimitedTelemetryLoggingEventsAllowList = new HashSet<string> { "GetModelMetadata", "GetSchemaDataSet" };

		// Token: 0x04000158 RID: 344
		private readonly ITelemetryService _telemetryService;
	}
}
