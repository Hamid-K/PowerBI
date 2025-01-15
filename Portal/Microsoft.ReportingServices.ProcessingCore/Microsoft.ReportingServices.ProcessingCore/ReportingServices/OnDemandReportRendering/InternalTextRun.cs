using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200031A RID: 794
	internal sealed class InternalTextRun : TextRun, IROMActionOwner
	{
		// Token: 0x06001D73 RID: 7539 RVA: 0x00073F55 File Offset: 0x00072155
		internal InternalTextRun(Paragraph paragraph, int indexIntoParentCollectionDef, TextRun textRun, RenderingContext renderingContext)
			: base(paragraph, indexIntoParentCollectionDef, renderingContext)
		{
			this.m_textRunDef = textRun;
		}

		// Token: 0x17001088 RID: 4232
		// (get) Token: 0x06001D74 RID: 7540 RVA: 0x00073F68 File Offset: 0x00072168
		internal override IStyleContainer StyleContainer
		{
			get
			{
				return this.m_textRunDef;
			}
		}

		// Token: 0x17001089 RID: 4233
		// (get) Token: 0x06001D75 RID: 7541 RVA: 0x00073F70 File Offset: 0x00072170
		public override string ID
		{
			get
			{
				return this.m_textRunDef.RenderingModelID;
			}
		}

		// Token: 0x1700108A RID: 4234
		// (get) Token: 0x06001D76 RID: 7542 RVA: 0x00073F7D File Offset: 0x0007217D
		public override string Label
		{
			get
			{
				return this.m_textRunDef.Label;
			}
		}

		// Token: 0x1700108B RID: 4235
		// (get) Token: 0x06001D77 RID: 7543 RVA: 0x00073F8A File Offset: 0x0007218A
		public override ReportStringProperty Value
		{
			get
			{
				if (this.m_value == null)
				{
					this.m_value = new ReportStringProperty(this.m_textRunDef.Value);
				}
				return this.m_value;
			}
		}

		// Token: 0x1700108C RID: 4236
		// (get) Token: 0x06001D78 RID: 7544 RVA: 0x00073FB0 File Offset: 0x000721B0
		string IROMActionOwner.UniqueName
		{
			get
			{
				return this.TextRunDef.UniqueName;
			}
		}

		// Token: 0x1700108D RID: 4237
		// (get) Token: 0x06001D79 RID: 7545 RVA: 0x00073FC0 File Offset: 0x000721C0
		public override ActionInfo ActionInfo
		{
			get
			{
				if (this.m_actionInfo == null && this.m_textRunDef.Action != null)
				{
					this.m_actionInfo = new ActionInfo(base.RenderingContext, this.ReportScope, this.m_textRunDef.Action, this.TextRunDef, this, this.m_textRunDef.ObjectType, this.m_textRunDef.Name, this);
				}
				return this.m_actionInfo;
			}
		}

		// Token: 0x1700108E RID: 4238
		// (get) Token: 0x06001D7A RID: 7546 RVA: 0x00074028 File Offset: 0x00072228
		public override ReportStringProperty ToolTip
		{
			get
			{
				if (this.m_toolTip == null && this.m_textRunDef.ToolTip != null)
				{
					this.m_toolTip = new ReportStringProperty(this.m_textRunDef.ToolTip);
				}
				return this.m_toolTip;
			}
		}

		// Token: 0x1700108F RID: 4239
		// (get) Token: 0x06001D7B RID: 7547 RVA: 0x0007405C File Offset: 0x0007225C
		public override ReportEnumProperty<MarkupType> MarkupType
		{
			get
			{
				if (this.m_markupType == null)
				{
					ExpressionInfo markupType = this.m_textRunDef.MarkupType;
					if (markupType != null)
					{
						MarkupType markupType2 = Microsoft.ReportingServices.OnDemandReportRendering.MarkupType.None;
						if (!markupType.IsExpression)
						{
							markupType2 = RichTextHelpers.TranslateMarkupType(markupType.StringValue);
						}
						this.m_markupType = new ReportEnumProperty<MarkupType>(markupType.IsExpression, markupType.OriginalText, markupType2);
					}
					else
					{
						this.m_markupType = new ReportEnumProperty<MarkupType>(Microsoft.ReportingServices.OnDemandReportRendering.MarkupType.None);
					}
				}
				return this.m_markupType;
			}
		}

		// Token: 0x17001090 RID: 4240
		// (get) Token: 0x06001D7C RID: 7548 RVA: 0x000740C2 File Offset: 0x000722C2
		public override TypeCode SharedTypeCode
		{
			get
			{
				return this.m_textRunDef.ValueTypeCode;
			}
		}

		// Token: 0x17001091 RID: 4241
		// (get) Token: 0x06001D7D RID: 7549 RVA: 0x000740CF File Offset: 0x000722CF
		internal TextRun TextRunDef
		{
			get
			{
				return this.m_textRunDef;
			}
		}

		// Token: 0x17001092 RID: 4242
		// (get) Token: 0x06001D7E RID: 7550 RVA: 0x000740D8 File Offset: 0x000722D8
		public override bool FormattedValueExpressionBased
		{
			get
			{
				if (this.m_formattedValueExpressionBased == null)
				{
					if (!this.MarkupType.IsExpression && this.MarkupType.Value == Microsoft.ReportingServices.OnDemandReportRendering.MarkupType.None)
					{
						if (this.m_textRunDef.Value != null && this.m_textRunDef.ValueTypeCode != TypeCode.String)
						{
							Style styleClass = this.m_textRunDef.StyleClass;
							this.m_formattedValueExpressionBased = new bool?(this.m_textRunDef.Value.IsExpression || (styleClass != null && (this.StyleAttributeExpressionBased(styleClass, "Language") || this.StyleAttributeExpressionBased(styleClass, "Format") || this.StyleAttributeExpressionBased(styleClass, "Calendar") || this.StyleAttributeExpressionBased(styleClass, "NumeralLanguage") || this.StyleAttributeExpressionBased(styleClass, "NumeralVariant"))));
						}
						else
						{
							this.m_formattedValueExpressionBased = new bool?(false);
						}
					}
					else
					{
						this.m_formattedValueExpressionBased = new bool?(true);
					}
				}
				return this.m_formattedValueExpressionBased.Value;
			}
		}

		// Token: 0x17001093 RID: 4243
		// (get) Token: 0x06001D7F RID: 7551 RVA: 0x000741D4 File Offset: 0x000723D4
		public override TextRunInstance Instance
		{
			get
			{
				if (base.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new InternalTextRunInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x17001094 RID: 4244
		// (get) Token: 0x06001D80 RID: 7552 RVA: 0x000741FF File Offset: 0x000723FF
		internal override List<string> FieldsUsedInValueExpression
		{
			get
			{
				if (base.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				return ((InternalTextRunInstance)this.Instance).GetFieldsUsedInValueExpression();
			}
		}

		// Token: 0x17001095 RID: 4245
		// (get) Token: 0x06001D81 RID: 7553 RVA: 0x00074220 File Offset: 0x00072420
		public override CompiledRichTextInstance CompiledInstance
		{
			get
			{
				if (this.Instance.MarkupType == Microsoft.ReportingServices.OnDemandReportRendering.MarkupType.None)
				{
					return null;
				}
				if (this.m_compiledRichTextInstance == null)
				{
					this.m_compiledRichTextInstance = new CompiledRichTextInstance(this.ReportScope, this, this.m_paragraph, this.m_paragraph.TextRuns.Count == 1);
				}
				return this.m_compiledRichTextInstance;
			}
		}

		// Token: 0x06001D82 RID: 7554 RVA: 0x00074275 File Offset: 0x00072475
		internal override void SetNewContext()
		{
			base.SetNewContext();
		}

		// Token: 0x06001D83 RID: 7555 RVA: 0x0007427D File Offset: 0x0007247D
		internal override void SetNewContextChildren()
		{
			base.SetNewContextChildren();
			if (this.m_compiledRichTextInstance != null)
			{
				this.m_compiledRichTextInstance.SetNewContext();
			}
			if (this.m_actionInfo != null)
			{
				this.m_actionInfo.SetNewContext();
			}
		}

		// Token: 0x06001D84 RID: 7556 RVA: 0x000742AC File Offset: 0x000724AC
		private bool StyleAttributeExpressionBased(Style style, string styleName)
		{
			AttributeInfo attributeInfo;
			return style.GetAttributeInfo(styleName, out attributeInfo) && attributeInfo.IsExpression;
		}

		// Token: 0x04000F50 RID: 3920
		private ReportStringProperty m_toolTip;

		// Token: 0x04000F51 RID: 3921
		private ActionInfo m_actionInfo;

		// Token: 0x04000F52 RID: 3922
		private CompiledRichTextInstance m_compiledRichTextInstance;

		// Token: 0x04000F53 RID: 3923
		private TextRun m_textRunDef;
	}
}
