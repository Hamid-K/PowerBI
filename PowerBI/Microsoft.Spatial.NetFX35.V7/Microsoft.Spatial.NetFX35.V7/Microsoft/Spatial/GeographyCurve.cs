using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000010 RID: 16
	public abstract class GeographyCurve : Geography
	{
		// Token: 0x06000095 RID: 149 RVA: 0x00002E38 File Offset: 0x00001038
		protected GeographyCurve(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}
	}
}
