using System;
using System.IO;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000023 RID: 35
	internal class DimeReader
	{
		// Token: 0x06000200 RID: 512 RVA: 0x00009DC3 File Offset: 0x00007FC3
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

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00009DFA File Offset: 0x00007FFA
		public TransportCapabilities Options
		{
			get
			{
				return this.m_Options;
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00009E04 File Offset: 0x00008004
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

		// Token: 0x06000203 RID: 515 RVA: 0x00009EA0 File Offset: 0x000080A0
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

		// Token: 0x040001AD RID: 429
		private Stream m_stream;

		// Token: 0x040001AE RID: 430
		private DimeRecord m_currentRecord;

		// Token: 0x040001AF RID: 431
		private TransportCapabilities m_Options;

		// Token: 0x040001B0 RID: 432
		private bool m_closed;
	}
}
