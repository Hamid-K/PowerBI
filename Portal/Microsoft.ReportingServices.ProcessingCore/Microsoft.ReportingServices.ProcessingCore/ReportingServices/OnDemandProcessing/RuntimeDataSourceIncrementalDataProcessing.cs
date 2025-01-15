using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x0200080C RID: 2060
	internal sealed class RuntimeDataSourceIncrementalDataProcessing : RuntimeIncrementalDataSource
	{
		// Token: 0x06007292 RID: 29330 RVA: 0x001DC838 File Offset: 0x001DAA38
		internal RuntimeDataSourceIncrementalDataProcessing(DataSet dataSet, OnDemandProcessingContext odpContext)
			: base(odpContext.ReportDefinition, dataSet, odpContext)
		{
		}

		// Token: 0x06007293 RID: 29331 RVA: 0x001DC848 File Offset: 0x001DAA48
		protected override List<RuntimeDataSet> CreateRuntimeDataSets()
		{
			DataSetInstance dataSetInstance = new DataSetInstance(this.m_dataSet);
			this.m_odpContext.CurrentReportInstance.SetDataSetInstance(dataSetInstance);
			this.m_runtimeDataSet = new RuntimeOnDemandIncrementalDataSet(base.DataSourceDefinition, this.m_dataSet, dataSetInstance, base.OdpContext);
			return new List<RuntimeDataSet>(1) { this.m_runtimeDataSet };
		}

		// Token: 0x06007294 RID: 29332 RVA: 0x001DC8A4 File Offset: 0x001DAAA4
		public void Advance()
		{
			try
			{
				this.m_runtimeDataSet.Advance();
			}
			catch (Exception ex)
			{
				base.HandleException(ex);
				this.FinalCleanup();
				throw;
			}
		}

		// Token: 0x170026D4 RID: 9940
		// (get) Token: 0x06007295 RID: 29333 RVA: 0x001DC8E0 File Offset: 0x001DAAE0
		public IOnDemandScopeInstance GroupTreeRoot
		{
			get
			{
				return this.m_runtimeDataSet.GroupTreeRoot;
			}
		}

		// Token: 0x170026D5 RID: 9941
		// (get) Token: 0x06007296 RID: 29334 RVA: 0x001DC8ED File Offset: 0x001DAAED
		protected override RuntimeIncrementalDataSet RuntimeDataSet
		{
			get
			{
				return this.m_runtimeDataSet;
			}
		}

		// Token: 0x04003AC1 RID: 15041
		private RuntimeOnDemandIncrementalDataSet m_runtimeDataSet;
	}
}
