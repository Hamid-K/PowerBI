using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200008F RID: 143
	public interface IInvertedIndexLookup
	{
		// Token: 0x060005C0 RID: 1472
		bool TryGetSignatureRidList(ISession session, int lookupId, int hashTableIndex, int signature, ref int[] ridBuffer, out RidList ridList);
	}
}
