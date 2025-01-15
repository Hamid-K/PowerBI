using System;
using System.Collections;

namespace Microsoft.DataShaping.Common.Json
{
	// Token: 0x0200001C RID: 28
	internal static class FlagSequenceConverter
	{
		// Token: 0x060000EC RID: 236 RVA: 0x00003FE0 File Offset: 0x000021E0
		internal static long ConvertToNumber(BitArray flags)
		{
			long num = 0L;
			for (int i = 0; i < flags.Count; i++)
			{
				if (flags[i])
				{
					num |= 1L << i;
				}
			}
			return num;
		}

		// Token: 0x04000049 RID: 73
		internal static int MaxEncodingBits = 53;
	}
}
