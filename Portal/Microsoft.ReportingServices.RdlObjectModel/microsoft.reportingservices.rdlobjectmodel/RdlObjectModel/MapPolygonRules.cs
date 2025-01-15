using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001A7 RID: 423
	public class MapPolygonRules : ReportObject
	{
		// Token: 0x06000DE7 RID: 3559 RVA: 0x00022C78 File Offset: 0x00020E78
		public MapPolygonRules()
		{
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x00022C80 File Offset: 0x00020E80
		internal MapPolygonRules(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x00022C89 File Offset: 0x00020E89
		// (set) Token: 0x06000DEA RID: 3562 RVA: 0x00022C9C File Offset: 0x00020E9C
		public MapColorRule MapColorRule
		{
			get
			{
				return (MapColorRule)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x00022CAB File Offset: 0x00020EAB
		public override void Initialize()
		{
			base.Initialize();
		}

		// Token: 0x020003D3 RID: 979
		internal class Definition : DefinitionStore<MapPolygonRules, MapPolygonRules.Definition.Properties>
		{
			// Token: 0x06001877 RID: 6263 RVA: 0x0003B739 File Offset: 0x00039939
			private Definition()
			{
			}

			// Token: 0x020004EB RID: 1259
			internal enum Properties
			{
				// Token: 0x04001020 RID: 4128
				MapColorRule,
				// Token: 0x04001021 RID: 4129
				PropertyCount
			}
		}
	}
}
