using System;
using System.Diagnostics;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000005 RID: 5
	internal static class ProgressiveReportCounterInfo
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00003160 File Offset: 0x00001360
		internal static CounterCreationDataCollection GetProgressiveReportPerfCounters()
		{
			if (ProgressiveReportCounterInfo.m_progressiveReportPerfCounters == null)
			{
				ProgressiveReportCounterInfo.m_progressiveReportPerfCounters = new CounterCreationDataCollection();
				ProgressiveReportCounterInfo.m_progressiveReportPerfCounters.Add(new CounterCreationData("Report Requests Active", RSPerfCounterRes.ReportRequestsActive, PerformanceCounterType.NumberOfItems32));
				ProgressiveReportCounterInfo.m_progressiveReportPerfCounters.Add(new CounterCreationData("Report Requests Total", RSPerfCounterRes.ReportRequestsTotal, PerformanceCounterType.NumberOfItems32));
				ProgressiveReportCounterInfo.m_progressiveReportPerfCounters.Add(new CounterCreationData("Report Requests/sec", RSPerfCounterRes.ReportRequestsPerSecond, PerformanceCounterType.RateOfCountsPerSecond32));
				ProgressiveReportCounterInfo.m_progressiveReportPerfCounters.Add(new CounterCreationData("Report Requests Average Duration (ms)", RSPerfCounterRes.ReportRequestsAverageDuration, PerformanceCounterType.AverageCount64));
				ProgressiveReportCounterInfo.m_progressiveReportPerfCounters.Add(new CounterCreationData("Report Requests Average Duration base", string.Empty, PerformanceCounterType.AverageBase));
				ProgressiveReportCounterInfo.m_progressiveReportPerfCounters.Add(new CounterCreationData("Model Requests Active", RSPerfCounterRes.ModelRequestsActive, PerformanceCounterType.NumberOfItems32));
				ProgressiveReportCounterInfo.m_progressiveReportPerfCounters.Add(new CounterCreationData("Model Requests Total", RSPerfCounterRes.ModelRequestsTotal, PerformanceCounterType.NumberOfItems32));
				ProgressiveReportCounterInfo.m_progressiveReportPerfCounters.Add(new CounterCreationData("Model Requests/sec", RSPerfCounterRes.ModelRequestsPerSecond, PerformanceCounterType.RateOfCountsPerSecond32));
				ProgressiveReportCounterInfo.m_progressiveReportPerfCounters.Add(new CounterCreationData("Model Requests Average Duration (ms)", RSPerfCounterRes.ModelRequestsAverageDuration, PerformanceCounterType.AverageCount64));
				ProgressiveReportCounterInfo.m_progressiveReportPerfCounters.Add(new CounterCreationData("Model Requests Average Duration base", string.Empty, PerformanceCounterType.AverageBase));
				ProgressiveReportCounterInfo.m_progressiveReportPerfCounters.Add(new CounterCreationData("Query Requests Active", RSPerfCounterRes.QueryRequestsActive, PerformanceCounterType.NumberOfItems32));
				ProgressiveReportCounterInfo.m_progressiveReportPerfCounters.Add(new CounterCreationData("Query Requests Total", RSPerfCounterRes.QueryRequestsTotal, PerformanceCounterType.NumberOfItems32));
				ProgressiveReportCounterInfo.m_progressiveReportPerfCounters.Add(new CounterCreationData("Query Requests/sec", RSPerfCounterRes.QueryRequestsPerSecond, PerformanceCounterType.RateOfCountsPerSecond32));
				ProgressiveReportCounterInfo.m_progressiveReportPerfCounters.Add(new CounterCreationData("Query Requests Average Duration (ms)", RSPerfCounterRes.QueryRequestsAverageDuration, PerformanceCounterType.AverageCount64));
				ProgressiveReportCounterInfo.m_progressiveReportPerfCounters.Add(new CounterCreationData("Query Requests Average Duration base", string.Empty, PerformanceCounterType.AverageBase));
				ProgressiveReportCounterInfo.m_progressiveReportPerfCounters.Add(new CounterCreationData("Failures Total", RSPerfCounterRes.FailuresTotal, PerformanceCounterType.NumberOfItems32));
				ProgressiveReportCounterInfo.m_progressiveReportPerfCounters.Add(new CounterCreationData("Failures/sec", RSPerfCounterRes.FailuresPerSecond, PerformanceCounterType.RateOfCountsPerSecond32));
				ProgressiveReportCounterInfo.m_progressiveReportPerfCounters.Add(new CounterCreationData("Sessions Active", RSPerfCounterRes.SessionsActive, PerformanceCounterType.NumberOfItems32));
				ProgressiveReportCounterInfo.m_progressiveReportPerfCounters.Add(new CounterCreationData("Requests in New Sessions Total", RSPerfCounterRes.NewSessionTotal, PerformanceCounterType.NumberOfItems32));
				ProgressiveReportCounterInfo.m_progressiveReportPerfCounters.Add(new CounterCreationData("Requests in New Sessions/sec", RSPerfCounterRes.NewSessionPerSecond, PerformanceCounterType.RateOfCountsPerSecond32));
				ProgressiveReportCounterInfo.m_progressiveReportPerfCounters.Add(new CounterCreationData("Requests in Existing Sessions Total", RSPerfCounterRes.ExistingSessionTotal, PerformanceCounterType.NumberOfItems32));
				ProgressiveReportCounterInfo.m_progressiveReportPerfCounters.Add(new CounterCreationData("Requests in Existing Sessions/sec", RSPerfCounterRes.ExistingSessionPerSecond, PerformanceCounterType.RateOfCountsPerSecond32));
				ProgressiveReportCounterInfo.m_progressiveReportPerfCounters.Add(new CounterCreationData("Requests Total", RSPerfCounterRes.AllRequestsTotal, PerformanceCounterType.NumberOfItems32));
				ProgressiveReportCounterInfo.m_progressiveReportPerfCounters.Add(new CounterCreationData("Requests/sec", RSPerfCounterRes.AllRequestsPerSecond, PerformanceCounterType.RateOfCountsPerSecond32));
				ProgressiveReportCounterInfo.m_progressiveReportPerfCounters.Add(new CounterCreationData("Data Source Connection Failures Total", RSPerfCounterRes.DataSourceConnectionFailuresTotal, PerformanceCounterType.NumberOfItems32));
				ProgressiveReportCounterInfo.m_progressiveReportPerfCounters.Add(new CounterCreationData("Data Source Connection Failures/sec", RSPerfCounterRes.DataSourceConnectionFailuresPerSecond, PerformanceCounterType.RateOfCountsPerSecond32));
				ProgressiveReportCounterInfo.m_progressiveReportPerfCounters.Add(new CounterCreationData("Threads Active", RSPerfCounterRes.ThreadsActive, PerformanceCounterType.NumberOfItems32));
			}
			return ProgressiveReportCounterInfo.m_progressiveReportPerfCounters;
		}

		// Token: 0x04000008 RID: 8
		internal const string ProgressiveReportsClass = "ReportServer.Power View";

		// Token: 0x04000009 RID: 9
		internal const string ReportRequestsActive = "Report Requests Active";

		// Token: 0x0400000A RID: 10
		internal const string ReportRequestsTotal = "Report Requests Total";

		// Token: 0x0400000B RID: 11
		internal const string ReportRequestsPerSecond = "Report Requests/sec";

		// Token: 0x0400000C RID: 12
		internal const string ReportRequestsAverageDuration = "Report Requests Average Duration (ms)";

		// Token: 0x0400000D RID: 13
		internal const string ReportRequestsAverageDurationBase = "Report Requests Average Duration base";

		// Token: 0x0400000E RID: 14
		internal const string ModelRequestsActive = "Model Requests Active";

		// Token: 0x0400000F RID: 15
		internal const string ModelRequestsTotal = "Model Requests Total";

		// Token: 0x04000010 RID: 16
		internal const string ModelRequestsPerSecond = "Model Requests/sec";

		// Token: 0x04000011 RID: 17
		internal const string ModelRequestsAverageDuration = "Model Requests Average Duration (ms)";

		// Token: 0x04000012 RID: 18
		internal const string ModelRequestsAverageDurationBase = "Model Requests Average Duration base";

		// Token: 0x04000013 RID: 19
		internal const string QueryRequestsActive = "Query Requests Active";

		// Token: 0x04000014 RID: 20
		internal const string QueryRequestsTotal = "Query Requests Total";

		// Token: 0x04000015 RID: 21
		internal const string QueryRequestsPerSecond = "Query Requests/sec";

		// Token: 0x04000016 RID: 22
		internal const string QueryRequestsAverageDuration = "Query Requests Average Duration (ms)";

		// Token: 0x04000017 RID: 23
		internal const string QueryRequestsAverageDurationBase = "Query Requests Average Duration base";

		// Token: 0x04000018 RID: 24
		internal const string FailuresTotal = "Failures Total";

		// Token: 0x04000019 RID: 25
		internal const string FailuresPerSecond = "Failures/sec";

		// Token: 0x0400001A RID: 26
		internal const string SessionsActive = "Sessions Active";

		// Token: 0x0400001B RID: 27
		internal const string NewSessionTotal = "Requests in New Sessions Total";

		// Token: 0x0400001C RID: 28
		internal const string NewSessionPerSecond = "Requests in New Sessions/sec";

		// Token: 0x0400001D RID: 29
		internal const string ExistingSessionTotal = "Requests in Existing Sessions Total";

		// Token: 0x0400001E RID: 30
		internal const string ExistingSessionPerSecond = "Requests in Existing Sessions/sec";

		// Token: 0x0400001F RID: 31
		internal const string AllRequestsTotal = "Requests Total";

		// Token: 0x04000020 RID: 32
		internal const string AllRequestsPerSecond = "Requests/sec";

		// Token: 0x04000021 RID: 33
		internal const string DataSourceConnectionsPooledActive = "Data Source Pooled Connections Active";

		// Token: 0x04000022 RID: 34
		internal const string DataSourceConnectionsAllActive = "Data Source Connections Active";

		// Token: 0x04000023 RID: 35
		internal const string DataSourceConnectionFailuresTotal = "Data Source Connection Failures Total";

		// Token: 0x04000024 RID: 36
		internal const string DataSourceConnectionFailuresPerSecond = "Data Source Connection Failures/sec";

		// Token: 0x04000025 RID: 37
		internal const string ThreadsActive = "Threads Active";

		// Token: 0x04000026 RID: 38
		private static CounterCreationDataCollection m_progressiveReportPerfCounters;
	}
}
