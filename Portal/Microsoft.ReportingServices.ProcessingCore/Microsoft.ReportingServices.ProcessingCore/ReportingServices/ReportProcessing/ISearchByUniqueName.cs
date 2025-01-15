using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006C2 RID: 1730
	internal interface ISearchByUniqueName
	{
		// Token: 0x06005CFA RID: 23802
		object Find(int targetUniqueName, ref NonComputedUniqueNames nonCompNames, ChunkManager.RenderingChunkManager chunkManager);
	}
}
