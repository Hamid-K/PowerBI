using System;
using System.IO;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001C0D RID: 7181
	public abstract class PagesStream : Stream
	{
		// Token: 0x0600B32C RID: 45868 RVA: 0x002472E4 File Offset: 0x002454E4
		protected PagesStream(int pageSize, long totalLength)
		{
			this.pageSize = pageSize;
			this.length = totalLength;
			this.singleByteBuffer = new byte[1];
		}

		// Token: 0x0600B32D RID: 45869
		protected abstract int Read(int page, int pageOffset, int length, byte[] buffer, int bufferOffset);

		// Token: 0x0600B32E RID: 45870
		protected abstract void Write(int page, int pageOffset, int length, byte[] buffer, int bufferOffset);

		// Token: 0x17002CE5 RID: 11493
		// (get) Token: 0x0600B32F RID: 45871 RVA: 0x00247306 File Offset: 0x00245506
		// (set) Token: 0x0600B330 RID: 45872 RVA: 0x0024730E File Offset: 0x0024550E
		public override long Position
		{
			get
			{
				return this.position;
			}
			set
			{
				if (!this.CanSeek)
				{
					throw new InvalidOperationException();
				}
				if (value > this.Length)
				{
					throw new ArgumentException("offset");
				}
				this.Flush();
				this.position = value;
			}
		}

		// Token: 0x17002CE6 RID: 11494
		// (get) Token: 0x0600B331 RID: 45873 RVA: 0x0024733F File Offset: 0x0024553F
		public override long Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x0600B332 RID: 45874 RVA: 0x00247348 File Offset: 0x00245548
		public override long Seek(long offset, SeekOrigin origin)
		{
			long num;
			switch (origin)
			{
			case SeekOrigin.Begin:
				num = offset;
				break;
			case SeekOrigin.Current:
				num = this.Position + offset;
				break;
			case SeekOrigin.End:
				num = this.Length + offset;
				break;
			default:
				throw new ArgumentException("origin");
			}
			if (num > this.Length || num < 0L)
			{
				throw new ArgumentException("offset");
			}
			this.Position = num;
			return num;
		}

		// Token: 0x0600B333 RID: 45875 RVA: 0x000033E7 File Offset: 0x000015E7
		public override void SetLength(long length)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600B334 RID: 45876 RVA: 0x002473B0 File Offset: 0x002455B0
		public override int Read(byte[] array, int offset, int count)
		{
			int num = 0;
			count = (int)Math.Min((long)count, this.length - this.position);
			while (count > 0)
			{
				int num2 = (int)(this.position / (long)this.pageSize);
				int num3 = (int)(this.position % (long)this.pageSize);
				int num4 = Math.Min(this.pageSize - num3, count);
				if (num4 > 0)
				{
					int num5 = this.Read(num2, num3, num4, array, offset);
					if (num5 == 0)
					{
						break;
					}
					num += num5;
					offset += num5;
					count -= num5;
					this.position += (long)num5;
				}
			}
			return num;
		}

		// Token: 0x0600B335 RID: 45877 RVA: 0x00247444 File Offset: 0x00245644
		public override void Write(byte[] array, int offset, int count)
		{
			while (count > 0)
			{
				int num = (int)(this.position / (long)this.pageSize);
				int num2 = (int)(this.position % (long)this.pageSize);
				int num3 = Math.Min(this.pageSize - num2, count);
				if (num3 > 0)
				{
					this.Write(num, num2, num3, array, offset);
					offset += num3;
					count -= num3;
					this.position += (long)num3;
					this.length = Math.Max(this.length, this.position);
				}
			}
		}

		// Token: 0x0600B336 RID: 45878 RVA: 0x002474C5 File Offset: 0x002456C5
		public override int ReadByte()
		{
			if (this.Read(this.singleByteBuffer, 0, 1) != 0)
			{
				return (int)this.singleByteBuffer[0];
			}
			return -1;
		}

		// Token: 0x0600B337 RID: 45879 RVA: 0x002474E1 File Offset: 0x002456E1
		public override void WriteByte(byte value)
		{
			this.singleByteBuffer[0] = value;
			this.Write(this.singleByteBuffer, 0, 1);
		}

		// Token: 0x0600B338 RID: 45880 RVA: 0x0000336E File Offset: 0x0000156E
		public override void Flush()
		{
		}

		// Token: 0x04005B6C RID: 23404
		protected readonly int pageSize;

		// Token: 0x04005B6D RID: 23405
		private readonly byte[] singleByteBuffer;

		// Token: 0x04005B6E RID: 23406
		private long position;

		// Token: 0x04005B6F RID: 23407
		private long length;
	}
}
