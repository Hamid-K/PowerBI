using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel
{
	// Token: 0x020007C2 RID: 1986
	internal sealed class ParagraphsImpl : Paragraphs
	{
		// Token: 0x06007078 RID: 28792 RVA: 0x001D4B3C File Offset: 0x001D2D3C
		internal ParagraphsImpl(TextBox textBoxDef, ReportRuntime reportRT, IErrorContext iErrorContext, IScope scope)
		{
			this.m_textBoxDef = textBoxDef;
			this.m_reportRT = reportRT;
			this.m_iErrorContext = iErrorContext;
			this.m_scope = scope;
			List<Paragraph> paragraphs = this.m_textBoxDef.Paragraphs;
			if (paragraphs != null)
			{
				this.m_paragraphs = new ParagraphImpl[paragraphs.Count];
				return;
			}
			this.m_paragraphs = new ParagraphImpl[0];
		}

		// Token: 0x17002655 RID: 9813
		public override Paragraph this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				ParagraphImpl paragraphImpl = this.m_paragraphs[index];
				if (paragraphImpl == null)
				{
					paragraphImpl = new ParagraphImpl(this.m_textBoxDef.Paragraphs[index], this.m_reportRT, this.m_iErrorContext, this.m_scope);
					this.m_paragraphs[index] = paragraphImpl;
				}
				return paragraphImpl;
			}
		}

		// Token: 0x17002656 RID: 9814
		// (get) Token: 0x0600707A RID: 28794 RVA: 0x001D4C00 File Offset: 0x001D2E00
		internal int Count
		{
			get
			{
				return this.m_paragraphs.Length;
			}
		}

		// Token: 0x0600707B RID: 28795 RVA: 0x001D4C0C File Offset: 0x001D2E0C
		internal void Reset()
		{
			for (int i = 0; i < this.m_paragraphs.Length; i++)
			{
				ParagraphImpl paragraphImpl = this.m_paragraphs[i];
				if (paragraphImpl != null)
				{
					paragraphImpl.Reset();
				}
			}
		}

		// Token: 0x04003A19 RID: 14873
		private TextBox m_textBoxDef;

		// Token: 0x04003A1A RID: 14874
		private ParagraphImpl[] m_paragraphs;

		// Token: 0x04003A1B RID: 14875
		private ReportRuntime m_reportRT;

		// Token: 0x04003A1C RID: 14876
		private IErrorContext m_iErrorContext;

		// Token: 0x04003A1D RID: 14877
		private IScope m_scope;
	}
}
