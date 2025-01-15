using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x0200003E RID: 62
	public abstract class SpatialImplementation
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000177 RID: 375 RVA: 0x0000462B File Offset: 0x0000282B
		// (set) Token: 0x06000178 RID: 376 RVA: 0x00004632 File Offset: 0x00002832
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

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000179 RID: 377
		// (set) Token: 0x0600017A RID: 378
		public abstract SpatialOperations Operations { get; set; }

		// Token: 0x0600017B RID: 379
		public abstract SpatialBuilder CreateBuilder();

		// Token: 0x0600017C RID: 380
		public abstract GeoJsonObjectFormatter CreateGeoJsonObjectFormatter();

		// Token: 0x0600017D RID: 381
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gml", Justification = "Gml is the common name for this format")]
		public abstract GmlFormatter CreateGmlFormatter();

		// Token: 0x0600017E RID: 382
		public abstract WellKnownTextSqlFormatter CreateWellKnownTextSqlFormatter();

		// Token: 0x0600017F RID: 383
		public abstract WellKnownTextSqlFormatter CreateWellKnownTextSqlFormatter(bool allowOnlyTwoDimensions);

		// Token: 0x06000180 RID: 384
		public abstract SpatialPipeline CreateValidator();

		// Token: 0x06000181 RID: 385 RVA: 0x0000463C File Offset: 0x0000283C
		internal SpatialOperations VerifyAndGetNonNullOperations()
		{
			SpatialOperations operations = this.Operations;
			if (operations == null)
			{
				throw new NotImplementedException(Strings.SpatialImplementation_NoRegisteredOperations);
			}
			return operations;
		}

		// Token: 0x0400003C RID: 60
		private static SpatialImplementation spatialImplementation = new DataServicesSpatialImplementation();
	}
}
