using System;
using System.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x0200004B RID: 75
	internal class DataServicesSpatialImplementation : SpatialImplementation
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00005AC3 File Offset: 0x00003CC3
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x00005ACB File Offset: 0x00003CCB
		public override SpatialOperations Operations { get; set; }

		// Token: 0x060001E7 RID: 487 RVA: 0x00005AD4 File Offset: 0x00003CD4
		public override SpatialBuilder CreateBuilder()
		{
			GeographyBuilderImplementation geographyBuilderImplementation = new GeographyBuilderImplementation(this);
			GeometryBuilderImplementation geometryBuilderImplementation = new GeometryBuilderImplementation(this);
			ForwardingSegment forwardingSegment = new ForwardingSegment(geographyBuilderImplementation, geometryBuilderImplementation);
			return new SpatialBuilder(forwardingSegment, forwardingSegment, geographyBuilderImplementation, geometryBuilderImplementation);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00005B0A File Offset: 0x00003D0A
		public override GmlFormatter CreateGmlFormatter()
		{
			return new GmlFormatterImplementation(this);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00005B12 File Offset: 0x00003D12
		public override GeoJsonObjectFormatter CreateGeoJsonObjectFormatter()
		{
			return new GeoJsonObjectFormatterImplementation(this);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00005B1A File Offset: 0x00003D1A
		public override WellKnownTextSqlFormatter CreateWellKnownTextSqlFormatter()
		{
			return new WellKnownTextSqlFormatterImplementation(this);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00005B22 File Offset: 0x00003D22
		public override WellKnownTextSqlFormatter CreateWellKnownTextSqlFormatter(bool allowOnlyTwoDimensions)
		{
			return new WellKnownTextSqlFormatterImplementation(this, allowOnlyTwoDimensions);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00005B2B File Offset: 0x00003D2B
		public override SpatialPipeline CreateValidator()
		{
			return new ForwardingSegment(new SpatialValidatorImplementation());
		}
	}
}
