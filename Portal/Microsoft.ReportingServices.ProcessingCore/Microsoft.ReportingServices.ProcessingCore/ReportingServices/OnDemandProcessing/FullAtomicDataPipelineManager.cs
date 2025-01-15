using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x020007EB RID: 2027
	internal sealed class FullAtomicDataPipelineManager : AtomicDataPipelineManager
	{
		// Token: 0x06007179 RID: 29049 RVA: 0x001D7C20 File Offset: 0x001D5E20
		public FullAtomicDataPipelineManager(OnDemandProcessingContext odpContext, DataSet dataSet)
			: base(odpContext, dataSet)
		{
		}

		// Token: 0x0600717A RID: 29050 RVA: 0x001D7C2A File Offset: 0x001D5E2A
		protected override RuntimeDataSourceDataProcessing CreateDataSource()
		{
			return new RuntimeDataSourceFullDataProcessing(this.m_dataSet, this.m_odpContext);
		}
	}
}
