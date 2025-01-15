using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Buffers
{
	// Token: 0x020000F2 RID: 242
	internal abstract class ArrayPool<T>
	{
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x0002B0F4 File Offset: 0x000292F4
		public static ArrayPool<T> Shared
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Volatile.Read<ArrayPool<T>>(ref ArrayPool<T>.s_sharedInstance) ?? ArrayPool<T>.EnsureSharedCreated();
			}
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0002B10C File Offset: 0x0002930C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static ArrayPool<T> EnsureSharedCreated()
		{
			Interlocked.CompareExchange<ArrayPool<T>>(ref ArrayPool<T>.s_sharedInstance, ArrayPool<T>.Create(), null);
			return ArrayPool<T>.s_sharedInstance;
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0002B124 File Offset: 0x00029324
		public static ArrayPool<T> Create()
		{
			return new DefaultArrayPool<T>();
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0002B12C File Offset: 0x0002932C
		public static ArrayPool<T> Create(int maxArrayLength, int maxArraysPerBucket)
		{
			return new DefaultArrayPool<T>(maxArrayLength, maxArraysPerBucket);
		}

		// Token: 0x060008EF RID: 2287
		public abstract T[] Rent(int minimumLength);

		// Token: 0x060008F0 RID: 2288
		public abstract void Return(T[] array, bool clearArray = false);

		// Token: 0x040002B0 RID: 688
		private static ArrayPool<T> s_sharedInstance;
	}
}
