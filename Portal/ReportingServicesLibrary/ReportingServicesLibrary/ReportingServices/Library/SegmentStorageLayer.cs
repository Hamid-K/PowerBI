using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002CF RID: 719
	internal abstract class SegmentStorageLayer
	{
		// Token: 0x060019C8 RID: 6600
		public abstract void Read(SegmentChunkDbInterface storage, SegmentStorageLayer.ReadWriteParameters parameters, ref SegmentStorageLayer.ReadWriteStatistics statistics);

		// Token: 0x060019C9 RID: 6601
		public abstract void Write(SegmentChunkDbInterface storage, SegmentStorageLayer.ReadWriteParameters parameters, ref SegmentStorageLayer.ReadWriteStatistics statistics);

		// Token: 0x060019CA RID: 6602
		public abstract SegmentedChunkStorage.SegmentData CreateSegment(SegmentChunkDbInterface storage, SegmentStorageLayer.ReadWriteParameters parameters, ref SegmentStorageLayer.ReadWriteStatistics statistics);

		// Token: 0x060019CB RID: 6603 RVA: 0x00067B25 File Offset: 0x00065D25
		public virtual Guid VersionChunk(SegmentChunkDbInterface storage, Guid snapshotId, Guid chunkId, bool isPermanent)
		{
			return storage.ShallowCopyChunk(snapshotId, chunkId, isPermanent);
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public virtual void Close()
		{
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x060019CD RID: 6605 RVA: 0x00067B31 File Offset: 0x00065D31
		public static SegmentStorageLayer Uncompressed
		{
			get
			{
				return SegmentStorageLayer.m_uncompressed;
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x060019CE RID: 6606 RVA: 0x00067B38 File Offset: 0x00065D38
		public static SegmentStorageLayer Compressed
		{
			get
			{
				return SegmentStorageLayer.m_compressed;
			}
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x00067B40 File Offset: 0x00065D40
		public static SegmentStorageLayer GetStorageLayer(Guid chunkId, ChunkFlags flags, bool create, bool enableCaching)
		{
			SegmentStorageLayer segmentStorageLayer;
			if ((flags & ChunkFlags.Compressed) == ChunkFlags.Compressed)
			{
				segmentStorageLayer = SegmentStorageLayer.Compressed;
			}
			else
			{
				segmentStorageLayer = SegmentStorageLayer.Uncompressed;
			}
			SegmentStorageLayer segmentStorageLayer2 = segmentStorageLayer;
			if ((flags & ChunkFlags.FileSystem) == ChunkFlags.FileSystem)
			{
				if (RSTrace.ChunkTracer.TraceVerbose)
				{
					RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "Attempting to use filesystem chunk '{0}'", new object[] { chunkId });
				}
				SegmentStorageLayer.FileSystemStorageLayer fileSystemStorageLayer = new SegmentStorageLayer.FileSystemStorageLayer(chunkId, segmentStorageLayer);
				fileSystemStorageLayer.CreateChunkFileStream(!create);
				segmentStorageLayer2 = fileSystemStorageLayer;
			}
			if (enableCaching)
			{
				return new CacheStorageLayer(segmentStorageLayer2);
			}
			return segmentStorageLayer2;
		}

		// Token: 0x04000964 RID: 2404
		private static readonly SegmentStorageLayer m_uncompressed = new SegmentStorageLayer.UncompressedStorageLayer();

		// Token: 0x04000965 RID: 2405
		private static readonly SegmentStorageLayer m_compressed = new SegmentStorageLayer.CompressedStorageLayer();

		// Token: 0x020004DF RID: 1247
		internal struct ReadWriteParameters
		{
			// Token: 0x04001137 RID: 4407
			public byte[] Buffer;

			// Token: 0x04001138 RID: 4408
			public int Offset;

			// Token: 0x04001139 RID: 4409
			public int Length;

			// Token: 0x0400113A RID: 4410
			public Guid ChunkId;

			// Token: 0x0400113B RID: 4411
			public bool IsPermanent;

			// Token: 0x0400113C RID: 4412
			public SegmentedChunkStorage.SegmentData Segment;

			// Token: 0x0400113D RID: 4413
			public int DataIndex;

			// Token: 0x0400113E RID: 4414
			public long AbsolutePosition;
		}

		// Token: 0x020004E0 RID: 1248
		internal struct ReadWriteStatistics
		{
			// Token: 0x0600248E RID: 9358 RVA: 0x0008638D File Offset: 0x0008458D
			public static SegmentStorageLayer.ReadWriteStatistics operator +(SegmentStorageLayer.ReadWriteStatistics lhs, SegmentStorageLayer.ReadWriteStatistics rhs)
			{
				SegmentStorageLayer.ReadWriteStatistics readWriteStatistics = lhs;
				lhs.CompressionTime += rhs.CompressionTime;
				lhs.UncompressionTime += rhs.UncompressionTime;
				return readWriteStatistics;
			}

			// Token: 0x0400113F RID: 4415
			public long CompressionTime;

			// Token: 0x04001140 RID: 4416
			public long UncompressionTime;
		}

		// Token: 0x020004E1 RID: 1249
		private class UncompressedStorageLayer : SegmentStorageLayer
		{
			// Token: 0x0600248F RID: 9359 RVA: 0x000863B2 File Offset: 0x000845B2
			public override void Read(SegmentChunkDbInterface storage, SegmentStorageLayer.ReadWriteParameters parameters, ref SegmentStorageLayer.ReadWriteStatistics statistics)
			{
				storage.ReadChunkSegment(parameters.ChunkId, parameters.IsPermanent, parameters.Segment.SegmentId, parameters.DataIndex, parameters.Buffer, parameters.Offset, parameters.Length);
			}

			// Token: 0x06002490 RID: 9360 RVA: 0x000863EC File Offset: 0x000845EC
			public override void Write(SegmentChunkDbInterface storage, SegmentStorageLayer.ReadWriteParameters parameters, ref SegmentStorageLayer.ReadWriteStatistics statistics)
			{
				SegmentedChunkStorage.SegmentData segment = parameters.Segment;
				int num = Math.Max(segment.LogicalSegmentLength, parameters.DataIndex + parameters.Length);
				storage.WriteChunkSegment(parameters.ChunkId, parameters.IsPermanent, parameters.Segment.SegmentId, parameters.DataIndex, num, parameters.Buffer, parameters.Offset, parameters.Length);
				segment.ActualSegmentLength = num;
				segment.LogicalSegmentLength = num;
			}

			// Token: 0x06002491 RID: 9361 RVA: 0x0008645C File Offset: 0x0008465C
			public override SegmentedChunkStorage.SegmentData CreateSegment(SegmentChunkDbInterface storage, SegmentStorageLayer.ReadWriteParameters parameters, ref SegmentStorageLayer.ReadWriteStatistics statistics)
			{
				return new SegmentedChunkStorage.SegmentData(storage.CreateChunkSegment(Guid.Empty, parameters.IsPermanent, parameters.ChunkId, parameters.AbsolutePosition, parameters.Length, parameters.Buffer, parameters.Offset, parameters.Length), false, parameters.Length, parameters.Length);
			}
		}

		// Token: 0x020004E2 RID: 1250
		private class CompressedStorageLayer : SegmentStorageLayer
		{
			// Token: 0x06002493 RID: 9363 RVA: 0x000864B8 File Offset: 0x000846B8
			public override void Read(SegmentChunkDbInterface storage, SegmentStorageLayer.ReadWriteParameters parameters, ref SegmentStorageLayer.ReadWriteStatistics statistics)
			{
				SegmentedChunkStorage.SegmentData segment = parameters.Segment;
				byte[] array = new byte[segment.ActualSegmentLength];
				storage.ReadChunkSegment(parameters.ChunkId, parameters.IsPermanent, segment.SegmentId, 0, array, 0, array.Length);
				Timer timer = new Timer();
				timer.StartTimer();
				using (MemoryStream memoryStream = new MemoryStream(array))
				{
					using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
					{
						using (MemoryStream memoryStream2 = new MemoryStream(parameters.Buffer, parameters.Offset, parameters.Length, true))
						{
							memoryStream.Seek(0L, SeekOrigin.Begin);
							byte[] array2 = new byte[segment.LogicalSegmentLength];
							int num = gzipStream.Read(array2, 0, array2.Length);
							if (RSTrace.ChunkTracer.TraceVerbose)
							{
								RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "Decompressed segment {0}, original size = {1}, decompressed size = {2}", new object[] { segment.SegmentId, array.Length, num });
							}
							if (num != array2.Length)
							{
								throw new InvalidOperationException("Expected read count not achieved");
							}
							memoryStream2.Write(array2, parameters.DataIndex, parameters.Length);
						}
					}
				}
				statistics.UncompressionTime += timer.ElapsedTimeMs();
			}

			// Token: 0x06002494 RID: 9364 RVA: 0x0008661C File Offset: 0x0008481C
			public override void Write(SegmentChunkDbInterface storage, SegmentStorageLayer.ReadWriteParameters parameters, ref SegmentStorageLayer.ReadWriteStatistics statistics)
			{
				SegmentedChunkStorage.SegmentData segment = parameters.Segment;
				bool flag = (parameters.DataIndex == 0 && parameters.Length == segment.LogicalSegmentLength) || segment.LogicalSegmentLength == 0;
				byte[] array = parameters.Buffer;
				int num = parameters.Length;
				int num2 = parameters.Offset;
				if (!flag)
				{
					num = Math.Max(segment.LogicalSegmentLength, parameters.DataIndex + parameters.Length);
					SegmentStorageLayer.ReadWriteParameters readWriteParameters = new SegmentStorageLayer.ReadWriteParameters
					{
						Buffer = new byte[num],
						Offset = 0,
						DataIndex = 0,
						Length = segment.LogicalSegmentLength,
						Segment = parameters.Segment,
						ChunkId = parameters.ChunkId,
						IsPermanent = parameters.IsPermanent
					};
					this.Read(storage, readWriteParameters, ref statistics);
					using (MemoryStream memoryStream = new MemoryStream(readWriteParameters.Buffer, true))
					{
						memoryStream.Seek((long)parameters.DataIndex, SeekOrigin.Begin);
						memoryStream.Write(parameters.Buffer, parameters.Offset, parameters.Length);
						memoryStream.Flush();
						array = readWriteParameters.Buffer;
					}
					num2 = 0;
				}
				long num3;
				byte[] array2 = SegmentStorageLayer.CompressedStorageLayer.CompressBuffer(array, num2, num, out num3);
				int num4 = array2.Length;
				storage.WriteChunkSegment(parameters.ChunkId, parameters.IsPermanent, segment.SegmentId, 0, num, array2, 0, num4);
				segment.ActualSegmentLength = num4;
				segment.LogicalSegmentLength = num;
				statistics.CompressionTime += num3;
			}

			// Token: 0x06002495 RID: 9365 RVA: 0x0008679C File Offset: 0x0008499C
			public override SegmentedChunkStorage.SegmentData CreateSegment(SegmentChunkDbInterface storage, SegmentStorageLayer.ReadWriteParameters parameters, ref SegmentStorageLayer.ReadWriteStatistics statistics)
			{
				long num;
				byte[] array = SegmentStorageLayer.CompressedStorageLayer.CompressBuffer(parameters.Buffer, parameters.Offset, parameters.Length, out num);
				int num2 = array.Length;
				SegmentedChunkStorage.SegmentData segmentData = new SegmentedChunkStorage.SegmentData(storage.CreateChunkSegment(Guid.Empty, parameters.IsPermanent, parameters.ChunkId, parameters.AbsolutePosition, parameters.Length, array, 0, num2), false, parameters.Length, num2);
				statistics.CompressionTime += num;
				return segmentData;
			}

			// Token: 0x06002496 RID: 9366 RVA: 0x00086808 File Offset: 0x00084A08
			private static byte[] CompressBuffer(byte[] buffer, int offset, int count, out long elapsedTimeMs)
			{
				Timer timer = new Timer();
				timer.StartTimer();
				byte[] array2;
				using (MemoryStream memoryStream = new MemoryStream(Math.Max(1024, count / 4)))
				{
					GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Compress);
					gzipStream.Write(buffer, offset, count);
					gzipStream.Close();
					byte[] array = memoryStream.ToArray();
					elapsedTimeMs = timer.ElapsedTimeMs();
					array2 = array;
				}
				return array2;
			}
		}

		// Token: 0x020004E3 RID: 1251
		private class FileSystemStorageLayer : SegmentStorageLayer
		{
			// Token: 0x06002498 RID: 9368 RVA: 0x00086878 File Offset: 0x00084A78
			public FileSystemStorageLayer(Guid chunkId, SegmentStorageLayer catalogStore)
			{
				RSTrace.CatalogTrace.Assert(catalogStore != null, "permanentStore");
				RSTrace.CatalogTrace.Assert(chunkId != Guid.Empty);
				this.m_catalogStore = catalogStore;
				this.m_chunkId = chunkId;
			}

			// Token: 0x06002499 RID: 9369 RVA: 0x000868B8 File Offset: 0x00084AB8
			public override void Read(SegmentChunkDbInterface storage, SegmentStorageLayer.ReadWriteParameters parameters, ref SegmentStorageLayer.ReadWriteStatistics statistics)
			{
				RSTrace.ChunkTracer.Assert(parameters.ChunkId == this.m_chunkId, "parameters.ChunkId");
				if (this.m_fileStream != null)
				{
					this.m_fileStream.Seek(parameters.AbsolutePosition, SeekOrigin.Begin);
					StreamSupport.ReadToCountOrEnd(parameters.Buffer, parameters.Offset, parameters.Length, new StreamSupport.StreamRead(this.m_fileStream.Read));
					return;
				}
				this.m_catalogStore.Read(storage, parameters, ref statistics);
			}

			// Token: 0x0600249A RID: 9370 RVA: 0x0008693C File Offset: 0x00084B3C
			public override void Write(SegmentChunkDbInterface storage, SegmentStorageLayer.ReadWriteParameters parameters, ref SegmentStorageLayer.ReadWriteStatistics statistics)
			{
				this.PerformWriteOperation(storage, parameters, ref statistics, delegate(ref SegmentStorageLayer.ReadWriteStatistics operationStats)
				{
					this.m_catalogStore.Write(storage, parameters, ref operationStats);
				});
			}

			// Token: 0x0600249B RID: 9371 RVA: 0x00086984 File Offset: 0x00084B84
			public override SegmentedChunkStorage.SegmentData CreateSegment(SegmentChunkDbInterface storage, SegmentStorageLayer.ReadWriteParameters parameters, ref SegmentStorageLayer.ReadWriteStatistics statistics)
			{
				SegmentedChunkStorage.SegmentData sd = null;
				this.PerformWriteOperation(storage, parameters, ref statistics, delegate(ref SegmentStorageLayer.ReadWriteStatistics operationStats)
				{
					sd = this.m_catalogStore.CreateSegment(storage, parameters, ref operationStats);
				});
				return sd;
			}

			// Token: 0x0600249C RID: 9372 RVA: 0x000869D8 File Offset: 0x00084BD8
			public override Guid VersionChunk(SegmentChunkDbInterface storage, Guid snapshotId, Guid chunkId, bool isPermanent)
			{
				Guid guid = base.VersionChunk(storage, snapshotId, chunkId, isPermanent);
				this.m_chunkId = guid;
				if (this.m_fileStream != null)
				{
					try
					{
						this.m_fileStream.Close();
					}
					finally
					{
						this.m_fileStream = null;
					}
					this.m_fileStream = Global.PartitionManager.CopyChunkFile(chunkId, guid);
				}
				return guid;
			}

			// Token: 0x0600249D RID: 9373 RVA: 0x00086A38 File Offset: 0x00084C38
			public void CreateChunkFileStream(bool openExisting)
			{
				RSTrace.CatalogTrace.Assert(this.m_fileStream == null, "m_fileStream already opened");
				this.m_fileStream = Global.PartitionManager.CreateChunkFile(this.m_chunkId, openExisting);
			}

			// Token: 0x0600249E RID: 9374 RVA: 0x00086A6C File Offset: 0x00084C6C
			public override void Close()
			{
				if (this.m_fileStream != null)
				{
					try
					{
						this.m_fileStream.Close();
					}
					finally
					{
						this.m_fileStream = null;
					}
				}
			}

			// Token: 0x0600249F RID: 9375 RVA: 0x00086AA8 File Offset: 0x00084CA8
			private void PerformWriteOperation(SegmentChunkDbInterface storage, SegmentStorageLayer.ReadWriteParameters parameters, ref SegmentStorageLayer.ReadWriteStatistics statistics, SegmentStorageLayer.FileSystemStorageLayer.WriteOperation writeOperation)
			{
				RSTrace.ChunkTracer.Assert(parameters.ChunkId == this.m_chunkId, "parameters.ChunkId");
				RSTrace.ChunkTracer.Assert(writeOperation != null, "writeOperation");
				IAsyncResult asyncResult = null;
				try
				{
					if (this.m_fileStream != null)
					{
						this.m_fileStream.Seek(parameters.AbsolutePosition, SeekOrigin.Begin);
						asyncResult = this.m_fileStream.BeginWrite(parameters.Buffer, parameters.Offset, parameters.Length, null, null);
						RSTrace.ChunkTracer.Assert(asyncResult != null, "asyncWriteResult");
					}
					writeOperation(ref statistics);
				}
				catch (Exception ex)
				{
					if (RSTrace.ChunkTracer.TraceError)
					{
						RSTrace.ChunkTracer.TraceException(TraceLevel.Error, "Error while performing async file write operation: " + ex.ToString());
					}
					throw;
				}
				finally
				{
					if (asyncResult != null)
					{
						this.m_fileStream.EndWrite(asyncResult);
						RSTrace.ChunkTracer.Assert(asyncResult.IsCompleted, "asyncWriteResult.IsCompleted");
						if (RSTrace.ChunkTracer.TraceWarning && asyncResult.CompletedSynchronously)
						{
							RSTrace.ChunkTracer.Trace(TraceLevel.Warning, "Async write operation on chunk '{0}' completed synchronously", new object[] { parameters.ChunkId });
						}
					}
				}
			}

			// Token: 0x04001141 RID: 4417
			private readonly SegmentStorageLayer m_catalogStore;

			// Token: 0x04001142 RID: 4418
			private Guid m_chunkId;

			// Token: 0x04001143 RID: 4419
			private Stream m_fileStream;

			// Token: 0x0200053D RID: 1341
			// (Invoke) Token: 0x06002568 RID: 9576
			private delegate void WriteOperation(ref SegmentStorageLayer.ReadWriteStatistics statistics);
		}
	}
}
