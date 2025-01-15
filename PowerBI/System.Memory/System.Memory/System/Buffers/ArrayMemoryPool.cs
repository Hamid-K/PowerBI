using System;
using System.Runtime.CompilerServices;

namespace System.Buffers
{
	// Token: 0x0200001C RID: 28
	internal sealed class ArrayMemoryPool<T> : MemoryPool<T>
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000178 RID: 376 RVA: 0x0000A2F2 File Offset: 0x000084F2
		public sealed override int MaxBufferSize
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000A2F9 File Offset: 0x000084F9
		public sealed override IMemoryOwner<T> Rent(int minimumBufferSize = -1)
		{
			if (minimumBufferSize == -1)
			{
				minimumBufferSize = 1 + 4095 / Unsafe.SizeOf<T>();
			}
			else if (minimumBufferSize > 2147483647)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.minimumBufferSize);
			}
			return new ArrayMemoryPool<T>.ArrayMemoryPoolBuffer(minimumBufferSize);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00002C58 File Offset: 0x00000E58
		protected sealed override void Dispose(bool disposing)
		{
		}

		// Token: 0x0400006D RID: 109
		private const int s_maxBufferSize = 2147483647;

		// Token: 0x0200003B RID: 59
		private sealed class ArrayMemoryPoolBuffer : IMemoryOwner<T>, IDisposable
		{
			// Token: 0x060002B6 RID: 694 RVA: 0x00012A07 File Offset: 0x00010C07
			public ArrayMemoryPoolBuffer(int size)
			{
				this._array = ArrayPool<T>.Shared.Rent(size);
			}

			// Token: 0x17000045 RID: 69
			// (get) Token: 0x060002B7 RID: 695 RVA: 0x00012A20 File Offset: 0x00010C20
			public Memory<T> Memory
			{
				get
				{
					T[] array = this._array;
					if (array == null)
					{
						ThrowHelper.ThrowObjectDisposedException_ArrayMemoryPoolBuffer();
					}
					return new Memory<T>(array);
				}
			}

			// Token: 0x060002B8 RID: 696 RVA: 0x00012A44 File Offset: 0x00010C44
			public void Dispose()
			{
				T[] array = this._array;
				if (array != null)
				{
					this._array = null;
					ArrayPool<T>.Shared.Return(array, false);
				}
			}

			// Token: 0x040000E5 RID: 229
			private T[] _array;
		}
	}
}
