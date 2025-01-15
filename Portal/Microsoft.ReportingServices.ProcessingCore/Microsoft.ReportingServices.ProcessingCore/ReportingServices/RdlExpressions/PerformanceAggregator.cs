using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Microsoft.ReportingServices.RdlExpressions
{
	// Token: 0x02000567 RID: 1383
	internal class PerformanceAggregator
	{
		// Token: 0x06005096 RID: 20630 RVA: 0x00152AB0 File Offset: 0x00150CB0
		internal List<PerformanceAggregatorSummary> GetSummaryInMs()
		{
			ConcurrentDictionary<PerformanceMetricType, PerformanceAggregatorSummary> summaryDictionary = this.m_summaryDictionary;
			List<PerformanceAggregatorSummary> list;
			if (summaryDictionary == null)
			{
				list = null;
			}
			else
			{
				ICollection<PerformanceAggregatorSummary> values = summaryDictionary.Values;
				list = ((values != null) ? values.ToList<PerformanceAggregatorSummary>() : null);
			}
			return list ?? new List<PerformanceAggregatorSummary>();
		}

		// Token: 0x06005097 RID: 20631 RVA: 0x00152AD9 File Offset: 0x00150CD9
		internal PerformanceMeasurement StartMeasurement(PerformanceMetricType metricType)
		{
			return new PerformanceMeasurement(metricType, new Action<PerformanceMetricType, long>(this.EndMeasurement));
		}

		// Token: 0x06005098 RID: 20632 RVA: 0x00152AF0 File Offset: 0x00150CF0
		private void EndMeasurement(PerformanceMetricType metricType, long startTicks)
		{
			long timestamp = Stopwatch.GetTimestamp();
			PerformanceAggregatorSummary orAdd = this.m_summaryDictionary.GetOrAdd(metricType, new PerformanceAggregatorSummary(metricType));
			float num = Convert.ToSingle(timestamp - startTicks) / (float)Stopwatch.Frequency * 1000f;
			orAdd.Sum += num;
			orAdd.Count += 1f;
			orAdd.Max = ((num > orAdd.Max) ? num : orAdd.Max);
			orAdd.Min = ((num < orAdd.Min) ? num : orAdd.Min);
		}

		// Token: 0x0400288C RID: 10380
		private readonly ConcurrentDictionary<PerformanceMetricType, PerformanceAggregatorSummary> m_summaryDictionary = new ConcurrentDictionary<PerformanceMetricType, PerformanceAggregatorSummary>();
	}
}
