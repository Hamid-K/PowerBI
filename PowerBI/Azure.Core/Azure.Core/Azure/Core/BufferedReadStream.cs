using System;
using System.Buffers;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.Core
{
	// Token: 0x0200004D RID: 77
	[NullableContext(1)]
	[Nullable(0)]
	internal class BufferedReadStream : Stream
	{
		// Token: 0x06000237 RID: 567 RVA: 0x00006E09 File Offset: 0x00005009
		public BufferedReadStream(Stream inner, int bufferSize)
			: this(inner, bufferSize, ArrayPool<byte>.Shared)
		{
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00006E18 File Offset: 0x00005018
		public BufferedReadStream(Stream inner, int bufferSize, ArrayPool<byte> bytePool)
		{
			if (inner == null)
			{
				throw new ArgumentNullException("inner");
			}
			this._inner = inner;
			this._bytePool = bytePool;
			this._buffer = bytePool.Rent(bufferSize);
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00006E49 File Offset: 0x00005049
		[Nullable(0)]
		public ArraySegment<byte> BufferedData
		{
			[NullableContext(0)]
			get
			{
				return new ArraySegment<byte>(this._buffer, this._bufferOffset, this._bufferCount);
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00006E62 File Offset: 0x00005062
		public override bool CanRead
		{
			get
			{
				return this._inner.CanRead || this._bufferCount > 0;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00006E7C File Offset: 0x0000507C
		public override bool CanSeek
		{
			get
			{
				return this._inner.CanSeek;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00006E89 File Offset: 0x00005089
		public override bool CanTimeout
		{
			get
			{
				return this._inner.CanTimeout;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00006E96 File Offset: 0x00005096
		public override bool CanWrite
		{
			get
			{
				return this._inner.CanWrite;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00006EA3 File Offset: 0x000050A3
		public override long Length
		{
			get
			{
				return this._inner.Length;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00006EB0 File Offset: 0x000050B0
		// (set) Token: 0x06000240 RID: 576 RVA: 0x00006EC8 File Offset: 0x000050C8
		public override long Position
		{
			get
			{
				return this._inner.Position - (long)this._bufferCount;
			}
			set
			{
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", value, "Position must be positive.");
				}
				if (value == this.Position)
				{
					return;
				}
				if (value > this._inner.Position)
				{
					this._bufferOffset = 0;
					this._bufferCount = 0;
					this._inner.Position = value;
					return;
				}
				int num = (int)(this._inner.Position - value);
				if (num <= this._bufferCount)
				{
					this._bufferOffset += num;
					this._bufferCount -= num;
					return;
				}
				this._bufferOffset = 0;
				this._bufferCount = 0;
				this._inner.Position = value;
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00006F72 File Offset: 0x00005172
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (origin == SeekOrigin.Begin)
			{
				this.Position = offset;
			}
			else if (origin == SeekOrigin.Current)
			{
				this.Position += offset;
			}
			else
			{
				this.Position = this.Length + offset;
			}
			return this.Position;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00006FA8 File Offset: 0x000051A8
		public override void SetLength(long value)
		{
			this._inner.SetLength(value);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00006FB6 File Offset: 0x000051B6
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (!this._disposed)
			{
				this._disposed = true;
				this._bytePool.Return(this._buffer, false);
				if (disposing)
				{
					this._inner.Dispose();
				}
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00006FEE File Offset: 0x000051EE
		public override void Flush()
		{
			this._inner.Flush();
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00006FFB File Offset: 0x000051FB
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return this._inner.FlushAsync(cancellationToken);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00007009 File Offset: 0x00005209
		public override void Write(byte[] buffer, int offset, int count)
		{
			this._inner.Write(buffer, offset, count);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00007019 File Offset: 0x00005219
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this._inner.WriteAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000702C File Offset: 0x0000522C
		public override int Read(byte[] buffer, int offset, int count)
		{
			BufferedReadStream.ValidateBuffer(buffer, offset, count);
			if (this._bufferCount > 0)
			{
				int num = Math.Min(this._bufferCount, count);
				Buffer.BlockCopy(this._buffer, this._bufferOffset, buffer, offset, num);
				this._bufferOffset += num;
				this._bufferCount -= num;
				return num;
			}
			return this._inner.Read(buffer, offset, count);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00007098 File Offset: 0x00005298
		public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			BufferedReadStream.ValidateBuffer(buffer, offset, count);
			int num2;
			if (this._bufferCount > 0)
			{
				int num = Math.Min(this._bufferCount, count);
				Buffer.BlockCopy(this._buffer, this._bufferOffset, buffer, offset, num);
				this._bufferOffset += num;
				this._bufferCount -= num;
				num2 = num;
			}
			else
			{
				num2 = await this._inner.ReadAsync(buffer, offset, count, cancellationToken).ConfigureAwait(false);
			}
			return num2;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x000070FC File Offset: 0x000052FC
		public bool EnsureBuffered()
		{
			if (this._bufferCount > 0)
			{
				return true;
			}
			this._bufferOffset = 0;
			this._bufferCount = this._inner.Read(this._buffer, 0, this._buffer.Length);
			return this._bufferCount > 0;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000713C File Offset: 0x0000533C
		public async Task<bool> EnsureBufferedAsync(CancellationToken cancellationToken)
		{
			bool flag;
			if (this._bufferCount > 0)
			{
				flag = true;
			}
			else
			{
				this._bufferOffset = 0;
				int num = await this._inner.ReadAsync(this._buffer, 0, this._buffer.Length, cancellationToken).ConfigureAwait(false);
				this._bufferCount = num;
				flag = this._bufferCount > 0;
			}
			return flag;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00007188 File Offset: 0x00005388
		public bool EnsureBuffered(int minCount)
		{
			if (minCount > this._buffer.Length)
			{
				throw new ArgumentOutOfRangeException("minCount", minCount, "The value must be smaller than the buffer size: " + this._buffer.Length.ToString(CultureInfo.InvariantCulture));
			}
			while (this._bufferCount < minCount)
			{
				if (this._bufferOffset > 0)
				{
					if (this._bufferCount > 0)
					{
						Buffer.BlockCopy(this._buffer, this._bufferOffset, this._buffer, 0, this._bufferCount);
					}
					this._bufferOffset = 0;
				}
				int num = this._inner.Read(this._buffer, this._bufferOffset + this._bufferCount, this._buffer.Length - this._bufferCount - this._bufferOffset);
				this._bufferCount += num;
				if (num == 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00007260 File Offset: 0x00005460
		public async Task<bool> EnsureBufferedAsync(int minCount, CancellationToken cancellationToken)
		{
			if (minCount > this._buffer.Length)
			{
				throw new ArgumentOutOfRangeException("minCount", minCount, "The value must be smaller than the buffer size: " + this._buffer.Length.ToString(CultureInfo.InvariantCulture));
			}
			while (this._bufferCount < minCount)
			{
				if (this._bufferOffset > 0)
				{
					if (this._bufferCount > 0)
					{
						Buffer.BlockCopy(this._buffer, this._bufferOffset, this._buffer, 0, this._bufferCount);
					}
					this._bufferOffset = 0;
				}
				int num = await this._inner.ReadAsync(this._buffer, this._bufferOffset + this._bufferCount, this._buffer.Length - this._bufferCount - this._bufferOffset, cancellationToken).ConfigureAwait(false);
				this._bufferCount += num;
				if (num == 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x000072B4 File Offset: 0x000054B4
		public string ReadLine(int lengthLimit)
		{
			this.CheckDisposed();
			string text;
			using (MemoryStream memoryStream = new MemoryStream(200))
			{
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				while (!flag2 && !flag3 && this.EnsureBuffered())
				{
					if (memoryStream.Length > (long)lengthLimit)
					{
						throw new InvalidDataException(string.Format("Line length limit {0} exceeded.", lengthLimit));
					}
					this.ProcessLineChar(memoryStream, ref flag, ref flag2, ref flag3);
				}
				text = BufferedReadStream.DecodeLine(memoryStream, flag2, flag3);
			}
			return text;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000733C File Offset: 0x0000553C
		public async Task<string> ReadLineAsync(int lengthLimit, CancellationToken cancellationToken)
		{
			this.CheckDisposed();
			string text;
			using (MemoryStream builder = new MemoryStream(200))
			{
				bool foundCR = false;
				bool foundCRLF = false;
				bool foundLF = false;
				for (;;)
				{
					bool flag = !foundCRLF && !foundLF;
					if (flag)
					{
						flag = await this.EnsureBufferedAsync(cancellationToken).ConfigureAwait(false);
					}
					if (!flag)
					{
						goto Block_5;
					}
					if (builder.Length > (long)lengthLimit)
					{
						break;
					}
					this.ProcessLineChar(builder, ref foundCR, ref foundCRLF, ref foundLF);
				}
				throw new InvalidDataException(string.Format("Line length limit {0} exceeded.", lengthLimit));
				Block_5:
				text = BufferedReadStream.DecodeLine(builder, foundCRLF, foundLF);
			}
			return text;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00007390 File Offset: 0x00005590
		private void ProcessLineChar(MemoryStream builder, ref bool foundCR, ref bool foundCRLF, ref bool foundLF)
		{
			byte b = this._buffer[this._bufferOffset];
			builder.WriteByte(b);
			this._bufferOffset++;
			this._bufferCount--;
			if (b == 10)
			{
				foundLF = true;
				foundCRLF = foundCR & foundLF;
				return;
			}
			foundCR = b == 13;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x000073E8 File Offset: 0x000055E8
		private static string DecodeLine(MemoryStream builder, bool foundCRLF, bool foundLF)
		{
			long num;
			if (foundCRLF)
			{
				num = builder.Length - 2L;
			}
			else if (foundLF)
			{
				num = builder.Length - 1L;
			}
			else
			{
				num = builder.Length;
			}
			long num2 = num;
			return Encoding.UTF8.GetString(builder.ToArray(), 0, (int)num2);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000742F File Offset: 0x0000562F
		private void CheckDisposed()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("BufferedReadStream");
			}
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00007444 File Offset: 0x00005644
		private static void ValidateBuffer(byte[] buffer, int offset, int count)
		{
			new ArraySegment<byte>(buffer, offset, count);
			if (count == 0)
			{
				throw new ArgumentOutOfRangeException("count", "The value must be greater than zero.");
			}
		}

		// Token: 0x040000FB RID: 251
		private const byte CR = 13;

		// Token: 0x040000FC RID: 252
		private const byte LF = 10;

		// Token: 0x040000FD RID: 253
		private readonly Stream _inner;

		// Token: 0x040000FE RID: 254
		private readonly byte[] _buffer;

		// Token: 0x040000FF RID: 255
		private readonly ArrayPool<byte> _bytePool;

		// Token: 0x04000100 RID: 256
		private int _bufferOffset;

		// Token: 0x04000101 RID: 257
		private int _bufferCount;

		// Token: 0x04000102 RID: 258
		private bool _disposed;
	}
}
