using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000091 RID: 145
	public class ChartStripLine : ReportObject
	{
		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x000198CA File Offset: 0x00017ACA
		// (set) Token: 0x060005F0 RID: 1520 RVA: 0x000198DD File Offset: 0x00017ADD
		public Style Style
		{
			get
			{
				return (Style)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x000198EC File Offset: 0x00017AEC
		// (set) Token: 0x060005F2 RID: 1522 RVA: 0x000198FA File Offset: 0x00017AFA
		[ReportExpressionDefaultValue]
		public ReportExpression Title
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x0001990E File Offset: 0x00017B0E
		// (set) Token: 0x060005F4 RID: 1524 RVA: 0x0001991C File Offset: 0x00017B1C
		[ReportExpressionDefaultValue(typeof(TextOrientations), TextOrientations.Auto)]
		public ReportExpression<TextOrientations> TextOrientation
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<TextOrientations>>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x00019930 File Offset: 0x00017B30
		// (set) Token: 0x060005F6 RID: 1526 RVA: 0x00019943 File Offset: 0x00017B43
		public ActionInfo ActionInfo
		{
			get
			{
				return (ActionInfo)base.PropertyStore.GetObject(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x00019952 File Offset: 0x00017B52
		// (set) Token: 0x060005F8 RID: 1528 RVA: 0x00019960 File Offset: 0x00017B60
		[ReportExpressionDefaultValue]
		public ReportExpression ToolTip
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x00019974 File Offset: 0x00017B74
		// (set) Token: 0x060005FA RID: 1530 RVA: 0x00019982 File Offset: 0x00017B82
		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> Interval
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

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x00019996 File Offset: 0x00017B96
		// (set) Token: 0x060005FC RID: 1532 RVA: 0x000199A4 File Offset: 0x00017BA4
		[ReportExpressionDefaultValue(typeof(ChartIntervalTypes), ChartIntervalTypes.Auto)]
		public ReportExpression<ChartIntervalTypes> IntervalType
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartIntervalTypes>>(7);
			}
			set
			{
				base.PropertyStore.SetObject(7, value);
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x000199B8 File Offset: 0x00017BB8
		// (set) Token: 0x060005FE RID: 1534 RVA: 0x000199C6 File Offset: 0x00017BC6
		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> IntervalOffset
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(8);
			}
			set
			{
				base.PropertyStore.SetObject(8, value);
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x000199DA File Offset: 0x00017BDA
		// (set) Token: 0x06000600 RID: 1536 RVA: 0x000199E9 File Offset: 0x00017BE9
		[ReportExpressionDefaultValue(typeof(ChartIntervalOffsetTypes), ChartIntervalOffsetTypes.Auto)]
		public ReportExpression<ChartIntervalOffsetTypes> IntervalOffsetType
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartIntervalOffsetTypes>>(9);
			}
			set
			{
				base.PropertyStore.SetObject(9, value);
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x000199FE File Offset: 0x00017BFE
		// (set) Token: 0x06000602 RID: 1538 RVA: 0x00019A0D File Offset: 0x00017C0D
		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> StripWidth
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(10);
			}
			set
			{
				base.PropertyStore.SetObject(10, value);
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x00019A22 File Offset: 0x00017C22
		// (set) Token: 0x06000604 RID: 1540 RVA: 0x00019A31 File Offset: 0x00017C31
		[ReportExpressionDefaultValue(typeof(ChartStripWidthTypes), ChartStripWidthTypes.Auto)]
		public ReportExpression<ChartStripWidthTypes> StripWidthType
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartStripWidthTypes>>(11);
			}
			set
			{
				base.PropertyStore.SetObject(11, value);
			}
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00019A46 File Offset: 0x00017C46
		public ChartStripLine()
		{
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00019A4E File Offset: 0x00017C4E
		internal ChartStripLine(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00019A57 File Offset: 0x00017C57
		public override void Initialize()
		{
			base.Initialize();
			this.IntervalType = ChartIntervalTypes.Auto;
			this.IntervalOffsetType = ChartIntervalOffsetTypes.Auto;
		}

		// Token: 0x02000347 RID: 839
		internal class Definition : DefinitionStore<ChartStripLine, ChartStripLine.Definition.Properties>
		{
			// Token: 0x060017CA RID: 6090 RVA: 0x0003AB73 File Offset: 0x00038D73
			private Definition()
			{
			}

			// Token: 0x02000466 RID: 1126
			internal enum Properties
			{
				// Token: 0x040009FD RID: 2557
				Style,
				// Token: 0x040009FE RID: 2558
				Title,
				// Token: 0x040009FF RID: 2559
				TextOrientation,
				// Token: 0x04000A00 RID: 2560
				ActionInfo,
				// Token: 0x04000A01 RID: 2561
				ToolTip,
				// Token: 0x04000A02 RID: 2562
				ToolTipLocID,
				// Token: 0x04000A03 RID: 2563
				Interval,
				// Token: 0x04000A04 RID: 2564
				IntervalType,
				// Token: 0x04000A05 RID: 2565
				IntervalOffset,
				// Token: 0x04000A06 RID: 2566
				IntervalOffsetType,
				// Token: 0x04000A07 RID: 2567
				StripWidth,
				// Token: 0x04000A08 RID: 2568
				StripWidthType,
				// Token: 0x04000A09 RID: 2569
				PropertyCount
			}
		}
	}
}
