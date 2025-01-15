using System;
using Microsoft.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x0200000E RID: 14
	internal abstract class TypeWashedPipeline
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600009F RID: 159
		public abstract bool IsGeography { get; }

		// Token: 0x060000A0 RID: 160
		internal abstract void SetCoordinateSystem(int? epsgId);

		// Token: 0x060000A1 RID: 161
		internal abstract void Reset();

		// Token: 0x060000A2 RID: 162
		internal abstract void BeginGeo(SpatialType type);

		// Token: 0x060000A3 RID: 163
		internal abstract void BeginFigure(double coordinate1, double coordinate2, double? coordinate3, double? coordinate4);

		// Token: 0x060000A4 RID: 164
		internal abstract void LineTo(double coordinate1, double coordinate2, double? coordinate3, double? coordinate4);

		// Token: 0x060000A5 RID: 165
		internal abstract void EndFigure();

		// Token: 0x060000A6 RID: 166
		internal abstract void EndGeo();
	}
}
