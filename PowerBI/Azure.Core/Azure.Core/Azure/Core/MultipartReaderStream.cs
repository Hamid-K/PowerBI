using System;
using System.Buffers;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.Core
{
	// Token: 0x02000051 RID: 81
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class MultipartReaderStream : Stream
	{
		// Token: 0x0600026F RID: 623 RVA: 0x00007907 File Offset: 0x00005B07
		public MultipartReaderStream(BufferedReadStream stream, MultipartBoundary boundary)
			: this(stream, boundary, ArrayPool<byte>.Shared)
		{
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00007918 File Offset: 0x00005B18
		public MultipartReaderStream(BufferedReadStream stream, MultipartBoundary boundary, ArrayPool<byte> bytePool)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (boundary == null)
			{
				throw new ArgumentNullException("boundary");
			}
			this._bytePool = bytePool;
			this._innerStream = stream;
			this._innerOffset = (this._innerStream.CanSeek ? this._innerStream.Position : 0L);
			this._boundary = boundary;
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000797E File Offset: 0x00005B7E
		// (set) Token: 0x06000272 RID: 626 RVA: 0x00007986 File Offset: 0x00005B86
		public bool FinalBoundaryFound { get; private set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000798F File Offset: 0x00005B8F
		// (set) Token: 0x06000274 RID: 628 RVA: 0x00007997 File Offset: 0x00005B97
		public long? LengthLimit { get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000275 RID: 629 RVA: 0x000079A0 File Offset: 0x00005BA0
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000276 RID: 630 RVA: 0x000079A3 File Offset: 0x00005BA3
		public override bool CanSeek
		{
			get
			{
				return this._innerStream.CanSeek;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000277 RID: 631 RVA: 0x000079B0 File Offset: 0x00005BB0
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000278 RID: 632 RVA: 0x000079B3 File Offset: 0x00005BB3
		public override long Length
		{
			get
			{
				return this._observedLength;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000279 RID: 633 RVA: 0x000079BB File Offset: 0x00005BBB
		// (set) Token: 0x0600027A RID: 634 RVA: 0x000079C4 File Offset: 0x00005BC4
		public override long Position
		{
			get
			{
				return this._position;
			}
			set
			{
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", value, "The Position must be positive.");
				}
				if (value > this._observedLength)
				{
					throw new ArgumentOutOfRangeException("value", value, "The Position must be less than length.");
				}
				this._position = value;
				if (this._position < this._observedLength)
				{
					this._finished = false;
				}
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00007A27 File Offset: 0x00005C27
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

		// Token: 0x0600027C RID: 636 RVA: 0x00007A5D File Offset: 0x00005C5D
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00007A64 File Offset: 0x00005C64
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00007A6B File Offset: 0x00005C6B
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00007A72 File Offset: 0x00005C72
		public override void Flush()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00007A7C File Offset: 0x00005C7C
		private void PositionInnerStream()
		{
			if (this._innerStream.CanSeek && this._innerStream.Position != this._innerOffset + this._position)
			{
				this._innerStream.Position = this._innerOffset + this._position;
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00007AC8 File Offset: 0x00005CC8
		private int UpdatePosition(int read)
		{
			this._position += (long)read;
			if (this._observedLength < this._position)
			{
				this._observedLength = this._position;
				if (this.LengthLimit != null && this._observedLength > this.LengthLimit.GetValueOrDefault())
				{
					throw new InvalidDataException(string.Format("Multipart body length limit {0} exceeded.", this.LengthLimit.GetValueOrDefault()));
				}
			}
			return read;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00007B48 File Offset: 0x00005D48
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._finished)
			{
				return 0;
			}
			this.PositionInnerStream();
			if (!this._innerStream.EnsureBuffered(this._boundary.FinalBoundaryLength))
			{
				throw new IOException("Unexpected end of Stream, the content may have already been read by another component. ");
			}
			ArraySegment<byte> bufferedData = this._innerStream.BufferedData;
			int num;
			int num2;
			int num3;
			if (!this.SubMatch(bufferedData, this._boundary.BoundaryBytes, out num, out num2))
			{
				num3 = this._innerStream.Read(buffer, offset, Math.Min(count, bufferedData.Count));
				return this.UpdatePosition(num3);
			}
			if (num > bufferedData.Offset)
			{
				num3 = this._innerStream.Read(buffer, offset, Math.Min(count, num - bufferedData.Offset));
				return this.UpdatePosition(num3);
			}
			int num4 = this._boundary.BoundaryBytes.Length;
			byte[] array = this._bytePool.Rent(num4);
			num3 = this._innerStream.Read(array, 0, num4);
			this._bytePool.Return(array, false);
			string text = this._innerStream.ReadLine(100);
			text = text.Trim();
			if (string.Equals("--", text, StringComparison.Ordinal))
			{
				this.FinalBoundaryFound = true;
			}
			this._finished = true;
			return 0;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00007C74 File Offset: 0x00005E74
		public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			int num;
			if (this._finished)
			{
				num = 0;
			}
			else
			{
				this.PositionInnerStream();
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this._innerStream.EnsureBufferedAsync(this._boundary.FinalBoundaryLength, cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				if (!configuredTaskAwaiter.GetResult())
				{
					throw new IOException("Unexpected end of Stream, the content may have already been read by another component. ");
				}
				ArraySegment<byte> bufferedData = this._innerStream.BufferedData;
				int num2;
				int num3;
				if (this.SubMatch(bufferedData, this._boundary.BoundaryBytes, out num2, out num3))
				{
					if (num2 > bufferedData.Offset)
					{
						int num4 = this._innerStream.Read(buffer, offset, Math.Min(count, num2 - bufferedData.Offset));
						num = this.UpdatePosition(num4);
					}
					else
					{
						int num5 = this._boundary.BoundaryBytes.Length;
						byte[] array = this._bytePool.Rent(num5);
						int num4 = this._innerStream.Read(array, 0, num5);
						this._bytePool.Return(array, false);
						if (string.Equals("--", (await this._innerStream.ReadLineAsync(100, cancellationToken).ConfigureAwait(false)).Trim(), StringComparison.Ordinal))
						{
							this.FinalBoundaryFound = true;
						}
						this._finished = true;
						num = 0;
					}
				}
				else
				{
					int num4 = this._innerStream.Read(buffer, offset, Math.Min(count, bufferedData.Count));
					num = this.UpdatePosition(num4);
				}
			}
			return num;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00007CD8 File Offset: 0x00005ED8
		[NullableContext(0)]
		private bool SubMatch(ArraySegment<byte> segment1, [Nullable(1)] byte[] matchBytes, out int matchOffset, out int matchCount)
		{
			matchCount = 0;
			int num = matchBytes.Length - 1;
			byte b = matchBytes[num];
			int num2 = segment1.Offset + segment1.Count - matchBytes.Length;
			byte b2;
			for (matchOffset = segment1.Offset; matchOffset < num2; matchOffset += this._boundary.GetSkipValue(b2))
			{
				b2 = segment1.Array[matchOffset + num];
				if (b2 == b && MultipartReaderStream.CompareBuffers(segment1.Array, matchOffset, matchBytes, 0, num) == 0)
				{
					matchCount = matchBytes.Length;
					return true;
				}
			}
			int num3 = segment1.Offset + segment1.Count;
			matchCount = 0;
			while (matchOffset < num3)
			{
				int num4 = num3 - matchOffset;
				matchCount = 0;
				while (matchCount < matchBytes.Length && matchCount < num4)
				{
					if (matchBytes[matchCount] != segment1.Array[matchOffset + matchCount])
					{
						matchCount = 0;
						break;
					}
					matchCount++;
				}
				if (matchCount > 0)
				{
					break;
				}
				matchOffset++;
			}
			return matchCount > 0;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00007DC2 File Offset: 0x00005FC2
		private static int CompareBuffers(byte[] buffer1, int offset1, byte[] buffer2, int offset2, int count)
		{
			while (count-- > 0)
			{
				if (buffer1[offset1] != buffer2[offset2])
				{
					return (int)(buffer1[offset1] - buffer2[offset2]);
				}
				offset1++;
				offset2++;
			}
			return 0;
		}

		// Token: 0x04000115 RID: 277
		private readonly MultipartBoundary _boundary;

		// Token: 0x04000116 RID: 278
		private readonly BufferedReadStream _innerStream;

		// Token: 0x04000117 RID: 279
		private readonly ArrayPool<byte> _bytePool;

		// Token: 0x04000118 RID: 280
		private readonly long _innerOffset;

		// Token: 0x04000119 RID: 281
		private long _position;

		// Token: 0x0400011A RID: 282
		private long _observedLength;

		// Token: 0x0400011B RID: 283
		private bool _finished;
	}
}
