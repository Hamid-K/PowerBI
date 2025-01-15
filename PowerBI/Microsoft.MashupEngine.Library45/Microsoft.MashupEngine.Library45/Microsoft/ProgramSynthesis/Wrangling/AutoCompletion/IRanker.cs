using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Transformation.Text.Autocompletion;

namespace Microsoft.ProgramSynthesis.Wrangling.AutoCompletion
{
	// Token: 0x02000247 RID: 583
	public interface IRanker
	{
		// Token: 0x06000C6C RID: 3180
		IList<CompletionResultWithIndex> Rank(IEnumerable<CompletionResultWithIndex> completionResults);
	}
}
