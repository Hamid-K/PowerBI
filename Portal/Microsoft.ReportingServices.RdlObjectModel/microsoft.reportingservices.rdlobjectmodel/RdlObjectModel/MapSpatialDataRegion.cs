using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200018F RID: 399
	public class MapSpatialDataRegion : MapSpatialData
	{
		// Token: 0x06000CC2 RID: 3266 RVA: 0x00021806 File Offset: 0x0001FA06
		public MapSpatialDataRegion()
		{
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x0002180E File Offset: 0x0001FA0E
		internal MapSpatialDataRegion(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06000CC4 RID: 3268 RVA: 0x00021817 File Offset: 0x0001FA17
		// (set) Token: 0x06000CC5 RID: 3269 RVA: 0x00021825 File Offset: 0x0001FA25
		public ReportExpression VectorData
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x00021839 File Offset: 0x0001FA39
		public override void Initialize()
		{
			base.Initialize();
		}

		// Token: 0x020003BC RID: 956
		internal class Definition : DefinitionStore<MapSpatialDataRegion, MapSpatialDataRegion.Definition.Properties>
		{
			// Token: 0x06001860 RID: 6240 RVA: 0x0003B681 File Offset: 0x00039881
			private Definition()
			{
			}

			// Token: 0x020004D4 RID: 1236
			internal enum Properties
			{
				// Token: 0x04000F2E RID: 3886
				VectorData,
				// Token: 0x04000F2F RID: 3887
				PropertyCount
			}
		}
	}
}
