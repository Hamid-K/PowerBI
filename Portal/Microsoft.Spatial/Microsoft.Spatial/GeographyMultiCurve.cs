using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000014 RID: 20
	public abstract class GeographyMultiCurve : GeographyCollection
	{
		// Token: 0x060000DA RID: 218 RVA: 0x0000364C File Offset: 0x0000184C
		protected GeographyMultiCurve(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}
	}
}
