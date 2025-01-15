using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000090 RID: 144
	public class ChartAxisScaleBreak : ReportObject
	{
		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x00019796 File Offset: 0x00017996
		// (set) Token: 0x060005DF RID: 1503 RVA: 0x000197A4 File Offset: 0x000179A4
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> Enabled
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

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x000197B8 File Offset: 0x000179B8
		// (set) Token: 0x060005E1 RID: 1505 RVA: 0x000197C6 File Offset: 0x000179C6
		[ReportExpressionDefaultValue(typeof(ChartBreakLineTypes), ChartBreakLineTypes.Ragged)]
		public ReportExpression<ChartBreakLineTypes> BreakLineType
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartBreakLineTypes>>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x000197DA File Offset: 0x000179DA
		// (set) Token: 0x060005E3 RID: 1507 RVA: 0x000197E8 File Offset: 0x000179E8
		[ReportExpressionDefaultValue(typeof(int), 25)]
		public ReportExpression<int> CollapsibleSpaceThreshold
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<int>>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x000197FC File Offset: 0x000179FC
		// (set) Token: 0x060005E5 RID: 1509 RVA: 0x0001980A File Offset: 0x00017A0A
		[ReportExpressionDefaultValue(typeof(int), 2)]
		public ReportExpression<int> MaxNumberOfBreaks
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<int>>(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x0001981E File Offset: 0x00017A1E
		// (set) Token: 0x060005E7 RID: 1511 RVA: 0x0001982C File Offset: 0x00017A2C
		[ReportExpressionDefaultValue(typeof(double), 1.5)]
		public ReportExpression<double> Spacing
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x00019840 File Offset: 0x00017A40
		// (set) Token: 0x060005E9 RID: 1513 RVA: 0x0001984E File Offset: 0x00017A4E
		[ReportExpressionDefaultValue(typeof(ChartIncludeZeroTypes), ChartIncludeZeroTypes.Auto)]
		public ReportExpression<ChartIncludeZeroTypes> IncludeZero
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartIncludeZeroTypes>>(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x00019862 File Offset: 0x00017A62
		// (set) Token: 0x060005EB RID: 1515 RVA: 0x00019875 File Offset: 0x00017A75
		public Style Style
		{
			get
			{
				return (Style)base.PropertyStore.GetObject(6);
			}
			set
			{
				base.PropertyStore.SetObject(6, value);
			}
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00019884 File Offset: 0x00017A84
		public ChartAxisScaleBreak()
		{
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0001988C File Offset: 0x00017A8C
		internal ChartAxisScaleBreak(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00019895 File Offset: 0x00017A95
		public override void Initialize()
		{
			base.Initialize();
			this.Spacing = 1.5;
			this.CollapsibleSpaceThreshold = 25;
			this.MaxNumberOfBreaks = 2;
		}

		// Token: 0x02000346 RID: 838
		internal class Definition : DefinitionStore<ChartAxisScaleBreak, ChartAxisScaleBreak.Definition.Properties>
		{
			// Token: 0x060017C9 RID: 6089 RVA: 0x0003AB6B File Offset: 0x00038D6B
			private Definition()
			{
			}

			// Token: 0x02000465 RID: 1125
			internal enum Properties
			{
				// Token: 0x040009F4 RID: 2548
				Enabled,
				// Token: 0x040009F5 RID: 2549
				BreakLineType,
				// Token: 0x040009F6 RID: 2550
				CollapsibleSpaceThreshold,
				// Token: 0x040009F7 RID: 2551
				MaxNumberOfBreaks,
				// Token: 0x040009F8 RID: 2552
				Spacing,
				// Token: 0x040009F9 RID: 2553
				IncludeZero,
				// Token: 0x040009FA RID: 2554
				Style,
				// Token: 0x040009FB RID: 2555
				PropertyCount
			}
		}
	}
}
