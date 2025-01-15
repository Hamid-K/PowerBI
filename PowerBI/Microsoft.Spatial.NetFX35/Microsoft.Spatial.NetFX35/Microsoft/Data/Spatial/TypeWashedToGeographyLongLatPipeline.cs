using System;
using Microsoft.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x02000010 RID: 16
	internal class TypeWashedToGeographyLongLatPipeline : TypeWashedPipeline
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x00002D4F File Offset: 0x00000F4F
		public TypeWashedToGeographyLongLatPipeline(SpatialPipeline output)
		{
			this.output = output;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00002D63 File Offset: 0x00000F63
		public override bool IsGeography
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00002D68 File Offset: 0x00000F68
		internal override void SetCoordinateSystem(int? epsgId)
		{
			CoordinateSystem coordinateSystem = CoordinateSystem.Geography(epsgId);
			this.output.SetCoordinateSystem(coordinateSystem);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00002D88 File Offset: 0x00000F88
		internal override void Reset()
		{
			this.output.Reset();
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00002D95 File Offset: 0x00000F95
		internal override void BeginGeo(SpatialType type)
		{
			this.output.BeginGeography(type);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00002DA3 File Offset: 0x00000FA3
		internal override void BeginFigure(double coordinate1, double coordinate2, double? coordinate3, double? coordinate4)
		{
			this.output.BeginFigure(new GeographyPosition(coordinate2, coordinate1, coordinate3, coordinate4));
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00002DBA File Offset: 0x00000FBA
		internal override void LineTo(double coordinate1, double coordinate2, double? coordinate3, double? coordinate4)
		{
			this.output.LineTo(new GeographyPosition(coordinate2, coordinate1, coordinate3, coordinate4));
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00002DD1 File Offset: 0x00000FD1
		internal override void EndFigure()
		{
			this.output.EndFigure();
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00002DDE File Offset: 0x00000FDE
		internal override void EndGeo()
		{
			this.output.EndGeography();
		}

		// Token: 0x04000014 RID: 20
		private readonly GeographyPipeline output;
	}
}
