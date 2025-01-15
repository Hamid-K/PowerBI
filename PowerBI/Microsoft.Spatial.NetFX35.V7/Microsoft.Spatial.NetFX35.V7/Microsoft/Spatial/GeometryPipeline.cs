using System;

namespace Microsoft.Spatial
{
	// Token: 0x0200001C RID: 28
	public abstract class GeometryPipeline
	{
		// Token: 0x060000CC RID: 204
		public abstract void BeginGeometry(SpatialType type);

		// Token: 0x060000CD RID: 205
		public abstract void BeginFigure(GeometryPosition position);

		// Token: 0x060000CE RID: 206
		public abstract void LineTo(GeometryPosition position);

		// Token: 0x060000CF RID: 207
		public abstract void EndFigure();

		// Token: 0x060000D0 RID: 208
		public abstract void EndGeometry();

		// Token: 0x060000D1 RID: 209
		public abstract void SetCoordinateSystem(CoordinateSystem coordinateSystem);

		// Token: 0x060000D2 RID: 210
		public abstract void Reset();
	}
}
