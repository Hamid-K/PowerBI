using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000852 RID: 2130
	internal sealed class RIFStorage : IStorage, IDisposable
	{
		// Token: 0x060076CE RID: 30414 RVA: 0x001EBB7C File Offset: 0x001E9D7C
		public RIFStorage(IStreamHandler streamHandler, int bufferPageSize, int bufferPageCount, int tempStreamSize, ISpaceManager spaceManager, IScalabilityObjectCreator appObjectCreator, IReferenceCreator appReferenceCreator, GlobalIDOwnerCollection globalIdsFromOtherStream, bool fromExistingStream, int rifCompatVersion)
		{
			this.m_streamCreator = streamHandler;
			this.m_scalabilityCache = null;
			this.m_bufferPageSize = bufferPageSize;
			this.m_bufferPageCount = bufferPageCount;
			this.m_tempStreamSize = tempStreamSize;
			this.m_stream = null;
			this.m_spaceManager = spaceManager;
			this.m_unifiedObjectCreator = new UnifiedObjectCreator(appObjectCreator, appReferenceCreator);
			this.m_referenceCreator = new UnifiedReferenceCreator(appReferenceCreator);
			this.m_fromExistingStream = fromExistingStream;
			this.m_globalIdsFromOtherStream = globalIdsFromOtherStream;
			this.m_rifCompatVersion = rifCompatVersion;
		}

		// Token: 0x060076CF RID: 30415 RVA: 0x001EBBF8 File Offset: 0x001E9DF8
		private void SetupStorage()
		{
			if (this.m_stream != null)
			{
				return;
			}
			Stream stream = this.m_streamCreator.OpenStream();
			this.m_streamCreator = null;
			this.m_stream = new PageBufferedStream(stream, this.m_bufferPageSize, this.m_bufferPageCount);
			this.m_stream.FreezePageAllocations = this.m_freezeAllocations;
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration> declarations = this.m_unifiedObjectCreator.GetDeclarations();
			this.m_memoryStream = new MemoryStream(this.m_tempStreamSize);
			this.m_writer = new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter(this.m_memoryStream, declarations, this.m_scalabilityCache, this.m_rifCompatVersion);
			if (this.m_fromExistingStream)
			{
				this.m_spaceManager.StreamEnd = this.m_stream.Length;
				this.m_reader = new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader(this.m_stream, this.m_unifiedObjectCreator, this.m_globalIdsFromOtherStream, this.m_scalabilityCache, declarations, IntermediateFormatVersion.Current, PersistenceFlags.Seekable);
				return;
			}
			this.m_spaceManager.StreamEnd = this.m_stream.Position;
			this.m_reader = new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader(this.m_stream, this.m_unifiedObjectCreator, this.m_globalIdsFromOtherStream, this.m_scalabilityCache, declarations, IntermediateFormatVersion.Current, PersistenceFlags.Seekable);
		}

		// Token: 0x060076D0 RID: 30416 RVA: 0x001EBD10 File Offset: 0x001E9F10
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable Retrieve(long offset)
		{
			long num;
			return this.Retrieve(offset, out num);
		}

		// Token: 0x060076D1 RID: 30417 RVA: 0x001EBD28 File Offset: 0x001E9F28
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable Retrieve(long offset, out long persistedSize)
		{
			this.SetupStorage();
			this.Seek(offset, SeekOrigin.Begin);
			Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable persistable = this.m_reader.ReadRIFObject();
			persistedSize = this.CalculatePersistedSize(persistable, offset);
			return persistable;
		}

		// Token: 0x060076D2 RID: 30418 RVA: 0x001EBD5C File Offset: 0x001E9F5C
		public T Retrieve<T>(long offset, out long persistedSize) where T : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, new()
		{
			this.SetupStorage();
			this.Seek(offset, SeekOrigin.Begin);
			T t = this.m_reader.ReadRIFObject<T>();
			persistedSize = this.CalculatePersistedSize(t, offset);
			return t;
		}

		// Token: 0x060076D3 RID: 30419 RVA: 0x001EBD93 File Offset: 0x001E9F93
		private long CalculatePersistedSize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable item, long offset)
		{
			return this.m_stream.Position - offset;
		}

		// Token: 0x060076D4 RID: 30420 RVA: 0x001EBDA2 File Offset: 0x001E9FA2
		public long Allocate(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable obj)
		{
			return this.WriteObject(obj, -1L, -1L);
		}

		// Token: 0x060076D5 RID: 30421 RVA: 0x001EBDB0 File Offset: 0x001E9FB0
		private long SeekToFreeSpace(long size)
		{
			long num = this.m_spaceManager.AllocateSpace(size);
			this.Seek(num, SeekOrigin.Begin);
			return num;
		}

		// Token: 0x060076D6 RID: 30422 RVA: 0x001EBDD3 File Offset: 0x001E9FD3
		public long ReserveSpace(int length)
		{
			this.SetupStorage();
			return this.m_spaceManager.AllocateSpace((long)length);
		}

		// Token: 0x060076D7 RID: 30423 RVA: 0x001EBDE8 File Offset: 0x001E9FE8
		public long Update(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable obj, long offset, long oldPersistedSize)
		{
			return this.WriteObject(obj, offset, oldPersistedSize);
		}

		// Token: 0x060076D8 RID: 30424 RVA: 0x001EBDF4 File Offset: 0x001E9FF4
		private long WriteObject(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable obj, long offset, long oldSize)
		{
			this.SetupStorage();
			this.m_memoryStream.Seek(0L, SeekOrigin.Begin);
			this.m_writer.Write(obj);
			long position = this.m_memoryStream.Position;
			if (oldSize < 0L || offset < 0L)
			{
				offset = this.SeekToFreeSpace(position);
			}
			else if (position != oldSize)
			{
				offset = this.m_spaceManager.Resize(offset, oldSize, position);
				this.Seek(offset, SeekOrigin.Begin);
			}
			else
			{
				this.Seek(offset, SeekOrigin.Begin);
			}
			this.m_stream.Write(this.m_memoryStream.GetBuffer(), 0, (int)position);
			return offset;
		}

		// Token: 0x060076D9 RID: 30425 RVA: 0x001EBE83 File Offset: 0x001EA083
		public void Free(long offset, int size)
		{
			this.m_spaceManager.Free(offset, (long)size);
		}

		// Token: 0x060076DA RID: 30426 RVA: 0x001EBE93 File Offset: 0x001EA093
		public void Close()
		{
			if (this.m_stream != null)
			{
				this.m_stream.Close();
				this.m_stream = null;
				this.m_memoryStream.Close();
				this.m_memoryStream = null;
			}
		}

		// Token: 0x060076DB RID: 30427 RVA: 0x001EBEC1 File Offset: 0x001EA0C1
		public void Flush()
		{
			if (this.m_stream != null)
			{
				this.m_stream.Flush();
			}
		}

		// Token: 0x060076DC RID: 30428 RVA: 0x001EBED6 File Offset: 0x001EA0D6
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x060076DD RID: 30429 RVA: 0x001EBEDE File Offset: 0x001EA0DE
		private void Seek(long offset, SeekOrigin origin)
		{
			this.m_stream.Seek(offset, origin);
			this.m_spaceManager.Seek(offset, origin);
		}

		// Token: 0x170027C2 RID: 10178
		// (get) Token: 0x060076DE RID: 30430 RVA: 0x001EBEFB File Offset: 0x001EA0FB
		public long StreamSize
		{
			get
			{
				return this.m_spaceManager.StreamEnd;
			}
		}

		// Token: 0x170027C3 RID: 10179
		// (get) Token: 0x060076DF RID: 30431 RVA: 0x001EBF08 File Offset: 0x001EA108
		// (set) Token: 0x060076E0 RID: 30432 RVA: 0x001EBF10 File Offset: 0x001EA110
		public IScalabilityCache ScalabilityCache
		{
			get
			{
				return this.m_scalabilityCache;
			}
			set
			{
				this.m_scalabilityCache = value;
				this.m_unifiedObjectCreator.ScalabilityCache = value;
			}
		}

		// Token: 0x170027C4 RID: 10180
		// (get) Token: 0x060076E1 RID: 30433 RVA: 0x001EBF25 File Offset: 0x001EA125
		public IReferenceCreator ReferenceCreator
		{
			get
			{
				return this.m_referenceCreator;
			}
		}

		// Token: 0x170027C5 RID: 10181
		// (get) Token: 0x060076E2 RID: 30434 RVA: 0x001EBF2D File Offset: 0x001EA12D
		// (set) Token: 0x060076E3 RID: 30435 RVA: 0x001EBF35 File Offset: 0x001EA135
		public bool FreezeAllocations
		{
			get
			{
				return this.m_freezeAllocations;
			}
			set
			{
				this.m_freezeAllocations = value;
				if (this.m_stream != null)
				{
					this.m_stream.FreezePageAllocations = value;
				}
			}
		}

		// Token: 0x060076E4 RID: 30436 RVA: 0x001EBF52 File Offset: 0x001EA152
		public void TraceStats()
		{
			this.m_spaceManager.TraceStats();
		}

		// Token: 0x04003C1B RID: 15387
		private PageBufferedStream m_stream;

		// Token: 0x04003C1C RID: 15388
		private MemoryStream m_memoryStream;

		// Token: 0x04003C1D RID: 15389
		private Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter m_writer;

		// Token: 0x04003C1E RID: 15390
		private Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader m_reader;

		// Token: 0x04003C1F RID: 15391
		private int m_bufferPageSize;

		// Token: 0x04003C20 RID: 15392
		private int m_bufferPageCount;

		// Token: 0x04003C21 RID: 15393
		private int m_tempStreamSize;

		// Token: 0x04003C22 RID: 15394
		private IScalabilityCache m_scalabilityCache;

		// Token: 0x04003C23 RID: 15395
		private IStreamHandler m_streamCreator;

		// Token: 0x04003C24 RID: 15396
		private ISpaceManager m_spaceManager;

		// Token: 0x04003C25 RID: 15397
		private IReferenceCreator m_referenceCreator;

		// Token: 0x04003C26 RID: 15398
		private UnifiedObjectCreator m_unifiedObjectCreator;

		// Token: 0x04003C27 RID: 15399
		private bool m_fromExistingStream;

		// Token: 0x04003C28 RID: 15400
		private GlobalIDOwnerCollection m_globalIdsFromOtherStream;

		// Token: 0x04003C29 RID: 15401
		private bool m_freezeAllocations;

		// Token: 0x04003C2A RID: 15402
		private readonly int m_rifCompatVersion;
	}
}
