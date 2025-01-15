using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http
{
	// Token: 0x02000007 RID: 7
	internal class NonOwnedStream : Stream
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00002571 File Offset: 0x00000771
		protected NonOwnedStream()
		{
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002579 File Offset: 0x00000779
		public NonOwnedStream(Stream innerStream)
		{
			if (innerStream == null)
			{
				throw new ArgumentNullException("innerStream");
			}
			this.InnerStream = innerStream;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002596 File Offset: 0x00000796
		// (set) Token: 0x06000030 RID: 48 RVA: 0x0000259E File Offset: 0x0000079E
		protected Stream InnerStream { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000025A7 File Offset: 0x000007A7
		// (set) Token: 0x06000032 RID: 50 RVA: 0x000025AF File Offset: 0x000007AF
		private protected bool IsDisposed { protected get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000025B8 File Offset: 0x000007B8
		public override bool CanRead
		{
			get
			{
				return !this.IsDisposed && this.InnerStream.CanRead;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000025CF File Offset: 0x000007CF
		public override bool CanSeek
		{
			get
			{
				return !this.IsDisposed && this.InnerStream.CanSeek;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000025E6 File Offset: 0x000007E6
		public override bool CanTimeout
		{
			get
			{
				return this.InnerStream.CanTimeout;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000025F3 File Offset: 0x000007F3
		public override bool CanWrite
		{
			get
			{
				return !this.IsDisposed && this.InnerStream.CanWrite;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000037 RID: 55 RVA: 0x0000260A File Offset: 0x0000080A
		public override long Length
		{
			get
			{
				this.ThrowIfDisposed();
				return this.InnerStream.Length;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000038 RID: 56 RVA: 0x0000261D File Offset: 0x0000081D
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002630 File Offset: 0x00000830
		public override long Position
		{
			get
			{
				this.ThrowIfDisposed();
				return this.InnerStream.Position;
			}
			set
			{
				this.ThrowIfDisposed();
				this.InnerStream.Position = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002644 File Offset: 0x00000844
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002657 File Offset: 0x00000857
		public override int ReadTimeout
		{
			get
			{
				this.ThrowIfDisposed();
				return this.InnerStream.ReadTimeout;
			}
			set
			{
				this.ThrowIfDisposed();
				this.InnerStream.ReadTimeout = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003C RID: 60 RVA: 0x0000266B File Offset: 0x0000086B
		// (set) Token: 0x0600003D RID: 61 RVA: 0x0000267E File Offset: 0x0000087E
		public override int WriteTimeout
		{
			get
			{
				this.ThrowIfDisposed();
				return this.InnerStream.WriteTimeout;
			}
			set
			{
				this.ThrowIfDisposed();
				this.InnerStream.WriteTimeout = value;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002692 File Offset: 0x00000892
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			this.ThrowIfDisposed();
			return this.InnerStream.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000026AC File Offset: 0x000008AC
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			this.ThrowIfDisposed();
			return this.InnerStream.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000026C6 File Offset: 0x000008C6
		public override void Close()
		{
			base.Close();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000026CE File Offset: 0x000008CE
		public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			this.ThrowIfDisposed();
			return this.InnerStream.CopyToAsync(destination, bufferSize, cancellationToken);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000026E4 File Offset: 0x000008E4
		protected override void Dispose(bool disposing)
		{
			if (!this.IsDisposed)
			{
				base.Dispose(disposing);
				this.IsDisposed = true;
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000026FC File Offset: 0x000008FC
		public override int EndRead(IAsyncResult asyncResult)
		{
			this.ThrowIfDisposed();
			return this.InnerStream.EndRead(asyncResult);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002710 File Offset: 0x00000910
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this.ThrowIfDisposed();
			this.InnerStream.EndWrite(asyncResult);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002724 File Offset: 0x00000924
		public override void Flush()
		{
			this.ThrowIfDisposed();
			this.InnerStream.Flush();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002737 File Offset: 0x00000937
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			this.ThrowIfDisposed();
			return this.InnerStream.FlushAsync(cancellationToken);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000274B File Offset: 0x0000094B
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.ThrowIfDisposed();
			return this.InnerStream.Read(buffer, offset, count);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002761 File Offset: 0x00000961
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			this.ThrowIfDisposed();
			return this.InnerStream.ReadAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002779 File Offset: 0x00000979
		public override int ReadByte()
		{
			this.ThrowIfDisposed();
			return this.InnerStream.ReadByte();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000278C File Offset: 0x0000098C
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.ThrowIfDisposed();
			return this.InnerStream.Seek(offset, origin);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000027A1 File Offset: 0x000009A1
		public override void SetLength(long value)
		{
			this.ThrowIfDisposed();
			this.InnerStream.SetLength(value);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000027B5 File Offset: 0x000009B5
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.ThrowIfDisposed();
			this.InnerStream.Write(buffer, offset, count);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000027CB File Offset: 0x000009CB
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			this.ThrowIfDisposed();
			return this.InnerStream.WriteAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000027E3 File Offset: 0x000009E3
		public override void WriteByte(byte value)
		{
			this.ThrowIfDisposed();
			this.InnerStream.WriteByte(value);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000027F7 File Offset: 0x000009F7
		protected void ThrowIfDisposed()
		{
			if (this.IsDisposed)
			{
				throw new ObjectDisposedException(null);
			}
		}
	}
}
