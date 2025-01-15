using System;

namespace Microsoft.Spatial
{
	// Token: 0x0200001D RID: 29
	public abstract class GeographyMultiCurve : GeographyCollection
	{
		// Token: 0x06000100 RID: 256 RVA: 0x00003A6C File Offset: 0x00001C6C
		protected GeographyMultiCurve(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}
	}
}
