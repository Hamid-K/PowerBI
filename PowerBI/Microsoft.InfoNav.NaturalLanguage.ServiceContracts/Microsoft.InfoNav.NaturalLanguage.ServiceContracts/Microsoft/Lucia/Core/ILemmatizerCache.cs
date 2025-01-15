using System;
using System.Collections.Generic;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200011B RID: 283
	public interface ILemmatizerCache
	{
		// Token: 0x060005D0 RID: 1488
		bool TryGet(string value, StemmingOptions options, PosTagKind posTagKind, out IReadOnlyList<StemmerSuggestion> result);

		// Token: 0x060005D1 RID: 1489
		void Add(string value, StemmingOptions options, PosTagKind posTagKind, IReadOnlyList<StemmerSuggestion> result);
	}
}
