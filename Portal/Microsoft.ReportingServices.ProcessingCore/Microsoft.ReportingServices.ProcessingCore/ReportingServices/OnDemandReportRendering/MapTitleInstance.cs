using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200019A RID: 410
	public sealed class MapTitleInstance : MapDockableSubItemInstance
	{
		// Token: 0x06001092 RID: 4242 RVA: 0x00046678 File Offset: 0x00044878
		internal MapTitleInstance(MapTitle defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06001093 RID: 4243 RVA: 0x00046688 File Offset: 0x00044888
		public string Text
		{
			get
			{
				if (!this.m_textEvaluated)
				{
					this.m_text = ((MapTitle)this.m_defObject.MapDockableSubItemDef).EvaluateText(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext);
					this.m_textEvaluated = true;
				}
				return this.m_text;
			}
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06001094 RID: 4244 RVA: 0x000466E0 File Offset: 0x000448E0
		public double Angle
		{
			get
			{
				if (this.m_angle == null)
				{
					this.m_angle = new double?(((MapTitle)this.m_defObject.MapDockableSubItemDef).EvaluateAngle(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_angle.Value;
			}
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06001095 RID: 4245 RVA: 0x00046744 File Offset: 0x00044944
		public ReportSize TextShadowOffset
		{
			get
			{
				if (this.m_textShadowOffset == null)
				{
					this.m_textShadowOffset = new ReportSize(((MapTitle)this.m_defObject.MapDockableSubItemDef).EvaluateTextShadowOffset(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_textShadowOffset;
			}
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x0004679A File Offset: 0x0004499A
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_text = null;
			this.m_textEvaluated = false;
			this.m_angle = null;
			this.m_textShadowOffset = null;
		}

		// Token: 0x040007CF RID: 1999
		private MapTitle m_defObject;

		// Token: 0x040007D0 RID: 2000
		private string m_text;

		// Token: 0x040007D1 RID: 2001
		private bool m_textEvaluated;

		// Token: 0x040007D2 RID: 2002
		private double? m_angle;

		// Token: 0x040007D3 RID: 2003
		private ReportSize m_textShadowOffset;
	}
}
