using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.OData
{
	// Token: 0x02000005 RID: 5
	internal sealed class AsyncBufferedStream : Stream
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002376 File Offset: 0x00000576
		internal AsyncBufferedStream(Stream stream)
		{
			this.innerStream = stream;
			this.bufferQueue = new Queue<AsyncBufferedStream.DataBuffer>();
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002390 File Offset: 0x00000590
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002390 File Offset: 0x00000590
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002393 File Offset: 0x00000593
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002396 File Offset: 0x00000596
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002396 File Offset: 0x00000596
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002396 File Offset: 0x00000596
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

		// Token: 0x06000017 RID: 23 RVA: 0x0000239D File Offset: 0x0000059D
		public override void Flush()
		{
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002396 File Offset: 0x00000596
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002396 File Offset: 0x00000596
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002396 File Offset: 0x00000596
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023A0 File Offset: 0x000005A0
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

		// Token: 0x0600001C RID: 28 RVA: 0x000023E8 File Offset: 0x000005E8
		internal void Clear()
		{
			this.bufferQueue.Clear();
			this.bufferToAppendTo = null;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000023FC File Offset: 0x000005FC
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

		// Token: 0x0600001E RID: 30 RVA: 0x00002430 File Offset: 0x00000630
		internal new Task FlushAsync()
		{
			return this.FlushAsyncInternal();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002438 File Offset: 0x00000638
		internal Task FlushAsyncInternal()
		{
			Queue<AsyncBufferedStream.DataBuffer> queue = this.PrepareFlushBuffers();
			if (queue == null)
			{
				return TaskUtils.CompletedTask;
			}
			return Task.Factory.Iterate(this.FlushBuffersAsync(queue));
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002466 File Offset: 0x00000666
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.bufferQueue.Count > 0)
			{
				throw new ODataException(Strings.AsyncBufferedStream_WriterDisposedWithoutFlush);
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000248B File Offset: 0x0000068B
		private void QueueNewBuffer()
		{
			this.bufferToAppendTo = new AsyncBufferedStream.DataBuffer();
			this.bufferQueue.Enqueue(this.bufferToAppendTo);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000024AC File Offset: 0x000006AC
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

		// Token: 0x06000023 RID: 35 RVA: 0x000024E2 File Offset: 0x000006E2
		private IEnumerable<Task> FlushBuffersAsync(Queue<AsyncBufferedStream.DataBuffer> buffers)
		{
			while (buffers.Count > 0)
			{
				AsyncBufferedStream.DataBuffer dataBuffer = buffers.Dequeue();
				yield return dataBuffer.WriteToStreamAsync(this.innerStream);
			}
			yield break;
		}

		// Token: 0x04000010 RID: 16
		private readonly Stream innerStream;

		// Token: 0x04000011 RID: 17
		private Queue<AsyncBufferedStream.DataBuffer> bufferQueue;

		// Token: 0x04000012 RID: 18
		private AsyncBufferedStream.DataBuffer bufferToAppendTo;

		// Token: 0x02000272 RID: 626
		private sealed class DataBuffer
		{
			// Token: 0x06001BF3 RID: 7155 RVA: 0x00055B95 File Offset: 0x00053D95
			public DataBuffer()
			{
				this.buffer = new byte[80896];
				this.storedCount = 0;
			}

			// Token: 0x06001BF4 RID: 7156 RVA: 0x00055BB4 File Offset: 0x00053DB4
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

			// Token: 0x06001BF5 RID: 7157 RVA: 0x00055C0C File Offset: 0x00053E0C
			public void WriteToStream(Stream stream)
			{
				stream.Write(this.buffer, 0, this.storedCount);
			}

			// Token: 0x06001BF6 RID: 7158 RVA: 0x00055C21 File Offset: 0x00053E21
			public Task WriteToStreamAsync(Stream stream)
			{
				return stream.WriteAsync(this.buffer, 0, this.storedCount);
			}

			// Token: 0x04000BA9 RID: 2985
			private const int BufferSize = 80896;

			// Token: 0x04000BAA RID: 2986
			private readonly byte[] buffer;

			// Token: 0x04000BAB RID: 2987
			private int storedCount;
		}
	}
}
