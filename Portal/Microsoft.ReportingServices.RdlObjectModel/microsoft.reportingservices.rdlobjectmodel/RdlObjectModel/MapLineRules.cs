using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001A9 RID: 425
	public class MapLineRules : ReportObject
	{
		// Token: 0x06000DF5 RID: 3573 RVA: 0x00022D32 File Offset: 0x00020F32
		public MapLineRules()
		{
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x00022D3A File Offset: 0x00020F3A
		internal MapLineRules(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06000DF7 RID: 3575 RVA: 0x00022D43 File Offset: 0x00020F43
		// (set) Token: 0x06000DF8 RID: 3576 RVA: 0x00022D56 File Offset: 0x00020F56
		public MapSizeRule MapSizeRule
		{
			get
			{
				return (MapSizeRule)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06000DF9 RID: 3577 RVA: 0x00022D65 File Offset: 0x00020F65
		// (set) Token: 0x06000DFA RID: 3578 RVA: 0x00022D78 File Offset: 0x00020F78
		public MapColorRule MapColorRule
		{
			get
			{
				return (MapColorRule)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x00022D87 File Offset: 0x00020F87
		public override void Initialize()
		{
			base.Initialize();
		}

		// Token: 0x020003D5 RID: 981
		internal class Definition : DefinitionStore<MapLineRules, MapLineRules.Definition.Properties>
		{
			// Token: 0x06001879 RID: 6265 RVA: 0x0003B749 File Offset: 0x00039949
			private Definition()
			{
			}

			// Token: 0x020004ED RID: 1261
			internal enum Properties
			{
				// Token: 0x04001028 RID: 4136
				MapSizeRule,
				// Token: 0x04001029 RID: 4137
				MapColorRule,
				// Token: 0x0400102A RID: 4138
				PropertyCount
			}
		}
	}
}
