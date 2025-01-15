using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000061 RID: 97
	internal class TypeWashedToGeometryPipeline : TypeWashedPipeline
	{
		// Token: 0x0600024C RID: 588 RVA: 0x00005DA6 File Offset: 0x00003FA6
		public TypeWashedToGeometryPipeline(SpatialPipeline output)
		{
			this.output = output;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600024D RID: 589 RVA: 0x000054E6 File Offset: 0x000036E6
		public override bool IsGeography
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00005DBC File Offset: 0x00003FBC
		internal override void SetCoordinateSystem(int? epsgId)
		{
			CoordinateSystem coordinateSystem = CoordinateSystem.Geometry(epsgId);
			this.output.SetCoordinateSystem(coordinateSystem);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00005DDC File Offset: 0x00003FDC
		internal override void Reset()
		{
			this.output.Reset();
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00005DE9 File Offset: 0x00003FE9
		internal override void BeginGeo(SpatialType type)
		{
			this.output.BeginGeometry(type);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00005DF7 File Offset: 0x00003FF7
		internal override void BeginFigure(double coordinate1, double coordinate2, double? coordinate3, double? coordinate4)
		{
			this.output.BeginFigure(new GeometryPosition(coordinate1, coordinate2, coordinate3, coordinate4));
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00005E0E File Offset: 0x0000400E
		internal override void LineTo(double coordinate1, double coordinate2, double? coordinate3, double? coordinate4)
		{
			this.output.LineTo(new GeometryPosition(coordinate1, coordinate2, coordinate3, coordinate4));
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00005E25 File Offset: 0x00004025
		internal override void EndFigure()
		{
			this.output.EndFigure();
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00005E32 File Offset: 0x00004032
		internal override void EndGeo()
		{
			this.output.EndGeometry();
		}

		// Token: 0x0400009F RID: 159
		private readonly GeometryPipeline output;
	}
}
