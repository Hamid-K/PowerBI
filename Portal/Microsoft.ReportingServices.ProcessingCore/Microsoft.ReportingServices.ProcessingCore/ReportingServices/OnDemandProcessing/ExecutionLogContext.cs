using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Internal;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x0200081A RID: 2074
	public sealed class ExecutionLogContext
	{
		// Token: 0x06007322 RID: 29474 RVA: 0x001DE9FC File Offset: 0x001DCBFC
		internal ExecutionLogContext(IJobContext jobContext)
		{
			this.m_activeScaleCaches.Push(new ExecutionLogContext.ScaleCacheInfo(int.MinValue));
			this.m_jobContext = jobContext;
			if (this.m_jobContext != null)
			{
				this.m_metricManager = new TimeMetricManager(ExecutionLogContext.TimeMetricCount);
			}
		}

		// Token: 0x06007323 RID: 29475 RVA: 0x001DEA64 File Offset: 0x001DCC64
		public static long TimerMeasurementAdjusted(long durationMs)
		{
			return Math.Max(0L, durationMs);
		}

		// Token: 0x06007324 RID: 29476 RVA: 0x001DEA6E File Offset: 0x001DCC6E
		public static long NormalizeCalculatedDuration(long durationMs)
		{
			return Math.Max(-1L, durationMs);
		}

		// Token: 0x170026F0 RID: 9968
		// (get) Token: 0x06007325 RID: 29477 RVA: 0x001DEA78 File Offset: 0x001DCC78
		internal bool IsProcessingTimerRunning
		{
			get
			{
				return this.m_metricManager != null && this.m_metricManager[0].IsRunning;
			}
		}

		// Token: 0x170026F1 RID: 9969
		// (get) Token: 0x06007326 RID: 29478 RVA: 0x001DEA95 File Offset: 0x001DCC95
		internal bool IsRenderingTimerRunning
		{
			get
			{
				return this.m_metricManager != null && this.m_metricManager[1].IsRunning;
			}
		}

		// Token: 0x170026F2 RID: 9970
		// (get) Token: 0x06007327 RID: 29479 RVA: 0x001DEAB2 File Offset: 0x001DCCB2
		internal long PeakTablixProcessingMemoryUsageKB
		{
			get
			{
				Global.Tracer.Assert(this.m_activeScaleCaches.Count > 0, "Missing root of active cache tree");
				return this.m_activeScaleCaches.Peek().ChildPeakMemoryUsageKB;
			}
		}

		// Token: 0x170026F3 RID: 9971
		// (get) Token: 0x06007328 RID: 29480 RVA: 0x001DEAE1 File Offset: 0x001DCCE1
		internal long PeakGroupTreeMemoryUsageKB
		{
			get
			{
				return this.m_peakGroupTreeMemoryUsageKB;
			}
		}

		// Token: 0x170026F4 RID: 9972
		// (get) Token: 0x06007329 RID: 29481 RVA: 0x001DEAE9 File Offset: 0x001DCCE9
		internal long PeakProcesssingMemoryUsage
		{
			get
			{
				return this.PeakTablixProcessingMemoryUsageKB + this.PeakGroupTreeMemoryUsageKB;
			}
		}

		// Token: 0x170026F5 RID: 9973
		// (get) Token: 0x0600732A RID: 29482 RVA: 0x001DEAF8 File Offset: 0x001DCCF8
		internal long DataProcessingDurationMsNormalized
		{
			get
			{
				return this.GetNormalizedAdjustedMetric(ExecutionLogContext.TimeMetricType.DataRetrieval);
			}
		}

		// Token: 0x170026F6 RID: 9974
		// (get) Token: 0x0600732B RID: 29483 RVA: 0x001DEB01 File Offset: 0x001DCD01
		internal long ProcessingScalabilityDurationMsNormalized
		{
			get
			{
				return ExecutionLogContext.NormalizeCalculatedDuration(this.m_processingScalabilityDurationMs);
			}
		}

		// Token: 0x170026F7 RID: 9975
		// (get) Token: 0x0600732C RID: 29484 RVA: 0x001DEB0E File Offset: 0x001DCD0E
		internal long ReportRenderingDurationMsNormalized
		{
			get
			{
				return this.GetNormalizedAdjustedMetric(ExecutionLogContext.TimeMetricType.Rendering);
			}
		}

		// Token: 0x170026F8 RID: 9976
		// (get) Token: 0x0600732D RID: 29485 RVA: 0x001DEB18 File Offset: 0x001DCD18
		internal long ReportProcessingDurationMsNormalized
		{
			get
			{
				long normalizedAdjustedMetric = this.GetNormalizedAdjustedMetric(ExecutionLogContext.TimeMetricType.Processing);
				long normalizedAdjustedMetric2 = this.GetNormalizedAdjustedMetric(ExecutionLogContext.TimeMetricType.TablixProcessing);
				return ExecutionLogContext.NormalizeCalculatedDuration(normalizedAdjustedMetric + normalizedAdjustedMetric2);
			}
		}

		// Token: 0x170026F9 RID: 9977
		// (get) Token: 0x0600732E RID: 29486 RVA: 0x001DEB3B File Offset: 0x001DCD3B
		internal long ExternalImageDurationMs
		{
			get
			{
				return this.m_externalImageDurationMs;
			}
		}

		// Token: 0x170026FA RID: 9978
		// (get) Token: 0x0600732F RID: 29487 RVA: 0x001DEB43 File Offset: 0x001DCD43
		// (set) Token: 0x06007330 RID: 29488 RVA: 0x001DEB4B File Offset: 0x001DCD4B
		internal long ExternalImageCount
		{
			get
			{
				return this.m_externalImageCount;
			}
			set
			{
				this.m_externalImageCount = value;
			}
		}

		// Token: 0x170026FB RID: 9979
		// (get) Token: 0x06007331 RID: 29489 RVA: 0x001DEB54 File Offset: 0x001DCD54
		// (set) Token: 0x06007332 RID: 29490 RVA: 0x001DEB5C File Offset: 0x001DCD5C
		internal long ExternalImageBytes
		{
			get
			{
				return this.m_externalImageBytes;
			}
			set
			{
				this.m_externalImageBytes = value;
			}
		}

		// Token: 0x170026FC RID: 9980
		// (get) Token: 0x06007333 RID: 29491 RVA: 0x001DEB65 File Offset: 0x001DCD65
		internal List<Pair<string, DataProcessingMetrics>> DataSetMetrics
		{
			get
			{
				return this.m_dataSetMetrics;
			}
		}

		// Token: 0x170026FD RID: 9981
		// (get) Token: 0x06007334 RID: 29492 RVA: 0x001DEB6D File Offset: 0x001DCD6D
		internal List<DataSourceMetrics> DataSourceConnectionMetrics
		{
			get
			{
				return this.m_dataSourceConnectionMetrics;
			}
		}

		// Token: 0x06007335 RID: 29493 RVA: 0x001DEB78 File Offset: 0x001DCD78
		internal List<Connection> GetConnectionMetrics()
		{
			if (this.DataSourceConnectionMetrics != null)
			{
				List<Connection> list = new List<Connection>();
				foreach (DataSourceMetrics dataSourceMetrics in this.DataSourceConnectionMetrics)
				{
					Connection connection = dataSourceMetrics.ToAdditionalInfoConnection(this.m_jobContext);
					if (connection != null)
					{
						list.Add(connection);
					}
				}
				return list;
			}
			return null;
		}

		// Token: 0x06007336 RID: 29494 RVA: 0x001DEBEC File Offset: 0x001DCDEC
		public void StopAllRunningTimers()
		{
			if (this.m_metricManager != null)
			{
				this.m_metricManager.StopAllRunningTimers();
			}
		}

		// Token: 0x06007337 RID: 29495 RVA: 0x001DEC01 File Offset: 0x001DCE01
		public void UpdateForTreeScaleCache(long scaleTimeDurationMs, long peakGroupTreeMemoryUsageKB)
		{
			this.m_processingScalabilityDurationMs += scaleTimeDurationMs;
			this.m_peakGroupTreeMemoryUsageKB += peakGroupTreeMemoryUsageKB;
		}

		// Token: 0x06007338 RID: 29496 RVA: 0x001DEC1F File Offset: 0x001DCE1F
		internal void AddLegacyDataProcessingTime(long durationMs)
		{
			if (this.m_metricManager != null)
			{
				this.m_metricManager[3].AddTime(durationMs);
			}
		}

		// Token: 0x06007339 RID: 29497 RVA: 0x001DEC3B File Offset: 0x001DCE3B
		internal void AddDataProcessingTime(TimeMetric childMetric)
		{
			if (this.m_metricManager != null && childMetric != null)
			{
				this.m_metricManager[3].Add(childMetric);
			}
		}

		// Token: 0x0600733A RID: 29498 RVA: 0x001DEC5C File Offset: 0x001DCE5C
		internal void AddDataSetMetrics(string dataSetName, DataProcessingMetrics metrics)
		{
			List<Pair<string, DataProcessingMetrics>> dataSetMetrics = this.m_dataSetMetrics;
			lock (dataSetMetrics)
			{
				this.m_dataSetMetrics.Add(new Pair<string, DataProcessingMetrics>(dataSetName, metrics));
			}
		}

		// Token: 0x0600733B RID: 29499 RVA: 0x001DECA8 File Offset: 0x001DCEA8
		internal void AddDataSourceParallelExecutionMetrics(string dataSourceName, string dataSourceReference, string dataSourceType, DataProcessingMetrics parallelDataSetMetrics)
		{
			List<DataSourceMetrics> dataSourceConnectionMetrics = this.m_dataSourceConnectionMetrics;
			lock (dataSourceConnectionMetrics)
			{
				this.m_dataSourceConnectionMetrics.Add(new DataSourceMetrics(dataSourceName, dataSourceReference, dataSourceType, parallelDataSetMetrics));
			}
		}

		// Token: 0x0600733C RID: 29500 RVA: 0x001DECF8 File Offset: 0x001DCEF8
		internal void AddDataSourceMetrics(string dataSourceName, string dataSourceReference, string dataSourceType, DataProcessingMetrics aggregatedMetrics, DataProcessingMetrics[] dataSetsMetrics)
		{
			List<DataSourceMetrics> dataSourceConnectionMetrics = this.m_dataSourceConnectionMetrics;
			lock (dataSourceConnectionMetrics)
			{
				this.m_dataSourceConnectionMetrics.Add(new DataSourceMetrics(dataSourceName, dataSourceReference, dataSourceType, aggregatedMetrics, dataSetsMetrics));
			}
		}

		// Token: 0x0600733D RID: 29501 RVA: 0x001DED4C File Offset: 0x001DCF4C
		internal void StartProcessingTimer()
		{
			this.StartTimer(ExecutionLogContext.TimeMetricType.Processing);
		}

		// Token: 0x0600733E RID: 29502 RVA: 0x001DED55 File Offset: 0x001DCF55
		internal void StopProcessingTimer()
		{
			this.StopTimer(ExecutionLogContext.TimeMetricType.Processing);
		}

		// Token: 0x0600733F RID: 29503 RVA: 0x001DED5E File Offset: 0x001DCF5E
		internal void StartRenderingTimer()
		{
			this.StartTimer(ExecutionLogContext.TimeMetricType.Rendering);
		}

		// Token: 0x06007340 RID: 29504 RVA: 0x001DED67 File Offset: 0x001DCF67
		internal void StopRenderingTimer()
		{
			this.StopTimer(ExecutionLogContext.TimeMetricType.Rendering);
		}

		// Token: 0x06007341 RID: 29505 RVA: 0x001DED70 File Offset: 0x001DCF70
		internal void StartTablixProcessingTimer()
		{
			this.StartTimer(ExecutionLogContext.TimeMetricType.TablixProcessing);
		}

		// Token: 0x06007342 RID: 29506 RVA: 0x001DED79 File Offset: 0x001DCF79
		internal bool TryStartTablixProcessingTimer()
		{
			return this.TryStartTimer(ExecutionLogContext.TimeMetricType.TablixProcessing);
		}

		// Token: 0x06007343 RID: 29507 RVA: 0x001DED82 File Offset: 0x001DCF82
		internal void StopTablixProcessingTimer()
		{
			this.StopTimer(ExecutionLogContext.TimeMetricType.TablixProcessing);
		}

		// Token: 0x06007344 RID: 29508 RVA: 0x001DED8B File Offset: 0x001DCF8B
		internal void StartExternalImageTimer()
		{
			if (this.m_jobContext != null)
			{
				this.m_externalImageTimer = new Timer();
				this.m_externalImageTimer.StartTimer();
			}
		}

		// Token: 0x06007345 RID: 29509 RVA: 0x001DEDAB File Offset: 0x001DCFAB
		internal void StopExternalImageTimer()
		{
			if (this.m_externalImageTimer != null)
			{
				this.m_externalImageDurationMs += this.m_externalImageTimer.ElapsedTimeMs();
				this.m_externalImageTimer = null;
			}
		}

		// Token: 0x06007346 RID: 29510 RVA: 0x001DEDD4 File Offset: 0x001DCFD4
		public TimeMetric CreateDataRetrievalWorkerTimer()
		{
			return this.m_metricManager.CreateTimeMetric(3);
		}

		// Token: 0x06007347 RID: 29511 RVA: 0x001DEDE2 File Offset: 0x001DCFE2
		private void StartTimer(ExecutionLogContext.TimeMetricType metricType)
		{
			if (this.m_metricManager != null)
			{
				this.m_metricManager[(int)metricType].StartTimer();
			}
		}

		// Token: 0x06007348 RID: 29512 RVA: 0x001DEDFD File Offset: 0x001DCFFD
		private bool TryStartTimer(ExecutionLogContext.TimeMetricType metricType)
		{
			return this.m_metricManager != null && this.m_metricManager[(int)metricType].TryStartTimer();
		}

		// Token: 0x06007349 RID: 29513 RVA: 0x001DEE1A File Offset: 0x001DD01A
		private void StopTimer(ExecutionLogContext.TimeMetricType metricType)
		{
			if (this.m_metricManager != null)
			{
				this.m_metricManager[(int)metricType].StopTimer();
			}
		}

		// Token: 0x0600734A RID: 29514 RVA: 0x001DEE35 File Offset: 0x001DD035
		private long GetNormalizedAdjustedMetric(ExecutionLogContext.TimeMetricType metricType)
		{
			if (this.m_metricManager == null)
			{
				return 0L;
			}
			return this.m_metricManager.GetNormalizedAdjustedMetric((int)metricType);
		}

		// Token: 0x0600734B RID: 29515 RVA: 0x001DEE4E File Offset: 0x001DD04E
		internal void RegisterTablixProcessingScaleCache(int reportId)
		{
			this.m_activeScaleCaches.Push(new ExecutionLogContext.ScaleCacheInfo(reportId));
		}

		// Token: 0x0600734C RID: 29516 RVA: 0x001DEE64 File Offset: 0x001DD064
		internal void UnRegisterTablixProcessingScaleCache(int reportId, IScalabilityCache tablixProcessingCache)
		{
			this.m_processingScalabilityDurationMs += tablixProcessingCache.ScalabilityDurationMs;
			long num = tablixProcessingCache.PeakMemoryUsageKBytes;
			ExecutionLogContext.ScaleCacheInfo scaleCacheInfo = this.m_activeScaleCaches.Peek();
			if (scaleCacheInfo.ReportGlobalId == reportId && reportId != -2147483648)
			{
				num += scaleCacheInfo.ChildPeakMemoryUsageKB;
				this.m_activeScaleCaches.Pop();
				scaleCacheInfo = this.m_activeScaleCaches.Peek();
			}
			scaleCacheInfo.ChildPeakMemoryUsageKB = Math.Max(scaleCacheInfo.ChildPeakMemoryUsageKB, num);
		}

		// Token: 0x04003AFF RID: 15103
		private long m_processingScalabilityDurationMs;

		// Token: 0x04003B00 RID: 15104
		private long m_peakGroupTreeMemoryUsageKB;

		// Token: 0x04003B01 RID: 15105
		private long m_externalImageDurationMs;

		// Token: 0x04003B02 RID: 15106
		private long m_externalImageCount;

		// Token: 0x04003B03 RID: 15107
		private long m_externalImageBytes;

		// Token: 0x04003B04 RID: 15108
		private List<Pair<string, DataProcessingMetrics>> m_dataSetMetrics = new List<Pair<string, DataProcessingMetrics>>();

		// Token: 0x04003B05 RID: 15109
		private List<DataSourceMetrics> m_dataSourceConnectionMetrics = new List<DataSourceMetrics>();

		// Token: 0x04003B06 RID: 15110
		private Timer m_externalImageTimer;

		// Token: 0x04003B07 RID: 15111
		private TimeMetricManager m_metricManager;

		// Token: 0x04003B08 RID: 15112
		private static readonly int TimeMetricCount = Enum.GetValues(typeof(ExecutionLogContext.TimeMetricType)).Length;

		// Token: 0x04003B09 RID: 15113
		private readonly IJobContext m_jobContext;

		// Token: 0x04003B0A RID: 15114
		private readonly Stack<ExecutionLogContext.ScaleCacheInfo> m_activeScaleCaches = new Stack<ExecutionLogContext.ScaleCacheInfo>();

		// Token: 0x04003B0B RID: 15115
		private const int RootScaleCacheInfoId = -2147483648;

		// Token: 0x02000CF5 RID: 3317
		private enum TimeMetricType
		{
			// Token: 0x04004FC7 RID: 20423
			Processing,
			// Token: 0x04004FC8 RID: 20424
			Rendering,
			// Token: 0x04004FC9 RID: 20425
			TablixProcessing,
			// Token: 0x04004FCA RID: 20426
			DataRetrieval
		}

		// Token: 0x02000CF6 RID: 3318
		private sealed class ScaleCacheInfo
		{
			// Token: 0x06008E12 RID: 36370 RVA: 0x002440F9 File Offset: 0x002422F9
			public ScaleCacheInfo(int reportGlobalId)
			{
				this.m_reportGlobalId = reportGlobalId;
				this.m_childPeakMemoryUsageKB = 0L;
			}

			// Token: 0x17002B83 RID: 11139
			// (get) Token: 0x06008E13 RID: 36371 RVA: 0x00244110 File Offset: 0x00242310
			// (set) Token: 0x06008E14 RID: 36372 RVA: 0x00244118 File Offset: 0x00242318
			public long ChildPeakMemoryUsageKB
			{
				get
				{
					return this.m_childPeakMemoryUsageKB;
				}
				set
				{
					this.m_childPeakMemoryUsageKB = value;
				}
			}

			// Token: 0x17002B84 RID: 11140
			// (get) Token: 0x06008E15 RID: 36373 RVA: 0x00244121 File Offset: 0x00242321
			public int ReportGlobalId
			{
				get
				{
					return this.m_reportGlobalId;
				}
			}

			// Token: 0x04004FCB RID: 20427
			private long m_childPeakMemoryUsageKB;

			// Token: 0x04004FCC RID: 20428
			private readonly int m_reportGlobalId;
		}
	}
}
