using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000028 RID: 40
	public abstract class GeometryMultiCurve : GeometryCollection
	{
		// Token: 0x06000135 RID: 309 RVA: 0x00003FD4 File Offset: 0x000021D4
		protected GeometryMultiCurve(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}
	}
}
