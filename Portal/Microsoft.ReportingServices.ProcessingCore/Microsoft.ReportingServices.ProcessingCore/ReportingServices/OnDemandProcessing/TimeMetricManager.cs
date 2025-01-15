using System;
using System.Diagnostics;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x02000819 RID: 2073
	public sealed class TimeMetricManager
	{
		// Token: 0x0600731B RID: 29467 RVA: 0x001DE894 File Offset: 0x001DCA94
		public TimeMetricManager(int metricCount)
		{
			this.m_timeMetrics = new TimeMetric[metricCount];
			for (int i = 0; i < this.m_timeMetrics.Length; i++)
			{
				this.m_timeMetrics[i] = this.CreateTimeMetric(i);
			}
		}

		// Token: 0x170026EF RID: 9967
		public TimeMetric this[int index]
		{
			get
			{
				return this.m_timeMetrics[index];
			}
		}

		// Token: 0x0600731D RID: 29469 RVA: 0x001DE8DF File Offset: 0x001DCADF
		public TimeMetric CreateTimeMetric(int index)
		{
			return new TimeMetric(index, this, this.m_timeMetrics.Length);
		}

		// Token: 0x0600731E RID: 29470 RVA: 0x001DE8F0 File Offset: 0x001DCAF0
		public long GetNormalizedAdjustedMetric(int targetIndex)
		{
			long num = this.m_timeMetrics[targetIndex].TotalDurationMs;
			for (int i = 0; i < this.m_timeMetrics.Length; i++)
			{
				if (i != targetIndex)
				{
					TimeMetric timeMetric = this.m_timeMetrics[i];
					num -= timeMetric.OtherMetricAdjustments[targetIndex];
				}
			}
			return ExecutionLogContext.NormalizeCalculatedDuration(num);
		}

		// Token: 0x0600731F RID: 29471 RVA: 0x001DE93C File Offset: 0x001DCB3C
		public void UpdateTimeMetricAdjustments(long lastDurationMs, long[] metricAdjustments)
		{
			if (lastDurationMs <= 0L)
			{
				return;
			}
			for (int i = this.m_timeMetrics.Length - 1; i >= 0; i--)
			{
				if (this.m_timeMetrics[i].IsRunning)
				{
					metricAdjustments[i] += lastDurationMs;
					return;
				}
			}
		}

		// Token: 0x06007320 RID: 29472 RVA: 0x001DE980 File Offset: 0x001DCB80
		public void StopAllRunningTimers()
		{
			for (int i = this.m_timeMetrics.Length - 1; i >= 0; i--)
			{
				TimeMetric timeMetric = this.m_timeMetrics[i];
				if (timeMetric.IsRunning)
				{
					timeMetric.StopTimer();
				}
			}
		}

		// Token: 0x06007321 RID: 29473 RVA: 0x001DE9BC File Offset: 0x001DCBBC
		[Conditional("DEBUG")]
		public void VerifyStartOrder(int index)
		{
			for (int i = index; i < this.m_timeMetrics.Length; i++)
			{
				Global.Tracer.Assert(!this.m_timeMetrics[i].IsRunning, "Later metric must not be running when starting an earlier metric or adjustments will not work.");
			}
		}

		// Token: 0x04003AFE RID: 15102
		private readonly TimeMetric[] m_timeMetrics;
	}
}
