using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200009A RID: 154
	public class ChartDataLabel : ReportObject
	{
		// Token: 0x17000215 RID: 533
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x0001A334 File Offset: 0x00018534
		// (set) Token: 0x0600068E RID: 1678 RVA: 0x0001A347 File Offset: 0x00018547
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

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x0001A356 File Offset: 0x00018556
		// (set) Token: 0x06000690 RID: 1680 RVA: 0x0001A364 File Offset: 0x00018564
		[ReportExpressionDefaultValue]
		public ReportExpression Label
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

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x0001A378 File Offset: 0x00018578
		// (set) Token: 0x06000692 RID: 1682 RVA: 0x0001A386 File Offset: 0x00018586
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> UseValueAsLabel
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

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x0001A39A File Offset: 0x0001859A
		// (set) Token: 0x06000694 RID: 1684 RVA: 0x0001A3A8 File Offset: 0x000185A8
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> Visible
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

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x0001A3BC File Offset: 0x000185BC
		// (set) Token: 0x06000696 RID: 1686 RVA: 0x0001A3CA File Offset: 0x000185CA
		[ReportExpressionDefaultValue(typeof(ChartDataLabelPositions), ChartDataLabelPositions.Auto)]
		public ReportExpression<ChartDataLabelPositions> Position
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartDataLabelPositions>>(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x0001A3DE File Offset: 0x000185DE
		// (set) Token: 0x06000698 RID: 1688 RVA: 0x0001A3EC File Offset: 0x000185EC
		[ReportExpressionDefaultValue(typeof(int), 0)]
		public ReportExpression<int> Rotation
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<int>>(6);
			}
			set
			{
				base.PropertyStore.SetObject(6, value);
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x0001A400 File Offset: 0x00018600
		// (set) Token: 0x0600069A RID: 1690 RVA: 0x0001A40E File Offset: 0x0001860E
		[ReportExpressionDefaultValue]
		public ReportExpression ToolTip
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

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x0001A422 File Offset: 0x00018622
		// (set) Token: 0x0600069C RID: 1692 RVA: 0x0001A435 File Offset: 0x00018635
		public ActionInfo ActionInfo
		{
			get
			{
				return (ActionInfo)base.PropertyStore.GetObject(8);
			}
			set
			{
				base.PropertyStore.SetObject(8, value);
			}
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0001A444 File Offset: 0x00018644
		public ChartDataLabel()
		{
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0001A44C File Offset: 0x0001864C
		internal ChartDataLabel(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x02000350 RID: 848
		internal class Definition : DefinitionStore<ChartDataLabel, ChartDataLabel.Definition.Properties>
		{
			// Token: 0x060017D3 RID: 6099 RVA: 0x0003ABBB File Offset: 0x00038DBB
			private Definition()
			{
			}

			// Token: 0x0200046F RID: 1135
			internal enum Properties
			{
				// Token: 0x04000A53 RID: 2643
				Style,
				// Token: 0x04000A54 RID: 2644
				Label,
				// Token: 0x04000A55 RID: 2645
				LabelLocID,
				// Token: 0x04000A56 RID: 2646
				UseValueAsLabel,
				// Token: 0x04000A57 RID: 2647
				Visible,
				// Token: 0x04000A58 RID: 2648
				Position,
				// Token: 0x04000A59 RID: 2649
				Rotation,
				// Token: 0x04000A5A RID: 2650
				ToolTip,
				// Token: 0x04000A5B RID: 2651
				ActionInfo,
				// Token: 0x04000A5C RID: 2652
				PropertyCount
			}
		}
	}
}
