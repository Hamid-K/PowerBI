using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x0200080E RID: 2062
	internal sealed class RuntimeAggregationIncrementalDataSource : RuntimeIncrementalDataSource
	{
		// Token: 0x0600729B RID: 29339 RVA: 0x001DC99C File Offset: 0x001DAB9C
		internal RuntimeAggregationIncrementalDataSource(DataSet dataSet, OnDemandProcessingContext odpContext)
			: base(odpContext.ReportDefinition, dataSet, odpContext)
		{
		}

		// Token: 0x0600729C RID: 29340 RVA: 0x001DC9AC File Offset: 0x001DABAC
		protected override List<RuntimeDataSet> CreateRuntimeDataSets()
		{
			DataSetInstance dataSetInstance = new DataSetInstance(this.m_dataSet);
			this.m_odpContext.CurrentReportInstance.SetDataSetInstance(dataSetInstance);
			this.m_runtimeDataSet = new RuntimeAggregationIncrementalDataSet(base.DataSourceDefinition, this.m_dataSet, dataSetInstance, base.OdpContext);
			return new List<RuntimeDataSet>(1) { this.m_runtimeDataSet };
		}

		// Token: 0x0600729D RID: 29341 RVA: 0x001DCA08 File Offset: 0x001DAC08
		public void CalculateDataSetAggregates()
		{
			try
			{
				this.m_runtimeDataSet.CalculateDataSetAggregates();
			}
			catch (Exception ex)
			{
				base.HandleException(ex);
				this.FinalCleanup();
				throw;
			}
		}

		// Token: 0x170026D7 RID: 9943
		// (get) Token: 0x0600729E RID: 29342 RVA: 0x001DCA44 File Offset: 0x001DAC44
		public IOnDemandScopeInstance GroupTreeRoot
		{
			get
			{
				return this.m_runtimeDataSet.GroupTreeRoot;
			}
		}

		// Token: 0x170026D8 RID: 9944
		// (get) Token: 0x0600729F RID: 29343 RVA: 0x001DCA51 File Offset: 0x001DAC51
		protected override RuntimeIncrementalDataSet RuntimeDataSet
		{
			get
			{
				return this.m_runtimeDataSet;
			}
		}

		// Token: 0x04003AC3 RID: 15043
		private RuntimeAggregationIncrementalDataSet m_runtimeDataSet;
	}
}
