using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000198 RID: 408
	public sealed class MapColorScaleTitleInstance : BaseInstance
	{
		// Token: 0x0600108A RID: 4234 RVA: 0x000464C6 File Offset: 0x000446C6
		internal MapColorScaleTitleInstance(MapColorScaleTitle defObject)
			: base(defObject.MapDef.ReportScope)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x0600108B RID: 4235 RVA: 0x000464E0 File Offset: 0x000446E0
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

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x0600108C RID: 4236 RVA: 0x0004652C File Offset: 0x0004472C
		public string Caption
		{
			get
			{
				if (!this.m_captionEvaluated)
				{
					this.m_caption = this.m_defObject.MapColorScaleTitleDef.EvaluateCaption(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext);
					this.m_captionEvaluated = true;
				}
				return this.m_caption;
			}
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x0004657F File Offset: 0x0004477F
		protected override void ResetInstanceCache()
		{
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			this.m_caption = null;
			this.m_captionEvaluated = false;
		}

		// Token: 0x040007C8 RID: 1992
		private MapColorScaleTitle m_defObject;

		// Token: 0x040007C9 RID: 1993
		private StyleInstance m_style;

		// Token: 0x040007CA RID: 1994
		private string m_caption;

		// Token: 0x040007CB RID: 1995
		private bool m_captionEvaluated;
	}
}
