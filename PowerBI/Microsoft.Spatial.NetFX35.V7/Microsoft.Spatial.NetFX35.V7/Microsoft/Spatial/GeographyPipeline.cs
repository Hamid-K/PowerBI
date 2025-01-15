using System;

namespace Microsoft.Spatial
{
	// Token: 0x0200000E RID: 14
	public abstract class GeographyPipeline
	{
		// Token: 0x06000089 RID: 137
		public abstract void BeginGeography(SpatialType type);

		// Token: 0x0600008A RID: 138
		public abstract void BeginFigure(GeographyPosition position);

		// Token: 0x0600008B RID: 139
		public abstract void LineTo(GeographyPosition position);

		// Token: 0x0600008C RID: 140
		public abstract void EndFigure();

		// Token: 0x0600008D RID: 141
		public abstract void EndGeography();

		// Token: 0x0600008E RID: 142
		public abstract void SetCoordinateSystem(CoordinateSystem coordinateSystem);

		// Token: 0x0600008F RID: 143
		public abstract void Reset();
	}
}
