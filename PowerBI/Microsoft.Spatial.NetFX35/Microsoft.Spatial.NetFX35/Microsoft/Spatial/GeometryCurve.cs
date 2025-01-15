using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000026 RID: 38
	public abstract class GeometryCurve : Geometry
	{
		// Token: 0x0600012F RID: 303 RVA: 0x00003F68 File Offset: 0x00002168
		protected GeometryCurve(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}
	}
}
