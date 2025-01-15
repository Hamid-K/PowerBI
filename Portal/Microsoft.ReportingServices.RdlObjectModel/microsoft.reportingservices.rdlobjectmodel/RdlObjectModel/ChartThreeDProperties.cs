using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200009E RID: 158
	public class ChartThreeDProperties : ReportObject
	{
		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x0001A7C0 File Offset: 0x000189C0
		// (set) Token: 0x060006D6 RID: 1750 RVA: 0x0001A7CE File Offset: 0x000189CE
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

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x0001A7E2 File Offset: 0x000189E2
		// (set) Token: 0x060006D8 RID: 1752 RVA: 0x0001A7F0 File Offset: 0x000189F0
		[ReportExpressionDefaultValue(typeof(ChartProjectionModes), ChartProjectionModes.Oblique)]
		public ReportExpression<ChartProjectionModes> ProjectionMode
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartProjectionModes>>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x0001A804 File Offset: 0x00018A04
		// (set) Token: 0x060006DA RID: 1754 RVA: 0x0001A812 File Offset: 0x00018A12
		[ReportExpressionDefaultValue(typeof(int), 0)]
		public ReportExpression<int> Perspective
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

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x0001A826 File Offset: 0x00018A26
		// (set) Token: 0x060006DC RID: 1756 RVA: 0x0001A834 File Offset: 0x00018A34
		[ReportExpressionDefaultValue(typeof(int), 30)]
		public ReportExpression<int> Rotation
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

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x0001A848 File Offset: 0x00018A48
		// (set) Token: 0x060006DE RID: 1758 RVA: 0x0001A856 File Offset: 0x00018A56
		[ReportExpressionDefaultValue(typeof(int), 30)]
		public ReportExpression<int> Inclination
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<int>>(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x0001A86A File Offset: 0x00018A6A
		// (set) Token: 0x060006E0 RID: 1760 RVA: 0x0001A878 File Offset: 0x00018A78
		[ReportExpressionDefaultValue(typeof(int), 100)]
		public ReportExpression<int> DepthRatio
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<int>>(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x0001A88C File Offset: 0x00018A8C
		// (set) Token: 0x060006E2 RID: 1762 RVA: 0x0001A89A File Offset: 0x00018A9A
		[ReportExpressionDefaultValue(typeof(ChartShadings), ChartShadings.Real)]
		public ReportExpression<ChartShadings> Shading
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartShadings>>(6);
			}
			set
			{
				base.PropertyStore.SetObject(6, value);
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x0001A8AE File Offset: 0x00018AAE
		// (set) Token: 0x060006E4 RID: 1764 RVA: 0x0001A8BC File Offset: 0x00018ABC
		[ReportExpressionDefaultValue(typeof(int), 100)]
		public ReportExpression<int> GapDepth
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<int>>(7);
			}
			set
			{
				base.PropertyStore.SetObject(7, value);
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x0001A8D0 File Offset: 0x00018AD0
		// (set) Token: 0x060006E6 RID: 1766 RVA: 0x0001A8DE File Offset: 0x00018ADE
		[ReportExpressionDefaultValue(typeof(int), 7)]
		public ReportExpression<int> WallThickness
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<int>>(8);
			}
			set
			{
				base.PropertyStore.SetObject(8, value);
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x0001A8F2 File Offset: 0x00018AF2
		// (set) Token: 0x060006E8 RID: 1768 RVA: 0x0001A901 File Offset: 0x00018B01
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> Clustered
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(9);
			}
			set
			{
				base.PropertyStore.SetObject(9, value);
			}
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0001A916 File Offset: 0x00018B16
		public ChartThreeDProperties()
		{
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0001A91E File Offset: 0x00018B1E
		internal ChartThreeDProperties(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0001A928 File Offset: 0x00018B28
		public override void Initialize()
		{
			base.Initialize();
			this.Rotation = 30;
			this.Inclination = 30;
			this.DepthRatio = 100;
			this.GapDepth = 100;
			this.WallThickness = 7;
		}

		// Token: 0x02000354 RID: 852
		internal class Definition : DefinitionStore<ChartThreeDProperties, ChartThreeDProperties.Definition.Properties>
		{
			// Token: 0x060017D7 RID: 6103 RVA: 0x0003ABDB File Offset: 0x00038DDB
			private Definition()
			{
			}

			// Token: 0x02000473 RID: 1139
			internal enum Properties
			{
				// Token: 0x04000A7C RID: 2684
				Enabled,
				// Token: 0x04000A7D RID: 2685
				ProjectionMode,
				// Token: 0x04000A7E RID: 2686
				Perspective,
				// Token: 0x04000A7F RID: 2687
				Rotation,
				// Token: 0x04000A80 RID: 2688
				Inclination,
				// Token: 0x04000A81 RID: 2689
				DepthRatio,
				// Token: 0x04000A82 RID: 2690
				Shading,
				// Token: 0x04000A83 RID: 2691
				GapDepth,
				// Token: 0x04000A84 RID: 2692
				WallThickness,
				// Token: 0x04000A85 RID: 2693
				Clustered,
				// Token: 0x04000A86 RID: 2694
				PropertyCount
			}
		}
	}
}
