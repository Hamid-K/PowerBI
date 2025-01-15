using System;
using System.IO;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x020008A8 RID: 2216
	internal sealed class ChunkFactoryStreamHandler : IStreamHandler
	{
		// Token: 0x06007935 RID: 31029 RVA: 0x001F3545 File Offset: 0x001F1745
		internal ChunkFactoryStreamHandler(string chunkName, ReportProcessing.ReportChunkTypes chunkType, IChunkFactory chunkFactory, bool existingChunk)
		{
			this.m_chunkName = chunkName;
			this.m_chunkType = chunkType;
			this.m_chunkFactory = chunkFactory;
			this.m_existingChunk = existingChunk;
		}

		// Token: 0x06007936 RID: 31030 RVA: 0x001F356C File Offset: 0x001F176C
		public Stream OpenStream()
		{
			if (this.m_existingChunk)
			{
				string text;
				return this.m_chunkFactory.GetChunk(this.m_chunkName, this.m_chunkType, ChunkMode.Open, out text);
			}
			return this.m_chunkFactory.CreateChunk(this.m_chunkName, this.m_chunkType, null);
		}

		// Token: 0x04003CDB RID: 15579
		private string m_chunkName;

		// Token: 0x04003CDC RID: 15580
		private ReportProcessing.ReportChunkTypes m_chunkType;

		// Token: 0x04003CDD RID: 15581
		private IChunkFactory m_chunkFactory;

		// Token: 0x04003CDE RID: 15582
		private bool m_existingChunk;
	}
}
