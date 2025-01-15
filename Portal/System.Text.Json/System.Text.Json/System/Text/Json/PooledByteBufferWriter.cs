using System;
using System.Buffers;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Text.Json
{
	// Token: 0x0200002A RID: 42
	internal sealed class PooledByteBufferWriter : IBufferWriter<byte>, IDisposable
	{
		// Token: 0x06000156 RID: 342 RVA: 0x00003594 File Offset: 0x00001794
		private PooledByteBufferWriter()
		{
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000359C File Offset: 0x0000179C
		public PooledByteBufferWriter(int initialCapacity)
			: this()
		{
			this._rentedBuffer = ArrayPool<byte>.Shared.Rent(initialCapacity);
			this._index = 0;
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000158 RID: 344 RVA: 0x000035BC File Offset: 0x000017BC
		public ReadOnlyMemory<byte> WrittenMemory
		{
			get
			{
				return this._rentedBuffer.AsMemory(0, this._index);
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000159 RID: 345 RVA: 0x000035D5 File Offset: 0x000017D5
		public int WrittenCount
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600015A RID: 346 RVA: 0x000035DD File Offset: 0x000017DD
		public int Capacity
		{
			get
			{
				return this._rentedBuffer.Length;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600015B RID: 347 RVA: 0x000035E7 File Offset: 0x000017E7
		public int FreeCapacity
		{
			get
			{
				return this._rentedBuffer.Length - this._index;
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000035F8 File Offset: 0x000017F8
		public void Clear()
		{
			this.ClearHelper();
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00003600 File Offset: 0x00001800
		public void ClearAndReturnBuffers()
		{
			this.ClearHelper();
			byte[] rentedBuffer = this._rentedBuffer;
			this._rentedBuffer = null;
			ArrayPool<byte>.Shared.Return(rentedBuffer, false);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00003630 File Offset: 0x00001830
		private void ClearHelper()
		{
			this._rentedBuffer.AsSpan(0, this._index).Clear();
			this._index = 0;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00003660 File Offset: 0x00001860
		public void Dispose()
		{
			if (this._rentedBuffer == null)
			{
				return;
			}
			this.ClearHelper();
			byte[] rentedBuffer = this._rentedBuffer;
			this._rentedBuffer = null;
			ArrayPool<byte>.Shared.Return(rentedBuffer, false);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00003696 File Offset: 0x00001896
		public void InitializeEmptyInstance(int initialCapacity)
		{
			this._rentedBuffer = ArrayPool<byte>.Shared.Rent(initialCapacity);
			this._index = 0;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000036B0 File Offset: 0x000018B0
		public static PooledByteBufferWriter CreateEmptyInstanceForCaching()
		{
			return new PooledByteBufferWriter();
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000036B7 File Offset: 0x000018B7
		public void Advance(int count)
		{
			this._index += count;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000036C7 File Offset: 0x000018C7
		public Memory<byte> GetMemory(int sizeHint = 256)
		{
			this.CheckAndResizeBuffer(sizeHint);
			return this._rentedBuffer.AsMemory(this._index);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x000036E1 File Offset: 0x000018E1
		public Span<byte> GetSpan(int sizeHint = 256)
		{
			this.CheckAndResizeBuffer(sizeHint);
			return this._rentedBuffer.AsSpan(this._index);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000036FB File Offset: 0x000018FB
		internal Task WriteToStreamAsync(Stream destination, CancellationToken cancellationToken)
		{
			return destination.WriteAsync(this._rentedBuffer, 0, this._index, cancellationToken);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00003711 File Offset: 0x00001911
		internal void WriteToStream(Stream destination)
		{
			destination.Write(this._rentedBuffer, 0, this._index);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00003728 File Offset: 0x00001928
		private void CheckAndResizeBuffer(int sizeHint)
		{
			int num = this._rentedBuffer.Length;
			int num2 = num - this._index;
			if (this._index >= 1073741795)
			{
				sizeHint = Math.Max(sizeHint, 2147483591 - num);
			}
			if (sizeHint > num2)
			{
				int num3 = Math.Max(sizeHint, num);
				int num4 = num + num3;
				if (num4 > 2147483591)
				{
					num4 = num + sizeHint;
					if (num4 > 2147483591)
					{
						ThrowHelper.ThrowOutOfMemoryException_BufferMaximumSizeExceeded((uint)num4);
					}
				}
				byte[] rentedBuffer = this._rentedBuffer;
				this._rentedBuffer = ArrayPool<byte>.Shared.Rent(num4);
				Span<byte> span = rentedBuffer.AsSpan(0, this._index);
				span.CopyTo(this._rentedBuffer);
				span.Clear();
				ArrayPool<byte>.Shared.Return(rentedBuffer, false);
			}
		}

		// Token: 0x040000C3 RID: 195
		private byte[] _rentedBuffer;

		// Token: 0x040000C4 RID: 196
		private int _index;

		// Token: 0x040000C5 RID: 197
		private const int MinimumBufferSize = 256;

		// Token: 0x040000C6 RID: 198
		public const int MaximumBufferSize = 2147483591;
	}
}
