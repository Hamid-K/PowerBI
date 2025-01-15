using System;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http.Properties;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace System.Net.Http.Internal
{
	// Token: 0x0200002D RID: 45
	internal class ByteRangeStream : DelegatingStream
	{
		// Token: 0x060001AC RID: 428 RVA: 0x00006144 File Offset: 0x00004344
		public ByteRangeStream(Stream innerStream, RangeItemHeaderValue range)
			: base(innerStream)
		{
			if (range == null)
			{
				throw Error.ArgumentNull("range");
			}
			if (!innerStream.CanSeek)
			{
				throw Error.Argument("innerStream", Resources.ByteRangeStreamNotSeekable, new object[] { typeof(ByteRangeStream).Name });
			}
			if (innerStream.Length < 1L)
			{
				throw Error.ArgumentOutOfRange("innerStream", innerStream.Length, Resources.ByteRangeStreamEmpty, new object[] { typeof(ByteRangeStream).Name });
			}
			if (range.From != null && range.From.Value > innerStream.Length)
			{
				throw Error.ArgumentOutOfRange("range", range.From, Resources.ByteRangeStreamInvalidFrom, new object[] { innerStream.Length });
			}
			long num = innerStream.Length - 1L;
			long num2;
			if (range.To != null)
			{
				if (range.From != null)
				{
					num2 = Math.Min(range.To.Value, num);
					this._lowerbounds = range.From.Value;
				}
				else
				{
					num2 = num;
					this._lowerbounds = Math.Max(innerStream.Length - range.To.Value, 0L);
				}
			}
			else if (range.From != null)
			{
				num2 = num;
				this._lowerbounds = range.From.Value;
			}
			else
			{
				num2 = num;
				this._lowerbounds = 0L;
			}
			this._totalCount = num2 - this._lowerbounds + 1L;
			this.ContentRange = new ContentRangeHeaderValue(this._lowerbounds, num2, innerStream.Length);
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001AD RID: 429 RVA: 0x000062FE File Offset: 0x000044FE
		// (set) Token: 0x060001AE RID: 430 RVA: 0x00006306 File Offset: 0x00004506
		public ContentRangeHeaderValue ContentRange { get; private set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000630F File Offset: 0x0000450F
		public override long Length
		{
			get
			{
				return this._totalCount;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00006317 File Offset: 0x00004517
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x0000631A File Offset: 0x0000451A
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x00006322 File Offset: 0x00004522
		public override long Position
		{
			get
			{
				return this._currentCount;
			}
			set
			{
				if (value < 0L)
				{
					throw Error.ArgumentMustBeGreaterThanOrEqualTo("value", value, 0L);
				}
				this._currentCount = value;
			}
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00006348 File Offset: 0x00004548
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return base.BeginRead(buffer, offset, this.PrepareStreamForRangeRead(count), callback, state);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000635D File Offset: 0x0000455D
		public override int Read(byte[] buffer, int offset, int count)
		{
			return base.Read(buffer, offset, this.PrepareStreamForRangeRead(count));
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000636E File Offset: 0x0000456E
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return base.ReadAsync(buffer, offset, this.PrepareStreamForRangeRead(count), cancellationToken);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00006381 File Offset: 0x00004581
		public override int ReadByte()
		{
			if (this.PrepareStreamForRangeRead(1) <= 0)
			{
				return -1;
			}
			return base.ReadByte();
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00006398 File Offset: 0x00004598
		public override long Seek(long offset, SeekOrigin origin)
		{
			switch (origin)
			{
			case SeekOrigin.Begin:
				this._currentCount = offset;
				break;
			case SeekOrigin.Current:
				this._currentCount += offset;
				break;
			case SeekOrigin.End:
				this._currentCount = this._totalCount + offset;
				break;
			default:
				throw Error.InvalidEnumArgument("origin", (int)origin, typeof(SeekOrigin));
			}
			if (this._currentCount < 0L)
			{
				throw new IOException(Resources.ByteRangeStreamInvalidOffset);
			}
			return this._currentCount;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00006413 File Offset: 0x00004613
		public override void SetLength(long value)
		{
			throw Error.NotSupported(Resources.ByteRangeStreamReadOnly, new object[0]);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00006413 File Offset: 0x00004613
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw Error.NotSupported(Resources.ByteRangeStreamReadOnly, new object[0]);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00006413 File Offset: 0x00004613
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			throw Error.NotSupported(Resources.ByteRangeStreamReadOnly, new object[0]);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00006413 File Offset: 0x00004613
		public override void EndWrite(IAsyncResult asyncResult)
		{
			throw Error.NotSupported(Resources.ByteRangeStreamReadOnly, new object[0]);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00006413 File Offset: 0x00004613
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			throw Error.NotSupported(Resources.ByteRangeStreamReadOnly, new object[0]);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00006413 File Offset: 0x00004613
		public override void WriteByte(byte value)
		{
			throw Error.NotSupported(Resources.ByteRangeStreamReadOnly, new object[0]);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00006428 File Offset: 0x00004628
		private int PrepareStreamForRangeRead(int count)
		{
			if (count <= 0)
			{
				return count;
			}
			if (this._currentCount >= this._totalCount)
			{
				return 0;
			}
			long num = Math.Min((long)count, this._totalCount - this._currentCount);
			long num2 = this._lowerbounds + this._currentCount;
			long position = base.InnerStream.Position;
			if (num2 != position)
			{
				base.InnerStream.Position = num2;
			}
			this._currentCount += num;
			return (int)num;
		}

		// Token: 0x04000088 RID: 136
		private readonly long _lowerbounds;

		// Token: 0x04000089 RID: 137
		private readonly long _totalCount;

		// Token: 0x0400008A RID: 138
		private long _currentCount;
	}
}
