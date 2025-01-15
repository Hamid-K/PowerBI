using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x02000809 RID: 2057
	internal abstract class RuntimeIncrementalDataSet : RuntimeDataSet
	{
		// Token: 0x0600727E RID: 29310 RVA: 0x001DC4C5 File Offset: 0x001DA6C5
		protected RuntimeIncrementalDataSet(Microsoft.ReportingServices.ReportIntermediateFormat.DataSource dataSource, Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet, DataSetInstance dataSetInstance, OnDemandProcessingContext odpContext)
			: base(dataSource, dataSet, dataSetInstance, odpContext, true)
		{
		}

		// Token: 0x0600727F RID: 29311 RVA: 0x001DC4D4 File Offset: 0x001DA6D4
		internal void Initialize(ExecutedQuery existingQuery)
		{
			try
			{
				this.InitializeDataSet();
				if (this.m_dataSet.IsReferenceToSharedDataSet)
				{
					Global.Tracer.Assert(false, "Shared data sets cannot be used with a RuntimeIncrementalDataSet");
				}
				else if (existingQuery != null)
				{
					base.InitializeAndRunFromExistingQuery(existingQuery);
				}
				else
				{
					base.InitializeAndRunLiveQuery();
				}
			}
			catch (Exception)
			{
				this.CleanupForException();
				this.FinalCleanup();
				throw;
			}
		}

		// Token: 0x06007280 RID: 29312 RVA: 0x001DC53C File Offset: 0x001DA73C
		internal void Teardown()
		{
			try
			{
				this.CleanupProcess();
				this.TeardownDataSet();
			}
			catch (Exception)
			{
				this.CleanupForException();
				throw;
			}
			finally
			{
				this.FinalCleanup();
			}
		}

		// Token: 0x06007281 RID: 29313 RVA: 0x001DC584 File Offset: 0x001DA784
		internal void RecordSkippedRowCount(long rowCount)
		{
			this.m_executionMetrics.AddSkippedRowCount(rowCount);
		}

		// Token: 0x170026CF RID: 9935
		// (get) Token: 0x06007282 RID: 29314 RVA: 0x001DC592 File Offset: 0x001DA792
		protected override bool ShouldCancelCommandDuringCleanup
		{
			get
			{
				return true;
			}
		}
	}
}
