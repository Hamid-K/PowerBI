using System;
using System.IO;

namespace Microsoft.OData.Core
{
	// Token: 0x02000127 RID: 295
	internal static class MessageStreamWrapper
	{
		// Token: 0x06000B22 RID: 2850 RVA: 0x000290BC File Offset: 0x000272BC
		internal static Stream CreateNonDisposingStream(Stream innerStream)
		{
			return new MessageStreamWrapper.MessageStreamWrappingStream(innerStream, true, -1L);
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x000290C7 File Offset: 0x000272C7
		internal static Stream CreateStreamWithMaxSize(Stream innerStream, long maxBytesToBeRead)
		{
			return new MessageStreamWrapper.MessageStreamWrappingStream(innerStream, false, maxBytesToBeRead);
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x000290D1 File Offset: 0x000272D1
		internal static Stream CreateNonDisposingStreamWithMaxSize(Stream innerStream, long maxBytesToBeRead)
		{
			return new MessageStreamWrapper.MessageStreamWrappingStream(innerStream, true, maxBytesToBeRead);
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x000290DC File Offset: 0x000272DC
		internal static bool IsNonDisposingStream(Stream stream)
		{
			MessageStreamWrapper.MessageStreamWrappingStream messageStreamWrappingStream = stream as MessageStreamWrapper.MessageStreamWrappingStream;
			return messageStreamWrappingStream != null && messageStreamWrappingStream.IgnoreDispose;
		}

		// Token: 0x02000128 RID: 296
		private sealed class MessageStreamWrappingStream : Stream
		{
			// Token: 0x06000B26 RID: 2854 RVA: 0x000290FB File Offset: 0x000272FB
			internal MessageStreamWrappingStream(Stream innerStream, bool ignoreDispose, long maxBytesToBeRead)
			{
				this.innerStream = innerStream;
				this.ignoreDispose = ignoreDispose;
				this.maxBytesToBeRead = maxBytesToBeRead;
			}

			// Token: 0x1700024F RID: 591
			// (get) Token: 0x06000B27 RID: 2855 RVA: 0x00029118 File Offset: 0x00027318
			public override bool CanRead
			{
				get
				{
					return this.innerStream.CanRead;
				}
			}

			// Token: 0x17000250 RID: 592
			// (get) Token: 0x06000B28 RID: 2856 RVA: 0x00029125 File Offset: 0x00027325
			public override bool CanSeek
			{
				get
				{
					return this.innerStream.CanSeek;
				}
			}

			// Token: 0x17000251 RID: 593
			// (get) Token: 0x06000B29 RID: 2857 RVA: 0x00029132 File Offset: 0x00027332
			public override bool CanWrite
			{
				get
				{
					return this.innerStream.CanWrite;
				}
			}

			// Token: 0x17000252 RID: 594
			// (get) Token: 0x06000B2A RID: 2858 RVA: 0x0002913F File Offset: 0x0002733F
			public override long Length
			{
				get
				{
					return this.innerStream.Length;
				}
			}

			// Token: 0x17000253 RID: 595
			// (get) Token: 0x06000B2B RID: 2859 RVA: 0x0002914C File Offset: 0x0002734C
			// (set) Token: 0x06000B2C RID: 2860 RVA: 0x00029159 File Offset: 0x00027359
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

			// Token: 0x17000254 RID: 596
			// (get) Token: 0x06000B2D RID: 2861 RVA: 0x00029167 File Offset: 0x00027367
			internal bool IgnoreDispose
			{
				get
				{
					return this.ignoreDispose;
				}
			}

			// Token: 0x06000B2E RID: 2862 RVA: 0x0002916F File Offset: 0x0002736F
			public override void Flush()
			{
				this.innerStream.Flush();
			}

			// Token: 0x06000B2F RID: 2863 RVA: 0x0002917C File Offset: 0x0002737C
			public override int Read(byte[] buffer, int offset, int count)
			{
				int num = this.innerStream.Read(buffer, offset, count);
				this.IncreaseTotalBytesRead(num);
				return num;
			}

			// Token: 0x06000B30 RID: 2864 RVA: 0x000291A0 File Offset: 0x000273A0
			public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				return this.innerStream.BeginRead(buffer, offset, count, callback, state);
			}

			// Token: 0x06000B31 RID: 2865 RVA: 0x000291B4 File Offset: 0x000273B4
			public override int EndRead(IAsyncResult asyncResult)
			{
				int num = this.innerStream.EndRead(asyncResult);
				this.IncreaseTotalBytesRead(num);
				return num;
			}

			// Token: 0x06000B32 RID: 2866 RVA: 0x000291D6 File Offset: 0x000273D6
			public override long Seek(long offset, SeekOrigin origin)
			{
				return this.innerStream.Seek(offset, origin);
			}

			// Token: 0x06000B33 RID: 2867 RVA: 0x000291E5 File Offset: 0x000273E5
			public override void SetLength(long value)
			{
				this.innerStream.SetLength(value);
			}

			// Token: 0x06000B34 RID: 2868 RVA: 0x000291F3 File Offset: 0x000273F3
			public override void Write(byte[] buffer, int offset, int count)
			{
				this.innerStream.Write(buffer, offset, count);
			}

			// Token: 0x06000B35 RID: 2869 RVA: 0x00029203 File Offset: 0x00027403
			public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				return this.innerStream.BeginWrite(buffer, offset, count, callback, state);
			}

			// Token: 0x06000B36 RID: 2870 RVA: 0x00029217 File Offset: 0x00027417
			public override void EndWrite(IAsyncResult asyncResult)
			{
				this.innerStream.EndWrite(asyncResult);
			}

			// Token: 0x06000B37 RID: 2871 RVA: 0x00029225 File Offset: 0x00027425
			protected override void Dispose(bool disposing)
			{
				if (disposing && !this.ignoreDispose && this.innerStream != null)
				{
					this.innerStream.Dispose();
					this.innerStream = null;
				}
				base.Dispose(disposing);
			}

			// Token: 0x06000B38 RID: 2872 RVA: 0x00029254 File Offset: 0x00027454
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

			// Token: 0x0400047F RID: 1151
			private readonly long maxBytesToBeRead;

			// Token: 0x04000480 RID: 1152
			private readonly bool ignoreDispose;

			// Token: 0x04000481 RID: 1153
			private Stream innerStream;

			// Token: 0x04000482 RID: 1154
			private long totalBytesRead;
		}
	}
}
