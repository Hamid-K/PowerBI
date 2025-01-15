using System;

namespace Microsoft.Spatial
{
	// Token: 0x0200000A RID: 10
	internal class TypeWashedToGeographyLongLatPipeline : TypeWashedPipeline
	{
		// Token: 0x06000075 RID: 117 RVA: 0x00002B6F File Offset: 0x00000D6F
		public TypeWashedToGeographyLongLatPipeline(SpatialPipeline output)
		{
			this.output = output;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002AE6 File Offset: 0x00000CE6
		public override bool IsGeography
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002B84 File Offset: 0x00000D84
		internal override void SetCoordinateSystem(int? epsgId)
		{
			CoordinateSystem coordinateSystem = CoordinateSystem.Geography(epsgId);
			this.output.SetCoordinateSystem(coordinateSystem);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002BA4 File Offset: 0x00000DA4
		internal override void Reset()
		{
			this.output.Reset();
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002BB1 File Offset: 0x00000DB1
		internal override void BeginGeo(SpatialType type)
		{
			this.output.BeginGeography(type);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002BBF File Offset: 0x00000DBF
		internal override void BeginFigure(double coordinate1, double coordinate2, double? coordinate3, double? coordinate4)
		{
			this.output.BeginFigure(new GeographyPosition(coordinate2, coordinate1, coordinate3, coordinate4));
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002BD6 File Offset: 0x00000DD6
		internal override void LineTo(double coordinate1, double coordinate2, double? coordinate3, double? coordinate4)
		{
			this.output.LineTo(new GeographyPosition(coordinate2, coordinate1, coordinate3, coordinate4));
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002BED File Offset: 0x00000DED
		internal override void EndFigure()
		{
			this.output.EndFigure();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002BFA File Offset: 0x00000DFA
		internal override void EndGeo()
		{
			this.output.EndGeography();
		}

		// Token: 0x04000014 RID: 20
		private readonly GeographyPipeline output;
	}
}
