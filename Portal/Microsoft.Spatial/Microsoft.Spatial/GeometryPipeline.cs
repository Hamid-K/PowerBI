using System;

namespace Microsoft.Spatial
{
	// Token: 0x0200001E RID: 30
	public abstract class GeometryPipeline
	{
		// Token: 0x0600010B RID: 267
		public abstract void BeginGeometry(SpatialType type);

		// Token: 0x0600010C RID: 268
		public abstract void BeginFigure(GeometryPosition position);

		// Token: 0x0600010D RID: 269
		public abstract void LineTo(GeometryPosition position);

		// Token: 0x0600010E RID: 270
		public abstract void EndFigure();

		// Token: 0x0600010F RID: 271
		public abstract void EndGeometry();

		// Token: 0x06000110 RID: 272
		public abstract void SetCoordinateSystem(CoordinateSystem coordinateSystem);

		// Token: 0x06000111 RID: 273
		public abstract void Reset();
	}
}
