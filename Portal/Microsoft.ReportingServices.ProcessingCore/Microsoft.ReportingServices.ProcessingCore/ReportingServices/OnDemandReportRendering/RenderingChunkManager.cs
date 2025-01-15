using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001FD RID: 509
	internal sealed class RenderingChunkManager
	{
		// Token: 0x06001304 RID: 4868 RVA: 0x0004D376 File Offset: 0x0004B576
		internal RenderingChunkManager(string rendererID, IChunkFactory chunkFactory)
		{
			this.m_rendererID = rendererID;
			this.m_chunkFactory = chunkFactory;
			this.m_chunks = new Dictionary<string, Stream>();
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x0004D398 File Offset: 0x0004B598
		internal Stream GetOrCreateChunk(ReportProcessing.ReportChunkTypes type, string chunkName, bool createChunkIfNotExists, out bool isNewChunk)
		{
			isNewChunk = false;
			if (chunkName == null)
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterValue, new object[] { "chunkName" });
			}
			if (this.m_chunks.ContainsKey(chunkName))
			{
				return this.m_chunks[chunkName];
			}
			string text;
			Stream stream = this.m_chunkFactory.GetChunk(chunkName, type, ChunkMode.Open, out text);
			if (createChunkIfNotExists && stream == null)
			{
				stream = this.m_chunkFactory.CreateChunk(chunkName, type, null);
				isNewChunk = true;
			}
			if (stream != null)
			{
				this.m_chunks.Add(chunkName, stream);
			}
			return stream;
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x0004D41C File Offset: 0x0004B61C
		internal Stream CreateChunk(ReportProcessing.ReportChunkTypes type, string chunkName)
		{
			if (chunkName == null)
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterValue, new object[] { "chunkName" });
			}
			Stream stream;
			if (this.m_chunks.TryGetValue(chunkName, out stream))
			{
				stream.Close();
				this.m_chunks.Remove(chunkName);
			}
			stream = this.m_chunkFactory.CreateChunk(chunkName, type, null);
			if (stream != null)
			{
				this.m_chunks.Add(chunkName, stream);
			}
			return stream;
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x0004D488 File Offset: 0x0004B688
		internal void CloseAllChunks()
		{
			foreach (Stream stream in this.m_chunks.Values)
			{
				stream.Close();
			}
			this.m_chunks.Clear();
		}

		// Token: 0x04000929 RID: 2345
		private string m_rendererID;

		// Token: 0x0400092A RID: 2346
		private IChunkFactory m_chunkFactory;

		// Token: 0x0400092B RID: 2347
		private Dictionary<string, Stream> m_chunks;
	}
}
