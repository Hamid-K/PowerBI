using System;
using Microsoft.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x0200000F RID: 15
	internal class TypeWashedToGeographyLatLongPipeline : TypeWashedPipeline
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x00002CB2 File Offset: 0x00000EB2
		public TypeWashedToGeographyLatLongPipeline(SpatialPipeline output)
		{
			this.output = output;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00002CC6 File Offset: 0x00000EC6
		public override bool IsGeography
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00002CCC File Offset: 0x00000ECC
		internal override void SetCoordinateSystem(int? epsgId)
		{
			CoordinateSystem coordinateSystem = CoordinateSystem.Geography(epsgId);
			this.output.SetCoordinateSystem(coordinateSystem);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00002CEC File Offset: 0x00000EEC
		internal override void Reset()
		{
			this.output.Reset();
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00002CF9 File Offset: 0x00000EF9
		internal override void BeginGeo(SpatialType type)
		{
			this.output.BeginGeography(type);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00002D07 File Offset: 0x00000F07
		internal override void BeginFigure(double coordinate1, double coordinate2, double? coordinate3, double? coordinate4)
		{
			this.output.BeginFigure(new GeographyPosition(coordinate1, coordinate2, coordinate3, coordinate4));
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00002D1E File Offset: 0x00000F1E
		internal override void LineTo(double coordinate1, double coordinate2, double? coordinate3, double? coordinate4)
		{
			this.output.LineTo(new GeographyPosition(coordinate1, coordinate2, coordinate3, coordinate4));
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00002D35 File Offset: 0x00000F35
		internal override void EndFigure()
		{
			this.output.EndFigure();
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00002D42 File Offset: 0x00000F42
		internal override void EndGeo()
		{
			this.output.EndGeography();
		}

		// Token: 0x04000013 RID: 19
		private readonly GeographyPipeline output;
	}
}
