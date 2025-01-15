using System;

namespace Microsoft.Spatial
{
	// Token: 0x0200001B RID: 27
	public abstract class GeographySurface : Geography
	{
		// Token: 0x060000CB RID: 203 RVA: 0x00002E38 File Offset: 0x00001038
		protected GeographySurface(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}
	}
}
