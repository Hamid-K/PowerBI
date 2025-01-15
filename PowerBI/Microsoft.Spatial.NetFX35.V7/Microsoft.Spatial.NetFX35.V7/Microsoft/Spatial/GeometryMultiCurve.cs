using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000020 RID: 32
	public abstract class GeometryMultiCurve : GeometryCollection
	{
		// Token: 0x060000DF RID: 223 RVA: 0x0000344C File Offset: 0x0000164C
		protected GeometryMultiCurve(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}
	}
}
