using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000043 RID: 67
	public abstract class SpatialImplementation
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060001ED RID: 493 RVA: 0x000052FF File Offset: 0x000034FF
		// (set) Token: 0x060001EE RID: 494 RVA: 0x00005306 File Offset: 0x00003506
		public static SpatialImplementation CurrentImplementation
		{
			get
			{
				return SpatialImplementation.spatialImplementation;
			}
			internal set
			{
				SpatialImplementation.spatialImplementation = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060001EF RID: 495
		// (set) Token: 0x060001F0 RID: 496
		public abstract SpatialOperations Operations { get; set; }

		// Token: 0x060001F1 RID: 497
		public abstract SpatialBuilder CreateBuilder();

		// Token: 0x060001F2 RID: 498
		public abstract GeoJsonObjectFormatter CreateGeoJsonObjectFormatter();

		// Token: 0x060001F3 RID: 499
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gml", Justification = "Gml is the common name for this format")]
		public abstract GmlFormatter CreateGmlFormatter();

		// Token: 0x060001F4 RID: 500
		public abstract WellKnownTextSqlFormatter CreateWellKnownTextSqlFormatter();

		// Token: 0x060001F5 RID: 501
		public abstract WellKnownTextSqlFormatter CreateWellKnownTextSqlFormatter(bool allowOnlyTwoDimensions);

		// Token: 0x060001F6 RID: 502
		public abstract SpatialPipeline CreateValidator();

		// Token: 0x060001F7 RID: 503 RVA: 0x00005310 File Offset: 0x00003510
		internal SpatialOperations VerifyAndGetNonNullOperations()
		{
			SpatialOperations operations = this.Operations;
			if (operations == null)
			{
				throw new NotImplementedException(Strings.SpatialImplementation_NoRegisteredOperations);
			}
			return operations;
		}

		// Token: 0x04000049 RID: 73
		private static SpatialImplementation spatialImplementation = new DataServicesSpatialImplementation();
	}
}
