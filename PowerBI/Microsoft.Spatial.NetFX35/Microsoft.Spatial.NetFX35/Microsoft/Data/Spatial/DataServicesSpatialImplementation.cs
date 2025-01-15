using System;
using Microsoft.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x0200004C RID: 76
	internal class DataServicesSpatialImplementation : SpatialImplementation
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00005A33 File Offset: 0x00003C33
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x00005A3B File Offset: 0x00003C3B
		public override SpatialOperations Operations { get; set; }

		// Token: 0x060001F1 RID: 497 RVA: 0x00005A44 File Offset: 0x00003C44
		public override SpatialBuilder CreateBuilder()
		{
			GeographyBuilderImplementation geographyBuilderImplementation = new GeographyBuilderImplementation(this);
			GeometryBuilderImplementation geometryBuilderImplementation = new GeometryBuilderImplementation(this);
			ForwardingSegment forwardingSegment = new ForwardingSegment(geographyBuilderImplementation, geometryBuilderImplementation);
			return new SpatialBuilder(forwardingSegment, forwardingSegment, geographyBuilderImplementation, geometryBuilderImplementation);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00005A7A File Offset: 0x00003C7A
		public override GmlFormatter CreateGmlFormatter()
		{
			return new GmlFormatterImplementation(this);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00005A82 File Offset: 0x00003C82
		public override GeoJsonObjectFormatter CreateGeoJsonObjectFormatter()
		{
			return new GeoJsonObjectFormatterImplementation(this);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00005A8A File Offset: 0x00003C8A
		public override WellKnownTextSqlFormatter CreateWellKnownTextSqlFormatter()
		{
			return new WellKnownTextSqlFormatterImplementation(this);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00005A92 File Offset: 0x00003C92
		public override WellKnownTextSqlFormatter CreateWellKnownTextSqlFormatter(bool allowOnlyTwoDimensions)
		{
			return new WellKnownTextSqlFormatterImplementation(this, allowOnlyTwoDimensions);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00005A9B File Offset: 0x00003C9B
		public override SpatialPipeline CreateValidator()
		{
			return new ForwardingSegment(new SpatialValidatorImplementation());
		}
	}
}
