using System;
using System.IO;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000640 RID: 1600
	public interface IChunkFactory
	{
		// Token: 0x0600577C RID: 22396
		Stream CreateChunk(string chunkName, ReportProcessing.ReportChunkTypes type, string mimeType);

		// Token: 0x0600577D RID: 22397
		Stream GetChunk(string chunkName, ReportProcessing.ReportChunkTypes type, ChunkMode mode, out string mimeType);

		// Token: 0x0600577E RID: 22398
		bool Erase(string chunkName, ReportProcessing.ReportChunkTypes type);

		// Token: 0x17002001 RID: 8193
		// (get) Token: 0x0600577F RID: 22399
		ReportProcessingFlags ReportProcessingFlags { get; }
	}
}
