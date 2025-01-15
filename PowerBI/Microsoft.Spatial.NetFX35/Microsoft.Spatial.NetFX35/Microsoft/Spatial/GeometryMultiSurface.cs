using System;

namespace Microsoft.Spatial
{
	// Token: 0x0200002B RID: 43
	public abstract class GeometryMultiSurface : GeometryCollection
	{
		// Token: 0x06000140 RID: 320 RVA: 0x000040A4 File Offset: 0x000022A4
		internal GeometryMultiSurface(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}
	}
}
