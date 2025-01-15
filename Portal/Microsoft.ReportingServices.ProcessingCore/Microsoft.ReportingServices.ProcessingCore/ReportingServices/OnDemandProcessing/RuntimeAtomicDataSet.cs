using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x020007FF RID: 2047
	internal abstract class RuntimeAtomicDataSet : RuntimeDataSet, IRowConsumer
	{
		// Token: 0x06007207 RID: 29191 RVA: 0x001D9907 File Offset: 0x001D7B07
		protected RuntimeAtomicDataSet(Microsoft.ReportingServices.ReportIntermediateFormat.DataSource dataSource, Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet, DataSetInstance dataSetInstance, OnDemandProcessingContext odpContext, bool processRetrievedData)
			: base(dataSource, dataSet, dataSetInstance, odpContext, processRetrievedData)
		{
		}

		// Token: 0x06007208 RID: 29192 RVA: 0x001D9918 File Offset: 0x001D7B18
		internal void ProcessConcurrent(object threadSet)
		{
			Global.Tracer.Assert(this.m_dataSet.Name != null, "The name of a data set cannot be null.");
			try
			{
				if (Global.Tracer.TraceVerbose)
				{
					Global.Tracer.Trace(TraceLevel.Verbose, "Thread has started processing data set '{0}'", new object[] { this.m_dataSet.Name });
				}
				this.Process(null);
			}
			catch (ProcessingAbortedException)
			{
				if (Global.Tracer.TraceWarning)
				{
					Global.Tracer.Trace(TraceLevel.Warning, "Data set '{0}': Report processing has been aborted.", new object[] { this.m_dataSet.Name });
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
					Global.Tracer.Trace(TraceLevel.Error, "An exception has occurred in data set '{0}'. Details: {1}", new object[]
					{
						this.m_dataSet.Name,
						ex.ToString()
					});
				}
				if (this.m_odpContext.AbortInfo == null)
				{
					throw;
				}
				this.m_odpContext.AbortInfo.SetError(ex, this.m_odpContext.ProcessingAbortItemUniqueIdentifier);
			}
			finally
			{
				if (Global.Tracer.TraceVerbose)
				{
					Global.Tracer.Trace(TraceLevel.Verbose, "Processing of data set '{0}' completed.", new object[] { this.m_dataSet.Name });
				}
				ThreadSet threadSet2 = threadSet as ThreadSet;
				if (threadSet2 != null)
				{
					threadSet2.ThreadCompleted();
				}
			}
		}

		// Token: 0x06007209 RID: 29193 RVA: 0x001D9A98 File Offset: 0x001D7C98
		public void ProcessInline(ExecutedQuery existingQuery)
		{
			this.Process(existingQuery);
		}

		// Token: 0x0600720A RID: 29194 RVA: 0x001D9AA4 File Offset: 0x001D7CA4
		private void Process(ExecutedQuery existingQuery)
		{
			this.InitializeDataSet();
			try
			{
				try
				{
					this.InitializeRowSourceAndProcessRows(existingQuery);
				}
				finally
				{
					this.CleanupProcess();
				}
				this.AllRowsRead();
				this.TeardownDataSet();
			}
			catch (RSException)
			{
				throw;
			}
			catch (Exception ex)
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex))
				{
					throw;
				}
				this.CleanupForException();
				throw;
			}
			finally
			{
				this.FinalCleanup();
			}
		}

		// Token: 0x0600720B RID: 29195 RVA: 0x001D9B24 File Offset: 0x001D7D24
		protected virtual void InitializeRowSourceAndProcessRows(ExecutedQuery existingQuery)
		{
			if (this.m_dataSet.IsReferenceToSharedDataSet)
			{
				base.ProcessSharedDataSetReference();
				return;
			}
			if (existingQuery != null)
			{
				base.InitializeAndRunFromExistingQuery(existingQuery);
			}
			else
			{
				base.InitializeAndRunLiveQuery();
			}
			if (base.ProcessRetrievedData)
			{
				this.ProcessRows();
			}
		}

		// Token: 0x0600720C RID: 29196 RVA: 0x001D9B5B File Offset: 0x001D7D5B
		protected virtual void AllRowsRead()
		{
		}

		// Token: 0x0600720D RID: 29197 RVA: 0x001D9B60 File Offset: 0x001D7D60
		protected void ProcessRows()
		{
			int num;
			for (Microsoft.ReportingServices.ReportIntermediateFormat.RecordRow recordRow = base.ReadOneRow(out num); recordRow != null; recordRow = base.ReadOneRow(out num))
			{
				this.ProcessRow(recordRow, num);
			}
		}

		// Token: 0x0600720E RID: 29198
		protected abstract void ProcessRow(Microsoft.ReportingServices.ReportIntermediateFormat.RecordRow aRow, int rowNumber);

		// Token: 0x0600720F RID: 29199 RVA: 0x001D9B8C File Offset: 0x001D7D8C
		public virtual void SetProcessingDataReader(IProcessingDataReader dataReader)
		{
			this.m_dataReader = dataReader;
			this.m_dataReader.OverrideWithDataReaderSettings(this.m_odpContext, this.m_dataSetInstance, this.m_dataSet.DataSetCore);
			if (base.ProcessRetrievedData)
			{
				this.m_dataReader.GetDataReaderMappingForRowConsumer(this.m_dataSetInstance, out this.m_iRowConsumerMappingIdentical, out this.m_iRowConsumerMappingDataSetFieldIndexesToDataChunk);
			}
			this.InitializeBeforeProcessingRows(base.HasServerAggregateMetadata);
		}

		// Token: 0x06007210 RID: 29200 RVA: 0x001D9BF4 File Offset: 0x001D7DF4
		public void NextRow(Microsoft.ReportingServices.ReportIntermediateFormat.RecordRow originalRow)
		{
			if (!base.ProcessRetrievedData)
			{
				return;
			}
			this.m_odpContext.CheckAndThrowIfAborted();
			if (this.m_dataRowsRead == 0)
			{
				this.InitializeBeforeFirstRow(true);
			}
			Microsoft.ReportingServices.ReportIntermediateFormat.RecordRow recordRow;
			if (this.m_iRowConsumerMappingIdentical)
			{
				recordRow = originalRow;
			}
			else
			{
				recordRow = new Microsoft.ReportingServices.ReportIntermediateFormat.RecordRow(originalRow, this.m_iRowConsumerMappingDataSetFieldIndexesToDataChunk);
			}
			if (this.m_dataSet.IsReferenceToSharedDataSet && recordRow.IsAggregateRow && this.m_dataSet.InterpretSubtotalsAsDetails != Microsoft.ReportingServices.ReportIntermediateFormat.DataSet.TriState.False)
			{
				recordRow.IsAggregateRow = false;
			}
			this.ProcessRow(recordRow, this.m_dataRowsRead);
			base.IncrementRowCounterAndTrace();
		}

		// Token: 0x170026B5 RID: 9909
		// (get) Token: 0x06007211 RID: 29201 RVA: 0x001D9C7C File Offset: 0x001D7E7C
		public string ReportDataSetName
		{
			get
			{
				return this.m_dataSet.Name;
			}
		}

		// Token: 0x04003A9E RID: 15006
		private int[] m_iRowConsumerMappingDataSetFieldIndexesToDataChunk;

		// Token: 0x04003A9F RID: 15007
		private bool m_iRowConsumerMappingIdentical;
	}
}
