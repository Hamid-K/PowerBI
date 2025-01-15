using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200032B RID: 811
	public sealed class ParagraphInstanceEnumerator : IEnumerator<ParagraphInstance>, IDisposable, IEnumerator
	{
		// Token: 0x06001E5C RID: 7772 RVA: 0x0007612A File Offset: 0x0007432A
		internal ParagraphInstanceEnumerator(TextBox textbox)
		{
			this.m_textbox = textbox;
		}

		// Token: 0x1700110C RID: 4364
		// (get) Token: 0x06001E5D RID: 7773 RVA: 0x00076139 File Offset: 0x00074339
		public ParagraphInstance Current
		{
			get
			{
				return this.m_currentParagraphInstance;
			}
		}

		// Token: 0x06001E5E RID: 7774 RVA: 0x00076141 File Offset: 0x00074341
		public void Dispose()
		{
			this.Reset();
		}

		// Token: 0x1700110D RID: 4365
		// (get) Token: 0x06001E5F RID: 7775 RVA: 0x00076149 File Offset: 0x00074349
		object IEnumerator.Current
		{
			get
			{
				return this.m_currentParagraphInstance;
			}
		}

		// Token: 0x06001E60 RID: 7776 RVA: 0x00076154 File Offset: 0x00074354
		public bool MoveNext()
		{
			if (this.m_currentIndex < this.m_textbox.Paragraphs.Count)
			{
				Paragraph paragraph = this.m_textbox.Paragraphs[this.m_currentIndex];
				if (paragraph.TextRuns.Count == 1 && paragraph.TextRuns[0].Instance.MarkupType != MarkupType.None)
				{
					if (this.m_paragraphs == null)
					{
						this.m_paragraphs = paragraph.TextRuns[0].CompiledInstance.CompiledParagraphInstances;
					}
					if (this.m_currentCompiledIndex >= this.m_paragraphs.Count)
					{
						this.m_paragraphs = null;
						this.m_currentCompiledIndex = 0;
						this.m_currentIndex++;
						return this.MoveNext();
					}
					this.m_currentParagraphInstance = this.m_paragraphs[this.m_currentCompiledIndex];
					this.m_currentCompiledIndex++;
				}
				else
				{
					this.m_currentParagraphInstance = paragraph.Instance;
					this.m_currentIndex++;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001E61 RID: 7777 RVA: 0x0007625D File Offset: 0x0007445D
		public void Reset()
		{
			this.m_paragraphs = null;
			this.m_currentParagraphInstance = null;
			this.m_currentIndex = 0;
			this.m_currentCompiledIndex = 0;
		}

		// Token: 0x04000F8F RID: 3983
		private TextBox m_textbox;

		// Token: 0x04000F90 RID: 3984
		private ParagraphInstance m_currentParagraphInstance;

		// Token: 0x04000F91 RID: 3985
		private int m_currentCompiledIndex;

		// Token: 0x04000F92 RID: 3986
		private int m_currentIndex;

		// Token: 0x04000F93 RID: 3987
		private CompiledParagraphInstanceCollection m_paragraphs;
	}
}
