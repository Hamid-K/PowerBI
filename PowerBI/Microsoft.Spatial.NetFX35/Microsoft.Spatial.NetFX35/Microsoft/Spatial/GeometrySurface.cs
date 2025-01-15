using System;

namespace Microsoft.Spatial
{
	// Token: 0x0200002E RID: 46
	public abstract class GeometrySurface : Geometry
	{
		// Token: 0x06000152 RID: 338 RVA: 0x00004332 File Offset: 0x00002532
		internal GeometrySurface(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}
	}
}
