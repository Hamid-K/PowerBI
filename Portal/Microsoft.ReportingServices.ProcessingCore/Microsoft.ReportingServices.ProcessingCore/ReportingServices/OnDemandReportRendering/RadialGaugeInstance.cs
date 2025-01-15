using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000104 RID: 260
	public sealed class RadialGaugeInstance : GaugeInstance
	{
		// Token: 0x06000B80 RID: 2944 RVA: 0x00032EDC File Offset: 0x000310DC
		internal RadialGaugeInstance(RadialGauge defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06000B81 RID: 2945 RVA: 0x00032EEC File Offset: 0x000310EC
		public double PivotX
		{
			get
			{
				if (this.m_pivotX == null)
				{
					this.m_pivotX = new double?(((RadialGauge)this.m_defObject.GaugeDef).EvaluatePivotX(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_pivotX.Value;
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x00032F50 File Offset: 0x00031150
		public double PivotY
		{
			get
			{
				if (this.m_pivotY == null)
				{
					this.m_pivotY = new double?(((RadialGauge)this.m_defObject.GaugeDef).EvaluatePivotY(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_pivotY.Value;
			}
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x00032FB1 File Offset: 0x000311B1
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_pivotX = null;
			this.m_pivotY = null;
		}

		// Token: 0x040004E6 RID: 1254
		private RadialGauge m_defObject;

		// Token: 0x040004E7 RID: 1255
		private double? m_pivotX;

		// Token: 0x040004E8 RID: 1256
		private double? m_pivotY;
	}
}
