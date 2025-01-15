using System;
using System.IO;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Internal
{
	// Token: 0x020001C1 RID: 449
	internal class ParallelWriteBehindStream : ForwardWriteOnlyStream
	{
		// Token: 0x060008A0 RID: 2208 RVA: 0x00010F20 File Offset: 0x0000F120
		public static Stream NewBuffering(int pageSize, Func<Stream> createBuffer, IEngineHost engineHost, IResource resource, int writers, Action<int, Stream> putBufferedPage)
		{
			return new ParallelWriteBehindStream(pageSize, createBuffer, new ParallelAppender<Stream>(engineHost, resource, writers, putBufferedPage, delegate(Stream page)
			{
				page.Dispose();
			}));
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00010F53 File Offset: 0x0000F153
		private ParallelWriteBehindStream(int pageSize, Func<Stream> createBuffer, IAppender<Stream> streams)
		{
			this.oneByteBuffer = new byte[1];
			this.pageSize = pageSize;
			this.createBuffer = createBuffer;
			this.streams = streams;
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x060008A2 RID: 2210 RVA: 0x00010F7C File Offset: 0x0000F17C
		// (set) Token: 0x060008A3 RID: 2211 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override long Position
		{
			get
			{
				return this.position;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00010F84 File Offset: 0x0000F184
		public override void WriteByte(byte value)
		{
			this.oneByteBuffer[0] = value;
			this.Write(this.oneByteBuffer, 0, 1);
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x00010FA0 File Offset: 0x0000F1A0
		public override void Write(byte[] buffer, int offset, int count)
		{
			while (count > 0)
			{
				if (this.currentStream == null)
				{
					this.currentStream = this.createBuffer();
				}
				int num = (int)Math.Min((long)count, (long)this.pageSize - this.currentStream.Length);
				if (num > 0)
				{
					this.currentStream.Write(buffer, offset, num);
					offset += num;
					count -= num;
					this.position += (long)num;
				}
				else
				{
					this.Flush();
				}
			}
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0001101B File Offset: 0x0000F21B
		public override void Flush()
		{
			if (this.currentStream != null)
			{
				this.currentStream.Flush();
				this.currentStream.Position = 0L;
				this.streams.Append(this.currentStream);
				this.currentStream = null;
			}
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x00011055 File Offset: 0x0000F255
		public override void Close()
		{
			this.Flush();
			this.streams.Dispose();
		}

		// Token: 0x040004FD RID: 1277
		private readonly byte[] oneByteBuffer;

		// Token: 0x040004FE RID: 1278
		private readonly int pageSize;

		// Token: 0x040004FF RID: 1279
		private readonly Func<Stream> createBuffer;

		// Token: 0x04000500 RID: 1280
		private IAppender<Stream> streams;

		// Token: 0x04000501 RID: 1281
		private Stream currentStream;

		// Token: 0x04000502 RID: 1282
		private long position;
	}
}
