using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200009C RID: 156
	public class ChartNoMoveDirections : ReportObject
	{
		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x0001A628 File Offset: 0x00018828
		// (set) Token: 0x060006BC RID: 1724 RVA: 0x0001A636 File Offset: 0x00018836
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> Up
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

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x0001A64A File Offset: 0x0001884A
		// (set) Token: 0x060006BE RID: 1726 RVA: 0x0001A658 File Offset: 0x00018858
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> Left
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x0001A66C File Offset: 0x0001886C
		// (set) Token: 0x060006C0 RID: 1728 RVA: 0x0001A67A File Offset: 0x0001887A
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> Right
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x0001A68E File Offset: 0x0001888E
		// (set) Token: 0x060006C2 RID: 1730 RVA: 0x0001A69C File Offset: 0x0001889C
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> Down
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

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x0001A6B0 File Offset: 0x000188B0
		// (set) Token: 0x060006C4 RID: 1732 RVA: 0x0001A6BE File Offset: 0x000188BE
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> UpLeft
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x0001A6D2 File Offset: 0x000188D2
		// (set) Token: 0x060006C6 RID: 1734 RVA: 0x0001A6E0 File Offset: 0x000188E0
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> UpRight
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x0001A6F4 File Offset: 0x000188F4
		// (set) Token: 0x060006C8 RID: 1736 RVA: 0x0001A702 File Offset: 0x00018902
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> DownLeft
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(6);
			}
			set
			{
				base.PropertyStore.SetObject(6, value);
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x0001A716 File Offset: 0x00018916
		// (set) Token: 0x060006CA RID: 1738 RVA: 0x0001A724 File Offset: 0x00018924
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> DownRight
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(7);
			}
			set
			{
				base.PropertyStore.SetObject(7, value);
			}
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0001A738 File Offset: 0x00018938
		public ChartNoMoveDirections()
		{
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0001A740 File Offset: 0x00018940
		internal ChartNoMoveDirections(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x02000352 RID: 850
		internal class Definition : DefinitionStore<ChartNoMoveDirections, ChartNoMoveDirections.Definition.Properties>
		{
			// Token: 0x060017D5 RID: 6101 RVA: 0x0003ABCB File Offset: 0x00038DCB
			private Definition()
			{
			}

			// Token: 0x02000471 RID: 1137
			internal enum Properties
			{
				// Token: 0x04000A6D RID: 2669
				Up,
				// Token: 0x04000A6E RID: 2670
				Left,
				// Token: 0x04000A6F RID: 2671
				Right,
				// Token: 0x04000A70 RID: 2672
				Down,
				// Token: 0x04000A71 RID: 2673
				UpLeft,
				// Token: 0x04000A72 RID: 2674
				UpRight,
				// Token: 0x04000A73 RID: 2675
				DownLeft,
				// Token: 0x04000A74 RID: 2676
				DownRight,
				// Token: 0x04000A75 RID: 2677
				PropertyCount
			}
		}
	}
}
