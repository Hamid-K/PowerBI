using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.OData.Core
{
	// Token: 0x02000009 RID: 9
	internal sealed class AsyncBufferedStream : Stream
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002496 File Offset: 0x00000696
		internal AsyncBufferedStream(Stream stream)
		{
			this.innerStream = stream;
			this.bufferQueue = new Queue<AsyncBufferedStream.DataBuffer>();
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000024B0 File Offset: 0x000006B0
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000024B3 File Offset: 0x000006B3
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000024B6 File Offset: 0x000006B6
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000024B9 File Offset: 0x000006B9
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000024C0 File Offset: 0x000006C0
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000024C7 File Offset: 0x000006C7
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000024CE File Offset: 0x000006CE
		public override void Flush()
		{
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000024D0 File Offset: 0x000006D0
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000024D7 File Offset: 0x000006D7
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000024DE File Offset: 0x000006DE
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000024E8 File Offset: 0x000006E8
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (count > 0)
			{
				if (this.bufferToAppendTo == null)
				{
					this.QueueNewBuffer();
				}
				while (count > 0)
				{
					int num = this.bufferToAppendTo.Write(buffer, offset, count);
					if (num < count)
					{
						this.QueueNewBuffer();
					}
					count -= num;
					offset += num;
				}
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002530 File Offset: 0x00000730
		internal void Clear()
		{
			this.bufferQueue.Clear();
			this.bufferToAppendTo = null;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002544 File Offset: 0x00000744
		internal void FlushSync()
		{
			Queue<AsyncBufferedStream.DataBuffer> queue = this.PrepareFlushBuffers();
			if (queue == null)
			{
				return;
			}
			while (queue.Count > 0)
			{
				AsyncBufferedStream.DataBuffer dataBuffer = queue.Dequeue();
				dataBuffer.WriteToStream(this.innerStream);
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002578 File Offset: 0x00000778
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.bufferQueue.Count > 0)
			{
				throw new ODataException(Strings.AsyncBufferedStream_WriterDisposedWithoutFlush);
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000259D File Offset: 0x0000079D
		private void QueueNewBuffer()
		{
			this.bufferToAppendTo = new AsyncBufferedStream.DataBuffer();
			this.bufferQueue.Enqueue(this.bufferToAppendTo);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000025BC File Offset: 0x000007BC
		private Queue<AsyncBufferedStream.DataBuffer> PrepareFlushBuffers()
		{
			if (this.bufferQueue.Count == 0)
			{
				return null;
			}
			this.bufferToAppendTo = null;
			Queue<AsyncBufferedStream.DataBuffer> queue = this.bufferQueue;
			this.bufferQueue = new Queue<AsyncBufferedStream.DataBuffer>();
			return queue;
		}

		// Token: 0x0400000E RID: 14
		private readonly Stream innerStream;

		// Token: 0x0400000F RID: 15
		private Queue<AsyncBufferedStream.DataBuffer> bufferQueue;

		// Token: 0x04000010 RID: 16
		private AsyncBufferedStream.DataBuffer bufferToAppendTo;

		// Token: 0x0200000A RID: 10
		private sealed class DataBuffer
		{
			// Token: 0x0600002C RID: 44 RVA: 0x000025F2 File Offset: 0x000007F2
			public DataBuffer()
			{
				this.buffer = new byte[80896];
				this.storedCount = 0;
			}

			// Token: 0x0600002D RID: 45 RVA: 0x00002614 File Offset: 0x00000814
			public int Write(byte[] data, int index, int count)
			{
				int num = count;
				if (num > this.buffer.Length - this.storedCount)
				{
					num = this.buffer.Length - this.storedCount;
				}
				if (num > 0)
				{
					Array.Copy(data, index, this.buffer, this.storedCount, num);
					this.storedCount += num;
				}
				return num;
			}

			// Token: 0x0600002E RID: 46 RVA: 0x0000266C File Offset: 0x0000086C
			public void WriteToStream(Stream stream)
			{
				stream.Write(this.buffer, 0, this.storedCount);
			}

			// Token: 0x04000011 RID: 17
			private const int BufferSize = 80896;

			// Token: 0x04000012 RID: 18
			private readonly byte[] buffer;

			// Token: 0x04000013 RID: 19
			private int storedCount;
		}
	}
}
