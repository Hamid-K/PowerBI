using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200009B RID: 155
	public class ChartSmartLabel : ReportObject
	{
		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x0001A455 File Offset: 0x00018655
		// (set) Token: 0x060006A0 RID: 1696 RVA: 0x0001A463 File Offset: 0x00018663
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> Disabled
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x0001A477 File Offset: 0x00018677
		// (set) Token: 0x060006A2 RID: 1698 RVA: 0x0001A485 File Offset: 0x00018685
		[ReportExpressionDefaultValue(typeof(ChartAllowOutSidePlotAreaTypes), ChartAllowOutSidePlotAreaTypes.Partial)]
		public ReportExpression<ChartAllowOutSidePlotAreaTypes> AllowOutSidePlotArea
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartAllowOutSidePlotAreaTypes>>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x0001A499 File Offset: 0x00018699
		// (set) Token: 0x060006A4 RID: 1700 RVA: 0x0001A4A7 File Offset: 0x000186A7
		[ReportExpressionDefaultValue(typeof(ReportColor))]
		public ReportExpression<ReportColor> CalloutBackColor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x0001A4BB File Offset: 0x000186BB
		// (set) Token: 0x060006A6 RID: 1702 RVA: 0x0001A4C9 File Offset: 0x000186C9
		[ReportExpressionDefaultValue(typeof(ChartCalloutLineAnchorTypes), ChartCalloutLineAnchorTypes.Arrow)]
		public ReportExpression<ChartCalloutLineAnchorTypes> CalloutLineAnchor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartCalloutLineAnchorTypes>>(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x0001A4DD File Offset: 0x000186DD
		// (set) Token: 0x060006A8 RID: 1704 RVA: 0x0001A4EB File Offset: 0x000186EB
		[ReportExpressionDefaultValue(typeof(ReportColor))]
		public ReportExpression<ReportColor> CalloutLineColor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x0001A4FF File Offset: 0x000186FF
		// (set) Token: 0x060006AA RID: 1706 RVA: 0x0001A50D File Offset: 0x0001870D
		[ReportExpressionDefaultValue(typeof(ChartCalloutLineStyles), ChartCalloutLineStyles.Solid)]
		public ReportExpression<ChartCalloutLineStyles> CalloutLineStyle
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartCalloutLineStyles>>(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x0001A521 File Offset: 0x00018721
		// (set) Token: 0x060006AC RID: 1708 RVA: 0x0001A52F File Offset: 0x0001872F
		[ReportExpressionDefaultValue(typeof(ReportSize), "0.75pt")]
		public ReportExpression<ReportSize> CalloutLineWidth
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(6);
			}
			set
			{
				base.PropertyStore.SetObject(6, value);
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x0001A543 File Offset: 0x00018743
		// (set) Token: 0x060006AE RID: 1710 RVA: 0x0001A551 File Offset: 0x00018751
		[ReportExpressionDefaultValue(typeof(ChartCalloutStyles), ChartCalloutStyles.Underline)]
		public ReportExpression<ChartCalloutStyles> CalloutStyle
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartCalloutStyles>>(7);
			}
			set
			{
				base.PropertyStore.SetObject(7, value);
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x0001A565 File Offset: 0x00018765
		// (set) Token: 0x060006B0 RID: 1712 RVA: 0x0001A573 File Offset: 0x00018773
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> ShowOverlapped
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(8);
			}
			set
			{
				base.PropertyStore.SetObject(8, value);
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x0001A587 File Offset: 0x00018787
		// (set) Token: 0x060006B2 RID: 1714 RVA: 0x0001A596 File Offset: 0x00018796
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> MarkerOverlapping
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(9);
			}
			set
			{
				base.PropertyStore.SetObject(9, value);
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x0001A5AB File Offset: 0x000187AB
		// (set) Token: 0x060006B4 RID: 1716 RVA: 0x0001A5BA File Offset: 0x000187BA
		[ReportExpressionDefaultValue(typeof(ReportSize), "23pt")]
		public ReportExpression<ReportSize> MaxMovingDistance
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(10);
			}
			set
			{
				base.PropertyStore.SetObject(10, value);
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x0001A5CF File Offset: 0x000187CF
		// (set) Token: 0x060006B6 RID: 1718 RVA: 0x0001A5DE File Offset: 0x000187DE
		[ReportExpressionDefaultValue(typeof(ReportSize))]
		public ReportExpression<ReportSize> MinMovingDistance
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(11);
			}
			set
			{
				base.PropertyStore.SetObject(11, value);
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x0001A5F3 File Offset: 0x000187F3
		// (set) Token: 0x060006B8 RID: 1720 RVA: 0x0001A607 File Offset: 0x00018807
		public ChartNoMoveDirections ChartNoMoveDirections
		{
			get
			{
				return (ChartNoMoveDirections)base.PropertyStore.GetObject(12);
			}
			set
			{
				base.PropertyStore.SetObject(12, value);
			}
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0001A617 File Offset: 0x00018817
		public ChartSmartLabel()
		{
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0001A61F File Offset: 0x0001881F
		internal ChartSmartLabel(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x02000351 RID: 849
		internal class Definition : DefinitionStore<ChartSmartLabel, ChartSmartLabel.Definition.Properties>
		{
			// Token: 0x060017D4 RID: 6100 RVA: 0x0003ABC3 File Offset: 0x00038DC3
			private Definition()
			{
			}

			// Token: 0x02000470 RID: 1136
			internal enum Properties
			{
				// Token: 0x04000A5E RID: 2654
				Disabled,
				// Token: 0x04000A5F RID: 2655
				AllowOutSidePlotArea,
				// Token: 0x04000A60 RID: 2656
				CalloutBackColor,
				// Token: 0x04000A61 RID: 2657
				CalloutLineAnchor,
				// Token: 0x04000A62 RID: 2658
				CalloutLineColor,
				// Token: 0x04000A63 RID: 2659
				CalloutLineStyle,
				// Token: 0x04000A64 RID: 2660
				CalloutLineWidth,
				// Token: 0x04000A65 RID: 2661
				CalloutStyle,
				// Token: 0x04000A66 RID: 2662
				ShowOverlapped,
				// Token: 0x04000A67 RID: 2663
				MarkerOverlapping,
				// Token: 0x04000A68 RID: 2664
				MaxMovingDistance,
				// Token: 0x04000A69 RID: 2665
				MinMovingDistance,
				// Token: 0x04000A6A RID: 2666
				ChartNoMoveDirections,
				// Token: 0x04000A6B RID: 2667
				PropertyCount
			}
		}
	}
}
