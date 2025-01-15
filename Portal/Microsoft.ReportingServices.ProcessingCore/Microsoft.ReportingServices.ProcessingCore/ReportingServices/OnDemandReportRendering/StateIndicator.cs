using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000135 RID: 309
	public sealed class StateIndicator : GaugePanelItem
	{
		// Token: 0x06000D77 RID: 3447 RVA: 0x00039970 File Offset: 0x00037B70
		internal StateIndicator(StateIndicator defObject, GaugePanel gaugePanel)
			: base(defObject, gaugePanel)
		{
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06000D78 RID: 3448 RVA: 0x0003997A File Offset: 0x00037B7A
		public GaugeInputValue GaugeInputValue
		{
			get
			{
				if (this.m_gaugeInputValue == null && this.StateIndicatorDef.GaugeInputValue != null)
				{
					this.m_gaugeInputValue = new GaugeInputValue(this.StateIndicatorDef.GaugeInputValue, this.m_gaugePanel);
				}
				return this.m_gaugeInputValue;
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06000D79 RID: 3449 RVA: 0x000399B4 File Offset: 0x00037BB4
		public ReportEnumProperty<GaugeTransformationType> TransformationType
		{
			get
			{
				if (this.m_transformationType == null && this.StateIndicatorDef.TransformationType != null)
				{
					this.m_transformationType = new ReportEnumProperty<GaugeTransformationType>(this.StateIndicatorDef.TransformationType.IsExpression, this.StateIndicatorDef.TransformationType.OriginalText, EnumTranslator.TranslateGaugeTransformationType(this.StateIndicatorDef.TransformationType.StringValue, null));
				}
				return this.m_transformationType;
			}
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06000D7A RID: 3450 RVA: 0x00039A1D File Offset: 0x00037C1D
		public string TransformationScope
		{
			get
			{
				return this.StateIndicatorDef.TransformationScope;
			}
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06000D7B RID: 3451 RVA: 0x00039A2A File Offset: 0x00037C2A
		public GaugeInputValue MaximumValue
		{
			get
			{
				if (this.m_maximumValue == null && this.StateIndicatorDef.MaximumValue != null)
				{
					this.m_maximumValue = new GaugeInputValue(this.StateIndicatorDef.MaximumValue, this.m_gaugePanel);
				}
				return this.m_maximumValue;
			}
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x00039A63 File Offset: 0x00037C63
		public GaugeInputValue MinimumValue
		{
			get
			{
				if (this.m_minimumValue == null && this.StateIndicatorDef.MinimumValue != null)
				{
					this.m_minimumValue = new GaugeInputValue(this.StateIndicatorDef.MinimumValue, this.m_gaugePanel);
				}
				return this.m_minimumValue;
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06000D7D RID: 3453 RVA: 0x00039A9C File Offset: 0x00037C9C
		public ReportEnumProperty<GaugeStateIndicatorStyles> IndicatorStyle
		{
			get
			{
				if (this.m_indicatorStyle == null && this.StateIndicatorDef.IndicatorStyle != null)
				{
					this.m_indicatorStyle = new ReportEnumProperty<GaugeStateIndicatorStyles>(this.StateIndicatorDef.IndicatorStyle.IsExpression, this.StateIndicatorDef.IndicatorStyle.OriginalText, EnumTranslator.TranslateGaugeStateIndicatorStyles(this.StateIndicatorDef.IndicatorStyle.StringValue, null));
				}
				return this.m_indicatorStyle;
			}
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06000D7E RID: 3454 RVA: 0x00039B05 File Offset: 0x00037D05
		public IndicatorImage IndicatorImage
		{
			get
			{
				if (this.m_indicatorImage == null && this.StateIndicatorDef.IndicatorImage != null)
				{
					this.m_indicatorImage = new IndicatorImage(this.StateIndicatorDef.IndicatorImage, this.m_gaugePanel);
				}
				return this.m_indicatorImage;
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06000D7F RID: 3455 RVA: 0x00039B3E File Offset: 0x00037D3E
		public ReportDoubleProperty ScaleFactor
		{
			get
			{
				if (this.m_scaleFactor == null && this.StateIndicatorDef.ScaleFactor != null)
				{
					this.m_scaleFactor = new ReportDoubleProperty(this.StateIndicatorDef.ScaleFactor);
				}
				return this.m_scaleFactor;
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06000D80 RID: 3456 RVA: 0x00039B71 File Offset: 0x00037D71
		public IndicatorStateCollection IndicatorStates
		{
			get
			{
				if (this.m_indicatorStates == null && this.StateIndicatorDef.IndicatorStates != null)
				{
					this.m_indicatorStates = new IndicatorStateCollection(this, this.m_gaugePanel);
				}
				return this.m_indicatorStates;
			}
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06000D81 RID: 3457 RVA: 0x00039BA0 File Offset: 0x00037DA0
		public ReportEnumProperty<GaugeResizeModes> ResizeMode
		{
			get
			{
				if (this.m_resizeMode == null && this.StateIndicatorDef.ResizeMode != null)
				{
					this.m_resizeMode = new ReportEnumProperty<GaugeResizeModes>(this.StateIndicatorDef.ResizeMode.IsExpression, this.StateIndicatorDef.ResizeMode.OriginalText, EnumTranslator.TranslateGaugeResizeModes(this.StateIndicatorDef.ResizeMode.StringValue, null));
				}
				return this.m_resizeMode;
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06000D82 RID: 3458 RVA: 0x00039C09 File Offset: 0x00037E09
		public ReportDoubleProperty Angle
		{
			get
			{
				if (this.m_angle == null && this.StateIndicatorDef.Angle != null)
				{
					this.m_angle = new ReportDoubleProperty(this.StateIndicatorDef.Angle);
				}
				return this.m_angle;
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06000D83 RID: 3459 RVA: 0x00039C3C File Offset: 0x00037E3C
		public string StateDataElementName
		{
			get
			{
				return this.StateIndicatorDef.StateDataElementName;
			}
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06000D84 RID: 3460 RVA: 0x00039C49 File Offset: 0x00037E49
		public DataElementOutputTypes StateDataElementOutput
		{
			get
			{
				return this.StateIndicatorDef.StateDataElementOutput;
			}
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06000D85 RID: 3461 RVA: 0x00039C56 File Offset: 0x00037E56
		// (set) Token: 0x06000D86 RID: 3462 RVA: 0x00039C69 File Offset: 0x00037E69
		public string CompiledStateName
		{
			get
			{
				this.m_gaugePanel.ProcessCompiledInstances();
				return this.m_compiledStateName;
			}
			set
			{
				this.m_compiledStateName = value;
			}
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06000D87 RID: 3463 RVA: 0x00039C72 File Offset: 0x00037E72
		internal StateIndicator StateIndicatorDef
		{
			get
			{
				return (StateIndicator)this.m_defObject;
			}
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06000D88 RID: 3464 RVA: 0x00039C7F File Offset: 0x00037E7F
		public new StateIndicatorInstance Instance
		{
			get
			{
				return (StateIndicatorInstance)this.GetInstance();
			}
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x00039C8C File Offset: 0x00037E8C
		internal override BaseInstance GetInstance()
		{
			if (this.m_gaugePanel.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (this.m_instance == null)
			{
				this.m_instance = new StateIndicatorInstance(this);
			}
			return this.m_instance;
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x00039CBC File Offset: 0x00037EBC
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_gaugeInputValue != null)
			{
				this.m_gaugeInputValue.SetNewContext();
			}
			if (this.m_indicatorImage != null)
			{
				this.m_indicatorImage.SetNewContext();
			}
			if (this.m_indicatorStates != null)
			{
				this.m_indicatorStates.SetNewContext();
			}
			if (this.m_maximumValue != null)
			{
				this.m_maximumValue.SetNewContext();
			}
			if (this.m_minimumValue != null)
			{
				this.m_minimumValue.SetNewContext();
			}
		}

		// Token: 0x04000628 RID: 1576
		private GaugeInputValue m_gaugeInputValue;

		// Token: 0x04000629 RID: 1577
		private ReportEnumProperty<GaugeStateIndicatorStyles> m_indicatorStyle;

		// Token: 0x0400062A RID: 1578
		private IndicatorImage m_indicatorImage;

		// Token: 0x0400062B RID: 1579
		private ReportDoubleProperty m_scaleFactor;

		// Token: 0x0400062C RID: 1580
		private IndicatorStateCollection m_indicatorStates;

		// Token: 0x0400062D RID: 1581
		private ReportEnumProperty<GaugeResizeModes> m_resizeMode;

		// Token: 0x0400062E RID: 1582
		private ReportDoubleProperty m_angle;

		// Token: 0x0400062F RID: 1583
		private ReportEnumProperty<GaugeTransformationType> m_transformationType;

		// Token: 0x04000630 RID: 1584
		private GaugeInputValue m_maximumValue;

		// Token: 0x04000631 RID: 1585
		private GaugeInputValue m_minimumValue;

		// Token: 0x04000632 RID: 1586
		private string m_compiledStateName;
	}
}
