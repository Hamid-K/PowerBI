using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ProgramSynthesis.Wrangling.Logging;

namespace Microsoft.ProgramSynthesis.Wrangling.AutoCompletion
{
	// Token: 0x02000245 RID: 581
	public interface IAutoCompleter
	{
		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000C67 RID: 3175
		ILogger Logger { get; }

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000C68 RID: 3176
		IRanker Ranker { get; }

		// Token: 0x06000C69 RID: 3177
		Task<IReadOnlyList<ISuggestion>> SuggestAsync(string prefix, int minimumSuggestionCount, int maximumSuggestionCount);

		// Token: 0x06000C6A RID: 3178
		void ConfirmSuggestion(string suggestionEventId, int suggestionIndex);
	}
}
