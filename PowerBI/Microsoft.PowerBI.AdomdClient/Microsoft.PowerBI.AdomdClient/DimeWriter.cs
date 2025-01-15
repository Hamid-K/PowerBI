using System;
using System.IO;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000025 RID: 37
	internal class DimeWriter
	{
		// Token: 0x0600021D RID: 541 RVA: 0x0000AA00 File Offset: 0x00008C00
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

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000AA59 File Offset: 0x00008C59
		// (set) Token: 0x0600021F RID: 543 RVA: 0x0000AA61 File Offset: 0x00008C61
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

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000220 RID: 544 RVA: 0x0000AA79 File Offset: 0x00008C79
		// (set) Token: 0x06000221 RID: 545 RVA: 0x0000AA81 File Offset: 0x00008C81
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

		// Token: 0x06000222 RID: 546 RVA: 0x0000AA8C File Offset: 0x00008C8C
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

		// Token: 0x06000223 RID: 547 RVA: 0x0000AB0E File Offset: 0x00008D0E
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

		// Token: 0x040001CA RID: 458
		private const int ChunkSizeDefault = 1024;

		// Token: 0x040001CB RID: 459
		private Stream m_stream;

		// Token: 0x040001CC RID: 460
		private DimeRecord m_currentRecord;

		// Token: 0x040001CD RID: 461
		private TransportCapabilities m_Options;

		// Token: 0x040001CE RID: 462
		private bool m_closed;

		// Token: 0x040001CF RID: 463
		private bool m_firstRecord;

		// Token: 0x040001D0 RID: 464
		private int m_defaultChunkSize;
	}
}
