using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200008F RID: 143
	public class ChartAxisTitle : ReportObject
	{
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x000196FD File Offset: 0x000178FD
		// (set) Token: 0x060005D5 RID: 1493 RVA: 0x0001970B File Offset: 0x0001790B
		public ReportExpression Caption
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

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x0001971F File Offset: 0x0001791F
		// (set) Token: 0x060005D7 RID: 1495 RVA: 0x0001972D File Offset: 0x0001792D
		[ReportExpressionDefaultValue(typeof(ChartAxisTitlePositions), ChartAxisTitlePositions.Center)]
		public ReportExpression<ChartAxisTitlePositions> Position
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartAxisTitlePositions>>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x00019741 File Offset: 0x00017941
		// (set) Token: 0x060005D9 RID: 1497 RVA: 0x00019754 File Offset: 0x00017954
		public Style Style
		{
			get
			{
				return (Style)base.PropertyStore.GetObject(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x00019763 File Offset: 0x00017963
		// (set) Token: 0x060005DB RID: 1499 RVA: 0x00019771 File Offset: 0x00017971
		[ReportExpressionDefaultValue(typeof(TextOrientations), TextOrientations.Auto)]
		public ReportExpression<TextOrientations> TextOrientation
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<TextOrientations>>(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00019785 File Offset: 0x00017985
		public ChartAxisTitle()
		{
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0001978D File Offset: 0x0001798D
		internal ChartAxisTitle(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x02000345 RID: 837
		internal class Definition : DefinitionStore<ChartAxisTitle, ChartAxisTitle.Definition.Properties>
		{
			// Token: 0x060017C8 RID: 6088 RVA: 0x0003AB63 File Offset: 0x00038D63
			private Definition()
			{
			}

			// Token: 0x02000464 RID: 1124
			internal enum Properties
			{
				// Token: 0x040009ED RID: 2541
				Caption,
				// Token: 0x040009EE RID: 2542
				CaptionLocID,
				// Token: 0x040009EF RID: 2543
				Position,
				// Token: 0x040009F0 RID: 2544
				Style,
				// Token: 0x040009F1 RID: 2545
				TextOrientation,
				// Token: 0x040009F2 RID: 2546
				PropertyCount
			}
		}
	}
}
