using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000123 RID: 291
	public sealed class RadialScale : GaugeScale
	{
		// Token: 0x06000CCF RID: 3279 RVA: 0x0003712A File Offset: 0x0003532A
		internal RadialScale(RadialScale defObject, GaugePanel gaugePanel)
			: base(defObject, gaugePanel)
		{
			this.m_defObject = defObject;
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x00037142 File Offset: 0x00035342
		public RadialPointerCollection GaugePointers
		{
			get
			{
				if (this.m_gaugePointers == null && this.RadialScaleDef.GaugePointers != null)
				{
					this.m_gaugePointers = new RadialPointerCollection(this, this.m_gaugePanel);
				}
				return this.m_gaugePointers;
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x00037171 File Offset: 0x00035371
		public ReportDoubleProperty Radius
		{
			get
			{
				if (this.m_radius == null && this.RadialScaleDef.Radius != null)
				{
					this.m_radius = new ReportDoubleProperty(this.RadialScaleDef.Radius);
				}
				return this.m_radius;
			}
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06000CD2 RID: 3282 RVA: 0x000371A4 File Offset: 0x000353A4
		public ReportDoubleProperty StartAngle
		{
			get
			{
				if (this.m_startAngle == null && this.RadialScaleDef.StartAngle != null)
				{
					this.m_startAngle = new ReportDoubleProperty(this.RadialScaleDef.StartAngle);
				}
				return this.m_startAngle;
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x000371D7 File Offset: 0x000353D7
		public ReportDoubleProperty SweepAngle
		{
			get
			{
				if (this.m_sweepAngle == null && this.RadialScaleDef.SweepAngle != null)
				{
					this.m_sweepAngle = new ReportDoubleProperty(this.RadialScaleDef.SweepAngle);
				}
				return this.m_sweepAngle;
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x0003720A File Offset: 0x0003540A
		internal RadialScale RadialScaleDef
		{
			get
			{
				return (RadialScale)this.m_defObject;
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x00037217 File Offset: 0x00035417
		public new RadialScaleInstance Instance
		{
			get
			{
				return (RadialScaleInstance)this.GetInstance();
			}
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x00037224 File Offset: 0x00035424
		internal override GaugeScaleInstance GetInstance()
		{
			if (this.m_gaugePanel.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (this.m_instance == null)
			{
				this.m_instance = new RadialScaleInstance(this);
			}
			return (GaugeScaleInstance)this.m_instance;
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x00037259 File Offset: 0x00035459
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_gaugePointers != null)
			{
				this.m_gaugePointers.SetNewContext();
			}
		}

		// Token: 0x040005AD RID: 1453
		private RadialPointerCollection m_gaugePointers;

		// Token: 0x040005AE RID: 1454
		private ReportDoubleProperty m_radius;

		// Token: 0x040005AF RID: 1455
		private ReportDoubleProperty m_startAngle;

		// Token: 0x040005B0 RID: 1456
		private ReportDoubleProperty m_sweepAngle;
	}
}
