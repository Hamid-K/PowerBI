using System;
using System.Drawing;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000136 RID: 310
	public sealed class IndicatorState : GaugePanelObjectCollectionItem
	{
		// Token: 0x06000D8B RID: 3467 RVA: 0x00039D41 File Offset: 0x00037F41
		internal IndicatorState(IndicatorState defObject, GaugePanel gaugePanel)
		{
			this.m_defObject = defObject;
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x00039D57 File Offset: 0x00037F57
		public string Name
		{
			get
			{
				return this.m_defObject.Name;
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06000D8D RID: 3469 RVA: 0x00039D64 File Offset: 0x00037F64
		public GaugeInputValue StartValue
		{
			get
			{
				if (this.m_startValue == null && this.m_defObject.StartValue != null)
				{
					this.m_startValue = new GaugeInputValue(this.m_defObject.StartValue, this.m_gaugePanel);
				}
				return this.m_startValue;
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06000D8E RID: 3470 RVA: 0x00039D9D File Offset: 0x00037F9D
		public GaugeInputValue EndValue
		{
			get
			{
				if (this.m_endValue == null && this.m_defObject.EndValue != null)
				{
					this.m_endValue = new GaugeInputValue(this.m_defObject.EndValue, this.m_gaugePanel);
				}
				return this.m_endValue;
			}
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06000D8F RID: 3471 RVA: 0x00039DD8 File Offset: 0x00037FD8
		public ReportColorProperty Color
		{
			get
			{
				if (this.m_color == null && this.m_defObject.Color != null)
				{
					ExpressionInfo color = this.m_defObject.Color;
					if (color != null)
					{
						this.m_color = new ReportColorProperty(color.IsExpression, this.m_defObject.Color.OriginalText, color.IsExpression ? null : new ReportColor(color.StringValue.Trim(), true), color.IsExpression ? new ReportColor("", global::System.Drawing.Color.Empty, true) : null);
					}
				}
				return this.m_color;
			}
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06000D90 RID: 3472 RVA: 0x00039E67 File Offset: 0x00038067
		public ReportDoubleProperty ScaleFactor
		{
			get
			{
				if (this.m_scaleFactor == null && this.m_defObject.ScaleFactor != null)
				{
					this.m_scaleFactor = new ReportDoubleProperty(this.m_defObject.ScaleFactor);
				}
				return this.m_scaleFactor;
			}
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06000D91 RID: 3473 RVA: 0x00039E9C File Offset: 0x0003809C
		public ReportEnumProperty<GaugeStateIndicatorStyles> IndicatorStyle
		{
			get
			{
				if (this.m_indicatorStyle == null && this.m_defObject.IndicatorStyle != null)
				{
					this.m_indicatorStyle = new ReportEnumProperty<GaugeStateIndicatorStyles>(this.m_defObject.IndicatorStyle.IsExpression, this.m_defObject.IndicatorStyle.OriginalText, EnumTranslator.TranslateGaugeStateIndicatorStyles(this.m_defObject.IndicatorStyle.StringValue, null));
				}
				return this.m_indicatorStyle;
			}
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06000D92 RID: 3474 RVA: 0x00039F05 File Offset: 0x00038105
		public IndicatorImage IndicatorImage
		{
			get
			{
				if (this.m_indicatorImage == null && this.m_defObject.IndicatorImage != null)
				{
					this.m_indicatorImage = new IndicatorImage(this.m_defObject.IndicatorImage, this.m_gaugePanel);
				}
				return this.m_indicatorImage;
			}
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06000D93 RID: 3475 RVA: 0x00039F3E File Offset: 0x0003813E
		internal GaugePanel GaugePanelDef
		{
			get
			{
				return this.m_gaugePanel;
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06000D94 RID: 3476 RVA: 0x00039F46 File Offset: 0x00038146
		internal IndicatorState IndicatorStateDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06000D95 RID: 3477 RVA: 0x00039F4E File Offset: 0x0003814E
		public IndicatorStateInstance Instance
		{
			get
			{
				if (this.m_gaugePanel.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new IndicatorStateInstance(this);
				}
				return (IndicatorStateInstance)this.m_instance;
			}
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x00039F84 File Offset: 0x00038184
		internal override void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_startValue != null)
			{
				this.m_startValue.SetNewContext();
			}
			if (this.m_endValue != null)
			{
				this.m_endValue.SetNewContext();
			}
			if (this.m_indicatorImage != null)
			{
				this.m_indicatorImage.SetNewContext();
			}
		}

		// Token: 0x04000633 RID: 1587
		private GaugePanel m_gaugePanel;

		// Token: 0x04000634 RID: 1588
		private IndicatorState m_defObject;

		// Token: 0x04000635 RID: 1589
		private GaugeInputValue m_startValue;

		// Token: 0x04000636 RID: 1590
		private GaugeInputValue m_endValue;

		// Token: 0x04000637 RID: 1591
		private ReportColorProperty m_color;

		// Token: 0x04000638 RID: 1592
		private ReportDoubleProperty m_scaleFactor;

		// Token: 0x04000639 RID: 1593
		private ReportEnumProperty<GaugeStateIndicatorStyles> m_indicatorStyle;

		// Token: 0x0400063A RID: 1594
		private IndicatorImage m_indicatorImage;
	}
}
