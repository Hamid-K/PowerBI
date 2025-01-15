using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000273 RID: 627
	public sealed class ThreadSafeRandom
	{
		// Token: 0x0600108B RID: 4235 RVA: 0x00039994 File Offset: 0x00037B94
		public ThreadSafeRandom()
		{
			if (ThreadSafeRandom._local == null)
			{
				Random global = ThreadSafeRandom._global;
				int num;
				lock (global)
				{
					num = ThreadSafeRandom._global.Next();
				}
				ThreadSafeRandom._local = new Random(num);
			}
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x000399F0 File Offset: 0x00037BF0
		public int Next(int max)
		{
			return ThreadSafeRandom._local.Next(max);
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x000399FD File Offset: 0x00037BFD
		public double NextDouble()
		{
			return ThreadSafeRandom._local.NextDouble();
		}

		// Token: 0x0400062D RID: 1581
		private static readonly Random _global = new Random();

		// Token: 0x0400062E RID: 1582
		[ThreadStatic]
		private static Random _local;
	}
}
