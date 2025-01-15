using System;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200018C RID: 396
	[XmlElementClass("MapShapefile", typeof(MapShapefile))]
	[XmlElementClass("MapSpatialDataSet", typeof(MapSpatialDataSet))]
	[XmlElementClass("MapSpatialDataRegion", typeof(MapSpatialDataRegion))]
	public abstract class MapSpatialData : ReportObject
	{
		// Token: 0x06000CAD RID: 3245 RVA: 0x00021680 File Offset: 0x0001F880
		public MapSpatialData()
		{
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x00021688 File Offset: 0x0001F888
		internal MapSpatialData(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x00021691 File Offset: 0x0001F891
		public override void Initialize()
		{
			base.Initialize();
		}
	}
}
