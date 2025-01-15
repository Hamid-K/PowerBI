using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000012 RID: 18
	public abstract class GeographyMultiCurve : GeographyCollection
	{
		// Token: 0x0600009B RID: 155 RVA: 0x00002EA4 File Offset: 0x000010A4
		protected GeographyMultiCurve(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}
	}
}
