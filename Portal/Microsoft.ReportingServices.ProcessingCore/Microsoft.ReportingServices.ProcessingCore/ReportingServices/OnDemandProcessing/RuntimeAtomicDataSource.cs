using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x02000802 RID: 2050
	internal abstract class RuntimeAtomicDataSource : RuntimeDataSource
	{
		// Token: 0x06007260 RID: 29280 RVA: 0x001DBC41 File Offset: 0x001D9E41
		protected RuntimeAtomicDataSource(Microsoft.ReportingServices.ReportIntermediateFormat.Report report, Microsoft.ReportingServices.ReportIntermediateFormat.DataSource dataSource, OnDemandProcessingContext processingContext, bool mergeTransactions)
			: base(report, dataSource, processingContext, mergeTransactions)
		{
		}

		// Token: 0x06007261 RID: 29281 RVA: 0x001DBC50 File Offset: 0x001D9E50
		internal void ProcessConcurrent(object threadSet)
		{
			try
			{
				if (Global.Tracer.TraceVerbose)
				{
					Global.Tracer.Trace(TraceLevel.Verbose, "Thread has started processing data source '{0}'", new object[] { base.DataSourceDefinition.Name });
				}
				this.Process(false);
			}
			catch (ProcessingAbortedException)
			{
				if (Global.Tracer.TraceWarning)
				{
					Global.Tracer.Trace(TraceLevel.Warning, "Data source '{0}': Report processing has been aborted.", new object[] { base.DataSourceDefinition.Name });
				}
				if (this.m_odpContext.StreamingMode)
				{
					throw;
				}
			}
			catch (Exception ex)
			{
				if (Global.Tracer.TraceError)
				{
					Global.Tracer.Trace(TraceLevel.Error, "An exception has occurred in data source '{0}'. Details: {1}", new object[]
					{
						base.DataSourceDefinition.Name,
						ex.ToString()
					});
				}
				if (base.OdpContext.AbortInfo == null)
				{
					throw;
				}
				base.OdpContext.AbortInfo.SetError(ex, base.OdpContext.ProcessingAbortItemUniqueIdentifier);
			}
			finally
			{
				if (Global.Tracer.TraceVerbose)
				{
					Global.Tracer.Trace(TraceLevel.Verbose, "Processing of data source '{0}' completed.", new object[] { base.DataSourceDefinition.Name });
				}
				ThreadSet threadSet2 = threadSet as ThreadSet;
				if (threadSet2 != null)
				{
					threadSet2.ThreadCompleted();
				}
			}
		}

		// Token: 0x06007262 RID: 29282 RVA: 0x001DBDB4 File Offset: 0x001D9FB4
		private void Process(bool fromOdp)
		{
			try
			{
				if (base.InitializeDataSource(null))
				{
					if (this.m_useConcurrentDataSetProcessing)
					{
						this.ExecuteParallelDataSets();
					}
					else
					{
						this.ExecuteSequentialDataSets();
					}
					base.TeardownDataSource();
				}
			}
			catch (Exception ex)
			{
				base.HandleException(ex);
				throw;
			}
			finally
			{
				this.FinalCleanup();
			}
		}

		// Token: 0x06007263 RID: 29283 RVA: 0x001DBE18 File Offset: 0x001DA018
		private void ExecuteSequentialDataSets()
		{
			for (int i = 0; i < this.m_runtimeDataSets.Count; i++)
			{
				this.m_odpContext.CheckAndThrowIfAborted();
				RuntimeAtomicDataSet runtimeAtomicDataSet = (RuntimeAtomicDataSet)this.m_runtimeDataSets[i];
				runtimeAtomicDataSet.InitProcessingParams(this.m_connection, this.m_transaction);
				runtimeAtomicDataSet.ProcessConcurrent(null);
				this.m_executionMetrics.Add(runtimeAtomicDataSet.DataSetExecutionMetrics);
			}
		}

		// Token: 0x06007264 RID: 29284 RVA: 0x001DBE84 File Offset: 0x001DA084
		private void ExecuteParallelDataSets()
		{
			ThreadSet threadSet = new ThreadSet(this.m_runtimeDataSets.Count - 1);
			try
			{
				for (int i = 1; i < this.m_runtimeDataSets.Count; i++)
				{
					RuntimeAtomicDataSet runtimeAtomicDataSet = (RuntimeAtomicDataSet)this.m_runtimeDataSets[i];
					runtimeAtomicDataSet.InitProcessingParams(null, this.m_transaction);
					threadSet.TryQueueWorkItem(this.m_odpContext, new WaitCallback(runtimeAtomicDataSet.ProcessConcurrent));
				}
				RuntimeAtomicDataSet runtimeAtomicDataSet2 = (RuntimeAtomicDataSet)this.m_runtimeDataSets[0];
				runtimeAtomicDataSet2.InitProcessingParams(this.m_connection, this.m_transaction);
				runtimeAtomicDataSet2.ProcessConcurrent(null);
			}
			catch (Exception ex)
			{
				if (this.m_odpContext.AbortInfo != null)
				{
					this.m_odpContext.AbortInfo.SetError(ex, this.m_odpContext.ProcessingAbortItemUniqueIdentifier);
				}
				throw;
			}
			finally
			{
				threadSet.WaitForCompletion();
				threadSet.Dispose();
			}
			if (this.NeedsExecutionLogging && this.m_odpContext.JobContext != null)
			{
				DataProcessingMetrics dataProcessingMetrics = null;
				for (int j = 0; j < this.m_runtimeDataSets.Count; j++)
				{
					RuntimeDataSet runtimeDataSet = this.m_runtimeDataSets[j];
					if (dataProcessingMetrics == null || runtimeDataSet.DataSetExecutionMetrics.TotalDurationMs > dataProcessingMetrics.TotalDurationMs)
					{
						dataProcessingMetrics = runtimeDataSet.DataSetExecutionMetrics;
					}
				}
				this.m_executionMetrics.Add(dataProcessingMetrics);
			}
		}
	}
}
