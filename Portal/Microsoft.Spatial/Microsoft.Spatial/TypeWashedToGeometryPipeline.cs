using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000066 RID: 102
	internal class TypeWashedToGeometryPipeline : TypeWashedPipeline
	{
		// Token: 0x060002C2 RID: 706 RVA: 0x00006A6E File Offset: 0x00004C6E
		public TypeWashedToGeometryPipeline(SpatialPipeline output)
		{
			this.output = output;
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x000061AE File Offset: 0x000043AE
		public override bool IsGeography
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00006A84 File Offset: 0x00004C84
		internal override void SetCoordinateSystem(int? epsgId)
		{
			CoordinateSystem coordinateSystem = CoordinateSystem.Geometry(epsgId);
			this.output.SetCoordinateSystem(coordinateSystem);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00006AA4 File Offset: 0x00004CA4
		internal override void Reset()
		{
			this.output.Reset();
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00006AB1 File Offset: 0x00004CB1
		internal override void BeginGeo(SpatialType type)
		{
			this.output.BeginGeometry(type);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00006ABF File Offset: 0x00004CBF
		internal override void BeginFigure(double coordinate1, double coordinate2, double? coordinate3, double? coordinate4)
		{
			this.output.BeginFigure(new GeometryPosition(coordinate1, coordinate2, coordinate3, coordinate4));
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00006AD6 File Offset: 0x00004CD6
		internal override void LineTo(double coordinate1, double coordinate2, double? coordinate3, double? coordinate4)
		{
			this.output.LineTo(new GeometryPosition(coordinate1, coordinate2, coordinate3, coordinate4));
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00006AED File Offset: 0x00004CED
		internal override void EndFigure()
		{
			this.output.EndFigure();
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00006AFA File Offset: 0x00004CFA
		internal override void EndGeo()
		{
			this.output.EndGeometry();
		}

		// Token: 0x040000AC RID: 172
		private readonly GeometryPipeline output;
	}
}
