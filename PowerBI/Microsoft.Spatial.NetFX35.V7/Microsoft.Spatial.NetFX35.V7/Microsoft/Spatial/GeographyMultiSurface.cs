using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000016 RID: 22
	public abstract class GeographyMultiSurface : GeographyCollection
	{
		// Token: 0x060000AB RID: 171 RVA: 0x00002EA4 File Offset: 0x000010A4
		protected GeographyMultiSurface(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}
	}
}
