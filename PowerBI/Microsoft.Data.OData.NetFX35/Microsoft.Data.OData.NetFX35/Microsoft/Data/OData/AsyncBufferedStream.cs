using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Data.OData
{
	// Token: 0x02000283 RID: 643
	internal sealed class AsyncBufferedStream : Stream
	{
		// Token: 0x0600143C RID: 5180 RVA: 0x0004A544 File Offset: 0x00048744
		internal AsyncBufferedStream(Stream stream)
		{
			this.innerStream = stream;
			this.bufferQueue = new Queue<AsyncBufferedStream.DataBuffer>();
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x0600143D RID: 5181 RVA: 0x0004A55E File Offset: 0x0004875E
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x0600143E RID: 5182 RVA: 0x0004A561 File Offset: 0x00048761
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x0600143F RID: 5183 RVA: 0x0004A564 File Offset: 0x00048764
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x0004A567 File Offset: 0x00048767
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06001441 RID: 5185 RVA: 0x0004A56E File Offset: 0x0004876E
		// (set) Token: 0x06001442 RID: 5186 RVA: 0x0004A575 File Offset: 0x00048775
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

		// Token: 0x06001443 RID: 5187 RVA: 0x0004A57C File Offset: 0x0004877C
		public override void Flush()
		{
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x0004A57E File Offset: 0x0004877E
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x0004A585 File Offset: 0x00048785
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x0004A58C File Offset: 0x0004878C
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x0004A594 File Offset: 0x00048794
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

		// Token: 0x06001448 RID: 5192 RVA: 0x0004A5DC File Offset: 0x000487DC
		internal void Clear()
		{
			this.bufferQueue.Clear();
			this.bufferToAppendTo = null;
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x0004A5F0 File Offset: 0x000487F0
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

		// Token: 0x0600144A RID: 5194 RVA: 0x0004A624 File Offset: 0x00048824
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.bufferQueue.Count > 0)
			{
				throw new ODataException(Strings.AsyncBufferedStream_WriterDisposedWithoutFlush);
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x0004A649 File Offset: 0x00048849
		private void QueueNewBuffer()
		{
			this.bufferToAppendTo = new AsyncBufferedStream.DataBuffer();
			this.bufferQueue.Enqueue(this.bufferToAppendTo);
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x0004A668 File Offset: 0x00048868
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

		// Token: 0x040007C4 RID: 1988
		private readonly Stream innerStream;

		// Token: 0x040007C5 RID: 1989
		private Queue<AsyncBufferedStream.DataBuffer> bufferQueue;

		// Token: 0x040007C6 RID: 1990
		private AsyncBufferedStream.DataBuffer bufferToAppendTo;

		// Token: 0x02000284 RID: 644
		private sealed class DataBuffer
		{
			// Token: 0x0600144D RID: 5197 RVA: 0x0004A69E File Offset: 0x0004889E
			public DataBuffer()
			{
				this.buffer = new byte[80896];
				this.storedCount = 0;
			}

			// Token: 0x0600144E RID: 5198 RVA: 0x0004A6C0 File Offset: 0x000488C0
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

			// Token: 0x0600144F RID: 5199 RVA: 0x0004A718 File Offset: 0x00048918
			public void WriteToStream(Stream stream)
			{
				stream.Write(this.buffer, 0, this.storedCount);
			}

			// Token: 0x040007C7 RID: 1991
			private const int BufferSize = 80896;

			// Token: 0x040007C8 RID: 1992
			private readonly byte[] buffer;

			// Token: 0x040007C9 RID: 1993
			private int storedCount;
		}
	}
}
