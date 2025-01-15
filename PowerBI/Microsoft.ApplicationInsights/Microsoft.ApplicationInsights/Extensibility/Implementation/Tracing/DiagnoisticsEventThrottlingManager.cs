using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing
{
	// Token: 0x02000096 RID: 150
	internal class DiagnoisticsEventThrottlingManager<T> : IDiagnoisticsEventThrottlingManager where T : IDiagnoisticsEventThrottling
	{
		// Token: 0x060004BC RID: 1212 RVA: 0x0001444C File Offset: 0x0001264C
		internal DiagnoisticsEventThrottlingManager(T snapshotContainer, IDiagnoisticsEventThrottlingScheduler scheduler, uint throttlingRecycleIntervalInMinutes)
		{
			if (snapshotContainer == null)
			{
				throw new ArgumentNullException("snapshotContainer");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			if (!throttlingRecycleIntervalInMinutes.IsInRangeThrottlingRecycleInterval())
			{
				throw new ArgumentOutOfRangeException("throttlingRecycleIntervalInMinutes");
			}
			this.snapshotContainer = snapshotContainer;
			int num = (int)(throttlingRecycleIntervalInMinutes * 60U * 1000U);
			scheduler.ScheduleToRunEveryTimeIntervalInMilliseconds(num, new Action(this.ResetThrottling));
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x000144BC File Offset: 0x000126BC
		public bool ThrottleEvent(int eventId, long keywords)
		{
			T t = this.snapshotContainer;
			bool flag2;
			bool flag = t.ThrottleEvent(eventId, keywords, out flag2);
			if (flag2)
			{
				CoreEventSource.Log.DiagnosticsEventThrottlingHasBeenStartedForTheEvent(eventId.ToString(CultureInfo.InvariantCulture), "Incorrect");
			}
			return flag;
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00014500 File Offset: 0x00012700
		private void ResetThrottling()
		{
			T t = this.snapshotContainer;
			foreach (KeyValuePair<int, DiagnoisticsEventCounters> keyValuePair in t.CollectSnapshot())
			{
				CoreEventSource.Log.DiagnosticsEventThrottlingHasBeenResetForTheEvent(keyValuePair.Key, keyValuePair.Value.ExecCount, "Incorrect");
			}
		}

		// Token: 0x040001DD RID: 477
		private readonly T snapshotContainer;
	}
}
