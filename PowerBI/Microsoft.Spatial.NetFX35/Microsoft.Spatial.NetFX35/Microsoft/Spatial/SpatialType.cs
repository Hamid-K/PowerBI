using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000037 RID: 55
	[SuppressMessage("Microsoft.Design", "CA1028", Justification = "byte required for packing")]
	public enum SpatialType : byte
	{
		// Token: 0x04000024 RID: 36
		Unknown,
		// Token: 0x04000025 RID: 37
		Point,
		// Token: 0x04000026 RID: 38
		LineString,
		// Token: 0x04000027 RID: 39
		Polygon,
		// Token: 0x04000028 RID: 40
		MultiPoint,
		// Token: 0x04000029 RID: 41
		MultiLineString,
		// Token: 0x0400002A RID: 42
		MultiPolygon,
		// Token: 0x0400002B RID: 43
		Collection,
		// Token: 0x0400002C RID: 44
		FullGlobe = 11
	}
}
