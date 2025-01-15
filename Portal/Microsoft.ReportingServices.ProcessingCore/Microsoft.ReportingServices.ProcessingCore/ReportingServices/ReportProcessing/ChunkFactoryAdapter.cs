using System;
using System.IO;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200063E RID: 1598
	internal class ChunkFactoryAdapter
	{
		// Token: 0x06005773 RID: 22387 RVA: 0x0016F8AA File Offset: 0x0016DAAA
		internal ChunkFactoryAdapter(IChunkFactory aFactory)
		{
			this.m_chunkFactory = aFactory;
		}

		// Token: 0x06005774 RID: 22388 RVA: 0x0016F8B9 File Offset: 0x0016DAB9
		public Stream CreateReportChunk(string name, ReportProcessing.ReportChunkTypes type, string mimeType)
		{
			return this.m_chunkFactory.CreateChunk(name, type, mimeType);
		}

		// Token: 0x06005775 RID: 22389 RVA: 0x0016F8C9 File Offset: 0x0016DAC9
		public Stream GetReportChunk(string name, ReportProcessing.ReportChunkTypes type, out string mimeType)
		{
			return this.m_chunkFactory.GetChunk(name, type, ChunkMode.Open, out mimeType);
		}

		// Token: 0x06005776 RID: 22390 RVA: 0x0016F8DC File Offset: 0x0016DADC
		public string GetChunkMimeType(string name, ReportProcessing.ReportChunkTypes type)
		{
			string text;
			this.m_chunkFactory.GetChunk(name, type, ChunkMode.Open, out text).Close();
			return text;
		}

		// Token: 0x04002E45 RID: 11845
		private IChunkFactory m_chunkFactory;
	}
}
