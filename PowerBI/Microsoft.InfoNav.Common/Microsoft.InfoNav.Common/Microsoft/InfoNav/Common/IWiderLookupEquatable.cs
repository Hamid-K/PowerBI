using System;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x0200005E RID: 94
	public interface IWiderLookupEquatable
	{
		// Token: 0x06000397 RID: 919
		bool WiderEquals(IWiderLookupEquatable entry);

		// Token: 0x06000398 RID: 920
		bool WiderLookupEquals(IWiderLookupEquatable entry);

		// Token: 0x06000399 RID: 921
		bool LookupEquals(IWiderLookupEquatable lookup);

		// Token: 0x0600039A RID: 922
		int GetWiderHashCode();
	}
}
