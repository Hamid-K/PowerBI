using System;
using System.Runtime.CompilerServices;

namespace System.Buffers
{
	// Token: 0x02000029 RID: 41
	public abstract class MemoryManager<T> : IMemoryOwner<T>, IDisposable, IPinnable
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000B7D8 File Offset: 0x000099D8
		public virtual Memory<T> Memory
		{
			get
			{
				return new Memory<T>(this, this.GetSpan().Length);
			}
		}

		// Token: 0x060001DE RID: 478
		public abstract Span<T> GetSpan();

		// Token: 0x060001DF RID: 479
		public abstract MemoryHandle Pin(int elementIndex = 0);

		// Token: 0x060001E0 RID: 480
		public abstract void Unpin();

		// Token: 0x060001E1 RID: 481 RVA: 0x0000B7F9 File Offset: 0x000099F9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected Memory<T> CreateMemory(int length)
		{
			return new Memory<T>(this, length);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000B802 File Offset: 0x00009A02
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected Memory<T> CreateMemory(int start, int length)
		{
			return new Memory<T>(this, start, length);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000B80C File Offset: 0x00009A0C
		protected internal virtual bool TryGetArray(out ArraySegment<T> segment)
		{
			segment = default(ArraySegment<T>);
			return false;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000B816 File Offset: 0x00009A16
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060001E5 RID: 485
		protected abstract void Dispose(bool disposing);
	}
}
