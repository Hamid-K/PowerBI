using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000129 RID: 297
	public sealed class Thermometer : IROMStyleDefinitionContainer
	{
		// Token: 0x06000D0C RID: 3340 RVA: 0x00038130 File Offset: 0x00036330
		internal Thermometer(Thermometer defObject, GaugePanel gaugePanel)
		{
			this.m_defObject = defObject;
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06000D0D RID: 3341 RVA: 0x00038146 File Offset: 0x00036346
		public Style Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new Style(this.m_gaugePanel, this.m_gaugePanel, this.m_defObject, this.m_gaugePanel.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06000D0E RID: 3342 RVA: 0x0003817E File Offset: 0x0003637E
		public ReportDoubleProperty BulbOffset
		{
			get
			{
				if (this.m_bulbOffset == null && this.m_defObject.BulbOffset != null)
				{
					this.m_bulbOffset = new ReportDoubleProperty(this.m_defObject.BulbOffset);
				}
				return this.m_bulbOffset;
			}
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x000381B1 File Offset: 0x000363B1
		public ReportDoubleProperty BulbSize
		{
			get
			{
				if (this.m_bulbSize == null && this.m_defObject.BulbSize != null)
				{
					this.m_bulbSize = new ReportDoubleProperty(this.m_defObject.BulbSize);
				}
				return this.m_bulbSize;
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06000D10 RID: 3344 RVA: 0x000381E4 File Offset: 0x000363E4
		public ReportEnumProperty<GaugeThermometerStyles> ThermometerStyle
		{
			get
			{
				if (this.m_thermometerStyle == null && this.m_defObject.ThermometerStyle != null)
				{
					this.m_thermometerStyle = new ReportEnumProperty<GaugeThermometerStyles>(this.m_defObject.ThermometerStyle.IsExpression, this.m_defObject.ThermometerStyle.OriginalText, EnumTranslator.TranslateGaugeThermometerStyles(this.m_defObject.ThermometerStyle.StringValue, null));
				}
				return this.m_thermometerStyle;
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x0003824D File Offset: 0x0003644D
		internal GaugePanel GaugePanelDef
		{
			get
			{
				return this.m_gaugePanel;
			}
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x00038255 File Offset: 0x00036455
		internal Thermometer ThermometerDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06000D13 RID: 3347 RVA: 0x0003825D File Offset: 0x0003645D
		public ThermometerInstance Instance
		{
			get
			{
				if (this.m_gaugePanel.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new ThermometerInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x0003828D File Offset: 0x0003648D
		internal void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
		}

		// Token: 0x040005DF RID: 1503
		private GaugePanel m_gaugePanel;

		// Token: 0x040005E0 RID: 1504
		private Thermometer m_defObject;

		// Token: 0x040005E1 RID: 1505
		private ThermometerInstance m_instance;

		// Token: 0x040005E2 RID: 1506
		private Style m_style;

		// Token: 0x040005E3 RID: 1507
		private ReportDoubleProperty m_bulbOffset;

		// Token: 0x040005E4 RID: 1508
		private ReportDoubleProperty m_bulbSize;

		// Token: 0x040005E5 RID: 1509
		private ReportEnumProperty<GaugeThermometerStyles> m_thermometerStyle;
	}
}
