using System;
using System.ComponentModel;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x02000010 RID: 16
	internal class GridLines2005 : ChartGridLines
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00002B17 File Offset: 0x00000D17
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00002B25 File Offset: 0x00000D25
		[DefaultValue(false)]
		public bool ShowGridLines
		{
			get
			{
				return base.PropertyStore.GetBoolean(6);
			}
			set
			{
				base.PropertyStore.SetBoolean(6, value);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00002B34 File Offset: 0x00000D34
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00002B47 File Offset: 0x00000D47
		public new Style2005 Style
		{
			get
			{
				return (Style2005)base.PropertyStore.GetObject(7);
			}
			set
			{
				base.PropertyStore.SetObject(7, value);
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00002B56 File Offset: 0x00000D56
		public GridLines2005()
		{
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00002B5E File Offset: 0x00000D5E
		public GridLines2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x020002F9 RID: 761
		internal new class Definition : DefinitionStore<GridLines2005, GridLines2005.Definition.Properties>
		{
			// Token: 0x060016F5 RID: 5877 RVA: 0x00036442 File Offset: 0x00034642
			private Definition()
			{
			}

			// Token: 0x0200042D RID: 1069
			public enum Properties
			{
				// Token: 0x04000847 RID: 2119
				ShowGridLines = 6,
				// Token: 0x04000848 RID: 2120
				Style
			}
		}
	}
}
