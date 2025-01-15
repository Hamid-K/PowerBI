using System;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel
{
	// Token: 0x020007C3 RID: 1987
	internal sealed class ParagraphImpl : Paragraph
	{
		// Token: 0x0600707C RID: 28796 RVA: 0x001D4C3E File Offset: 0x001D2E3E
		internal ParagraphImpl(Paragraph paragraphDef, ReportRuntime reportRT, IErrorContext iErrorContext, IScope scope)
		{
			this.m_textRuns = new TextRunsImpl(paragraphDef, reportRT, iErrorContext, scope);
		}

		// Token: 0x17002657 RID: 9815
		// (get) Token: 0x0600707D RID: 28797 RVA: 0x001D4C56 File Offset: 0x001D2E56
		public override TextRuns TextRuns
		{
			get
			{
				return this.m_textRuns;
			}
		}

		// Token: 0x0600707E RID: 28798 RVA: 0x001D4C5E File Offset: 0x001D2E5E
		internal void Reset()
		{
			this.m_textRuns.Reset();
		}

		// Token: 0x04003A1E RID: 14878
		private TextRunsImpl m_textRuns;
	}
}
