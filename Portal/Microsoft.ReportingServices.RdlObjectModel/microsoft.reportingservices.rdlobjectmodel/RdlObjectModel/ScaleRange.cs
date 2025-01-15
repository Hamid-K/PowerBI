using System;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000159 RID: 345
	public class ScaleRange : ReportObject, INamedObject
	{
		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000A7A RID: 2682 RVA: 0x0001F254 File Offset: 0x0001D454
		// (set) Token: 0x06000A7B RID: 2683 RVA: 0x0001F267 File Offset: 0x0001D467
		[XmlAttribute(typeof(string))]
		public string Name
		{
			get
			{
				return (string)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000A7C RID: 2684 RVA: 0x0001F276 File Offset: 0x0001D476
		// (set) Token: 0x06000A7D RID: 2685 RVA: 0x0001F289 File Offset: 0x0001D489
		public Style Style
		{
			get
			{
				return (Style)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000A7E RID: 2686 RVA: 0x0001F298 File Offset: 0x0001D498
		// (set) Token: 0x06000A7F RID: 2687 RVA: 0x0001F2A6 File Offset: 0x0001D4A6
		[ReportExpressionDefaultValue(typeof(GaugeBackgroundGradients), GaugeBackgroundGradients.StartToEnd)]
		public ReportExpression<GaugeBackgroundGradients> BackgroundGradientType
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<GaugeBackgroundGradients>>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x0001F2BA File Offset: 0x0001D4BA
		// (set) Token: 0x06000A81 RID: 2689 RVA: 0x0001F2C8 File Offset: 0x0001D4C8
		public ReportExpression<double> DistanceFromScale
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x0001F2DC File Offset: 0x0001D4DC
		// (set) Token: 0x06000A83 RID: 2691 RVA: 0x0001F2EF File Offset: 0x0001D4EF
		public GaugeInputValue StartValue
		{
			get
			{
				return (GaugeInputValue)base.PropertyStore.GetObject(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x0001F2FE File Offset: 0x0001D4FE
		// (set) Token: 0x06000A85 RID: 2693 RVA: 0x0001F311 File Offset: 0x0001D511
		public GaugeInputValue EndValue
		{
			get
			{
				return (GaugeInputValue)base.PropertyStore.GetObject(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x0001F320 File Offset: 0x0001D520
		// (set) Token: 0x06000A87 RID: 2695 RVA: 0x0001F32E File Offset: 0x0001D52E
		public ReportExpression<double> StartWidth
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(6);
			}
			set
			{
				base.PropertyStore.SetObject(6, value);
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000A88 RID: 2696 RVA: 0x0001F342 File Offset: 0x0001D542
		// (set) Token: 0x06000A89 RID: 2697 RVA: 0x0001F350 File Offset: 0x0001D550
		public ReportExpression<double> EndWidth
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(7);
			}
			set
			{
				base.PropertyStore.SetObject(7, value);
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000A8A RID: 2698 RVA: 0x0001F364 File Offset: 0x0001D564
		// (set) Token: 0x06000A8B RID: 2699 RVA: 0x0001F372 File Offset: 0x0001D572
		[ReportExpressionDefaultValue(typeof(ReportColor))]
		public ReportExpression<ReportColor> InRangeBarPointerColor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(8);
			}
			set
			{
				base.PropertyStore.SetObject(8, value);
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000A8C RID: 2700 RVA: 0x0001F386 File Offset: 0x0001D586
		// (set) Token: 0x06000A8D RID: 2701 RVA: 0x0001F395 File Offset: 0x0001D595
		[ReportExpressionDefaultValue(typeof(ReportColor))]
		public ReportExpression<ReportColor> InRangeLabelColor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(9);
			}
			set
			{
				base.PropertyStore.SetObject(9, value);
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000A8E RID: 2702 RVA: 0x0001F3AA File Offset: 0x0001D5AA
		// (set) Token: 0x06000A8F RID: 2703 RVA: 0x0001F3B9 File Offset: 0x0001D5B9
		[ReportExpressionDefaultValue(typeof(ReportColor))]
		public ReportExpression<ReportColor> InRangeTickMarksColor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(10);
			}
			set
			{
				base.PropertyStore.SetObject(10, value);
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000A90 RID: 2704 RVA: 0x0001F3CE File Offset: 0x0001D5CE
		// (set) Token: 0x06000A91 RID: 2705 RVA: 0x0001F3DD File Offset: 0x0001D5DD
		public ReportExpression<Placements> Placement
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<Placements>>(11);
			}
			set
			{
				base.PropertyStore.SetObject(11, value);
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000A92 RID: 2706 RVA: 0x0001F3F2 File Offset: 0x0001D5F2
		// (set) Token: 0x06000A93 RID: 2707 RVA: 0x0001F401 File Offset: 0x0001D601
		[ReportExpressionDefaultValue]
		public ReportExpression ToolTip
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(12);
			}
			set
			{
				base.PropertyStore.SetObject(12, value);
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000A94 RID: 2708 RVA: 0x0001F416 File Offset: 0x0001D616
		// (set) Token: 0x06000A95 RID: 2709 RVA: 0x0001F42A File Offset: 0x0001D62A
		public ActionInfo ActionInfo
		{
			get
			{
				return (ActionInfo)base.PropertyStore.GetObject(13);
			}
			set
			{
				base.PropertyStore.SetObject(13, value);
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000A96 RID: 2710 RVA: 0x0001F43A File Offset: 0x0001D63A
		// (set) Token: 0x06000A97 RID: 2711 RVA: 0x0001F449 File Offset: 0x0001D649
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> Hidden
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(14);
			}
			set
			{
				base.PropertyStore.SetObject(14, value);
			}
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0001F45E File Offset: 0x0001D65E
		public ScaleRange()
		{
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x0001F466 File Offset: 0x0001D666
		internal ScaleRange(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0200038A RID: 906
		internal class Definition : DefinitionStore<ScaleRange, ScaleRange.Definition.Properties>
		{
			// Token: 0x0600182D RID: 6189 RVA: 0x0003B493 File Offset: 0x00039693
			private Definition()
			{
			}

			// Token: 0x020004A3 RID: 1187
			internal enum Properties
			{
				// Token: 0x04000D08 RID: 3336
				Name,
				// Token: 0x04000D09 RID: 3337
				Style,
				// Token: 0x04000D0A RID: 3338
				BackgroundGradientType,
				// Token: 0x04000D0B RID: 3339
				DistanceFromScale,
				// Token: 0x04000D0C RID: 3340
				StartValue,
				// Token: 0x04000D0D RID: 3341
				EndValue,
				// Token: 0x04000D0E RID: 3342
				StartWidth,
				// Token: 0x04000D0F RID: 3343
				EndWidth,
				// Token: 0x04000D10 RID: 3344
				InRangeBarPointerColor,
				// Token: 0x04000D11 RID: 3345
				InRangeLabelColor,
				// Token: 0x04000D12 RID: 3346
				InRangeTickMarksColor,
				// Token: 0x04000D13 RID: 3347
				Placement,
				// Token: 0x04000D14 RID: 3348
				ToolTip,
				// Token: 0x04000D15 RID: 3349
				ActionInfo,
				// Token: 0x04000D16 RID: 3350
				Hidden,
				// Token: 0x04000D17 RID: 3351
				PropertyCount
			}
		}
	}
}
