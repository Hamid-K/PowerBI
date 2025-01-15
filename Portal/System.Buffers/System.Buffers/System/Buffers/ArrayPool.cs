using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Buffers
{
	// Token: 0x02000004 RID: 4
	public abstract class ArrayPool<T>
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002186 File Offset: 0x00000386
		public static ArrayPool<T> Shared
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Volatile.Read<ArrayPool<T>>(ref ArrayPool<T>.s_sharedInstance) ?? ArrayPool<T>.EnsureSharedCreated();
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000219B File Offset: 0x0000039B
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static ArrayPool<T> EnsureSharedCreated()
		{
			Interlocked.CompareExchange<ArrayPool<T>>(ref ArrayPool<T>.s_sharedInstance, ArrayPool<T>.Create(), null);
			return ArrayPool<T>.s_sharedInstance;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021B3 File Offset: 0x000003B3
		public static ArrayPool<T> Create()
		{
			return new DefaultArrayPool<T>();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021BA File Offset: 0x000003BA
		public static ArrayPool<T> Create(int maxArrayLength, int maxArraysPerBucket)
		{
			return new DefaultArrayPool<T>(maxArrayLength, maxArraysPerBucket);
		}

		// Token: 0x0600000F RID: 15
		public abstract T[] Rent(int minimumLength);

		// Token: 0x06000010 RID: 16
		public abstract void Return(T[] array, bool clearArray = false);

		// Token: 0x04000003 RID: 3
		private static ArrayPool<T> s_sharedInstance;
	}
}
