using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000083 RID: 131
	public class ChartBorderSkin : ReportObject
	{
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x0001829E File Offset: 0x0001649E
		// (set) Token: 0x060004AC RID: 1196 RVA: 0x000182AC File Offset: 0x000164AC
		[ReportExpressionDefaultValue(typeof(ChartBorderSkinTypes), ChartBorderSkinTypes.None)]
		public ReportExpression<ChartBorderSkinTypes> ChartBorderSkinType
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartBorderSkinTypes>>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x000182C0 File Offset: 0x000164C0
		// (set) Token: 0x060004AE RID: 1198 RVA: 0x000182D3 File Offset: 0x000164D3
		public Style Style
		{
			get
			{
				return (Style)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x000182E2 File Offset: 0x000164E2
		public ChartBorderSkin()
		{
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x000182EA File Offset: 0x000164EA
		internal ChartBorderSkin(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x02000336 RID: 822
		internal class Definition : DefinitionStore<ChartBorderSkin, ChartBorderSkin.Definition.Properties>
		{
			// Token: 0x060017A1 RID: 6049 RVA: 0x0003A69C File Offset: 0x0003889C
			private Definition()
			{
			}

			// Token: 0x02000458 RID: 1112
			internal enum Properties
			{
				// Token: 0x0400094A RID: 2378
				ChartBorderSkinType,
				// Token: 0x0400094B RID: 2379
				Style,
				// Token: 0x0400094C RID: 2380
				PropertyCount
			}
		}
	}
}
