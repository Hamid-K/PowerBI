using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000101 RID: 257
	public sealed class RadialGauge : Gauge
	{
		// Token: 0x06000B71 RID: 2929 RVA: 0x00032C3D File Offset: 0x00030E3D
		internal RadialGauge(RadialGauge defObject, GaugePanel gaugePanel)
			: base(defObject, gaugePanel)
		{
			this.m_defObject = defObject;
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x00032C55 File Offset: 0x00030E55
		public RadialScaleCollection GaugeScales
		{
			get
			{
				if (this.m_gaugeScales == null && this.RadialGaugeDef.GaugeScales != null)
				{
					this.m_gaugeScales = new RadialScaleCollection(this, this.m_gaugePanel);
				}
				return this.m_gaugeScales;
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x00032C84 File Offset: 0x00030E84
		public ReportDoubleProperty PivotX
		{
			get
			{
				if (this.m_pivotX == null && this.RadialGaugeDef.PivotX != null)
				{
					this.m_pivotX = new ReportDoubleProperty(this.RadialGaugeDef.PivotX);
				}
				return this.m_pivotX;
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06000B74 RID: 2932 RVA: 0x00032CB7 File Offset: 0x00030EB7
		public ReportDoubleProperty PivotY
		{
			get
			{
				if (this.m_pivotY == null && this.RadialGaugeDef.PivotY != null)
				{
					this.m_pivotY = new ReportDoubleProperty(this.RadialGaugeDef.PivotY);
				}
				return this.m_pivotY;
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x00032CEA File Offset: 0x00030EEA
		internal RadialGauge RadialGaugeDef
		{
			get
			{
				return (RadialGauge)this.m_defObject;
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x00032CF7 File Offset: 0x00030EF7
		public new RadialGaugeInstance Instance
		{
			get
			{
				return (RadialGaugeInstance)this.GetInstance();
			}
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x00032D04 File Offset: 0x00030F04
		internal override BaseInstance GetInstance()
		{
			if (this.m_gaugePanel.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (this.m_instance == null)
			{
				this.m_instance = new RadialGaugeInstance(this);
			}
			return this.m_instance;
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x00032D34 File Offset: 0x00030F34
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_gaugeScales != null)
			{
				this.m_gaugeScales.SetNewContext();
			}
		}

		// Token: 0x040004DE RID: 1246
		private RadialScaleCollection m_gaugeScales;

		// Token: 0x040004DF RID: 1247
		private ReportDoubleProperty m_pivotX;

		// Token: 0x040004E0 RID: 1248
		private ReportDoubleProperty m_pivotY;
	}
}
