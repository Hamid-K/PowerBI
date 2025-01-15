using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000009 RID: 9
	internal class TypeWashedToGeographyLatLongPipeline : TypeWashedPipeline
	{
		// Token: 0x0600007E RID: 126 RVA: 0x00002EBA File Offset: 0x000010BA
		public TypeWashedToGeographyLatLongPipeline(SpatialPipeline output)
		{
			this.output = output;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00002ECE File Offset: 0x000010CE
		public override bool IsGeography
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002ED4 File Offset: 0x000010D4
		internal override void SetCoordinateSystem(int? epsgId)
		{
			CoordinateSystem coordinateSystem = CoordinateSystem.Geography(epsgId);
			this.output.SetCoordinateSystem(coordinateSystem);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002EF4 File Offset: 0x000010F4
		internal override void Reset()
		{
			this.output.Reset();
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002F01 File Offset: 0x00001101
		internal override void BeginGeo(SpatialType type)
		{
			this.output.BeginGeography(type);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002F0F File Offset: 0x0000110F
		internal override void BeginFigure(double coordinate1, double coordinate2, double? coordinate3, double? coordinate4)
		{
			this.output.BeginFigure(new GeographyPosition(coordinate1, coordinate2, coordinate3, coordinate4));
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002F26 File Offset: 0x00001126
		internal override void LineTo(double coordinate1, double coordinate2, double? coordinate3, double? coordinate4)
		{
			this.output.LineTo(new GeographyPosition(coordinate1, coordinate2, coordinate3, coordinate4));
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002F3D File Offset: 0x0000113D
		internal override void EndFigure()
		{
			this.output.EndFigure();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00002F4A File Offset: 0x0000114A
		internal override void EndGeo()
		{
			this.output.EndGeography();
		}

		// Token: 0x04000014 RID: 20
		private readonly GeographyPipeline output;
	}
}
