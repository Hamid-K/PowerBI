using System;

namespace Microsoft.Spatial
{
	// Token: 0x0200001A RID: 26
	public abstract class GeographyCurve : Geography
	{
		// Token: 0x060000F5 RID: 245 RVA: 0x0000399C File Offset: 0x00001B9C
		protected GeographyCurve(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}
	}
}
