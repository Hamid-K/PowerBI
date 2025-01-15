using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020000A2 RID: 162
	public class ChartAlignType : ReportObject
	{
		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x0001ABEA File Offset: 0x00018DEA
		// (set) Token: 0x06000714 RID: 1812 RVA: 0x0001ABF8 File Offset: 0x00018DF8
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> AxesView
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

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x0001AC0C File Offset: 0x00018E0C
		// (set) Token: 0x06000716 RID: 1814 RVA: 0x0001AC1A File Offset: 0x00018E1A
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> Cursor
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

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x0001AC2E File Offset: 0x00018E2E
		// (set) Token: 0x06000718 RID: 1816 RVA: 0x0001AC3C File Offset: 0x00018E3C
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> Position
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

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x0001AC50 File Offset: 0x00018E50
		// (set) Token: 0x0600071A RID: 1818 RVA: 0x0001AC5E File Offset: 0x00018E5E
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> InnerPlotPosition
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

		// Token: 0x0600071B RID: 1819 RVA: 0x0001AC72 File Offset: 0x00018E72
		public ChartAlignType()
		{
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0001AC7A File Offset: 0x00018E7A
		internal ChartAlignType(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x02000358 RID: 856
		internal class Definition : DefinitionStore<ChartAlignType, ChartAlignType.Definition.Properties>
		{
			// Token: 0x060017DB RID: 6107 RVA: 0x0003ABFB File Offset: 0x00038DFB
			private Definition()
			{
			}

			// Token: 0x02000477 RID: 1143
			internal enum Properties
			{
				// Token: 0x04000A9E RID: 2718
				AxesView,
				// Token: 0x04000A9F RID: 2719
				Cursor,
				// Token: 0x04000AA0 RID: 2720
				Position,
				// Token: 0x04000AA1 RID: 2721
				InnerPlotPosition
			}
		}
	}
}
