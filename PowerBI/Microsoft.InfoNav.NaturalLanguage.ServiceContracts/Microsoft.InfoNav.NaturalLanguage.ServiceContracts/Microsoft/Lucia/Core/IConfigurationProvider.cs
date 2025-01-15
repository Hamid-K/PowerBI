using System;
using System.Collections.Generic;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000091 RID: 145
	public interface IConfigurationProvider
	{
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000291 RID: 657
		int MaxInterpretations { get; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000292 RID: 658
		bool AllowMultipleEntityInstances { get; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000293 RID: 659
		bool AllowPartialMatchForModelTerms { get; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000294 RID: 660
		ISet<LanguageIdentifier> SupportedLanguages { get; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000295 RID: 661
		ISet<LanguageIdentifier> SupportedLanguagesForCortana { get; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000296 RID: 662
		IReadOnlyDictionary<LanguageIdentifier, GrammarType> LanguageGrammarTypes { get; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000297 RID: 663
		bool FastHeuristicsPipeline { get; }
	}
}
