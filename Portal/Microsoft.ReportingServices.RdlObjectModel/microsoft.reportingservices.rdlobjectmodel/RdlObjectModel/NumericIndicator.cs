using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000150 RID: 336
	public class NumericIndicator : GaugePanelItem
	{
		// Token: 0x1700034B RID: 843
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x0001E2B6 File Offset: 0x0001C4B6
		// (set) Token: 0x060009AC RID: 2476 RVA: 0x0001E2C5 File Offset: 0x0001C4C5
		[ReportExpressionDefaultValue(typeof(ResizeModes), ResizeModes.AutoFit)]
		public ReportExpression<ResizeModes> ResizeMode
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ResizeModes>>(11);
			}
			set
			{
				base.PropertyStore.SetObject(11, value);
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x0001E2DA File Offset: 0x0001C4DA
		// (set) Token: 0x060009AE RID: 2478 RVA: 0x0001E2EE File Offset: 0x0001C4EE
		public GaugeInputValue GaugeInputValue
		{
			get
			{
				return (GaugeInputValue)base.PropertyStore.GetObject(12);
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.PropertyStore.SetObject(12, value);
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x0001E30C File Offset: 0x0001C50C
		// (set) Token: 0x060009B0 RID: 2480 RVA: 0x0001E31B File Offset: 0x0001C51B
		[ReportExpressionDefaultValueConstant(typeof(ReportColor), "DefaultDecimalDigitColor")]
		public ReportExpression<ReportColor> DecimalDigitColor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(13);
			}
			set
			{
				base.PropertyStore.SetObject(13, value);
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x0001E330 File Offset: 0x0001C530
		// (set) Token: 0x060009B2 RID: 2482 RVA: 0x0001E33F File Offset: 0x0001C53F
		[ReportExpressionDefaultValue(typeof(int), 1)]
		public ReportExpression<int> DecimalDigits
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<int>>(14);
			}
			set
			{
				base.PropertyStore.SetObject(14, value);
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x0001E354 File Offset: 0x0001C554
		// (set) Token: 0x060009B4 RID: 2484 RVA: 0x0001E363 File Offset: 0x0001C563
		[ReportExpressionDefaultValueConstant(typeof(ReportColor), "DefaultDigitColor")]
		public ReportExpression<ReportColor> DigitColor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(15);
			}
			set
			{
				base.PropertyStore.SetObject(15, value);
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x0001E378 File Offset: 0x0001C578
		// (set) Token: 0x060009B6 RID: 2486 RVA: 0x0001E387 File Offset: 0x0001C587
		[ReportExpressionDefaultValue(typeof(int), 6)]
		public ReportExpression<int> Digits
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<int>>(16);
			}
			set
			{
				base.PropertyStore.SetObject(16, value);
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x0001E39C File Offset: 0x0001C59C
		// (set) Token: 0x060009B8 RID: 2488 RVA: 0x0001E3AB File Offset: 0x0001C5AB
		[ReportExpressionDefaultValue(typeof(NumericIndicatorStyles), NumericIndicatorStyles.Mechanical)]
		public ReportExpression<NumericIndicatorStyles> IndicatorStyle
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<NumericIndicatorStyles>>(17);
			}
			set
			{
				base.PropertyStore.SetObject(17, value);
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x060009B9 RID: 2489 RVA: 0x0001E3C0 File Offset: 0x0001C5C0
		// (set) Token: 0x060009BA RID: 2490 RVA: 0x0001E3CF File Offset: 0x0001C5CF
		[ReportExpressionDefaultValue(typeof(ReportColor))]
		public ReportExpression<ReportColor> LedDimColor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(18);
			}
			set
			{
				base.PropertyStore.SetObject(18, value);
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x060009BB RID: 2491 RVA: 0x0001E3E4 File Offset: 0x0001C5E4
		// (set) Token: 0x060009BC RID: 2492 RVA: 0x0001E3F8 File Offset: 0x0001C5F8
		public GaugeInputValue MaximumValue
		{
			get
			{
				return (GaugeInputValue)base.PropertyStore.GetObject(19);
			}
			set
			{
				base.PropertyStore.SetObject(19, value);
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x060009BD RID: 2493 RVA: 0x0001E408 File Offset: 0x0001C608
		// (set) Token: 0x060009BE RID: 2494 RVA: 0x0001E41C File Offset: 0x0001C61C
		public GaugeInputValue MinimumValue
		{
			get
			{
				return (GaugeInputValue)base.PropertyStore.GetObject(20);
			}
			set
			{
				base.PropertyStore.SetObject(20, value);
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x0001E42C File Offset: 0x0001C62C
		// (set) Token: 0x060009C0 RID: 2496 RVA: 0x0001E43B File Offset: 0x0001C63B
		[ReportExpressionDefaultValue(typeof(double), 1.0)]
		public ReportExpression<double> Multiplier
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

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x0001E450 File Offset: 0x0001C650
		// (set) Token: 0x060009C2 RID: 2498 RVA: 0x0001E45F File Offset: 0x0001C65F
		[ReportExpressionDefaultValueConstant("DefaultGaugeIndicatorNonNumericString")]
		public ReportExpression NonNumericString
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(22);
			}
			set
			{
				base.PropertyStore.SetObject(22, value);
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x060009C3 RID: 2499 RVA: 0x0001E474 File Offset: 0x0001C674
		// (set) Token: 0x060009C4 RID: 2500 RVA: 0x0001E483 File Offset: 0x0001C683
		[ReportExpressionDefaultValueConstant("DefaultGaugeIndicatorOutOfRangeString")]
		public ReportExpression OutOfRangeString
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(23);
			}
			set
			{
				base.PropertyStore.SetObject(23, value);
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x0001E498 File Offset: 0x0001C698
		// (set) Token: 0x060009C6 RID: 2502 RVA: 0x0001E4AC File Offset: 0x0001C6AC
		[XmlElement(typeof(RdlCollection<NumericIndicatorRange>))]
		public IList<NumericIndicatorRange> NumericIndicatorRanges
		{
			get
			{
				return (IList<NumericIndicatorRange>)base.PropertyStore.GetObject(24);
			}
			set
			{
				base.PropertyStore.SetObject(24, value);
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x0001E4BC File Offset: 0x0001C6BC
		// (set) Token: 0x060009C8 RID: 2504 RVA: 0x0001E4CB File Offset: 0x0001C6CB
		[ReportExpressionDefaultValueConstant(typeof(ReportColor), "DefaultSeparatorColor")]
		public ReportExpression<ReportColor> SeparatorColor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(25);
			}
			set
			{
				base.PropertyStore.SetObject(25, value);
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x0001E4E0 File Offset: 0x0001C6E0
		// (set) Token: 0x060009CA RID: 2506 RVA: 0x0001E4EF File Offset: 0x0001C6EF
		[ReportExpressionDefaultValue(typeof(double), 1.0)]
		public ReportExpression<double> SeparatorWidth
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(26);
			}
			set
			{
				base.PropertyStore.SetObject(26, value);
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x0001E504 File Offset: 0x0001C704
		// (set) Token: 0x060009CC RID: 2508 RVA: 0x0001E513 File Offset: 0x0001C713
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> ShowDecimalPoint
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(27);
			}
			set
			{
				base.PropertyStore.SetObject(27, value);
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x0001E528 File Offset: 0x0001C728
		// (set) Token: 0x060009CE RID: 2510 RVA: 0x0001E537 File Offset: 0x0001C737
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> ShowLeadingZeros
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(28);
			}
			set
			{
				base.PropertyStore.SetObject(28, value);
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x060009CF RID: 2511 RVA: 0x0001E54C File Offset: 0x0001C74C
		// (set) Token: 0x060009D0 RID: 2512 RVA: 0x0001E55B File Offset: 0x0001C75B
		[ReportExpressionDefaultValue(typeof(GaugeShowSigns), GaugeShowSigns.NegativeOnly)]
		public ReportExpression<GaugeShowSigns> ShowSign
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<GaugeShowSigns>>(29);
			}
			set
			{
				base.PropertyStore.SetObject(29, value);
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x060009D1 RID: 2513 RVA: 0x0001E570 File Offset: 0x0001C770
		// (set) Token: 0x060009D2 RID: 2514 RVA: 0x0001E57F File Offset: 0x0001C77F
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> SnappingEnabled
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(30);
			}
			set
			{
				base.PropertyStore.SetObject(30, value);
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x0001E594 File Offset: 0x0001C794
		// (set) Token: 0x060009D4 RID: 2516 RVA: 0x0001E5A3 File Offset: 0x0001C7A3
		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> SnappingInterval
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(31);
			}
			set
			{
				base.PropertyStore.SetObject(31, value);
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x060009D5 RID: 2517 RVA: 0x0001E5B8 File Offset: 0x0001C7B8
		// (set) Token: 0x060009D6 RID: 2518 RVA: 0x0001E5C7 File Offset: 0x0001C7C7
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> UseFontPercent
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(32);
			}
			set
			{
				base.PropertyStore.SetObject(32, value);
			}
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0001E5DC File Offset: 0x0001C7DC
		public NumericIndicator()
		{
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0001E5E4 File Offset: 0x0001C7E4
		internal NumericIndicator(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0001E5F0 File Offset: 0x0001C7F0
		public override void Initialize()
		{
			base.Initialize();
			this.GaugeInputValue = new GaugeInputValue();
			this.NumericIndicatorRanges = new RdlCollection<NumericIndicatorRange>();
			this.DecimalDigitColor = Constants.DefaultDecimalDigitColor;
			this.DecimalDigits = 1;
			this.DigitColor = Constants.DefaultDigitColor;
			this.Digits = 6;
			this.NonNumericString = "-";
			this.OutOfRangeString = "#Error";
			this.SeparatorColor = Constants.DefaultSeparatorColor;
			this.Multiplier = 1.0;
			this.SeparatorWidth = 1.0;
		}

		// Token: 0x02000381 RID: 897
		internal new class Definition : DefinitionStore<NumericIndicator, NumericIndicator.Definition.Properties>
		{
			// Token: 0x06001824 RID: 6180 RVA: 0x0003B44B File Offset: 0x0003964B
			private Definition()
			{
			}

			// Token: 0x0200049A RID: 1178
			internal enum Properties
			{
				// Token: 0x04000C39 RID: 3129
				Name,
				// Token: 0x04000C3A RID: 3130
				Style,
				// Token: 0x04000C3B RID: 3131
				Top,
				// Token: 0x04000C3C RID: 3132
				Left,
				// Token: 0x04000C3D RID: 3133
				Height,
				// Token: 0x04000C3E RID: 3134
				Width,
				// Token: 0x04000C3F RID: 3135
				ZIndex,
				// Token: 0x04000C40 RID: 3136
				Hidden,
				// Token: 0x04000C41 RID: 3137
				ToolTip,
				// Token: 0x04000C42 RID: 3138
				ActionInfo,
				// Token: 0x04000C43 RID: 3139
				ParentItem,
				// Token: 0x04000C44 RID: 3140
				ResizeMode,
				// Token: 0x04000C45 RID: 3141
				GaugeInputValue,
				// Token: 0x04000C46 RID: 3142
				DecimalDigitColor,
				// Token: 0x04000C47 RID: 3143
				DecimalDigits,
				// Token: 0x04000C48 RID: 3144
				DigitColor,
				// Token: 0x04000C49 RID: 3145
				Digits,
				// Token: 0x04000C4A RID: 3146
				IndicatorStyle,
				// Token: 0x04000C4B RID: 3147
				LedDimColor,
				// Token: 0x04000C4C RID: 3148
				MaximumValue,
				// Token: 0x04000C4D RID: 3149
				MinimumValue,
				// Token: 0x04000C4E RID: 3150
				Multiplier,
				// Token: 0x04000C4F RID: 3151
				NonNumericString,
				// Token: 0x04000C50 RID: 3152
				OutOfRangeString,
				// Token: 0x04000C51 RID: 3153
				NumericIndicatorRanges,
				// Token: 0x04000C52 RID: 3154
				SeparatorColor,
				// Token: 0x04000C53 RID: 3155
				SeparatorWidth,
				// Token: 0x04000C54 RID: 3156
				ShowDecimalPoint,
				// Token: 0x04000C55 RID: 3157
				ShowLeadingZeros,
				// Token: 0x04000C56 RID: 3158
				ShowSign,
				// Token: 0x04000C57 RID: 3159
				SnappingEnabled,
				// Token: 0x04000C58 RID: 3160
				SnappingInterval,
				// Token: 0x04000C59 RID: 3161
				UseFontPercent,
				// Token: 0x04000C5A RID: 3162
				PropertyCount
			}
		}
	}
}
