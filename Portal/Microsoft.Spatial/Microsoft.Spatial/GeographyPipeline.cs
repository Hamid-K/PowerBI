using System;

namespace Microsoft.Spatial
{
	// Token: 0x0200000E RID: 14
	public abstract class GeographyPipeline
	{
		// Token: 0x0600009B RID: 155
		public abstract void BeginGeography(SpatialType type);

		// Token: 0x0600009C RID: 156
		public abstract void BeginFigure(GeographyPosition position);

		// Token: 0x0600009D RID: 157
		public abstract void LineTo(GeographyPosition position);

		// Token: 0x0600009E RID: 158
		public abstract void EndFigure();

		// Token: 0x0600009F RID: 159
		public abstract void EndGeography();

		// Token: 0x060000A0 RID: 160
		public abstract void SetCoordinateSystem(CoordinateSystem coordinateSystem);

		// Token: 0x060000A1 RID: 161
		public abstract void Reset();
	}
}
