using System;
using System.IO;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000025 RID: 37
	internal class DimeWriter
	{
		// Token: 0x0600022A RID: 554 RVA: 0x0000AD00 File Offset: 0x00008F00
		public DimeWriter(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanWrite)
			{
				throw new ArgumentException(XmlaSR.DimeWriter_CannotWriteToStream, "stream");
			}
			this.m_stream = stream;
			this.m_firstRecord = true;
			this.m_defaultChunkSize = 1024;
			this.m_Options = null;
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000AD59 File Offset: 0x00008F59
		// (set) Token: 0x0600022C RID: 556 RVA: 0x0000AD61 File Offset: 0x00008F61
		public int DefaultChunkSize
		{
			get
			{
				return this.m_defaultChunkSize;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentException(XmlaSR.DimeWriter_InvalidDefaultChunkSize);
				}
				this.m_defaultChunkSize = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000AD79 File Offset: 0x00008F79
		// (set) Token: 0x0600022E RID: 558 RVA: 0x0000AD81 File Offset: 0x00008F81
		public TransportCapabilities Options
		{
			get
			{
				return this.m_Options;
			}
			set
			{
				this.m_Options = value;
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000AD8C File Offset: 0x00008F8C
		public DimeRecord CreateRecord(Uri id, string type, TypeFormatEnum typeFormat, int contentLength)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (this.m_closed)
			{
				throw new InvalidOperationException(XmlaSR.DimeWriter_WriterIsClosed);
			}
			if (this.m_currentRecord != null)
			{
				this.m_currentRecord.Close(false);
			}
			this.m_currentRecord = new DimeRecord(this.m_stream, id, type, typeFormat, this.m_firstRecord, contentLength, this.m_defaultChunkSize);
			this.m_currentRecord.Options = this.m_Options;
			this.m_firstRecord = false;
			return this.m_currentRecord;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000AE0E File Offset: 0x0000900E
		public void Close()
		{
			if (!this.m_closed)
			{
				if (this.m_currentRecord != null)
				{
					this.m_currentRecord.Close(true);
					this.m_currentRecord = null;
				}
				this.m_closed = true;
			}
		}

		// Token: 0x040001D7 RID: 471
		private const int ChunkSizeDefault = 1024;

		// Token: 0x040001D8 RID: 472
		private Stream m_stream;

		// Token: 0x040001D9 RID: 473
		private DimeRecord m_currentRecord;

		// Token: 0x040001DA RID: 474
		private TransportCapabilities m_Options;

		// Token: 0x040001DB RID: 475
		private bool m_closed;

		// Token: 0x040001DC RID: 476
		private bool m_firstRecord;

		// Token: 0x040001DD RID: 477
		private int m_defaultChunkSize;
	}
}
