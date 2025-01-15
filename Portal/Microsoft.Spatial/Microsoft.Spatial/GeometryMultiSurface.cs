using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000028 RID: 40
	public abstract class GeometryMultiSurface : GeometryCollection
	{
		// Token: 0x0600015B RID: 347 RVA: 0x00003F5C File Offset: 0x0000215C
		internal GeometryMultiSurface(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}
	}
}
