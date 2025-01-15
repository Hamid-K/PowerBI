using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x020007EA RID: 2026
	internal sealed class StreamingAtomicDataPipelineManager : AtomicDataPipelineManager
	{
		// Token: 0x06007176 RID: 29046 RVA: 0x001D7BE8 File Offset: 0x001D5DE8
		public StreamingAtomicDataPipelineManager(OnDemandProcessingContext odpContext, DataSet dataSet)
			: base(odpContext, dataSet)
		{
		}

		// Token: 0x06007177 RID: 29047 RVA: 0x001D7BF2 File Offset: 0x001D5DF2
		protected override RuntimeDataSourceDataProcessing CreateDataSource()
		{
			return new RuntimeDataSourceStreamingDataProcessing(this.m_dataSet, this.m_odpContext);
		}

		// Token: 0x06007178 RID: 29048 RVA: 0x001D7C05 File Offset: 0x001D5E05
		public override void Abort()
		{
			base.Abort();
			if (this.RuntimeDataSource != null)
			{
				this.RuntimeDataSource.Abort();
			}
		}
	}
}
