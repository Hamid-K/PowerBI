using System;
using System.IO;

namespace Microsoft.AnalysisServices
{
	// Token: 0x0200003B RID: 59
	internal class DimeReader
	{
		// Token: 0x06000299 RID: 665 RVA: 0x0000CFBF File Offset: 0x0000B1BF
		public DimeReader(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException(XmlaSR.DimeReader_CannotReadFromStream);
			}
			this.m_stream = stream;
			this.m_Options = null;
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000CFF6 File Offset: 0x0000B1F6
		public TransportCapabilities Options
		{
			get
			{
				return this.m_Options;
			}
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000D000 File Offset: 0x0000B200
		public DimeRecord ReadRecord()
		{
			if (this.m_closed)
			{
				throw new InvalidOperationException(XmlaSR.DimeReader_IsClosed);
			}
			if (this.m_currentRecord != null)
			{
				if (this.m_currentRecord.EndOfMessage)
				{
					return null;
				}
				this.m_currentRecord.Close();
			}
			this.m_currentRecord = new DimeRecord(this.m_stream);
			if (this.m_Options == null)
			{
				this.m_Options = this.m_currentRecord.Options;
			}
			if (this.m_currentRecord.TypeFormat == TypeFormatEnum.None && this.m_currentRecord.EndOfMessage)
			{
				this.m_currentRecord.Close();
				return null;
			}
			return this.m_currentRecord;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000D09C File Offset: 0x0000B29C
		public void Close()
		{
			if (!this.m_closed)
			{
				if (this.m_currentRecord != null)
				{
					if (this.m_currentRecord.CanRead)
					{
						throw new InvalidOperationException(XmlaSR.DimeReader_PreviousRecordStreamStillOpened);
					}
					while (!this.m_currentRecord.EndOfMessage)
					{
						if (this.ReadRecord() != null)
						{
							this.m_currentRecord.Close(false);
						}
					}
					this.m_currentRecord.Close();
				}
				this.m_closed = true;
			}
		}

		// Token: 0x040001FF RID: 511
		private Stream m_stream;

		// Token: 0x04000200 RID: 512
		private DimeRecord m_currentRecord;

		// Token: 0x04000201 RID: 513
		private TransportCapabilities m_Options;

		// Token: 0x04000202 RID: 514
		private bool m_closed;
	}
}
