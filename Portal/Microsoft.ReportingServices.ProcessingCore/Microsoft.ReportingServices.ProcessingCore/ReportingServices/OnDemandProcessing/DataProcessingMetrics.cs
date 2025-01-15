using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Internal;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x02000816 RID: 2070
	internal sealed class DataProcessingMetrics
	{
		// Token: 0x060072E7 RID: 29415 RVA: 0x001DDFAD File Offset: 0x001DC1AD
		internal DataProcessingMetrics(Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet, IJobContext jobContext, ExecutionLogContext executionLogContext)
			: this(jobContext, executionLogContext)
		{
			this.m_specificDataSet = dataSet;
		}

		// Token: 0x060072E8 RID: 29416 RVA: 0x001DDFBE File Offset: 0x001DC1BE
		internal DataProcessingMetrics(IJobContext jobContext, ExecutionLogContext executionLogContext)
		{
			this.m_jobContext = jobContext;
			if (jobContext != null)
			{
				this.m_timers = new Timer[6];
				this.m_totalTimeMetric = executionLogContext.CreateDataRetrievalWorkerTimer();
				return;
			}
			this.m_timers = null;
		}

		// Token: 0x170026DE RID: 9950
		// (get) Token: 0x060072E9 RID: 29417 RVA: 0x001DDFF9 File Offset: 0x001DC1F9
		internal long TotalRowsRead
		{
			get
			{
				return this.m_totalRowsRead;
			}
		}

		// Token: 0x170026DF RID: 9951
		// (get) Token: 0x060072EA RID: 29418 RVA: 0x001DE001 File Offset: 0x001DC201
		internal long TotalRowsSkipped
		{
			get
			{
				return this.m_totalRowsSkipped;
			}
		}

		// Token: 0x170026E0 RID: 9952
		// (get) Token: 0x060072EB RID: 29419 RVA: 0x001DE009 File Offset: 0x001DC209
		internal long TotalDurationMs
		{
			get
			{
				if (this.m_totalTimeMetric == null)
				{
					return 0L;
				}
				return this.m_totalTimeMetric.TotalDurationMs;
			}
		}

		// Token: 0x170026E1 RID: 9953
		// (get) Token: 0x060072EC RID: 29420 RVA: 0x001DE021 File Offset: 0x001DC221
		internal TimeMetric TotalDuration
		{
			get
			{
				return this.m_totalTimeMetric;
			}
		}

		// Token: 0x170026E2 RID: 9954
		// (get) Token: 0x060072ED RID: 29421 RVA: 0x001DE029 File Offset: 0x001DC229
		internal long QueryDurationMs
		{
			get
			{
				return this.m_queryDurationMs;
			}
		}

		// Token: 0x170026E3 RID: 9955
		// (get) Token: 0x060072EE RID: 29422 RVA: 0x001DE031 File Offset: 0x001DC231
		internal long ExecuteReaderDurationMs
		{
			get
			{
				return this.m_executeReaderDurationMs;
			}
		}

		// Token: 0x170026E4 RID: 9956
		// (get) Token: 0x060072EF RID: 29423 RVA: 0x001DE039 File Offset: 0x001DC239
		internal long DataReaderMappingDurationMs
		{
			get
			{
				return this.m_dataReaderMappingDurationMs;
			}
		}

		// Token: 0x170026E5 RID: 9957
		// (get) Token: 0x060072F0 RID: 29424 RVA: 0x001DE041 File Offset: 0x001DC241
		internal long OpenConnectionDurationMs
		{
			get
			{
				return this.m_openConnectionDurationMs;
			}
		}

		// Token: 0x170026E6 RID: 9958
		// (get) Token: 0x060072F1 RID: 29425 RVA: 0x001DE049 File Offset: 0x001DC249
		internal long DisposeDataReaderDurationMs
		{
			get
			{
				return this.m_disposeDataReaderDurationMs;
			}
		}

		// Token: 0x170026E7 RID: 9959
		// (get) Token: 0x060072F2 RID: 29426 RVA: 0x001DE051 File Offset: 0x001DC251
		internal long CancelCommandDurationMs
		{
			get
			{
				return this.m_cancelCommandDurationMs;
			}
		}

		// Token: 0x060072F3 RID: 29427 RVA: 0x001DE05C File Offset: 0x001DC25C
		internal void Add(DataProcessingMetrics metrics)
		{
			if (metrics == null)
			{
				return;
			}
			if (this.m_totalTimeMetric != null)
			{
				this.m_totalTimeMetric.Add(metrics.m_totalTimeMetric);
			}
			this.Add(DataProcessingMetrics.MetricType.ExecuteReader, metrics.m_executeReaderDurationMs);
			this.Add(DataProcessingMetrics.MetricType.DataReaderMapping, metrics.m_dataReaderMappingDurationMs);
			this.Add(DataProcessingMetrics.MetricType.Query, metrics.m_queryDurationMs);
			this.Add(DataProcessingMetrics.MetricType.OpenConnection, metrics.m_openConnectionDurationMs);
			this.Add(DataProcessingMetrics.MetricType.DisposeDataReader, metrics.m_disposeDataReaderDurationMs);
			this.Add(DataProcessingMetrics.MetricType.CancelCommand, metrics.m_cancelCommandDurationMs);
		}

		// Token: 0x060072F4 RID: 29428 RVA: 0x001DE0D4 File Offset: 0x001DC2D4
		internal long Add(DataProcessingMetrics.MetricType type, Timer timer)
		{
			if (timer == null)
			{
				return -1L;
			}
			long num = timer.ElapsedTimeMs();
			this.Add(type, num);
			return num;
		}

		// Token: 0x060072F5 RID: 29429 RVA: 0x001DE0F8 File Offset: 0x001DC2F8
		internal void Add(DataProcessingMetrics.MetricType type, long elapsedTimeMs)
		{
			switch (type)
			{
			case DataProcessingMetrics.MetricType.ExecuteReader:
				this.Add(ref this.m_executeReaderDurationMs, elapsedTimeMs);
				return;
			case DataProcessingMetrics.MetricType.DataReaderMapping:
				this.Add(ref this.m_dataReaderMappingDurationMs, elapsedTimeMs);
				return;
			case DataProcessingMetrics.MetricType.Query:
				this.Add(ref this.m_queryDurationMs, elapsedTimeMs);
				return;
			case DataProcessingMetrics.MetricType.OpenConnection:
				this.Add(ref this.m_openConnectionDurationMs, elapsedTimeMs);
				return;
			case DataProcessingMetrics.MetricType.DisposeDataReader:
				this.Add(ref this.m_disposeDataReaderDurationMs, elapsedTimeMs);
				return;
			case DataProcessingMetrics.MetricType.CancelCommand:
				this.Add(ref this.m_cancelCommandDurationMs, elapsedTimeMs);
				return;
			default:
				return;
			}
		}

		// Token: 0x060072F6 RID: 29430 RVA: 0x001DE177 File Offset: 0x001DC377
		private void Add(ref long currentDurationMs, long elapsedTimeMs)
		{
			if (currentDurationMs == -2L)
			{
				currentDurationMs = 0L;
			}
			currentDurationMs += ExecutionLogContext.TimerMeasurementAdjusted(elapsedTimeMs);
		}

		// Token: 0x060072F7 RID: 29431 RVA: 0x001DE18F File Offset: 0x001DC38F
		internal void AddRowCount(long rowCount)
		{
			this.m_totalRowsRead += rowCount;
		}

		// Token: 0x060072F8 RID: 29432 RVA: 0x001DE19F File Offset: 0x001DC39F
		internal void AddSkippedRowCount(long rowCount)
		{
			this.m_totalRowsSkipped += rowCount;
		}

		// Token: 0x060072F9 RID: 29433 RVA: 0x001DE1B0 File Offset: 0x001DC3B0
		internal void StartTimer(DataProcessingMetrics.MetricType type)
		{
			if (this.m_jobContext == null)
			{
				return;
			}
			(this.m_timers[(int)type] = new Timer()).StartTimer();
		}

		// Token: 0x060072FA RID: 29434 RVA: 0x001DE1E0 File Offset: 0x001DC3E0
		internal long RecordTimerMeasurement(DataProcessingMetrics.MetricType type)
		{
			if (this.m_jobContext == null)
			{
				return 0L;
			}
			if (this.m_timers[(int)type] == null)
			{
				return 0L;
			}
			long num = this.Add(type, this.m_timers[(int)type]);
			this.m_timers[(int)type] = null;
			return num;
		}

		// Token: 0x060072FB RID: 29435 RVA: 0x001DE220 File Offset: 0x001DC420
		internal long RecordTimerMeasurementWithUpdatedTotal(DataProcessingMetrics.MetricType type)
		{
			long num = this.RecordTimerMeasurement(type);
			if (this.m_totalTimeMetric != null && !this.m_totalTimeMetric.IsRunning)
			{
				this.m_totalTimeMetric.AddTime(num);
			}
			return num;
		}

		// Token: 0x060072FC RID: 29436 RVA: 0x001DE257 File Offset: 0x001DC457
		public void StartTotalTimer()
		{
			if (this.m_totalTimeMetric == null)
			{
				return;
			}
			this.m_totalTimeMetric.StartTimer();
		}

		// Token: 0x060072FD RID: 29437 RVA: 0x001DE26D File Offset: 0x001DC46D
		public void RecordTotalTimerMeasurement()
		{
			if (this.m_totalTimeMetric == null)
			{
				return;
			}
			this.m_totalTimeMetric.StopTimer();
		}

		// Token: 0x170026E8 RID: 9960
		// (get) Token: 0x060072FE RID: 29438 RVA: 0x001DE283 File Offset: 0x001DC483
		// (set) Token: 0x060072FF RID: 29439 RVA: 0x001DE28B File Offset: 0x001DC48B
		public string CommandText
		{
			get
			{
				return this.m_commandText;
			}
			set
			{
				this.m_commandText = value;
			}
		}

		// Token: 0x170026E9 RID: 9961
		// (get) Token: 0x06007300 RID: 29440 RVA: 0x001DE294 File Offset: 0x001DC494
		// (set) Token: 0x06007301 RID: 29441 RVA: 0x001DE29C File Offset: 0x001DC49C
		internal string ResolvedConnectionString
		{
			get
			{
				return this.m_resolvedConnectionString;
			}
			set
			{
				this.m_resolvedConnectionString = value;
			}
		}

		// Token: 0x170026EA RID: 9962
		// (get) Token: 0x06007302 RID: 29442 RVA: 0x001DE2A5 File Offset: 0x001DC4A5
		// (set) Token: 0x06007303 RID: 29443 RVA: 0x001DE2AD File Offset: 0x001DC4AD
		internal bool? ConnectionFromPool
		{
			get
			{
				return this.m_connectionFromPool;
			}
			set
			{
				this.m_connectionFromPool = value;
			}
		}

		// Token: 0x06007304 RID: 29444 RVA: 0x001DE2B6 File Offset: 0x001DC4B6
		internal void SetStartAtParameters(IDataParameter[] startAtParameters)
		{
			this.m_startAtParameters = startAtParameters;
		}

		// Token: 0x06007305 RID: 29445 RVA: 0x001DE2BF File Offset: 0x001DC4BF
		internal void SetQueryParameters(IDataParameterCollection queryParameters)
		{
			this.m_queryParameters = queryParameters;
		}

		// Token: 0x06007306 RID: 29446 RVA: 0x001DE2C8 File Offset: 0x001DC4C8
		internal Microsoft.ReportingServices.Diagnostics.Internal.DataSet ToAdditionalInfoDataSet(IJobContext jobContext)
		{
			if (jobContext == null || this.m_specificDataSet == null)
			{
				return null;
			}
			Microsoft.ReportingServices.Diagnostics.Internal.DataSet dataSet = new Microsoft.ReportingServices.Diagnostics.Internal.DataSet();
			dataSet.Name = this.m_specificDataSet.Name;
			if (jobContext.ExecutionLogLevel == ExecutionLogLevel.Verbose)
			{
				dataSet.CommandText = this.m_commandText;
				if (this.m_startAtParameters != null || this.m_queryParameters != null)
				{
					List<QueryParameter> list = new List<QueryParameter>();
					this.AddStartAtParameters(list);
					this.AddQueryParameters(list);
					if (list.Count > 0)
					{
						dataSet.QueryParameters = list;
					}
				}
			}
			dataSet.RowsRead = new long?(this.m_totalRowsRead);
			dataSet.TotalTimeDataRetrieval = new long?(this.m_totalTimeMetric.TotalDurationMs);
			if (jobContext.ExecutionLogLevel == ExecutionLogLevel.Verbose)
			{
				dataSet.QueryPrepareAndExecutionTime = new long?(this.m_queryDurationMs);
			}
			dataSet.ExecuteReaderTime = new long?(this.m_executeReaderDurationMs);
			if (jobContext.ExecutionLogLevel == ExecutionLogLevel.Verbose)
			{
				dataSet.DataReaderMappingTime = new long?(this.m_dataReaderMappingDurationMs);
				dataSet.DisposeDataReaderTime = new long?(this.m_disposeDataReaderDurationMs);
			}
			if (this.m_cancelCommandDurationMs != -2L)
			{
				dataSet.CancelCommandTime = this.m_cancelCommandDurationMs.ToString(CultureInfo.InvariantCulture);
			}
			return dataSet;
		}

		// Token: 0x06007307 RID: 29447 RVA: 0x001DE3E4 File Offset: 0x001DC5E4
		private void AddStartAtParameters(List<QueryParameter> queryParams)
		{
			if (this.m_startAtParameters == null || this.m_startAtParameters.Length == 0)
			{
				return;
			}
			foreach (IDataParameter dataParameter in this.m_startAtParameters)
			{
				if (dataParameter != null)
				{
					queryParams.Add(DataProcessingMetrics.CreateAdditionalInfoQueryParameter(dataParameter.ParameterName, dataParameter.Value));
				}
			}
		}

		// Token: 0x06007308 RID: 29448 RVA: 0x001DE438 File Offset: 0x001DC638
		private void AddQueryParameters(List<QueryParameter> queryParams)
		{
			if (this.m_queryParameters == null)
			{
				return;
			}
			foreach (object obj in this.m_queryParameters)
			{
				IDataParameter dataParameter = (IDataParameter)obj;
				if (dataParameter != null)
				{
					IDataMultiValueParameter dataMultiValueParameter = dataParameter as IDataMultiValueParameter;
					if (dataMultiValueParameter != null)
					{
						DataProcessingMetrics.AddMultiValueQueryParameter(queryParams, dataMultiValueParameter);
					}
					else
					{
						queryParams.Add(DataProcessingMetrics.CreateAdditionalInfoQueryParameter(dataParameter.ParameterName, dataParameter.Value));
					}
				}
			}
		}

		// Token: 0x06007309 RID: 29449 RVA: 0x001DE4C0 File Offset: 0x001DC6C0
		private static void AddMultiValueQueryParameter(List<QueryParameter> queryParams, IDataMultiValueParameter parameter)
		{
			if (parameter.Values == null)
			{
				return;
			}
			foreach (object obj in parameter.Values)
			{
				queryParams.Add(DataProcessingMetrics.CreateAdditionalInfoQueryParameter(parameter.ParameterName, obj));
			}
		}

		// Token: 0x0600730A RID: 29450 RVA: 0x001DE504 File Offset: 0x001DC704
		private static QueryParameter CreateAdditionalInfoQueryParameter(string parameterName, object parameterValue)
		{
			QueryParameter queryParameter = new QueryParameter();
			queryParameter.Name = parameterName;
			if (parameterValue != null)
			{
				queryParameter.Value = Convert.ToString(parameterValue, CultureInfo.InvariantCulture);
				queryParameter.TypeName = parameterValue.GetType().ToString();
			}
			return queryParameter;
		}

		// Token: 0x04003ADF RID: 15071
		private IJobContext m_jobContext;

		// Token: 0x04003AE0 RID: 15072
		private Timer[] m_timers;

		// Token: 0x04003AE1 RID: 15073
		private TimeMetric m_totalTimeMetric;

		// Token: 0x04003AE2 RID: 15074
		private long m_totalRowsRead;

		// Token: 0x04003AE3 RID: 15075
		private long m_totalRowsSkipped;

		// Token: 0x04003AE4 RID: 15076
		private long m_queryDurationMs;

		// Token: 0x04003AE5 RID: 15077
		private long m_dataReaderMappingDurationMs;

		// Token: 0x04003AE6 RID: 15078
		private long m_executeReaderDurationMs;

		// Token: 0x04003AE7 RID: 15079
		private long m_openConnectionDurationMs;

		// Token: 0x04003AE8 RID: 15080
		private long m_disposeDataReaderDurationMs;

		// Token: 0x04003AE9 RID: 15081
		private long m_cancelCommandDurationMs = -2L;

		// Token: 0x04003AEA RID: 15082
		private string m_commandText;

		// Token: 0x04003AEB RID: 15083
		private Microsoft.ReportingServices.ReportIntermediateFormat.DataSet m_specificDataSet;

		// Token: 0x04003AEC RID: 15084
		private IDataParameter[] m_startAtParameters;

		// Token: 0x04003AED RID: 15085
		private IDataParameterCollection m_queryParameters;

		// Token: 0x04003AEE RID: 15086
		private string m_resolvedConnectionString;

		// Token: 0x04003AEF RID: 15087
		private bool? m_connectionFromPool;

		// Token: 0x04003AF0 RID: 15088
		private const long DurationNotMeasured = -2L;

		// Token: 0x02000CF4 RID: 3316
		internal enum MetricType
		{
			// Token: 0x04004FC0 RID: 20416
			ExecuteReader,
			// Token: 0x04004FC1 RID: 20417
			DataReaderMapping,
			// Token: 0x04004FC2 RID: 20418
			Query,
			// Token: 0x04004FC3 RID: 20419
			OpenConnection,
			// Token: 0x04004FC4 RID: 20420
			DisposeDataReader,
			// Token: 0x04004FC5 RID: 20421
			CancelCommand
		}
	}
}
