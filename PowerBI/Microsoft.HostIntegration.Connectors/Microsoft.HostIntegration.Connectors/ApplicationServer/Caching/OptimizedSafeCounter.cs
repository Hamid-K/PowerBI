using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200034E RID: 846
	internal class OptimizedSafeCounter
	{
		// Token: 0x06001DFD RID: 7677 RVA: 0x00059E9F File Offset: 0x0005809F
		public OptimizedSafeCounter(int maximumValue)
		{
			this.maximumValue = maximumValue;
			this.counter = new OptimizedThreadSafeCounter(-1);
		}

		// Token: 0x06001DFE RID: 7678 RVA: 0x00059EBA File Offset: 0x000580BA
		public int Next()
		{
			return this.Next(this.maximumValue);
		}

		// Token: 0x06001DFF RID: 7679 RVA: 0x00059EC8 File Offset: 0x000580C8
		public int Next(int maxVal)
		{
			if (maxVal == 1)
			{
				return 0;
			}
			return this.counter.Next() % maxVal;
		}

		// Token: 0x040010DF RID: 4319
		private OptimizedThreadSafeCounter counter;

		// Token: 0x040010E0 RID: 4320
		private int maximumValue;
	}
}
