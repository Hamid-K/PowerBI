using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000012 RID: 18
	public abstract class GeographyCurve : Geography
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x000035E0 File Offset: 0x000017E0
		protected GeographyCurve(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}
	}
}
