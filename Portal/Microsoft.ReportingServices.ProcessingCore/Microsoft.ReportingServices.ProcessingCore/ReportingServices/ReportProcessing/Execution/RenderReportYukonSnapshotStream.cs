using System;
using System.Collections;
using System.Collections.Specialized;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.OnDemandReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing.Execution
{
	// Token: 0x020007DE RID: 2014
	internal sealed class RenderReportYukonSnapshotStream : RenderReportYukonSnapshot
	{
		// Token: 0x0600712A RID: 28970 RVA: 0x001D6F4C File Offset: 0x001D514C
		public RenderReportYukonSnapshotStream(ProcessingContext pc, RenderingContext rc, ReportProcessing processing, string streamName)
			: base(pc, rc, processing)
		{
			this.m_streamName = streamName;
		}

		// Token: 0x0600712B RID: 28971 RVA: 0x001D6F5F File Offset: 0x001D515F
		protected override bool InvokeRenderer(IRenderingExtension renderer, Microsoft.ReportingServices.OnDemandReportRendering.Report report, NameValueCollection reportServerParameters, NameValueCollection deviceInfo, NameValueCollection clientCapabilities, ref Hashtable renderProperties, CreateAndRegisterStream createAndRegisterStream)
		{
			return renderer.RenderStream(this.m_streamName, report, reportServerParameters, deviceInfo, clientCapabilities, ref renderProperties, createAndRegisterStream);
		}

		// Token: 0x17002685 RID: 9861
		// (get) Token: 0x0600712C RID: 28972 RVA: 0x001D6F77 File Offset: 0x001D5177
		protected override bool IsRenderStream
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04003A56 RID: 14934
		private readonly string m_streamName;
	}
}
