using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000065 RID: 101
	internal abstract class TypeWashedPipeline
	{
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060002B9 RID: 697
		public abstract bool IsGeography { get; }

		// Token: 0x060002BA RID: 698
		internal abstract void SetCoordinateSystem(int? epsgId);

		// Token: 0x060002BB RID: 699
		internal abstract void Reset();

		// Token: 0x060002BC RID: 700
		internal abstract void BeginGeo(SpatialType type);

		// Token: 0x060002BD RID: 701
		internal abstract void BeginFigure(double coordinate1, double coordinate2, double? coordinate3, double? coordinate4);

		// Token: 0x060002BE RID: 702
		internal abstract void LineTo(double coordinate1, double coordinate2, double? coordinate3, double? coordinate4);

		// Token: 0x060002BF RID: 703
		internal abstract void EndFigure();

		// Token: 0x060002C0 RID: 704
		internal abstract void EndGeo();
	}
}
