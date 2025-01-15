using System;
using System.Diagnostics;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x02000813 RID: 2067
	internal sealed class RuntimeQueryExecutor : RuntimeLiveQueryExecutor
	{
		// Token: 0x060072CE RID: 29390 RVA: 0x001DD939 File Offset: 0x001DBB39
		internal RuntimeQueryExecutor(Microsoft.ReportingServices.ReportIntermediateFormat.DataSource dataSource, Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet, OnDemandProcessingContext odpContext)
			: base(dataSource, dataSet, odpContext)
		{
		}

		// Token: 0x060072CF RID: 29391 RVA: 0x001DD944 File Offset: 0x001DBB44
		internal void CreateQuery(out ExecutedQuery query)
		{
			query = new ExecutedQuery(this.m_dataSource, this.m_dataSet, this.m_odpContext, this.m_executionMetrics, this.m_commandText, this.m_queryExecutionTimestamp, this.m_errorInspector);
			query.AssumeOwnership(ref this.m_dataSourceConnection, ref this.m_command, ref this.m_commandWrappedForCancel, ref this.m_dataReader);
		}

		// Token: 0x060072D0 RID: 29392 RVA: 0x001DD9A4 File Offset: 0x001DBBA4
		internal void ExecuteConcurrent(object threadSet)
		{
			try
			{
				if (Global.Tracer.TraceVerbose)
				{
					Global.Tracer.Trace(TraceLevel.Verbose, "Thread has started query execution for data set '{0}'", new object[] { this.m_dataSet.Name });
				}
				this.Execute();
			}
			catch (ProcessingAbortedException)
			{
				if (Global.Tracer.TraceWarning)
				{
					Global.Tracer.Trace(TraceLevel.Warning, "Data set '{0}': query execution has been aborted.", new object[] { this.m_dataSet.Name });
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

		// Token: 0x060072D1 RID: 29393 RVA: 0x001DDAF8 File Offset: 0x001DBCF8
		private void Execute()
		{
			this.m_odpContext.CheckAndThrowIfAborted();
			this.m_queryExecutionTimestamp = DateTime.Now;
			this.m_executionMetrics.StartTotalTimer();
			try
			{
				this.m_isConnectionOwner = true;
				base.RunLiveQuery(null, null);
			}
			finally
			{
				this.m_executionMetrics.RecordTotalTimerMeasurement();
			}
			this.m_odpContext.CheckAndThrowIfAborted();
		}

		// Token: 0x060072D2 RID: 29394 RVA: 0x001DDB60 File Offset: 0x001DBD60
		protected override void StoreDataReader(IDataReader dataReader, DataSourceErrorInspector errorInspector)
		{
			this.m_dataReader = dataReader;
			this.m_errorInspector = errorInspector;
		}

		// Token: 0x060072D3 RID: 29395 RVA: 0x001DDB70 File Offset: 0x001DBD70
		protected override void ExtractRewrittenCommandText(IDbCommand command)
		{
		}

		// Token: 0x060072D4 RID: 29396 RVA: 0x001DDB72 File Offset: 0x001DBD72
		protected override void SetRestartPosition(IDbCommand command)
		{
		}

		// Token: 0x060072D5 RID: 29397 RVA: 0x001DDB74 File Offset: 0x001DBD74
		protected override void StoreCommandText(string commandText)
		{
			this.m_commandText = commandText;
		}

		// Token: 0x060072D6 RID: 29398 RVA: 0x001DDB7D File Offset: 0x001DBD7D
		protected override void EagerInlineReaderCleanup(ref IDataReader reader)
		{
			base.DisposeDataExtensionObject<IDataReader>(ref reader, "data reader");
		}

		// Token: 0x060072D7 RID: 29399 RVA: 0x001DDB8B File Offset: 0x001DBD8B
		internal void Close()
		{
			base.CancelCommand();
			base.DisposeDataExtensionObject<IDataReader>(ref this.m_dataReader, "data reader", new DataProcessingMetrics.MetricType?(DataProcessingMetrics.MetricType.DisposeDataReader));
			base.DisposeCommand();
			base.CloseConnection();
		}

		// Token: 0x04003AD3 RID: 15059
		private IDataReader m_dataReader;

		// Token: 0x04003AD4 RID: 15060
		private DataSourceErrorInspector m_errorInspector;

		// Token: 0x04003AD5 RID: 15061
		private DateTime m_queryExecutionTimestamp;

		// Token: 0x04003AD6 RID: 15062
		private string m_commandText;
	}
}
