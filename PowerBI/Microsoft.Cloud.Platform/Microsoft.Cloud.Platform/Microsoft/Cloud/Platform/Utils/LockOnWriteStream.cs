using System;
using System.IO;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000297 RID: 663
	public class LockOnWriteStream : Stream
	{
		// Token: 0x060011CC RID: 4556 RVA: 0x0003E4FE File Offset: 0x0003C6FE
		public LockOnWriteStream([NotNull] object locker, [NotNull] Stream stream)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<object>(locker, "locker");
			ExtendedDiagnostics.EnsureArgumentNotNull<Stream>(stream, "stream");
			this.m_locker = locker;
			this.m_stream = stream;
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060011CD RID: 4557 RVA: 0x0003E52A File Offset: 0x0003C72A
		public override bool CanRead
		{
			get
			{
				return this.m_stream.CanRead;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060011CE RID: 4558 RVA: 0x0003E537 File Offset: 0x0003C737
		public override bool CanSeek
		{
			get
			{
				return this.m_stream.CanSeek;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060011CF RID: 4559 RVA: 0x0003E544 File Offset: 0x0003C744
		public override bool CanWrite
		{
			get
			{
				return this.m_stream.CanWrite;
			}
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x0003E551 File Offset: 0x0003C751
		public override void Flush()
		{
			this.m_stream.Flush();
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x060011D1 RID: 4561 RVA: 0x0003E55E File Offset: 0x0003C75E
		public override long Length
		{
			get
			{
				return this.m_stream.Length;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x060011D2 RID: 4562 RVA: 0x0003E56B File Offset: 0x0003C76B
		// (set) Token: 0x060011D3 RID: 4563 RVA: 0x0003E578 File Offset: 0x0003C778
		public override long Position
		{
			get
			{
				return this.m_stream.Position;
			}
			set
			{
				this.m_stream.Position = value;
			}
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x0003E586 File Offset: 0x0003C786
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.m_stream.Read(buffer, offset, count);
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x0003E598 File Offset: 0x0003C798
		public override long Seek(long offset, SeekOrigin origin)
		{
			object locker = this.m_locker;
			long num;
			lock (locker)
			{
				num = this.m_stream.Seek(offset, origin);
			}
			return num;
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x0003E5E4 File Offset: 0x0003C7E4
		public override void SetLength(long value)
		{
			this.m_stream.SetLength(value);
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x0003E5F4 File Offset: 0x0003C7F4
		public override void Write(byte[] buffer, int offset, int count)
		{
			object locker = this.m_locker;
			lock (locker)
			{
				this.m_stream.Write(buffer, offset, count);
			}
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x0003E63C File Offset: 0x0003C83C
		public override void Close()
		{
			object locker = this.m_locker;
			lock (locker)
			{
				this.m_stream.Close();
			}
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x0003E684 File Offset: 0x0003C884
		public new void Dispose()
		{
			object locker = this.m_locker;
			lock (locker)
			{
				this.m_stream.Dispose();
			}
		}

		// Token: 0x040006A7 RID: 1703
		private object m_locker;

		// Token: 0x040006A8 RID: 1704
		private Stream m_stream;
	}
}
