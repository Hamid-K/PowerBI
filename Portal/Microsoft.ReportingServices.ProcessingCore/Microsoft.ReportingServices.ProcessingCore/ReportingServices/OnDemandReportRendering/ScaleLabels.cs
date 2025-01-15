using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000124 RID: 292
	public sealed class ScaleLabels : IROMStyleDefinitionContainer
	{
		// Token: 0x06000CD8 RID: 3288 RVA: 0x00037287 File Offset: 0x00035487
		internal ScaleLabels(ScaleLabels defObject, GaugePanel gaugePanel)
		{
			this.m_defObject = defObject;
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x0003729D File Offset: 0x0003549D
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

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06000CDA RID: 3290 RVA: 0x000372D5 File Offset: 0x000354D5
		public ReportDoubleProperty Interval
		{
			get
			{
				if (this.m_interval == null && this.m_defObject.Interval != null)
				{
					this.m_interval = new ReportDoubleProperty(this.m_defObject.Interval);
				}
				return this.m_interval;
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06000CDB RID: 3291 RVA: 0x00037308 File Offset: 0x00035508
		public ReportDoubleProperty IntervalOffset
		{
			get
			{
				if (this.m_intervalOffset == null && this.m_defObject.IntervalOffset != null)
				{
					this.m_intervalOffset = new ReportDoubleProperty(this.m_defObject.IntervalOffset);
				}
				return this.m_intervalOffset;
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06000CDC RID: 3292 RVA: 0x0003733B File Offset: 0x0003553B
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

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06000CDD RID: 3293 RVA: 0x0003736E File Offset: 0x0003556E
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

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x000373A1 File Offset: 0x000355A1
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

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x000373D4 File Offset: 0x000355D4
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

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x0003743D File Offset: 0x0003563D
		public ReportBoolProperty RotateLabels
		{
			get
			{
				if (this.m_rotateLabels == null && this.m_defObject.RotateLabels != null)
				{
					this.m_rotateLabels = new ReportBoolProperty(this.m_defObject.RotateLabels);
				}
				return this.m_rotateLabels;
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x00037470 File Offset: 0x00035670
		public ReportBoolProperty ShowEndLabels
		{
			get
			{
				if (this.m_showEndLabels == null && this.m_defObject.ShowEndLabels != null)
				{
					this.m_showEndLabels = new ReportBoolProperty(this.m_defObject.ShowEndLabels);
				}
				return this.m_showEndLabels;
			}
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x000374A3 File Offset: 0x000356A3
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

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x000374D6 File Offset: 0x000356D6
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

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x00037509 File Offset: 0x00035709
		internal GaugePanel GaugePanelDef
		{
			get
			{
				return this.m_gaugePanel;
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x00037511 File Offset: 0x00035711
		internal ScaleLabels ScaleLabelsDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x00037519 File Offset: 0x00035719
		public ScaleLabelsInstance Instance
		{
			get
			{
				if (this.m_gaugePanel.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new ScaleLabelsInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x00037549 File Offset: 0x00035749
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

		// Token: 0x040005B1 RID: 1457
		private GaugePanel m_gaugePanel;

		// Token: 0x040005B2 RID: 1458
		private ScaleLabels m_defObject;

		// Token: 0x040005B3 RID: 1459
		private ScaleLabelsInstance m_instance;

		// Token: 0x040005B4 RID: 1460
		private Style m_style;

		// Token: 0x040005B5 RID: 1461
		private ReportDoubleProperty m_interval;

		// Token: 0x040005B6 RID: 1462
		private ReportDoubleProperty m_intervalOffset;

		// Token: 0x040005B7 RID: 1463
		private ReportBoolProperty m_allowUpsideDown;

		// Token: 0x040005B8 RID: 1464
		private ReportDoubleProperty m_distanceFromScale;

		// Token: 0x040005B9 RID: 1465
		private ReportDoubleProperty m_fontAngle;

		// Token: 0x040005BA RID: 1466
		private ReportEnumProperty<GaugeLabelPlacements> m_placement;

		// Token: 0x040005BB RID: 1467
		private ReportBoolProperty m_rotateLabels;

		// Token: 0x040005BC RID: 1468
		private ReportBoolProperty m_showEndLabels;

		// Token: 0x040005BD RID: 1469
		private ReportBoolProperty m_hidden;

		// Token: 0x040005BE RID: 1470
		private ReportBoolProperty m_useFontPercent;
	}
}
