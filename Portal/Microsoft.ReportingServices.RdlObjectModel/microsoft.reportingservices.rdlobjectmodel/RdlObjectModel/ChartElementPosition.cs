using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020000A3 RID: 163
	public class ChartElementPosition : ReportObject
	{
		// Token: 0x17000253 RID: 595
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x0001AC83 File Offset: 0x00018E83
		// (set) Token: 0x0600071E RID: 1822 RVA: 0x0001AC91 File Offset: 0x00018E91
		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> Top
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x0001ACA5 File Offset: 0x00018EA5
		// (set) Token: 0x06000720 RID: 1824 RVA: 0x0001ACB3 File Offset: 0x00018EB3
		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> Left
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x0001ACC7 File Offset: 0x00018EC7
		// (set) Token: 0x06000722 RID: 1826 RVA: 0x0001ACD5 File Offset: 0x00018ED5
		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> Height
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

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000723 RID: 1827 RVA: 0x0001ACE9 File Offset: 0x00018EE9
		// (set) Token: 0x06000724 RID: 1828 RVA: 0x0001ACF7 File Offset: 0x00018EF7
		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> Width
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

		// Token: 0x06000725 RID: 1829 RVA: 0x0001AD0B File Offset: 0x00018F0B
		public ChartElementPosition()
		{
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0001AD13 File Offset: 0x00018F13
		internal ChartElementPosition(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x02000359 RID: 857
		internal class Definition : DefinitionStore<ChartElementPosition, ChartElementPosition.Definition.Properties>
		{
			// Token: 0x060017DC RID: 6108 RVA: 0x0003AC03 File Offset: 0x00038E03
			private Definition()
			{
			}

			// Token: 0x02000478 RID: 1144
			internal enum Properties
			{
				// Token: 0x04000AA3 RID: 2723
				Top,
				// Token: 0x04000AA4 RID: 2724
				Left,
				// Token: 0x04000AA5 RID: 2725
				Height,
				// Token: 0x04000AA6 RID: 2726
				Width
			}
		}
	}
}
