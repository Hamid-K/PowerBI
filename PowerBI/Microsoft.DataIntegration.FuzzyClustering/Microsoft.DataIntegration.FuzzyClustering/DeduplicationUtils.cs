using System;

namespace Microsoft.DataIntegration.FuzzyClustering
{
	// Token: 0x0200000E RID: 14
	internal static class DeduplicationUtils
	{
		// Token: 0x06000040 RID: 64 RVA: 0x0000304D File Offset: 0x0000124D
		internal static long GetKey(FuzzyLookupMatch match)
		{
			return DeduplicationUtils.GetKey(match.DedupId, match.MatchDedupId);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003060 File Offset: 0x00001260
		internal static long GetKey(int valueId, int matchValueId)
		{
			if (valueId > matchValueId)
			{
				int num = valueId;
				valueId = matchValueId;
				matchValueId = num;
			}
			long num2 = (long)valueId;
			long num3 = (long)matchValueId;
			return (num2 << 32) | num3;
		}
	}
}
