using System;
using System.Runtime.CompilerServices;

namespace System.Buffers
{
	// Token: 0x020000DA RID: 218
	internal sealed class ArrayMemoryPool<T> : MemoryPool<T>
	{
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x000218B0 File Offset: 0x0001FAB0
		public sealed override int MaxBufferSize
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x000218B8 File Offset: 0x0001FAB8
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

		// Token: 0x060007BA RID: 1978 RVA: 0x000218EC File Offset: 0x0001FAEC
		protected sealed override void Dispose(bool disposing)
		{
		}

		// Token: 0x04000245 RID: 581
		private const int s_maxBufferSize = 2147483647;

		// Token: 0x0200014C RID: 332
		private sealed class ArrayMemoryPoolBuffer : IMemoryOwner<T>, IDisposable
		{
			// Token: 0x06000A23 RID: 2595 RVA: 0x0002C654 File Offset: 0x0002A854
			public ArrayMemoryPoolBuffer(int size)
			{
				this._array = ArrayPool<T>.Shared.Rent(size);
			}

			// Token: 0x1700011A RID: 282
			// (get) Token: 0x06000A24 RID: 2596 RVA: 0x0002C670 File Offset: 0x0002A870
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

			// Token: 0x06000A25 RID: 2597 RVA: 0x0002C69C File Offset: 0x0002A89C
			public void Dispose()
			{
				T[] array = this._array;
				if (array != null)
				{
					this._array = null;
					ArrayPool<T>.Shared.Return(array, false);
				}
			}

			// Token: 0x04000330 RID: 816
			private T[] _array;
		}
	}
}
