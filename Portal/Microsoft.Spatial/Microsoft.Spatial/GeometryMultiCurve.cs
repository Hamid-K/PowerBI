using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000024 RID: 36
	public abstract class GeometryMultiCurve : GeometryCollection
	{
		// Token: 0x0600014B RID: 331 RVA: 0x00003F5C File Offset: 0x0000215C
		protected GeometryMultiCurve(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}
	}
}
