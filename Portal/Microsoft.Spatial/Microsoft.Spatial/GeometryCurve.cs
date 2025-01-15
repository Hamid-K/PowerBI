using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000020 RID: 32
	public abstract class GeometryCurve : Geometry
	{
		// Token: 0x06000118 RID: 280 RVA: 0x00003B2C File Offset: 0x00001D2C
		protected GeometryCurve(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}
	}
}
