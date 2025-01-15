using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x020007EE RID: 2030
	internal sealed class DataShapeDataPrefetchManager
	{
		// Token: 0x06007189 RID: 29065 RVA: 0x001D7DA5 File Offset: 0x001D5FA5
		internal DataShapeDataPrefetchManager(OnDemandProcessingContext odpContext)
		{
			this.m_odpContext = odpContext;
			this.m_queryExecutors = new List<RuntimeQueryExecutor>();
		}

		// Token: 0x0600718A RID: 29066 RVA: 0x001D7DC0 File Offset: 0x001D5FC0
		internal void ExecuteQueries()
		{
			bool flag = false;
			try
			{
				if (this.m_odpContext.AbortInfo != null)
				{
					flag = this.m_odpContext.AbortInfo.EnforceSingleAbortException;
					this.m_odpContext.AbortInfo.EnforceSingleAbortException = false;
				}
				this.CreateQueryExecutors();
				this.FetchData();
				this.PopulateQueryCache();
			}
			catch (Exception)
			{
				this.CloseQueryExecutors();
				throw;
			}
			finally
			{
				this.UpdateExecutionLog();
				this.m_queryExecutors.Clear();
				if (this.m_odpContext.AbortInfo != null)
				{
					this.m_odpContext.AbortInfo.EnforceSingleAbortException = flag;
				}
			}
		}

		// Token: 0x0600718B RID: 29067 RVA: 0x001D7E68 File Offset: 0x001D6068
		private void UpdateExecutionLog()
		{
			DataProcessingMetrics dataProcessingMetrics = null;
			for (int i = 0; i < this.m_queryExecutors.Count; i++)
			{
				RuntimeQueryExecutor runtimeQueryExecutor = this.m_queryExecutors[i];
				if (runtimeQueryExecutor != null && (dataProcessingMetrics == null || runtimeQueryExecutor.DataSetExecutionMetrics.TotalDurationMs > dataProcessingMetrics.TotalDurationMs))
				{
					dataProcessingMetrics = runtimeQueryExecutor.DataSetExecutionMetrics;
				}
			}
			if (dataProcessingMetrics != null)
			{
				this.m_odpContext.ExecutionLogContext.AddDataProcessingTime(dataProcessingMetrics.TotalDuration);
			}
		}

		// Token: 0x0600718C RID: 29068 RVA: 0x001D7ED4 File Offset: 0x001D60D4
		private void CloseQueryExecutors()
		{
			for (int i = 0; i < this.m_queryExecutors.Count; i++)
			{
				RuntimeQueryExecutor runtimeQueryExecutor = this.m_queryExecutors[i];
				if (runtimeQueryExecutor != null)
				{
					runtimeQueryExecutor.Close();
				}
			}
		}

		// Token: 0x0600718D RID: 29069 RVA: 0x001D7F10 File Offset: 0x001D6110
		private void CreateQueryExecutors()
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.Report reportDefinition = this.m_odpContext.ReportDefinition;
			List<Microsoft.ReportingServices.ReportIntermediateFormat.DataSet> mappingDataSetIndexToDataSet = reportDefinition.MappingDataSetIndexToDataSet;
			for (int i = 0; i < mappingDataSetIndexToDataSet.Count; i++)
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet = mappingDataSetIndexToDataSet[i];
				int num = reportDefinition.MappingDataSetIndexToDataSourceIndex[i];
				RuntimeQueryExecutor runtimeQueryExecutor = new RuntimeQueryExecutor(reportDefinition.DataSources[num], dataSet, this.m_odpContext);
				this.m_queryExecutors.Add(runtimeQueryExecutor);
			}
		}

		// Token: 0x0600718E RID: 29070 RVA: 0x001D7F80 File Offset: 0x001D6180
		private void PopulateQueryCache()
		{
			ExecutedQueryCache executedQueryCache = this.m_odpContext.StateManager.SetupExecutedQueryCache();
			Global.Tracer.Assert(executedQueryCache != null, "ExecutedQueryCache should have been setup");
			foreach (RuntimeQueryExecutor runtimeQueryExecutor in this.m_queryExecutors)
			{
				ExecutedQuery executedQuery = null;
				try
				{
					runtimeQueryExecutor.CreateQuery(out executedQuery);
					executedQueryCache.Add(executedQuery);
				}
				catch (Exception)
				{
					if (executedQuery != null)
					{
						executedQuery.Close();
					}
					throw;
				}
			}
		}

		// Token: 0x0600718F RID: 29071 RVA: 0x001D801C File Offset: 0x001D621C
		private bool FetchData()
		{
			EventHandler eventHandler = null;
			int count = this.m_queryExecutors.Count;
			ThreadSet threadSet = null;
			try
			{
				if (this.m_odpContext.AbortInfo != null)
				{
					eventHandler = new EventHandler(this.AbortHandler);
					this.m_odpContext.AbortInfo.ProcessingAbortEvent += eventHandler;
				}
				if (count > 1)
				{
					threadSet = new ThreadSet(count - 1);
					try
					{
						for (int i = 0; i < count - 1; i++)
						{
							RuntimeQueryExecutor runtimeQueryExecutor = this.m_queryExecutors[i];
							threadSet.QueueWorkItem(this.m_odpContext, new WaitCallback(runtimeQueryExecutor.ExecuteConcurrent));
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
				if (count > 0)
				{
					this.m_queryExecutors[count - 1].ExecuteConcurrent(null);
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

		// Token: 0x06007190 RID: 29072 RVA: 0x001D8144 File Offset: 0x001D6344
		private void AbortHandler(object sender, EventArgs e)
		{
			if (e is ProcessingAbortEventArgs && ((ProcessingAbortEventArgs)e).UniqueName == this.m_odpContext.ProcessingAbortItemUniqueIdentifier)
			{
				if (Global.Tracer.TraceInfo)
				{
					Global.Tracer.Trace(TraceLevel.Info, "DataPrefetch abort handler called for Report with ID={0}. Aborting data sets ...", new object[] { this.m_odpContext.ProcessingAbortItemUniqueIdentifier });
				}
				for (int i = 0; i < this.m_queryExecutors.Count; i++)
				{
					this.m_queryExecutors[i].Abort();
				}
			}
		}

		// Token: 0x04003A71 RID: 14961
		private readonly OnDemandProcessingContext m_odpContext;

		// Token: 0x04003A72 RID: 14962
		private readonly List<RuntimeQueryExecutor> m_queryExecutors;
	}
}
