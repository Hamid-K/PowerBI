using System;
using System.Diagnostics;
using System.IO;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002CE RID: 718
	internal sealed class SegmentedChunkStorage : IStorage, ICommitOnClose, IHasMimeType, IReadWriteStatistics, IBufferedStreamHintProvider, IUpdateSnapshot
	{
		// Token: 0x060019A5 RID: 6565 RVA: 0x000670F0 File Offset: 0x000652F0
		public static Stream Open(Guid snapshotId, bool isPermanent, string chunkName, int chunkType, ChunkConnectionManager connectionMananger, WriteOptions writeOptions)
		{
			if (RSTrace.ChunkTracer.TraceVerbose)
			{
				RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "Open Segmented Chunk ({0}, {1}, {2}, '{3}', {4})", new object[] { snapshotId, isPermanent, chunkName, chunkType, writeOptions });
			}
			if (writeOptions == WriteOptions.Create)
			{
				throw new InternalCatalogException("Invalid writeOptions: WriteOptions.Create");
			}
			SegmentedChunkStorage segmentedChunkStorage = new SegmentedChunkStorage(snapshotId, isPermanent, chunkName, chunkType, connectionMananger, writeOptions);
			if (segmentedChunkStorage.m_isAvailable)
			{
				return new StorageStream(segmentedChunkStorage);
			}
			if (RSTrace.ChunkTracer.TraceVerbose)
			{
				RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "Segmented Chunk '{0}' was not found.", new object[] { chunkName });
			}
			return null;
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x0006719C File Offset: 0x0006539C
		public static Stream OpenOrCreate(Guid snapshotId, bool isPermanent, string chunkName, string mimeType, int chunkType, ChunkFlags chunkFlags, short version, ChunkConnectionManager connectionManager, WriteOptions writeOptions)
		{
			if (RSTrace.ChunkTracer.TraceVerbose)
			{
				RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "Open|Create Segmented Chunk ({0}, {1}, {2}, '{3}', {4})", new object[] { snapshotId, isPermanent, chunkName, chunkType, writeOptions });
			}
			SegmentedChunkStorage segmentedChunkStorage = new SegmentedChunkStorage(snapshotId, isPermanent, chunkName, chunkType, connectionManager, writeOptions);
			if (!segmentedChunkStorage.m_isAvailable)
			{
				segmentedChunkStorage.MimeType = mimeType;
				segmentedChunkStorage.Flags = chunkFlags;
				segmentedChunkStorage.Version = version;
				if (RSTrace.ChunkTracer.TraceVerbose)
				{
					RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "Creating chunk ({0}, {1}, {2}, '{3}'", new object[] { snapshotId, isPermanent, chunkName, chunkType });
				}
				segmentedChunkStorage.Create();
			}
			return new StorageStream(segmentedChunkStorage);
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x00067274 File Offset: 0x00065474
		private SegmentedChunkStorage(Guid snapshotId, bool isPermanent, string chunkName, int chunkType, ChunkConnectionManager connectionManager, WriteOptions writeOptions)
		{
			this.m_storage = connectionManager.DbInterface;
			RSTrace.CatalogTrace.Assert(this.m_storage != null, "m_storage");
			this.m_ownsConnection = connectionManager.IsConnectionOwner;
			this.m_chunkName = chunkName;
			this.m_snapshotId = snapshotId;
			this.m_isPermanent = isPermanent;
			this.m_type = chunkType;
			this.m_writeOptions = writeOptions;
			this.m_versionOnWrite = writeOptions == WriteOptions.Version;
			if (writeOptions != WriteOptions.Create)
			{
				this.m_length = 0L;
				this.m_isAvailable = this.Storage.OpenSegmentedChunk(this.m_snapshotId, this.m_isPermanent, this.m_chunkName, this.m_type, out this.m_chunkId, out this.m_flags, out this.m_mimeType, delegate(Guid segmentId, int logicalLength, int actualLength)
				{
					this.m_segments.AddSegmentData(new SegmentedChunkStorage.SegmentData(segmentId, this.m_versionOnWrite, logicalLength, actualLength));
					this.m_length += (long)logicalLength;
				});
				if ((this.m_flags & ChunkFlags.CrossDatabaseSharing) > ChunkFlags.None)
				{
					RSTrace.CatalogTrace.Assert(!this.m_isPermanent, "!m_isPermanent");
					this.m_isPermanent = true;
					this.m_writeOptions = WriteOptions.None;
				}
			}
			else
			{
				this.m_isAvailable = false;
			}
			if (this.m_isAvailable)
			{
				this.InitializeStorageLayer();
			}
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x00067394 File Offset: 0x00065594
		public int Read(long position, byte[] buffer, int offset, int length)
		{
			RSTrace.CatalogTrace.Assert(this.m_storage != null, "attempt to read after Close()");
			int num;
			SegmentedChunkStorage.SegmentData segmentData = this.m_segments.MapPositionToSegment(position, out num);
			if (segmentData == null)
			{
				return 0;
			}
			int num2 = segmentData.LogicalSegmentLength - num;
			SegmentStorageLayer.ReadWriteParameters readWriteParameters;
			readWriteParameters.Buffer = buffer;
			readWriteParameters.Offset = offset;
			readWriteParameters.Length = num2;
			readWriteParameters.ChunkId = this.Id;
			readWriteParameters.IsPermanent = this.IsPermanent;
			readWriteParameters.Segment = segmentData;
			readWriteParameters.DataIndex = num;
			readWriteParameters.AbsolutePosition = position;
			SegmentStorageLayer.ReadWriteStatistics readWriteStatistics = default(SegmentStorageLayer.ReadWriteStatistics);
			this.m_storageLayer.Read(this.m_storage, readWriteParameters, ref readWriteStatistics);
			this.m_timeSpentCompression += readWriteStatistics.CompressionTime;
			return num2;
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x00067454 File Offset: 0x00065654
		public void Write(long position, byte[] buffer, int offset, int length)
		{
			RSTrace.CatalogTrace.Assert(this.m_storage != null, "attempt to write after Close()");
			if (this.m_writeOptions == WriteOptions.None)
			{
				throw new InternalCatalogException("Chunk was not opened with write access");
			}
			if (position > this.Length)
			{
				throw new InvalidOperationException("Attempt to write beyond end of stream");
			}
			int i = 0;
			if (this.m_versionOnWrite)
			{
				this.VersionChunk();
			}
			while (i < length)
			{
				int num;
				SegmentedChunkStorage.SegmentData segmentData = this.m_segments.MapPositionToSegment(position, out num);
				if (segmentData != null && segmentData.VersionOnWrite)
				{
					this.VersionSegment(segmentData);
				}
				else if (segmentData == null)
				{
					RSTrace.ChunkTracer.Assert(num == 0, "dataIndex == 0");
				}
				int num2 = Math.Min(length - i, Global.ChunkSegmentSize - num);
				RSTrace.CatalogTrace.Assert(num2 >= 0, "bytesToWrite");
				int num3 = i + offset;
				SegmentStorageLayer.ReadWriteParameters readWriteParameters;
				readWriteParameters.Buffer = buffer;
				readWriteParameters.Offset = num3;
				readWriteParameters.Length = num2;
				readWriteParameters.ChunkId = this.Id;
				readWriteParameters.IsPermanent = this.IsPermanent;
				readWriteParameters.Segment = segmentData;
				readWriteParameters.DataIndex = num;
				readWriteParameters.AbsolutePosition = position;
				SegmentStorageLayer.ReadWriteStatistics readWriteStatistics = default(SegmentStorageLayer.ReadWriteStatistics);
				if (segmentData != null)
				{
					this.m_storageLayer.Write(this.m_storage, readWriteParameters, ref readWriteStatistics);
				}
				else
				{
					this.CreateSegmentInStorageLayer(ref readWriteParameters, ref readWriteStatistics);
				}
				this.m_timeSpentCompression += readWriteStatistics.CompressionTime;
				this.m_timeSpentUncompression += readWriteStatistics.UncompressionTime;
				num = 0;
				i += num2;
				position += (long)num2;
			}
			this.m_length = Math.Max(this.m_length, position);
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x060019AA RID: 6570 RVA: 0x000675EA File Offset: 0x000657EA
		public bool CanRead
		{
			get
			{
				return this.m_storage != null;
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x060019AB RID: 6571 RVA: 0x000675EA File Offset: 0x000657EA
		public bool CanWrite
		{
			get
			{
				return this.m_storage != null;
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x060019AC RID: 6572 RVA: 0x000675F5 File Offset: 0x000657F5
		public Guid Id
		{
			get
			{
				return this.m_chunkId;
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x060019AD RID: 6573 RVA: 0x000675FD File Offset: 0x000657FD
		public Guid SnapshotId
		{
			get
			{
				return this.m_snapshotId;
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x060019AE RID: 6574 RVA: 0x00067605 File Offset: 0x00065805
		public bool IsPermanent
		{
			get
			{
				return this.m_isPermanent;
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x060019AF RID: 6575 RVA: 0x0006760D File Offset: 0x0006580D
		public long Length
		{
			get
			{
				return this.m_length;
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x060019B0 RID: 6576 RVA: 0x00067615 File Offset: 0x00065815
		public int Type
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x060019B1 RID: 6577 RVA: 0x0006761D File Offset: 0x0006581D
		public string Name
		{
			get
			{
				return this.m_chunkName;
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x060019B2 RID: 6578 RVA: 0x00067625 File Offset: 0x00065825
		// (set) Token: 0x060019B3 RID: 6579 RVA: 0x0006762D File Offset: 0x0006582D
		public string MimeType
		{
			get
			{
				return this.m_mimeType;
			}
			set
			{
				this.ThrowIfChunkAlreadyOpened();
				this.m_mimeType = value;
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x060019B4 RID: 6580 RVA: 0x0006763C File Offset: 0x0006583C
		// (set) Token: 0x060019B5 RID: 6581 RVA: 0x00067644 File Offset: 0x00065844
		public short Version
		{
			get
			{
				return this.m_version;
			}
			set
			{
				this.ThrowIfChunkAlreadyOpened();
				this.m_version = value;
			}
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x060019B6 RID: 6582 RVA: 0x00067653 File Offset: 0x00065853
		// (set) Token: 0x060019B7 RID: 6583 RVA: 0x0006765B File Offset: 0x0006585B
		public ChunkFlags Flags
		{
			get
			{
				return this.m_flags;
			}
			set
			{
				this.ThrowIfChunkAlreadyOpened();
				this.m_flags = value;
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x060019B8 RID: 6584 RVA: 0x0006766A File Offset: 0x0006586A
		private SegmentChunkDbInterface Storage
		{
			get
			{
				return this.m_storage;
			}
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x00067674 File Offset: 0x00065874
		private void InitializeStorageLayer()
		{
			RSTrace.ChunkTracer.Assert(this.m_isAvailable, "m_isAvailable");
			RSTrace.ChunkTracer.Assert(this.m_storageLayer == null, "m_storageLayer");
			bool flag = this.m_writeOptions == WriteOptions.Create;
			bool flag2 = false;
			ReportProcessing.ReportChunkTypes type = (ReportProcessing.ReportChunkTypes)this.Type;
			switch (type)
			{
			case ReportProcessing.ReportChunkTypes.Image:
			case ReportProcessing.ReportChunkTypes.StaticImage:
			case ReportProcessing.ReportChunkTypes.Data:
				break;
			case ReportProcessing.ReportChunkTypes.Other:
			case ReportProcessing.ReportChunkTypes.ServerRdlMapping:
				goto IL_0063;
			default:
				if (type != ReportProcessing.ReportChunkTypes.CompiledDefinition)
				{
					goto IL_0063;
				}
				break;
			}
			flag2 = true;
			IL_0063:
			this.m_storageLayer = SegmentStorageLayer.GetStorageLayer(this.Id, this.Flags, flag, flag2);
		}

		// Token: 0x060019BA RID: 6586 RVA: 0x000676FD File Offset: 0x000658FD
		private void ThrowIfChunkAlreadyOpened()
		{
			if (this.m_isAvailable)
			{
				throw new InternalCatalogException("Attempt to set chunk property after it was opened");
			}
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x00067714 File Offset: 0x00065914
		private void Create()
		{
			if (this.m_isAvailable)
			{
				throw new InternalCatalogException("Attempt to create chunk which already exists");
			}
			this.m_chunkId = this.Storage.CreateSegmentedChunk(this.SnapshotId, this.IsPermanent, this.Name, this.MimeType, this.Type, this.Flags, this.Version);
			this.m_isAvailable = true;
			this.InitializeStorageLayer();
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x0006777C File Offset: 0x0006597C
		private void VersionSegment(SegmentedChunkStorage.SegmentData segment)
		{
			if (!segment.VersionOnWrite)
			{
				throw new ArgumentException("Attempt to unnecessarily version segment");
			}
			if (RSTrace.ChunkTracer.TraceVerbose)
			{
				RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "Versioning segment {0} of chunk ({1}, {2}, '{3})", new object[] { segment.SegmentId, this.SnapshotId, this.IsPermanent, this.m_chunkName });
			}
			Guid segmentId = segment.SegmentId;
			Guid guid = this.Storage.DeepCopySegment(this.m_chunkId, this.IsPermanent, segmentId);
			segment.SegmentId = guid;
			segment.VersionOnWrite = false;
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x00067820 File Offset: 0x00065A20
		private void VersionChunk()
		{
			Guid guid = this.m_storageLayer.VersionChunk(this.Storage, this.SnapshotId, this.Id, this.IsPermanent);
			this.m_chunkId = guid;
			this.m_versionOnWrite = false;
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x00067860 File Offset: 0x00065A60
		private void CreateSegmentInStorageLayer(ref SegmentStorageLayer.ReadWriteParameters wp, ref SegmentStorageLayer.ReadWriteStatistics stats)
		{
			SegmentedChunkStorage.SegmentData segmentData = this.m_storageLayer.CreateSegment(this.m_storage, wp, ref stats);
			this.m_segments.AddSegmentData(segmentData);
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x060019BF RID: 6591 RVA: 0x00067892 File Offset: 0x00065A92
		public long TimeCompressing
		{
			get
			{
				return this.m_timeSpentCompression;
			}
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x060019C0 RID: 6592 RVA: 0x0006789A File Offset: 0x00065A9A
		public long TimeUncompressing
		{
			get
			{
				return this.m_timeSpentUncompression;
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x060019C1 RID: 6593 RVA: 0x000678A4 File Offset: 0x00065AA4
		public long CompressedLength
		{
			get
			{
				if ((this.Flags & ChunkFlags.Compressed) == ChunkFlags.Compressed)
				{
					long num = 0L;
					object syncRoot = this.m_segments.SyncRoot;
					lock (syncRoot)
					{
						foreach (SegmentedChunkStorage.SegmentData segmentData in this.m_segments)
						{
							num += (long)segmentData.ActualSegmentLength;
						}
					}
					return num;
				}
				return this.UncompressedLength;
			}
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x060019C2 RID: 6594 RVA: 0x00067938 File Offset: 0x00065B38
		public long UncompressedLength
		{
			get
			{
				return this.Length;
			}
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x00067940 File Offset: 0x00065B40
		public void Close(bool commit)
		{
			if (RSTrace.ChunkTracer.TraceVerbose)
			{
				RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "Closing segmented chunk ({0}, {1}, '{2}')", new object[] { this.SnapshotId, this.IsPermanent, this.m_chunkName });
			}
			try
			{
				if (this.m_storageLayer != null)
				{
					this.m_storageLayer.Close();
				}
			}
			finally
			{
				if (this.m_ownsConnection && this.m_storage != null)
				{
					if (commit && RSTrace.ChunkTracer.TraceVerbose)
					{
						RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "Committing transaction for chunk ({0}, {1}, '{2}')", new object[] { this.SnapshotId, this.IsPermanent, this.m_chunkName });
					}
					if (!commit && RSTrace.ChunkTracer.TraceWarning)
					{
						RSTrace.ChunkTracer.Trace(TraceLevel.Warning, "Rolling back transaction for chunk ({0}, {1}, '{2}')", new object[] { this.SnapshotId, this.IsPermanent, this.m_chunkName });
					}
					try
					{
						if (commit)
						{
							this.m_storage.Commit();
						}
						else
						{
							this.m_storage.AbortTransaction();
						}
					}
					finally
					{
						this.m_storage.Disconnect();
					}
				}
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x060019C4 RID: 6596 RVA: 0x000053DC File Offset: 0x000035DC
		public bool CanProvideHints
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x00067AA0 File Offset: 0x00065CA0
		public BufferedStreamHint GetReadHint(int availableBufferSpace, long readPosition)
		{
			int num;
			SegmentedChunkStorage.SegmentData segmentData = this.m_segments.MapPositionToSegment(readPosition, out num);
			BufferedStreamHint bufferedStreamHint = default(BufferedStreamHint);
			if (segmentData != null)
			{
				bufferedStreamHint.SuggestedReadPosition = readPosition - (long)num;
				bufferedStreamHint.SuggestedReadAmount = segmentData.ActualSegmentLength;
			}
			else
			{
				bufferedStreamHint.SuggestedReadAmount = 0;
				bufferedStreamHint.SuggestedReadPosition = readPosition;
			}
			return bufferedStreamHint;
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x00067AF2 File Offset: 0x00065CF2
		public void UpdateSnapshot(Guid newSnapshotId)
		{
			this.m_snapshotId = newSnapshotId;
		}

		// Token: 0x04000952 RID: 2386
		private Guid m_chunkId;

		// Token: 0x04000953 RID: 2387
		private Guid m_snapshotId;

		// Token: 0x04000954 RID: 2388
		private readonly bool m_isPermanent;

		// Token: 0x04000955 RID: 2389
		private long m_length;

		// Token: 0x04000956 RID: 2390
		private readonly int m_type;

		// Token: 0x04000957 RID: 2391
		private readonly string m_chunkName;

		// Token: 0x04000958 RID: 2392
		private string m_mimeType;

		// Token: 0x04000959 RID: 2393
		private short m_version;

		// Token: 0x0400095A RID: 2394
		private ChunkFlags m_flags;

		// Token: 0x0400095B RID: 2395
		private readonly SegmentList m_segments = new SegmentList();

		// Token: 0x0400095C RID: 2396
		private readonly WriteOptions m_writeOptions;

		// Token: 0x0400095D RID: 2397
		private SegmentChunkDbInterface m_storage;

		// Token: 0x0400095E RID: 2398
		private bool m_versionOnWrite;

		// Token: 0x0400095F RID: 2399
		private bool m_ownsConnection;

		// Token: 0x04000960 RID: 2400
		private bool m_isAvailable;

		// Token: 0x04000961 RID: 2401
		private SegmentStorageLayer m_storageLayer;

		// Token: 0x04000962 RID: 2402
		private long m_timeSpentCompression;

		// Token: 0x04000963 RID: 2403
		private long m_timeSpentUncompression;

		// Token: 0x020004DE RID: 1246
		internal sealed class SegmentData
		{
			// Token: 0x0600248D RID: 9357 RVA: 0x00086368 File Offset: 0x00084568
			public SegmentData(Guid segmentId, bool versionOnWrite, int logicalLength, int actualLength)
			{
				this.SegmentId = segmentId;
				this.VersionOnWrite = versionOnWrite;
				this.LogicalSegmentLength = logicalLength;
				this.ActualSegmentLength = actualLength;
			}

			// Token: 0x04001133 RID: 4403
			public Guid SegmentId;

			// Token: 0x04001134 RID: 4404
			public bool VersionOnWrite;

			// Token: 0x04001135 RID: 4405
			public int LogicalSegmentLength;

			// Token: 0x04001136 RID: 4406
			public int ActualSegmentLength;
		}
	}
}
