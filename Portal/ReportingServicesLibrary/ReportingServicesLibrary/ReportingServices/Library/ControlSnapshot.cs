using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200029A RID: 666
	[Serializable]
	internal sealed class ControlSnapshot : SnapshotBase, IChunkFactory, IDisposable
	{
		// Token: 0x06001839 RID: 6201 RVA: 0x000624E2 File Offset: 0x000606E2
		public ControlSnapshot()
			: base(false)
		{
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x000624F6 File Offset: 0x000606F6
		public void Dispose()
		{
			this.DeleteSnapshotAndChunks();
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x0000289C File Offset: 0x00000A9C
		public override SnapshotBase Duplicate()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x00062504 File Offset: 0x00060704
		private void CopyAllChunksTo(ControlSnapshot target)
		{
			foreach (ControlSnapshot.Chunk chunk in this.m_allChunks)
			{
				if (!target.m_allChunks.Contains(chunk))
				{
					target.m_allChunks.Add(chunk);
				}
			}
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x00062564 File Offset: 0x00060764
		public void CopyDataChunksTo(IChunkFactory chunkFactory, out bool hasDataChunks)
		{
			hasDataChunks = false;
			foreach (ControlSnapshot.Chunk chunk in this.m_allChunks)
			{
				if (chunk.Header.ChunkType == 5)
				{
					hasDataChunks = true;
					using (Stream stream = chunkFactory.CreateChunk(chunk.Header.ChunkName, ReportProcessing.ReportChunkTypes.Data, null))
					{
						chunk.Stream.Position = 0L;
						StreamSupport.CopyStreamUsingBuffer(chunk.Stream, stream, 4096);
					}
				}
			}
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x0006260C File Offset: 0x0006080C
		public override void PrepareExecutionSnapshot(SnapshotBase target, string compiledDefinitionChunkName)
		{
			ControlSnapshot controlSnapshot = (ControlSnapshot)target;
			int imageChunkTypeToCopy = (int)ReportProcessing.GetImageChunkTypeToCopy(ReportProcessingFlags.OnDemandEngine);
			string text = compiledDefinitionChunkName;
			if (text == null)
			{
				text = "CompiledDefinition";
			}
			foreach (ControlSnapshot.Chunk chunk in this.m_allChunks)
			{
				ChunkHeader header = chunk.Header;
				if (header.ChunkType == imageChunkTypeToCopy)
				{
					controlSnapshot.m_allChunks.Add(chunk);
				}
				else if (header.ChunkName.Equals("CompiledDefinition", StringComparison.Ordinal))
				{
					ControlSnapshot.Chunk chunk2 = new ControlSnapshot.Chunk(chunk);
					chunk2.Header.ChunkName = text;
					controlSnapshot.m_allChunks.Add(chunk2);
				}
				else if (header.ChunkType == 5 || header.ChunkType == 9)
				{
					ControlSnapshot.Chunk chunk3 = new ControlSnapshot.Chunk(chunk);
					controlSnapshot.m_allChunks.Add(chunk3);
				}
			}
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x000626F8 File Offset: 0x000608F8
		[Obsolete("Use PrepareExecutionSnapshot instead")]
		public override void CopyImageChunksTo(SnapshotBase target)
		{
			ControlSnapshot controlSnapshot = (ControlSnapshot)target;
			this.CopyAllChunksTo(controlSnapshot);
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x00062714 File Offset: 0x00060914
		public override void DeleteSnapshotAndChunks()
		{
			foreach (ControlSnapshot.Chunk chunk in this.m_allChunks)
			{
				chunk.Stream.CanBeClosed = true;
				chunk.Dispose();
			}
			this.m_allChunks.Clear();
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x00062778 File Offset: 0x00060978
		public override Stream GetChunk(string name, ReportProcessing.ReportChunkTypes type, out string mimeType)
		{
			ControlSnapshot.Chunk chunkImpl = this.GetChunkImpl(name, type);
			if (chunkImpl == null)
			{
				mimeType = null;
				return null;
			}
			mimeType = chunkImpl.Header.MimeType;
			chunkImpl.Stream.Seek(0L, SeekOrigin.Begin);
			if (chunkImpl.Header.ChunkFlag == ChunkFlags.Compressed)
			{
				throw new InternalCatalogException("Cannot read compressed chunk.");
			}
			return chunkImpl.Stream;
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x000627D4 File Offset: 0x000609D4
		public override string GetStreamMimeType(string name, ReportProcessing.ReportChunkTypes type)
		{
			ControlSnapshot.Chunk chunkImpl = this.GetChunkImpl(name, type);
			if (chunkImpl == null)
			{
				return null;
			}
			return chunkImpl.Header.MimeType;
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x000627FC File Offset: 0x000609FC
		public override Stream CreateChunk(string name, ReportProcessing.ReportChunkTypes type, string mimeType)
		{
			this.Erase(name, type);
			ControlSnapshot.Chunk chunk = new ControlSnapshot.Chunk(mimeType, name, type);
			this.m_allChunks.Add(chunk);
			return chunk.Stream;
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x00062830 File Offset: 0x00060A30
		public Stream GetChunk(string chunkName, ReportProcessing.ReportChunkTypes chunkType, ChunkMode chunkMode, out string mimeType)
		{
			Stream stream = this.GetChunk(chunkName, chunkType, out mimeType);
			if (chunkMode == ChunkMode.OpenOrCreate && stream == null)
			{
				mimeType = null;
				stream = this.CreateChunk(chunkName, chunkType, mimeType);
			}
			return stream;
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x00062860 File Offset: 0x00060A60
		public bool Erase(string chunkName, ReportProcessing.ReportChunkTypes type)
		{
			foreach (ControlSnapshot.Chunk chunk in this.m_allChunks)
			{
				if (chunk.Header.ChunkName == chunkName && chunk.Header.ChunkType == (int)type)
				{
					this.m_allChunks.Remove(chunk);
					return true;
				}
			}
			return false;
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06001846 RID: 6214 RVA: 0x000053DC File Offset: 0x000035DC
		public ReportProcessingFlags ReportProcessingFlags
		{
			get
			{
				return ReportProcessingFlags.OnDemandEngine;
			}
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x000628DC File Offset: 0x00060ADC
		private ControlSnapshot.Chunk GetChunkImpl(string name, ReportProcessing.ReportChunkTypes type)
		{
			foreach (ControlSnapshot.Chunk chunk in this.m_allChunks)
			{
				if (chunk.Header.ChunkName == name && chunk.Header.ChunkType == (int)type)
				{
					return chunk;
				}
			}
			return null;
		}

		// Token: 0x040008BF RID: 2239
		private IList<ControlSnapshot.Chunk> m_allChunks = new List<ControlSnapshot.Chunk>();

		// Token: 0x020004D1 RID: 1233
		[Serializable]
		private class Chunk : IDisposable
		{
			// Token: 0x06002464 RID: 9316 RVA: 0x0008612F File Offset: 0x0008432F
			public Chunk(string mimeType, string name, ReportProcessing.ReportChunkTypes type)
			{
				this.m_header = new ChunkHeader(name, (int)type, ChunkFlags.None, mimeType, ChunkHeader.CurrentVersion, 0L);
			}

			// Token: 0x06002465 RID: 9317 RVA: 0x00086158 File Offset: 0x00084358
			public Chunk(ControlSnapshot.Chunk baseChunk)
			{
				this.m_header = new ChunkHeader(baseChunk.Header);
				this.m_stream = baseChunk.Stream;
			}

			// Token: 0x06002466 RID: 9318 RVA: 0x00086188 File Offset: 0x00084388
			public void Dispose()
			{
				if (this.m_stream != null)
				{
					this.m_stream.Dispose();
					this.m_stream = null;
				}
				GC.SuppressFinalize(this);
			}

			// Token: 0x17000A9F RID: 2719
			// (get) Token: 0x06002467 RID: 9319 RVA: 0x000861AA File Offset: 0x000843AA
			public ChunkHeader Header
			{
				get
				{
					return this.m_header;
				}
			}

			// Token: 0x17000AA0 RID: 2720
			// (get) Token: 0x06002468 RID: 9320 RVA: 0x000861B2 File Offset: 0x000843B2
			public ChunkMemoryStream Stream
			{
				get
				{
					return this.m_stream;
				}
			}

			// Token: 0x0400111A RID: 4378
			private ChunkHeader m_header;

			// Token: 0x0400111B RID: 4379
			private ChunkMemoryStream m_stream = new ChunkMemoryStream();
		}
	}
}
