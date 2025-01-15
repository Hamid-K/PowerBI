using System;

namespace Microsoft.Spatial
{
	// Token: 0x0200004C RID: 76
	internal class DataServicesSpatialImplementation : SpatialImplementation
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00005A4F File Offset: 0x00003C4F
		// (set) Token: 0x0600022F RID: 559 RVA: 0x00005A57 File Offset: 0x00003C57
		public override SpatialOperations Operations { get; set; }

		// Token: 0x06000230 RID: 560 RVA: 0x00005A60 File Offset: 0x00003C60
		public override SpatialBuilder CreateBuilder()
		{
			GeographyBuilderImplementation geographyBuilderImplementation = new GeographyBuilderImplementation(this);
			GeometryBuilderImplementation geometryBuilderImplementation = new GeometryBuilderImplementation(this);
			ForwardingSegment forwardingSegment = new ForwardingSegment(geographyBuilderImplementation, geometryBuilderImplementation);
			return new SpatialBuilder(forwardingSegment, forwardingSegment, geographyBuilderImplementation, geometryBuilderImplementation);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00005A96 File Offset: 0x00003C96
		public override GmlFormatter CreateGmlFormatter()
		{
			return new GmlFormatterImplementation(this);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00005A9E File Offset: 0x00003C9E
		public override GeoJsonObjectFormatter CreateGeoJsonObjectFormatter()
		{
			return new GeoJsonObjectFormatterImplementation(this);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00005AA6 File Offset: 0x00003CA6
		public override WellKnownTextSqlFormatter CreateWellKnownTextSqlFormatter()
		{
			return new WellKnownTextSqlFormatterImplementation(this);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00005AAE File Offset: 0x00003CAE
		public override WellKnownTextSqlFormatter CreateWellKnownTextSqlFormatter(bool allowOnlyTwoDimensions)
		{
			return new WellKnownTextSqlFormatterImplementation(this, allowOnlyTwoDimensions);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00005AB7 File Offset: 0x00003CB7
		public override SpatialPipeline CreateValidator()
		{
			return new ForwardingSegment(new SpatialValidatorImplementation());
		}
	}
}
