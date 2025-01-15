using System;
using System.Buffers;

namespace Azure.Core
{
	// Token: 0x02000012 RID: 18
	internal sealed class ArrayBufferWriter<T> : IBufferWriter<T>
	{
		// Token: 0x0600003C RID: 60 RVA: 0x000025FE File Offset: 0x000007FE
		public ArrayBufferWriter()
		{
			this._buffer = Array.Empty<T>();
			this.WrittenCount = 0;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002618 File Offset: 0x00000818
		public ArrayBufferWriter(int initialCapacity)
		{
			if (initialCapacity <= 0)
			{
				throw new ArgumentException("initialCapacity");
			}
			this._buffer = new T[initialCapacity];
			this.WrittenCount = 0;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002642 File Offset: 0x00000842
		public ReadOnlyMemory<T> WrittenMemory
		{
			get
			{
				return MemoryExtensions.AsMemory<T>(this._buffer, 0, this.WrittenCount);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003F RID: 63 RVA: 0x0000265B File Offset: 0x0000085B
		public ReadOnlySpan<T> WrittenSpan
		{
			get
			{
				return MemoryExtensions.AsSpan<T>(this._buffer, 0, this.WrittenCount);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002674 File Offset: 0x00000874
		// (set) Token: 0x06000041 RID: 65 RVA: 0x0000267C File Offset: 0x0000087C
		public int WrittenCount { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002685 File Offset: 0x00000885
		public int Capacity
		{
			get
			{
				return this._buffer.Length;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000043 RID: 67 RVA: 0x0000268F File Offset: 0x0000088F
		public int FreeCapacity
		{
			get
			{
				return this._buffer.Length - this.WrittenCount;
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000026A0 File Offset: 0x000008A0
		public void Clear()
		{
			MemoryExtensions.AsSpan<T>(this._buffer, 0, this.WrittenCount).Clear();
			this.WrittenCount = 0;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000026CE File Offset: 0x000008CE
		public void Advance(int count)
		{
			if (count < 0)
			{
				throw new ArgumentException("count");
			}
			if (this.WrittenCount > this._buffer.Length - count)
			{
				ArrayBufferWriter<T>.ThrowInvalidOperationException_AdvancedTooFar(this._buffer.Length);
			}
			this.WrittenCount += count;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000270C File Offset: 0x0000090C
		public Memory<T> GetMemory(int sizeHint = 0)
		{
			this.CheckAndResizeBuffer(sizeHint);
			return MemoryExtensions.AsMemory<T>(this._buffer, this.WrittenCount);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002726 File Offset: 0x00000926
		public Span<T> GetSpan(int sizeHint = 0)
		{
			this.CheckAndResizeBuffer(sizeHint);
			return MemoryExtensions.AsSpan<T>(this._buffer, this.WrittenCount);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002740 File Offset: 0x00000940
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
				int num = Math.Max(sizeHint, this._buffer.Length);
				if (this._buffer.Length == 0)
				{
					num = Math.Max(num, 256);
				}
				int num2 = checked(this._buffer.Length + num);
				Array.Resize<T>(ref this._buffer, num2);
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000027A6 File Offset: 0x000009A6
		private static void ThrowInvalidOperationException_AdvancedTooFar(int capacity)
		{
			throw new InvalidOperationException(string.Format("Advanced past capacity of {0}", capacity));
		}

		// Token: 0x04000028 RID: 40
		private T[] _buffer;

		// Token: 0x04000029 RID: 41
		private const int DefaultInitialBufferSize = 256;
	}
}
