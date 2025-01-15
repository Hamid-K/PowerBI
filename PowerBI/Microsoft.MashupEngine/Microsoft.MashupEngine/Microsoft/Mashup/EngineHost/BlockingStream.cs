using System;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.OleDb.Serialization;

namespace Microsoft.Mashup.EngineHost
{
	// Token: 0x0200194D RID: 6477
	internal class BlockingStream
	{
		// Token: 0x0600A444 RID: 42052 RVA: 0x00220337 File Offset: 0x0021E537
		public BlockingStream(int maxChunks, int startChunkSize, int maxChunkSize)
		{
			this.queue = new BlockingQueue<byte[]>(maxChunks);
			this.inputStream = new BlockingStream.BlockingInputStream(this);
			this.outputStream = new BlockingStream.BlockingOutputStream(this, startChunkSize, maxChunkSize);
			this.disposalLock = new object();
			this.disposed = false;
		}

		// Token: 0x170029EA RID: 10730
		// (get) Token: 0x0600A445 RID: 42053 RVA: 0x00220377 File Offset: 0x0021E577
		public Stream InputStream
		{
			get
			{
				return this.inputStream;
			}
		}

		// Token: 0x170029EB RID: 10731
		// (get) Token: 0x0600A446 RID: 42054 RVA: 0x0022037F File Offset: 0x0021E57F
		public Stream OutputStream
		{
			get
			{
				return this.outputStream;
			}
		}

		// Token: 0x170029EC RID: 10732
		// (get) Token: 0x0600A447 RID: 42055 RVA: 0x00220388 File Offset: 0x0021E588
		private bool Disposed
		{
			get
			{
				object obj = this.disposalLock;
				bool flag2;
				lock (obj)
				{
					flag2 = this.disposed;
				}
				return flag2;
			}
		}

		// Token: 0x0600A448 RID: 42056 RVA: 0x002203CC File Offset: 0x0021E5CC
		private void DisposeStreams()
		{
			object obj = this.disposalLock;
			bool flag2;
			lock (obj)
			{
				flag2 = this.disposed;
				this.disposed = true;
			}
			if (!flag2)
			{
				if (this.queue.IsFull)
				{
					this.queue.Dequeue();
				}
				if (this.queue.IsEmpty)
				{
					this.queue.Enqueue(new byte[0]);
				}
			}
		}

		// Token: 0x0400558B RID: 21899
		private readonly BlockingQueue<byte[]> queue;

		// Token: 0x0400558C RID: 21900
		private readonly Stream inputStream;

		// Token: 0x0400558D RID: 21901
		private readonly Stream outputStream;

		// Token: 0x0400558E RID: 21902
		private readonly object disposalLock;

		// Token: 0x0400558F RID: 21903
		private bool disposed;

		// Token: 0x0200194E RID: 6478
		private class BlockingInputStream : ChunkedInputStream
		{
			// Token: 0x0600A449 RID: 42057 RVA: 0x00220450 File Offset: 0x0021E650
			public BlockingInputStream(BlockingStream shared)
			{
				this.shared = shared;
			}

			// Token: 0x0600A44A RID: 42058 RVA: 0x0022045F File Offset: 0x0021E65F
			protected override byte[] ReadNextChunk()
			{
				if (this.shared.Disposed)
				{
					return new byte[0];
				}
				return this.shared.queue.Dequeue();
			}

			// Token: 0x0600A44B RID: 42059 RVA: 0x00220485 File Offset: 0x0021E685
			protected override void Dispose(bool disposing)
			{
				base.Dispose(disposing);
				if (disposing)
				{
					this.shared.DisposeStreams();
				}
			}

			// Token: 0x04005590 RID: 21904
			private readonly BlockingStream shared;
		}

		// Token: 0x0200194F RID: 6479
		private class BlockingOutputStream : ChunkedOutputStream
		{
			// Token: 0x0600A44C RID: 42060 RVA: 0x0022049C File Offset: 0x0021E69C
			public BlockingOutputStream(BlockingStream shared, int startChunkSize, int maxChunkSize)
				: base(startChunkSize, maxChunkSize)
			{
				this.shared = shared;
			}

			// Token: 0x0600A44D RID: 42061 RVA: 0x002204B0 File Offset: 0x0021E6B0
			protected override void WriteNextChunk(byte[] buffer)
			{
				if (!this.shared.Disposed)
				{
					byte[] array = new byte[buffer.Length];
					Buffer.BlockCopy(buffer, 0, array, 0, buffer.Length);
					this.shared.queue.Enqueue(array);
				}
			}

			// Token: 0x0600A44E RID: 42062 RVA: 0x002204F0 File Offset: 0x0021E6F0
			protected override void Dispose(bool disposing)
			{
				base.Dispose(disposing);
				if (disposing)
				{
					this.shared.DisposeStreams();
				}
			}

			// Token: 0x04005591 RID: 21905
			private readonly BlockingStream shared;
		}
	}
}
