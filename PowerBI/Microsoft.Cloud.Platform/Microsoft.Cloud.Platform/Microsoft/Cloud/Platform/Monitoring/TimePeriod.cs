using System;
using System.Threading;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Monitoring
{
	// Token: 0x02000094 RID: 148
	internal class TimePeriod
	{
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x0000EF7C File Offset: 0x0000D17C
		internal long NumberSuccesses
		{
			get
			{
				return Interlocked.Read(ref this.m_numberSuccesses);
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x0000EF89 File Offset: 0x0000D189
		internal long NumberFailures
		{
			get
			{
				return Interlocked.Read(ref this.m_numberFailures);
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0000EF96 File Offset: 0x0000D196
		// (set) Token: 0x06000429 RID: 1065 RVA: 0x0000EF9E File Offset: 0x0000D19E
		internal DateTimeRange TimeRange { get; private set; }

		// Token: 0x0600042A RID: 1066 RVA: 0x0000EFA8 File Offset: 0x0000D1A8
		internal TimePeriod(DateTime startTime, TimeSpan periodLength, int trafficQuota, int threshold)
		{
			ExtendedDiagnostics.EnsureArgumentIsPositive(trafficQuota - 1, "trafficQuota");
			ExtendedDiagnostics.EnsureArgumentIsBetween(threshold, 1, 100, "threshold");
			ExtendedDiagnostics.EnsureArgumentIsPositive(TimeSpan.Compare(periodLength, TimeSpan.Zero), "periodLength");
			this.TimeRange = new DateTimeRange(startTime, periodLength);
			this.m_trafficQuota = trafficQuota;
			this.m_threshold = threshold;
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000F008 File Offset: 0x0000D208
		internal int ResetPeriod()
		{
			TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Info, "Resseting time period. #Successes: {0}. #Failures: {1}", new object[] { this.NumberSuccesses, this.NumberFailures });
			long numberFailures = this.NumberFailures;
			long numberSuccesses = this.NumberSuccesses;
			Interlocked.Add(ref this.m_numberFailures, numberFailures * -1L);
			TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Info, "Decreased {0} from #of failures. New value is {1}.", new object[] { numberFailures, this.NumberFailures });
			Interlocked.Add(ref this.m_numberSuccesses, numberSuccesses * -1L);
			TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Info, "Decreased {0} from #of successes. New value is {1}.", new object[] { numberSuccesses, this.NumberSuccesses });
			int num;
			try
			{
				num = checked((int)(numberFailures + numberSuccesses));
			}
			catch (OverflowException)
			{
				TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Warning, "Caught Overflow Exception while calculating total traffic. #Failures: {0}. #Successes: {1}. Assigned int.Max", new object[] { numberFailures, numberSuccesses });
				num = int.MaxValue;
			}
			int num2 = 0;
			if (num != 0)
			{
				float num3 = (float)numberFailures / (float)num * 100f;
				TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Percent failures: {0}", new object[] { num3 });
				int num4 = ((num3 < (float)this.m_threshold) ? (-1) : 1);
				TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Calculated sign: {0}. threshold is: {1}. Percent failure is {2}", new object[] { num4, this.m_threshold, num3 });
				num2 = num4 * Math.Min(num, this.m_trafficQuota);
			}
			TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Info, "Calculated value: {0} for period starting at {1}. #successes: {2}. #fails: {3}. Total traffic: {4}. Quota: {5}. Threshold: {6}", new object[]
			{
				num2,
				this.TimeRange.Begin,
				numberSuccesses,
				numberFailures,
				num,
				this.m_trafficQuota,
				this.m_threshold
			});
			this.SetNextStartTime();
			return num2;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000F218 File Offset: 0x0000D418
		internal int ResetPeriodAsNoTraffic()
		{
			this.SetNextStartTime();
			return 0;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000F224 File Offset: 0x0000D424
		internal void NotifySuccess()
		{
			long num = Interlocked.Increment(ref this.m_numberSuccesses);
			TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Notified success. Counter is: {0}", new object[] { num });
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000F25C File Offset: 0x0000D45C
		internal void NotifyFailure()
		{
			long num = Interlocked.Increment(ref this.m_numberFailures);
			TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Notified failure. Counter is: {0}", new object[] { num });
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000F294 File Offset: 0x0000D494
		internal bool IsQueryTimeAfterPeriodEndTime(DateTime queryTime)
		{
			bool flag = this.TimeRange.Begin.Add(this.TimeRange.Span) <= queryTime;
			TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Info, "Period {0}. Start time: {1}. Period length {2}. Query time: {3}", new object[]
			{
				flag ? "ended" : "not ended",
				this.TimeRange.Begin,
				this.TimeRange.Span,
				queryTime
			});
			return flag;
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000F320 File Offset: 0x0000D520
		private void SetNextStartTime()
		{
			DateTime begin = this.TimeRange.Begin;
			this.TimeRange = new DateTimeRange(this.TimeRange.Begin.Add(this.TimeRange.Span), this.TimeRange.Span);
			TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Info, "Calculated new start time: {0}. Previous time was: {1}. Period length {2}", new object[]
			{
				this.TimeRange.Begin,
				begin,
				this.TimeRange.Span
			});
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000F3B2 File Offset: 0x0000D5B2
		internal static bool IsSuccess(int calculatedValue)
		{
			return calculatedValue < 0;
		}

		// Token: 0x04000162 RID: 354
		private long m_numberSuccesses;

		// Token: 0x04000163 RID: 355
		private long m_numberFailures;

		// Token: 0x04000164 RID: 356
		private readonly int m_trafficQuota;

		// Token: 0x04000165 RID: 357
		private readonly int m_threshold;
	}
}
