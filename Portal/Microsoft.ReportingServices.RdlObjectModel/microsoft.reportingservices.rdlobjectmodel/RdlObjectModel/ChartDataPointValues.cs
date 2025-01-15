using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000095 RID: 149
	public class ChartDataPointValues : ReportObject
	{
		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x00019F1D File Offset: 0x0001811D
		// (set) Token: 0x0600064C RID: 1612 RVA: 0x00019F2B File Offset: 0x0001812B
		[ReportExpressionDefaultValue]
		public ReportExpression X
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

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x00019F3F File Offset: 0x0001813F
		// (set) Token: 0x0600064E RID: 1614 RVA: 0x00019F4D File Offset: 0x0001814D
		[ReportExpressionDefaultValue]
		public ReportExpression Y
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

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x00019F61 File Offset: 0x00018161
		// (set) Token: 0x06000650 RID: 1616 RVA: 0x00019F6F File Offset: 0x0001816F
		[ReportExpressionDefaultValue]
		public ReportExpression Size
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x00019F83 File Offset: 0x00018183
		// (set) Token: 0x06000652 RID: 1618 RVA: 0x00019F91 File Offset: 0x00018191
		[ReportExpressionDefaultValue]
		public ReportExpression High
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x00019FA5 File Offset: 0x000181A5
		// (set) Token: 0x06000654 RID: 1620 RVA: 0x00019FB3 File Offset: 0x000181B3
		[ReportExpressionDefaultValue]
		public ReportExpression Low
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

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x00019FC7 File Offset: 0x000181C7
		// (set) Token: 0x06000656 RID: 1622 RVA: 0x00019FD5 File Offset: 0x000181D5
		[ReportExpressionDefaultValue]
		public ReportExpression Start
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x00019FE9 File Offset: 0x000181E9
		// (set) Token: 0x06000658 RID: 1624 RVA: 0x00019FF7 File Offset: 0x000181F7
		[ReportExpressionDefaultValue]
		public ReportExpression End
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(6);
			}
			set
			{
				base.PropertyStore.SetObject(6, value);
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x0001A00B File Offset: 0x0001820B
		// (set) Token: 0x0600065A RID: 1626 RVA: 0x0001A019 File Offset: 0x00018219
		[ReportExpressionDefaultValue]
		public ReportExpression Mean
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(7);
			}
			set
			{
				base.PropertyStore.SetObject(7, value);
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x0001A02D File Offset: 0x0001822D
		// (set) Token: 0x0600065C RID: 1628 RVA: 0x0001A03B File Offset: 0x0001823B
		[ReportExpressionDefaultValue]
		public ReportExpression Median
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(8);
			}
			set
			{
				base.PropertyStore.SetObject(8, value);
			}
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0001A04F File Offset: 0x0001824F
		public ChartDataPointValues()
		{
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0001A057 File Offset: 0x00018257
		internal ChartDataPointValues(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0200034B RID: 843
		internal class Definition : DefinitionStore<ChartDataPointValues, ChartDataPointValues.Definition.Properties>
		{
			// Token: 0x060017CE RID: 6094 RVA: 0x0003AB93 File Offset: 0x00038D93
			private Definition()
			{
			}

			// Token: 0x0200046A RID: 1130
			internal enum Properties
			{
				// Token: 0x04000A2E RID: 2606
				X,
				// Token: 0x04000A2F RID: 2607
				Y,
				// Token: 0x04000A30 RID: 2608
				Size,
				// Token: 0x04000A31 RID: 2609
				High,
				// Token: 0x04000A32 RID: 2610
				Low,
				// Token: 0x04000A33 RID: 2611
				Start,
				// Token: 0x04000A34 RID: 2612
				End,
				// Token: 0x04000A35 RID: 2613
				Mean,
				// Token: 0x04000A36 RID: 2614
				Median,
				// Token: 0x04000A37 RID: 2615
				PropertyCount
			}
		}
	}
}
