using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000060 RID: 96
	internal abstract class TypeWashedPipeline
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000243 RID: 579
		public abstract bool IsGeography { get; }

		// Token: 0x06000244 RID: 580
		internal abstract void SetCoordinateSystem(int? epsgId);

		// Token: 0x06000245 RID: 581
		internal abstract void Reset();

		// Token: 0x06000246 RID: 582
		internal abstract void BeginGeo(SpatialType type);

		// Token: 0x06000247 RID: 583
		internal abstract void BeginFigure(double coordinate1, double coordinate2, double? coordinate3, double? coordinate4);

		// Token: 0x06000248 RID: 584
		internal abstract void LineTo(double coordinate1, double coordinate2, double? coordinate3, double? coordinate4);

		// Token: 0x06000249 RID: 585
		internal abstract void EndFigure();

		// Token: 0x0600024A RID: 586
		internal abstract void EndGeo();
	}
}
