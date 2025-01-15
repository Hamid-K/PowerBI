using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing.Execution
{
	// Token: 0x020007D4 RID: 2004
	internal class RenderReportOdpLiveAndCacheData : RenderReportOdpInitial
	{
		// Token: 0x060070FF RID: 28927 RVA: 0x001D64FB File Offset: 0x001D46FB
		public RenderReportOdpLiveAndCacheData(ProcessingContext pc, RenderingContext rc, DateTime executionTimeStamp, IConfiguration configuration, IChunkFactory metaDataChunkFactory)
			: base(pc, rc, executionTimeStamp, configuration)
		{
			Global.Tracer.Assert(metaDataChunkFactory != null, "Must supply a IChunkFactory to store the cached data");
			this.m_metaDataChunkFactory = metaDataChunkFactory;
		}

		// Token: 0x06007100 RID: 28928 RVA: 0x001D6524 File Offset: 0x001D4724
		protected override void CleanupSuccessfulProcessing(ProcessingErrorContext errorContext)
		{
			ChunkManager.OnDemandProcessingManager.SerializeMetadata(this.m_metaDataChunkFactory, base.OdpContext.OdpMetadata, base.OdpContext.GetActiveCompatibilityVersion(), base.OdpContext.ProhibitSerializableValues);
			base.CleanupSuccessfulProcessing(errorContext);
		}

		// Token: 0x04003A4A RID: 14922
		private IChunkFactory m_metaDataChunkFactory;
	}
}
