using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020000A0 RID: 160
	public class ChartTickMarks : ReportObject
	{
		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x0001AA58 File Offset: 0x00018C58
		// (set) Token: 0x060006FB RID: 1787 RVA: 0x0001AA66 File Offset: 0x00018C66
		[ReportExpressionDefaultValue(typeof(ChartTickMarksEnabledTypes), ChartTickMarksEnabledTypes.Auto)]
		public ReportExpression<ChartTickMarksEnabledTypes> Enabled
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartTickMarksEnabledTypes>>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x0001AA7A File Offset: 0x00018C7A
		// (set) Token: 0x060006FD RID: 1789 RVA: 0x0001AA88 File Offset: 0x00018C88
		[ReportExpressionDefaultValue(typeof(ChartTickMarkTypes), ChartTickMarkTypes.Outside)]
		public ReportExpression<ChartTickMarkTypes> Type
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartTickMarkTypes>>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x0001AA9C File Offset: 0x00018C9C
		// (set) Token: 0x060006FF RID: 1791 RVA: 0x0001AAAF File Offset: 0x00018CAF
		public Style Style
		{
			get
			{
				return (Style)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x0001AABE File Offset: 0x00018CBE
		// (set) Token: 0x06000701 RID: 1793 RVA: 0x0001AACC File Offset: 0x00018CCC
		[ReportExpressionDefaultValue(typeof(double), 1.0)]
		public ReportExpression<double> Length
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

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x0001AAE0 File Offset: 0x00018CE0
		// (set) Token: 0x06000703 RID: 1795 RVA: 0x0001AAEE File Offset: 0x00018CEE
		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> Interval
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

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x0001AB02 File Offset: 0x00018D02
		// (set) Token: 0x06000705 RID: 1797 RVA: 0x0001AB10 File Offset: 0x00018D10
		[ReportExpressionDefaultValue(typeof(ChartIntervalTypes), ChartIntervalTypes.Default)]
		public ReportExpression<ChartIntervalTypes> IntervalType
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartIntervalTypes>>(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x0001AB24 File Offset: 0x00018D24
		// (set) Token: 0x06000707 RID: 1799 RVA: 0x0001AB32 File Offset: 0x00018D32
		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> IntervalOffset
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

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x0001AB46 File Offset: 0x00018D46
		// (set) Token: 0x06000709 RID: 1801 RVA: 0x0001AB54 File Offset: 0x00018D54
		[ReportExpressionDefaultValue(typeof(ChartIntervalOffsetTypes), ChartIntervalOffsetTypes.Default)]
		public ReportExpression<ChartIntervalOffsetTypes> IntervalOffsetType
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartIntervalOffsetTypes>>(7);
			}
			set
			{
				base.PropertyStore.SetObject(7, value);
			}
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0001AB68 File Offset: 0x00018D68
		public ChartTickMarks()
		{
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0001AB70 File Offset: 0x00018D70
		internal ChartTickMarks(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0001AB79 File Offset: 0x00018D79
		public override void Initialize()
		{
			base.Initialize();
			this.Length = 1.0;
		}

		// Token: 0x02000356 RID: 854
		internal class Definition : DefinitionStore<ChartTickMarks, ChartTickMarks.Definition.Properties>
		{
			// Token: 0x060017D9 RID: 6105 RVA: 0x0003ABEB File Offset: 0x00038DEB
			private Definition()
			{
			}

			// Token: 0x02000475 RID: 1141
			internal enum Properties
			{
				// Token: 0x04000A90 RID: 2704
				Enabled,
				// Token: 0x04000A91 RID: 2705
				Type,
				// Token: 0x04000A92 RID: 2706
				Style,
				// Token: 0x04000A93 RID: 2707
				Length,
				// Token: 0x04000A94 RID: 2708
				Interval,
				// Token: 0x04000A95 RID: 2709
				IntervalType,
				// Token: 0x04000A96 RID: 2710
				IntervalOffset,
				// Token: 0x04000A97 RID: 2711
				IntervalOffsetType,
				// Token: 0x04000A98 RID: 2712
				PropertyCount
			}
		}
	}
}
