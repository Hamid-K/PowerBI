using System;
using System.IO;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000284 RID: 644
	internal struct ReadOnlyChunkFactory : IChunkFactory
	{
		// Token: 0x06001777 RID: 6007 RVA: 0x0005ED86 File Offset: 0x0005CF86
		public static ReadOnlyChunkFactory FromSnapshot(ReportSnapshot snapshot)
		{
			return new ReadOnlyChunkFactory(snapshot);
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x0005ED8E File Offset: 0x0005CF8E
		private ReadOnlyChunkFactory(IChunkFactory chunkFactory)
		{
			RSTrace.CatalogTrace.Assert(chunkFactory != null, "chunkFactory");
			this.m_chunkFactory = chunkFactory;
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x0005EDAA File Offset: 0x0005CFAA
		public Stream CreateChunk(string chunkName, ReportProcessing.ReportChunkTypes type, string mimeType)
		{
			throw new InternalCatalogException("Attempt to create chunk in ReadOnlyChunkFactory");
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x0005EDB6 File Offset: 0x0005CFB6
		public Stream GetChunk(string chunkName, ReportProcessing.ReportChunkTypes type, ChunkMode mode, out string mimeType)
		{
			return this.m_chunkFactory.GetChunk(chunkName, type, mode, out mimeType);
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x0005EDC8 File Offset: 0x0005CFC8
		public bool Erase(string chunkName, ReportProcessing.ReportChunkTypes type)
		{
			throw new InternalCatalogException("Attempt to erase chunk in ReadOnlyChunkFactory");
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x0600177C RID: 6012 RVA: 0x0005EDD4 File Offset: 0x0005CFD4
		public ReportProcessingFlags ReportProcessingFlags
		{
			get
			{
				return this.m_chunkFactory.ReportProcessingFlags;
			}
		}

		// Token: 0x04000881 RID: 2177
		private readonly IChunkFactory m_chunkFactory;
	}
}
