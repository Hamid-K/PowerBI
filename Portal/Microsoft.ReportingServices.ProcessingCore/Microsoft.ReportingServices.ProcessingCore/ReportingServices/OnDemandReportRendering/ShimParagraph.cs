using System;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000317 RID: 791
	internal sealed class ShimParagraph : Paragraph
	{
		// Token: 0x06001D53 RID: 7507 RVA: 0x00073C52 File Offset: 0x00071E52
		internal ShimParagraph(TextBox textBox, RenderingContext renderingContext)
			: base(textBox, renderingContext)
		{
		}

		// Token: 0x17001071 RID: 4209
		// (get) Token: 0x06001D54 RID: 7508 RVA: 0x00073C5C File Offset: 0x00071E5C
		public override string ID
		{
			get
			{
				return base.TextBox.ID + "xK";
			}
		}

		// Token: 0x17001072 RID: 4210
		// (get) Token: 0x06001D55 RID: 7509 RVA: 0x00073C73 File Offset: 0x00071E73
		public override Style Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new ParagraphFilteredStyle(this.RenderReportItem, base.RenderingContext, this.UseRenderStyle);
				}
				return this.m_style;
			}
		}

		// Token: 0x17001073 RID: 4211
		// (get) Token: 0x06001D56 RID: 7510 RVA: 0x00073CA0 File Offset: 0x00071EA0
		public override ReportEnumProperty<ListStyle> ListStyle
		{
			get
			{
				if (this.m_listStyle == null)
				{
					this.m_listStyle = new ReportEnumProperty<ListStyle>(Microsoft.ReportingServices.OnDemandReportRendering.ListStyle.None);
				}
				return this.m_listStyle;
			}
		}

		// Token: 0x17001074 RID: 4212
		// (get) Token: 0x06001D57 RID: 7511 RVA: 0x00073CBC File Offset: 0x00071EBC
		public override ReportIntProperty ListLevel
		{
			get
			{
				if (this.m_listLevel == null)
				{
					this.m_listLevel = new ReportIntProperty(0);
				}
				return this.m_listLevel;
			}
		}

		// Token: 0x17001075 RID: 4213
		// (get) Token: 0x06001D58 RID: 7512 RVA: 0x00073CD8 File Offset: 0x00071ED8
		public override ParagraphInstance Instance
		{
			get
			{
				if (this.m_instance == null)
				{
					this.m_instance = new ShimParagraphInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x06001D59 RID: 7513 RVA: 0x00073CF4 File Offset: 0x00071EF4
		internal override void UpdateRenderReportItem(ReportItem renderReportItem)
		{
			base.UpdateRenderReportItem(renderReportItem);
			if (this.m_style != null)
			{
				this.m_style.UpdateStyleCache(renderReportItem);
			}
		}
	}
}
