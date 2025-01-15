using System;
using System.Collections.Generic;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x020004BC RID: 1212
	public interface ICanGetSummaryInKeyValuePairs
	{
		// Token: 0x060018EA RID: 6378
		KeyValuePair<string, object>[] GetSummaryInKeyValuePairs(FeatureNameCollection names);
	}
}
