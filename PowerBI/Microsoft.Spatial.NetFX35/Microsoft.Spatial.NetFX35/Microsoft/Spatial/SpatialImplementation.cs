using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Data.Spatial;

namespace Microsoft.Spatial
{
	// Token: 0x02000043 RID: 67
	public abstract class SpatialImplementation
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00005131 File Offset: 0x00003331
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x00005138 File Offset: 0x00003338
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

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001BA RID: 442
		// (set) Token: 0x060001BB RID: 443
		public abstract SpatialOperations Operations { get; set; }

		// Token: 0x060001BC RID: 444
		public abstract SpatialBuilder CreateBuilder();

		// Token: 0x060001BD RID: 445
		public abstract GeoJsonObjectFormatter CreateGeoJsonObjectFormatter();

		// Token: 0x060001BE RID: 446
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gml", Justification = "Gml is the common name for this format")]
		public abstract GmlFormatter CreateGmlFormatter();

		// Token: 0x060001BF RID: 447
		public abstract WellKnownTextSqlFormatter CreateWellKnownTextSqlFormatter();

		// Token: 0x060001C0 RID: 448
		public abstract WellKnownTextSqlFormatter CreateWellKnownTextSqlFormatter(bool allowOnlyTwoDimensions);

		// Token: 0x060001C1 RID: 449
		public abstract SpatialPipeline CreateValidator();

		// Token: 0x060001C2 RID: 450 RVA: 0x00005140 File Offset: 0x00003340
		internal SpatialOperations VerifyAndGetNonNullOperations()
		{
			SpatialOperations operations = this.Operations;
			if (operations == null)
			{
				throw new NotImplementedException(Strings.SpatialImplementation_NoRegisteredOperations);
			}
			return operations;
		}

		// Token: 0x04000042 RID: 66
		private static SpatialImplementation spatialImplementation = new DataServicesSpatialImplementation();
	}
}
