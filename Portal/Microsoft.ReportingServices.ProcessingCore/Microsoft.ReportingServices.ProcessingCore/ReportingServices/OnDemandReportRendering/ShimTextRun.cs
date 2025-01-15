using System;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200031B RID: 795
	internal sealed class ShimTextRun : TextRun
	{
		// Token: 0x06001D85 RID: 7557 RVA: 0x000742CC File Offset: 0x000724CC
		internal ShimTextRun(Paragraph paragraph, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
			: base(paragraph, renderingContext)
		{
		}

		// Token: 0x17001096 RID: 4246
		// (get) Token: 0x06001D86 RID: 7558 RVA: 0x000742D6 File Offset: 0x000724D6
		public override string ID
		{
			get
			{
				return base.TextBox.ID + "xL";
			}
		}

		// Token: 0x17001097 RID: 4247
		// (get) Token: 0x06001D87 RID: 7559 RVA: 0x000742ED File Offset: 0x000724ED
		public override Microsoft.ReportingServices.OnDemandReportRendering.Style Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new TextRunFilteredStyle(this.RenderReportItem, base.RenderingContext, this.UseRenderStyle);
				}
				return this.m_style;
			}
		}

		// Token: 0x17001098 RID: 4248
		// (get) Token: 0x06001D88 RID: 7560 RVA: 0x0007431C File Offset: 0x0007251C
		public override ReportStringProperty Value
		{
			get
			{
				if (this.m_value == null)
				{
					Microsoft.ReportingServices.ReportProcessing.TextBox textBox = (Microsoft.ReportingServices.ReportProcessing.TextBox)this.RenderReportItem.ReportItemDef;
					this.m_value = new ReportStringProperty(textBox.Value, textBox.Formula);
				}
				return this.m_value;
			}
		}

		// Token: 0x17001099 RID: 4249
		// (get) Token: 0x06001D89 RID: 7561 RVA: 0x0007435F File Offset: 0x0007255F
		public override TypeCode SharedTypeCode
		{
			get
			{
				return ((Microsoft.ReportingServices.ReportRendering.TextBox)base.TextBox.RenderReportItem).SharedTypeCode;
			}
		}

		// Token: 0x1700109A RID: 4250
		// (get) Token: 0x06001D8A RID: 7562 RVA: 0x00074376 File Offset: 0x00072576
		public override ReportEnumProperty<MarkupType> MarkupType
		{
			get
			{
				if (this.m_markupType == null)
				{
					this.m_markupType = new ReportEnumProperty<MarkupType>(Microsoft.ReportingServices.OnDemandReportRendering.MarkupType.None);
				}
				return this.m_markupType;
			}
		}

		// Token: 0x1700109B RID: 4251
		// (get) Token: 0x06001D8B RID: 7563 RVA: 0x00074394 File Offset: 0x00072594
		public override bool FormattedValueExpressionBased
		{
			get
			{
				if (this.m_formattedValueExpressionBased == null)
				{
					Microsoft.ReportingServices.ReportProcessing.TextBox textBox = (Microsoft.ReportingServices.ReportProcessing.TextBox)this.RenderReportItem.ReportItemDef;
					if (textBox.Value != null)
					{
						this.m_formattedValueExpressionBased = new bool?(textBox.Value.IsExpression);
					}
					else
					{
						this.m_formattedValueExpressionBased = new bool?(false);
					}
				}
				return this.m_formattedValueExpressionBased.Value;
			}
		}

		// Token: 0x1700109C RID: 4252
		// (get) Token: 0x06001D8C RID: 7564 RVA: 0x000743F6 File Offset: 0x000725F6
		public override TextRunInstance Instance
		{
			get
			{
				if (this.m_instance == null)
				{
					this.m_instance = new ShimTextRunInstance(this, (TextBoxInstance)base.TextBox.Instance);
				}
				return this.m_instance;
			}
		}

		// Token: 0x06001D8D RID: 7565 RVA: 0x00074422 File Offset: 0x00072622
		internal override void UpdateRenderReportItem(Microsoft.ReportingServices.ReportRendering.ReportItem renderReportItem)
		{
			if (this.m_style != null)
			{
				this.m_style.UpdateStyleCache(renderReportItem);
			}
		}
	}
}
