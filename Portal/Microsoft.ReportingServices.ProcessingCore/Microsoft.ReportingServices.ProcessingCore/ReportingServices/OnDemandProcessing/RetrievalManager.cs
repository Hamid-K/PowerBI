using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.ProcessingRenderingCommon;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x020007FE RID: 2046
	internal class RetrievalManager
	{
		// Token: 0x060071FC RID: 29180 RVA: 0x001D934D File Offset: 0x001D754D
		internal RetrievalManager(Microsoft.ReportingServices.ReportIntermediateFormat.Report report, OnDemandProcessingContext context)
		{
			this.m_report = report;
			this.m_odpContext = context;
		}

		// Token: 0x060071FD RID: 29181 RVA: 0x001D936E File Offset: 0x001D756E
		internal RetrievalManager(DataSetDefinition dataSetDefinition, OnDemandProcessingContext context)
		{
			this.m_dataSetDefinition = dataSetDefinition;
			this.m_odpContext = context;
		}

		// Token: 0x170026B4 RID: 9908
		// (get) Token: 0x060071FE RID: 29182 RVA: 0x001D938F File Offset: 0x001D758F
		internal bool NoRows
		{
			get
			{
				return this.m_noRows;
			}
		}

		// Token: 0x060071FF RID: 29183 RVA: 0x001D9398 File Offset: 0x001D7598
		internal void FetchParameterData(ReportParameterDataSetCache aCache, int aDataSourceIndex, int aDataSetIndex)
		{
			RuntimeDataSourceParameters runtimeDataSourceParameters = new RuntimeDataSourceParameters(this.m_report, this.m_report.DataSources[aDataSourceIndex], this.m_odpContext, aDataSetIndex, aCache);
			this.m_runtimeDataSources.Add(runtimeDataSourceParameters);
			this.FetchData();
		}

		// Token: 0x06007200 RID: 29184 RVA: 0x001D93E0 File Offset: 0x001D75E0
		internal bool FetchSharedDataSet(ParameterInfoCollection parameters)
		{
			if (parameters != null && parameters.Count != 0)
			{
				this.m_odpContext.ReportObjectModel.ParametersImpl.Clear();
				this.m_odpContext.ReportObjectModel.Initialize(parameters);
			}
			if (this.m_odpContext.ExternalDataSetContext.CachedDataChunkName == null)
			{
				return this.FetchSharedDataSetLive();
			}
			return this.FetchSharedDataSetCached();
		}

		// Token: 0x06007201 RID: 29185 RVA: 0x001D9440 File Offset: 0x001D7640
		private bool FetchSharedDataSetCached()
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet = new Microsoft.ReportingServices.ReportIntermediateFormat.DataSet(this.m_dataSetDefinition.DataSetCore);
			ProcessingDataReader processingDataReader = new ProcessingDataReader(new DataSetInstance(dataSet), dataSet, this.m_odpContext, true);
			IRowConsumer consumerRequest = this.m_odpContext.ExternalDataSetContext.ConsumerRequest;
			consumerRequest.SetProcessingDataReader(processingDataReader);
			long num = 0L;
			try
			{
				while (processingDataReader.GetNextRow())
				{
					if (num.IsMultipleOf(100))
					{
						Microsoft.ReportingServices.Diagnostics.ProcessingContext.DelayUntilResourcesAvailableBlocking();
					}
					Microsoft.ReportingServices.ReportIntermediateFormat.RecordRow underlyingRecordRowObject = processingDataReader.GetUnderlyingRecordRowObject();
					consumerRequest.NextRow(underlyingRecordRowObject);
					num += 1L;
				}
			}
			finally
			{
				if (this.m_odpContext.JobContext != null)
				{
					object syncRoot = this.m_odpContext.JobContext.SyncRoot;
					lock (syncRoot)
					{
						this.m_odpContext.JobContext.RowCount += num;
					}
				}
			}
			return true;
		}

		// Token: 0x06007202 RID: 29186 RVA: 0x001D952C File Offset: 0x001D772C
		private bool FetchSharedDataSetLive()
		{
			this.m_runtimeDataSources.Add(new RuntimeDataSourceSharedDataSet(this.m_dataSetDefinition, this.m_odpContext));
			bool flag;
			try
			{
				flag = this.FetchData();
			}
			catch
			{
				this.m_runtimeDataSources[0].EraseDataChunk();
				throw;
			}
			finally
			{
				this.FinallyBlockForDataSetExecution();
			}
			return flag;
		}

		// Token: 0x06007203 RID: 29187 RVA: 0x001D9598 File Offset: 0x001D7798
		internal bool PrefetchData(Microsoft.ReportingServices.ReportIntermediateFormat.ReportInstance reportInstance, ParameterInfoCollection parameters, bool mergeTran)
		{
			if (this.m_report.DataSourceCount == 0)
			{
				return true;
			}
			bool flag2;
			try
			{
				bool flag = true;
				for (int i = 0; i < this.m_report.DataSourceCount; i++)
				{
					this.m_runtimeDataSources.Add(new RuntimeDataSourcePrefetch(this.m_report, reportInstance, this.m_report.DataSources[i], this.m_odpContext, mergeTran));
				}
				flag &= this.FetchData();
				if (this.m_report.ParametersNotUsedInQuery && this.m_odpContext.ErrorSavingSnapshotData)
				{
					for (int j = 0; j < parameters.Count; j++)
					{
						parameters[j].UsedInQuery = true;
					}
					flag2 = false;
				}
				else
				{
					flag2 = flag;
				}
			}
			catch
			{
				foreach (RuntimeAtomicDataSource runtimeAtomicDataSource in this.m_runtimeDataSources)
				{
					runtimeAtomicDataSource.EraseDataChunk();
				}
				throw;
			}
			finally
			{
				this.FinallyBlockForDataSetExecution();
			}
			return flag2;
		}

		// Token: 0x06007204 RID: 29188 RVA: 0x001D96AC File Offset: 0x001D78AC
		private void FinallyBlockForDataSetExecution()
		{
			this.m_noRows = true;
			DataProcessingMetrics dataProcessingMetrics = null;
			foreach (RuntimeDataSource runtimeDataSource in this.m_runtimeDataSources)
			{
				if (dataProcessingMetrics == null || runtimeDataSource.ExecutionMetrics.TotalDurationMs > dataProcessingMetrics.TotalDurationMs)
				{
					dataProcessingMetrics = runtimeDataSource.ExecutionMetrics;
				}
				if (!runtimeDataSource.NoRows)
				{
					this.m_noRows = false;
				}
			}
			if (dataProcessingMetrics != null)
			{
				this.m_odpContext.ExecutionLogContext.AddDataProcessingTime(dataProcessingMetrics.TotalDuration);
			}
			this.m_runtimeDataSources.Clear();
		}

		// Token: 0x06007205 RID: 29189 RVA: 0x001D9754 File Offset: 0x001D7954
		private bool FetchData()
		{
			EventHandler eventHandler = null;
			int count = this.m_runtimeDataSources.Count;
			ThreadSet threadSet = null;
			try
			{
				if (this.m_odpContext.AbortInfo != null)
				{
					eventHandler = new EventHandler(this.AbortHandler);
					this.m_odpContext.AbortInfo.ProcessingAbortEvent += eventHandler;
				}
				if (count != 0)
				{
					RuntimeAtomicDataSource runtimeAtomicDataSource;
					if (count > 1)
					{
						threadSet = new ThreadSet(count - 1);
						try
						{
							for (int i = 1; i < count; i++)
							{
								runtimeAtomicDataSource = this.m_runtimeDataSources[i];
								threadSet.TryQueueWorkItem(this.m_odpContext, new WaitCallback(runtimeAtomicDataSource.ProcessConcurrent));
							}
						}
						catch (Exception ex)
						{
							if (this.m_odpContext.AbortInfo != null)
							{
								this.m_odpContext.AbortInfo.SetError(ex, this.m_odpContext.ProcessingAbortItemUniqueIdentifier);
							}
							throw;
						}
					}
					runtimeAtomicDataSource = this.m_runtimeDataSources[0];
					runtimeAtomicDataSource.ProcessConcurrent(null);
				}
			}
			finally
			{
				if (threadSet != null && count > 1)
				{
					threadSet.WaitForCompletion();
					threadSet.Dispose();
				}
				if (eventHandler != null)
				{
					this.m_odpContext.AbortInfo.ProcessingAbortEvent -= eventHandler;
				}
			}
			this.m_odpContext.CheckAndThrowIfAborted();
			return true;
		}

		// Token: 0x06007206 RID: 29190 RVA: 0x001D987C File Offset: 0x001D7A7C
		private void AbortHandler(object sender, EventArgs e)
		{
			if (e is ProcessingAbortEventArgs && ((ProcessingAbortEventArgs)e).UniqueName == this.m_odpContext.ProcessingAbortItemUniqueIdentifier)
			{
				if (Global.Tracer.TraceInfo)
				{
					Global.Tracer.Trace(TraceLevel.Info, "DataPrefetch abort handler called for Report with ID={0}. Aborting data sources ...", new object[] { this.m_odpContext.ProcessingAbortItemUniqueIdentifier });
				}
				int count = this.m_runtimeDataSources.Count;
				for (int i = 0; i < count; i++)
				{
					this.m_runtimeDataSources[i].Abort();
				}
			}
		}

		// Token: 0x04003A99 RID: 15001
		private Microsoft.ReportingServices.ReportIntermediateFormat.Report m_report;

		// Token: 0x04003A9A RID: 15002
		private DataSetDefinition m_dataSetDefinition;

		// Token: 0x04003A9B RID: 15003
		private bool m_noRows;

		// Token: 0x04003A9C RID: 15004
		private OnDemandProcessingContext m_odpContext;

		// Token: 0x04003A9D RID: 15005
		private List<RuntimeAtomicDataSource> m_runtimeDataSources = new List<RuntimeAtomicDataSource>();
	}
}
