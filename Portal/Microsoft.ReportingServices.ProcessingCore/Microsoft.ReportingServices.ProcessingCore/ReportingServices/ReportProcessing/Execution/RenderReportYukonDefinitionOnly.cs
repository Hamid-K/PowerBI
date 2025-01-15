using System;
using Microsoft.ReportingServices.OnDemandReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing.Execution
{
	// Token: 0x020007DC RID: 2012
	internal sealed class RenderReportYukonDefinitionOnly : RenderReportYukonInitial
	{
		// Token: 0x06007122 RID: 28962 RVA: 0x001D6BDD File Offset: 0x001D4DDD
		public RenderReportYukonDefinitionOnly(ProcessingContext pc, RenderingContext rc, DateTime executionTimeStamp, ReportProcessing processing, IChunkFactory yukonCompiledDefinition)
			: base(pc, rc, executionTimeStamp, processing, yukonCompiledDefinition)
		{
		}

		// Token: 0x06007123 RID: 28963 RVA: 0x001D6BEC File Offset: 0x001D4DEC
		protected override Report PrepareROM(out RenderingContext odpRenderingContext)
		{
			odpRenderingContext = new RenderingContext(base.PublicRenderingContext.Format, this.m_reportSnapshot, base.PublicProcessingContext.ChunkFactory, base.PublicRenderingContext.EventInfo);
			odpRenderingContext.InstanceAccessDisallowed = true;
			return new Report(this.m_reportSnapshot.Report, this.m_reportSnapshot.ReportInstance, this.m_renderingContext, odpRenderingContext, base.ReportName, base.PublicRenderingContext.ReportDescription);
		}
	}
}
