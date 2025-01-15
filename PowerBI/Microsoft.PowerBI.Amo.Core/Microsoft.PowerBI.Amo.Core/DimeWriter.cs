using System;
using System.IO;

namespace Microsoft.AnalysisServices
{
	// Token: 0x0200003D RID: 61
	internal class DimeWriter
	{
		// Token: 0x060002B6 RID: 694 RVA: 0x0000DBFC File Offset: 0x0000BDFC
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

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000DC55 File Offset: 0x0000BE55
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x0000DC5D File Offset: 0x0000BE5D
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

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000DC75 File Offset: 0x0000BE75
		// (set) Token: 0x060002BA RID: 698 RVA: 0x0000DC7D File Offset: 0x0000BE7D
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

		// Token: 0x060002BB RID: 699 RVA: 0x0000DC88 File Offset: 0x0000BE88
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

		// Token: 0x060002BC RID: 700 RVA: 0x0000DD0A File Offset: 0x0000BF0A
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

		// Token: 0x0400021C RID: 540
		private const int ChunkSizeDefault = 1024;

		// Token: 0x0400021D RID: 541
		private Stream m_stream;

		// Token: 0x0400021E RID: 542
		private DimeRecord m_currentRecord;

		// Token: 0x0400021F RID: 543
		private TransportCapabilities m_Options;

		// Token: 0x04000220 RID: 544
		private bool m_closed;

		// Token: 0x04000221 RID: 545
		private bool m_firstRecord;

		// Token: 0x04000222 RID: 546
		private int m_defaultChunkSize;
	}
}
