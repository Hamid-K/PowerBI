using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000006 RID: 6
	public abstract class GeometryPipeline
	{
		// Token: 0x06000046 RID: 70
		public abstract void BeginGeometry(SpatialType type);

		// Token: 0x06000047 RID: 71
		public abstract void BeginFigure(GeometryPosition position);

		// Token: 0x06000048 RID: 72
		public abstract void LineTo(GeometryPosition position);

		// Token: 0x06000049 RID: 73
		public abstract void EndFigure();

		// Token: 0x0600004A RID: 74
		public abstract void EndGeometry();

		// Token: 0x0600004B RID: 75
		public abstract void SetCoordinateSystem(CoordinateSystem coordinateSystem);

		// Token: 0x0600004C RID: 76
		public abstract void Reset();
	}
}
