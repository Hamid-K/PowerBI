using System;
using System.Diagnostics;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x0200080A RID: 2058
	internal abstract class RuntimeIncrementalDataSource : RuntimeDataSource
	{
		// Token: 0x06007283 RID: 29315 RVA: 0x001DC595 File Offset: 0x001DA795
		protected RuntimeIncrementalDataSource(Microsoft.ReportingServices.ReportIntermediateFormat.Report report, Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet, OnDemandProcessingContext odpContext)
			: base(report, dataSet.DataSource, odpContext, false)
		{
			this.m_dataSet = dataSet;
		}

		// Token: 0x06007284 RID: 29316 RVA: 0x001DC5B0 File Offset: 0x001DA7B0
		internal void Initialize()
		{
			ExecutedQuery executedQuery = null;
			try
			{
				ExecutedQueryCache executedQueryCache = this.m_odpContext.StateManager.ExecutedQueryCache;
				if (executedQueryCache != null)
				{
					executedQueryCache.Extract(this.m_dataSet, out executedQuery);
				}
				base.InitializeDataSource(executedQuery);
				this.InitializeDataSet(executedQuery);
			}
			catch (Exception ex)
			{
				base.HandleException(ex);
				this.FinalCleanup();
				if (executedQuery != null)
				{
					executedQuery.Close();
				}
				throw;
			}
		}

		// Token: 0x06007285 RID: 29317 RVA: 0x001DC61C File Offset: 0x001DA81C
		internal override void Abort()
		{
			if (Global.Tracer.TraceVerbose)
			{
				Global.Tracer.Trace(TraceLevel.Verbose, "Data source '{0}': Abort handler called.", new object[] { this.m_dataSource.Name });
			}
			if (this.RuntimeDataSet != null)
			{
				this.RuntimeDataSet.Abort();
			}
		}

		// Token: 0x170026D0 RID: 9936
		// (get) Token: 0x06007286 RID: 29318
		protected abstract RuntimeIncrementalDataSet RuntimeDataSet { get; }

		// Token: 0x06007287 RID: 29319 RVA: 0x001DC66C File Offset: 0x001DA86C
		protected void InitializeDataSet(ExecutedQuery existingQuery)
		{
			this.RuntimeDataSet.InitProcessingParams(this.m_connection, this.m_transaction);
			this.RuntimeDataSet.Initialize(existingQuery);
		}

		// Token: 0x06007288 RID: 29320 RVA: 0x001DC694 File Offset: 0x001DA894
		internal void Teardown()
		{
			try
			{
				this.TeardownDataSet();
				base.TeardownDataSource();
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

		// Token: 0x06007289 RID: 29321 RVA: 0x001DC6DC File Offset: 0x001DA8DC
		protected override void FinalCleanup()
		{
			base.FinalCleanup();
			if (this.RuntimeDataSet != null)
			{
				TimeMetric timeMetric = this.RuntimeDataSet.DataSetExecutionMetrics.TotalDuration;
				if (this.m_totalDurationFromExistingQuery != null)
				{
					timeMetric = new TimeMetric(timeMetric);
					timeMetric.Subtract(this.m_totalDurationFromExistingQuery);
				}
				this.m_odpContext.ExecutionLogContext.AddDataProcessingTime(timeMetric);
				this.m_executionMetrics.Add(this.RuntimeDataSet.DataSetExecutionMetrics);
			}
		}

		// Token: 0x0600728A RID: 29322 RVA: 0x001DC74A File Offset: 0x001DA94A
		protected virtual void TeardownDataSet()
		{
			this.RuntimeDataSet.Teardown();
		}

		// Token: 0x170026D1 RID: 9937
		// (get) Token: 0x0600728B RID: 29323 RVA: 0x001DC757 File Offset: 0x001DA957
		internal override bool NoRows
		{
			get
			{
				return base.CheckNoRows(this.RuntimeDataSet);
			}
		}

		// Token: 0x0600728C RID: 29324 RVA: 0x001DC765 File Offset: 0x001DA965
		internal void RecordSkippedRowCount(long rowCount)
		{
			this.RuntimeDataSet.RecordSkippedRowCount(rowCount);
		}

		// Token: 0x04003ABE RID: 15038
		protected readonly Microsoft.ReportingServices.ReportIntermediateFormat.DataSet m_dataSet;
	}
}
