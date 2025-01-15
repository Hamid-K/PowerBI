using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000202 RID: 514
	internal abstract class RichTextParser
	{
		// Token: 0x06001339 RID: 4921 RVA: 0x000507B9 File Offset: 0x0004E9B9
		internal RichTextParser(bool allowMultipleParagraphs, IRichTextInstanceCreator iRichTextInstanceCreator, IRichTextLogger richTextLogger)
		{
			this.m_allowMultipleParagraphs = allowMultipleParagraphs;
			this.m_IRichTextInstanceCreator = iRichTextInstanceCreator;
			this.m_richTextLogger = richTextLogger;
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x000507D8 File Offset: 0x0004E9D8
		internal virtual IList<ICompiledParagraphInstance> Parse(string richText)
		{
			this.m_currentStyle = new CompiledStyleInfo();
			this.m_currentParagraph = new CompiledParagraphInfo();
			this.m_paragraphInstanceCollection = this.m_IRichTextInstanceCreator.CreateParagraphInstanceCollection();
			if (!string.IsNullOrEmpty(richText))
			{
				this.InternalParse(richText);
			}
			this.m_currentParagraph = new CompiledParagraphInfo();
			if (this.m_paragraphInstanceCollection.Count == 0)
			{
				this.m_currentParagraphInstance = this.CreateParagraphInstance();
				this.m_currentTextRunInstance = this.CreateTextRunInstance();
				this.m_currentParagraphInstance.Style = this.m_IRichTextInstanceCreator.CreateStyleInstance(true);
				this.m_currentTextRunInstance.Style = this.m_IRichTextInstanceCreator.CreateStyleInstance(false);
			}
			else
			{
				for (int i = 0; i < this.m_paragraphInstanceCollection.Count; i++)
				{
					this.m_currentParagraphInstance = this.m_paragraphInstanceCollection[i];
					if (this.m_currentParagraphInstance.CompiledTextRunInstances == null || this.m_currentParagraphInstance.CompiledTextRunInstances.Count == 0)
					{
						this.m_currentTextRunInstance = this.CreateTextRunInstance();
						this.m_currentTextRunInstance.Style = this.m_IRichTextInstanceCreator.CreateStyleInstance(true);
					}
				}
			}
			this.CloseParagraph();
			return this.m_paragraphInstanceCollection;
		}

		// Token: 0x0600133B RID: 4923
		protected abstract void InternalParse(string richText);

		// Token: 0x0600133C RID: 4924 RVA: 0x000508F1 File Offset: 0x0004EAF1
		protected virtual bool AppendText(string value)
		{
			return this.AppendText(value, false);
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x000508FC File Offset: 0x0004EAFC
		protected virtual bool AppendText(string value, bool onlyIfValueExists)
		{
			if (this.m_currentParagraphInstance != null)
			{
				IList<ICompiledTextRunInstance> compiledTextRunInstances = this.m_currentParagraphInstance.CompiledTextRunInstances;
				if (compiledTextRunInstances.Count > 0)
				{
					this.m_currentTextRunInstance = compiledTextRunInstances[compiledTextRunInstances.Count - 1];
					if (onlyIfValueExists && string.IsNullOrEmpty(this.m_currentTextRunInstance.Value))
					{
						this.m_currentTextRunInstance = null;
						return false;
					}
				}
			}
			this.SetTextRunValue(value);
			return true;
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x00050960 File Offset: 0x0004EB60
		protected virtual void SetTextRunValue(string value)
		{
			if (this.m_currentTextRunInstance == null)
			{
				this.m_currentTextRunInstance = this.CreateTextRunInstance();
			}
			this.m_currentTextRunInstance.Value = this.m_currentTextRunInstance.Value + value;
			if (this.m_currentTextRunInstance.Style == null)
			{
				ICompiledStyleInstance compiledStyleInstance = this.m_IRichTextInstanceCreator.CreateStyleInstance(false);
				this.m_currentStyle.PopulateStyleInstance(compiledStyleInstance, false);
				this.m_currentTextRunInstance.Style = compiledStyleInstance;
			}
			if (this.m_currentParagraphInstance.Style == null)
			{
				this.m_currentParagraphInstance.Style = this.m_IRichTextInstanceCreator.CreateStyleInstance(true);
			}
			this.m_currentTextRunInstance = null;
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x000509FC File Offset: 0x0004EBFC
		protected virtual ICompiledParagraphInstance CreateParagraphInstance()
		{
			if (!this.m_allowMultipleParagraphs && this.m_onlyParagraphInstance != null)
			{
				this.m_currentParagraphInstance = this.m_onlyParagraphInstance;
				this.AppendText(Environment.NewLine, true);
				return this.m_onlyParagraphInstance;
			}
			ICompiledParagraphInstance compiledParagraphInstance = this.m_IRichTextInstanceCreator.CreateParagraphInstance();
			if (this.m_allowMultipleParagraphs)
			{
				this.m_currentParagraph.PopulateParagraph(compiledParagraphInstance);
				int listLevel = compiledParagraphInstance.ListLevel;
				if (listLevel > 9)
				{
					if (!this.m_loggedListLevelWarning)
					{
						this.m_richTextLogger.RegisterOutOfRangeSizeWarning("ListLevel", Convert.ToString(listLevel, CultureInfo.InvariantCulture), Convert.ToString(0, CultureInfo.InvariantCulture), Convert.ToString(9, CultureInfo.InvariantCulture));
						this.m_loggedListLevelWarning = true;
					}
					compiledParagraphInstance.ListLevel = 9;
				}
			}
			else
			{
				this.m_onlyParagraphInstance = compiledParagraphInstance;
			}
			ICompiledStyleInstance compiledStyleInstance = this.m_IRichTextInstanceCreator.CreateStyleInstance(true);
			this.m_currentStyle.PopulateStyleInstance(compiledStyleInstance, true);
			compiledParagraphInstance.Style = compiledStyleInstance;
			IList<ICompiledTextRunInstance> list = this.m_IRichTextInstanceCreator.CreateTextRunInstanceCollection();
			compiledParagraphInstance.CompiledTextRunInstances = list;
			this.m_paragraphInstanceCollection.Add(compiledParagraphInstance);
			return compiledParagraphInstance;
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x00050AF8 File Offset: 0x0004ECF8
		protected virtual ICompiledTextRunInstance CreateTextRunInstance()
		{
			if (this.m_currentParagraphInstance == null)
			{
				this.m_currentParagraphInstance = this.CreateParagraphInstance();
			}
			ICollection<ICompiledTextRunInstance> compiledTextRunInstances = this.m_currentParagraphInstance.CompiledTextRunInstances;
			ICompiledTextRunInstance compiledTextRunInstance = this.m_IRichTextInstanceCreator.CreateTextRunInstance();
			ICompiledStyleInstance compiledStyleInstance = this.m_IRichTextInstanceCreator.CreateStyleInstance(false);
			compiledTextRunInstance.Style = compiledStyleInstance;
			this.m_currentStyle.PopulateStyleInstance(compiledStyleInstance, false);
			compiledTextRunInstances.Add(compiledTextRunInstance);
			return compiledTextRunInstance;
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x00050B58 File Offset: 0x0004ED58
		protected virtual void CloseParagraph()
		{
			this.m_currentParagraphInstance = null;
			this.m_currentTextRunInstance = null;
		}

		// Token: 0x0400093E RID: 2366
		protected CompiledStyleInfo m_currentStyle;

		// Token: 0x0400093F RID: 2367
		protected CompiledParagraphInfo m_currentParagraph;

		// Token: 0x04000940 RID: 2368
		protected static ReportSize DefaultParagraphSpacing = new ReportSize("10pt");

		// Token: 0x04000941 RID: 2369
		internal const int ParagraphListLevelMin = 0;

		// Token: 0x04000942 RID: 2370
		internal const int ParagraphListLevelMax = 9;

		// Token: 0x04000943 RID: 2371
		protected bool m_allowMultipleParagraphs;

		// Token: 0x04000944 RID: 2372
		protected ICompiledParagraphInstance m_currentParagraphInstance;

		// Token: 0x04000945 RID: 2373
		protected ICompiledTextRunInstance m_currentTextRunInstance;

		// Token: 0x04000946 RID: 2374
		protected IRichTextInstanceCreator m_IRichTextInstanceCreator;

		// Token: 0x04000947 RID: 2375
		protected IList<ICompiledParagraphInstance> m_paragraphInstanceCollection;

		// Token: 0x04000948 RID: 2376
		protected ICompiledParagraphInstance m_onlyParagraphInstance;

		// Token: 0x04000949 RID: 2377
		protected IRichTextLogger m_richTextLogger;

		// Token: 0x0400094A RID: 2378
		private bool m_loggedListLevelWarning;

		// Token: 0x0400094B RID: 2379
		private const string m_propertyListLevel = "ListLevel";
	}
}
