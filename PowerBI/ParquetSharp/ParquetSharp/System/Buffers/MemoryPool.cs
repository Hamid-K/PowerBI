using System;

namespace System.Buffers
{
	// Token: 0x020000DD RID: 221
	internal abstract class MemoryPool<T> : IDisposable
	{
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x00021B48 File Offset: 0x0001FD48
		public static MemoryPool<T> Shared
		{
			get
			{
				return MemoryPool<T>.s_shared;
			}
		}

		// Token: 0x060007C7 RID: 1991
		public abstract IMemoryOwner<T> Rent(int minBufferSize = -1);

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060007C8 RID: 1992
		public abstract int MaxBufferSize { get; }

		// Token: 0x060007CA RID: 1994 RVA: 0x00021B58 File Offset: 0x0001FD58
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060007CB RID: 1995
		protected abstract void Dispose(bool disposing);

		// Token: 0x04000246 RID: 582
		private static readonly MemoryPool<T> s_shared = new ArrayMemoryPool<T>();
	}
}
