using System;

namespace System.Buffers
{
	// Token: 0x02000028 RID: 40
	internal sealed class ArrayBufferWriter<T> : IBufferWriter<T>
	{
		// Token: 0x06000147 RID: 327 RVA: 0x0000339C File Offset: 0x0000159C
		public ArrayBufferWriter()
		{
			this._buffer = Array.Empty<T>();
			this._index = 0;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000033B6 File Offset: 0x000015B6
		public ArrayBufferWriter(int initialCapacity)
		{
			if (initialCapacity <= 0)
			{
				throw new ArgumentException(null, "initialCapacity");
			}
			this._buffer = new T[initialCapacity];
			this._index = 0;
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000149 RID: 329 RVA: 0x000033E1 File Offset: 0x000015E1
		public ReadOnlyMemory<T> WrittenMemory
		{
			get
			{
				return this._buffer.AsMemory(0, this._index);
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600014A RID: 330 RVA: 0x000033FA File Offset: 0x000015FA
		public ReadOnlySpan<T> WrittenSpan
		{
			get
			{
				return this._buffer.AsSpan(0, this._index);
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00003413 File Offset: 0x00001613
		public int WrittenCount
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600014C RID: 332 RVA: 0x0000341B File Offset: 0x0000161B
		public int Capacity
		{
			get
			{
				return this._buffer.Length;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00003425 File Offset: 0x00001625
		public int FreeCapacity
		{
			get
			{
				return this._buffer.Length - this._index;
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00003438 File Offset: 0x00001638
		public void Clear()
		{
			this._buffer.AsSpan(0, this._index).Clear();
			this._index = 0;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00003466 File Offset: 0x00001666
		public void ResetWrittenCount()
		{
			this._index = 0;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000346F File Offset: 0x0000166F
		public void Advance(int count)
		{
			if (count < 0)
			{
				throw new ArgumentException(null, "count");
			}
			if (this._index > this._buffer.Length - count)
			{
				ArrayBufferWriter<T>.ThrowInvalidOperationException_AdvancedTooFar(this._buffer.Length);
			}
			this._index += count;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000034AE File Offset: 0x000016AE
		public Memory<T> GetMemory(int sizeHint = 0)
		{
			this.CheckAndResizeBuffer(sizeHint);
			return this._buffer.AsMemory(this._index);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000034C8 File Offset: 0x000016C8
		public Span<T> GetSpan(int sizeHint = 0)
		{
			this.CheckAndResizeBuffer(sizeHint);
			return this._buffer.AsSpan(this._index);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000034E4 File Offset: 0x000016E4
		private void CheckAndResizeBuffer(int sizeHint)
		{
			if (sizeHint < 0)
			{
				throw new ArgumentException("sizeHint");
			}
			if (sizeHint == 0)
			{
				sizeHint = 1;
			}
			if (sizeHint > this.FreeCapacity)
			{
				int num = this._buffer.Length;
				int num2 = Math.Max(sizeHint, num);
				if (num == 0)
				{
					num2 = Math.Max(num2, 256);
				}
				int num3 = num + num2;
				if (num3 > 2147483647)
				{
					uint num4 = (uint)(num - this.FreeCapacity + sizeHint);
					if (num4 > 2147483591U)
					{
						ArrayBufferWriter<T>.ThrowOutOfMemoryException(num4);
					}
					num3 = 2147483591;
				}
				Array.Resize<T>(ref this._buffer, num3);
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00003566 File Offset: 0x00001766
		private static void ThrowInvalidOperationException_AdvancedTooFar(int capacity)
		{
			throw new InvalidOperationException(SR.Format(SR.BufferWriterAdvancedTooFar, capacity));
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000357D File Offset: 0x0000177D
		private static void ThrowOutOfMemoryException(uint capacity)
		{
			throw new OutOfMemoryException(SR.Format(SR.BufferMaximumSizeExceeded, capacity));
		}

		// Token: 0x040000BA RID: 186
		private const int ArrayMaxLength = 2147483591;

		// Token: 0x040000BB RID: 187
		private const int DefaultInitialBufferSize = 256;

		// Token: 0x040000BC RID: 188
		private T[] _buffer;

		// Token: 0x040000BD RID: 189
		private int _index;
	}
}
