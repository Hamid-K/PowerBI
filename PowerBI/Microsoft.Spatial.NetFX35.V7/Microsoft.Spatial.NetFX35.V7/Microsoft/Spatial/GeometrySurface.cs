using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000027 RID: 39
	public abstract class GeometrySurface : Geometry
	{
		// Token: 0x06000101 RID: 257 RVA: 0x00003384 File Offset: 0x00001584
		internal GeometrySurface(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}
	}
}
