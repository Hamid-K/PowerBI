using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x0200080B RID: 2059
	internal sealed class RuntimeLiveReaderDataSource : RuntimeIncrementalDataSource
	{
		// Token: 0x0600728D RID: 29325 RVA: 0x001DC773 File Offset: 0x001DA973
		internal RuntimeLiveReaderDataSource(Report report, DataSet dataSet, OnDemandProcessingContext odpContext)
			: base(report, dataSet, odpContext)
		{
		}

		// Token: 0x0600728E RID: 29326 RVA: 0x001DC780 File Offset: 0x001DA980
		protected override List<RuntimeDataSet> CreateRuntimeDataSets()
		{
			this.m_dataSetInstance = new DataSetInstance(this.m_dataSet);
			this.m_odpContext.CurrentReportInstance.SetDataSetInstance(this.m_dataSetInstance);
			this.m_runtimeDataSet = new RuntimeLiveDataReaderDataSet(base.DataSourceDefinition, this.m_dataSet, this.m_dataSetInstance, base.OdpContext);
			return new List<RuntimeDataSet>(1) { this.m_runtimeDataSet };
		}

		// Token: 0x0600728F RID: 29327 RVA: 0x001DC7EC File Offset: 0x001DA9EC
		public RecordRow ReadNextRow()
		{
			RecordRow recordRow;
			try
			{
				recordRow = this.m_runtimeDataSet.ReadNextRow();
			}
			catch (Exception ex)
			{
				base.HandleException(ex);
				this.FinalCleanup();
				throw;
			}
			return recordRow;
		}

		// Token: 0x170026D2 RID: 9938
		// (get) Token: 0x06007290 RID: 29328 RVA: 0x001DC828 File Offset: 0x001DAA28
		public DataSetInstance DataSetInstance
		{
			get
			{
				return this.m_dataSetInstance;
			}
		}

		// Token: 0x170026D3 RID: 9939
		// (get) Token: 0x06007291 RID: 29329 RVA: 0x001DC830 File Offset: 0x001DAA30
		protected override RuntimeIncrementalDataSet RuntimeDataSet
		{
			get
			{
				return this.m_runtimeDataSet;
			}
		}

		// Token: 0x04003ABF RID: 15039
		private DataSetInstance m_dataSetInstance;

		// Token: 0x04003AC0 RID: 15040
		private RuntimeLiveDataReaderDataSet m_runtimeDataSet;
	}
}
