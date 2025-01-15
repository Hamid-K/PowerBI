using System;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x02000808 RID: 2056
	internal sealed class RuntimeDataSourceStreamingDataProcessing : RuntimeDataSourceDataProcessing
	{
		// Token: 0x0600727C RID: 29308 RVA: 0x001DC475 File Offset: 0x001DA675
		internal RuntimeDataSourceStreamingDataProcessing(DataSet dataSet, OnDemandProcessingContext processingContext)
			: base(dataSet, processingContext)
		{
		}

		// Token: 0x0600727D RID: 29309 RVA: 0x001DC480 File Offset: 0x001DA680
		protected override RuntimeOnDemandDataSet CreateRuntimeDataSet()
		{
			DataSetInstance dataSetInstance = new DataSetInstance(this.m_dataSet);
			this.m_odpContext.CurrentReportInstance.SetDataSetInstance(dataSetInstance);
			return new RuntimeOnDemandDataSet(base.DataSourceDefinition, this.m_dataSet, dataSetInstance, this.m_odpContext, true, false, false);
		}
	}
}
