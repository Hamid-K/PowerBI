using System;
using Microsoft.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x02000069 RID: 105
	internal class TypeWashedToGeometryPipeline : TypeWashedPipeline
	{
		// Token: 0x060002AE RID: 686 RVA: 0x000078E4 File Offset: 0x00005AE4
		public TypeWashedToGeometryPipeline(SpatialPipeline output)
		{
			this.output = output;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060002AF RID: 687 RVA: 0x000078F8 File Offset: 0x00005AF8
		public override bool IsGeography
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x000078FC File Offset: 0x00005AFC
		internal override void SetCoordinateSystem(int? epsgId)
		{
			CoordinateSystem coordinateSystem = CoordinateSystem.Geometry(epsgId);
			this.output.SetCoordinateSystem(coordinateSystem);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000791C File Offset: 0x00005B1C
		internal override void Reset()
		{
			this.output.Reset();
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00007929 File Offset: 0x00005B29
		internal override void BeginGeo(SpatialType type)
		{
			this.output.BeginGeometry(type);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00007937 File Offset: 0x00005B37
		internal override void BeginFigure(double coordinate1, double coordinate2, double? coordinate3, double? coordinate4)
		{
			this.output.BeginFigure(new GeometryPosition(coordinate1, coordinate2, coordinate3, coordinate4));
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000794E File Offset: 0x00005B4E
		internal override void LineTo(double coordinate1, double coordinate2, double? coordinate3, double? coordinate4)
		{
			this.output.LineTo(new GeometryPosition(coordinate1, coordinate2, coordinate3, coordinate4));
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00007965 File Offset: 0x00005B65
		internal override void EndFigure()
		{
			this.output.EndFigure();
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00007972 File Offset: 0x00005B72
		internal override void EndGeo()
		{
			this.output.EndGeometry();
		}

		// Token: 0x040000B8 RID: 184
		private readonly GeometryPipeline output;
	}
}
