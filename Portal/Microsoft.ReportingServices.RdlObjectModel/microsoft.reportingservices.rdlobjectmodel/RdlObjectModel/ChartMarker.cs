using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200009D RID: 157
	public class ChartMarker : ReportObject
	{
		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x0001A749 File Offset: 0x00018949
		// (set) Token: 0x060006CE RID: 1742 RVA: 0x0001A757 File Offset: 0x00018957
		[ReportExpressionDefaultValue(typeof(ChartMarkerTypes), ChartMarkerTypes.None)]
		public ReportExpression<ChartMarkerTypes> Type
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartMarkerTypes>>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x0001A76B File Offset: 0x0001896B
		// (set) Token: 0x060006D0 RID: 1744 RVA: 0x0001A779 File Offset: 0x00018979
		[ReportExpressionDefaultValue(typeof(ReportSize), "3.75pt")]
		public ReportExpression<ReportSize> Size
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x0001A78D File Offset: 0x0001898D
		// (set) Token: 0x060006D2 RID: 1746 RVA: 0x0001A7A0 File Offset: 0x000189A0
		public EmptyColorStyle Style
		{
			get
			{
				return (EmptyColorStyle)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0001A7AF File Offset: 0x000189AF
		public ChartMarker()
		{
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0001A7B7 File Offset: 0x000189B7
		internal ChartMarker(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x02000353 RID: 851
		internal class Definition : DefinitionStore<ChartMarker, ChartMarker.Definition.Properties>
		{
			// Token: 0x060017D6 RID: 6102 RVA: 0x0003ABD3 File Offset: 0x00038DD3
			private Definition()
			{
			}

			// Token: 0x02000472 RID: 1138
			internal enum Properties
			{
				// Token: 0x04000A77 RID: 2679
				Type,
				// Token: 0x04000A78 RID: 2680
				Size,
				// Token: 0x04000A79 RID: 2681
				Style,
				// Token: 0x04000A7A RID: 2682
				PropertyCount
			}
		}
	}
}
