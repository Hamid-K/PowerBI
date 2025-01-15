using System;
using System.Threading;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200034F RID: 847
	internal class OptimizedThreadSafeCounter
	{
		// Token: 0x06001E00 RID: 7680 RVA: 0x00059EDD File Offset: 0x000580DD
		public OptimizedThreadSafeCounter(int startValue)
		{
			this.counter = startValue;
		}

		// Token: 0x06001E01 RID: 7681 RVA: 0x00059EEC File Offset: 0x000580EC
		public int Next()
		{
			int num = Interlocked.Increment(ref this.counter);
			if (num < 0)
			{
				num = 0;
				Interlocked.Exchange(ref this.counter, num);
			}
			return num;
		}

		// Token: 0x06001E02 RID: 7682 RVA: 0x00059F19 File Offset: 0x00058119
		public void Set(int newSeed)
		{
			Interlocked.Exchange(ref this.counter, newSeed);
		}

		// Token: 0x040010E1 RID: 4321
		private int counter;
	}
}
