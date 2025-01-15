using System;

namespace Microsoft.InfoNav.Analytics.Clustering
{
	// Token: 0x0200003B RID: 59
	internal enum ClusteringErrorType
	{
		// Token: 0x0400012D RID: 301
		LessThanKDataPoints,
		// Token: 0x0400012E RID: 302
		TooManyDataPoints,
		// Token: 0x0400012F RID: 303
		PrepareSpatialClusteringCalledTwice,
		// Token: 0x04000130 RID: 304
		PrepareSpatialClusteringNotCalled,
		// Token: 0x04000131 RID: 305
		NonExistingColumn,
		// Token: 0x04000132 RID: 306
		InvalidRoleMappingTable,
		// Token: 0x04000133 RID: 307
		MissingMappingColumnName,
		// Token: 0x04000134 RID: 308
		MissingMappingColumnRole,
		// Token: 0x04000135 RID: 309
		MissingRequiredAttributeColumn,
		// Token: 0x04000136 RID: 310
		MissingRequiredItemColumn,
		// Token: 0x04000137 RID: 311
		NonNumericAttributeColumn,
		// Token: 0x04000138 RID: 312
		MissingOutputRole,
		// Token: 0x04000139 RID: 313
		ClusteringAlgorithm,
		// Token: 0x0400013A RID: 314
		SpatialClusteringCalledFromService,
		// Token: 0x0400013B RID: 315
		KMeansClusteringCalledFromService
	}
}
