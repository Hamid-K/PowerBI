using System;
using System.Data.Entity.Spatial;

namespace System.Data.Entity.SqlServer
{
	// Token: 0x02000008 RID: 8
	internal static class IDbSpatialValueExtensionMethods
	{
		// Token: 0x06000039 RID: 57 RVA: 0x0000254D File Offset: 0x0000074D
		internal static IDbSpatialValue AsSpatialValue(this DbGeography geographyValue)
		{
			return new DbGeographyAdapter(geographyValue);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002555 File Offset: 0x00000755
		internal static IDbSpatialValue AsSpatialValue(this DbGeometry geometryValue)
		{
			return new DbGeometryAdapter(geometryValue);
		}
	}
}
