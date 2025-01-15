using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000316 RID: 790
	internal sealed class InternalParagraph : Paragraph
	{
		// Token: 0x06001D47 RID: 7495 RVA: 0x00073A36 File Offset: 0x00071C36
		internal InternalParagraph(TextBox textBox, int indexIntoParentCollectionDef, Paragraph paragraph, RenderingContext renderingContext)
			: base(textBox, indexIntoParentCollectionDef, renderingContext)
		{
			this.m_paragraphDef = paragraph;
		}

		// Token: 0x17001066 RID: 4198
		// (get) Token: 0x06001D48 RID: 7496 RVA: 0x00073A49 File Offset: 0x00071C49
		internal override IStyleContainer StyleContainer
		{
			get
			{
				return this.m_paragraphDef;
			}
		}

		// Token: 0x17001067 RID: 4199
		// (get) Token: 0x06001D49 RID: 7497 RVA: 0x00073A51 File Offset: 0x00071C51
		public override string ID
		{
			get
			{
				return this.m_paragraphDef.RenderingModelID;
			}
		}

		// Token: 0x17001068 RID: 4200
		// (get) Token: 0x06001D4A RID: 7498 RVA: 0x00073A5E File Offset: 0x00071C5E
		public override ReportSizeProperty LeftIndent
		{
			get
			{
				if (this.m_leftIndent == null && this.m_paragraphDef.LeftIndent != null)
				{
					this.m_leftIndent = new ReportSizeProperty(this.m_paragraphDef.LeftIndent);
				}
				return this.m_leftIndent;
			}
		}

		// Token: 0x17001069 RID: 4201
		// (get) Token: 0x06001D4B RID: 7499 RVA: 0x00073A91 File Offset: 0x00071C91
		public override ReportSizeProperty RightIndent
		{
			get
			{
				if (this.m_rightIndent == null && this.m_paragraphDef.RightIndent != null)
				{
					this.m_rightIndent = new ReportSizeProperty(this.m_paragraphDef.RightIndent);
				}
				return this.m_rightIndent;
			}
		}

		// Token: 0x1700106A RID: 4202
		// (get) Token: 0x06001D4C RID: 7500 RVA: 0x00073AC4 File Offset: 0x00071CC4
		public override ReportSizeProperty HangingIndent
		{
			get
			{
				if (this.m_hangingIndent == null && this.m_paragraphDef.HangingIndent != null)
				{
					this.m_hangingIndent = new ReportSizeProperty(this.m_paragraphDef.HangingIndent, true);
				}
				return this.m_hangingIndent;
			}
		}

		// Token: 0x1700106B RID: 4203
		// (get) Token: 0x06001D4D RID: 7501 RVA: 0x00073AF8 File Offset: 0x00071CF8
		public override ReportEnumProperty<ListStyle> ListStyle
		{
			get
			{
				if (this.m_listStyle == null)
				{
					ExpressionInfo listStyle = this.m_paragraphDef.ListStyle;
					if (listStyle != null)
					{
						ListStyle listStyle2 = Microsoft.ReportingServices.OnDemandReportRendering.ListStyle.None;
						if (!listStyle.IsExpression)
						{
							listStyle2 = RichTextHelpers.TranslateListStyle(listStyle.StringValue);
						}
						this.m_listStyle = new ReportEnumProperty<ListStyle>(listStyle.IsExpression, listStyle.OriginalText, listStyle2);
					}
					else
					{
						this.m_listStyle = new ReportEnumProperty<ListStyle>(Microsoft.ReportingServices.OnDemandReportRendering.ListStyle.None);
					}
				}
				return this.m_listStyle;
			}
		}

		// Token: 0x1700106C RID: 4204
		// (get) Token: 0x06001D4E RID: 7502 RVA: 0x00073B60 File Offset: 0x00071D60
		public override ReportIntProperty ListLevel
		{
			get
			{
				if (this.m_listLevel == null)
				{
					if (this.m_paragraphDef.ListLevel != null)
					{
						this.m_listLevel = new ReportIntProperty(this.m_paragraphDef.ListLevel);
					}
					else
					{
						this.m_listLevel = new ReportIntProperty((this.Instance.ListStyle > Microsoft.ReportingServices.OnDemandReportRendering.ListStyle.None) ? 1 : 0);
					}
				}
				return this.m_listLevel;
			}
		}

		// Token: 0x1700106D RID: 4205
		// (get) Token: 0x06001D4F RID: 7503 RVA: 0x00073BB9 File Offset: 0x00071DB9
		public override ReportSizeProperty SpaceBefore
		{
			get
			{
				if (this.m_spaceBefore == null && this.m_paragraphDef.SpaceBefore != null)
				{
					this.m_spaceBefore = new ReportSizeProperty(this.m_paragraphDef.SpaceBefore);
				}
				return this.m_spaceBefore;
			}
		}

		// Token: 0x1700106E RID: 4206
		// (get) Token: 0x06001D50 RID: 7504 RVA: 0x00073BEC File Offset: 0x00071DEC
		public override ReportSizeProperty SpaceAfter
		{
			get
			{
				if (this.m_spaceAfter == null && this.m_paragraphDef.SpaceAfter != null)
				{
					this.m_spaceAfter = new ReportSizeProperty(this.m_paragraphDef.SpaceAfter);
				}
				return this.m_spaceAfter;
			}
		}

		// Token: 0x1700106F RID: 4207
		// (get) Token: 0x06001D51 RID: 7505 RVA: 0x00073C1F File Offset: 0x00071E1F
		internal Paragraph ParagraphDef
		{
			get
			{
				return this.m_paragraphDef;
			}
		}

		// Token: 0x17001070 RID: 4208
		// (get) Token: 0x06001D52 RID: 7506 RVA: 0x00073C27 File Offset: 0x00071E27
		public override ParagraphInstance Instance
		{
			get
			{
				if (base.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new InternalParagraphInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x04000F41 RID: 3905
		private ReportSizeProperty m_leftIndent;

		// Token: 0x04000F42 RID: 3906
		private ReportSizeProperty m_rightIndent;

		// Token: 0x04000F43 RID: 3907
		private ReportSizeProperty m_hangingIndent;

		// Token: 0x04000F44 RID: 3908
		private ReportSizeProperty m_spaceBefore;

		// Token: 0x04000F45 RID: 3909
		private ReportSizeProperty m_spaceAfter;

		// Token: 0x04000F46 RID: 3910
		private Paragraph m_paragraphDef;
	}
}
