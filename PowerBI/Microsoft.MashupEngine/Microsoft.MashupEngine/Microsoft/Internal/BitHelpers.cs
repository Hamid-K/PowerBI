using System;

namespace Microsoft.Internal
{
	// Token: 0x02000180 RID: 384
	internal static class BitHelpers
	{
		// Token: 0x06000744 RID: 1860 RVA: 0x0000CA7D File Offset: 0x0000AC7D
		public static bool GetBit(this int value, int bit)
		{
			return (value & (1 << bit)) != 0;
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0000CA8A File Offset: 0x0000AC8A
		public static int SetBit(this int value, int bit)
		{
			return value | (1 << bit);
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0000CA94 File Offset: 0x0000AC94
		public static int ResetBit(this int value, int bit)
		{
			return value & ~(1 << bit);
		}
	}
}
