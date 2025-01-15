using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200032C RID: 812
	public sealed class TextRunInstanceEnumerator : IEnumerator<TextRunInstance>, IDisposable, IEnumerator
	{
		// Token: 0x06001E62 RID: 7778 RVA: 0x0007627B File Offset: 0x0007447B
		internal TextRunInstanceEnumerator(ParagraphInstance paragraphInstance)
		{
			this.m_paragraphInstance = paragraphInstance;
		}

		// Token: 0x1700110E RID: 4366
		// (get) Token: 0x06001E63 RID: 7779 RVA: 0x0007628A File Offset: 0x0007448A
		public TextRunInstance Current
		{
			get
			{
				return this.m_textRunInstance;
			}
		}

		// Token: 0x06001E64 RID: 7780 RVA: 0x00076292 File Offset: 0x00074492
		public void Dispose()
		{
			this.Reset();
		}

		// Token: 0x1700110F RID: 4367
		// (get) Token: 0x06001E65 RID: 7781 RVA: 0x0007629A File Offset: 0x0007449A
		object IEnumerator.Current
		{
			get
			{
				return this.m_textRunInstance;
			}
		}

		// Token: 0x06001E66 RID: 7782 RVA: 0x000762A4 File Offset: 0x000744A4
		public bool MoveNext()
		{
			TextRunCollection textRuns = this.m_paragraphInstance.Definition.TextRuns;
			if (this.m_currentIndex < textRuns.Count)
			{
				TextRun textRun = textRuns[this.m_currentIndex];
				if (textRun.Instance.MarkupType != MarkupType.None)
				{
					if (this.m_textRunInstances == null)
					{
						if (textRuns.Count > 1)
						{
							this.m_textRunInstances = textRun.CompiledInstance.CompiledParagraphInstances[0].CompiledTextRunInstances;
						}
						else
						{
							this.m_textRunInstances = ((CompiledParagraphInstance)this.m_paragraphInstance).CompiledTextRunInstances;
						}
					}
					if (this.m_currentCompiledIndex >= this.m_textRunInstances.Count)
					{
						this.m_textRunInstances = null;
						this.m_currentCompiledIndex = 0;
						this.m_currentIndex++;
						return this.MoveNext();
					}
					this.m_textRunInstance = this.m_textRunInstances[this.m_currentCompiledIndex];
					this.m_currentCompiledIndex++;
				}
				else
				{
					this.m_textRunInstance = textRun.Instance;
					this.m_currentIndex++;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001E67 RID: 7783 RVA: 0x000763AF File Offset: 0x000745AF
		public void Reset()
		{
			this.m_textRunInstance = null;
			this.m_currentIndex = 0;
			this.m_currentCompiledIndex = 0;
		}

		// Token: 0x04000F94 RID: 3988
		private ParagraphInstance m_paragraphInstance;

		// Token: 0x04000F95 RID: 3989
		private TextRunInstance m_textRunInstance;

		// Token: 0x04000F96 RID: 3990
		private int m_currentIndex;

		// Token: 0x04000F97 RID: 3991
		private int m_currentCompiledIndex;

		// Token: 0x04000F98 RID: 3992
		private CompiledTextRunInstanceCollection m_textRunInstances;
	}
}
