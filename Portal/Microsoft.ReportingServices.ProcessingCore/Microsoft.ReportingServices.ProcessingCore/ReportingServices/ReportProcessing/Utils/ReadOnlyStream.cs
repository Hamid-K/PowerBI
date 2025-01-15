using System;
using System.IO;

namespace Microsoft.ReportingServices.ReportProcessing.Utils
{
	// Token: 0x020007AA RID: 1962
	internal class ReadOnlyStream : Stream
	{
		// Token: 0x06006EF3 RID: 28403 RVA: 0x001CFC5E File Offset: 0x001CDE5E
		public ReadOnlyStream(Stream underlyingStream, bool canCloseUnderlyingStream)
		{
			if (underlyingStream == null)
			{
				throw new ArgumentNullException("underlyingStream");
			}
			this.m_underlyingStream = underlyingStream;
			this.m_canCloseUnderlyingStream = canCloseUnderlyingStream;
		}

		// Token: 0x170025CB RID: 9675
		// (get) Token: 0x06006EF4 RID: 28404 RVA: 0x001CFC82 File Offset: 0x001CDE82
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170025CC RID: 9676
		// (get) Token: 0x06006EF5 RID: 28405 RVA: 0x001CFC85 File Offset: 0x001CDE85
		public override bool CanSeek
		{
			get
			{
				return this.m_underlyingStream.CanSeek;
			}
		}

		// Token: 0x170025CD RID: 9677
		// (get) Token: 0x06006EF6 RID: 28406 RVA: 0x001CFC92 File Offset: 0x001CDE92
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006EF7 RID: 28407 RVA: 0x001CFC95 File Offset: 0x001CDE95
		public override void Flush()
		{
			throw new InvalidOperationException("This Stream does not support this operation.");
		}

		// Token: 0x170025CE RID: 9678
		// (get) Token: 0x06006EF8 RID: 28408 RVA: 0x001CFCA1 File Offset: 0x001CDEA1
		public override long Length
		{
			get
			{
				return this.m_underlyingStream.Length;
			}
		}

		// Token: 0x170025CF RID: 9679
		// (get) Token: 0x06006EF9 RID: 28409 RVA: 0x001CFCAE File Offset: 0x001CDEAE
		// (set) Token: 0x06006EFA RID: 28410 RVA: 0x001CFCBB File Offset: 0x001CDEBB
		public override long Position
		{
			get
			{
				return this.m_underlyingStream.Position;
			}
			set
			{
				throw new InvalidOperationException("This Stream does not support this operation.");
			}
		}

		// Token: 0x06006EFB RID: 28411 RVA: 0x001CFCC7 File Offset: 0x001CDEC7
		public override int ReadByte()
		{
			return this.m_underlyingStream.ReadByte();
		}

		// Token: 0x06006EFC RID: 28412 RVA: 0x001CFCD4 File Offset: 0x001CDED4
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.m_underlyingStream.Read(buffer, offset, count);
		}

		// Token: 0x06006EFD RID: 28413 RVA: 0x001CFCE4 File Offset: 0x001CDEE4
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.m_underlyingStream.Seek(offset, origin);
		}

		// Token: 0x06006EFE RID: 28414 RVA: 0x001CFCF3 File Offset: 0x001CDEF3
		public override void SetLength(long value)
		{
			throw new InvalidOperationException("This Stream does not support this operation.");
		}

		// Token: 0x06006EFF RID: 28415 RVA: 0x001CFCFF File Offset: 0x001CDEFF
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new InvalidOperationException("This Stream does not support this operation.");
		}

		// Token: 0x06006F00 RID: 28416 RVA: 0x001CFD0B File Offset: 0x001CDF0B
		public override void Close()
		{
			if (this.m_canCloseUnderlyingStream)
			{
				this.m_underlyingStream.Close();
			}
		}

		// Token: 0x04003987 RID: 14727
		private readonly Stream m_underlyingStream;

		// Token: 0x04003988 RID: 14728
		private readonly bool m_canCloseUnderlyingStream;
	}
}
