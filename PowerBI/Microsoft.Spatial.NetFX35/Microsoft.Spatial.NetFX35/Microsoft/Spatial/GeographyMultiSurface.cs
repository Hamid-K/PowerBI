using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000020 RID: 32
	public abstract class GeographyMultiSurface : GeographyCollection
	{
		// Token: 0x0600010B RID: 267 RVA: 0x00003B3C File Offset: 0x00001D3C
		protected GeographyMultiSurface(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}
	}
}
