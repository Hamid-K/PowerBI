using System;

namespace Microsoft.Spatial
{
	// Token: 0x0200001E RID: 30
	public abstract class GeometryCurve : Geometry
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x00003384 File Offset: 0x00001584
		protected GeometryCurve(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}
	}
}
