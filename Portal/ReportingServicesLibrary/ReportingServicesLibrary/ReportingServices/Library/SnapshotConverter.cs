using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000285 RID: 645
	internal sealed class SnapshotConverter
	{
		// Token: 0x0600177D RID: 6013 RVA: 0x0005EDE4 File Offset: 0x0005CFE4
		internal static void ConvertFromV1(CatalogItemContext reportContext, ReportSnapshot snapshot, bool isSnapshotProcessing)
		{
			ChunkStorage chunkStorage = new ChunkStorage();
			bool flag;
			if (snapshot.ConnectionManager == null)
			{
				chunkStorage.ConnectionManager = new ConnectionManager(ConnectionTransactionType.Explicit, IsolationLevel.ReadCommitted);
				chunkStorage.ConnectionManager.WillDisconnectStorage();
				flag = false;
			}
			else
			{
				chunkStorage.ConnectionManager = snapshot.ConnectionManager;
				flag = true;
			}
			try
			{
				chunkStorage.LockSnapshotForUpgrade(snapshot.SnapshotDataID, snapshot.IsPermanentSnapshot);
				IEnumerable<ChunkHeader> chunksForSnapshot = chunkStorage.GetChunksForSnapshot(snapshot.SnapshotDataID, snapshot.IsPermanentSnapshot);
				int num = 0;
				foreach (ChunkHeader chunkHeader in chunksForSnapshot)
				{
					if (chunkHeader.Version != ChunkHeader.CurrentVersion)
					{
						BufferedCompressedReadStream bufferedCompressedReadStream = null;
						PartitionFileStream partitionFileStream = null;
						PartitionFileStream partitionFileStream2 = null;
						Stream stream = null;
						try
						{
							string text;
							bufferedCompressedReadStream = SnapshotChunkStreamFactory.GetCompressedContiguousChunk(snapshot.SnapshotDataID, snapshot.IsPermanentSnapshot, chunkHeader.ChunkName, chunkHeader.ChunkType, chunkStorage.ConnectionManager, out text);
							if (chunkHeader.ChunkName.StartsWith("RenderingInfo", Localization.CatalogStringComparison))
							{
								chunkStorage.DeleteOneChunk(snapshot.SnapshotDataID, snapshot.IsPermanentSnapshot, chunkHeader.ChunkName, chunkHeader.ChunkType);
							}
							else if (bufferedCompressedReadStream == null)
							{
								num++;
							}
							else
							{
								Global.PartitionManager.DeleteFile(SnapshotConverter.ConversionFolder, SnapshotConverter.FileName(chunkHeader, snapshot));
								partitionFileStream = Global.PartitionManager.CreateFile(SnapshotConverter.ConversionFolder, SnapshotConverter.FileName(chunkHeader, snapshot), false);
								StreamSupport.CopyStreamUsingBuffer(bufferedCompressedReadStream, partitionFileStream, Global.SqlStreamingBufferSize);
								partitionFileStream.Close();
								chunkStorage.DeleteOneChunk(snapshot.SnapshotDataID, snapshot.IsPermanentSnapshot, chunkHeader.ChunkName, chunkHeader.ChunkType);
								stream = SnapshotChunkStreamFactory.CreateWriteStream(snapshot.SnapshotDataID, snapshot.IsPermanentSnapshot, chunkHeader.ChunkName, chunkHeader.ChunkType, chunkHeader.MimeType, Global.SqlStreamingBufferSize, chunkStorage.ConnectionManager, WriteOptions.Create, false);
								partitionFileStream2 = Global.PartitionManager.GetFile(SnapshotConverter.ConversionFolder, SnapshotConverter.FileName(chunkHeader, snapshot));
								if (partitionFileStream2 != null)
								{
									StreamSupport.CopyStreamUsingBuffer(partitionFileStream2, stream, Global.SqlStreamingBufferSize);
									partitionFileStream2.Close();
									partitionFileStream2 = null;
									num++;
								}
							}
						}
						finally
						{
							if (bufferedCompressedReadStream != null)
							{
								bufferedCompressedReadStream.Close();
							}
							if (partitionFileStream != null)
							{
								partitionFileStream.Close();
							}
							if (partitionFileStream2 != null)
							{
								partitionFileStream2.Close();
							}
							if (stream != null)
							{
								stream.Close();
							}
							Global.PartitionManager.DeleteFile(SnapshotConverter.ConversionFolder, SnapshotConverter.FileName(chunkHeader, snapshot));
						}
					}
				}
				ISnapshotTransaction snapshotTransaction = null;
				try
				{
					if (!flag)
					{
						snapshotTransaction = snapshot.EnterTransactionContext();
					}
					if (num != 0)
					{
						chunkStorage.MarkSnapshotChunksAsUpdated(snapshot.SnapshotDataID, snapshot.IsPermanentSnapshot, ChunkHeader.CurrentVersion);
						snapshot.ConnectionManager = chunkStorage.ConnectionManager;
						bool isInUpgradeScope = snapshot.IsInUpgradeScope;
						snapshot.IsInUpgradeScope = true;
						try
						{
							int num2;
							bool flag2;
							ReportProcessing.UpgradeSnapshot(snapshot, isSnapshotProcessing, snapshot, reportContext, out num2, out flag2);
							chunkStorage.PromoteSnapshotInfo(snapshot, num2, flag2, PaginationMode.Estimate, ReportProcessingFlags.NotSet);
						}
						finally
						{
							snapshot.IsInUpgradeScope = isInUpgradeScope;
						}
						if (isSnapshotProcessing)
						{
							chunkStorage.DeleteOneChunk(snapshot.SnapshotDataID, snapshot.IsPermanentSnapshot, "Special", 0);
						}
					}
					if (snapshotTransaction != null)
					{
						RSTrace.CatalogTrace.Assert(!flag);
						snapshotTransaction.Commit();
						RSTrace.CatalogTrace.Assert(snapshot.ConnectionManager == null, "snapshot.ConnectionManager");
					}
					if (RSTrace.ChunkTracer.TraceVerbose)
					{
						RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "### Snapshot successfully upgraded: {0}, {1}", new object[] { snapshot.SnapshotDataID, snapshot.IsPermanentSnapshot });
					}
				}
				finally
				{
					if (snapshotTransaction != null)
					{
						snapshotTransaction.Dispose();
					}
				}
			}
			catch (Exception ex)
			{
				if (RSTrace.ChunkTracer.TraceError)
				{
					RSTrace.ChunkTracer.Trace(TraceLevel.Error, "### SnapshotConverter({0}, {1}), {2}", new object[]
					{
						snapshot.SnapshotDataID,
						snapshot.IsPermanentSnapshot,
						ex.ToString()
					});
				}
				if (!flag)
				{
					chunkStorage.AbortTransaction();
				}
				throw;
			}
			finally
			{
				if (!flag)
				{
					chunkStorage.DisconnectStorage();
				}
			}
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x0005F228 File Offset: 0x0005D428
		private static string FileName(ChunkHeader chunkHeader, ReportSnapshot snapshot)
		{
			return snapshot.SnapshotDataID + chunkHeader.ChunkName;
		}

		// Token: 0x04000882 RID: 2178
		private static readonly string ConversionFolder = PartitionManager.SnapshotConversionFolder;
	}
}
