using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000097 RID: 151
	public class ChartItemInLegend : ReportObject
	{
		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x0001A172 File Offset: 0x00018372
		// (set) Token: 0x06000671 RID: 1649 RVA: 0x0001A180 File Offset: 0x00018380
		[ReportExpressionDefaultValue]
		public ReportExpression LegendText
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x0001A194 File Offset: 0x00018394
		// (set) Token: 0x06000673 RID: 1651 RVA: 0x0001A1A2 File Offset: 0x000183A2
		[ReportExpressionDefaultValue]
		public ReportExpression ToolTip
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

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x0001A1B6 File Offset: 0x000183B6
		// (set) Token: 0x06000675 RID: 1653 RVA: 0x0001A1C9 File Offset: 0x000183C9
		public ActionInfo ActionInfo
		{
			get
			{
				return (ActionInfo)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x0001A1D8 File Offset: 0x000183D8
		// (set) Token: 0x06000677 RID: 1655 RVA: 0x0001A1E6 File Offset: 0x000183E6
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> Hidden
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0001A1FA File Offset: 0x000183FA
		public ChartItemInLegend()
		{
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0001A202 File Offset: 0x00018402
		internal ChartItemInLegend(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0200034D RID: 845
		internal class Definition : DefinitionStore<ChartItemInLegend, ChartItemInLegend.Definition.Properties>
		{
			// Token: 0x060017D0 RID: 6096 RVA: 0x0003ABA3 File Offset: 0x00038DA3
			private Definition()
			{
			}

			// Token: 0x0200046C RID: 1132
			internal enum Properties
			{
				// Token: 0x04000A42 RID: 2626
				LegendText,
				// Token: 0x04000A43 RID: 2627
				ToolTip,
				// Token: 0x04000A44 RID: 2628
				ActionInfo,
				// Token: 0x04000A45 RID: 2629
				Hidden,
				// Token: 0x04000A46 RID: 2630
				PropertyCount
			}
		}
	}
}
