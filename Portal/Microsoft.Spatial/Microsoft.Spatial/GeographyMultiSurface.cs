using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000018 RID: 24
	public abstract class GeographyMultiSurface : GeographyCollection
	{
		// Token: 0x060000EA RID: 234 RVA: 0x0000364C File Offset: 0x0000184C
		protected GeographyMultiSurface(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}
	}
}
