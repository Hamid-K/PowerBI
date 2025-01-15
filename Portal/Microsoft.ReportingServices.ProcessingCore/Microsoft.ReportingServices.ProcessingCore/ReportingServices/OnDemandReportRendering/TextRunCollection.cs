using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000318 RID: 792
	public sealed class TextRunCollection : ReportElementCollectionBase<TextRun>
	{
		// Token: 0x06001D5A RID: 7514 RVA: 0x00073D14 File Offset: 0x00071F14
		internal TextRunCollection(Paragraph paragraph)
		{
			this.m_paragraph = paragraph;
			if (this.m_paragraph.IsOldSnapshot)
			{
				this.m_textRuns = new TextRun[1];
				return;
			}
			List<TextRun> textRuns = ((InternalParagraph)this.m_paragraph).ParagraphDef.TextRuns;
			if (textRuns != null)
			{
				this.m_textRuns = new TextRun[textRuns.Count];
				return;
			}
			this.m_textRuns = new TextRun[0];
		}

		// Token: 0x17001076 RID: 4214
		public override TextRun this[int i]
		{
			get
			{
				if (i < 0 || i >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { i, 0, this.Count });
				}
				TextRun textRun = this.m_textRuns[i];
				if (textRun == null)
				{
					if (this.m_paragraph.IsOldSnapshot)
					{
						textRun = new ShimTextRun(this.m_paragraph, this.m_paragraph.RenderingContext);
					}
					else
					{
						TextRun textRun2 = ((InternalParagraph)this.m_paragraph).ParagraphDef.TextRuns[i];
						textRun = new InternalTextRun(this.m_paragraph, i, textRun2, this.m_paragraph.RenderingContext);
					}
					this.m_textRuns[i] = textRun;
				}
				return textRun;
			}
		}

		// Token: 0x17001077 RID: 4215
		// (get) Token: 0x06001D5C RID: 7516 RVA: 0x00073E3C File Offset: 0x0007203C
		public override int Count
		{
			get
			{
				return this.m_textRuns.Length;
			}
		}

		// Token: 0x06001D5D RID: 7517 RVA: 0x00073E48 File Offset: 0x00072048
		internal void SetNewContext()
		{
			for (int i = 0; i < this.m_textRuns.Length; i++)
			{
				if (this.m_textRuns[i] != null)
				{
					this.m_textRuns[i].SetNewContext();
				}
			}
		}

		// Token: 0x04000F47 RID: 3911
		private Paragraph m_paragraph;

		// Token: 0x04000F48 RID: 3912
		private TextRun[] m_textRuns;
	}
}
