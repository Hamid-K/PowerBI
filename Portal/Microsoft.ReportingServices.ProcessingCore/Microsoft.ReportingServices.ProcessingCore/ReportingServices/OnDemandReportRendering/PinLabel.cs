using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000106 RID: 262
	public sealed class PinLabel : IROMStyleDefinitionContainer
	{
		// Token: 0x06000B8E RID: 2958 RVA: 0x00033184 File Offset: 0x00031384
		internal PinLabel(PinLabel defObject, GaugePanel gaugePanel)
		{
			this.m_defObject = defObject;
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06000B8F RID: 2959 RVA: 0x0003319A File Offset: 0x0003139A
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

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06000B90 RID: 2960 RVA: 0x000331D2 File Offset: 0x000313D2
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

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06000B91 RID: 2961 RVA: 0x00033205 File Offset: 0x00031405
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

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06000B92 RID: 2962 RVA: 0x00033238 File Offset: 0x00031438
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

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06000B93 RID: 2963 RVA: 0x0003326B File Offset: 0x0003146B
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

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x000332A0 File Offset: 0x000314A0
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

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06000B95 RID: 2965 RVA: 0x00033309 File Offset: 0x00031509
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

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x0003333C File Offset: 0x0003153C
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

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06000B97 RID: 2967 RVA: 0x0003336F File Offset: 0x0003156F
		internal GaugePanel GaugePanelDef
		{
			get
			{
				return this.m_gaugePanel;
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x00033377 File Offset: 0x00031577
		internal PinLabel PinLabelDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06000B99 RID: 2969 RVA: 0x0003337F File Offset: 0x0003157F
		public PinLabelInstance Instance
		{
			get
			{
				if (this.m_gaugePanel.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new PinLabelInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x000333AF File Offset: 0x000315AF
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

		// Token: 0x040004EE RID: 1262
		private GaugePanel m_gaugePanel;

		// Token: 0x040004EF RID: 1263
		private PinLabel m_defObject;

		// Token: 0x040004F0 RID: 1264
		private PinLabelInstance m_instance;

		// Token: 0x040004F1 RID: 1265
		private Style m_style;

		// Token: 0x040004F2 RID: 1266
		private ReportStringProperty m_text;

		// Token: 0x040004F3 RID: 1267
		private ReportBoolProperty m_allowUpsideDown;

		// Token: 0x040004F4 RID: 1268
		private ReportDoubleProperty m_distanceFromScale;

		// Token: 0x040004F5 RID: 1269
		private ReportDoubleProperty m_fontAngle;

		// Token: 0x040004F6 RID: 1270
		private ReportEnumProperty<GaugeLabelPlacements> m_placement;

		// Token: 0x040004F7 RID: 1271
		private ReportBoolProperty m_rotateLabel;

		// Token: 0x040004F8 RID: 1272
		private ReportBoolProperty m_useFontPercent;
	}
}
