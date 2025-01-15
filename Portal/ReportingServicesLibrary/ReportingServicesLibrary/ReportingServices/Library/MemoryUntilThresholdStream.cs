using System;
using System.IO;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000249 RID: 585
	internal abstract class MemoryUntilThresholdStream : Stream, ICacheable
	{
		// Token: 0x06001558 RID: 5464 RVA: 0x00054AF5 File Offset: 0x00052CF5
		protected MemoryUntilThresholdStream(int threshold)
		{
			this.m_bufferStream = new MemoryStream(Global.BufferInitialSize);
			this.m_threshold = threshold;
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001559 RID: 5465 RVA: 0x00054B14 File Offset: 0x00052D14
		public bool IsClosed
		{
			get
			{
				return this.m_closed;
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x0600155A RID: 5466 RVA: 0x00054B1C File Offset: 0x00052D1C
		public override bool CanRead
		{
			get
			{
				return this.m_bufferStream != null && this.m_bufferStream.CanRead;
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x0600155B RID: 5467 RVA: 0x00054B33 File Offset: 0x00052D33
		public override bool CanSeek
		{
			get
			{
				return this.m_bufferStream != null && this.m_bufferStream.CanSeek;
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x0600155C RID: 5468 RVA: 0x00054B4A File Offset: 0x00052D4A
		public override bool CanWrite
		{
			get
			{
				return this.m_bufferStream != null && this.m_bufferStream.CanWrite;
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x0600155D RID: 5469 RVA: 0x00054B61 File Offset: 0x00052D61
		public override long Length
		{
			get
			{
				if (this.m_bufferStream == null)
				{
					throw new InvalidOperationException("Length is no longer accessible");
				}
				return this.m_bufferStream.Length;
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x0600155E RID: 5470 RVA: 0x00054B81 File Offset: 0x00052D81
		// (set) Token: 0x0600155F RID: 5471 RVA: 0x00054BA1 File Offset: 0x00052DA1
		public override long Position
		{
			get
			{
				if (this.m_bufferStream == null)
				{
					throw new InvalidOperationException("Position is no longer accessible");
				}
				return this.m_bufferStream.Position;
			}
			set
			{
				if (this.m_bufferStream != null)
				{
					this.m_bufferStream.Position = value;
				}
			}
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public override void Flush()
		{
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06001561 RID: 5473 RVA: 0x00054BB7 File Offset: 0x00052DB7
		public bool IsCacheable
		{
			get
			{
				return this.m_bufferStream != null && this.m_bufferStream.CanRead && this.m_bufferStream.CanSeek;
			}
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x00054BE0 File Offset: 0x00052DE0
		protected override void Dispose(bool disposing)
		{
			if (this.m_closed)
			{
				return;
			}
			try
			{
				this.m_closed = true;
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x00054C1C File Offset: 0x00052E1C
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.m_bufferStream == null)
			{
				throw new InvalidOperationException("Read can no longer be done");
			}
			return this.m_bufferStream.Read(buffer, offset, count);
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x00054C3F File Offset: 0x00052E3F
		public override int ReadByte()
		{
			if (this.m_bufferStream == null)
			{
				throw new InvalidOperationException("Read can no longer be done");
			}
			return this.m_bufferStream.ReadByte();
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x00054C5F File Offset: 0x00052E5F
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (this.m_bufferStream == null)
			{
				throw new InvalidOperationException("Seek can no longer be done");
			}
			return this.m_bufferStream.Seek(offset, origin);
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x00054C81 File Offset: 0x00052E81
		public override void SetLength(long value)
		{
			if (!this.m_thresholdReached && value > (long)this.m_threshold)
			{
				this.ThresholdReached();
			}
			if (this.m_bufferStream != null)
			{
				this.m_bufferStream.SetLength(value);
			}
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x00054CAF File Offset: 0x00052EAF
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (!this.m_thresholdReached && this.Position + (long)count > (long)this.m_threshold)
			{
				this.ThresholdReached();
			}
			if (this.m_bufferStream != null)
			{
				this.m_bufferStream.Write(buffer, offset, count);
			}
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x00054CE7 File Offset: 0x00052EE7
		public override void WriteByte(byte value)
		{
			if (!this.m_thresholdReached && this.Position >= (long)this.m_threshold)
			{
				this.ThresholdReached();
			}
			if (this.m_bufferStream != null)
			{
				this.m_bufferStream.WriteByte(value);
			}
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x00054D1C File Offset: 0x00052F1C
		protected virtual void ThresholdReached()
		{
			RSTrace.CatalogTrace.Assert(!this.m_thresholdReached);
			RSTrace.CatalogTrace.Assert(this.m_bufferStream is MemoryStream);
			this.m_bufferStream.SetLength(0L);
			this.m_bufferStream.Close();
			this.m_bufferStream = null;
			this.m_thresholdReached = true;
		}

		// Token: 0x040007C4 RID: 1988
		protected Stream m_bufferStream;

		// Token: 0x040007C5 RID: 1989
		protected bool m_thresholdReached;

		// Token: 0x040007C6 RID: 1990
		protected bool m_closed;

		// Token: 0x040007C7 RID: 1991
		private int m_threshold;
	}
}
