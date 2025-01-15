using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001D0 RID: 464
	public sealed class MapColorRangeRuleInstance : MapColorRuleInstance
	{
		// Token: 0x060011FC RID: 4604 RVA: 0x0004A3D4 File Offset: 0x000485D4
		internal MapColorRangeRuleInstance(MapColorRangeRule defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x060011FD RID: 4605 RVA: 0x0004A3E4 File Offset: 0x000485E4
		public ReportColor StartColor
		{
			get
			{
				if (this.m_startColor == null)
				{
					this.m_startColor = new ReportColor(((MapColorRangeRule)this.m_defObject.MapColorRuleDef).EvaluateStartColor(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_startColor;
			}
		}

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x060011FE RID: 4606 RVA: 0x0004A43C File Offset: 0x0004863C
		public ReportColor MiddleColor
		{
			get
			{
				if (this.m_middleColor == null)
				{
					this.m_middleColor = new ReportColor(((MapColorRangeRule)this.m_defObject.MapColorRuleDef).EvaluateMiddleColor(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_middleColor;
			}
		}

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x060011FF RID: 4607 RVA: 0x0004A494 File Offset: 0x00048694
		public ReportColor EndColor
		{
			get
			{
				if (this.m_endColor == null)
				{
					this.m_endColor = new ReportColor(((MapColorRangeRule)this.m_defObject.MapColorRuleDef).EvaluateEndColor(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_endColor;
			}
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x0004A4EA File Offset: 0x000486EA
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_startColor = null;
			this.m_middleColor = null;
			this.m_endColor = null;
		}

		// Token: 0x0400088A RID: 2186
		private MapColorRangeRule m_defObject;

		// Token: 0x0400088B RID: 2187
		private ReportColor m_startColor;

		// Token: 0x0400088C RID: 2188
		private ReportColor m_middleColor;

		// Token: 0x0400088D RID: 2189
		private ReportColor m_endColor;
	}
}
