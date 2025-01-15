using System;

namespace Microsoft.Spatial
{
	// Token: 0x0200002B RID: 43
	public abstract class GeometrySurface : Geometry
	{
		// Token: 0x0600016D RID: 365 RVA: 0x00003B2C File Offset: 0x00001D2C
		internal GeometrySurface(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}
	}
}
