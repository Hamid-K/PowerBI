using System;

namespace System.Buffers
{
	// Token: 0x0200001F RID: 31
	public abstract class MemoryPool<T> : IDisposable
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000186 RID: 390 RVA: 0x0000A541 File Offset: 0x00008741
		public static MemoryPool<T> Shared
		{
			get
			{
				return MemoryPool<T>.s_shared;
			}
		}

		// Token: 0x06000187 RID: 391
		public abstract IMemoryOwner<T> Rent(int minBufferSize = -1);

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000188 RID: 392
		public abstract int MaxBufferSize { get; }

		// Token: 0x0600018A RID: 394 RVA: 0x0000A548 File Offset: 0x00008748
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600018B RID: 395
		protected abstract void Dispose(bool disposing);

		// Token: 0x0400006E RID: 110
		private static readonly MemoryPool<T> s_shared = new ArrayMemoryPool<T>();
	}
}
