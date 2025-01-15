using System;

namespace Microsoft.DI.RoaringBitmap.Utilities
{
	// Token: 0x0200000B RID: 11
	internal struct Pair<T> where T : struct
	{
		// Token: 0x06000045 RID: 69 RVA: 0x00003206 File Offset: 0x00001406
		public Pair(T high, T low)
		{
			this.high = high;
			this.low = low;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00003216 File Offset: 0x00001416
		public T High
		{
			get
			{
				return this.high;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000047 RID: 71 RVA: 0x0000321E File Offset: 0x0000141E
		public T Low
		{
			get
			{
				return this.low;
			}
		}

		// Token: 0x0400001E RID: 30
		private readonly T high;

		// Token: 0x0400001F RID: 31
		private readonly T low;
	}
}
