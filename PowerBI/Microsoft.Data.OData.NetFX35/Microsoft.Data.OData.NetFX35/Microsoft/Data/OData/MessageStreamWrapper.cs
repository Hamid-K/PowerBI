using System;
using System.IO;

namespace Microsoft.Data.OData
{
	// Token: 0x020001B6 RID: 438
	internal static class MessageStreamWrapper
	{
		// Token: 0x06000CDF RID: 3295 RVA: 0x0002D630 File Offset: 0x0002B830
		internal static Stream CreateNonDisposingStream(Stream innerStream)
		{
			return new MessageStreamWrapper.MessageStreamWrappingStream(innerStream, true, -1L);
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x0002D63B File Offset: 0x0002B83B
		internal static Stream CreateStreamWithMaxSize(Stream innerStream, long maxBytesToBeRead)
		{
			return new MessageStreamWrapper.MessageStreamWrappingStream(innerStream, false, maxBytesToBeRead);
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x0002D645 File Offset: 0x0002B845
		internal static Stream CreateNonDisposingStreamWithMaxSize(Stream innerStream, long maxBytesToBeRead)
		{
			return new MessageStreamWrapper.MessageStreamWrappingStream(innerStream, true, maxBytesToBeRead);
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x0002D650 File Offset: 0x0002B850
		internal static bool IsNonDisposingStream(Stream stream)
		{
			MessageStreamWrapper.MessageStreamWrappingStream messageStreamWrappingStream = stream as MessageStreamWrapper.MessageStreamWrappingStream;
			return messageStreamWrappingStream != null && messageStreamWrappingStream.IgnoreDispose;
		}

		// Token: 0x020001B7 RID: 439
		private sealed class MessageStreamWrappingStream : Stream
		{
			// Token: 0x06000CE3 RID: 3299 RVA: 0x0002D66F File Offset: 0x0002B86F
			internal MessageStreamWrappingStream(Stream innerStream, bool ignoreDispose, long maxBytesToBeRead)
			{
				this.innerStream = innerStream;
				this.ignoreDispose = ignoreDispose;
				this.maxBytesToBeRead = maxBytesToBeRead;
			}

			// Token: 0x170002EF RID: 751
			// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x0002D68C File Offset: 0x0002B88C
			public override bool CanRead
			{
				get
				{
					return this.innerStream.CanRead;
				}
			}

			// Token: 0x170002F0 RID: 752
			// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x0002D699 File Offset: 0x0002B899
			public override bool CanSeek
			{
				get
				{
					return this.innerStream.CanSeek;
				}
			}

			// Token: 0x170002F1 RID: 753
			// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x0002D6A6 File Offset: 0x0002B8A6
			public override bool CanWrite
			{
				get
				{
					return this.innerStream.CanWrite;
				}
			}

			// Token: 0x170002F2 RID: 754
			// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x0002D6B3 File Offset: 0x0002B8B3
			public override long Length
			{
				get
				{
					return this.innerStream.Length;
				}
			}

			// Token: 0x170002F3 RID: 755
			// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x0002D6C0 File Offset: 0x0002B8C0
			// (set) Token: 0x06000CE9 RID: 3305 RVA: 0x0002D6CD File Offset: 0x0002B8CD
			public override long Position
			{
				get
				{
					return this.innerStream.Position;
				}
				set
				{
					this.innerStream.Position = value;
				}
			}

			// Token: 0x170002F4 RID: 756
			// (get) Token: 0x06000CEA RID: 3306 RVA: 0x0002D6DB File Offset: 0x0002B8DB
			internal bool IgnoreDispose
			{
				get
				{
					return this.ignoreDispose;
				}
			}

			// Token: 0x06000CEB RID: 3307 RVA: 0x0002D6E3 File Offset: 0x0002B8E3
			public override void Flush()
			{
				this.innerStream.Flush();
			}

			// Token: 0x06000CEC RID: 3308 RVA: 0x0002D6F0 File Offset: 0x0002B8F0
			public override int Read(byte[] buffer, int offset, int count)
			{
				int num = this.innerStream.Read(buffer, offset, count);
				this.IncreaseTotalBytesRead(num);
				return num;
			}

			// Token: 0x06000CED RID: 3309 RVA: 0x0002D714 File Offset: 0x0002B914
			public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				return this.innerStream.BeginRead(buffer, offset, count, callback, state);
			}

			// Token: 0x06000CEE RID: 3310 RVA: 0x0002D728 File Offset: 0x0002B928
			public override int EndRead(IAsyncResult asyncResult)
			{
				int num = this.innerStream.EndRead(asyncResult);
				this.IncreaseTotalBytesRead(num);
				return num;
			}

			// Token: 0x06000CEF RID: 3311 RVA: 0x0002D74A File Offset: 0x0002B94A
			public override long Seek(long offset, SeekOrigin origin)
			{
				return this.innerStream.Seek(offset, origin);
			}

			// Token: 0x06000CF0 RID: 3312 RVA: 0x0002D759 File Offset: 0x0002B959
			public override void SetLength(long value)
			{
				this.innerStream.SetLength(value);
			}

			// Token: 0x06000CF1 RID: 3313 RVA: 0x0002D767 File Offset: 0x0002B967
			public override void Write(byte[] buffer, int offset, int count)
			{
				this.innerStream.Write(buffer, offset, count);
			}

			// Token: 0x06000CF2 RID: 3314 RVA: 0x0002D777 File Offset: 0x0002B977
			public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				return this.innerStream.BeginWrite(buffer, offset, count, callback, state);
			}

			// Token: 0x06000CF3 RID: 3315 RVA: 0x0002D78B File Offset: 0x0002B98B
			public override void EndWrite(IAsyncResult asyncResult)
			{
				this.innerStream.EndWrite(asyncResult);
			}

			// Token: 0x06000CF4 RID: 3316 RVA: 0x0002D799 File Offset: 0x0002B999
			protected override void Dispose(bool disposing)
			{
				if (disposing && !this.ignoreDispose && this.innerStream != null)
				{
					this.innerStream.Dispose();
					this.innerStream = null;
				}
				base.Dispose(disposing);
			}

			// Token: 0x06000CF5 RID: 3317 RVA: 0x0002D7C8 File Offset: 0x0002B9C8
			private void IncreaseTotalBytesRead(int bytesRead)
			{
				if (this.maxBytesToBeRead <= 0L)
				{
					return;
				}
				this.totalBytesRead += (long)((bytesRead < 0) ? 0 : bytesRead);
				if (this.totalBytesRead > this.maxBytesToBeRead)
				{
					throw new ODataException(Strings.MessageStreamWrappingStream_ByteLimitExceeded(this.totalBytesRead, this.maxBytesToBeRead));
				}
			}

			// Token: 0x0400048E RID: 1166
			private readonly long maxBytesToBeRead;

			// Token: 0x0400048F RID: 1167
			private readonly bool ignoreDispose;

			// Token: 0x04000490 RID: 1168
			private Stream innerStream;

			// Token: 0x04000491 RID: 1169
			private long totalBytesRead;
		}
	}
}
