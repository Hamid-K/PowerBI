using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000109 RID: 265
	public sealed class GaugePanel : Microsoft.ReportingServices.OnDemandReportRendering.DataRegion
	{
		// Token: 0x06000BB1 RID: 2993 RVA: 0x00033917 File Offset: 0x00031B17
		internal GaugePanel(IDefinitionPath parentDefinitionPath, int indexIntoParentCollectionDef, GaugePanel reportItemDef, RenderingContext renderingContext)
			: base(parentDefinitionPath, indexIntoParentCollectionDef, reportItemDef, renderingContext)
		{
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06000BB2 RID: 2994 RVA: 0x00033924 File Offset: 0x00031B24
		public GaugeMember GaugeMember
		{
			get
			{
				if (this.m_gaugeMember == null)
				{
					this.m_gaugeMember = new GaugeMember(this.ReportScope, this, this, null, this.GaugePanelDef.GaugeMember);
				}
				return this.m_gaugeMember;
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06000BB3 RID: 2995 RVA: 0x00033953 File Offset: 0x00031B53
		internal GaugePanel GaugePanelDef
		{
			get
			{
				return this.m_reportItemDef as GaugePanel;
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06000BB4 RID: 2996 RVA: 0x00033960 File Offset: 0x00031B60
		internal override bool HasDataCells
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06000BB5 RID: 2997 RVA: 0x00033963 File Offset: 0x00031B63
		internal override IDataRegionRowCollection RowCollection
		{
			get
			{
				if (this.m_gaugeRowCollection == null && this.GaugePanelDef.Rows != null)
				{
					this.m_gaugeRowCollection = new GaugeRowCollection(this, (GaugeRowList)this.GaugePanelDef.Rows);
				}
				return this.m_gaugeRowCollection;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06000BB6 RID: 2998 RVA: 0x0003399C File Offset: 0x00031B9C
		public LinearGaugeCollection LinearGauges
		{
			get
			{
				if (this.m_linearGauges == null && this.GaugePanelDef.LinearGauges != null)
				{
					this.m_linearGauges = new LinearGaugeCollection(this);
				}
				return this.m_linearGauges;
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06000BB7 RID: 2999 RVA: 0x000339C5 File Offset: 0x00031BC5
		public RadialGaugeCollection RadialGauges
		{
			get
			{
				if (this.m_radialGauges == null && this.GaugePanelDef.RadialGauges != null)
				{
					this.m_radialGauges = new RadialGaugeCollection(this);
				}
				return this.m_radialGauges;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06000BB8 RID: 3000 RVA: 0x000339EE File Offset: 0x00031BEE
		public NumericIndicatorCollection NumericIndicators
		{
			get
			{
				if (this.m_numericIndicators == null && this.GaugePanelDef.NumericIndicators != null)
				{
					this.m_numericIndicators = new NumericIndicatorCollection(this);
				}
				return this.m_numericIndicators;
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x00033A17 File Offset: 0x00031C17
		public StateIndicatorCollection StateIndicators
		{
			get
			{
				if (this.m_stateIndicators == null && this.GaugePanelDef.StateIndicators != null)
				{
					this.m_stateIndicators = new StateIndicatorCollection(this);
				}
				return this.m_stateIndicators;
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06000BBA RID: 3002 RVA: 0x00033A40 File Offset: 0x00031C40
		public GaugeImageCollection GaugeImages
		{
			get
			{
				if (this.m_gaugeImages == null && this.GaugePanelDef.GaugeImages != null)
				{
					this.m_gaugeImages = new GaugeImageCollection(this);
				}
				return this.m_gaugeImages;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x00033A69 File Offset: 0x00031C69
		public GaugeLabelCollection GaugeLabels
		{
			get
			{
				if (this.m_gaugeLabels == null && this.GaugePanelDef.GaugeLabels != null)
				{
					this.m_gaugeLabels = new GaugeLabelCollection(this);
				}
				return this.m_gaugeLabels;
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06000BBC RID: 3004 RVA: 0x00033A94 File Offset: 0x00031C94
		public ReportEnumProperty<GaugeAntiAliasings> AntiAliasing
		{
			get
			{
				if (this.m_antiAliasing == null && this.GaugePanelDef.AntiAliasing != null)
				{
					this.m_antiAliasing = new ReportEnumProperty<GaugeAntiAliasings>(this.GaugePanelDef.AntiAliasing.IsExpression, this.GaugePanelDef.AntiAliasing.OriginalText, EnumTranslator.TranslateGaugeAntiAliasings(this.GaugePanelDef.AntiAliasing.StringValue, null));
				}
				return this.m_antiAliasing;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x00033AFD File Offset: 0x00031CFD
		public ReportBoolProperty AutoLayout
		{
			get
			{
				if (this.m_autoLayout == null && this.GaugePanelDef.AutoLayout != null)
				{
					this.m_autoLayout = new ReportBoolProperty(this.GaugePanelDef.AutoLayout);
				}
				return this.m_autoLayout;
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06000BBE RID: 3006 RVA: 0x00033B30 File Offset: 0x00031D30
		public BackFrame BackFrame
		{
			get
			{
				if (this.m_backFrame == null && this.GaugePanelDef.BackFrame != null)
				{
					this.m_backFrame = new BackFrame(this.GaugePanelDef.BackFrame, this);
				}
				return this.m_backFrame;
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06000BBF RID: 3007 RVA: 0x00033B64 File Offset: 0x00031D64
		public ReportDoubleProperty ShadowIntensity
		{
			get
			{
				if (this.m_shadowIntensity == null && this.GaugePanelDef.ShadowIntensity != null)
				{
					this.m_shadowIntensity = new ReportDoubleProperty(this.GaugePanelDef.ShadowIntensity);
				}
				return this.m_shadowIntensity;
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x00033B98 File Offset: 0x00031D98
		public ReportEnumProperty<TextAntiAliasingQualities> TextAntiAliasingQuality
		{
			get
			{
				if (this.m_textAntiAliasingQuality == null && this.GaugePanelDef.TextAntiAliasingQuality != null)
				{
					this.m_textAntiAliasingQuality = new ReportEnumProperty<TextAntiAliasingQualities>(this.GaugePanelDef.TextAntiAliasingQuality.IsExpression, this.GaugePanelDef.TextAntiAliasingQuality.OriginalText, EnumTranslator.TranslateTextAntiAliasingQualities(this.GaugePanelDef.TextAntiAliasingQuality.StringValue, null));
				}
				return this.m_textAntiAliasingQuality;
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x00033C01 File Offset: 0x00031E01
		public TopImage TopImage
		{
			get
			{
				if (this.m_topImage == null && this.GaugePanelDef.TopImage != null)
				{
					this.m_topImage = new TopImage(this.GaugePanelDef.TopImage, this);
				}
				return this.m_topImage;
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x00033C35 File Offset: 0x00031E35
		public new GaugePanelInstance Instance
		{
			get
			{
				return (GaugePanelInstance)this.GetOrCreateInstance();
			}
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x00033C42 File Offset: 0x00031E42
		internal override ReportItemInstance GetOrCreateInstance()
		{
			if (this.m_instance == null)
			{
				this.m_instance = new GaugePanelInstance(this);
			}
			return this.m_instance;
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x00033C60 File Offset: 0x00031E60
		internal override void SetNewContextChildren()
		{
			if (this.m_gaugeMember != null)
			{
				this.m_gaugeMember.ResetContext();
			}
			if (this.m_gaugeRowMember != null)
			{
				this.m_gaugeRowMember.ResetContext();
			}
			if (this.m_gaugeRowCollection != null)
			{
				this.m_gaugeRowCollection.SetNewContext();
			}
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_linearGauges != null)
			{
				this.m_linearGauges.SetNewContext();
			}
			if (this.m_radialGauges != null)
			{
				this.m_radialGauges.SetNewContext();
			}
			if (this.m_numericIndicators != null)
			{
				this.m_numericIndicators.SetNewContext();
			}
			if (this.m_stateIndicators != null)
			{
				this.m_stateIndicators.SetNewContext();
			}
			if (this.m_gaugeImages != null)
			{
				this.m_gaugeImages.SetNewContext();
			}
			if (this.m_gaugeLabels != null)
			{
				this.m_gaugeLabels.SetNewContext();
			}
			if (this.m_backFrame != null)
			{
				this.m_backFrame.SetNewContext();
			}
			if (this.m_topImage != null)
			{
				this.m_topImage.SetNewContext();
			}
			this.m_compilationState = GaugePanel.CompilationState.NotCompiled;
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x00033D58 File Offset: 0x00031F58
		internal List<GaugeInputValue> GetGaugeInputValues()
		{
			List<GaugeInputValue> list = new List<GaugeInputValue>();
			if (this.RadialGauges != null)
			{
				foreach (RadialGauge radialGauge in this.RadialGauges)
				{
					if (radialGauge.GaugeScales != null)
					{
						foreach (RadialScale radialScale in radialGauge.GaugeScales)
						{
							if (radialScale.MaximumValue != null)
							{
								list.Add(radialScale.MaximumValue);
							}
							if (radialScale.MinimumValue != null)
							{
								list.Add(radialScale.MinimumValue);
							}
							if (radialScale.GaugePointers != null)
							{
								foreach (RadialPointer radialPointer in radialScale.GaugePointers)
								{
									if (radialPointer.GaugeInputValue != null)
									{
										list.Add(radialPointer.GaugeInputValue);
									}
								}
							}
							if (radialScale.ScaleRanges != null)
							{
								foreach (ScaleRange scaleRange in radialScale.ScaleRanges)
								{
									if (scaleRange.StartValue != null)
									{
										list.Add(scaleRange.StartValue);
									}
									if (scaleRange.EndValue != null)
									{
										list.Add(scaleRange.EndValue);
									}
								}
							}
						}
					}
				}
			}
			if (this.LinearGauges != null)
			{
				foreach (LinearGauge linearGauge in this.LinearGauges)
				{
					if (linearGauge.GaugeScales != null)
					{
						foreach (LinearScale linearScale in linearGauge.GaugeScales)
						{
							if (linearScale.MaximumValue != null)
							{
								list.Add(linearScale.MaximumValue);
							}
							if (linearScale.MinimumValue != null)
							{
								list.Add(linearScale.MinimumValue);
							}
							if (linearScale.GaugePointers != null)
							{
								foreach (LinearPointer linearPointer in linearScale.GaugePointers)
								{
									if (linearPointer.GaugeInputValue != null)
									{
										list.Add(linearPointer.GaugeInputValue);
									}
								}
							}
							if (linearScale.ScaleRanges != null)
							{
								foreach (ScaleRange scaleRange2 in linearScale.ScaleRanges)
								{
									if (scaleRange2.StartValue != null)
									{
										list.Add(scaleRange2.StartValue);
									}
									if (scaleRange2.EndValue != null)
									{
										list.Add(scaleRange2.EndValue);
									}
								}
							}
						}
					}
				}
			}
			NumericIndicatorCollection numericIndicators = this.NumericIndicators;
			if (this.StateIndicators != null)
			{
				foreach (StateIndicator stateIndicator in this.StateIndicators)
				{
					if (stateIndicator.GaugeInputValue != null)
					{
						list.Add(stateIndicator.GaugeInputValue);
					}
					if (stateIndicator.MinimumValue != null)
					{
						list.Add(stateIndicator.MinimumValue);
					}
					if (stateIndicator.MaximumValue != null)
					{
						list.Add(stateIndicator.MaximumValue);
					}
					if (stateIndicator.IndicatorStates != null)
					{
						foreach (IndicatorState indicatorState in stateIndicator.IndicatorStates)
						{
							if (indicatorState.StartValue != null)
							{
								list.Add(indicatorState.StartValue);
							}
							if (indicatorState.EndValue != null)
							{
								list.Add(indicatorState.EndValue);
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x00034208 File Offset: 0x00032408
		internal void ProcessCompiledInstances()
		{
			if (!this.RequiresCompilation)
			{
				return;
			}
			if (this.m_compilationState == GaugePanel.CompilationState.NotCompiled)
			{
				try
				{
					this.m_compilationState = GaugePanel.CompilationState.Compiling;
					GaugeMapperFactory.CreateGaugeMapperInstance(this, base.RenderingContext.OdpContext.ReportDefinition.DefaultFontFamily).RenderDataGaugePanel();
					this.m_compilationState = GaugePanel.CompilationState.Compiled;
				}
				catch (Exception ex)
				{
					this.m_compilationState = GaugePanel.CompilationState.NotCompiled;
					throw new RenderingObjectModelException(ex);
				}
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06000BC7 RID: 3015 RVA: 0x00034274 File Offset: 0x00032474
		private bool RequiresCompilation
		{
			get
			{
				return !this.GaugeMember.IsStatic || this.StateIndicators != null;
			}
		}

		// Token: 0x04000509 RID: 1289
		private GaugeMember m_gaugeMember;

		// Token: 0x0400050A RID: 1290
		private GaugeMember m_gaugeRowMember;

		// Token: 0x0400050B RID: 1291
		private GaugeRowCollection m_gaugeRowCollection;

		// Token: 0x0400050C RID: 1292
		private LinearGaugeCollection m_linearGauges;

		// Token: 0x0400050D RID: 1293
		private RadialGaugeCollection m_radialGauges;

		// Token: 0x0400050E RID: 1294
		private NumericIndicatorCollection m_numericIndicators;

		// Token: 0x0400050F RID: 1295
		private StateIndicatorCollection m_stateIndicators;

		// Token: 0x04000510 RID: 1296
		private GaugeImageCollection m_gaugeImages;

		// Token: 0x04000511 RID: 1297
		private GaugeLabelCollection m_gaugeLabels;

		// Token: 0x04000512 RID: 1298
		private ReportEnumProperty<GaugeAntiAliasings> m_antiAliasing;

		// Token: 0x04000513 RID: 1299
		private ReportBoolProperty m_autoLayout;

		// Token: 0x04000514 RID: 1300
		private BackFrame m_backFrame;

		// Token: 0x04000515 RID: 1301
		private ReportDoubleProperty m_shadowIntensity;

		// Token: 0x04000516 RID: 1302
		private ReportEnumProperty<TextAntiAliasingQualities> m_textAntiAliasingQuality;

		// Token: 0x04000517 RID: 1303
		private TopImage m_topImage;

		// Token: 0x04000518 RID: 1304
		private GaugePanel.CompilationState m_compilationState;

		// Token: 0x0200092E RID: 2350
		private enum CompilationState
		{
			// Token: 0x04003F9B RID: 16283
			NotCompiled,
			// Token: 0x04003F9C RID: 16284
			Compiling,
			// Token: 0x04003F9D RID: 16285
			Compiled
		}
	}
}
