using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000024 RID: 36
	public abstract class GeometryMultiSurface : GeometryCollection
	{
		// Token: 0x060000EF RID: 239 RVA: 0x0000344C File Offset: 0x0000164C
		internal GeometryMultiSurface(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}
	}
}
