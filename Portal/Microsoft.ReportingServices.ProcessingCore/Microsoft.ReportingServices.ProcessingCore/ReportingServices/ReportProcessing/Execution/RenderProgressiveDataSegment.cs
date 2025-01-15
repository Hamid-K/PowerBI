using System;
using System.IO;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing.Execution
{
	// Token: 0x020007D0 RID: 2000
	internal sealed class RenderProgressiveDataSegment : RenderStreamingOdp
	{
		// Token: 0x060070D4 RID: 28884 RVA: 0x001D5D32 File Offset: 0x001D3F32
		public RenderProgressiveDataSegment(ProgressiveProcessingContext pc, ICatalogItemContext reportContext, ParameterInfoCollection parameters, Microsoft.ReportingServices.ReportIntermediateFormat.Report compiledDefinition, GlobalIDOwnerCollection globalIDOwnerCollection, string description, Stream dataSegmentQuery, IDataSegmentRenderer renderer)
			: base(pc, reportContext, parameters, compiledDefinition, pc.DataSources, globalIDOwnerCollection)
		{
			this.m_description = description;
			this.m_dataSegmentQuery = dataSegmentQuery;
			this.m_renderer = renderer;
		}

		// Token: 0x060070D5 RID: 28885 RVA: 0x001D5D60 File Offset: 0x001D3F60
		protected override RenderingContext Render(ExecutionLogContext executionLogContext)
		{
			RenderingContext renderingContext;
			Microsoft.ReportingServices.OnDemandReportRendering.Report report = this.PrepareROM(out renderingContext);
			if (this.m_renderer == null)
			{
				this.m_renderer = DataSegmentRendererFactory.CreateDataSegmentRenderer();
			}
			executionLogContext.StartRenderingTimer();
			this.m_renderer.RenderSegment(report, this.m_dataSegmentQuery, base.PublicProcessingContext.CreateStreamCallback);
			return renderingContext;
		}

		// Token: 0x060070D6 RID: 28886 RVA: 0x001D5DB0 File Offset: 0x001D3FB0
		private Microsoft.ReportingServices.OnDemandReportRendering.Report PrepareROM(out RenderingContext odpRenderingContext)
		{
			odpRenderingContext = new RenderingContext("RPDS", this.m_odpReportSnapshot, null, this.m_odpContext);
			return new Microsoft.ReportingServices.OnDemandReportRendering.Report(this.m_odpReportSnapshot.Report, this.m_odpReportSnapshot.ReportInstance, odpRenderingContext, base.ReportName, this.m_description);
		}

		// Token: 0x04003A40 RID: 14912
		internal const string DataSegmentRendererFormatString = "RPDS";

		// Token: 0x04003A41 RID: 14913
		private readonly string m_description;

		// Token: 0x04003A42 RID: 14914
		private readonly Stream m_dataSegmentQuery;

		// Token: 0x04003A43 RID: 14915
		private IDataSegmentRenderer m_renderer;
	}
}
