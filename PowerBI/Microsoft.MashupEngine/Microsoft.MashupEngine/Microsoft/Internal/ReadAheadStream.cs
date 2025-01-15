using System;
using System.IO;
using System.Threading;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Internal
{
	// Token: 0x020001C4 RID: 452
	internal class ReadAheadStream : ForwardReadOnlyStream
	{
		// Token: 0x060008B5 RID: 2229 RVA: 0x000112B8 File Offset: 0x0000F4B8
		public ReadAheadStream(IThreadPoolService threadPool, Stream stream, int bufferSize)
		{
			this.stream = stream;
			this.readChunk = new ReadAheadStream.Chunk(bufferSize);
			this.fillChunk = new ReadAheadStream.Chunk(bufferSize);
			this.readChunk.Next = this.fillChunk;
			this.fillChunk.Next = this.readChunk;
			this.fillChunk.ReadComplete();
			WaitCallback waitCallback = CloneCurrentCultures.CreateWrapper(new WaitCallback(this.Reader));
			threadPool.QueueUserWorkItem(waitCallback);
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x00011330 File Offset: 0x0000F530
		private void Reader(object obj)
		{
			for (;;)
			{
				this.fillChunk.WaitForRead();
				if (this.close)
				{
					break;
				}
				this.fillChunk.Fill(this.stream);
				this.fillChunk.FillComplete();
				this.fillChunk = this.fillChunk.Next;
			}
			this.fillChunk.FillComplete();
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0001138C File Offset: 0x0000F58C
		public override void Close()
		{
			if (this.stream != null)
			{
				this.close = true;
				this.readChunk.ReadComplete();
				this.readChunk = this.readChunk.Next;
				this.readChunk.WaitForFill();
				this.stream.Close();
				this.stream = null;
			}
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x000113E4 File Offset: 0x0000F5E4
		public override int ReadByte()
		{
			int num;
			for (;;)
			{
				num = this.readChunk.ReadByte();
				if (num != -1)
				{
					break;
				}
				if (this.readChunk.Eof)
				{
					return -1;
				}
				this.readChunk.ReadComplete();
				this.readChunk = this.readChunk.Next;
				this.readChunk.WaitForFill();
			}
			return num;
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0001143C File Offset: 0x0000F63C
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = offset;
			while (count > 0)
			{
				int num2 = this.readChunk.Read(buffer, offset, count);
				if (num2 == 0)
				{
					if (this.readChunk.Eof)
					{
						break;
					}
					this.readChunk.ReadComplete();
					this.readChunk = this.readChunk.Next;
					this.readChunk.WaitForFill();
				}
				offset += num2;
				count -= num2;
			}
			return offset - num;
		}

		// Token: 0x04000508 RID: 1288
		private Stream stream;

		// Token: 0x04000509 RID: 1289
		private bool close;

		// Token: 0x0400050A RID: 1290
		private ReadAheadStream.Chunk readChunk;

		// Token: 0x0400050B RID: 1291
		private ReadAheadStream.Chunk fillChunk;

		// Token: 0x020001C5 RID: 453
		private class Chunk
		{
			// Token: 0x060008BA RID: 2234 RVA: 0x000114A4 File Offset: 0x0000F6A4
			public Chunk(int maxBufferSize)
			{
				this.maxBufferSize = maxBufferSize;
				this.buffer = new byte[0];
				this.offset = 0;
				this.count = 0;
				this.fillComplete = new AutoResetEvent(false);
				this.readComplete = new AutoResetEvent(false);
			}

			// Token: 0x17000285 RID: 645
			// (get) Token: 0x060008BB RID: 2235 RVA: 0x000114F0 File Offset: 0x0000F6F0
			// (set) Token: 0x060008BC RID: 2236 RVA: 0x000114F8 File Offset: 0x0000F6F8
			public ReadAheadStream.Chunk Next
			{
				get
				{
					return this.next;
				}
				set
				{
					this.next = value;
				}
			}

			// Token: 0x17000286 RID: 646
			// (get) Token: 0x060008BD RID: 2237 RVA: 0x00011501 File Offset: 0x0000F701
			public int Count
			{
				get
				{
					return this.count;
				}
			}

			// Token: 0x17000287 RID: 647
			// (get) Token: 0x060008BE RID: 2238 RVA: 0x00011509 File Offset: 0x0000F709
			public bool Eof
			{
				get
				{
					return this.count != this.buffer.Length;
				}
			}

			// Token: 0x060008BF RID: 2239 RVA: 0x00011520 File Offset: 0x0000F720
			public int Read(byte[] buffer, int offset, int count)
			{
				int num = Math.Min(count, this.count - this.offset);
				Buffer.BlockCopy(this.buffer, this.offset, buffer, offset, num);
				this.offset += num;
				return num;
			}

			// Token: 0x060008C0 RID: 2240 RVA: 0x00011564 File Offset: 0x0000F764
			public int ReadByte()
			{
				if (this.offset == this.count)
				{
					return -1;
				}
				byte[] array = this.buffer;
				int num = this.offset;
				this.offset = num + 1;
				return array[num];
			}

			// Token: 0x060008C1 RID: 2241 RVA: 0x00011599 File Offset: 0x0000F799
			public void ReadComplete()
			{
				this.readComplete.Set();
			}

			// Token: 0x060008C2 RID: 2242 RVA: 0x000115A7 File Offset: 0x0000F7A7
			public void WaitForRead()
			{
				this.readComplete.WaitOne();
			}

			// Token: 0x060008C3 RID: 2243 RVA: 0x000115B5 File Offset: 0x0000F7B5
			public void FillComplete()
			{
				this.fillComplete.Set();
			}

			// Token: 0x060008C4 RID: 2244 RVA: 0x000115C3 File Offset: 0x0000F7C3
			public void WaitForFill()
			{
				this.fillComplete.WaitOne();
				if (this.exception != null)
				{
					throw this.exception;
				}
			}

			// Token: 0x060008C5 RID: 2245 RVA: 0x000115E0 File Offset: 0x0000F7E0
			public void Fill(Stream stream)
			{
				try
				{
					if (this.buffer.Length < this.maxBufferSize)
					{
						this.buffer = new byte[Math.Max(Math.Min(this.buffer.Length * 2, this.maxBufferSize), 4096)];
					}
					this.offset = 0;
					this.count = 0;
					while (this.count < this.buffer.Length)
					{
						int num = stream.Read(this.buffer, this.count, this.buffer.Length - this.count);
						if (num == 0)
						{
							break;
						}
						this.count += num;
					}
				}
				catch (Exception ex)
				{
					if (!SafeExceptions.IsSafeException(ex))
					{
						throw;
					}
					this.exception = ex;
				}
			}

			// Token: 0x0400050C RID: 1292
			private int maxBufferSize;

			// Token: 0x0400050D RID: 1293
			private byte[] buffer;

			// Token: 0x0400050E RID: 1294
			private int offset;

			// Token: 0x0400050F RID: 1295
			private int count;

			// Token: 0x04000510 RID: 1296
			private ReadAheadStream.Chunk next;

			// Token: 0x04000511 RID: 1297
			private AutoResetEvent fillComplete;

			// Token: 0x04000512 RID: 1298
			private AutoResetEvent readComplete;

			// Token: 0x04000513 RID: 1299
			private Exception exception;
		}
	}
}
