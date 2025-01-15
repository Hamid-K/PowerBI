using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200012D RID: 301
	public sealed class ScalePin : TickMarkStyle
	{
		// Token: 0x06000D32 RID: 3378 RVA: 0x0003884A File Offset: 0x00036A4A
		internal ScalePin(ScalePin defObject, GaugePanel gaugePanel)
			: base(defObject, gaugePanel)
		{
			this.m_defObject = defObject;
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06000D33 RID: 3379 RVA: 0x00038862 File Offset: 0x00036A62
		public ReportDoubleProperty Location
		{
			get
			{
				if (this.m_location == null && this.ScalePinDef.Location != null)
				{
					this.m_location = new ReportDoubleProperty(this.ScalePinDef.Location);
				}
				return this.m_location;
			}
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06000D34 RID: 3380 RVA: 0x00038895 File Offset: 0x00036A95
		public ReportBoolProperty Enable
		{
			get
			{
				if (this.m_enable == null && this.ScalePinDef.Enable != null)
				{
					this.m_enable = new ReportBoolProperty(this.ScalePinDef.Enable);
				}
				return this.m_enable;
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06000D35 RID: 3381 RVA: 0x000388C8 File Offset: 0x00036AC8
		public PinLabel PinLabel
		{
			get
			{
				if (this.m_pinLabel == null && this.ScalePinDef.PinLabel != null)
				{
					this.m_pinLabel = new PinLabel(this.ScalePinDef.PinLabel, this.m_gaugePanel);
				}
				return this.m_pinLabel;
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x00038901 File Offset: 0x00036B01
		internal ScalePin ScalePinDef
		{
			get
			{
				return (ScalePin)this.m_defObject;
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06000D37 RID: 3383 RVA: 0x0003890E File Offset: 0x00036B0E
		public new ScalePinInstance Instance
		{
			get
			{
				if (this.m_gaugePanel.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = this.GetInstance();
				}
				return (ScalePinInstance)this.m_instance;
			}
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x00038943 File Offset: 0x00036B43
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_pinLabel != null)
			{
				this.m_pinLabel.SetNewContext();
			}
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x00038971 File Offset: 0x00036B71
		protected override TickMarkStyleInstance GetInstance()
		{
			return new ScalePinInstance(this);
		}

		// Token: 0x040005FA RID: 1530
		private ReportDoubleProperty m_location;

		// Token: 0x040005FB RID: 1531
		private ReportBoolProperty m_enable;

		// Token: 0x040005FC RID: 1532
		private PinLabel m_pinLabel;
	}
}
