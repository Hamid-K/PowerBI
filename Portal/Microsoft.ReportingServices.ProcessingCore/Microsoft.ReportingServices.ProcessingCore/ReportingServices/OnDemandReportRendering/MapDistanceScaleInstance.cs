using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000199 RID: 409
	public sealed class MapDistanceScaleInstance : MapDockableSubItemInstance
	{
		// Token: 0x0600108E RID: 4238 RVA: 0x000465A2 File Offset: 0x000447A2
		internal MapDistanceScaleInstance(MapDistanceScale defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x0600108F RID: 4239 RVA: 0x000465B4 File Offset: 0x000447B4
		public ReportColor ScaleColor
		{
			get
			{
				if (this.m_scaleColor == null)
				{
					this.m_scaleColor = new ReportColor(((MapDistanceScale)this.m_defObject.MapDockableSubItemDef).EvaluateScaleColor(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_scaleColor;
			}
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06001090 RID: 4240 RVA: 0x0004660C File Offset: 0x0004480C
		public ReportColor ScaleBorderColor
		{
			get
			{
				if (this.m_scaleBorderColor == null)
				{
					this.m_scaleBorderColor = new ReportColor(((MapDistanceScale)this.m_defObject.MapDockableSubItemDef).EvaluateScaleBorderColor(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_scaleBorderColor;
			}
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x00046662 File Offset: 0x00044862
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_scaleColor = null;
			this.m_scaleBorderColor = null;
		}

		// Token: 0x040007CC RID: 1996
		private MapDistanceScale m_defObject;

		// Token: 0x040007CD RID: 1997
		private ReportColor m_scaleColor;

		// Token: 0x040007CE RID: 1998
		private ReportColor m_scaleBorderColor;
	}
}
