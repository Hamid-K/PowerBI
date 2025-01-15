using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x02000804 RID: 2052
	internal abstract class RuntimeDataSourceDataProcessing : RuntimeAtomicDataSource
	{
		// Token: 0x0600726A RID: 29290 RVA: 0x001DC19D File Offset: 0x001DA39D
		internal RuntimeDataSourceDataProcessing(DataSet dataSet, OnDemandProcessingContext processingContext)
			: base(processingContext.ReportDefinition, dataSet.DataSource, processingContext, false)
		{
			this.m_dataSet = dataSet;
		}

		// Token: 0x0600726B RID: 29291 RVA: 0x001DC1BC File Offset: 0x001DA3BC
		internal void ProcessSingleOdp()
		{
			ExecutedQuery executedQuery = null;
			try
			{
				ExecutedQueryCache executedQueryCache = this.m_odpContext.StateManager.ExecutedQueryCache;
				if (executedQueryCache != null)
				{
					executedQueryCache.Extract(this.m_dataSet, out executedQuery);
				}
				if (base.InitializeDataSource(executedQuery))
				{
					this.m_runtimeDataSet.InitProcessingParams(this.m_connection, this.m_transaction);
					this.m_runtimeDataSet.ProcessInline(executedQuery);
					this.m_executionMetrics.Add(this.m_runtimeDataSet.DataSetExecutionMetrics);
					if (this.m_totalDurationFromExistingQuery != null)
					{
						this.m_executionMetrics.TotalDuration.Subtract(this.m_totalDurationFromExistingQuery);
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
				if (executedQuery != null)
				{
					executedQuery.Close();
				}
			}
		}

		// Token: 0x0600726C RID: 29292 RVA: 0x001DC290 File Offset: 0x001DA490
		protected override List<RuntimeDataSet> CreateRuntimeDataSets()
		{
			this.m_runtimeDataSet = this.CreateRuntimeDataSet();
			return new List<RuntimeDataSet>(1) { this.m_runtimeDataSet };
		}

		// Token: 0x0600726D RID: 29293
		protected abstract RuntimeOnDemandDataSet CreateRuntimeDataSet();

		// Token: 0x170026C9 RID: 9929
		// (get) Token: 0x0600726E RID: 29294 RVA: 0x001DC2B0 File Offset: 0x001DA4B0
		internal RuntimeOnDemandDataSet RuntimeDataSet
		{
			get
			{
				return this.m_runtimeDataSet;
			}
		}

		// Token: 0x170026CA RID: 9930
		// (get) Token: 0x0600726F RID: 29295 RVA: 0x001DC2B8 File Offset: 0x001DA4B8
		internal override bool NoRows
		{
			get
			{
				return base.CheckNoRows(this.m_runtimeDataSet);
			}
		}

		// Token: 0x04003AB7 RID: 15031
		protected readonly DataSet m_dataSet;

		// Token: 0x04003AB8 RID: 15032
		private RuntimeOnDemandDataSet m_runtimeDataSet;
	}
}
