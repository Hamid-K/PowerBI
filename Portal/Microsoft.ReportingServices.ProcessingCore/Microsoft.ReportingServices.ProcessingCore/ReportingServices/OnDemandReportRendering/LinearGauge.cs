using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000100 RID: 256
	public sealed class LinearGauge : Gauge
	{
		// Token: 0x06000B6A RID: 2922 RVA: 0x00032B14 File Offset: 0x00030D14
		internal LinearGauge(LinearGauge defObject, GaugePanel gaugePanel)
			: base(defObject, gaugePanel)
		{
			this.m_defObject = defObject;
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06000B6B RID: 2923 RVA: 0x00032B2C File Offset: 0x00030D2C
		public LinearScaleCollection GaugeScales
		{
			get
			{
				if (this.m_gaugeScales == null && this.LinearGaugeDef.GaugeScales != null)
				{
					this.m_gaugeScales = new LinearScaleCollection(this, this.m_gaugePanel);
				}
				return this.m_gaugeScales;
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x00032B5C File Offset: 0x00030D5C
		public ReportEnumProperty<GaugeOrientations> Orientation
		{
			get
			{
				if (this.m_orientation == null && this.LinearGaugeDef.Orientation != null)
				{
					this.m_orientation = new ReportEnumProperty<GaugeOrientations>(this.LinearGaugeDef.Orientation.IsExpression, this.LinearGaugeDef.Orientation.OriginalText, EnumTranslator.TranslateGaugeOrientations(this.LinearGaugeDef.Orientation.StringValue, null));
				}
				return this.m_orientation;
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06000B6D RID: 2925 RVA: 0x00032BC5 File Offset: 0x00030DC5
		internal LinearGauge LinearGaugeDef
		{
			get
			{
				return (LinearGauge)this.m_defObject;
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x00032BD2 File Offset: 0x00030DD2
		public new LinearGaugeInstance Instance
		{
			get
			{
				return (LinearGaugeInstance)this.GetInstance();
			}
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x00032BDF File Offset: 0x00030DDF
		internal override BaseInstance GetInstance()
		{
			if (this.m_gaugePanel.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (this.m_instance == null)
			{
				this.m_instance = new LinearGaugeInstance(this);
			}
			return this.m_instance;
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x00032C0F File Offset: 0x00030E0F
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

		// Token: 0x040004DC RID: 1244
		private LinearScaleCollection m_gaugeScales;

		// Token: 0x040004DD RID: 1245
		private ReportEnumProperty<GaugeOrientations> m_orientation;
	}
}
