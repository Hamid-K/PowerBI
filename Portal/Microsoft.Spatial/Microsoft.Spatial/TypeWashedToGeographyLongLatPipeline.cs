using System;

namespace Microsoft.Spatial
{
	// Token: 0x0200000A RID: 10
	internal class TypeWashedToGeographyLongLatPipeline : TypeWashedPipeline
	{
		// Token: 0x06000087 RID: 135 RVA: 0x00002F57 File Offset: 0x00001157
		public TypeWashedToGeographyLongLatPipeline(SpatialPipeline output)
		{
			this.output = output;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00002ECE File Offset: 0x000010CE
		public override bool IsGeography
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002F6C File Offset: 0x0000116C
		internal override void SetCoordinateSystem(int? epsgId)
		{
			CoordinateSystem coordinateSystem = CoordinateSystem.Geography(epsgId);
			this.output.SetCoordinateSystem(coordinateSystem);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00002F8C File Offset: 0x0000118C
		internal override void Reset()
		{
			this.output.Reset();
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00002F99 File Offset: 0x00001199
		internal override void BeginGeo(SpatialType type)
		{
			this.output.BeginGeography(type);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00002FA7 File Offset: 0x000011A7
		internal override void BeginFigure(double coordinate1, double coordinate2, double? coordinate3, double? coordinate4)
		{
			this.output.BeginFigure(new GeographyPosition(coordinate2, coordinate1, coordinate3, coordinate4));
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00002FBE File Offset: 0x000011BE
		internal override void LineTo(double coordinate1, double coordinate2, double? coordinate3, double? coordinate4)
		{
			this.output.LineTo(new GeographyPosition(coordinate2, coordinate1, coordinate3, coordinate4));
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00002FD5 File Offset: 0x000011D5
		internal override void EndFigure()
		{
			this.output.EndFigure();
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00002FE2 File Offset: 0x000011E2
		internal override void EndGeo()
		{
			this.output.EndGeography();
		}

		// Token: 0x04000015 RID: 21
		private readonly GeographyPipeline output;
	}
}
