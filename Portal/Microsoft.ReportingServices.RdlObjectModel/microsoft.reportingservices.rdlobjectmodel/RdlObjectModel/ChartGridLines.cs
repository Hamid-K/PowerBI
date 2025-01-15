using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200009F RID: 159
	public class ChartGridLines : ReportObject
	{
		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x0001A97B File Offset: 0x00018B7B
		// (set) Token: 0x060006ED RID: 1773 RVA: 0x0001A989 File Offset: 0x00018B89
		[ReportExpressionDefaultValue(typeof(ChartGridLinesEnabledTypes), ChartGridLinesEnabledTypes.Auto)]
		public ReportExpression<ChartGridLinesEnabledTypes> Enabled
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartGridLinesEnabledTypes>>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x0001A99D File Offset: 0x00018B9D
		// (set) Token: 0x060006EF RID: 1775 RVA: 0x0001A9B0 File Offset: 0x00018BB0
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

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x0001A9BF File Offset: 0x00018BBF
		// (set) Token: 0x060006F1 RID: 1777 RVA: 0x0001A9CD File Offset: 0x00018BCD
		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> Interval
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x0001A9E1 File Offset: 0x00018BE1
		// (set) Token: 0x060006F3 RID: 1779 RVA: 0x0001A9EF File Offset: 0x00018BEF
		[ReportExpressionDefaultValue(typeof(ChartIntervalTypes), ChartIntervalTypes.Default)]
		public ReportExpression<ChartIntervalTypes> IntervalType
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartIntervalTypes>>(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x0001AA03 File Offset: 0x00018C03
		// (set) Token: 0x060006F5 RID: 1781 RVA: 0x0001AA11 File Offset: 0x00018C11
		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> IntervalOffset
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

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x0001AA25 File Offset: 0x00018C25
		// (set) Token: 0x060006F7 RID: 1783 RVA: 0x0001AA33 File Offset: 0x00018C33
		[ReportExpressionDefaultValue(typeof(ChartIntervalOffsetTypes), ChartIntervalOffsetTypes.Default)]
		public ReportExpression<ChartIntervalOffsetTypes> IntervalOffsetType
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartIntervalOffsetTypes>>(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0001AA47 File Offset: 0x00018C47
		public ChartGridLines()
		{
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0001AA4F File Offset: 0x00018C4F
		internal ChartGridLines(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x02000355 RID: 853
		internal class Definition : DefinitionStore<ChartGridLines, ChartGridLines.Definition.Properties>
		{
			// Token: 0x060017D8 RID: 6104 RVA: 0x0003ABE3 File Offset: 0x00038DE3
			private Definition()
			{
			}

			// Token: 0x02000474 RID: 1140
			internal enum Properties
			{
				// Token: 0x04000A88 RID: 2696
				Hidden,
				// Token: 0x04000A89 RID: 2697
				Style,
				// Token: 0x04000A8A RID: 2698
				Interval,
				// Token: 0x04000A8B RID: 2699
				IntervalType,
				// Token: 0x04000A8C RID: 2700
				IntervalOffset,
				// Token: 0x04000A8D RID: 2701
				IntervalOffsetType,
				// Token: 0x04000A8E RID: 2702
				PropertyCount
			}
		}
	}
}
