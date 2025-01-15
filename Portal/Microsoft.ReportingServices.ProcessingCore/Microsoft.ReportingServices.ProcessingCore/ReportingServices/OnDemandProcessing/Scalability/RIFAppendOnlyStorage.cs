using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000853 RID: 2131
	internal sealed class RIFAppendOnlyStorage : IStorage, IDisposable
	{
		// Token: 0x060076E5 RID: 30437 RVA: 0x001EBF60 File Offset: 0x001EA160
		internal RIFAppendOnlyStorage(IStreamHandler streamHandler, IScalabilityObjectCreator appObjectCreator, IReferenceCreator appReferenceCreator, GlobalIDOwnerCollection globalIdsFromOtherStream, bool fromExistingStream, int rifCompatVersion, bool prohibitSerializableValues)
		{
			this.m_streamCreator = streamHandler;
			this.m_scalabilityCache = null;
			this.m_stream = null;
			this.m_unifiedObjectCreator = new UnifiedObjectCreator(appObjectCreator, appReferenceCreator);
			this.m_referenceCreator = new UnifiedReferenceCreator(appReferenceCreator);
			this.m_fromExistingStream = fromExistingStream;
			this.m_globalIdsFromOtherStream = globalIdsFromOtherStream;
			this.m_prohibitSerializableValues = prohibitSerializableValues;
			this.m_rifCompatVersion = rifCompatVersion;
		}

		// Token: 0x060076E6 RID: 30438 RVA: 0x001EBFCC File Offset: 0x001EA1CC
		private void SetupStorage()
		{
			if (this.m_stream != null)
			{
				return;
			}
			this.m_stream = this.m_streamCreator.OpenStream();
			this.m_streamCreator = null;
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration> declarations = this.m_unifiedObjectCreator.GetDeclarations();
			if (this.m_fromExistingStream)
			{
				this.m_reader = new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader(this.m_stream, this.m_unifiedObjectCreator, this.m_globalIdsFromOtherStream, this.m_scalabilityCache);
				if (this.m_stream.CanWrite)
				{
					this.m_writer = new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter(this.m_stream, this.m_stream.Length, declarations, this.m_scalabilityCache, this.m_rifCompatVersion, this.m_prohibitSerializableValues);
					this.m_writerSetup = true;
				}
				this.m_atEnd = false;
			}
			else
			{
				this.m_writer = new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter(this.m_stream, declarations, this.m_scalabilityCache, this.m_rifCompatVersion, this.m_prohibitSerializableValues);
				this.m_writerSetup = true;
				this.m_reader = new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader(this.m_stream, this.m_unifiedObjectCreator, this.m_globalIdsFromOtherStream, this.m_scalabilityCache, declarations, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatVersion.Current, PersistenceFlags.Seekable);
				this.m_atEnd = true;
			}
			this.m_fromExistingStream = true;
		}

		// Token: 0x060076E7 RID: 30439 RVA: 0x001EC0E0 File Offset: 0x001EA2E0
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable Retrieve(long offset, out long persistedSize)
		{
			this.SetupStorage();
			this.m_stream.Seek(offset, SeekOrigin.Begin);
			this.m_atEnd = false;
			long position = this.m_stream.Position;
			Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable persistable = this.m_reader.ReadRIFObject();
			persistedSize = this.m_stream.Position - position;
			return persistable;
		}

		// Token: 0x060076E8 RID: 30440 RVA: 0x001EC130 File Offset: 0x001EA330
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable Retrieve(long offset)
		{
			long num;
			return this.Retrieve(offset, out num);
		}

		// Token: 0x060076E9 RID: 30441 RVA: 0x001EC148 File Offset: 0x001EA348
		public T Retrieve<T>(long offset, out long persistedSize) where T : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, new()
		{
			this.SetupStorage();
			this.m_stream.Seek(offset, SeekOrigin.Begin);
			this.m_atEnd = false;
			long position = this.m_stream.Position;
			T t = this.m_reader.ReadRIFObject<T>();
			persistedSize = this.m_stream.Position - position;
			return t;
		}

		// Token: 0x060076EA RID: 30442 RVA: 0x001EC198 File Offset: 0x001EA398
		public long Allocate(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable obj)
		{
			this.SetupStorage();
			if (!this.m_atEnd)
			{
				this.m_stream.Seek(0L, SeekOrigin.End);
				this.m_atEnd = true;
			}
			this.m_streamLength = this.m_stream.Position;
			this.m_writer.Write(obj);
			return this.m_streamLength;
		}

		// Token: 0x060076EB RID: 30443 RVA: 0x001EC1EC File Offset: 0x001EA3EC
		public void Free(long offset, int size)
		{
			Global.Tracer.Assert(false, "RIFAppendOnlyStorage does not support Free(...)");
		}

		// Token: 0x060076EC RID: 30444 RVA: 0x001EC1FE File Offset: 0x001EA3FE
		public long Update(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable obj, long offset, long oldPersistedSize)
		{
			Global.Tracer.Assert(false, "RIFAppendOnlyStorage does not support Update(...)s");
			return -1L;
		}

		// Token: 0x060076ED RID: 30445 RVA: 0x001EC212 File Offset: 0x001EA412
		public void Close()
		{
			if (this.m_stream != null)
			{
				this.m_stream.Close();
				this.m_stream = null;
			}
		}

		// Token: 0x060076EE RID: 30446 RVA: 0x001EC22E File Offset: 0x001EA42E
		public void Flush()
		{
			if (this.m_stream != null)
			{
				this.m_stream.Flush();
			}
		}

		// Token: 0x170027C6 RID: 10182
		// (get) Token: 0x060076EF RID: 30447 RVA: 0x001EC243 File Offset: 0x001EA443
		internal bool FromExistingStream
		{
			get
			{
				return this.m_fromExistingStream;
			}
		}

		// Token: 0x060076F0 RID: 30448 RVA: 0x001EC24B File Offset: 0x001EA44B
		internal void Reset(IStreamHandler streamHandler)
		{
			this.Close();
			this.m_writerSetup = false;
			this.m_streamCreator = streamHandler;
		}

		// Token: 0x170027C7 RID: 10183
		// (get) Token: 0x060076F1 RID: 30449 RVA: 0x001EC261 File Offset: 0x001EA461
		public long StreamSize
		{
			get
			{
				return this.m_streamLength;
			}
		}

		// Token: 0x170027C8 RID: 10184
		// (get) Token: 0x060076F2 RID: 30450 RVA: 0x001EC269 File Offset: 0x001EA469
		// (set) Token: 0x060076F3 RID: 30451 RVA: 0x001EC271 File Offset: 0x001EA471
		public IScalabilityCache ScalabilityCache
		{
			get
			{
				return this.m_scalabilityCache;
			}
			set
			{
				this.m_scalabilityCache = value;
			}
		}

		// Token: 0x170027C9 RID: 10185
		// (get) Token: 0x060076F4 RID: 30452 RVA: 0x001EC27A File Offset: 0x001EA47A
		public IReferenceCreator ReferenceCreator
		{
			get
			{
				return this.m_referenceCreator;
			}
		}

		// Token: 0x170027CA RID: 10186
		// (get) Token: 0x060076F5 RID: 30453 RVA: 0x001EC282 File Offset: 0x001EA482
		// (set) Token: 0x060076F6 RID: 30454 RVA: 0x001EC285 File Offset: 0x001EA485
		public bool FreezeAllocations
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x060076F7 RID: 30455 RVA: 0x001EC287 File Offset: 0x001EA487
		public void TraceStats()
		{
		}

		// Token: 0x060076F8 RID: 30456 RVA: 0x001EC289 File Offset: 0x001EA489
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x04003C2B RID: 15403
		private Stream m_stream;

		// Token: 0x04003C2C RID: 15404
		private Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter m_writer;

		// Token: 0x04003C2D RID: 15405
		private Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader m_reader;

		// Token: 0x04003C2E RID: 15406
		private IScalabilityCache m_scalabilityCache;

		// Token: 0x04003C2F RID: 15407
		private bool m_writerSetup;

		// Token: 0x04003C30 RID: 15408
		private IStreamHandler m_streamCreator;

		// Token: 0x04003C31 RID: 15409
		private IReferenceCreator m_referenceCreator;

		// Token: 0x04003C32 RID: 15410
		private UnifiedObjectCreator m_unifiedObjectCreator;

		// Token: 0x04003C33 RID: 15411
		private bool m_fromExistingStream;

		// Token: 0x04003C34 RID: 15412
		private GlobalIDOwnerCollection m_globalIdsFromOtherStream;

		// Token: 0x04003C35 RID: 15413
		private readonly int m_rifCompatVersion;

		// Token: 0x04003C36 RID: 15414
		private bool m_atEnd;

		// Token: 0x04003C37 RID: 15415
		private long m_streamLength = -1L;

		// Token: 0x04003C38 RID: 15416
		private readonly bool m_prohibitSerializableValues;
	}
}
