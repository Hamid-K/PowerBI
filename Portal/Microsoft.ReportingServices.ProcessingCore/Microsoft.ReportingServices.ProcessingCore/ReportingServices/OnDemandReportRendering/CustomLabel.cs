using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020000EE RID: 238
	public sealed class CustomLabel : GaugePanelObjectCollectionItem, IROMStyleDefinitionContainer
	{
		// Token: 0x06000B08 RID: 2824 RVA: 0x00031836 File Offset: 0x0002FA36
		internal CustomLabel(CustomLabel defObject, GaugePanel gaugePanel)
		{
			this.m_defObject = defObject;
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06000B09 RID: 2825 RVA: 0x0003184C File Offset: 0x0002FA4C
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

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06000B0A RID: 2826 RVA: 0x00031884 File Offset: 0x0002FA84
		public string Name
		{
			get
			{
				return this.m_defObject.Name;
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x00031891 File Offset: 0x0002FA91
		public ReportStringProperty Text
		{
			get
			{
				if (this.m_text == null && this.m_defObject.Text != null)
				{
					this.m_text = new ReportStringProperty(this.m_defObject.Text);
				}
				return this.m_text;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06000B0C RID: 2828 RVA: 0x000318C4 File Offset: 0x0002FAC4
		public ReportBoolProperty AllowUpsideDown
		{
			get
			{
				if (this.m_allowUpsideDown == null && this.m_defObject.AllowUpsideDown != null)
				{
					this.m_allowUpsideDown = new ReportBoolProperty(this.m_defObject.AllowUpsideDown);
				}
				return this.m_allowUpsideDown;
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x000318F7 File Offset: 0x0002FAF7
		public ReportDoubleProperty DistanceFromScale
		{
			get
			{
				if (this.m_distanceFromScale == null && this.m_defObject.DistanceFromScale != null)
				{
					this.m_distanceFromScale = new ReportDoubleProperty(this.m_defObject.DistanceFromScale);
				}
				return this.m_distanceFromScale;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06000B0E RID: 2830 RVA: 0x0003192A File Offset: 0x0002FB2A
		public ReportDoubleProperty FontAngle
		{
			get
			{
				if (this.m_fontAngle == null && this.m_defObject.FontAngle != null)
				{
					this.m_fontAngle = new ReportDoubleProperty(this.m_defObject.FontAngle);
				}
				return this.m_fontAngle;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x00031960 File Offset: 0x0002FB60
		public ReportEnumProperty<GaugeLabelPlacements> Placement
		{
			get
			{
				if (this.m_placement == null && this.m_defObject.Placement != null)
				{
					this.m_placement = new ReportEnumProperty<GaugeLabelPlacements>(this.m_defObject.Placement.IsExpression, this.m_defObject.Placement.OriginalText, EnumTranslator.TranslateGaugeLabelPlacements(this.m_defObject.Placement.StringValue, null));
				}
				return this.m_placement;
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x000319C9 File Offset: 0x0002FBC9
		public ReportBoolProperty RotateLabel
		{
			get
			{
				if (this.m_rotateLabel == null && this.m_defObject.RotateLabel != null)
				{
					this.m_rotateLabel = new ReportBoolProperty(this.m_defObject.RotateLabel);
				}
				return this.m_rotateLabel;
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x000319FC File Offset: 0x0002FBFC
		public TickMarkStyle TickMarkStyle
		{
			get
			{
				if (this.m_tickMarkStyle == null && this.m_defObject.TickMarkStyle != null)
				{
					this.m_tickMarkStyle = new TickMarkStyle(this.m_defObject.TickMarkStyle, this.m_gaugePanel);
				}
				return this.m_tickMarkStyle;
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06000B12 RID: 2834 RVA: 0x00031A35 File Offset: 0x0002FC35
		public ReportDoubleProperty Value
		{
			get
			{
				if (this.m_value == null && this.m_defObject.Value != null)
				{
					this.m_value = new ReportDoubleProperty(this.m_defObject.Value);
				}
				return this.m_value;
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x00031A68 File Offset: 0x0002FC68
		public ReportBoolProperty Hidden
		{
			get
			{
				if (this.m_hidden == null && this.m_defObject.Hidden != null)
				{
					this.m_hidden = new ReportBoolProperty(this.m_defObject.Hidden);
				}
				return this.m_hidden;
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x00031A9B File Offset: 0x0002FC9B
		public ReportBoolProperty UseFontPercent
		{
			get
			{
				if (this.m_useFontPercent == null && this.m_defObject.UseFontPercent != null)
				{
					this.m_useFontPercent = new ReportBoolProperty(this.m_defObject.UseFontPercent);
				}
				return this.m_useFontPercent;
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x00031ACE File Offset: 0x0002FCCE
		internal GaugePanel GaugePanelDef
		{
			get
			{
				return this.m_gaugePanel;
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x00031AD6 File Offset: 0x0002FCD6
		internal CustomLabel CustomLabelDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x00031ADE File Offset: 0x0002FCDE
		public CustomLabelInstance Instance
		{
			get
			{
				if (this.m_gaugePanel.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new CustomLabelInstance(this);
				}
				return (CustomLabelInstance)this.m_instance;
			}
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x00031B13 File Offset: 0x0002FD13
		internal override void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			if (this.m_tickMarkStyle != null)
			{
				this.m_tickMarkStyle.SetNewContext();
			}
		}

		// Token: 0x040004AA RID: 1194
		private GaugePanel m_gaugePanel;

		// Token: 0x040004AB RID: 1195
		private CustomLabel m_defObject;

		// Token: 0x040004AC RID: 1196
		private Style m_style;

		// Token: 0x040004AD RID: 1197
		private ReportStringProperty m_text;

		// Token: 0x040004AE RID: 1198
		private ReportBoolProperty m_allowUpsideDown;

		// Token: 0x040004AF RID: 1199
		private ReportDoubleProperty m_distanceFromScale;

		// Token: 0x040004B0 RID: 1200
		private ReportDoubleProperty m_fontAngle;

		// Token: 0x040004B1 RID: 1201
		private ReportEnumProperty<GaugeLabelPlacements> m_placement;

		// Token: 0x040004B2 RID: 1202
		private ReportBoolProperty m_rotateLabel;

		// Token: 0x040004B3 RID: 1203
		private TickMarkStyle m_tickMarkStyle;

		// Token: 0x040004B4 RID: 1204
		private ReportDoubleProperty m_value;

		// Token: 0x040004B5 RID: 1205
		private ReportBoolProperty m_hidden;

		// Token: 0x040004B6 RID: 1206
		private ReportBoolProperty m_useFontPercent;
	}
}
