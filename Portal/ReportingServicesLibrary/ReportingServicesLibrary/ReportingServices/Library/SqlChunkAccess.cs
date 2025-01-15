using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000293 RID: 659
	internal sealed class SqlChunkAccess
	{
		// Token: 0x0600181C RID: 6172 RVA: 0x00061FE7 File Offset: 0x000601E7
		public SqlChunkAccess(SnapshotChunkStreamBase chunk, ChunkConnectionManager connectionManager, bool isForUpgrade)
		{
			RSTrace.CatalogTrace.Assert(connectionManager != null, "connectionManager");
			this.m_connectionManager = connectionManager;
			this.m_chunk = chunk;
			this.m_openedForUpgrade = isForUpgrade;
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x00062017 File Offset: 0x00060217
		public ChunkStorage CreateChunk(byte[] buffer, int offset, int count, out object chunkPointer)
		{
			return this.InitializeChunk(buffer, offset, count, out chunkPointer);
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x00062024 File Offset: 0x00060224
		public bool OpenExistingChunk(out object chunkPointer, out long chunkLength, out string mimeType, out ChunkFlags chunkFlags, out short version, out ChunkStorage storage)
		{
			storage = this.GetChunkStorage();
			version = ChunkHeader.MissingVersion;
			try
			{
				if (!storage.GetChunkPointerAndLength(this.m_chunk.SnapshotDataID, this.m_chunk.IsPermanent, this.m_chunk.ChunkName, this.m_chunk.ChunkType, out chunkPointer, out chunkLength, out mimeType, out chunkFlags, out version))
				{
					if (this.IsConnectionOwner)
					{
						storage.AbortTransaction();
						storage.Disconnect();
					}
					return false;
				}
				if (!this.IsOpenedForUpgrade && version != ChunkHeader.CurrentVersion)
				{
					if (RSTrace.ChunkTracer.TraceInfo)
					{
						RSTrace.ChunkTracer.Trace(TraceLevel.Info, "Returning old chunk for: ({0}, '{1}', {2})", new object[]
						{
							this.m_chunk.SnapshotDataID,
							this.m_chunk.ChunkName,
							this.m_chunk.ChunkType
						});
					}
					throw new VersionMismatchException(this.m_chunk.SnapshotDataID, this.m_chunk.IsPermanent);
				}
			}
			catch (Exception)
			{
				if (this.IsConnectionOwner)
				{
					storage.AbortTransaction();
					storage.Disconnect();
				}
				throw;
			}
			return true;
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x0600181F RID: 6175 RVA: 0x00062154 File Offset: 0x00060354
		public bool IsConnectionOwner
		{
			get
			{
				return this.m_connectionManager.IsConnectionOwner;
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06001820 RID: 6176 RVA: 0x00062161 File Offset: 0x00060361
		private bool IsOpenedForUpgrade
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_openedForUpgrade;
			}
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x0006216C File Offset: 0x0006036C
		private ChunkStorage InitializeChunk(byte[] buffer, int offset, int count, out object chunkPointer)
		{
			ChunkStorage chunkStorage = this.GetChunkStorage();
			if (buffer == null)
			{
				buffer = new byte[0];
				count = (offset = 0);
			}
			chunkStorage.CreateChunk(this.m_chunk.SnapshotDataID, this.m_chunk.IsPermanent, this.m_chunk.ChunkName, this.m_chunk.ChunkType, this.m_chunk.MimeType, this.m_chunk.ChunkFlags, ChunkHeader.CurrentVersion, buffer, offset, count, out chunkPointer);
			return chunkStorage;
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x000621E2 File Offset: 0x000603E2
		private ChunkStorage GetChunkStorage()
		{
			return this.m_connectionManager.DbInterface;
		}

		// Token: 0x040008AC RID: 2220
		private readonly ChunkConnectionManager m_connectionManager;

		// Token: 0x040008AD RID: 2221
		private readonly SnapshotChunkStreamBase m_chunk;

		// Token: 0x040008AE RID: 2222
		private readonly bool m_openedForUpgrade;
	}
}
