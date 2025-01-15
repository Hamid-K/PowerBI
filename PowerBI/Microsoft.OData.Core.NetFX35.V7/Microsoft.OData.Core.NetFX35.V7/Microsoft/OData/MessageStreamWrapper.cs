using System;
using System.IO;

namespace Microsoft.OData
{
	// Token: 0x02000025 RID: 37
	internal static class MessageStreamWrapper
	{
		// Token: 0x060000F2 RID: 242 RVA: 0x0000508A File Offset: 0x0000328A
		internal static Stream CreateNonDisposingStream(Stream innerStream)
		{
			return new MessageStreamWrapper.MessageStreamWrappingStream(innerStream, true, -1L);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00005095 File Offset: 0x00003295
		internal static Stream CreateStreamWithMaxSize(Stream innerStream, long maxBytesToBeRead)
		{
			return new MessageStreamWrapper.MessageStreamWrappingStream(innerStream, false, maxBytesToBeRead);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000509F File Offset: 0x0000329F
		internal static Stream CreateNonDisposingStreamWithMaxSize(Stream innerStream, long maxBytesToBeRead)
		{
			return new MessageStreamWrapper.MessageStreamWrappingStream(innerStream, true, maxBytesToBeRead);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000050AC File Offset: 0x000032AC
		internal static bool IsNonDisposingStream(Stream stream)
		{
			MessageStreamWrapper.MessageStreamWrappingStream messageStreamWrappingStream = stream as MessageStreamWrapper.MessageStreamWrappingStream;
			return messageStreamWrappingStream != null && messageStreamWrappingStream.IgnoreDispose;
		}

		// Token: 0x0200024B RID: 587
		private sealed class MessageStreamWrappingStream : Stream
		{
			// Token: 0x06001740 RID: 5952 RVA: 0x00047068 File Offset: 0x00045268
			internal MessageStreamWrappingStream(Stream innerStream, bool ignoreDispose, long maxBytesToBeRead)
			{
				this.innerStream = innerStream;
				this.ignoreDispose = ignoreDispose;
				this.maxBytesToBeRead = maxBytesToBeRead;
			}

			// Token: 0x17000537 RID: 1335
			// (get) Token: 0x06001741 RID: 5953 RVA: 0x00047085 File Offset: 0x00045285
			public override bool CanRead
			{
				get
				{
					return this.innerStream.CanRead;
				}
			}

			// Token: 0x17000538 RID: 1336
			// (get) Token: 0x06001742 RID: 5954 RVA: 0x00047092 File Offset: 0x00045292
			public override bool CanSeek
			{
				get
				{
					return this.innerStream.CanSeek;
				}
			}

			// Token: 0x17000539 RID: 1337
			// (get) Token: 0x06001743 RID: 5955 RVA: 0x0004709F File Offset: 0x0004529F
			public override bool CanWrite
			{
				get
				{
					return this.innerStream.CanWrite;
				}
			}

			// Token: 0x1700053A RID: 1338
			// (get) Token: 0x06001744 RID: 5956 RVA: 0x000470AC File Offset: 0x000452AC
			public override long Length
			{
				get
				{
					return this.innerStream.Length;
				}
			}

			// Token: 0x1700053B RID: 1339
			// (get) Token: 0x06001745 RID: 5957 RVA: 0x000470B9 File Offset: 0x000452B9
			// (set) Token: 0x06001746 RID: 5958 RVA: 0x000470C6 File Offset: 0x000452C6
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

			// Token: 0x1700053C RID: 1340
			// (get) Token: 0x06001747 RID: 5959 RVA: 0x000470D4 File Offset: 0x000452D4
			internal bool IgnoreDispose
			{
				get
				{
					return this.ignoreDispose;
				}
			}

			// Token: 0x06001748 RID: 5960 RVA: 0x000470DC File Offset: 0x000452DC
			public override void Flush()
			{
				this.innerStream.Flush();
			}

			// Token: 0x06001749 RID: 5961 RVA: 0x000470EC File Offset: 0x000452EC
			public override int Read(byte[] buffer, int offset, int count)
			{
				int num = this.innerStream.Read(buffer, offset, count);
				this.IncreaseTotalBytesRead(num);
				return num;
			}

			// Token: 0x0600174A RID: 5962 RVA: 0x00047110 File Offset: 0x00045310
			public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				return this.innerStream.BeginRead(buffer, offset, count, callback, state);
			}

			// Token: 0x0600174B RID: 5963 RVA: 0x00047124 File Offset: 0x00045324
			public override int EndRead(IAsyncResult asyncResult)
			{
				int num = this.innerStream.EndRead(asyncResult);
				this.IncreaseTotalBytesRead(num);
				return num;
			}

			// Token: 0x0600174C RID: 5964 RVA: 0x00047146 File Offset: 0x00045346
			public override long Seek(long offset, SeekOrigin origin)
			{
				return this.innerStream.Seek(offset, origin);
			}

			// Token: 0x0600174D RID: 5965 RVA: 0x00047155 File Offset: 0x00045355
			public override void SetLength(long value)
			{
				this.innerStream.SetLength(value);
			}

			// Token: 0x0600174E RID: 5966 RVA: 0x00047163 File Offset: 0x00045363
			public override void Write(byte[] buffer, int offset, int count)
			{
				this.innerStream.Write(buffer, offset, count);
			}

			// Token: 0x0600174F RID: 5967 RVA: 0x00047173 File Offset: 0x00045373
			public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				return this.innerStream.BeginWrite(buffer, offset, count, callback, state);
			}

			// Token: 0x06001750 RID: 5968 RVA: 0x00047187 File Offset: 0x00045387
			public override void EndWrite(IAsyncResult asyncResult)
			{
				this.innerStream.EndWrite(asyncResult);
			}

			// Token: 0x06001751 RID: 5969 RVA: 0x00047195 File Offset: 0x00045395
			protected override void Dispose(bool disposing)
			{
				if (disposing && !this.ignoreDispose && this.innerStream != null)
				{
					this.innerStream.Dispose();
					this.innerStream = null;
				}
				base.Dispose(disposing);
			}

			// Token: 0x06001752 RID: 5970 RVA: 0x000471C4 File Offset: 0x000453C4
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

			// Token: 0x04000AC0 RID: 2752
			private readonly long maxBytesToBeRead;

			// Token: 0x04000AC1 RID: 2753
			private readonly bool ignoreDispose;

			// Token: 0x04000AC2 RID: 2754
			private Stream innerStream;

			// Token: 0x04000AC3 RID: 2755
			private long totalBytesRead;
		}
	}
}
