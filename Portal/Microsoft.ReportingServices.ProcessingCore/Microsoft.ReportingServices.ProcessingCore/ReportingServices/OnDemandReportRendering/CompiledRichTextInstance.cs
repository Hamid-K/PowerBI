using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000326 RID: 806
	public sealed class CompiledRichTextInstance : BaseInstance, IRichTextInstanceCreator, IRichTextLogger
	{
		// Token: 0x06001E1F RID: 7711 RVA: 0x000758FC File Offset: 0x00073AFC
		internal CompiledRichTextInstance(IReportScope reportScope, TextRun textRunDef, Paragraph paragraphDef, bool multipleParagraphsAllowed)
			: base(reportScope)
		{
			this.m_paragraphDef = paragraphDef;
			this.m_textRunDef = textRunDef;
			this.m_multipleParagraphsAllowed = multipleParagraphsAllowed;
			this.m_errorContext = this.m_textRunDef.RenderingContext.ErrorContext;
		}

		// Token: 0x170010FC RID: 4348
		// (get) Token: 0x06001E20 RID: 7712 RVA: 0x00075934 File Offset: 0x00073B34
		public string UniqueName
		{
			get
			{
				if (this.m_uniqueName == null)
				{
					this.m_uniqueName = this.m_textRunDef.InstanceUniqueName + "x" + this.GenerateID().ToString(CultureInfo.InvariantCulture);
				}
				return this.m_uniqueName;
			}
		}

		// Token: 0x170010FD RID: 4349
		// (get) Token: 0x06001E21 RID: 7713 RVA: 0x0007597D File Offset: 0x00073B7D
		public CompiledParagraphInstanceCollection CompiledParagraphInstances
		{
			get
			{
				this.Parse();
				return this.m_compiledParagraphCollection;
			}
		}

		// Token: 0x170010FE RID: 4350
		// (get) Token: 0x06001E22 RID: 7714 RVA: 0x0007598B File Offset: 0x00073B8B
		internal TextRun TextRunDefinition
		{
			get
			{
				return this.m_textRunDef;
			}
		}

		// Token: 0x170010FF RID: 4351
		// (get) Token: 0x06001E23 RID: 7715 RVA: 0x00075993 File Offset: 0x00073B93
		internal Paragraph ParagraphDefinition
		{
			get
			{
				return this.m_paragraphDef;
			}
		}

		// Token: 0x17001100 RID: 4352
		// (get) Token: 0x06001E24 RID: 7716 RVA: 0x0007599B File Offset: 0x00073B9B
		public bool ParseErrorOccured
		{
			get
			{
				this.Parse();
				return this.m_parseErrorOccured;
			}
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x000759AC File Offset: 0x00073BAC
		private void Parse()
		{
			if (!this.m_parsed)
			{
				try
				{
					this.m_parsed = true;
					this.m_paragraphDef.CriGenerationPhase = ReportElement.CriGenerationPhases.Definition;
					this.m_textRunDef.CriGenerationPhase = ReportElement.CriGenerationPhases.Definition;
					ReportEnumProperty<MarkupType> markupType = this.m_textRunDef.MarkupType;
					MarkupType markupType2;
					if (markupType.IsExpression)
					{
						markupType2 = this.m_textRunDef.Instance.MarkupType;
					}
					else
					{
						markupType2 = markupType.Value;
					}
					if (markupType2 == MarkupType.HTML)
					{
						RichTextParser richTextParser = new HtmlParser(this.m_multipleParagraphsAllowed, this, this);
						InternalTextRunInstance internalTextRunInstance = (InternalTextRunInstance)this.m_textRunDef.Instance;
						Microsoft.ReportingServices.RdlExpressions.VariantResult originalValue = internalTextRunInstance.GetOriginalValue();
						if (!originalValue.ErrorOccurred && originalValue.TypeCode != TypeCode.Empty)
						{
							try
							{
								string text;
								if (originalValue.TypeCode == TypeCode.String)
								{
									text = originalValue.Value as string;
								}
								else
								{
									text = internalTextRunInstance.TextRunDef.FormatTextRunValue(originalValue.Value, originalValue.TypeCode, this.m_textRunDef.RenderingContext.OdpContext);
								}
								this.m_compiledParagraphCollection = (CompiledParagraphInstanceCollection)richTextParser.Parse(text);
								return;
							}
							catch (Exception ex)
							{
								this.m_errorContext.Register(ProcessingErrorCode.rsInvalidRichTextParseFailed, Severity.Warning, new string[]
								{
									"TextRun",
									internalTextRunInstance.TextRunDef.Name,
									ex.Message
								});
								this.m_parseErrorOccured = true;
								this.CreateSingleTextRun().Value = RPRes.rsRichTextParseErrorValue;
								return;
							}
						}
						ICompiledTextRunInstance compiledTextRunInstance = this.CreateSingleTextRun();
						if (originalValue.ErrorOccurred)
						{
							compiledTextRunInstance.Value = RPRes.rsExpressionErrorValue;
						}
					}
				}
				finally
				{
					this.m_textRunDef.CriGenerationPhase = ReportElement.CriGenerationPhases.None;
					this.m_paragraphDef.CriGenerationPhase = ReportElement.CriGenerationPhases.None;
				}
			}
		}

		// Token: 0x06001E26 RID: 7718 RVA: 0x00075B78 File Offset: 0x00073D78
		private ICompiledTextRunInstance CreateSingleTextRun()
		{
			ICompiledParagraphInstance compiledParagraphInstance = new CompiledParagraphInstance(this);
			ICompiledTextRunInstance compiledTextRunInstance = new CompiledTextRunInstance(this);
			CompiledRichTextStyleInstance compiledRichTextStyleInstance = new CompiledRichTextStyleInstance(this.m_textRunDef, this.m_textRunDef.ReportScope, this.m_textRunDef.RenderingContext);
			this.m_compiledParagraphCollection = new CompiledParagraphInstanceCollection(this);
			compiledParagraphInstance.CompiledTextRunInstances = new CompiledTextRunInstanceCollection(this);
			compiledTextRunInstance.Style = compiledRichTextStyleInstance;
			compiledParagraphInstance.Style = compiledRichTextStyleInstance;
			((ICollection<ICompiledParagraphInstance>)this.m_compiledParagraphCollection).Add(compiledParagraphInstance);
			compiledParagraphInstance.CompiledTextRunInstances.Add(compiledTextRunInstance);
			return compiledTextRunInstance;
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x00075BF4 File Offset: 0x00073DF4
		protected override void ResetInstanceCache()
		{
			this.m_compiledParagraphCollection = null;
			this.m_parseErrorOccured = false;
			this.m_parsed = false;
			this.m_uniqueName = null;
			this.m_objectCount = 0;
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x00075C1C File Offset: 0x00073E1C
		internal int GenerateID()
		{
			int objectCount = this.m_objectCount;
			this.m_objectCount = objectCount + 1;
			return objectCount;
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x00075C3A File Offset: 0x00073E3A
		IList<ICompiledParagraphInstance> IRichTextInstanceCreator.CreateParagraphInstanceCollection()
		{
			return new CompiledParagraphInstanceCollection(this);
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x00075C42 File Offset: 0x00073E42
		ICompiledParagraphInstance IRichTextInstanceCreator.CreateParagraphInstance()
		{
			return new CompiledParagraphInstance(this);
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x00075C4A File Offset: 0x00073E4A
		ICompiledTextRunInstance IRichTextInstanceCreator.CreateTextRunInstance()
		{
			return new CompiledTextRunInstance(this);
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x00075C52 File Offset: 0x00073E52
		IList<ICompiledTextRunInstance> IRichTextInstanceCreator.CreateTextRunInstanceCollection()
		{
			return new CompiledTextRunInstanceCollection(this);
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x00075C5C File Offset: 0x00073E5C
		ICompiledStyleInstance IRichTextInstanceCreator.CreateStyleInstance(bool isParagraphStyle)
		{
			if (isParagraphStyle)
			{
				return new CompiledRichTextStyleInstance(this.m_paragraphDef, this.m_paragraphDef.ReportScope, this.m_paragraphDef.RenderingContext);
			}
			return new CompiledRichTextStyleInstance(this.m_textRunDef, this.m_textRunDef.ReportScope, this.m_textRunDef.RenderingContext);
		}

		// Token: 0x06001E2E RID: 7726 RVA: 0x00075CB0 File Offset: 0x00073EB0
		IActionInstance IRichTextInstanceCreator.CreateActionInstance()
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.ActionItem actionItem = new Microsoft.ReportingServices.ReportIntermediateFormat.ActionItem();
			Microsoft.ReportingServices.ReportIntermediateFormat.Action action = new Microsoft.ReportingServices.ReportIntermediateFormat.Action();
			action.ActionItems.Add(actionItem);
			return new Microsoft.ReportingServices.OnDemandReportRendering.Action(new ActionInfo(this.m_textRunDef.RenderingContext, this.m_textRunDef.ReportScope, action, ((InternalTextRun)this.m_textRunDef).TextRunDef, this.m_textRunDef, ObjectType.TextRun, ((InternalTextRun)this.m_textRunDef).TextRunDef.Name, this.m_textRunDef), actionItem, 0).Instance;
		}

		// Token: 0x17001101 RID: 4353
		// (get) Token: 0x06001E2F RID: 7727 RVA: 0x00075D30 File Offset: 0x00073F30
		RSTrace IRichTextLogger.Tracer
		{
			get
			{
				return Global.Tracer;
			}
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x00075D38 File Offset: 0x00073F38
		void IRichTextLogger.RegisterOutOfRangeSizeWarning(string propertyName, string value, string minVal, string maxVal)
		{
			this.m_errorContext.Register(ProcessingErrorCode.rsParseErrorOutOfRangeSize, Severity.Warning, ObjectType.TextRun, ((InternalTextRun)this.m_textRunDef).TextRunDef.Name, propertyName, new string[] { value, minVal, maxVal });
		}

		// Token: 0x06001E31 RID: 7729 RVA: 0x00075D84 File Offset: 0x00073F84
		void IRichTextLogger.RegisterInvalidValueWarning(string propertyName, string value, int charPosition)
		{
			this.m_errorContext.Register(ProcessingErrorCode.rsParseErrorInvalidValue, Severity.Warning, ObjectType.TextRun, ((InternalTextRun)this.m_textRunDef).TextRunDef.Name, propertyName, new string[]
			{
				value,
				charPosition.ToString(CultureInfo.InvariantCulture)
			});
		}

		// Token: 0x06001E32 RID: 7730 RVA: 0x00075DD4 File Offset: 0x00073FD4
		void IRichTextLogger.RegisterInvalidColorWarning(string propertyName, string value, int charPosition)
		{
			this.m_errorContext.Register(ProcessingErrorCode.rsParseErrorInvalidColor, Severity.Warning, ObjectType.TextRun, ((InternalTextRun)this.m_textRunDef).TextRunDef.Name, propertyName, new string[]
			{
				value,
				charPosition.ToString(CultureInfo.InvariantCulture)
			});
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x00075E24 File Offset: 0x00074024
		void IRichTextLogger.RegisterInvalidSizeWarning(string propertyName, string value, int charPosition)
		{
			this.m_errorContext.Register(ProcessingErrorCode.rsParseErrorInvalidSize, Severity.Warning, ObjectType.TextRun, ((InternalTextRun)this.m_textRunDef).TextRunDef.Name, propertyName, new string[]
			{
				value,
				charPosition.ToString(CultureInfo.InvariantCulture)
			});
		}

		// Token: 0x04000F80 RID: 3968
		private bool m_multipleParagraphsAllowed;

		// Token: 0x04000F81 RID: 3969
		private TextRun m_textRunDef;

		// Token: 0x04000F82 RID: 3970
		private Paragraph m_paragraphDef;

		// Token: 0x04000F83 RID: 3971
		private CompiledParagraphInstanceCollection m_compiledParagraphCollection;

		// Token: 0x04000F84 RID: 3972
		private bool m_parseErrorOccured;

		// Token: 0x04000F85 RID: 3973
		private bool m_parsed;

		// Token: 0x04000F86 RID: 3974
		private string m_uniqueName;

		// Token: 0x04000F87 RID: 3975
		private int m_objectCount;

		// Token: 0x04000F88 RID: 3976
		private IErrorContext m_errorContext;
	}
}
