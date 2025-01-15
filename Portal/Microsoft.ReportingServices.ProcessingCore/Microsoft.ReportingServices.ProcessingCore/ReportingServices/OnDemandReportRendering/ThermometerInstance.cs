using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200012A RID: 298
	public sealed class ThermometerInstance : BaseInstance
	{
		// Token: 0x06000D15 RID: 3349 RVA: 0x000382B5 File Offset: 0x000364B5
		internal ThermometerInstance(Thermometer defObject)
			: base(defObject.GaugePanelDef)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06000D16 RID: 3350 RVA: 0x000382CA File Offset: 0x000364CA
		public StyleInstance Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new StyleInstance(this.m_defObject, this.m_defObject.GaugePanelDef, this.m_defObject.GaugePanelDef.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06000D17 RID: 3351 RVA: 0x00038308 File Offset: 0x00036508
		public double BulbOffset
		{
			get
			{
				if (this.m_bulbOffset == null)
				{
					this.m_bulbOffset = new double?(this.m_defObject.ThermometerDef.EvaluateBulbOffset(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_bulbOffset.Value;
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06000D18 RID: 3352 RVA: 0x00038364 File Offset: 0x00036564
		public double BulbSize
		{
			get
			{
				if (this.m_bulbSize == null)
				{
					this.m_bulbSize = new double?(this.m_defObject.ThermometerDef.EvaluateBulbSize(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_bulbSize.Value;
			}
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06000D19 RID: 3353 RVA: 0x000383C0 File Offset: 0x000365C0
		public GaugeThermometerStyles ThermometerStyle
		{
			get
			{
				if (this.m_thermometerStyle == null)
				{
					this.m_thermometerStyle = new GaugeThermometerStyles?(this.m_defObject.ThermometerDef.EvaluateThermometerStyle(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_thermometerStyle.Value;
			}
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x0003841B File Offset: 0x0003661B
		protected override void ResetInstanceCache()
		{
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			this.m_bulbOffset = null;
			this.m_bulbSize = null;
			this.m_thermometerStyle = null;
		}

		// Token: 0x040005E6 RID: 1510
		private Thermometer m_defObject;

		// Token: 0x040005E7 RID: 1511
		private StyleInstance m_style;

		// Token: 0x040005E8 RID: 1512
		private double? m_bulbOffset;

		// Token: 0x040005E9 RID: 1513
		private double? m_bulbSize;

		// Token: 0x040005EA RID: 1514
		private GaugeThermometerStyles? m_thermometerStyle;
	}
}
