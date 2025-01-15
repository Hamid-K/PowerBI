using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000151 RID: 337
	public class StateIndicator : GaugePanelItem
	{
		// Token: 0x060009DA RID: 2522 RVA: 0x0001E6A9 File Offset: 0x0001C8A9
		public StateIndicator()
		{
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0001E6B1 File Offset: 0x0001C8B1
		internal StateIndicator(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x060009DC RID: 2524 RVA: 0x0001E6BA File Offset: 0x0001C8BA
		// (set) Token: 0x060009DD RID: 2525 RVA: 0x0001E6CE File Offset: 0x0001C8CE
		public GaugeInputValue GaugeInputValue
		{
			get
			{
				return (GaugeInputValue)base.PropertyStore.GetObject(11);
			}
			set
			{
				base.PropertyStore.SetObject(11, value);
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x0001E6DE File Offset: 0x0001C8DE
		// (set) Token: 0x060009DF RID: 2527 RVA: 0x0001E6ED File Offset: 0x0001C8ED
		[ReportExpressionDefaultValue(typeof(GaugeTransformationType), GaugeTransformationType.Percentage)]
		public ReportExpression<GaugeTransformationType> TransformationType
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<GaugeTransformationType>>(12);
			}
			set
			{
				base.PropertyStore.SetObject(12, value);
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x0001E702 File Offset: 0x0001C902
		// (set) Token: 0x060009E1 RID: 2529 RVA: 0x0001E716 File Offset: 0x0001C916
		public string TransformationScope
		{
			get
			{
				return (string)base.PropertyStore.GetObject(13);
			}
			set
			{
				base.PropertyStore.SetObject(13, value);
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x0001E726 File Offset: 0x0001C926
		// (set) Token: 0x060009E3 RID: 2531 RVA: 0x0001E73A File Offset: 0x0001C93A
		public GaugeInputValue MinimumValue
		{
			get
			{
				return (GaugeInputValue)base.PropertyStore.GetObject(14);
			}
			set
			{
				base.PropertyStore.SetObject(14, value);
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x0001E74A File Offset: 0x0001C94A
		// (set) Token: 0x060009E5 RID: 2533 RVA: 0x0001E75E File Offset: 0x0001C95E
		public GaugeInputValue MaximumValue
		{
			get
			{
				return (GaugeInputValue)base.PropertyStore.GetObject(15);
			}
			set
			{
				base.PropertyStore.SetObject(15, value);
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x0001E76E File Offset: 0x0001C96E
		// (set) Token: 0x060009E7 RID: 2535 RVA: 0x0001E77D File Offset: 0x0001C97D
		[ReportExpressionDefaultValue(typeof(GaugeStateIndicatorStyles), GaugeStateIndicatorStyles.Circle)]
		public ReportExpression<GaugeStateIndicatorStyles> IndicatorStyle
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<GaugeStateIndicatorStyles>>(16);
			}
			set
			{
				base.PropertyStore.SetObject(16, value);
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x0001E792 File Offset: 0x0001C992
		// (set) Token: 0x060009E9 RID: 2537 RVA: 0x0001E7A6 File Offset: 0x0001C9A6
		public IndicatorImage IndicatorImage
		{
			get
			{
				return (IndicatorImage)base.PropertyStore.GetObject(17);
			}
			set
			{
				base.PropertyStore.SetObject(17, value);
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x0001E7B6 File Offset: 0x0001C9B6
		// (set) Token: 0x060009EB RID: 2539 RVA: 0x0001E7C5 File Offset: 0x0001C9C5
		public ReportExpression<double> ScaleFactor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(18);
			}
			set
			{
				base.PropertyStore.SetObject(18, value);
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x060009EC RID: 2540 RVA: 0x0001E7DA File Offset: 0x0001C9DA
		// (set) Token: 0x060009ED RID: 2541 RVA: 0x0001E7EE File Offset: 0x0001C9EE
		[XmlElement(typeof(RdlCollection<IndicatorState>))]
		public IList<IndicatorState> IndicatorStates
		{
			get
			{
				return (IList<IndicatorState>)base.PropertyStore.GetObject(19);
			}
			set
			{
				base.PropertyStore.SetObject(19, value);
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x060009EE RID: 2542 RVA: 0x0001E7FE File Offset: 0x0001C9FE
		// (set) Token: 0x060009EF RID: 2543 RVA: 0x0001E80D File Offset: 0x0001CA0D
		[ReportExpressionDefaultValue(typeof(ResizeModes), ResizeModes.AutoFit)]
		public ReportExpression<ResizeModes> ResizeMode
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ResizeModes>>(20);
			}
			set
			{
				base.PropertyStore.SetObject(20, value);
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x0001E822 File Offset: 0x0001CA22
		// (set) Token: 0x060009F1 RID: 2545 RVA: 0x0001E831 File Offset: 0x0001CA31
		public ReportExpression<double> Angle
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(21);
			}
			set
			{
				base.PropertyStore.SetObject(21, value);
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x0001E846 File Offset: 0x0001CA46
		// (set) Token: 0x060009F3 RID: 2547 RVA: 0x0001E85A File Offset: 0x0001CA5A
		public string StateDataElementName
		{
			get
			{
				return (string)base.PropertyStore.GetObject(22);
			}
			set
			{
				base.PropertyStore.SetObject(22, value);
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x0001E86A File Offset: 0x0001CA6A
		// (set) Token: 0x060009F5 RID: 2549 RVA: 0x0001E879 File Offset: 0x0001CA79
		[DefaultValue(DataElementOutputTypes.Output)]
		[ValidEnumValues("GaugeInputValueDataElementOutputTypes")]
		public DataElementOutputTypes StateDataElementOutput
		{
			get
			{
				return (DataElementOutputTypes)base.PropertyStore.GetInteger(23);
			}
			set
			{
				((EnumProperty)DefinitionStore<StateIndicator, StateIndicator.Definition.Properties>.GetProperty(23)).Validate(this, (int)value);
				base.PropertyStore.SetInteger(23, (int)value);
			}
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0001E89C File Offset: 0x0001CA9C
		public override void Initialize()
		{
			base.Initialize();
			this.IndicatorStyle = GaugeStateIndicatorStyles.Circle;
			this.ScaleFactor = 1.0;
			this.IndicatorStates = new RdlCollection<IndicatorState>();
			this.ResizeMode = ResizeModes.AutoFit;
			this.StateDataElementOutput = DataElementOutputTypes.Output;
		}

		// Token: 0x02000382 RID: 898
		internal new class Definition : DefinitionStore<StateIndicator, StateIndicator.Definition.Properties>
		{
			// Token: 0x06001825 RID: 6181 RVA: 0x0003B453 File Offset: 0x00039653
			private Definition()
			{
			}

			// Token: 0x0200049B RID: 1179
			internal enum Properties
			{
				// Token: 0x04000C5C RID: 3164
				Name,
				// Token: 0x04000C5D RID: 3165
				Style,
				// Token: 0x04000C5E RID: 3166
				Top,
				// Token: 0x04000C5F RID: 3167
				Left,
				// Token: 0x04000C60 RID: 3168
				Height,
				// Token: 0x04000C61 RID: 3169
				Width,
				// Token: 0x04000C62 RID: 3170
				ZIndex,
				// Token: 0x04000C63 RID: 3171
				Hidden,
				// Token: 0x04000C64 RID: 3172
				ToolTip,
				// Token: 0x04000C65 RID: 3173
				ActionInfo,
				// Token: 0x04000C66 RID: 3174
				ParentItem,
				// Token: 0x04000C67 RID: 3175
				GaugeInputValue,
				// Token: 0x04000C68 RID: 3176
				TransformationType,
				// Token: 0x04000C69 RID: 3177
				TransformationScope,
				// Token: 0x04000C6A RID: 3178
				MinimumValue,
				// Token: 0x04000C6B RID: 3179
				MaximumValue,
				// Token: 0x04000C6C RID: 3180
				IndicatorStyle,
				// Token: 0x04000C6D RID: 3181
				IndicatorImage,
				// Token: 0x04000C6E RID: 3182
				ScaleFactor,
				// Token: 0x04000C6F RID: 3183
				IndicatorStates,
				// Token: 0x04000C70 RID: 3184
				ResizeMode,
				// Token: 0x04000C71 RID: 3185
				Angle,
				// Token: 0x04000C72 RID: 3186
				StateDataElementName,
				// Token: 0x04000C73 RID: 3187
				StateDataElementOutput,
				// Token: 0x04000C74 RID: 3188
				PropertyCount
			}
		}
	}
}
