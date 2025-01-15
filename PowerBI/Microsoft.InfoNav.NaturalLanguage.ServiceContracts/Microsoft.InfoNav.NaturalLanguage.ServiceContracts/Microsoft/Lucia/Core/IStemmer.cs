using System;
using System.Collections.Generic;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000123 RID: 291
	public interface IStemmer
	{
		// Token: 0x060005DB RID: 1499
		IEnumerable<IStemmerToken> Stem(IEnumerable<IPosTaggerToken> posTaggerTokens, StemmingOptions options = StemmingOptions.All, ILemmatizerCache lemmatizerCache = null);
	}
}
