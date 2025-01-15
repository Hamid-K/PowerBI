using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x02000818 RID: 2072
	public sealed class TimeMetric
	{
		// Token: 0x06007310 RID: 29456 RVA: 0x001DE6BD File Offset: 0x001DC8BD
		public TimeMetric(int indexInCollection, TimeMetricManager metricAdjuster, int otherMetricCount)
		{
			this.m_indexInCollection = indexInCollection;
			this.m_timer = new Timer();
			this.m_totalDurationMs = 0L;
			this.m_isRunning = false;
			this.m_otherMetricAdjustments = new long[otherMetricCount];
			this.m_metricAdjuster = metricAdjuster;
		}

		// Token: 0x06007311 RID: 29457 RVA: 0x001DE6FC File Offset: 0x001DC8FC
		public TimeMetric(TimeMetric other)
		{
			this.m_indexInCollection = other.m_indexInCollection;
			this.m_timer = new Timer();
			this.m_totalDurationMs = other.m_totalDurationMs;
			this.m_isRunning = false;
			this.m_otherMetricAdjustments = (long[])other.m_otherMetricAdjustments.Clone();
			this.m_metricAdjuster = other.m_metricAdjuster;
		}

		// Token: 0x06007312 RID: 29458 RVA: 0x001DE75B File Offset: 0x001DC95B
		public void StartTimer()
		{
			this.m_timer.StartTimer();
			this.m_isRunning = true;
		}

		// Token: 0x06007313 RID: 29459 RVA: 0x001DE76F File Offset: 0x001DC96F
		public bool TryStartTimer()
		{
			if (this.m_isRunning)
			{
				return false;
			}
			this.StartTimer();
			return true;
		}

		// Token: 0x06007314 RID: 29460 RVA: 0x001DE784 File Offset: 0x001DC984
		public void StopTimer()
		{
			this.m_isRunning = false;
			long num = this.m_timer.ElapsedTimeMs();
			this.AddTime(num);
		}

		// Token: 0x06007315 RID: 29461 RVA: 0x001DE7AC File Offset: 0x001DC9AC
		public void Add(TimeMetric otherMetric)
		{
			this.m_totalDurationMs += otherMetric.TotalDurationMs;
			for (int i = 0; i < this.m_otherMetricAdjustments.Length; i++)
			{
				this.m_otherMetricAdjustments[i] += otherMetric.m_otherMetricAdjustments[i];
			}
		}

		// Token: 0x06007316 RID: 29462 RVA: 0x001DE7F7 File Offset: 0x001DC9F7
		public void AddTime(long durationMs)
		{
			durationMs = ExecutionLogContext.TimerMeasurementAdjusted(durationMs);
			this.m_metricAdjuster.UpdateTimeMetricAdjustments(durationMs, this.m_otherMetricAdjustments);
			this.m_totalDurationMs += durationMs;
		}

		// Token: 0x06007317 RID: 29463 RVA: 0x001DE824 File Offset: 0x001DCA24
		public void Subtract(TimeMetric other)
		{
			this.m_totalDurationMs = ExecutionLogContext.TimerMeasurementAdjusted(this.m_totalDurationMs - other.m_totalDurationMs);
			for (int i = 0; i < this.m_otherMetricAdjustments.Length; i++)
			{
				long num = this.m_otherMetricAdjustments[i] - other.m_otherMetricAdjustments[i];
				this.m_otherMetricAdjustments[i] = ExecutionLogContext.TimerMeasurementAdjusted(num);
			}
		}

		// Token: 0x170026EC RID: 9964
		// (get) Token: 0x06007318 RID: 29464 RVA: 0x001DE87C File Offset: 0x001DCA7C
		public bool IsRunning
		{
			get
			{
				return this.m_isRunning;
			}
		}

		// Token: 0x170026ED RID: 9965
		// (get) Token: 0x06007319 RID: 29465 RVA: 0x001DE884 File Offset: 0x001DCA84
		public long TotalDurationMs
		{
			get
			{
				return this.m_totalDurationMs;
			}
		}

		// Token: 0x170026EE RID: 9966
		// (get) Token: 0x0600731A RID: 29466 RVA: 0x001DE88C File Offset: 0x001DCA8C
		public long[] OtherMetricAdjustments
		{
			get
			{
				return this.m_otherMetricAdjustments;
			}
		}

		// Token: 0x04003AF8 RID: 15096
		private bool m_isRunning;

		// Token: 0x04003AF9 RID: 15097
		private long m_totalDurationMs;

		// Token: 0x04003AFA RID: 15098
		private readonly Timer m_timer;

		// Token: 0x04003AFB RID: 15099
		private readonly long[] m_otherMetricAdjustments;

		// Token: 0x04003AFC RID: 15100
		private readonly TimeMetricManager m_metricAdjuster;

		// Token: 0x04003AFD RID: 15101
		private readonly int m_indexInCollection;
	}
}
