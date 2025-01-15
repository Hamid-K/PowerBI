using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000037 RID: 55
	[SuppressMessage("Microsoft.Design", "CA1028", Justification = "byte required for packing")]
	public enum SpatialType : byte
	{
		// Token: 0x0400002D RID: 45
		Unknown,
		// Token: 0x0400002E RID: 46
		Point,
		// Token: 0x0400002F RID: 47
		LineString,
		// Token: 0x04000030 RID: 48
		Polygon,
		// Token: 0x04000031 RID: 49
		MultiPoint,
		// Token: 0x04000032 RID: 50
		MultiLineString,
		// Token: 0x04000033 RID: 51
		MultiPolygon,
		// Token: 0x04000034 RID: 52
		Collection,
		// Token: 0x04000035 RID: 53
		FullGlobe = 11
	}
}
