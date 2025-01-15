using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.OData
{
	// Token: 0x0200002D RID: 45
	internal sealed class BufferedReadStream : Stream
	{
		// Token: 0x0600018C RID: 396 RVA: 0x000044E2 File Offset: 0x000026E2
		private BufferedReadStream(Stream inputStream)
		{
			this.buffers = new List<BufferedReadStream.DataBuffer>();
			this.inputStream = inputStream;
			this.currentBufferIndex = -1;
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00002393 File Offset: 0x00000593
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00002390 File Offset: 0x00000590
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00002390 File Offset: 0x00000590
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00002396 File Offset: 0x00000596
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00002396 File Offset: 0x00000596
		// (set) Token: 0x06000192 RID: 402 RVA: 0x00002396 File Offset: 0x00000596
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

		// Token: 0x06000193 RID: 403 RVA: 0x00002396 File Offset: 0x00000596
		public override void Flush()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00004504 File Offset: 0x00002704
		public override int Read(byte[] buffer, int offset, int count)
		{
			ExceptionUtils.CheckArgumentNotNull<byte[]>(buffer, "buffer");
			if (this.currentBufferIndex == -1)
			{
				return 0;
			}
			BufferedReadStream.DataBuffer dataBuffer = this.buffers[this.currentBufferIndex];
			while (this.currentBufferReadCount >= dataBuffer.StoredCount)
			{
				this.currentBufferIndex++;
				if (this.currentBufferIndex >= this.buffers.Count)
				{
					this.currentBufferIndex = -1;
					return 0;
				}
				dataBuffer = this.buffers[this.currentBufferIndex];
				this.currentBufferReadCount = 0;
			}
			int num = count;
			if (count > dataBuffer.StoredCount - this.currentBufferReadCount)
			{
				num = dataBuffer.StoredCount - this.currentBufferReadCount;
			}
			Array.Copy(dataBuffer.Buffer, this.currentBufferReadCount, buffer, offset, num);
			this.currentBufferReadCount += num;
			return num;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00002396 File Offset: 0x00000596
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00002396 File Offset: 0x00000596
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00002396 File Offset: 0x00000596
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000198 RID: 408 RVA: 0x000045D0 File Offset: 0x000027D0
		internal static Task<BufferedReadStream> BufferStreamAsync(Stream inputStream)
		{
			BufferedReadStream bufferedReadStream = new BufferedReadStream(inputStream);
			return Task.Factory.Iterate(bufferedReadStream.BufferInputStream()).FollowAlwaysWith(delegate(Task task)
			{
				inputStream.Dispose();
			}).FollowOnSuccessWith(delegate(Task task)
			{
				bufferedReadStream.ResetForReading();
				return bufferedReadStream;
			});
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00004632 File Offset: 0x00002832
		internal void ResetForReading()
		{
			this.currentBufferIndex = ((this.buffers.Count == 0) ? (-1) : 0);
			this.currentBufferReadCount = 0;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00004652 File Offset: 0x00002852
		private IEnumerable<Task> BufferInputStream()
		{
			while (this.inputStream != null)
			{
				BufferedReadStream.DataBuffer currentBuffer = ((this.currentBufferIndex == -1) ? null : this.buffers[this.currentBufferIndex]);
				if (currentBuffer != null && currentBuffer.FreeBytes < 1024)
				{
					currentBuffer = null;
				}
				if (currentBuffer == null)
				{
					currentBuffer = this.AddNewBuffer();
				}
				yield return this.inputStream.ReadAsync(currentBuffer.Buffer, currentBuffer.OffsetToWriteTo, currentBuffer.FreeBytes).ContinueWith(delegate(Task<int> t)
				{
					try
					{
						int result = t.Result;
						if (result == 0)
						{
							this.inputStream = null;
						}
						else
						{
							currentBuffer.MarkBytesAsWritten(result);
						}
					}
					catch (Exception ex)
					{
						if (!ExceptionUtils.IsCatchableExceptionType(ex))
						{
							throw;
						}
						this.inputStream = null;
						throw;
					}
				});
			}
			yield break;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00004664 File Offset: 0x00002864
		private BufferedReadStream.DataBuffer AddNewBuffer()
		{
			BufferedReadStream.DataBuffer dataBuffer = new BufferedReadStream.DataBuffer();
			this.buffers.Add(dataBuffer);
			this.currentBufferIndex = this.buffers.Count - 1;
			return dataBuffer;
		}

		// Token: 0x04000080 RID: 128
		private readonly List<BufferedReadStream.DataBuffer> buffers;

		// Token: 0x04000081 RID: 129
		private Stream inputStream;

		// Token: 0x04000082 RID: 130
		private int currentBufferIndex;

		// Token: 0x04000083 RID: 131
		private int currentBufferReadCount;

		// Token: 0x0200027F RID: 639
		private sealed class DataBuffer
		{
			// Token: 0x06001C1B RID: 7195 RVA: 0x00055E34 File Offset: 0x00054034
			public DataBuffer()
			{
				this.buffer = new byte[65536];
				this.StoredCount = 0;
			}

			// Token: 0x170005B9 RID: 1465
			// (get) Token: 0x06001C1C RID: 7196 RVA: 0x00055E53 File Offset: 0x00054053
			public byte[] Buffer
			{
				get
				{
					return this.buffer;
				}
			}

			// Token: 0x170005BA RID: 1466
			// (get) Token: 0x06001C1D RID: 7197 RVA: 0x00055E5B File Offset: 0x0005405B
			public int OffsetToWriteTo
			{
				get
				{
					return this.StoredCount;
				}
			}

			// Token: 0x170005BB RID: 1467
			// (get) Token: 0x06001C1E RID: 7198 RVA: 0x00055E63 File Offset: 0x00054063
			// (set) Token: 0x06001C1F RID: 7199 RVA: 0x00055E6B File Offset: 0x0005406B
			public int StoredCount { get; private set; }

			// Token: 0x170005BC RID: 1468
			// (get) Token: 0x06001C20 RID: 7200 RVA: 0x00055E74 File Offset: 0x00054074
			public int FreeBytes
			{
				get
				{
					return this.buffer.Length - this.StoredCount;
				}
			}

			// Token: 0x06001C21 RID: 7201 RVA: 0x00055E85 File Offset: 0x00054085
			public void MarkBytesAsWritten(int count)
			{
				this.StoredCount += count;
			}

			// Token: 0x04000BD5 RID: 3029
			internal const int MinReadBufferSize = 1024;

			// Token: 0x04000BD6 RID: 3030
			private const int BufferSize = 65536;

			// Token: 0x04000BD7 RID: 3031
			private readonly byte[] buffer;
		}
	}
}
