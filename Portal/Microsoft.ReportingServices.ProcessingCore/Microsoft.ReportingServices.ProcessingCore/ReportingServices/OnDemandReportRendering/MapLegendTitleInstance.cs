using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200019C RID: 412
	public sealed class MapLegendTitleInstance : BaseInstance
	{
		// Token: 0x060010A0 RID: 4256 RVA: 0x00046AC1 File Offset: 0x00044CC1
		internal MapLegendTitleInstance(MapLegendTitle defObject)
			: base(defObject.MapDef.ReportScope)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x060010A1 RID: 4257 RVA: 0x00046ADC File Offset: 0x00044CDC
		public StyleInstance Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new StyleInstance(this.m_defObject, this.m_defObject.MapDef.ReportScope, this.m_defObject.MapDef.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x060010A2 RID: 4258 RVA: 0x00046B28 File Offset: 0x00044D28
		public string Caption
		{
			get
			{
				if (!this.m_captionEvaluated)
				{
					this.m_caption = this.m_defObject.MapLegendTitleDef.EvaluateCaption(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext);
					this.m_captionEvaluated = true;
				}
				return this.m_caption;
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x060010A3 RID: 4259 RVA: 0x00046B7C File Offset: 0x00044D7C
		public MapLegendTitleSeparator TitleSeparator
		{
			get
			{
				if (this.m_titleSeparator == null)
				{
					this.m_titleSeparator = new MapLegendTitleSeparator?(this.m_defObject.MapLegendTitleDef.EvaluateTitleSeparator(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_titleSeparator.Value;
			}
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x060010A4 RID: 4260 RVA: 0x00046BD8 File Offset: 0x00044DD8
		public ReportColor TitleSeparatorColor
		{
			get
			{
				if (this.m_titleSeparatorColor == null)
				{
					this.m_titleSeparatorColor = new ReportColor(this.m_defObject.MapLegendTitleDef.EvaluateTitleSeparatorColor(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_titleSeparatorColor;
			}
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x00046C29 File Offset: 0x00044E29
		protected override void ResetInstanceCache()
		{
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			this.m_caption = null;
			this.m_captionEvaluated = false;
			this.m_titleSeparator = null;
			this.m_titleSeparatorColor = null;
		}

		// Token: 0x040007DC RID: 2012
		private MapLegendTitle m_defObject;

		// Token: 0x040007DD RID: 2013
		private StyleInstance m_style;

		// Token: 0x040007DE RID: 2014
		private string m_caption;

		// Token: 0x040007DF RID: 2015
		private bool m_captionEvaluated;

		// Token: 0x040007E0 RID: 2016
		private MapLegendTitleSeparator? m_titleSeparator;

		// Token: 0x040007E1 RID: 2017
		private ReportColor m_titleSeparatorColor;
	}
}
