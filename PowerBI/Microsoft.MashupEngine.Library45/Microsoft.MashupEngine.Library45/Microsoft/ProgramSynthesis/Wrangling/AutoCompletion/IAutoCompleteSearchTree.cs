using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Wrangling.AutoCompletion.SearchTree;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.AutoCompletion
{
	// Token: 0x02000246 RID: 582
	public interface IAutoCompleteSearchTree : IPrefixSearchTree<char, string, StringSubSequence, List<CompletionInfo>>
	{
		// Token: 0x06000C6B RID: 3179
		void Add(string key, CompletionInfo value);
	}
}
