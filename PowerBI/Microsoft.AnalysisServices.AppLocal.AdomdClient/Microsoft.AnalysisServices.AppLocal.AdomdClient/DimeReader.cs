using System;
using System.IO;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000023 RID: 35
	internal class DimeReader
	{
		// Token: 0x0600020D RID: 525 RVA: 0x0000A0C3 File Offset: 0x000082C3
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

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000A0FA File Offset: 0x000082FA
		public TransportCapabilities Options
		{
			get
			{
				return this.m_Options;
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000A104 File Offset: 0x00008304
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

		// Token: 0x06000210 RID: 528 RVA: 0x0000A1A0 File Offset: 0x000083A0
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

		// Token: 0x040001BA RID: 442
		private Stream m_stream;

		// Token: 0x040001BB RID: 443
		private DimeRecord m_currentRecord;

		// Token: 0x040001BC RID: 444
		private TransportCapabilities m_Options;

		// Token: 0x040001BD RID: 445
		private bool m_closed;
	}
}
