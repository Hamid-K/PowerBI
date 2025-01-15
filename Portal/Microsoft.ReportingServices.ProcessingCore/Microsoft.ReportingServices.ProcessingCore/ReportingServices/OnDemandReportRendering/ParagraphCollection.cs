using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000314 RID: 788
	public sealed class ParagraphCollection : ReportElementCollectionBase<Paragraph>
	{
		// Token: 0x06001D31 RID: 7473 RVA: 0x000737C0 File Offset: 0x000719C0
		internal ParagraphCollection(Microsoft.ReportingServices.OnDemandReportRendering.TextBox textBox)
		{
			this.m_textBox = textBox;
			if (this.m_textBox.IsOldSnapshot)
			{
				this.m_paragraphs = new Paragraph[1];
				return;
			}
			List<Paragraph> paragraphs = this.m_textBox.TexBoxDef.Paragraphs;
			if (paragraphs != null)
			{
				this.m_paragraphs = new Paragraph[paragraphs.Count];
				return;
			}
			this.m_paragraphs = new Paragraph[0];
		}

		// Token: 0x17001057 RID: 4183
		public override Paragraph this[int i]
		{
			get
			{
				if (i < 0 || i >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { i, 0, this.Count });
				}
				Paragraph paragraph = this.m_paragraphs[i];
				if (paragraph == null)
				{
					if (this.m_textBox.IsOldSnapshot)
					{
						paragraph = new ShimParagraph(this.m_textBox, this.m_textBox.RenderingContext);
					}
					else
					{
						Paragraph paragraph2 = this.m_textBox.TexBoxDef.Paragraphs[i];
						paragraph = new InternalParagraph(this.m_textBox, i, paragraph2, this.m_textBox.RenderingContext);
					}
					this.m_paragraphs[i] = paragraph;
				}
				return paragraph;
			}
		}

		// Token: 0x17001058 RID: 4184
		// (get) Token: 0x06001D33 RID: 7475 RVA: 0x000738DF File Offset: 0x00071ADF
		public override int Count
		{
			get
			{
				return this.m_paragraphs.Length;
			}
		}

		// Token: 0x06001D34 RID: 7476 RVA: 0x000738EC File Offset: 0x00071AEC
		internal void SetNewContext()
		{
			for (int i = 0; i < this.m_paragraphs.Length; i++)
			{
				if (this.m_paragraphs[i] != null)
				{
					this.m_paragraphs[i].SetNewContext();
				}
			}
		}

		// Token: 0x04000F38 RID: 3896
		private Microsoft.ReportingServices.OnDemandReportRendering.TextBox m_textBox;

		// Token: 0x04000F39 RID: 3897
		private Paragraph[] m_paragraphs;
	}
}
