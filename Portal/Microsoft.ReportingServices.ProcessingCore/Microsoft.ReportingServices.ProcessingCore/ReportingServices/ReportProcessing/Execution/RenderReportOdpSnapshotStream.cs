using System;
using System.Collections;
using System.Collections.Specialized;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.OnDemandReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing.Execution
{
	// Token: 0x020007D6 RID: 2006
	internal class RenderReportOdpSnapshotStream : RenderReportOdpSnapshot
	{
		// Token: 0x06007106 RID: 28934 RVA: 0x001D6633 File Offset: 0x001D4833
		public RenderReportOdpSnapshotStream(ProcessingContext pc, RenderingContext rc, IConfiguration configuration, string streamName)
			: base(pc, rc, configuration)
		{
			this.m_streamName = streamName;
		}

		// Token: 0x06007107 RID: 28935 RVA: 0x001D6646 File Offset: 0x001D4846
		protected override bool InvokeRenderer(IRenderingExtension renderer, Microsoft.ReportingServices.OnDemandReportRendering.Report report, NameValueCollection reportServerParameters, NameValueCollection deviceInfo, NameValueCollection clientCapabilities, ref Hashtable renderProperties, CreateAndRegisterStream createAndRegisterStream)
		{
			ProcessingContext.DelayUntilResourcesAvailableBlocking();
			return renderer.RenderStream(this.m_streamName, report, reportServerParameters, deviceInfo, clientCapabilities, ref renderProperties, createAndRegisterStream);
		}

		// Token: 0x1700267F RID: 9855
		// (get) Token: 0x06007108 RID: 28936 RVA: 0x001D6663 File Offset: 0x001D4863
		protected override bool IsRenderStream
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04003A4B RID: 14923
		private readonly string m_streamName;
	}
}
