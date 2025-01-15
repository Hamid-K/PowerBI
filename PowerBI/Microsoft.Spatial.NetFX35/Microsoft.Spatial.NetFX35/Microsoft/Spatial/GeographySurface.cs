using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000018 RID: 24
	public abstract class GeographySurface : Geography
	{
		// Token: 0x060000F0 RID: 240 RVA: 0x0000392D File Offset: 0x00001B2D
		protected GeographySurface(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}
	}
}
