using System;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x02000011 RID: 17
	internal class PlotArea2005 : ReportObject
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00002B67 File Offset: 0x00000D67
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00002B7A File Offset: 0x00000D7A
		public Style2005 Style
		{
			get
			{
				return (Style2005)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00002B89 File Offset: 0x00000D89
		public PlotArea2005()
		{
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00002B91 File Offset: 0x00000D91
		public PlotArea2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x020002FA RID: 762
		internal class Definition : DefinitionStore<PlotArea2005, PlotArea2005.Definition.Properties>
		{
			// Token: 0x060016F6 RID: 5878 RVA: 0x0003644A File Offset: 0x0003464A
			private Definition()
			{
			}

			// Token: 0x0200042E RID: 1070
			public enum Properties
			{
				// Token: 0x0400084A RID: 2122
				Style
			}
		}
	}
}
