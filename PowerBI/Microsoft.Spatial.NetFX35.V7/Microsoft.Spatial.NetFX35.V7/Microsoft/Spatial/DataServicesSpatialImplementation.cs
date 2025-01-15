using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000047 RID: 71
	internal class DataServicesSpatialImplementation : SpatialImplementation
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00004D87 File Offset: 0x00002F87
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x00004D8F File Offset: 0x00002F8F
		public override SpatialOperations Operations { get; set; }

		// Token: 0x060001BA RID: 442 RVA: 0x00004D98 File Offset: 0x00002F98
		public override SpatialBuilder CreateBuilder()
		{
			GeographyBuilderImplementation geographyBuilderImplementation = new GeographyBuilderImplementation(this);
			GeometryBuilderImplementation geometryBuilderImplementation = new GeometryBuilderImplementation(this);
			ForwardingSegment forwardingSegment = new ForwardingSegment(geographyBuilderImplementation, geometryBuilderImplementation);
			return new SpatialBuilder(forwardingSegment, forwardingSegment, geographyBuilderImplementation, geometryBuilderImplementation);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00004DCE File Offset: 0x00002FCE
		public override GmlFormatter CreateGmlFormatter()
		{
			return new GmlFormatterImplementation(this);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00004DD6 File Offset: 0x00002FD6
		public override GeoJsonObjectFormatter CreateGeoJsonObjectFormatter()
		{
			return new GeoJsonObjectFormatterImplementation(this);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00004DDE File Offset: 0x00002FDE
		public override WellKnownTextSqlFormatter CreateWellKnownTextSqlFormatter()
		{
			return new WellKnownTextSqlFormatterImplementation(this);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00004DE6 File Offset: 0x00002FE6
		public override WellKnownTextSqlFormatter CreateWellKnownTextSqlFormatter(bool allowOnlyTwoDimensions)
		{
			return new WellKnownTextSqlFormatterImplementation(this, allowOnlyTwoDimensions);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00004DEF File Offset: 0x00002FEF
		public override SpatialPipeline CreateValidator()
		{
			return new ForwardingSegment(new SpatialValidatorImplementation());
		}
	}
}
