using System;
using System.Collections.Generic;
using System.Globalization;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Monitoring
{
	// Token: 0x02000095 RID: 149
	internal class History
	{
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x0000F3B8 File Offset: 0x0000D5B8
		private int LastCalculatedValue
		{
			get
			{
				int count = this.m_calculatedValues.Count;
				if (count != 0)
				{
					return this.m_calculatedValues[count - 1];
				}
				return 0;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x0000F3E4 File Offset: 0x0000D5E4
		// (set) Token: 0x06000434 RID: 1076 RVA: 0x0000F3EC File Offset: 0x0000D5EC
		public object FailureContext { get; private set; }

		// Token: 0x06000435 RID: 1077 RVA: 0x0000F3F8 File Offset: 0x0000D5F8
		public History(DateTime startTime, int alarmThreshold, int maxPeriodsInHistory, double successTolerance, int trafficQuotaPerPeriod, int faultThresholdPerPeriod, TimeSpan timePeriodLength)
		{
			ExtendedDiagnostics.EnsureArgumentIsPositive(alarmThreshold, "alarmThreshold");
			ExtendedDiagnostics.EnsureArgumentIsPositive(trafficQuotaPerPeriod, "trafficQuotaPerPeriod");
			ExtendedDiagnostics.EnsureArgumentIsPositive(maxPeriodsInHistory, "maxPeriodsInHistory");
			ExtendedDiagnostics.EnsureArgumentIsPositive(maxPeriodsInHistory, "faultThresholdPerPeriod");
			ExtendedDiagnostics.EnsureArgumentIsNotNegative((int)Math.Ceiling(successTolerance), "successTolerance");
			ExtendedDiagnostics.EnsureArgumentIsPositive(TimeSpan.Compare(timePeriodLength, TimeSpan.MinValue), "timePeriodLength");
			ExtendedDiagnostics.EnsureArgument(maxPeriodsInHistory, "alarmThreshold", maxPeriodsInHistory >= alarmThreshold);
			this.m_normalizedAlarmThreshold = alarmThreshold * trafficQuotaPerPeriod;
			this.m_maxPeriodsInHistory = maxPeriodsInHistory;
			this.m_maxTrafficRepresented = this.m_normalizedAlarmThreshold + (int)Math.Ceiling(2.0 * successTolerance * (double)trafficQuotaPerPeriod);
			this.m_calculatedValues = new List<int>();
			this.m_closeTimePeriodsLock = new object();
			this.m_currentTimePeriod = new TimePeriod(startTime, timePeriodLength, trafficQuotaPerPeriod, faultThresholdPerPeriod);
			this.m_callGate = new CallGate();
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0000F4DC File Offset: 0x0000D6DC
		public void ReceiveReport(DateTime reportTime, ReportTypes reportType, [NotNull] Action<object> onStateCalculated, object context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Action<object>>(onStateCalculated, "onStateCalculated");
			ExtendedDiagnostics.EnsureArgument(string.Format(CultureInfo.InvariantCulture, "{0}. {1}", new object[] { reportType, context }), reportType == ReportTypes.Failure || context == null, "Received a success report with a non null context");
			TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Info, "History received a {0} report. Current time: {1}", new object[] { reportType, reportTime });
			if (this.m_currentTimePeriod.IsQueryTimeAfterPeriodEndTime(reportTime))
			{
				object closeTimePeriodsLock = this.m_closeTimePeriodsLock;
				lock (closeTimePeriodsLock)
				{
					bool flag2 = true;
					while (this.m_currentTimePeriod.IsQueryTimeAfterPeriodEndTime(reportTime))
					{
						int num = (flag2 ? this.m_currentTimePeriod.ResetPeriod() : this.m_currentTimePeriod.ResetPeriodAsNoTraffic());
						flag2 = false;
						this.UpdateLastCalculatedValue(num);
						StateCalculatedResult stateCalculatedResult = this.Evaluate();
						TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Info, "Calculated result: {0}", new object[] { stateCalculatedResult });
						this.m_callGate.CallAsync(onStateCalculated, stateCalculatedResult);
					}
				}
			}
			if (reportType == ReportTypes.Success)
			{
				this.m_currentTimePeriod.NotifySuccess();
				return;
			}
			if (reportType != ReportTypes.Failure)
			{
				return;
			}
			this.m_currentTimePeriod.NotifyFailure();
			this.FailureContext = context;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0000F624 File Offset: 0x0000D824
		private void UpdateLastCalculatedValue(int valueToAdd)
		{
			TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Info, "Adding new calculated value: {0} to calculated values: {1}.", new object[]
			{
				valueToAdd,
				this.GetCalculatedValuesString()
			});
			this.m_calculatedValues.Add(valueToAdd);
			int num = 0;
			if (this.m_calculatedValues.Count > this.m_maxPeriodsInHistory)
			{
				num = this.m_calculatedValues[0];
				this.m_calculatedValues.RemoveAt(0);
				TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Info, "Removed first value: {0} from calculated values because # of members in history: {1} was above maximum: {2}", new object[]
				{
					num,
					this.m_calculatedValues.Count + 1,
					this.m_maxPeriodsInHistory
				});
			}
			this.UpdateRepresentedTraffic(valueToAdd, num);
			TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Info, "Added new calculated value: {0}. Calculated values are: {1}. Represented traffic is: {2}", new object[]
			{
				this.m_maxTrafficRepresented,
				this.GetCalculatedValuesString(),
				this.m_trafficRepresented
			});
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0000F718 File Offset: 0x0000D918
		private void UpdateRepresentedTraffic(int newAddition, int valueRemoved)
		{
			int trafficRepresented = this.m_trafficRepresented;
			this.m_trafficRepresented += Math.Abs(newAddition);
			this.m_trafficRepresented -= Math.Abs(valueRemoved);
			TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Info, "Updated represented traffic to: {0} after receiving value: {1} and removing value: {2}. Old represented traffic was: {3}", new object[] { this.m_trafficRepresented, newAddition, valueRemoved, trafficRepresented });
			int num = this.m_calculatedValues[0];
			while (this.m_trafficRepresented - Math.Abs(num) >= this.m_maxTrafficRepresented)
			{
				this.m_trafficRepresented -= Math.Abs(num);
				this.m_calculatedValues.RemoveAt(0);
				TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Info, "Represented traffic was above maximum needed: {0}. Removed first value: {1}. New represented traffic is: {2}", new object[] { this.m_maxTrafficRepresented, num, this.m_trafficRepresented });
				num = this.m_calculatedValues[0];
			}
			if (this.m_trafficRepresented > this.m_maxTrafficRepresented)
			{
				int num2 = this.m_trafficRepresented - this.m_maxTrafficRepresented;
				this.m_trafficRepresented -= num2;
				this.m_calculatedValues[0] = ((this.m_calculatedValues[0] < 0) ? (this.m_calculatedValues[0] + num2) : (this.m_calculatedValues[0] - num2));
				TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Trimmed {0} from first calculated value. New first calc value is: {1}", new object[]
				{
					num2,
					this.m_calculatedValues[0]
				});
			}
			this.m_calculatedValues.TrimExcess();
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0000F8C0 File Offset: 0x0000DAC0
		private StateCalculatedResult Evaluate()
		{
			TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Info, "Evaluate called. History of calculated values is: {0}", new object[] { this.GetCalculatedValuesString() });
			if (TimePeriod.IsSuccess(this.LastCalculatedValue))
			{
				TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Info, "Last period is not error so doing nothing");
				return new StateCalculatedResult(this.m_currentTimePeriod.TimeRange.Begin, State.Ok);
			}
			float num = 0f;
			for (int i = this.m_calculatedValues.Count - 1; i > -1; i--)
			{
				num += (float)this.m_calculatedValues[i];
				if (num >= (float)this.m_normalizedAlarmThreshold)
				{
					TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Info, "Sum {0} is at or above alarm threshold {1} after value {2}", new object[]
					{
						num,
						this.m_normalizedAlarmThreshold,
						this.m_calculatedValues[i]
					});
					return new StateCalculatedResult(this.m_currentTimePeriod.TimeRange.Begin, State.Error);
				}
				TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Sum {0} is below alarm threshold {1} after value {2}", new object[]
				{
					num,
					this.m_normalizedAlarmThreshold,
					this.m_calculatedValues[i]
				});
			}
			return new StateCalculatedResult(this.m_currentTimePeriod.TimeRange.Begin, State.Ok);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000FA0D File Offset: 0x0000DC0D
		private string GetCalculatedValuesString()
		{
			return this.m_calculatedValues.StringJoin(",");
		}

		// Token: 0x04000167 RID: 359
		private readonly int m_maxPeriodsInHistory;

		// Token: 0x04000168 RID: 360
		private readonly int m_normalizedAlarmThreshold;

		// Token: 0x04000169 RID: 361
		private readonly int m_maxTrafficRepresented;

		// Token: 0x0400016A RID: 362
		private readonly CallGate m_callGate;

		// Token: 0x0400016B RID: 363
		private readonly object m_closeTimePeriodsLock;

		// Token: 0x0400016C RID: 364
		private readonly List<int> m_calculatedValues;

		// Token: 0x0400016D RID: 365
		private TimePeriod m_currentTimePeriod;

		// Token: 0x0400016E RID: 366
		private int m_trafficRepresented;
	}
}
