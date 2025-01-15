using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000009 RID: 9
	internal class TypeWashedToGeographyLatLongPipeline : TypeWashedPipeline
	{
		// Token: 0x0600006C RID: 108 RVA: 0x00002AD2 File Offset: 0x00000CD2
		public TypeWashedToGeographyLatLongPipeline(SpatialPipeline output)
		{
			this.output = output;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002AE6 File Offset: 0x00000CE6
		public override bool IsGeography
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002AEC File Offset: 0x00000CEC
		internal override void SetCoordinateSystem(int? epsgId)
		{
			CoordinateSystem coordinateSystem = CoordinateSystem.Geography(epsgId);
			this.output.SetCoordinateSystem(coordinateSystem);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002B0C File Offset: 0x00000D0C
		internal override void Reset()
		{
			this.output.Reset();
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002B19 File Offset: 0x00000D19
		internal override void BeginGeo(SpatialType type)
		{
			this.output.BeginGeography(type);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002B27 File Offset: 0x00000D27
		internal override void BeginFigure(double coordinate1, double coordinate2, double? coordinate3, double? coordinate4)
		{
			this.output.BeginFigure(new GeographyPosition(coordinate1, coordinate2, coordinate3, coordinate4));
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002B3E File Offset: 0x00000D3E
		internal override void LineTo(double coordinate1, double coordinate2, double? coordinate3, double? coordinate4)
		{
			this.output.LineTo(new GeographyPosition(coordinate1, coordinate2, coordinate3, coordinate4));
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002B55 File Offset: 0x00000D55
		internal override void EndFigure()
		{
			this.output.EndFigure();
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002B62 File Offset: 0x00000D62
		internal override void EndGeo()
		{
			this.output.EndGeography();
		}

		// Token: 0x04000013 RID: 19
		private readonly GeographyPipeline output;
	}
}
