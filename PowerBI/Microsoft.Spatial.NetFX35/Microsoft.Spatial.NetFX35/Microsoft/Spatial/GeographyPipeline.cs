using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000004 RID: 4
	public abstract class GeographyPipeline
	{
		// Token: 0x06000036 RID: 54
		public abstract void BeginGeography(SpatialType type);

		// Token: 0x06000037 RID: 55
		public abstract void BeginFigure(GeographyPosition position);

		// Token: 0x06000038 RID: 56
		public abstract void LineTo(GeographyPosition position);

		// Token: 0x06000039 RID: 57
		public abstract void EndFigure();

		// Token: 0x0600003A RID: 58
		public abstract void EndGeography();

		// Token: 0x0600003B RID: 59
		public abstract void SetCoordinateSystem(CoordinateSystem coordinateSystem);

		// Token: 0x0600003C RID: 60
		public abstract void Reset();
	}
}
