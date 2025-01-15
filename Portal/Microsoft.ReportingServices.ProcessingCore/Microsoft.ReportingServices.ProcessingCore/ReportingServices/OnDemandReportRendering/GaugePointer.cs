using System;
using System.Collections.Generic;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000118 RID: 280
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class GaugePointer : GaugePanelObjectCollectionItem, IROMStyleDefinitionContainer, IROMActionOwner
	{
		// Token: 0x06000C55 RID: 3157 RVA: 0x00035784 File Offset: 0x00033984
		internal GaugePointer(GaugePointer defObject, GaugePanel gaugePanel)
		{
			this.m_defObject = defObject;
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x0003579A File Offset: 0x0003399A
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

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06000C57 RID: 3159 RVA: 0x000357D4 File Offset: 0x000339D4
		public string UniqueName
		{
			get
			{
				return this.m_gaugePanel.GaugePanelDef.UniqueName + "x" + this.m_defObject.ID.ToString();
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x00035810 File Offset: 0x00033A10
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

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06000C59 RID: 3161 RVA: 0x0003587E File Offset: 0x00033A7E
		public List<string> FieldsUsedInValueExpression
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06000C5A RID: 3162 RVA: 0x00035881 File Offset: 0x00033A81
		public string Name
		{
			get
			{
				return this.m_defObject.Name;
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06000C5B RID: 3163 RVA: 0x0003588E File Offset: 0x00033A8E
		public GaugeInputValue GaugeInputValue
		{
			get
			{
				if (this.m_gaugeInputValue == null && this.m_defObject.GaugeInputValue != null)
				{
					this.m_gaugeInputValue = new GaugeInputValue(this.m_defObject.GaugeInputValue, this.m_gaugePanel);
				}
				return this.m_gaugeInputValue;
			}
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06000C5C RID: 3164 RVA: 0x000358C8 File Offset: 0x00033AC8
		public ReportEnumProperty<GaugeBarStarts> BarStart
		{
			get
			{
				if (this.m_barStart == null && this.m_defObject.BarStart != null)
				{
					this.m_barStart = new ReportEnumProperty<GaugeBarStarts>(this.m_defObject.BarStart.IsExpression, this.m_defObject.BarStart.OriginalText, EnumTranslator.TranslateGaugeBarStarts(this.m_defObject.BarStart.StringValue, null));
				}
				return this.m_barStart;
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06000C5D RID: 3165 RVA: 0x00035931 File Offset: 0x00033B31
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

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06000C5E RID: 3166 RVA: 0x00035964 File Offset: 0x00033B64
		public PointerImage PointerImage
		{
			get
			{
				if (this.m_pointerImage == null && this.m_defObject.PointerImage != null)
				{
					this.m_pointerImage = new PointerImage(this.m_defObject.PointerImage, this.m_gaugePanel);
				}
				return this.m_pointerImage;
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06000C5F RID: 3167 RVA: 0x0003599D File Offset: 0x00033B9D
		public ReportDoubleProperty MarkerLength
		{
			get
			{
				if (this.m_markerLength == null && this.m_defObject.MarkerLength != null)
				{
					this.m_markerLength = new ReportDoubleProperty(this.m_defObject.MarkerLength);
				}
				return this.m_markerLength;
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x000359D0 File Offset: 0x00033BD0
		public ReportEnumProperty<GaugeMarkerStyles> MarkerStyle
		{
			get
			{
				if (this.m_markerStyle == null && this.m_defObject.MarkerStyle != null)
				{
					this.m_markerStyle = new ReportEnumProperty<GaugeMarkerStyles>(this.m_defObject.MarkerStyle.IsExpression, this.m_defObject.MarkerStyle.OriginalText, EnumTranslator.TranslateGaugeMarkerStyles(this.m_defObject.MarkerStyle.StringValue, null));
				}
				return this.m_markerStyle;
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06000C61 RID: 3169 RVA: 0x00035A3C File Offset: 0x00033C3C
		public ReportEnumProperty<GaugePointerPlacements> Placement
		{
			get
			{
				if (this.m_placement == null && this.m_defObject.Placement != null)
				{
					this.m_placement = new ReportEnumProperty<GaugePointerPlacements>(this.m_defObject.Placement.IsExpression, this.m_defObject.Placement.OriginalText, EnumTranslator.TranslateGaugePointerPlacements(this.m_defObject.Placement.StringValue, null));
				}
				return this.m_placement;
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06000C62 RID: 3170 RVA: 0x00035AA5 File Offset: 0x00033CA5
		public ReportBoolProperty SnappingEnabled
		{
			get
			{
				if (this.m_snappingEnabled == null && this.m_defObject.SnappingEnabled != null)
				{
					this.m_snappingEnabled = new ReportBoolProperty(this.m_defObject.SnappingEnabled);
				}
				return this.m_snappingEnabled;
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x00035AD8 File Offset: 0x00033CD8
		public ReportDoubleProperty SnappingInterval
		{
			get
			{
				if (this.m_snappingInterval == null && this.m_defObject.SnappingInterval != null)
				{
					this.m_snappingInterval = new ReportDoubleProperty(this.m_defObject.SnappingInterval);
				}
				return this.m_snappingInterval;
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06000C64 RID: 3172 RVA: 0x00035B0B File Offset: 0x00033D0B
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

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06000C65 RID: 3173 RVA: 0x00035B3E File Offset: 0x00033D3E
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

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x00035B71 File Offset: 0x00033D71
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

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06000C67 RID: 3175 RVA: 0x00035BA4 File Offset: 0x00033DA4
		internal GaugePanel GaugePanelDef
		{
			get
			{
				return this.m_gaugePanel;
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06000C68 RID: 3176 RVA: 0x00035BAC File Offset: 0x00033DAC
		internal GaugePointer GaugePointerDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06000C69 RID: 3177 RVA: 0x00035BB4 File Offset: 0x00033DB4
		public GaugePointerInstance Instance
		{
			get
			{
				return this.GetInstance();
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06000C6A RID: 3178 RVA: 0x00035BBC File Offset: 0x00033DBC
		// (set) Token: 0x06000C6B RID: 3179 RVA: 0x00035BCF File Offset: 0x00033DCF
		public CompiledGaugePointerInstance[] CompiledInstances
		{
			get
			{
				this.GaugePanelDef.ProcessCompiledInstances();
				return this.m_compiledInstances;
			}
			internal set
			{
				this.m_compiledInstances = value;
			}
		}

		// Token: 0x06000C6C RID: 3180
		internal abstract GaugePointerInstance GetInstance();

		// Token: 0x06000C6D RID: 3181 RVA: 0x00035BD8 File Offset: 0x00033DD8
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
			if (this.m_gaugeInputValue != null)
			{
				this.m_gaugeInputValue.SetNewContext();
			}
			if (this.m_pointerImage != null)
			{
				this.m_pointerImage.SetNewContext();
			}
		}

		// Token: 0x04000559 RID: 1369
		internal GaugePanel m_gaugePanel;

		// Token: 0x0400055A RID: 1370
		internal GaugePointer m_defObject;

		// Token: 0x0400055B RID: 1371
		private Microsoft.ReportingServices.OnDemandReportRendering.Style m_style;

		// Token: 0x0400055C RID: 1372
		private ActionInfo m_actionInfo;

		// Token: 0x0400055D RID: 1373
		private GaugeInputValue m_gaugeInputValue;

		// Token: 0x0400055E RID: 1374
		private ReportEnumProperty<GaugeBarStarts> m_barStart;

		// Token: 0x0400055F RID: 1375
		private ReportDoubleProperty m_distanceFromScale;

		// Token: 0x04000560 RID: 1376
		private PointerImage m_pointerImage;

		// Token: 0x04000561 RID: 1377
		private ReportDoubleProperty m_markerLength;

		// Token: 0x04000562 RID: 1378
		private ReportEnumProperty<GaugeMarkerStyles> m_markerStyle;

		// Token: 0x04000563 RID: 1379
		private ReportEnumProperty<GaugePointerPlacements> m_placement;

		// Token: 0x04000564 RID: 1380
		private ReportBoolProperty m_snappingEnabled;

		// Token: 0x04000565 RID: 1381
		private ReportDoubleProperty m_snappingInterval;

		// Token: 0x04000566 RID: 1382
		private ReportStringProperty m_toolTip;

		// Token: 0x04000567 RID: 1383
		private ReportBoolProperty m_hidden;

		// Token: 0x04000568 RID: 1384
		private ReportDoubleProperty m_width;

		// Token: 0x04000569 RID: 1385
		private CompiledGaugePointerInstance[] m_compiledInstances;
	}
}
