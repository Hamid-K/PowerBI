using System;
using System.Collections.Generic;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200011D RID: 285
	public interface IPosTagger
	{
		// Token: 0x060005D5 RID: 1493
		IEnumerable<IPosTaggerToken> Tag(IEnumerable<ISpellCorrectedToken> spellCorrectedTokens, IPosTaggerCache posTaggerCache = null);
	}
}
