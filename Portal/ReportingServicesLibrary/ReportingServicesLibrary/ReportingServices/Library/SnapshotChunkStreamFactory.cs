using System;
using System.Data;
using System.IO;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000286 RID: 646
	internal sealed class SnapshotChunkStreamFactory
	{
		// Token: 0x06001781 RID: 6017 RVA: 0x0005F24C File Offset: 0x0005D44C
		internal static BufferedCompressedReadStream GetCompressedContiguousChunk(Guid snapshotDataID, bool isPermanentSnapshot, string chunkName, int chunkType, ConnectionManager connectionManager, out string mimeType)
		{
			return SnapshotChunkStreamFactory.CreateReadStream(snapshotDataID, isPermanentSnapshot, chunkName, chunkType, false, connectionManager, false, WriteOptions.None, true, out mimeType) as BufferedCompressedReadStream;
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x0005F270 File Offset: 0x0005D470
		internal static Stream CreateReadStream(Guid snapshotDataID, bool isPermanentSnapshot, string chunkName, int chunkType, ConnectionManager connectionManager, bool supportReadWrite, WriteOptions updateMode, bool isForUpgrade, out string mimeType)
		{
			return SnapshotChunkStreamFactory.CreateReadStream(snapshotDataID, isPermanentSnapshot, chunkName, chunkType, false, connectionManager, supportReadWrite, updateMode, isForUpgrade, out mimeType);
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x0005F294 File Offset: 0x0005D494
		private static Stream CreateReadStream(Guid snapshotDataID, bool isPermanentSnapshot, string chunkName, int chunkType, bool rawStreamRequested, ConnectionManager connectionManager, bool supportReadWrite, WriteOptions updateMode, bool isForUpgrade, out string mimeType)
		{
			if (supportReadWrite)
			{
				RSTrace.CatalogTrace.Assert(updateMode == WriteOptions.Update || updateMode == WriteOptions.Version, "invalid updateMode when supportReadWrite=true");
			}
			else
			{
				RSTrace.CatalogTrace.Assert(updateMode == WriteOptions.None, "invalid updateMode when supportReadWrite=false");
			}
			mimeType = null;
			Stream stream = null;
			SqlChunkReadStream sqlChunkReadStream = null;
			FileChunkReadStream fileChunkReadStream = null;
			ChunkConnectionManager chunkConnectionManager = null;
			try
			{
				chunkConnectionManager = new ChunkConnectionManager(connectionManager);
				if (chunkConnectionManager.IsConnectionOwner)
				{
					chunkConnectionManager.IsolationLevel = IsolationLevel.RepeatableRead;
					chunkConnectionManager.ConnectionManager.WillDisconnectStorage();
				}
				if (chunkConnectionManager.DbInterface.IsSegmentedChunk(snapshotDataID, isPermanentSnapshot, chunkName, chunkType))
				{
					Stream stream2 = SegmentedChunkStorage.Open(snapshotDataID, isPermanentSnapshot, chunkName, chunkType, chunkConnectionManager, updateMode);
					RSTrace.CatalogTrace.Assert(stream2 != null, "segmentedChunkStore");
					IHasMimeType hasMimeType = stream2 as IHasMimeType;
					if (hasMimeType != null)
					{
						mimeType = hasMimeType.MimeType;
					}
					stream = (supportReadWrite ? BufferedReadWriteStream.GetReadWriteStream(stream2, Global.SqlStreamingBufferSize) : BufferedReadWriteStream.GetReadOnlyStream(stream2, Global.SqlStreamingBufferSize));
				}
				else
				{
					sqlChunkReadStream = new SqlChunkReadStream(snapshotDataID, isPermanentSnapshot, chunkName, chunkType, chunkConnectionManager, isForUpgrade);
					if (sqlChunkReadStream.IsAvailable)
					{
						if ((sqlChunkReadStream.Flags & ChunkFlags.FileSystem) == ChunkFlags.FileSystem)
						{
							RSTrace.ChunkTracer.Assert(!isPermanentSnapshot, "permanent chunk is stored in filesystem");
							fileChunkReadStream = new FileChunkReadStream(snapshotDataID, isPermanentSnapshot, chunkName, chunkType);
							if (fileChunkReadStream.IsAvailable)
							{
								if ((sqlChunkReadStream.Flags & ChunkFlags.Compressed) == ChunkFlags.Compressed && !rawStreamRequested)
								{
									stream = new BufferedCompressedReadStream(fileChunkReadStream, fileChunkReadStream.Version);
								}
								else
								{
									stream = new BufferedReadStream(fileChunkReadStream, Global.SqlStreamingBufferSize);
								}
							}
						}
						if (stream == null)
						{
							if ((sqlChunkReadStream.Flags & ChunkFlags.Compressed) == ChunkFlags.Compressed && !rawStreamRequested)
							{
								stream = new BufferedCompressedReadStream(sqlChunkReadStream, sqlChunkReadStream.Version);
							}
							else
							{
								stream = new BufferedReadStream(sqlChunkReadStream, Global.SqlStreamingBufferSize);
							}
						}
					}
					mimeType = sqlChunkReadStream.MimeType;
				}
			}
			finally
			{
				if (sqlChunkReadStream != null && fileChunkReadStream != null)
				{
					sqlChunkReadStream.Close();
				}
				if (stream == null && chunkConnectionManager != null && chunkConnectionManager.IsConnectionOwner)
				{
					chunkConnectionManager.ConnectionManager.DisconnectStorage();
				}
			}
			return stream;
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x0005F46C File Offset: 0x0005D66C
		internal static Stream CreateWriteStream(Guid snapshotDataID, bool isPermanentSnapshot, string chunkName, int chunkType, string mimeType, int bufferSize, ConnectionManager connectionManager, WriteOptions writeOptions, bool supportReadWrite)
		{
			bool flag = Global.UseLocalFileStoreForChunkType(isPermanentSnapshot, chunkType);
			bool flag2 = true;
			if (mimeType != null && Array.IndexOf<string>(SnapshotChunkStreamFactory.COMPRESSED_IMAGE_TYPE_LIST, mimeType.ToUpperInvariant()) != -1)
			{
				flag2 = false;
			}
			bool flag3 = Global.CompressSnapshots(isPermanentSnapshot) && flag2;
			ChunkFlags chunkFlags = ChunkFlags.None;
			if (flag)
			{
				chunkFlags |= ChunkFlags.FileSystem;
			}
			if (flag3)
			{
				chunkFlags |= ChunkFlags.Compressed;
			}
			if (flag)
			{
				RSTrace.ChunkTracer.Assert(!isPermanentSnapshot, "cannot create permanent chunk in filesystem");
			}
			ChunkConnectionManager chunkConnectionManager = new ChunkConnectionManager(connectionManager);
			if (chunkConnectionManager.IsConnectionOwner)
			{
				chunkConnectionManager.IsolationLevel = IsolationLevel.ReadCommitted;
				chunkConnectionManager.ConnectionManager.WillDisconnectStorage();
			}
			if (writeOptions != WriteOptions.Version)
			{
				bool flag4 = writeOptions == WriteOptions.Update;
			}
			Stream stream = SegmentedChunkStorage.OpenOrCreate(snapshotDataID, isPermanentSnapshot, chunkName, mimeType, chunkType, chunkFlags, ChunkHeader.CurrentVersion, chunkConnectionManager, writeOptions);
			if (!supportReadWrite)
			{
				return BufferedReadWriteStream.GetWriteOnlyStream(stream, bufferSize);
			}
			return BufferedReadWriteStream.GetReadWriteStream(stream, bufferSize);
		}

		// Token: 0x04000883 RID: 2179
		private static readonly string[] COMPRESSED_IMAGE_TYPE_LIST = new string[] { "IMAGE/GIF", "IMAGE/JPEG", "IMAGE/PNG" };
	}
}
