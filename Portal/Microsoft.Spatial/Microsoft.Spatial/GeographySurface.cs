using System;

namespace Microsoft.Spatial
{
	// Token: 0x0200001D RID: 29
	public abstract class GeographySurface : Geography
	{
		// Token: 0x0600010A RID: 266 RVA: 0x000035E0 File Offset: 0x000017E0
		protected GeographySurface(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}
	}
}
