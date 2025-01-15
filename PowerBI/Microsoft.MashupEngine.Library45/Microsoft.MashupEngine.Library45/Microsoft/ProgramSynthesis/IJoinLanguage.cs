using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Rules;

namespace Microsoft.ProgramSynthesis
{
	// Token: 0x0200008A RID: 138
	public interface IJoinLanguage : ILanguage
	{
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600030D RID: 781
		IEnumerable<ILanguage> JoinedLanguages { get; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600030E RID: 782
		NonterminalRule LanguageRule { get; }
	}
}
