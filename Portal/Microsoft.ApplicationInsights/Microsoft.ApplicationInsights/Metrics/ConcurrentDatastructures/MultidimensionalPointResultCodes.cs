using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.ApplicationInsights.Metrics.ConcurrentDatastructures
{
	// Token: 0x0200003A RID: 58
	[SuppressMessage("Microsoft.Design", "CA1008: Enums should have zero value", Justification = "Crafted these flags to fit into a byte to make the struct container cheaper.")]
	[Flags]
	internal enum MultidimensionalPointResultCodes : byte
	{
		// Token: 0x040000FE RID: 254
		Success_NewPointCreated = 1,
		// Token: 0x040000FF RID: 255
		Success_ExistingPointRetrieved = 2,
		// Token: 0x04000100 RID: 256
		Failure_SubdimensionsCountLimitReached = 8,
		// Token: 0x04000101 RID: 257
		Failure_TotalPointsCountLimitReached = 16,
		// Token: 0x04000102 RID: 258
		Failure_PointDoesNotExistCreationNotRequested = 32,
		// Token: 0x04000103 RID: 259
		Failure_AsyncTimeoutReached = 128
	}
}
