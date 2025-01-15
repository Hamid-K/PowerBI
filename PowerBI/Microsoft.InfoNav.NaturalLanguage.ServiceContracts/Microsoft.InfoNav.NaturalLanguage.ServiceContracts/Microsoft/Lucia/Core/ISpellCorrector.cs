using System;
using System.Collections.Generic;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000121 RID: 289
	public interface ISpellCorrector
	{
		// Token: 0x060005DA RID: 1498
		IEnumerable<ISpellCorrectedToken> SpellCorrect(IEnumerable<IToken> tokens);
	}
}
