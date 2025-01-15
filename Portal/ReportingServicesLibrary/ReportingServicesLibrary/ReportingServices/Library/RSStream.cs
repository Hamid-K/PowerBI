using System;
using System.IO;
using System.Text;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200024A RID: 586
	internal class RSStream : Stream, IRenderStreamFinishNotification, IRenderStream, ICacheable, IDisposable
	{
		// Token: 0x0600156A RID: 5482 RVA: 0x00054D7A File Offset: 0x00052F7A
		public RSStream(Stream stream, bool autoResponseOnClose)
			: this(stream, autoResponseOnClose, false)
		{
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x00054D85 File Offset: 0x00052F85
		public RSStream(Stream stream, bool autoResponseOnClose, bool timeUntilFinish)
		{
			this.m_lockObject = new object();
			base..ctor();
			this.m_innerStream = stream;
			this.m_autoResponseOnClose = autoResponseOnClose;
			if (timeUntilFinish)
			{
				this.m_timeToFinishTimer = new Timer();
				this.m_timeToFinishTimer.StartTimer();
			}
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x00054DC0 File Offset: 0x00052FC0
		public RSStream(RSStream stream)
		{
			this.m_lockObject = new object();
			base..ctor();
			this.m_autoResponseOnClose = stream.m_autoResponseOnClose;
			this.m_closed = stream.m_closed;
			this.m_disposed = stream.m_disposed;
			this.m_innerStream = stream.m_innerStream;
			this.m_mimeType = stream.m_mimeType;
			this.m_streamEncoding = stream.m_streamEncoding;
			this.m_streamExtension = stream.m_streamExtension;
			this.m_streamName = stream.m_streamName;
			this.m_timeToFinishTimer = stream.m_timeToFinishTimer;
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600156D RID: 5485 RVA: 0x00054E4C File Offset: 0x0005304C
		// (remove) Token: 0x0600156E RID: 5486 RVA: 0x00054E84 File Offset: 0x00053084
		public event EventHandler StreamFinished;

		// Token: 0x0600156F RID: 5487 RVA: 0x00054EB9 File Offset: 0x000530B9
		protected virtual void OnStreamFinished(EventArgs e)
		{
			if (this.StreamFinished != null)
			{
				this.StreamFinished(this, e);
			}
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x00054ED0 File Offset: 0x000530D0
		public void Finish()
		{
			this.OnStreamFinished(EventArgs.Empty);
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001571 RID: 5489 RVA: 0x00054EDD File Offset: 0x000530DD
		public bool AutoResponseOnClose
		{
			get
			{
				return this.m_autoResponseOnClose;
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001572 RID: 5490 RVA: 0x00054EE5 File Offset: 0x000530E5
		// (set) Token: 0x06001573 RID: 5491 RVA: 0x00054EED File Offset: 0x000530ED
		public string MimeType
		{
			get
			{
				return this.m_mimeType;
			}
			set
			{
				this.m_mimeType = value;
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001574 RID: 5492 RVA: 0x00054EF6 File Offset: 0x000530F6
		// (set) Token: 0x06001575 RID: 5493 RVA: 0x00054EFE File Offset: 0x000530FE
		public Encoding Encoding
		{
			get
			{
				return this.m_streamEncoding;
			}
			set
			{
				this.m_streamEncoding = value;
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001576 RID: 5494 RVA: 0x00054F07 File Offset: 0x00053107
		// (set) Token: 0x06001577 RID: 5495 RVA: 0x00054F0F File Offset: 0x0005310F
		public string Name
		{
			get
			{
				return this.m_streamName;
			}
			set
			{
				this.m_streamName = value;
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001578 RID: 5496 RVA: 0x00054F18 File Offset: 0x00053118
		// (set) Token: 0x06001579 RID: 5497 RVA: 0x00054F20 File Offset: 0x00053120
		public string Extension
		{
			get
			{
				return this.m_streamExtension;
			}
			set
			{
				this.m_streamExtension = value;
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x0600157A RID: 5498 RVA: 0x00054F2C File Offset: 0x0005312C
		public bool IsCacheable
		{
			get
			{
				ICacheable cacheable = this.m_innerStream as ICacheable;
				if (cacheable != null)
				{
					return cacheable.IsCacheable;
				}
				return this.m_innerStream.CanRead && this.m_innerStream.CanSeek;
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x0600157B RID: 5499 RVA: 0x00054F69 File Offset: 0x00053169
		public override bool CanRead
		{
			get
			{
				return this.m_innerStream.CanRead;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x0600157C RID: 5500 RVA: 0x00054F76 File Offset: 0x00053176
		public override bool CanWrite
		{
			get
			{
				return this.m_innerStream.CanWrite;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x0600157D RID: 5501 RVA: 0x00054F83 File Offset: 0x00053183
		public override bool CanSeek
		{
			get
			{
				return this.m_innerStream.CanSeek;
			}
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x00054F90 File Offset: 0x00053190
		public override void Flush()
		{
			this.m_innerStream.Flush();
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x0600157F RID: 5503 RVA: 0x00054FA0 File Offset: 0x000531A0
		public override long Length
		{
			get
			{
				object lockObject = this.m_lockObject;
				long num;
				lock (lockObject)
				{
					if (this.m_closed)
					{
						num = this.m_savedLength;
					}
					else
					{
						num = this.m_innerStream.Length;
					}
				}
				return num;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001580 RID: 5504 RVA: 0x00054FF8 File Offset: 0x000531F8
		// (set) Token: 0x06001581 RID: 5505 RVA: 0x00055005 File Offset: 0x00053205
		public override long Position
		{
			get
			{
				return this.m_innerStream.Position;
			}
			set
			{
				this.m_innerStream.Position = value;
			}
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x00055013 File Offset: 0x00053213
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.m_innerStream.Read(buffer, offset, count);
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x00055023 File Offset: 0x00053223
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.m_innerStream.Seek(offset, origin);
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x00055032 File Offset: 0x00053232
		public override void SetLength(long value)
		{
			this.m_innerStream.SetLength(value);
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x00055040 File Offset: 0x00053240
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.m_innerStream.Write(buffer, offset, count);
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001586 RID: 5510 RVA: 0x00055050 File Offset: 0x00053250
		public bool IsClosed
		{
			get
			{
				object lockObject = this.m_lockObject;
				bool closed;
				lock (lockObject)
				{
					closed = this.m_closed;
				}
				return closed;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001587 RID: 5511 RVA: 0x00055094 File Offset: 0x00053294
		public Stream InnerStream
		{
			get
			{
				return this.m_innerStream;
			}
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x0005509C File Offset: 0x0005329C
		protected override void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				if (disposing)
				{
					object lockObject = this.m_lockObject;
					lock (lockObject)
					{
						this.m_savedLength = this.m_innerStream.Length;
						this.m_innerStream.Close();
						this.m_autoResponseOnClose = false;
						this.m_closed = true;
					}
				}
				base.Dispose(disposing);
			}
			this.m_disposed = true;
		}

		// Token: 0x040007C9 RID: 1993
		private string m_mimeType;

		// Token: 0x040007CA RID: 1994
		private Encoding m_streamEncoding;

		// Token: 0x040007CB RID: 1995
		private string m_streamName;

		// Token: 0x040007CC RID: 1996
		private string m_streamExtension;

		// Token: 0x040007CD RID: 1997
		private Stream m_innerStream;

		// Token: 0x040007CE RID: 1998
		private bool m_autoResponseOnClose;

		// Token: 0x040007CF RID: 1999
		private bool m_closed;

		// Token: 0x040007D0 RID: 2000
		private bool m_disposed;

		// Token: 0x040007D1 RID: 2001
		private Timer m_timeToFinishTimer;

		// Token: 0x040007D2 RID: 2002
		private object m_lockObject;

		// Token: 0x040007D3 RID: 2003
		private long m_savedLength;
	}
}
