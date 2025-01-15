using System;
using System.Runtime.CompilerServices;

namespace System.Buffers
{
	// Token: 0x020000E7 RID: 231
	internal abstract class MemoryManager<T> : IMemoryOwner<T>, IDisposable, IPinnable
	{
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x00023050 File Offset: 0x00021250
		public virtual Memory<T> Memory
		{
			get
			{
				return new Memory<T>(this, this.GetSpan().Length);
			}
		}

		// Token: 0x0600081E RID: 2078
		public abstract Span<T> GetSpan();

		// Token: 0x0600081F RID: 2079
		public abstract MemoryHandle Pin(int elementIndex = 0);

		// Token: 0x06000820 RID: 2080
		public abstract void Unpin();

		// Token: 0x06000821 RID: 2081 RVA: 0x00023078 File Offset: 0x00021278
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected Memory<T> CreateMemory(int length)
		{
			return new Memory<T>(this, length);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00023084 File Offset: 0x00021284
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected Memory<T> CreateMemory(int start, int length)
		{
			return new Memory<T>(this, start, length);
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x00023090 File Offset: 0x00021290
		protected internal virtual bool TryGetArray(out ArraySegment<T> segment)
		{
			segment = default(ArraySegment<T>);
			return false;
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0002309C File Offset: 0x0002129C
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000825 RID: 2085
		protected abstract void Dispose(bool disposing);
	}
}
