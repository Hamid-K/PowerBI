using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.OData
{
	// Token: 0x02000007 RID: 7
	internal sealed class AsyncBufferedStream : Stream
	{
		// Token: 0x0600001B RID: 27 RVA: 0x000024E6 File Offset: 0x000006E6
		internal AsyncBufferedStream(Stream stream)
		{
			this.innerStream = stream;
			this.bufferQueue = new Queue<AsyncBufferedStream.DataBuffer>();
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002500 File Offset: 0x00000700
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002500 File Offset: 0x00000700
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002503 File Offset: 0x00000703
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002506 File Offset: 0x00000706
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002506 File Offset: 0x00000706
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002506 File Offset: 0x00000706
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

		// Token: 0x06000022 RID: 34 RVA: 0x0000250D File Offset: 0x0000070D
		public override void Flush()
		{
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002506 File Offset: 0x00000706
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002506 File Offset: 0x00000706
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002506 File Offset: 0x00000706
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002510 File Offset: 0x00000710
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

		// Token: 0x06000027 RID: 39 RVA: 0x00002558 File Offset: 0x00000758
		internal void Clear()
		{
			this.bufferQueue.Clear();
			this.bufferToAppendTo = null;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000256C File Offset: 0x0000076C
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

		// Token: 0x06000029 RID: 41 RVA: 0x000025A0 File Offset: 0x000007A0
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.bufferQueue.Count > 0)
			{
				throw new ODataException(Strings.AsyncBufferedStream_WriterDisposedWithoutFlush);
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000025C5 File Offset: 0x000007C5
		private void QueueNewBuffer()
		{
			this.bufferToAppendTo = new AsyncBufferedStream.DataBuffer();
			this.bufferQueue.Enqueue(this.bufferToAppendTo);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000025E4 File Offset: 0x000007E4
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

		// Token: 0x04000014 RID: 20
		private readonly Stream innerStream;

		// Token: 0x04000015 RID: 21
		private Queue<AsyncBufferedStream.DataBuffer> bufferQueue;

		// Token: 0x04000016 RID: 22
		private AsyncBufferedStream.DataBuffer bufferToAppendTo;

		// Token: 0x02000236 RID: 566
		private sealed class DataBuffer
		{
			// Token: 0x060016E6 RID: 5862 RVA: 0x000465CD File Offset: 0x000447CD
			public DataBuffer()
			{
				this.buffer = new byte[80896];
				this.storedCount = 0;
			}

			// Token: 0x060016E7 RID: 5863 RVA: 0x000465EC File Offset: 0x000447EC
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

			// Token: 0x060016E8 RID: 5864 RVA: 0x00046644 File Offset: 0x00044844
			public void WriteToStream(Stream stream)
			{
				stream.Write(this.buffer, 0, this.storedCount);
			}

			// Token: 0x04000A86 RID: 2694
			private const int BufferSize = 80896;

			// Token: 0x04000A87 RID: 2695
			private readonly byte[] buffer;

			// Token: 0x04000A88 RID: 2696
			private int storedCount;
		}
	}
}
