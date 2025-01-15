using System;
using System.Collections.Generic;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000121 RID: 289
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class GaugeScale : GaugePanelObjectCollectionItem, IROMStyleDefinitionContainer, IROMActionOwner
	{
		// Token: 0x06000CA8 RID: 3240 RVA: 0x000369CC File Offset: 0x00034BCC
		internal GaugeScale(GaugeScale defObject, GaugePanel gaugePanel)
		{
			this.m_defObject = defObject;
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x000369E2 File Offset: 0x00034BE2
		public Microsoft.ReportingServices.OnDemandReportRendering.Style Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new Microsoft.ReportingServices.OnDemandReportRendering.Style(this.m_gaugePanel, this.m_gaugePanel, this.m_defObject, this.m_gaugePanel.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x00036A1C File Offset: 0x00034C1C
		public string UniqueName
		{
			get
			{
				return this.m_gaugePanel.GaugePanelDef.UniqueName + "x" + this.m_defObject.ID.ToString();
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x00036A58 File Offset: 0x00034C58
		public ActionInfo ActionInfo
		{
			get
			{
				if (this.m_actionInfo == null && this.m_defObject.Action != null)
				{
					this.m_actionInfo = new ActionInfo(this.m_gaugePanel.RenderingContext, this.m_gaugePanel, this.m_defObject.Action, this.m_gaugePanel.GaugePanelDef, this.m_gaugePanel, ObjectType.GaugePanel, this.m_gaugePanel.Name, this);
				}
				return this.m_actionInfo;
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06000CAC RID: 3244 RVA: 0x00036AC6 File Offset: 0x00034CC6
		public List<string> FieldsUsedInValueExpression
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06000CAD RID: 3245 RVA: 0x00036AC9 File Offset: 0x00034CC9
		public string Name
		{
			get
			{
				return this.m_defObject.Name;
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06000CAE RID: 3246 RVA: 0x00036AD6 File Offset: 0x00034CD6
		public ScaleRangeCollection ScaleRanges
		{
			get
			{
				if (this.m_scaleRanges == null && this.m_defObject.ScaleRanges != null)
				{
					this.m_scaleRanges = new ScaleRangeCollection(this, this.m_gaugePanel);
				}
				return this.m_scaleRanges;
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06000CAF RID: 3247 RVA: 0x00036B05 File Offset: 0x00034D05
		public CustomLabelCollection CustomLabels
		{
			get
			{
				if (this.m_customLabels == null && this.m_defObject.CustomLabels != null)
				{
					this.m_customLabels = new CustomLabelCollection(this, this.m_gaugePanel);
				}
				return this.m_customLabels;
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x00036B34 File Offset: 0x00034D34
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

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x00036B67 File Offset: 0x00034D67
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

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06000CB2 RID: 3250 RVA: 0x00036B9A File Offset: 0x00034D9A
		public ReportBoolProperty Logarithmic
		{
			get
			{
				if (this.m_logarithmic == null && this.m_defObject.Logarithmic != null)
				{
					this.m_logarithmic = new ReportBoolProperty(this.m_defObject.Logarithmic);
				}
				return this.m_logarithmic;
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x00036BCD File Offset: 0x00034DCD
		public ReportDoubleProperty LogarithmicBase
		{
			get
			{
				if (this.m_logarithmicBase == null && this.m_defObject.LogarithmicBase != null)
				{
					this.m_logarithmicBase = new ReportDoubleProperty(this.m_defObject.LogarithmicBase);
				}
				return this.m_logarithmicBase;
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x00036C00 File Offset: 0x00034E00
		public GaugeInputValue MaximumValue
		{
			get
			{
				if (this.m_maximumValue == null && this.m_defObject.MaximumValue != null)
				{
					this.m_maximumValue = new GaugeInputValue(this.m_defObject.MaximumValue, this.m_gaugePanel);
				}
				return this.m_maximumValue;
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x00036C39 File Offset: 0x00034E39
		public GaugeInputValue MinimumValue
		{
			get
			{
				if (this.m_minimumValue == null && this.m_defObject.MinimumValue != null)
				{
					this.m_minimumValue = new GaugeInputValue(this.m_defObject.MinimumValue, this.m_gaugePanel);
				}
				return this.m_minimumValue;
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x00036C72 File Offset: 0x00034E72
		public ReportDoubleProperty Multiplier
		{
			get
			{
				if (this.m_multiplier == null && this.m_defObject.Multiplier != null)
				{
					this.m_multiplier = new ReportDoubleProperty(this.m_defObject.Multiplier);
				}
				return this.m_multiplier;
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x00036CA5 File Offset: 0x00034EA5
		public ReportBoolProperty Reversed
		{
			get
			{
				if (this.m_reversed == null && this.m_defObject.Reversed != null)
				{
					this.m_reversed = new ReportBoolProperty(this.m_defObject.Reversed);
				}
				return this.m_reversed;
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x00036CD8 File Offset: 0x00034ED8
		public GaugeTickMarks GaugeMajorTickMarks
		{
			get
			{
				if (this.m_gaugeMajorTickMarks == null && this.m_defObject.GaugeMajorTickMarks != null)
				{
					this.m_gaugeMajorTickMarks = new GaugeTickMarks(this.m_defObject.GaugeMajorTickMarks, this.m_gaugePanel);
				}
				return this.m_gaugeMajorTickMarks;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x00036D11 File Offset: 0x00034F11
		public GaugeTickMarks GaugeMinorTickMarks
		{
			get
			{
				if (this.m_gaugeMinorTickMarks == null && this.m_defObject.GaugeMinorTickMarks != null)
				{
					this.m_gaugeMinorTickMarks = new GaugeTickMarks(this.m_defObject.GaugeMinorTickMarks, this.m_gaugePanel);
				}
				return this.m_gaugeMinorTickMarks;
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x00036D4A File Offset: 0x00034F4A
		public ScalePin MaximumPin
		{
			get
			{
				if (this.m_maximumPin == null && this.m_defObject.MaximumPin != null)
				{
					this.m_maximumPin = new ScalePin(this.m_defObject.MaximumPin, this.m_gaugePanel);
				}
				return this.m_maximumPin;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06000CBB RID: 3259 RVA: 0x00036D83 File Offset: 0x00034F83
		public ScalePin MinimumPin
		{
			get
			{
				if (this.m_minimumPin == null && this.m_defObject.MinimumPin != null)
				{
					this.m_minimumPin = new ScalePin(this.m_defObject.MinimumPin, this.m_gaugePanel);
				}
				return this.m_minimumPin;
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06000CBC RID: 3260 RVA: 0x00036DBC File Offset: 0x00034FBC
		public ScaleLabels ScaleLabels
		{
			get
			{
				if (this.m_scaleLabels == null && this.m_defObject.ScaleLabels != null)
				{
					this.m_scaleLabels = new ScaleLabels(this.m_defObject.ScaleLabels, this.m_gaugePanel);
				}
				return this.m_scaleLabels;
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x00036DF5 File Offset: 0x00034FF5
		public ReportBoolProperty TickMarksOnTop
		{
			get
			{
				if (this.m_tickMarksOnTop == null && this.m_defObject.TickMarksOnTop != null)
				{
					this.m_tickMarksOnTop = new ReportBoolProperty(this.m_defObject.TickMarksOnTop);
				}
				return this.m_tickMarksOnTop;
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x00036E28 File Offset: 0x00035028
		public ReportStringProperty ToolTip
		{
			get
			{
				if (this.m_toolTip == null && this.m_defObject.ToolTip != null)
				{
					this.m_toolTip = new ReportStringProperty(this.m_defObject.ToolTip);
				}
				return this.m_toolTip;
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06000CBF RID: 3263 RVA: 0x00036E5B File Offset: 0x0003505B
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

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x00036E8E File Offset: 0x0003508E
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

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x00036EC1 File Offset: 0x000350C1
		internal GaugePanel GaugePanelDef
		{
			get
			{
				return this.m_gaugePanel;
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x00036EC9 File Offset: 0x000350C9
		internal GaugeScale GaugeScaleDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06000CC3 RID: 3267 RVA: 0x00036ED1 File Offset: 0x000350D1
		public GaugeScaleInstance Instance
		{
			get
			{
				return this.GetInstance();
			}
		}

		// Token: 0x06000CC4 RID: 3268
		internal abstract GaugeScaleInstance GetInstance();

		// Token: 0x06000CC5 RID: 3269 RVA: 0x00036EDC File Offset: 0x000350DC
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
			if (this.m_actionInfo != null)
			{
				this.m_actionInfo.SetNewContext();
			}
			if (this.m_scaleRanges != null)
			{
				this.m_scaleRanges.SetNewContext();
			}
			if (this.m_customLabels != null)
			{
				this.m_customLabels.SetNewContext();
			}
			if (this.m_maximumValue != null)
			{
				this.m_maximumValue.SetNewContext();
			}
			if (this.m_minimumValue != null)
			{
				this.m_minimumValue.SetNewContext();
			}
			if (this.m_gaugeMajorTickMarks != null)
			{
				this.m_gaugeMajorTickMarks.SetNewContext();
			}
			if (this.m_gaugeMinorTickMarks != null)
			{
				this.m_gaugeMinorTickMarks.SetNewContext();
			}
			if (this.m_maximumPin != null)
			{
				this.m_maximumPin.SetNewContext();
			}
			if (this.m_minimumPin != null)
			{
				this.m_minimumPin.SetNewContext();
			}
			if (this.m_scaleLabels != null)
			{
				this.m_scaleLabels.SetNewContext();
			}
		}

		// Token: 0x04000592 RID: 1426
		internal GaugePanel m_gaugePanel;

		// Token: 0x04000593 RID: 1427
		internal GaugeScale m_defObject;

		// Token: 0x04000594 RID: 1428
		private Microsoft.ReportingServices.OnDemandReportRendering.Style m_style;

		// Token: 0x04000595 RID: 1429
		private ActionInfo m_actionInfo;

		// Token: 0x04000596 RID: 1430
		private ScaleRangeCollection m_scaleRanges;

		// Token: 0x04000597 RID: 1431
		private CustomLabelCollection m_customLabels;

		// Token: 0x04000598 RID: 1432
		private ReportDoubleProperty m_interval;

		// Token: 0x04000599 RID: 1433
		private ReportDoubleProperty m_intervalOffset;

		// Token: 0x0400059A RID: 1434
		private ReportBoolProperty m_logarithmic;

		// Token: 0x0400059B RID: 1435
		private ReportDoubleProperty m_logarithmicBase;

		// Token: 0x0400059C RID: 1436
		private GaugeInputValue m_maximumValue;

		// Token: 0x0400059D RID: 1437
		private GaugeInputValue m_minimumValue;

		// Token: 0x0400059E RID: 1438
		private ReportDoubleProperty m_multiplier;

		// Token: 0x0400059F RID: 1439
		private ReportBoolProperty m_reversed;

		// Token: 0x040005A0 RID: 1440
		private GaugeTickMarks m_gaugeMajorTickMarks;

		// Token: 0x040005A1 RID: 1441
		private GaugeTickMarks m_gaugeMinorTickMarks;

		// Token: 0x040005A2 RID: 1442
		private ScalePin m_maximumPin;

		// Token: 0x040005A3 RID: 1443
		private ScalePin m_minimumPin;

		// Token: 0x040005A4 RID: 1444
		private ScaleLabels m_scaleLabels;

		// Token: 0x040005A5 RID: 1445
		private ReportBoolProperty m_tickMarksOnTop;

		// Token: 0x040005A6 RID: 1446
		private ReportStringProperty m_toolTip;

		// Token: 0x040005A7 RID: 1447
		private ReportBoolProperty m_hidden;

		// Token: 0x040005A8 RID: 1448
		private ReportDoubleProperty m_width;
	}
}
