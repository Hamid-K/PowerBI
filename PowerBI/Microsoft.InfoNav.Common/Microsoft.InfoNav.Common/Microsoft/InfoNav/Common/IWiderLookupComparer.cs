using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x0200005D RID: 93
	internal interface IWiderLookupComparer<TKey> : IEqualityComparer<TKey>
	{
		// Token: 0x06000396 RID: 918
		bool LookupEquals(TKey lookup, TKey entry);
	}
}
