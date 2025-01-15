using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x02000006 RID: 6
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ReadOnlyMemoryStream : Stream
	{
		// Token: 0x06000019 RID: 25 RVA: 0x000022E7 File Offset: 0x000004E7
		[NullableContext(0)]
		public ReadOnlyMemoryStream(ReadOnlyMemory<byte> content)
		{
			this._content = content;
			this._isOpen = true;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000022FD File Offset: 0x000004FD
		public override bool CanRead
		{
			get
			{
				return this._isOpen;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002305 File Offset: 0x00000505
		public override bool CanSeek
		{
			get
			{
				return this._isOpen;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000230D File Offset: 0x0000050D
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002310 File Offset: 0x00000510
		public override long Length
		{
			get
			{
				this.ValidateNotClosed();
				return (long)this._content.Length;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002324 File Offset: 0x00000524
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002333 File Offset: 0x00000533
		public override long Position
		{
			get
			{
				this.ValidateNotClosed();
				return (long)this._position;
			}
			set
			{
				this.ValidateNotClosed();
				if (value < 0L || value > 2147483647L)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._position = (int)value;
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000235C File Offset: 0x0000055C
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.ValidateNotClosed();
			long num;
			if (origin != SeekOrigin.Begin)
			{
				if (origin != SeekOrigin.Current)
				{
					if (origin != SeekOrigin.End)
					{
						throw new ArgumentOutOfRangeException("origin");
					}
					num = (long)this._content.Length + offset;
				}
				else
				{
					num = (long)this._position + offset;
				}
			}
			else
			{
				num = offset;
			}
			long num2 = num;
			if (num2 > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (num2 < 0L)
			{
				throw new IOException("An attempt was made to move the position before the beginning of the stream.");
			}
			this._position = (int)num2;
			return (long)this._position;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000023D8 File Offset: 0x000005D8
		public unsafe override int ReadByte()
		{
			this.ValidateNotClosed();
			ReadOnlySpan<byte> span = this._content.Span;
			if (this._position >= span.Length)
			{
				return -1;
			}
			int position = this._position;
			this._position = position + 1;
			return (int)(*span[position]);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002421 File Offset: 0x00000621
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.ValidateNotClosed();
			ReadOnlyMemoryStream.ValidateReadArrayArguments(buffer, offset, count);
			return this.ReadBuffer(new Span<byte>(buffer, offset, count));
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002440 File Offset: 0x00000640
		[NullableContext(0)]
		private int ReadBuffer(Span<byte> buffer)
		{
			int num = this._content.Length - this._position;
			if (num <= 0 || buffer.Length == 0)
			{
				return 0;
			}
			if (num <= buffer.Length)
			{
				this._content.Span.Slice(this._position).CopyTo(buffer);
				this._position = this._content.Length;
				return num;
			}
			this._content.Span.Slice(this._position, buffer.Length).CopyTo(buffer);
			this._position += buffer.Length;
			return buffer.Length;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000024F2 File Offset: 0x000006F2
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			this.ValidateNotClosed();
			ReadOnlyMemoryStream.ValidateReadArrayArguments(buffer, offset, count);
			if (!cancellationToken.IsCancellationRequested)
			{
				return Task.FromResult<int>(this.ReadBuffer(new Span<byte>(buffer, offset, count)));
			}
			return Task.FromCanceled<int>(cancellationToken);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002526 File Offset: 0x00000726
		public override void Flush()
		{
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002528 File Offset: 0x00000728
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000252F File Offset: 0x0000072F
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002536 File Offset: 0x00000736
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000253D File Offset: 0x0000073D
		private static void ValidateReadArrayArguments(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || buffer.Length - offset < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002573 File Offset: 0x00000773
		private void ValidateNotClosed()
		{
			if (!this._isOpen)
			{
				throw new ObjectDisposedException(null, "Cannot access a closed Stream");
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000258C File Offset: 0x0000078C
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this._isOpen = false;
					this._content = default(ReadOnlyMemory<byte>);
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x04000005 RID: 5
		[Nullable(0)]
		private ReadOnlyMemory<byte> _content;

		// Token: 0x04000006 RID: 6
		private bool _isOpen;

		// Token: 0x04000007 RID: 7
		private int _position;
	}
}
