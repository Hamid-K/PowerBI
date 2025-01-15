using System;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200012B RID: 299
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public class TickMarkStyle : IROMStyleDefinitionContainer
	{
		// Token: 0x06000D1B RID: 3355 RVA: 0x00038454 File Offset: 0x00036654
		internal TickMarkStyle(TickMarkStyle defObject, GaugePanel gaugePanel)
		{
			this.m_defObject = defObject;
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x0003846A File Offset: 0x0003666A
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

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06000D1D RID: 3357 RVA: 0x000384A2 File Offset: 0x000366A2
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

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06000D1E RID: 3358 RVA: 0x000384D8 File Offset: 0x000366D8
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

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06000D1F RID: 3359 RVA: 0x00038541 File Offset: 0x00036741
		public ReportBoolProperty EnableGradient
		{
			get
			{
				if (this.m_enableGradient == null && this.m_defObject.EnableGradient != null)
				{
					this.m_enableGradient = new ReportBoolProperty(this.m_defObject.EnableGradient);
				}
				return this.m_enableGradient;
			}
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06000D20 RID: 3360 RVA: 0x00038574 File Offset: 0x00036774
		public ReportDoubleProperty GradientDensity
		{
			get
			{
				if (this.m_gradientDensity == null && this.m_defObject.GradientDensity != null)
				{
					this.m_gradientDensity = new ReportDoubleProperty(this.m_defObject.GradientDensity);
				}
				return this.m_gradientDensity;
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06000D21 RID: 3361 RVA: 0x000385A7 File Offset: 0x000367A7
		public TopImage TickMarkImage
		{
			get
			{
				if (this.m_tickMarkImage == null && this.m_defObject.TickMarkImage != null)
				{
					this.m_tickMarkImage = new TopImage(this.m_defObject.TickMarkImage, this.m_gaugePanel);
				}
				return this.m_tickMarkImage;
			}
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06000D22 RID: 3362 RVA: 0x000385E0 File Offset: 0x000367E0
		public ReportDoubleProperty Length
		{
			get
			{
				if (this.m_length == null && this.m_defObject.Length != null)
				{
					this.m_length = new ReportDoubleProperty(this.m_defObject.Length);
				}
				return this.m_length;
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06000D23 RID: 3363 RVA: 0x00038613 File Offset: 0x00036813
		public ReportDoubleProperty Width
		{
			get
			{
				if (this.m_width == null && this.m_defObject.Width != null)
				{
					this.m_width = new ReportDoubleProperty(this.m_defObject.Width);
				}
				return this.m_width;
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06000D24 RID: 3364 RVA: 0x00038648 File Offset: 0x00036848
		public ReportEnumProperty<GaugeTickMarkShapes> Shape
		{
			get
			{
				if (this.m_shape == null && this.m_defObject.Shape != null)
				{
					this.m_shape = new ReportEnumProperty<GaugeTickMarkShapes>(this.m_defObject.Shape.IsExpression, this.m_defObject.Shape.OriginalText, EnumTranslator.TranslateGaugeTickMarkShapes(this.m_defObject.Shape.StringValue, null));
				}
				return this.m_shape;
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06000D25 RID: 3365 RVA: 0x000386B1 File Offset: 0x000368B1
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

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x000386E4 File Offset: 0x000368E4
		internal GaugePanel GaugePanelDef
		{
			get
			{
				return this.m_gaugePanel;
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06000D27 RID: 3367 RVA: 0x000386EC File Offset: 0x000368EC
		internal TickMarkStyle TickMarkStyleDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06000D28 RID: 3368 RVA: 0x000386F4 File Offset: 0x000368F4
		public TickMarkStyleInstance Instance
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
				return this.m_instance;
			}
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x00038724 File Offset: 0x00036924
		internal virtual void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			if (this.m_tickMarkImage != null)
			{
				this.m_tickMarkImage.SetNewContext();
			}
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x0003875F File Offset: 0x0003695F
		protected virtual TickMarkStyleInstance GetInstance()
		{
			return new TickMarkStyleInstance(this);
		}

		// Token: 0x040005EB RID: 1515
		internal GaugePanel m_gaugePanel;

		// Token: 0x040005EC RID: 1516
		internal TickMarkStyle m_defObject;

		// Token: 0x040005ED RID: 1517
		protected TickMarkStyleInstance m_instance;

		// Token: 0x040005EE RID: 1518
		private Style m_style;

		// Token: 0x040005EF RID: 1519
		private ReportDoubleProperty m_distanceFromScale;

		// Token: 0x040005F0 RID: 1520
		private ReportEnumProperty<GaugeLabelPlacements> m_placement;

		// Token: 0x040005F1 RID: 1521
		private ReportBoolProperty m_enableGradient;

		// Token: 0x040005F2 RID: 1522
		private ReportDoubleProperty m_gradientDensity;

		// Token: 0x040005F3 RID: 1523
		private TopImage m_tickMarkImage;

		// Token: 0x040005F4 RID: 1524
		private ReportDoubleProperty m_length;

		// Token: 0x040005F5 RID: 1525
		private ReportDoubleProperty m_width;

		// Token: 0x040005F6 RID: 1526
		private ReportEnumProperty<GaugeTickMarkShapes> m_shape;

		// Token: 0x040005F7 RID: 1527
		private ReportBoolProperty m_hidden;
	}
}
