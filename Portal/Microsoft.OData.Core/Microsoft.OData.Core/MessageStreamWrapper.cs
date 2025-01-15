using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.OData
{
	// Token: 0x0200004D RID: 77
	internal static class MessageStreamWrapper
	{
		// Token: 0x0600026B RID: 619 RVA: 0x00007BAF File Offset: 0x00005DAF
		internal static Stream CreateNonDisposingStream(Stream innerStream)
		{
			return new MessageStreamWrapper.MessageStreamWrappingStream(innerStream, true, -1L);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00007BBA File Offset: 0x00005DBA
		internal static Stream CreateStreamWithMaxSize(Stream innerStream, long maxBytesToBeRead)
		{
			return new MessageStreamWrapper.MessageStreamWrappingStream(innerStream, false, maxBytesToBeRead);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00007BC4 File Offset: 0x00005DC4
		internal static Stream CreateNonDisposingStreamWithMaxSize(Stream innerStream, long maxBytesToBeRead)
		{
			return new MessageStreamWrapper.MessageStreamWrappingStream(innerStream, true, maxBytesToBeRead);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00007BD0 File Offset: 0x00005DD0
		internal static bool IsNonDisposingStream(Stream stream)
		{
			MessageStreamWrapper.MessageStreamWrappingStream messageStreamWrappingStream = stream as MessageStreamWrapper.MessageStreamWrappingStream;
			return messageStreamWrappingStream != null && messageStreamWrappingStream.IgnoreDispose;
		}

		// Token: 0x02000298 RID: 664
		private sealed class MessageStreamWrappingStream : Stream
		{
			// Token: 0x06001C98 RID: 7320 RVA: 0x00056BD6 File Offset: 0x00054DD6
			internal MessageStreamWrappingStream(Stream innerStream, bool ignoreDispose, long maxBytesToBeRead)
			{
				this.innerStream = innerStream;
				this.ignoreDispose = ignoreDispose;
				this.maxBytesToBeRead = maxBytesToBeRead;
			}

			// Token: 0x170005D7 RID: 1495
			// (get) Token: 0x06001C99 RID: 7321 RVA: 0x00056BF3 File Offset: 0x00054DF3
			public override bool CanRead
			{
				get
				{
					return this.innerStream.CanRead;
				}
			}

			// Token: 0x170005D8 RID: 1496
			// (get) Token: 0x06001C9A RID: 7322 RVA: 0x00056C00 File Offset: 0x00054E00
			public override bool CanSeek
			{
				get
				{
					return this.innerStream.CanSeek;
				}
			}

			// Token: 0x170005D9 RID: 1497
			// (get) Token: 0x06001C9B RID: 7323 RVA: 0x00056C0D File Offset: 0x00054E0D
			public override bool CanWrite
			{
				get
				{
					return this.innerStream.CanWrite;
				}
			}

			// Token: 0x170005DA RID: 1498
			// (get) Token: 0x06001C9C RID: 7324 RVA: 0x00056C1A File Offset: 0x00054E1A
			public override long Length
			{
				get
				{
					return this.innerStream.Length;
				}
			}

			// Token: 0x170005DB RID: 1499
			// (get) Token: 0x06001C9D RID: 7325 RVA: 0x00056C27 File Offset: 0x00054E27
			// (set) Token: 0x06001C9E RID: 7326 RVA: 0x00056C34 File Offset: 0x00054E34
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

			// Token: 0x170005DC RID: 1500
			// (get) Token: 0x06001C9F RID: 7327 RVA: 0x00056C42 File Offset: 0x00054E42
			internal bool IgnoreDispose
			{
				get
				{
					return this.ignoreDispose;
				}
			}

			// Token: 0x06001CA0 RID: 7328 RVA: 0x00056C4A File Offset: 0x00054E4A
			public override void Flush()
			{
				this.innerStream.Flush();
			}

			// Token: 0x06001CA1 RID: 7329 RVA: 0x00056C57 File Offset: 0x00054E57
			public override Task FlushAsync(CancellationToken cancellationToken)
			{
				return this.innerStream.FlushAsync(cancellationToken);
			}

			// Token: 0x06001CA2 RID: 7330 RVA: 0x00056C68 File Offset: 0x00054E68
			public override int Read(byte[] buffer, int offset, int count)
			{
				int num = this.innerStream.Read(buffer, offset, count);
				this.IncreaseTotalBytesRead(num);
				return num;
			}

			// Token: 0x06001CA3 RID: 7331 RVA: 0x00056C8C File Offset: 0x00054E8C
			public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
			{
				int num = await this.innerStream.ReadAsync(buffer, offset, count, cancellationToken);
				int num2 = num;
				this.IncreaseTotalBytesRead(num2);
				return num2;
			}

			// Token: 0x06001CA4 RID: 7332 RVA: 0x00056CF2 File Offset: 0x00054EF2
			public override long Seek(long offset, SeekOrigin origin)
			{
				return this.innerStream.Seek(offset, origin);
			}

			// Token: 0x06001CA5 RID: 7333 RVA: 0x00056D01 File Offset: 0x00054F01
			public override void SetLength(long value)
			{
				this.innerStream.SetLength(value);
			}

			// Token: 0x06001CA6 RID: 7334 RVA: 0x00056D0F File Offset: 0x00054F0F
			public override void Write(byte[] buffer, int offset, int count)
			{
				this.innerStream.Write(buffer, offset, count);
			}

			// Token: 0x06001CA7 RID: 7335 RVA: 0x00056D1F File Offset: 0x00054F1F
			public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
			{
				return this.innerStream.WriteAsync(buffer, offset, count, cancellationToken);
			}

			// Token: 0x06001CA8 RID: 7336 RVA: 0x00056D31 File Offset: 0x00054F31
			protected override void Dispose(bool disposing)
			{
				if (disposing && !this.ignoreDispose && this.innerStream != null)
				{
					this.innerStream.Dispose();
					this.innerStream = null;
				}
				base.Dispose(disposing);
			}

			// Token: 0x06001CA9 RID: 7337 RVA: 0x00056D60 File Offset: 0x00054F60
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

			// Token: 0x04000C25 RID: 3109
			private readonly long maxBytesToBeRead;

			// Token: 0x04000C26 RID: 3110
			private readonly bool ignoreDispose;

			// Token: 0x04000C27 RID: 3111
			private Stream innerStream;

			// Token: 0x04000C28 RID: 3112
			private long totalBytesRead;
		}
	}
}
