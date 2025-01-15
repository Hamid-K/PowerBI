using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel
{
	// Token: 0x020007C4 RID: 1988
	internal sealed class TextRunsImpl : TextRuns
	{
		// Token: 0x0600707F RID: 28799 RVA: 0x001D4C6C File Offset: 0x001D2E6C
		internal TextRunsImpl(Paragraph paragraphDef, ReportRuntime reportRT, IErrorContext iErrorContext, IScope scope)
		{
			this.m_textBoxDef = paragraphDef.TextBox;
			this.m_paragraphDef = paragraphDef;
			this.m_reportRT = reportRT;
			this.m_iErrorContext = iErrorContext;
			this.m_scope = scope;
			List<TextRun> textRuns = this.m_paragraphDef.TextRuns;
			if (textRuns != null)
			{
				this.m_textRuns = new TextRunImpl[textRuns.Count];
				return;
			}
			this.m_textRuns = new TextRunImpl[0];
		}

		// Token: 0x17002658 RID: 9816
		public override TextRun this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				TextRunImpl textRunImpl = this.m_textRuns[index];
				if (textRunImpl == null)
				{
					TextRun textRun = this.m_paragraphDef.TextRuns[index];
					textRunImpl = new TextRunImpl(this.m_textBoxDef, textRun, this.m_reportRT, this.m_iErrorContext, this.m_scope);
					this.m_textRuns[index] = textRunImpl;
				}
				return textRunImpl;
			}
		}

		// Token: 0x17002659 RID: 9817
		// (get) Token: 0x06007081 RID: 28801 RVA: 0x001D4D44 File Offset: 0x001D2F44
		internal int Count
		{
			get
			{
				return this.m_textRuns.Length;
			}
		}

		// Token: 0x06007082 RID: 28802 RVA: 0x001D4D50 File Offset: 0x001D2F50
		internal void Reset()
		{
			for (int i = 0; i < this.m_textRuns.Length; i++)
			{
				TextRunImpl textRunImpl = this.m_textRuns[i];
				if (textRunImpl != null)
				{
					textRunImpl.Reset();
				}
			}
		}

		// Token: 0x04003A1F RID: 14879
		private TextBox m_textBoxDef;

		// Token: 0x04003A20 RID: 14880
		private Paragraph m_paragraphDef;

		// Token: 0x04003A21 RID: 14881
		private TextRunImpl[] m_textRuns;

		// Token: 0x04003A22 RID: 14882
		private ReportRuntime m_reportRT;

		// Token: 0x04003A23 RID: 14883
		private IErrorContext m_iErrorContext;

		// Token: 0x04003A24 RID: 14884
		private IScope m_scope;
	}
}
